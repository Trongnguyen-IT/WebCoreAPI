namespace WebCoreAPI.Data
{
    public interface IKeyEntity<T> where T : struct
    {
        public T Id { get; set; }
    }
}
