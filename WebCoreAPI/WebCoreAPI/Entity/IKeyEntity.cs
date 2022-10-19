namespace WebCoreAPI.Entity
{
    public interface IKeyEntity<T> where T : struct
    {
        public T Id { get; set; }
    }
}
