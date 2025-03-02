using AutoMapper;
using OtoKiralama.Application.Dtos.FilterRange;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Application.Services
{
    public class FilterRangeService : IFilterRangeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FilterRangeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateFilterRangeAsync(FilterRangeCreateDto filterRangeCreateDto)
        {
            var isExistFilterRange = await _unitOfWork.FilterRangeRepository.isExists(f => f.Type == filterRangeCreateDto.Type && f.MaxValue == filterRangeCreateDto.MaxValue && f.MinValue == filterRangeCreateDto.MinValue);
            if (isExistFilterRange)
                throw new CustomException(400, "Type", "This filter range already exist");
            var filterRange = _mapper.Map<FilterRange>(filterRangeCreateDto);
            await _unitOfWork.FilterRangeRepository.Create(filterRange);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteFilterRangeAsync(int id)
        {
            var existFilterRange = await _unitOfWork.FilterRangeRepository.GetEntity(f => f.Id == id);
            if (existFilterRange is null)
                throw new CustomException(404, "Id", "Filter range not found with this Id");

            await _unitOfWork.FilterRangeRepository.Delete(existFilterRange);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedResponse<FilterRangeReturnDto>> GetAllFilterRangeAsync(int pageNumber, int pageSize)
        {
            int totalFilterRanges = await _unitOfWork.FilterRangeRepository.CountAsync();
            var filterRanges = await _unitOfWork.FilterRangeRepository.GetAll(
                includes: query => query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                );
            return new PagedResponse<FilterRangeReturnDto>
            {
                Data = _mapper.Map<List<FilterRangeReturnDto>>(filterRanges),
                TotalCount = totalFilterRanges,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<FilterRangeReturnDto> GetFilterRangeByIdAsync(int id)
        {
            var filterRange = await _unitOfWork.FilterRangeRepository.GetEntity(f => f.Id == id);
            if (filterRange is null)
                throw new CustomException(404, "Id", "DFilter range not found with this Id");
            return _mapper.Map<FilterRangeReturnDto>(filterRange);
        }

        public async Task UpdateFilterRangeAsync(int id, FilterRangeUpdateDto filterRangeUpdateDto)
        {
            var existedFilterRange = await _unitOfWork.FilterRangeRepository.GetEntity(s => s.Id == id);
            if (existedFilterRange is null)
                throw new CustomException(404, "FilterRange", "Not found");
            var isExistedFilterRange = await _unitOfWork.FilterRangeRepository.isExists(f => f.Type == filterRangeUpdateDto.Type && f.MaxValue == filterRangeUpdateDto.MaxValue && f.MinValue == filterRangeUpdateDto.MinValue && f.Id != id);
            if (isExistedFilterRange)
                throw new CustomException(400, "FilterRange", "This Filter range already exists");

            _mapper.Map(filterRangeUpdateDto, existedFilterRange);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
