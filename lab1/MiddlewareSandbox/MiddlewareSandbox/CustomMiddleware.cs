namespace MiddlewareSandbox
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Query.ContainsKey("custom"))
            {
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("You've hit a custom middleware!");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
