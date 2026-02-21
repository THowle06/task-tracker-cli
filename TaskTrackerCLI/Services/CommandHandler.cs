using System.Reflection;
using TaskTrackerCLI.Commands;

namespace TaskTrackerCLI.Services;

public enum Command
{
    Add,
    Update,
    Delete,
    List,
    MarkInProgress,
    MarkDone
}

public class CommandHandler
{
    private readonly FileHandler _fileHandler;

    private string[] args;

    public CommandHandler(string[] args)
    {
        this.args = args;
        _fileHandler = new FileHandler();
    }

    public async Task Execute()
    {
        if (args.Length == 0)
        {
            Console.WriteLine("No command provided.");
            return;
        }

        string commandStr = args[0];

        // Handle commands with hyphens
        if (commandStr.Contains('-'))
        {
            commandStr = commandStr.Replace("-", "");
        }

        if (Enum.TryParse<Command>(commandStr, true, out var command))
        {
            switch (command)
            {
                case Command.Add:
                    await HandleAdd(args.Skip(1).ToArray());
                    break;
                case Command.Update:
                    await HandleUpdate(args.Skip(1).ToArray());
                    break;
                case Command.Delete:
                    await HandleDelete(args.Skip(1).ToArray());
                    break;
                case Command.List:
                    await HandleList(args.Skip(1).ToArray());
                    break;
                case Command.MarkInProgress:
                    await HandleMarkInProgress(args.Skip(1).ToArray());
                    break;
                case Command.MarkDone:
                    await HandleMarkDone(args.Skip(1).ToArray());
                    break;
                default:
                    HandleUnknown(args[0]);
                    break;
            }
        }
        else
        {
            HandleUnknown(args[0]);
        }
    }

    private async Task HandleAdd(string[] parameters)
    {
        if (parameters.Length != 1)
        {
            Console.WriteLine("'add' command requires exactly 1 parameter.");
            return;
        }

        AddCommand addCommand = new AddCommand(_fileHandler);
        await addCommand.AddTodoAsync(parameters[0]);
    }

    private async Task HandleUpdate(string[] parameters)
    {
        if (parameters.Length != 2)
        {
            Console.WriteLine("'update' command requires exactly 2 parameters.");
            return;
        }

        Console.WriteLine("Handling update command with parameters:");
        foreach (var param in parameters)
        {
            Console.WriteLine($"\t{param}");
        }
    }

    private async Task HandleDelete(string[] parameters)
    {
        if (parameters.Length != 1)
        {
            Console.WriteLine("'delete' command requires exactly 1 parameter.");
            return;
        }

        Console.WriteLine("Handling delete command with parameters:");
        foreach (var param in parameters)
        {
            Console.WriteLine($"\t{param}");
        }
    }

    private async Task HandleList(string[] parameters)
    {
        if (parameters.Length != 1)
        {
            Console.WriteLine("'list' command requires exactly 1 parameter.");
            return;
        }

        Console.WriteLine("Handling list command with parameters:");
        foreach (var param in parameters)
        {
            Console.WriteLine($"\t{param}");
        }
    }

    private async Task HandleMarkInProgress(string[] parameters)
    {
        if (parameters.Length != 1)
        {
            Console.WriteLine("'mark-in-progress' command requires exactly 1 parameter.");
            return;
        }

        Console.WriteLine("Handling mark in progress command with parameters:");
        foreach (var param in parameters)
        {
            Console.WriteLine($"\t{param}");
        }
    }

    private async Task HandleMarkDone(string[] parameters)
    {
        if (parameters.Length != 1)
        {
            Console.WriteLine("'mark-done' command requires exactly 1 parameter.");
            return;
        }

        Console.WriteLine("Handling mark done command with parameters:");
        foreach (var param in parameters)
        {
            Console.WriteLine($"\t{param}");
        }
    }

    private void HandleUnknown(string command)
    {
        Console.WriteLine($"Unknown command has been parsed: {command}");
        return;
    }
}