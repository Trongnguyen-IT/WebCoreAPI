namespace WebCoreAPI.Models
{
    public class ApiResponse<T>
    {
        public bool Status { get; set; }
        public T  Data { get; set; }
        public string Message { get; set; }
    }
}
