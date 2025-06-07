using be.Helpers;
using be.Models;
using be.Repos.Interfaces;
using be.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Newtonsoft.Json;
using be.DTOs.Socket;

namespace be.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class ValidationTokenController(IConfiguration configuration, ISocketManager socketManager, IRepository<ValidationToken> repoValidationToken, IUserRepository repoUser) : ControllerBase
    {
        private readonly IConfiguration configuration = configuration;
        private readonly IRepository<ValidationToken> repoValidationToken = repoValidationToken;
        private readonly IUserRepository repoUser = repoUser;
        private readonly ISocketManager socketManager = socketManager;

        [HttpGet("ChangeEmail")]
        public async Task<IActionResult> ChangeEmail([FromQuery] string token)
        {
            if (token == null)
            {
                return Forbid();
            }
            var resultToken = await new JwtSecurityTokenHandler().ValidateTokenAsync(token, new()
            {
                ValidateIssuer = false,
                ValidateAudience = true,
                ValidAudiences = ["changeEmail"],
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWTSecret:Key").Value!))
            });

            if (!resultToken.IsValid)
            {
                return Forbid();
            }

            var newEmail = resultToken.Claims["newEmail"].ToString();
            var TokenId = resultToken.Claims["jti"].ToString();
            var UserId = resultToken.Claims[ClaimTypes.NameIdentifier].ToString();

            if (newEmail == null || TokenId == null || UserId == null)
            {
                return Ok(new ApiResponse<string>
                {
                    Message = "invalid claims",
                    Data = null
                });
            }

            var existToken = await repoValidationToken.FindById(Guid.Parse(TokenId));

            if (existToken != null)
            {
                return Ok(new ApiResponse<string>
                {
                    Message = "token was used",
                    Data = null
                });
            }

            await repoValidationToken.Create(new()
            {
                Id = Guid.Parse(TokenId)
            });

            var existUser = await repoUser.FindById(Guid.Parse(UserId));
            if (existUser == null)
            {
                return Ok(new ApiResponse<string>
                {
                    Message = "user not found",
                    Data = null
                });
            }

            existUser.Email = newEmail;
            await repoUser.Update(existUser);

            var socket = await socketManager.FindById(existUser.Id);
            if (socket != null)
                await socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new SocketMessage
                {
                    Content = newEmail,
                    Type = "changeEmail",
                }, new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                }))), System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);


            return NoContent();
        }
    }
}