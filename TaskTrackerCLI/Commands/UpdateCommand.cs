using TaskTrackerCLI.Models;
using TaskTrackerCLI.Services;

namespace TaskTrackerCLI.Commands;

/// <summary>
/// Handles the updating of task descriptions in the task tracker.
/// </summary>
public class UpdateCommand
{
    private readonly FileHandler _fileHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCommand"/> class.
    /// </summary>
    /// <param name="fileHandler">The file handler used to persist tasks.</param>
    public UpdateCommand(FileHandler fileHandler)
    {
        _fileHandler = fileHandler;
    }

    /// <summary>
    /// Updates the description of a task with the specified ID.
    /// </summary>
    /// <param name="parameters">An array containing the task ID (as a string) at index 0 and the new description at index 1.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// If the ID is not a valid integer, the description is empty or whitespace, or no task with the given ID exists,
    /// an error message is displayed and no task is updated.
    /// The task's UpdatedAt timestamp is automatically set to the current date and time.
    /// </remarks>
    public async Task UpdateTodoAsync(string[] parameters)
    {
        if (!int.TryParse(parameters[0], out int id))
        {
            Console.WriteLine($"Error: Expected an integer index, got {parameters[0]}.");
            return;
        }
        else if (string.IsNullOrWhiteSpace(parameters[1]))
        {
            Console.WriteLine("Error: Task description cannot be empty.");
            return;
        }

        var todos = await _fileHandler.LoadTasksAsync();

        var todo = todos.FirstOrDefault(t => t.Id == id);
        if (todo == null)
        {
            Console.WriteLine($"Error: No task found with ID {id}.");
            return;
        }

        todo.Description = parameters[1];
        todo.UpdatedAt = DateTime.Now;

        await _fileHandler.SaveTasksAsync(todos);

        Console.WriteLine($"Task updated successfully (ID: {id})");
    }
}