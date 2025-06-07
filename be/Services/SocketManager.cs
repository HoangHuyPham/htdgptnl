using be.Services.Interfaces;

namespace be.Services
{
    using System.Collections.Concurrent;
    using System.Net.WebSockets;

    public class SocketManager() : ISocketManager
    {
        private readonly ConcurrentDictionary<Guid, WebSocket> sockets = [];

        public Task<WebSocket> AddSocket(Guid id, WebSocket socket)
        {
            if (sockets.ContainsKey(id))
            {
                var existSocket = sockets.GetValueOrDefault(id);
                existSocket?.Abort();
                existSocket?.Dispose();
            }

            sockets[id] = socket;
            return Task.FromResult(socket);
        }

        public Task<bool> Delete(Guid id)
        {
            if (sockets.ContainsKey(id))
            {
                var existSocket = sockets.GetValueOrDefault(id);
                existSocket?.Abort();
                existSocket?.Dispose();
            }

            return Task.FromResult(sockets.Remove(id, out _));
        }

        public Task<List<WebSocket>> FindAll()
        {
            return Task.FromResult(sockets.Values.ToList());
        }

        public Task<WebSocket?> FindById(Guid id)
        {
            return Task.FromResult(sockets.GetValueOrDefault(id));
        }
    }
}