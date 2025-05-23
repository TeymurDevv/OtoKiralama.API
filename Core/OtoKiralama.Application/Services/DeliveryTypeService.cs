﻿using AutoMapper;
using OtoKiralama.Application.Dtos.DeliveryType;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Application.Services
{
    public class DeliveryTypeService : IDeliveryTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeliveryTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateDeliveryTypeAsync(DeliveryTypeCreateDto deliveryTypeCreateDto)
        {

            var isExistDeliveryType = await _unitOfWork.DeliveryTypeRepository.isExists(d => d.Name == deliveryTypeCreateDto.Name);
            if (isExistDeliveryType)
                throw new CustomException(400, "Name", "Delivery type already exist with this name");
            var deliveryType = _mapper.Map<DeliveryType>(deliveryTypeCreateDto);
            await _unitOfWork.DeliveryTypeRepository.Create(deliveryType);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteDeliveryTypeAsync(int id)
        {
            var existDeliveryType = await _unitOfWork.DeliveryTypeRepository.GetEntity(d => d.Id == id);
            if (existDeliveryType is null)
                throw new CustomException(404, "Id", "Delivery type not found with this Id");
            await _unitOfWork.DeliveryTypeRepository.Delete(existDeliveryType);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedResponse<DeliveryTypeListItemDto>> GetAllDeliveryTypesAsync(int pageNumber, int pageSize)
        {
            int totalDeliveryTypes = await _unitOfWork.DeliveryTypeRepository.CountAsync();
            var deliveryTypes = await _unitOfWork.DeliveryTypeRepository.GetAll(
                includes: query => query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                );
            return new PagedResponse<DeliveryTypeListItemDto>
            {
                Data = _mapper.Map<List<DeliveryTypeListItemDto>>(deliveryTypes),
                TotalCount = totalDeliveryTypes,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<DeliveryTypeReturnDto> GetDeliveryTypeByIdAsync(int id)
        {
            var deliveryType = await _unitOfWork.DeliveryTypeRepository.GetEntity(d => d.Id == id);
            if (deliveryType is null)
                throw new CustomException(404, "Id", "Delivery type not found with this Id");
            return _mapper.Map<DeliveryTypeReturnDto>(deliveryType);
        }
        public async Task UpdateAsync(int? id, DeliveryTypeUpdateDto deliveryTypeUpdateDto)
        {
            if (id is null)
                throw new CustomException(400, "Id", "Id can not be left empty");
            var existedDeliveryType = await _unitOfWork.DeliveryTypeRepository.GetEntity(s => s.Id == id);
            if (existedDeliveryType is null)
                throw new CustomException(404, "DeliveryType", "Not found");
            var isExistedDeliveryType = await _unitOfWork.DeliveryTypeRepository.isExists(s => s.Name.ToLower() == deliveryTypeUpdateDto.Name.ToLower());
            if (isExistedDeliveryType)
                throw new CustomException(400, "Name", "This DeliveryType name already exists");


            _mapper.Map(deliveryTypeUpdateDto, existedDeliveryType);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
