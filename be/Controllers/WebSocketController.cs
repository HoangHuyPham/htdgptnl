using be.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Claims;
using be.DTOs.Socket;

namespace be.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class WebSocketController(ISocketManager socketManager, IConfiguration configuration) : ControllerBase
    {
        private readonly ISocketManager socketManager = socketManager;
        private readonly IConfiguration configuration = configuration;
        [HttpGet("connect")]
        public async Task<IActionResult> Connect(CancellationToken cancellationToken)
        {
            if (!HttpContext.WebSockets.IsWebSocketRequest)
            {
                return BadRequest("invalid protocol");
            }

            var token = HttpContext.Request.Query["token"].ToString();
            if (token == null)
            {
                return BadRequest("Invalid token");
            }

            var resultToken = await new JwtSecurityTokenHandler().ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWTSecret:Key").Value!)),
                RoleClaimType = "roleId",
            });

            if (!resultToken.IsValid)
            {
                return new EmptyResult();
            }

            var buffer = new byte[1024 * 2];
            Guid userId;
            var parseResult = Guid.TryParse(resultToken.Claims[ClaimTypes.NameIdentifier].ToString(), out userId);

            if (!parseResult)
            {
                return new EmptyResult();
            }

            try
            {
                var connection = await HttpContext.WebSockets.AcceptWebSocketAsync();
                var socket = await socketManager.AddSocket(userId, connection);
                Console.WriteLine("Client {0} is connected", userId);
                await socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new SocketMessage
                {
                    Content = "hi client",
                    Type = "notify",
                }, new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                }))), WebSocketMessageType.Text, true, CancellationToken.None);

                while (socket.State == WebSocketState.Open)
                {

                    var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
                    if (result.MessageType == WebSocketMessageType.Close || cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Client {0} is disconnected", userId);
                        break;
                    }
                }
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine("Server shutting down");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                await socketManager.Delete(userId);
            }

            return new EmptyResult();
        }


    }
}