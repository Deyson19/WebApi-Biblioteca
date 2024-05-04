namespace WebApi_Helpers
{
    public class Respuesta<T>
    {
        public bool IsSuccess { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }
    }
}