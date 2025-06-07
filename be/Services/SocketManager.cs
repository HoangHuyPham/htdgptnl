using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using be.Models;
using be.Repos;
using be.Repos.Interfaces;
using be.Services.Interfaces;

namespace be.Services
{
    using System.Collections.Concurrent;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net.WebSockets;
    using System.Security.Claims;
    using System.Text;
    using BCrypt.Net;
    using be.DTOs.Role;
    using be.DTOs.User;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.JsonWebTokens;
    using Microsoft.IdentityModel.Tokens;

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