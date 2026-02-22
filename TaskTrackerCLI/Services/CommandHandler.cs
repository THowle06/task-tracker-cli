using System.Reflection;
using TaskTrackerCLI.Commands;

namespace TaskTrackerCLI.Services;

/// <summary>
/// Defines the available commands for the task tracker CLI.
/// </summary>
public enum Command
{
    /// <summary>
    /// Adds a new task.
    /// </summary>
    Add,
    /// <summary>
    /// Updates an existing task's description.
    /// </summary>
    Update,
    /// <summary>
    /// Deletes a task.
    /// </summary>
    Delete,
    /// <summary>
    /// Lists tasks, optionally filtered by status.
    /// </summary>
    List,
    /// <summary>
    /// Marks a task as in-progress.
    /// </summary>
    MarkInProgress,
    /// <summary>
    /// Marks a task as done.
    /// </summary>
    MarkDone
}

/// <summary>
/// Handles parsing and execution of CLI commands for the task tracker.
/// </summary>
public class CommandHandler
{
    private readonly FileHandler _fileHandler;
    private string[] args;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandHandler"/> class.
    /// </summary>
    /// <param name="args">Command-line arguments to parse and execute.</param>
    public CommandHandler(string[] args)
    {
        this.args = args;
        _fileHandler = new FileHandler();
    }

    /// <summary>
    /// Executes the command specified in the command-line arguments.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// Parses the first argument as a command name and delegates to the appropriate handler.
    /// Commands with hyphens (e.g., "mark-in-progress") are normalized by removing hyphens before parsing.
    /// If no command is provided or the command is unknown, an error message is displayed.
    /// </remarks>
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

    /// <summary>
    /// Handles the 'add' command to create a new task.
    /// </summary>
    /// <param name="parameters">Command parameters.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
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

    /// <summary>
    /// Handles the 'update' command to modify a task's description.
    /// </summary>
    /// <param name="parameters">Command parameters.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task HandleUpdate(string[] parameters)
    {
        if (parameters.Length != 2)
        {
            Console.WriteLine("'update' command requires exactly 2 parameters.");
            return;
        }

        UpdateCommand updateCommand = new UpdateCommand(_fileHandler);
        await updateCommand.UpdateTodoAsync(parameters);
    }

    /// <summary>
    /// Handles the 'delete' command to remove a task.
    /// </summary>
    /// <param name="parameters">Command parameters.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task HandleDelete(string[] parameters)
    {
        if (parameters.Length != 1)
        {
            Console.WriteLine("'delete' command requires exactly 1 parameter.");
            return;
        }

        DeleteCommand deleteCommand = new DeleteCommand(_fileHandler);
        await deleteCommand.DeleteTodoAsync(parameters[0]);
    }

    /// <summary>
    /// Handles the 'list' command to display tasks.
    /// </summary>
    /// <param name="parameters">Command parameters.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task HandleList(string[] parameters)
    {
        string? filter = parameters.Length > 0 ? parameters[0] : null;

        ListCommand listCommand = new ListCommand(_fileHandler);
        await listCommand.ListTodosAsync(filter);
    }

    /// <summary>
    /// Handles the 'mark-in-progress' command to update a task's status,
    /// </summary>
    /// <param name="parameters">Command parameters.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task HandleMarkInProgress(string[] parameters)
    {
        if (parameters.Length != 1)
        {
            Console.WriteLine("'mark-in-progress' command requires exactly 1 parameter.");
            return;
        }

        StatusCommand statusCommand = new StatusCommand(_fileHandler);
        await statusCommand.UpdateStatusAsync(parameters[0], "in-progress");
    }

    /// <summary>
    /// Handles the 'mark-done' command to update a task's status.
    /// </summary>
    /// <param name="parameters">Command parameters.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task HandleMarkDone(string[] parameters)
    {
        if (parameters.Length != 1)
        {
            Console.WriteLine("'mark-done' command requires exactly 1 parameter.");
            return;
        }

        StatusCommand statusCommand = new StatusCommand(_fileHandler);
        await statusCommand.UpdateStatusAsync(parameters[0], "done");
    }

    /// <summary>
    /// Handles unknown or invalid commands.
    /// </summary>
    /// <param name="command">The unknown command string.</param>
    private void HandleUnknown(string command)
    {
        Console.WriteLine($"Unknown command has been parsed: {command}");
        return;
    }
}