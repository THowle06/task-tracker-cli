namespace TaskTrackerCLI.Models;

/// <summary>
/// Represents a task in the task tracker.
/// </summary>
public class Todo
{
    /// <summary>
    /// Gets or sets the unique identifier for the task.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the description of the task.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the status of the task (e.g., "todo", "in-progress", "done").
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the task was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the task was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}