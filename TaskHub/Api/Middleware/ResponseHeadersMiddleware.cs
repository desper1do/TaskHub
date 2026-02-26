using System.Diagnostics;

namespace Api.Middleware;

public class ResponseHeadersMiddleware
{
    private readonly RequestDelegate _next;
    private const string StudentName = "Danila Nasibulin"; 
    private const string StudentGroup = "RI-240943"; 

    public ResponseHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        // Добавляем заголовки ДО передачи управления
        context.Response.Headers.Append("X-Student-Name", StudentName);
        context.Response.Headers.Append("X-Student-Group", StudentGroup);

        // Перехватываем оригинальный Body stream
        var originalBodyStream = context.Response.Body;
        using (var memoryStream = new MemoryStream())
        {
            context.Response.Body = memoryStream;

            // Передаём управление следующему middleware
            await _next(context);

            stopwatch.Stop();

            // Добавляем заголовок с временем
            if (!context.Response.HasStarted)
            {
                context.Response.Headers.Append("X-Response-Time-Ms", stopwatch.ElapsedMilliseconds.ToString());
            }

            // Копируем ответ обратно в оригинальный stream
            await memoryStream.CopyToAsync(originalBodyStream);
        }
    }
}