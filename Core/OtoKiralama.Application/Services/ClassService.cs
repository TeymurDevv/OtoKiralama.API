using AutoMapper;
using OtoKiralama.Application.Dtos.Class;
using OtoKiralama.Application.Dtos.Fuel;
using OtoKiralama.Application.Dtos.Pagination;
using OtoKiralama.Application.Exceptions;
using OtoKiralama.Application.Interfaces;
using OtoKiralama.Domain.Entities;
using OtoKiralama.Domain.Repositories;

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
            var existClass = await _unitOfWork.ClassRepository.isExists(c => c.Name.ToLower() == classCreateDto.Name.ToLower());
            if (existClass)
                throw new CustomException(400, "Name", "Class already exist with this name");
            var @class = _mapper.Map<Class>(classCreateDto);

            await _unitOfWork.ClassRepository.Create(@class);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateClassAsync(int id, ClassUpdateDto classUpdateDto)
        {
            var @class = await _unitOfWork.ClassRepository.GetEntity(c => c.Id == id);
            if (@class is null)
                throw new CustomException(404, "Class", "Class not found with this Id");
            var existClass = await _unitOfWork.ClassRepository.isExists(c => c.Name.ToLower() == classUpdateDto.Name.ToLower() && c.Id != id);
            if (existClass)
                throw new CustomException(400, "Name", "Another class already exists with this name");

            _mapper.Map(classUpdateDto, @class);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteClassAsync(int id)
        {
            var @class = await _unitOfWork.ClassRepository.GetEntity(c => c.Id == id);
            if (@class is null)
                throw new CustomException(404, "Id", "Class not found with this Id");
            await _unitOfWork.ClassRepository.Delete(@class);
            await _unitOfWork.SaveChangesAsync();

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
        public  async Task UpdateAsync(int? id,ClassUpdateDto classUpdateDto)
        {
            if (id is null)
                throw new CustomException(400, "Id", "Id can not be left empty");
            var existedClass=await _unitOfWork.ClassRepository.GetEntity(s=>s.Id == id);
            if (existedClass is null)
                throw new CustomException(404, "Class", "Not found");
            if (!string.IsNullOrEmpty(classUpdateDto.Name) && !existedClass.Name.Equals(classUpdateDto.Name, StringComparison.OrdinalIgnoreCase))
            {
                if (await _unitOfWork.FuelRepository.isExists(s => s.Name.ToLower() == classUpdateDto.Name.ToLower()))
                {
                    throw new CustomException(400, "Name", "This Class name already exists");
                }
            }
            _mapper.Map(classUpdateDto,existedClass);
            await _unitOfWork.ClassRepository.Update(existedClass);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
