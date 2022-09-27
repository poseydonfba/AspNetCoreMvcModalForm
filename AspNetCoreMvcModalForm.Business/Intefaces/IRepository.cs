using AspNetCoreMvcModalForm.Business.Models;
using System.Linq.Expressions;

namespace AspNetCoreMvcModalForm.Business.Intefaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Adicionar(TEntity entity);
        Task Atualizar(TEntity entity);
        Task Remover(Guid id);
        Task Remover(TEntity entity);
        Task<TEntity> ObterPorId(Guid id);
        Task<TEntity> ObterPorId(Guid id, params Expression<Func<TEntity, object>>[] include);
        Task<List<TEntity>> ObterTodos();
        Task<List<TEntity>> ObterTodos(params Expression<Func<TEntity, object>>[] include);
        Task<TEntity> ObterPorIdAsNoTracking(Guid id);
        Task<TEntity> ObterPorIdAsNoTracking(Guid id, params Expression<Func<TEntity, object>>[] include);
        Task<List<TEntity>> ObterTodosAsNoTracking();
        Task<List<TEntity>> ObterTodosAsNoTracking(params Expression<Func<TEntity, object>>[] include);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] include);
        Task<IEnumerable<TEntity>> BuscarAsNoTracking(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> BuscarAsNoTracking(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] include);
        Task<int> SaveChanges();
    }
}
