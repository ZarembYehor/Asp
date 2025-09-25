using MiddlewareSandbox; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<LoggingMiddleware>();        
app.UseMiddleware<ApiKeyMiddleware>();         
app.UseMiddleware<CustomMiddleware>();    
app.UseMiddleware<RequestCounterMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
