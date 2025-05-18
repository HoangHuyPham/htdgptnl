using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using fe.Helpers;
using fe.Models;

namespace fe.ModelViews
{
    internal class LoginWindowModelView : AModelView
    {
        public RelayCommand LoginCmd => new((execute) =>
        {
            Login();
        }, (canExecute) => true);

        private LoginDTO loginDTO = new();
        public LoginDTO LoginDTO
        {
            get { return loginDTO; }
            set
            {
                loginDTO = value;
                OnPropertyChanged();
            }
        }

        private async void Login()
        {
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

}
        
    }
}
