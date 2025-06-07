using System.Net.WebSockets;

namespace be.Services.Interfaces
{
    public interface ISocketManager
    {
        Task<List<WebSocket>> FindAll();
        Task<WebSocket?> FindById(Guid id);
        Task<bool> Delete(Guid id);
        Task<WebSocket> AddSocket(Guid id, WebSocket socket);
    }
}