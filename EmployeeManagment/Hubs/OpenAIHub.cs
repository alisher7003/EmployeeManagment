using Microsoft.AspNetCore.SignalR;

namespace EmployeeManagment.Hubs;

public class OpenAIHub : Hub
{
    public async Task SendQuestion(string question, string connectionId)
    {
        await Clients.Client(connectionId).SendAsync("ReceiveQuestion", question);
    }
}
