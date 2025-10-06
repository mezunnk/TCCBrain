namespace BrainFlow.Repository.Interfaces
{
    public interface IBaseREP<T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<bool> Delete(int id);
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
    }
}