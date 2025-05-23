﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Application.Dtos.CarPhoto;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Application.Services
{
    public class CarPhotoService : ICarPhotoService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoService _photoService;

        public CarPhotoService(IMapper mapper, IUnitOfWork unitOfWork, IPhotoService photoService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _photoService = photoService;
        }

        public async Task CreateCarPhotoAsync(CarPhotoCreateDto carPhotoCreateDto)
        {
            var existModel = await _unitOfWork.ModelRepository.isExists(m => m.Id == carPhotoCreateDto.ModelId);
            if (!existModel)
                throw new CustomException(404, "ModelId", "Model not found with this Id");
            var eixstCarPhotoWithModelId = await _unitOfWork.CarPhotoRepository.isExists(cp => cp.ModelId == carPhotoCreateDto.ModelId);
            if (eixstCarPhotoWithModelId)
                throw new CustomException(400, "ModelId", "CarPhoto is Already exist with this Model");
            var carPhoto = _mapper.Map<CarPhoto>(carPhotoCreateDto);

            string imageUrl;
            try
            {
                imageUrl = await _photoService.UploadPhotoAsync(carPhotoCreateDto.Image);
            }
            catch (Exception)
            {
                throw new CustomException(500, "ImageUpload", "Failed to upload the image");
            }

            carPhoto.ImageUrl = imageUrl;
            await _unitOfWork.CarPhotoRepository.Create(carPhoto);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task DeleteCarPhotoAsync(int id)
        {
            var carPhoto = await _unitOfWork.CarPhotoRepository.GetEntity(cp => cp.Id == id);
            if (carPhoto is null)
                throw new CustomException(404, "Id", "CarPhoto not found with this Id");
            await _unitOfWork.CarPhotoRepository.Delete(carPhoto);
            await _photoService.DeletePhotoAsync(carPhoto.ImageUrl);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCarPhotoAsync(int id, CarPhotoUpdateDto carPhotoUpdateDto)
        {
            var existCarPhoto = await _unitOfWork.CarPhotoRepository.GetEntity(cp => cp.Id == id);
            if (existCarPhoto is null)
                throw new CustomException(404, "Id", "CarPhoto not found with this Id");

            if (carPhotoUpdateDto.Image != null)
            {
                try
                {
                    string newImageUrl = await _photoService.UploadPhotoAsync(carPhotoUpdateDto.Image);

                    if (!string.IsNullOrEmpty(existCarPhoto.ImageUrl))
                    {
                        await _photoService.DeletePhotoAsync(existCarPhoto.ImageUrl);
                    }

                    existCarPhoto.ImageUrl = newImageUrl;
                }
                catch (Exception ex)
                {
                    throw new CustomException(500, "ImageUpload", "Failed to upload the new image");
                }
            }

            _unitOfWork.CarPhotoRepository.Update(existCarPhoto);
            await _unitOfWork.SaveChangesAsync();
        }




        public async Task<PagedResponse<CarPhotoListItemDto>> GetAllCarPhotosAsync(int pageNumber, int pageSize)
        {
            int totalCarPhotos = await _unitOfWork.CarPhotoRepository.CountAsync();
            var carPhotos = await _unitOfWork.CarPhotoRepository.GetAll(
                includes: query => query
                    .Include(cp => cp.Model)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
            );
            return new PagedResponse<CarPhotoListItemDto>
            {
                Data = _mapper.Map<List<CarPhotoListItemDto>>(carPhotos),
                TotalCount = totalCarPhotos,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<CarPhotoReturnDto> GetCarPhotoByIdAsync(int id)
        {
            var carPhoto = await _unitOfWork.CarPhotoRepository.GetEntity(
                cp => cp.Id == id,
                includes: query => query
                    .Include(cp => cp.Model)
            );

            if (carPhoto is null)
                throw new CustomException(404, "Id", "CarPhoto not found with this Id");

            return _mapper.Map<CarPhotoReturnDto>(carPhoto);
        }
    }
}
