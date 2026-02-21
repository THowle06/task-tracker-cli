using TaskTrackerCLI.Models;
using TaskTrackerCLI.Services;

namespace TaskTrackerCLI.Commands;

public class StatusCommand
{
    private readonly FileHandler _fileHandler;

    public StatusCommand(FileHandler fileHandler)
    {
        _fileHandler = fileHandler;
    }

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