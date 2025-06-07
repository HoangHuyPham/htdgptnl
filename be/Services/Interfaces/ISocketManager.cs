using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

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