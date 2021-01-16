using System;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {  
            string ret = null;
            switch (statusCode)
            {
                
                case 400: 
                    ret = "A bad request, you have made";
                break;

                case 401: 
                    ret = "Authorized, you are not";
                break;

                case 404: 
                    ret ="Resource found, it was not";
                break;

                case 500: 
                    ret ="Errors are the path to the dark side.";
                break;

                default: 
                    ret = null;
                break;
            };

            return ret;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}