using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace be.DTOs.Socket
{
    public class SocketMessage
    {
        public string Type { get; set; } = "notify";
        public string Content { get; set; } = "";
    }
}