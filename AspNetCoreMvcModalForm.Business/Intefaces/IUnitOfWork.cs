namespace AspNetCoreMvcModalForm.Business.Intefaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Commit();
    }
}
