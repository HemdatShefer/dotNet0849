namespace DalApi
{
    /// <summary>
    /// Generic interface for CRUD operations on entities.
    /// T must be a value type, ensuring that ICrud operations are performed on entities with value-like behavior.
    /// </summary>
    /// <typeparam name="T">The type of entity this CRUD interface handles.</typeparam>
    public interface ICrud<T> where T : struct
    {
        public int Add(T item);
        public T GetById(int id);
        public void Update(T item);
        public void Delete(int id);
        public IEnumerable<T> GetAll();
    }
}
