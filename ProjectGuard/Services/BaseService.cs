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
    public class BaseService : ApplicationService
    {
        protected IocManager _ioc;
        protected Dictionary<Type, IRepository> _repositories;

        public BaseService()
        {
            _ioc = IocManager.Instance;
            _repositories = new Dictionary<Type, IRepository>();
        }

        #region Repository

        protected IQueryable<TEntity> GetAllQuery<TEntity>()
            where TEntity : BaseEntity
        {
            return GetRepository<TEntity>().GetAll();
        }

        protected async Task<List<TEntity>> GetAllListAsync<TEntity>()
            where TEntity : BaseEntity
        {
            return await GetAllQuery<TEntity>().ToListAsync();
        }

        protected TEntity Get<TEntity>(int id)
            where TEntity : BaseEntity
        {
            return GetRepository<TEntity>().FirstOrDefault(id);
        }

        protected async Task<TEntity> GetAsync<TEntity>(int id)
            where TEntity : BaseEntity
        {
            return await GetRepository<TEntity>().FirstOrDefaultAsync(id);
        }

        protected TEntity Insert<TEntity>(TEntity entity)
            where TEntity : BaseEntity
        {
            entity.Id = 0;
            GetRepository<TEntity>().InsertAndGetId(entity); // InsertAndGetId will set id
            return entity;
        }

        protected async Task<TEntity> InsertAsync<TEntity>(TEntity entity)
            where TEntity : BaseEntity
        {
            entity.Id = 0;
            await GetRepository<TEntity>().InsertAndGetIdAsync(entity);
            return entity;
        }

        protected TEntity Update<TEntity>(int id, TEntity entity)
            where TEntity : BaseEntity
        {
            entity.Id = id;
            return GetRepository<TEntity>().Update(entity);
        }

        protected async Task<TEntity> UpdateAsync<TEntity>(int id, TEntity entity)
            where TEntity : BaseEntity
        {
            entity.Id = id;
            return await GetRepository<TEntity>().UpdateAsync(entity);
        }

        protected void Delete<TEntity>(int id)
            where TEntity : BaseEntity
        {
            GetRepository<TEntity>().Delete(id);
        }

        protected async Task DeleteAsync<TEntity>(int id)
            where TEntity : BaseEntity
        {
            await GetRepository<TEntity>().DeleteAsync(id);
        }

        // TODO: bulk :)
        protected async Task BulkInsertAsync<TEntity>(IEnumerable<TEntity> entities)
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

        protected async Task BulkDeleteAsync<TEntity>(IEnumerable<TEntity> entities)
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

        protected IRepository<TEntity> GetRepository<TEntity>()
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
