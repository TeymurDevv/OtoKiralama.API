using AutoMapper;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Dtos.Setting;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Application.Services
{
    public class SettingService : ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SettingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateSettingAsync(SettingCreateDto settingCreateDto)
        {
            var existSettingWithName = await _unitOfWork.SettingRepository.isExists(s => s.Name == settingCreateDto.Name);
            if (existSettingWithName)
                throw new CustomException(400, "Name", "Setting with this name already exists");
            var existSettingWithKey = await _unitOfWork.SettingRepository.isExists(s => s.Key == settingCreateDto.Key);
            if (existSettingWithKey)
                throw new CustomException(400, "Key", "Setting with this key already exists");
            var setting = _mapper.Map<Setting>(settingCreateDto);
            await _unitOfWork.SettingRepository.Create(setting);
            await _unitOfWork.CommitAsync();
        }

        public async Task<PagedResponse<SettingListItemDto>> GetAllSettingsAsync(int pageNumber, int pageSize)
        {
            int totalSettings = await _unitOfWork.SettingRepository.CountAsync();
            var settings = await _unitOfWork.SettingRepository.GetAll(
                includes: query => query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                );
            return new PagedResponse<SettingListItemDto>
            {
                Data = _mapper.Map<List<SettingListItemDto>>(settings),
                TotalCount = totalSettings,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<SettingReturnDto> GetSettingByKeyAsync(string key)
        {
            var setting = await _unitOfWork.SettingRepository.GetEntity(s => s.Key == key);
            if (setting is null)
                throw new CustomException(404, "Key", "Setting not found with this Key");
            return _mapper.Map<SettingReturnDto>(setting);
        }
        public async Task<SettingReturnDto> GetSettingByIdAsync(int id)
        {
            var setting = await _unitOfWork.SettingRepository.GetEntity(s => s.Id == id);
            if (setting is null)
                throw new CustomException(404, "Id", "Setting not found with this Id");
            return _mapper.Map<SettingReturnDto>(setting);
        }
        public async Task UpdateSettingAsync(int id, SettingUpdateDto settingUpdateDto)
        {
            var existSetting = await _unitOfWork.SettingRepository.GetEntity(s => s.Id == id);
            if (existSetting is null)
                throw new CustomException(404, "Id", "Setting not found with this Id");
            _mapper.Map(settingUpdateDto, existSetting);
            await _unitOfWork.SettingRepository.Update(existSetting);
            await _unitOfWork.CommitAsync();
        }

    }
}
