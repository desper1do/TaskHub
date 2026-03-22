using System;

namespace Api.Controllers.Tasks.Requests;

public class CreateTaskRequest
{
    public Guid CreatedByUserId { get; set; }
    public string? Title { get; set; }
}