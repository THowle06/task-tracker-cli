using TaskTrackerCLI.Models;
using TaskTrackerCLI.Services;

namespace TaskTrackerCLI.Commands;

/// <summary>
/// Handles updating the status of tasks in the task tracker.
/// </summary>
public class StatusCommand
{
    private readonly FileHandler _fileHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="StatusCommand"/> class.
    /// </summary>
    /// <param name="fileHandler">The file handler used to persist tasks.</param>
    public StatusCommand(FileHandler fileHandler)
    {
        _fileHandler = fileHandler;
    }

    /// <summary>
    /// Updates the status of a task with the specified ID.
    /// </summary>
    /// <param name="idStr">The string representation of the task ID to update.</param>
    /// <param name="status">The new status to assign to the task (e.g., "todo", "in-progress", "done").</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// If the ID is not a valid integer or no task with the given ID exists,
    /// an error message is displayed and no task is updated.
    /// The task's UpdatedAt timestamp is automatically set to the current date and time.
    /// </remarks>
    public async Task UpdateStatusAsync(string idStr, string status)
    {
        if (!int.TryParse(idStr, out int id))
        {
            Console.WriteLine($"Error: Expected an integer ID, got {idStr}.");
            return;
        }

        var todos = await _fileHandler.LoadTasksAsync();

        var todo = todos.FirstOrDefault(t => t.Id == id);
        if (todo == null)
        {
            Console.WriteLine($"Error: No task found with ID {id}.");
            return;
        }

        todo.Status = status;
        todo.UpdatedAt = DateTime.Now;

        await _fileHandler.SaveTasksAsync(todos);

        Console.WriteLine($"Task {id} marked as {status}");
    }
}