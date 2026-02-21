using System.Globalization;
using TaskTrackerCLI.Models;
using TaskTrackerCLI.Services;

namespace TaskTrackerCLI.Commands;

public class ListCommand
{
    private readonly FileHandler _fileHandler;

    public ListCommand(FileHandler fileHandler)
    {
        _fileHandler = fileHandler;
    }

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