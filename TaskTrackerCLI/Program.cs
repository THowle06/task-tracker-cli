using TaskTrackerCLI.Services;

CommandHandler commandHandler = new CommandHandler(args);

await commandHandler.Execute();