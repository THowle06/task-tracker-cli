using TaskTrackerCLI.Services;

/// <summary>
/// Entry point for the Task Tracker CLI application.
/// </summary>
/// <remarks>
/// Initializes the file handler to ensure the tasks file exists,
/// then creates a command handler and executes the command specified in the command-line arguments.
/// </remarks> 

FileHandler fileHandler = new();
await fileHandler.InitializeFileAsync();

CommandHandler commandHandler = new(args);
await commandHandler.Execute();