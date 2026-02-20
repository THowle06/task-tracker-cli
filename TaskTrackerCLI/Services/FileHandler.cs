using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using TaskTrackerCLI.Models;

public class FileHandler
{
    private const string FILE_NAME = "todos.json";

    public async Task SaveTasksAsync(List<Todo> todos)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(todos, options);
        await File.WriteAllTextAsync(FILE_NAME, json);
    }

    public async Task<List<Todo>> LoadTasksAsync()
    {
        if (!File.Exists(FILE_NAME))
            return new List<Todo>();

        var json = await File.ReadAllTextAsync(FILE_NAME);
        return JsonSerializer.Deserialize<List<Todo>>(json) ?? new List<Todo>();
    }

    public bool TodoFileExists() => File.Exists(FILE_NAME);

    public async Task InitializeFileAsync()
    {
        if (!TodoFileExists())
            await SaveTasksAsync(new List<Todo>());
    }
}