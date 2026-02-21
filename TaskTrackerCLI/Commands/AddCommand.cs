using TaskTrackerCLI.Services;
using TaskTrackerCLI.Models;

namespace TaskTrackerCLI.Commands;

public class AddCommand
{
    private readonly FileHandler _fileHandler;

    public AddCommand(FileHandler fileHandler)
    {
        _fileHandler = fileHandler;
    }

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