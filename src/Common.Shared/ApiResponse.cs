﻿namespace Common.Shared
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public List<string> Errors { get; set; }

        public ApiResponse(bool success, string message, T? data, List<string>? errors = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors ?? [];
        }
    }

    public class ApiResponse : ApiResponse<object>
    {
        public ApiResponse(bool success, string message, object? data = null, List<string>? errors = null)
            : base(success, message, data ?? new object(), errors ?? []) { }
    }
}
