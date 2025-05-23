﻿using AutoMapper;
using OtoKiralama.Application.Dtos.Body;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Application.Services
{
    public class BodyService : IBodyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BodyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateBodyAsync(BodyCreateDto bodyCreateDto)
        {
            var existBody = await _unitOfWork.BodyRepository.isExists(b => b.Name.ToLower() == bodyCreateDto.Name.ToLower());

            if (existBody)
                throw new CustomException(400, "Name", "Body already exist with this name");

            var body = _mapper.Map<Body>(bodyCreateDto);
            await _unitOfWork.BodyRepository.Create(body);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task DeleteBodyAsync(int id)
        {
            var body = await _unitOfWork.BodyRepository.GetEntity(b => b.Id == id);
            if (body is null)
                throw new CustomException(404, "Id", "Body not found with this Id");
            await _unitOfWork.BodyRepository.Delete(body);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task UpdateBodyAsync(int id, BodyUpdateDto bodyUpdateDto)
        {
            var body = await _unitOfWork.BodyRepository.GetEntity(b => b.Id == id);
            if (body is null)
                throw new CustomException(404, "Id", "Body not found with this Id");

            var existBody = await _unitOfWork.BodyRepository.isExists(b => b.Name.ToLower() == bodyUpdateDto.Name.ToLower() && b.Id != id);
            if (existBody)
                throw new CustomException(400, "Name", "Another body already exists with this name");

            _mapper.Map(bodyUpdateDto, body);

            _unitOfWork.BodyRepository.Update(body);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedResponse<BodyListItemDto>> GetAllBodiesAsync(int pageNumber, int pageSize)
        {
            int totalBodies = await _unitOfWork.BodyRepository.CountAsync();
            var bodies = await _unitOfWork.BodyRepository.GetAll(
                includes: query => query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                );
            return new PagedResponse<BodyListItemDto>
            {
                Data = _mapper.Map<List<BodyListItemDto>>(bodies),
                TotalCount = totalBodies,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<BodyReturnDto> GetBodyByIdAsync(int id)
        {
            var body = await _unitOfWork.BodyRepository.GetEntity(b => b.Id == id);
            if (body is null)
                throw new CustomException(404, "Id", "Body not found with this Id");
            return _mapper.Map<BodyReturnDto>(body);
        }
    }
}
