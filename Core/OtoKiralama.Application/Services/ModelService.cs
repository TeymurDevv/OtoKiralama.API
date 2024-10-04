using AutoMapper;
using OtoKiralama.Application.Dtos.Model;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Persistance.Data.Implementations;

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
            var existBrand = await _unitOfWork.BrandRepository.isExists(b => b.Id == modelCreateDto.BrandId);
            if (!existBrand)
                throw new CustomException(404, "BrandId", "Brand not found with this Id");
            var model = _mapper.Map<Model>(modelCreateDto);
            var existModel = await _unitOfWork.ModelRepository.isExists(m => m.Name == modelCreateDto.Name);
            if (existModel)
                throw new CustomException(400, "Name", "Model already exist with this name");
            await _unitOfWork.ModelRepository.Create(model);
            _unitOfWork.Commit();
        }

        public async Task DeleteModelAsync(int id)
        {
            var model = await _unitOfWork.ModelRepository.GetEntity(m => m.Id == id);
            if (model is null)
                throw new CustomException(404, "Id", "Model not found with this Id");
            await _unitOfWork.ModelRepository.Delete(model);
            _unitOfWork.Commit();
        }

        public async Task<PagedResponse<ModelListItemDto>> GetAllModelsAsync(int pageNumber, int pageSize)
        {
            int totalModels = await _unitOfWork.ModelRepository.CountAsync();
            var models = await _unitOfWork.ModelRepository.GetAll(
                includes: query => query
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

        public async Task<ModelReturnDto> GetModelByIdAsync(int id)
        {
            var model = await _unitOfWork.ModelRepository.GetEntity(m => m.Id == id);
            if (model is null)
                throw new CustomException(404, "Id", "Model not found with this Id");
            return _mapper.Map<ModelReturnDto>(model);
        }
    }
}
