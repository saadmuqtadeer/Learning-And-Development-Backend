using Carter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class NotificationModule : CarterModule
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationModule(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/notify", async (HttpContext context) =>
        {
            using var reader = new StreamReader(context.Request.Body);
            var message = await reader.ReadToEndAsync();

            if (string.IsNullOrEmpty(message))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid message");
                return;
            }

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "System", message);
            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsync("Notification sent.");
        });
    }

}
