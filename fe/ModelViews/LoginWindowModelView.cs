using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using fe.Helpers;
using fe.Models;

namespace fe.ModelViews
{
    internal class LoginWindowModelView : AModelView
    {
        private LoginDTO loginDTO = new();
        private string message = string.Empty;
        private bool isError = false;
        private bool isLoading = false;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
        }
        public bool IsError
        {
            get => isError;
            set
            {
                isError = value;
                OnPropertyChanged();
            }
        }
        public string Message { 
            get => message;
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }
        public LoginDTO LoginDTO
        {
            get => loginDTO;
            set
            {
                loginDTO = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand LoginCmd => new((execute) =>
        {
            Login();
        }, (canExecute) => !string.IsNullOrEmpty(loginDTO.Username) && !string.IsNullOrEmpty(loginDTO.Password));


        private async void Login()
        {
            IsLoading = true;
            using HttpClient http = new();
            try
            {
                var body = new
                {
                    username = loginDTO.Username,
                    password = loginDTO.Password
                };

                var jsonParsed = JsonSerializer.Serialize(body);
                var jsonContent = new StringContent(jsonParsed, Encoding.UTF8, "application/json");
                var resp = await http.PostAsync("https://localhost:7061/api/Auth/login", jsonContent);
                var result = await resp.Content.ReadAsStringAsync();
           
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    Message = "Login success";
                    IsError = false;
                }
                else
                {
                    Message = "Login failed";
                    IsError = true;
                    return;
                }

                var deserialized = JsonSerializer.Deserialize<ApiResponse<string>>(result, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                Debug.WriteLine(deserialized?.Data);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsLoading = false;
            }

}
        
    }
}
