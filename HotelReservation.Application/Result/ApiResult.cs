using System.Net;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace HotelReservation.Application.Result;

public class ApiResult<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public ErrorResult Error { get; set; }


    [JsonIgnore] public HttpStatusCode StatusCode { get; set; }

    public ApiResult(bool success, string message, T data)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public ApiResult(bool success, string message, T data, ErrorResult error)
    {
        Success = success;
        Message = message;
        Data = data;
        Error = error;
    }

    // Başarılı bir sonuç için fabrika metodu
    public static ApiResult<T> SuccessResult(T data, string message = "İşlem Başarılı",
        HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ApiResult<T>(true, message, data);
    }

    // Hatalı bir sonuç için fabrika metodu
    public static ApiResult<T> FailureResult(ErrorResult Error, string message = "İşlem Başarısız", HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    {
        return new ApiResult<T>(false, message, default, Error);
    }
    public static ApiResult<T> FailureResult(ErrorResult Error, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    {
        return new ApiResult<T>(false, "İşlem Başarısız", default, Error);
    }

    // Uyarı veya özel mesaj gerektiren durumlar için fabrika metodu
    public static ApiResult<T> WarningResult(string message, T data = default,
        HttpStatusCode statusCode = HttpStatusCode.Accepted)
    {
        return new ApiResult<T>(true, message, data);
    }
}