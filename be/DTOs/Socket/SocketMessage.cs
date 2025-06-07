namespace be.DTOs.Socket
{
    public class SocketMessage
    {
        public string Type { get; set; } = "notify";
        public string Content { get; set; } = "";
    }
}