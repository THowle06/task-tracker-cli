using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using TaskTrackerCLI.Models;

namespace TaskTrackerCLI.Services;

/// <summary>
/// Handles file I/O operations for persisting and loading tasks from a JSON file.
/// </summary>
public class FileHandler
{
    private const string FILE_NAME = "todos.json";

    /// <summary>
    /// Saves a list of tasks to the JSON file.
    /// </summary>
    /// <param name="todos">The list of tasks to save.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// The tasks are serialized to JSON with indentation for readability.
    /// </remarks>
    public async Task SaveTasksAsync(List<Todo> todos)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(todos, options);
        await File.WriteAllTextAsync(FILE_NAME, json);
    }

    /// <summary>
    /// Loads the list of tasks from the JSON file.
    /// </summary>
    /// <returns>A task representing the asynchronous operation that returns the loaded tasks, or an empty list if the file doesn't exist or is empty.</returns>
    /// <remarks>
    /// Returns an empty list if the file does not exist or contains no data.
    /// </remarks>
    public async Task<List<Todo>> LoadTasksAsync()
    {
        if (!File.Exists(FILE_NAME))
            return new List<Todo>();

        var json = await File.ReadAllTextAsync(FILE_NAME);

        if (string.IsNullOrWhiteSpace(json))
            return new List<Todo>();

        return JsonSerializer.Deserialize<List<Todo>>(json) ?? new List<Todo>();
    }

    /// <summary>
    /// Checks whether the tasks file exists.
    /// </summary>
    /// <returns>True if the file exists; otherwise, false.</returns>
    public bool TodoFileExists() => File.Exists(FILE_NAME);

    /// <summary>
    /// Initializes the tasks file if it doesn't already exist.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// Creates a new file with an empty task list if the file does not exist.
    /// </remarks>
    public async Task InitializeFileAsync()
    {
        if (!TodoFileExists())
            await SaveTasksAsync(new List<Todo>());
    }
}