using TaskTrackerCLI.Services;

namespace TaskTrackerCLI.Commands;

/// <summary>
/// Handles the deletion of tasks from the task tracker.
/// </summary>
public class DeleteCommand
{
    private readonly FileHandler _fileHandler;

    /// <summary>
    /// Initialises a new instance of the <see cref="DeleteCommand"/> class.
    /// </summary>
    /// <param name="fileHandler">The file handler used to persist tasks.</param>
    public DeleteCommand(FileHandler fileHandler)
    {
        _fileHandler = fileHandler;
    }

    /// <summary>
    /// Deletes a task with the specified ID from the task list.
    /// </summary>
    /// <param name="index">The string representation of the task ID to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// If the index is not a valid integer or no task with the given ID exists,
    /// an error message is displayed and no task is deleted.
    /// </remarks>
    public async Task DeleteTodoAsync(string index)
    {
        if (!int.TryParse(index, out int id))
        {
            Console.WriteLine($"Error: Expected an integer index, got {index}.");
            return;
        }

        var todos = await _fileHandler.LoadTasksAsync();

        var todo = todos.FirstOrDefault(t => t.Id == id);
        if (todo == null)
        {
            Console.WriteLine($"Error: No task found with ID {id}.");
            return;
        }

        if (!todos.Remove(todo))
        {
            Console.WriteLine($"Error: Could not remove task with ID {id}.");
            return;
        }

        await _fileHandler.SaveTasksAsync(todos);

        Console.WriteLine($"Task deleted successfully (ID: {id})");
    }
}