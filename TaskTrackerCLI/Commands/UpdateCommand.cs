using TaskTrackerCLI.Models;
using TaskTrackerCLI.Services;

namespace TaskTrackerCLI.Commands;

public class UpdateCommand
{
    private readonly FileHandler _fileHandler;

    public UpdateCommand(FileHandler fileHandler)
    {
        _fileHandler = fileHandler;
    }

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