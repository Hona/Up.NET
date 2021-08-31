using System.Collections.Generic;

namespace Up.NET.Models
{
    public class UpResponse<T> where T : class
    {
        public bool Success => Errors == null && Response != default(T);
        public List<ErrorResponse> Errors { get; set; }
        public T Response { get; set; }
    }

    public abstract class UpResponse
    {
        public static UpResponse<T> FromSuccess<T>(T response) where T : class
            => new UpResponse<T>
            {
                Response = response
            };

        public static UpResponse<T> FromFail<T>(List<ErrorResponse> errorResponse) where T : class
            => new UpResponse<T>
            {
                Errors = errorResponse
            };
    }
}