namespace EmployeeManagment.DTOs;

public class AnswerDto
{
    public required string Answer { get; set; }
}

public class QuestionDto
{
    public required string Question { get; set; }

    public required string ConnectionId { get; set; }
}

public record AnswerDto1(string Name);

public class AnswerDto2
{
    public string Name { get; init; }
}