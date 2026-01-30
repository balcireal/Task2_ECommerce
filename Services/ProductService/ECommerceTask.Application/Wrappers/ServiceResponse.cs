namespace ECommerceTask.Application.Wrappers
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public List<string>? Errors { get; set; }

        public ServiceResponse()
        {
        }

        public ServiceResponse(T data)
        {
            Success = true;
            Data = data;
            Errors = null;
        }

        public ServiceResponse(T data, string message)
        {
            Success = true;
            Message = message;
            Data = data;
            Errors = null;
        }

        public ServiceResponse(string message)
        {
            Success = false;
            Message = message;
            Data = default;
            Errors = null;
        }
    }
}