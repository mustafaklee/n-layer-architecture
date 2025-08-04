using System.Net;
using System.Text.Json.Serialization;

namespace App.Services
{
    //geriye data dönmeli.
    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public List<string>? ErrorMessage { get; set; }
        [JsonIgnore]
        public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
        [JsonIgnore]
        public bool IsFail => !IsSuccess;
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        //static factory method
        public static ServiceResult<T> Success(T Data,HttpStatusCode status=HttpStatusCode.OK)
        {
            return new ServiceResult<T>()
            {
                Data = Data,
                StatusCode = status
            };
        }

        public static ServiceResult<T> Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>()
            {
                ErrorMessage = errorMessage,
                StatusCode =status
            };
        }

        public static ServiceResult<T> Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult<T>()
            {
                ErrorMessage = [errorMessage],
                StatusCode = status,
            };
        }


    }

    //geriye data dönmeden.
    public class ServiceResult
    {
        public List<string>? ErrorMessage { get; set; }

        [JsonIgnore]//json da bu dataları dönmeye gerek yok.Kendi iç yapımızda kullanırız.
        public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
        [JsonIgnore]
        public bool IsFail => !IsSuccess;
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        //static factory method
        public static ServiceResult Success(HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ServiceResult()
            {
                StatusCode = status
            };
        }

        public static ServiceResult Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult()
            {
                ErrorMessage = errorMessage,
                StatusCode = status
            };
        }

        public static ServiceResult Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ServiceResult()
            {
                ErrorMessage = [errorMessage],
                StatusCode = status,
            };
        }


    }

}
