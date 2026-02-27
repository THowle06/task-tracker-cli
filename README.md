# Task Tracker CLI

A command-line interface for managing tasks and tracking their progress.

This project is from [roadmap.sh](https://roadmap.sh/projects/task-tracker).

## Features

- **Add** - Create new tasks
- **Update** - Modify task descriptions
- **Delete** - Remove tasks
- **List** - View all tasks or filter by status
- **Mark In-Progress** - Set a task status to in-progress
- **Mark Done** - Mark a task as completed

## Usage

```bash
# Adding a new task
./TaskTrackerCLI add "Buy groceries"

# Updating and deleting tasks
./TaskTrackerCLI update 1 "Buy groceries and cook dinner"
./TaskTrackerCLI delete 1

# Marking a task as in progress or done
./TaskTrackerCLI mark-in-progress 1
./TaskTrackerCLI mark-done 1

# Listing all tasks
./TaskTrackerCLI list

# Listing tasks by status
./TaskTrackerCLI list done
./TaskTrackerCLI list todo
./TaskTrackerCLI list in-progress
```

## Building

```bash
dotnet build -c Release
```

## Publishing

```bash
dotnet publish -c Release -o ./output
```

The executable will be available in the `./output` directory.
