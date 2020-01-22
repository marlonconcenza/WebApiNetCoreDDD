namespace WebAPI.Domain.Entities
{
    public class Response
    {
        public bool success { get; set; }
        public object data { get; set; }
        public object error { get; set; }
    }
}