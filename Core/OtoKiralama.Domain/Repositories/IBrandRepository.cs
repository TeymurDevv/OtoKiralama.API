﻿using OtoKiralama.Domain.Entities;

namespace OtoKiralama.Domain.Repositories
{
    public interface IBrandRepository:IRepository<Brand>
    {
        Task<int> CountAsync();
    }
}
