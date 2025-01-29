using EmployeeManagment.DTOs;
using EmployeeManagment.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EmployeeManagment.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OpenApiController : ControllerBase
{
    private readonly IHubContext<OpenAIHub> hubContext;

    public OpenApiController(IHubContext<OpenAIHub> hubContext)
    {
        this.hubContext = hubContext;
    }

    [HttpPost("question")]
    public async Task<IActionResult> PostQuestion([FromBody] QuestionDto question)
    {
        // save to db.
        _ = ProcessQuestion(question);
        return Ok($"You asked: {question}");
    }

    [NonAction]
    public async Task ProcessQuestion(QuestionDto question)
    {
        await Task.Delay(5_000);
        var answer = new AnswerDto { Answer = "I am fine, thank you!" };

        await hubContext.Clients.Client(question.ConnectionId).SendAsync("ReceiveAnswer", answer);
    }
}
