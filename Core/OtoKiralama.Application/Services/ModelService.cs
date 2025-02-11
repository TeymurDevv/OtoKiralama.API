using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OtoKiralama.Application.Dtos.Model;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

namespace OtoKiralama.Application.Services
{
    public class ModelService : IModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ModelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateModelAsync(ModelCreateDto modelCreateDto)
        {
            var existBrand = await _unitOfWork.ModelRepository.isExists(b => b.Id == modelCreateDto.BrandId);
            if (!existBrand)
                throw new CustomException(404, "BrandId", "Brand not found with this Id");
            var model = _mapper.Map<Model>(modelCreateDto);
            var existModel = await _unitOfWork.ModelRepository.isExists(m => m.Name == modelCreateDto.Name);
            if (existModel)
                throw new CustomException(400, "Name", "Model already exist with this name");
            await _unitOfWork.ModelRepository.Create(model);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteModelAsync(int id)
        {
            var model = await _unitOfWork.ModelRepository.GetEntity(m => m.Id == id);
            if (model is null)
                throw new CustomException(404, "Id", "Model not found with this Id");
            await _unitOfWork.ModelRepository.Delete(model);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedResponse<ModelListItemDto>> GetAllModelsAsync(int pageNumber, int pageSize)
        {
            int totalModels = await _unitOfWork.ModelRepository.CountAsync();
            var models = await _unitOfWork.ModelRepository.GetAll(
                includes: query => query
                    .Include(m => m.Brand)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                );
            return new PagedResponse<ModelListItemDto>
            {
                Data = _mapper.Map<List<ModelListItemDto>>(models),
                TotalCount = totalModels,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<PagedResponse<ModelListItemDto>> GetAllModelsByBrandId(int brandId, int pageNumber, int pageSize)
        {
            var models = await _unitOfWork.ModelRepository.GetAll(
                includes: query => query
                 .Include(m => m.Brand)
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize),
                predicate: m => m.Brand.Id == brandId
                );
            int totalModels = models.Count();
            return new PagedResponse<ModelListItemDto>
            {
                Data = _mapper.Map<List<ModelListItemDto>>(models),
                TotalCount = totalModels,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<ModelReturnDto> GetModelByIdAsync(int id)
        {
            var model = await _unitOfWork.ModelRepository.GetEntity(m => m.Id == id);
            if (model is null)
                throw new CustomException(404, "Id", "Model not found with this Id");
            return _mapper.Map<ModelReturnDto>(model);
        }
        public async Task UpdateAsync(int id,ModelUpdateDto modelUpdateDto)
        {
            var existedModel= await _unitOfWork.ModelRepository.GetEntity(s=>s.Id== id);
            if (existedModel is null)
                throw new CustomException(404, "Model", "Not Found");
            var isExistedBrand=await _unitOfWork.BrandRepository.isExists(s=>s.Id == modelUpdateDto.BrandId);
            if (!isExistedBrand)
                throw new CustomException(400, "Brand", "Invalid brand id");
            var isExistedModel=await _unitOfWork.ModelRepository.isExists(s=>s.Name.ToLower()==modelUpdateDto.Name.ToLower());
            if (isExistedModel)
                throw new CustomException(400, "Model", "Model name already exists");
            _mapper.Map(modelUpdateDto, existedModel);
            await _unitOfWork.SaveChangesAsync();
         
        }
    }
}
