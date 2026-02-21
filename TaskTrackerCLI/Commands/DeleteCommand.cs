using System.IO.Compression;
using Microsoft.VisualBasic;
using TaskTrackerCLI.Models;
using TaskTrackerCLI.Services;

namespace TaskTrackerCLI.Commands;

public class DeleteCommand
{
    private readonly FileHandler _fileHandler;

    public DeleteCommand(FileHandler fileHandler)
    {
        _fileHandler = fileHandler;
    }

    public async Task DeleteTodoAsync(string index)
    {
        if (!int.TryParse(index, out int id))
        {
            Console.WriteLine($"Error: Expected an integer index, got {index}.");
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