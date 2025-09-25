namespace MiddlewareSandbox
{
    public class RequestCounterMiddleware
    {
        private readonly RequestDelegate _next;
        private static int _requestCount = 0;

        public RequestCounterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            int current = Interlocked.Increment(ref _requestCount);

            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync($"The amount of processed requests is {current}");
        }
    }
}
