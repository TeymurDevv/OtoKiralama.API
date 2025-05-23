﻿using AutoMapper;
using OtoKiralama.Application.Dtos.Fuel;
using OtoKiralama.Application.Dtos.Gear;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Application.Services
{
    public class GearService : IGearService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GearService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateGearAsync(GearCreateDto gearCreateDto)
        {
            var gear = _mapper.Map<Gear>(gearCreateDto);
            var existGear = await _unitOfWork.GearRepository.isExists(g => g.Name == gearCreateDto.Name);
            if (existGear)
                throw new CustomException(400, "Name", "Gear already exist with this name");
            await _unitOfWork.GearRepository.Create(gear);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteGearAsync(int id)
        {
            var gear = await _unitOfWork.GearRepository.GetEntity(g => g.Id == id);
            if (gear is null)
                throw new CustomException(404, "Id", "Gear not found with this Id");
            await _unitOfWork.GearRepository.Delete(gear);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedResponse<GearListItemDto>> GetAllGearsAsync(int pageNumber, int pageSize)
        {
            int totalGears = await _unitOfWork.GearRepository.CountAsync();
            var gears = await _unitOfWork.GearRepository.GetAll(
                includes: query => query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                );
            return new PagedResponse<GearListItemDto>
            {
                Data = _mapper.Map<List<GearListItemDto>>(gears),
                TotalCount = totalGears,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<GearReturnDto> GetGearByIdAsync(int id)
        {
            var gear = await _unitOfWork.GearRepository.GetEntity(g => g.Id == id);
            if (gear is null)
                throw new CustomException(404, "Id", "Gear not found with this Id");
            return _mapper.Map<GearReturnDto>(gear);
        }
        public async Task UpdateAsync(int? id,GearUpdateDto gearUpdateDto)
        {
            if (id is null)
                throw new CustomException(400, "Id", "Id can not be left empty");
            var existedGear = await _unitOfWork.GearRepository.GetEntity(s => s.Id == id);
            if (existedGear is null)
                throw new CustomException(404, "Gear", "Not found");
            var isExistedGear = await _unitOfWork.GearRepository.isExists(s => s.Name.ToLower() == gearUpdateDto.Name.ToLower());
                if (isExistedGear)
                    throw new CustomException(400, "Name", "This Fuel name already exists");
                
            
            _mapper.Map(gearUpdateDto, existedGear);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
