using AutoMapper;
using OtoKiralama.Application.Dtos.Class;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Persistance.Data.Implementations;

namespace OtoKiralama.Application.Services
{
    public class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClassService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateClassAsync(ClassCreateDto classCreateDto)
        {
            var @class = _mapper.Map<Class>(classCreateDto);
            var existClass = await _unitOfWork.ClassRepository.isExists(c => c.Name == classCreateDto.Name);
            if (existClass)
                throw new CustomException(400, "Name", "Class already exist with this name");
            await _unitOfWork.ClassRepository.Create(@class);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteClassAsync(int id)
        {
            var @class = await _unitOfWork.ClassRepository.GetEntity(c => c.Id == id);
            if (@class is null)
                throw new CustomException(404, "Id", "Class not found with this Id");
            await _unitOfWork.ClassRepository.Delete(@class);
            await _unitOfWork.CommitAsync();

        }

        public async Task<PagedResponse<ClassListItemDto>> GetAllClassesAsync(int pageNumber, int pageSize)
        {
            int totalClasses = await _unitOfWork.ClassRepository.CountAsync();
            var classes = await _unitOfWork.ClassRepository.GetAll(
                includes: query => query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                );
            return new PagedResponse<ClassListItemDto>
            {
                Data = _mapper.Map<List<ClassListItemDto>>(classes),
                TotalCount = totalClasses,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };
        }

        public async Task<ClassReturnDto> GetClassByIdAsync(int id)
        {
            var @class = await _unitOfWork.ClassRepository.GetEntity(c => c.Id == id);
            if (@class is null)
                throw new CustomException(404, "Id", "Class not found with this Id");
            return _mapper.Map<ClassReturnDto>(@class);
        }
    }
}
