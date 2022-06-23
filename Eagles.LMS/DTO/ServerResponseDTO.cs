using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eagles.LMS.DTO
{
    public class ServerResponseDTO
    {
        public ServerResponseState ServerResponseState { get; set; }
        public string Message { get; set; }
    }
    public enum ServerResponseState
    {
        Error = 500, Success = 200
    }
}