using System.Globalization;
using TaskTrackerCLI.Models;
using TaskTrackerCLI.Services;

namespace TaskTrackerCLI.Commands;

/// <summary>
/// Handles the listing and filtering of tasks from the task tracker.
/// </summary>
public class ListCommand
{
    private readonly FileHandler _fileHandler;

    /// <summary>
    /// Initialises a new instance of the <see cref="ListCommand"/> class.
    /// </summary>
    /// <param name="fileHandler">The file handler used to load tasks.</param>
    public ListCommand(FileHandler fileHandler)
    {
        _fileHandler = fileHandler;
    }

    /// <summary>
    /// Lists tasks, optionally filtered by status.
    /// </summary>
    /// <param name="filter">The status to filter by (e.g., "todo", "in-progress", "done"). If null or empty, all tasks are displayed.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// Displays all tasks if no filter is provodes, or only tasks matching the specified status.
    /// If no tasks are found matching the filter criteria, an informational message is displayed.
    /// </remarks>
    public async Task ListTodosAsync(string? filter)
    {
        var todos = await _fileHandler.LoadTasksAsync();

        var filteredTodos = string.IsNullOrWhiteSpace(filter)
            ? todos
            : todos.Where(t => t.Status == filter).ToList();

        if (filteredTodos.Count == 0)
        {
            Console.WriteLine($"No tasks found with status '{filter}'.");
            return;
        }

        foreach (var todo in filteredTodos)
        {
            Console.WriteLine($"[{todo.Id}] {todo.Description} - {todo.Status}");
        }
    }
}