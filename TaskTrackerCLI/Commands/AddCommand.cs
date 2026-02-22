using TaskTrackerCLI.Services;
using TaskTrackerCLI.Models;

namespace TaskTrackerCLI.Commands;

/// <summary>
/// Handles the addition of new tasks to the task tracker.
/// </summary>
public class AddCommand
{
    private readonly FileHandler _fileHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddCommand"/> class. 
    /// </summary>
    /// <param name="fileHandler">The file handler used to persist tasks.</param>
    public AddCommand(FileHandler fileHandler)
    {
        _fileHandler = fileHandler;
    }

    /// <summary>
    /// Adds a new task with the specified description to the task list.
    /// </summary>
    /// <param name="description">The description of the task to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// The new task is assigned a unique ID, set to "todo" status, and timestamped with the current date and time.
    /// If the description is empty or whitespace, an error message is displayed and no task is added.
    /// </remarks>
    public async Task AddTodoAsync(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            Console.WriteLine("Error: Task description cannot be empty.");
            return;
        }

        var todos = await _fileHandler.LoadTasksAsync();

        var newTodo = new Todo
        {
            Id = todos.Count > 0 ? todos.Max(t => t.Id) + 1 : 1,
            Description = description,
            Status = "todo",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        todos.Add(newTodo);
        await _fileHandler.SaveTasksAsync(todos);

        Console.WriteLine($"Task added successfully (ID: {newTodo.Id})");
    }
}