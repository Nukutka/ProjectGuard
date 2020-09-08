using Abp.Application.Services;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using ProjectGuard.Ef.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectGuard.Services
{
    public class DataService : ApplicationService
    {
        protected IocManager _ioc;
        protected Dictionary<Type, IRepository> _repositories;

        public DataService()
        {
            _ioc = IocManager.Instance;
            _repositories = new Dictionary<Type, IRepository>();
        }

        #region Repository

        public IQueryable<TEntity> GetAllQuery<TEntity>()
            where TEntity : BaseEntity
        {
            return GetRepository<TEntity>().GetAll();
        }

        public async Task<List<TEntity>> GetAllListAsync<TEntity>()
            where TEntity : BaseEntity
        {
            return await GetAllQuery<TEntity>().ToListAsync();
        }

        public TEntity Get<TEntity>(int id)
            where TEntity : BaseEntity
        {
            return GetRepository<TEntity>().FirstOrDefault(id);
        }

        public async Task<TEntity> GetAsync<TEntity>(int id)
            where TEntity : BaseEntity
        {
            return await GetRepository<TEntity>().FirstOrDefaultAsync(id);
        }

        public TEntity Insert<TEntity>(TEntity entity)
            where TEntity : BaseEntity
        {
            entity.Id = 0;
            GetRepository<TEntity>().InsertAndGetId(entity); // InsertAndGetId will set id
            return entity;
        }

        public async Task<TEntity> InsertAsync<TEntity>(TEntity entity)
            where TEntity : BaseEntity
        {
            entity.Id = 0;
            await GetRepository<TEntity>().InsertAndGetIdAsync(entity);
            return entity;
        }

        public TEntity Update<TEntity>(int id, TEntity entity)
            where TEntity : BaseEntity
        {
            entity.Id = id;
            return GetRepository<TEntity>().Update(entity);
        }

        public async Task<TEntity> UpdateAsync<TEntity>(int id, TEntity entity)
            where TEntity : BaseEntity
        {
            entity.Id = id;
            return await GetRepository<TEntity>().UpdateAsync(entity);
        }

        public void Delete<TEntity>(int id)
            where TEntity : BaseEntity
        {
            GetRepository<TEntity>().Delete(id);
        }

        public async Task DeleteAsync<TEntity>(int id)
            where TEntity : BaseEntity
        {
            await GetRepository<TEntity>().DeleteAsync(id);
        }

        // TODO: bulk :)
        public async Task BulkInsertAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : BaseEntity
        {
            var repository = GetRepository<TEntity>();

            await Task.Run(() =>
            {
                foreach (var entity in entities)
                {
                    repository.InsertAndGetId(entity);
                }
            });
        }

        public async Task BulkDeleteAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : BaseEntity
        {
            var repository = GetRepository<TEntity>();

            await Task.Run(() =>
            {
                foreach (var entity in entities)
                {
                    repository.Delete(entity);
                }
            });
        }

        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : BaseEntity
        {
            var entityType = typeof(TEntity);

            // resolve repository
            if (!_repositories.TryGetValue(entityType, out var repository))
            {
                var repoType = typeof(IRepository<TEntity>);
                repository = (IRepository)_ioc.Resolve(repoType);
                _repositories.Add(entityType, repository);
            }

            return (IRepository<TEntity>)repository;
        }

        #endregion
    }
}
