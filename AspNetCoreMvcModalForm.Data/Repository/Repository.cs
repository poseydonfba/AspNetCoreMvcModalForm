using AspNetCoreMvcModalForm.Business.Intefaces;
using AspNetCoreMvcModalForm.Business.Models;
using AspNetCoreMvcModalForm.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AspNetCoreMvcModalForm.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new() //Entity, new()
    {
        protected readonly DataDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(DataDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public virtual async Task Remover(TEntity entity)
        {
            DbSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] include)
        {
            var query = DbSet.AsQueryable();

            foreach (var property in include) query = query.Include(property);

            return await query.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> BuscarAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> BuscarAsNoTracking(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] include)
        {
            var query = DbSet.AsQueryable();

            foreach (var property in include) query = query.Include(property);

            return await query.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> ObterPorId(Guid id, params Expression<Func<TEntity, object>>[] include)
        {
            var query = DbSet.AsQueryable();

            foreach (var property in include) query = query.Include(property);

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<TEntity> ObterPorIdAsNoTracking(Guid id)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<TEntity> ObterPorIdAsNoTracking(Guid id, params Expression<Func<TEntity, object>>[] include)
        {
            var query = DbSet.AsQueryable();

            foreach (var property in include) query = query.Include(property);

            return await query.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<List<TEntity>> ObterTodos(params Expression<Func<TEntity, object>>[] include)
        {
            var query = DbSet.AsQueryable();

            foreach (var property in include) query = query.Include(property);

            return await query.ToListAsync();
        }

        public virtual async Task<List<TEntity>> ObterTodosAsNoTracking()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<List<TEntity>> ObterTodosAsNoTracking(params Expression<Func<TEntity, object>>[] include)
        {
            var query = DbSet.AsQueryable();

            foreach (var property in include) query = query.Include(property);

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await Task.FromResult(0);
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
