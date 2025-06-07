using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using be.Models;
using be.Repos.Interfaces;
using be.Services.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MimeKit;

namespace be.Services
{
    public class EmailService(IConfiguration configuration) : IEmailService
    {
        private readonly IConfiguration configuration = configuration;
        public async Task SendChangeEmail(User user, string address, string newEmail)
        {
            MimeMessage message = new()
            {
                Subject = "Verify Change Email",
            };
            message.From.Add(new MailboxAddress("HTDGPTNL", configuration.GetSection("SMTP:USERNAME").Value!));
            message.To.Add(new MailboxAddress(null, address));

            List<Claim> claims = [
                new Claim("sub", user.Id.ToString()),
                new Claim("aud", "changeEmail"),
                new Claim("newEmail", newEmail),
                new Claim("roleId", user.RoleId.ToString()!),
                new Claim("jti", Guid.NewGuid().ToString())
            ];

            var cred = new SigningCredentials(
                key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWTSecret:Key").Value!)),
                algorithm: SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: cred
            );

            string normalizeToken = new JwtSecurityTokenHandler().WriteToken(token);
            string validationLink = "http://localhost:5173/validation-token/" + normalizeToken;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = File.ReadAllText("./EmailTemplates/changeEmail.html")
                .Replace("{{userName}}", user.UserName)
                .Replace("{{newEmail}}", newEmail)
                .Replace("{{validationLink}}", validationLink)
            };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(configuration.GetSection("SMTP:HOST").Value, 587, false);
                await client.AuthenticateAsync(configuration.GetSection("SMTP:USERNAME").Value, configuration.GetSection("SMTP:PASSWORD").Value);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
            }

        }

        public Task SendResetPassword(User user, string subject, string address)
        {
            throw new NotImplementedException();
        }
    }
}