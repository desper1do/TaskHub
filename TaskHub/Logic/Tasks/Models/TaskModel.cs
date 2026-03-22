using System;

namespace Logic.Tasks.Models;

public class TaskModel
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public Guid CreatedByUserId { get; set; }
    public DateTimeOffset CreatedUtc { get; set; }
}