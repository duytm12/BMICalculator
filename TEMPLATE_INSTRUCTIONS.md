# Template Instructions

This is a reusable template for creating console applications. Follow these steps to customize it for your project:

## Step 1: Rename Project

1. Rename `ConsoleApp.csproj` to `[YourProjectName].csproj`
2. Rename `ConsoleApp.sln` to `[YourProjectName].sln` (if using solution file)
3. Update the solution file to reference the new project name

## Step 2: Update Namespaces

Replace all occurrences of `ConsoleApp` namespace with your project namespace:

- `Application.cs`
- `Program.cs`
- `Models/ExampleModel.cs`
- `Services/ExampleService.cs`
- `Helpers/InputHelpers.cs`
- `Helpers/OutPutHelpers.cs`

**Quick Find & Replace**: `namespace ConsoleApp` → `namespace [YourNamespace]`

## Step 3: Customize Application Logic

1. Open `Application.cs`
2. Replace the example code in `Run()` method with your application logic
3. Use the helper classes for input/output operations
4. Call your services for business logic

## Step 4: Create Your Models

1. Delete or replace `Models/ExampleModel.cs`
2. Create your own model classes
3. Models should only contain properties (no business logic)

## Step 5: Implement Your Services

1. Delete or replace `Services/ExampleService.cs`
2. Create your own service classes
3. Implement business logic in services
4. Services are called from `Application.cs`

## Step 6: Update README

1. Update `README.md` with your project-specific information
2. Remove or update this `TEMPLATE_INSTRUCTIONS.md` file

## Step 7: Clean Up

1. Delete `bin/` and `obj/` folders (they'll be regenerated)
2. Remove example files if not needed
3. Update `.gitignore` if needed

## Architecture Reminder

```
Program.cs (Entry Point)
    ↓
Application.cs (Orchestrator)
    ↓
    ├─→ InputHelpers (Get user input)
    ├─→ Models (Create data objects)
    ├─→ Services (Business logic)
    └─→ OutPutHelpers (Display results)
```

## Example Usage Flow

```csharp
// In Application.cs Run() method:
public void Run()
{
    OutPutHelpers.WriteHeader("My Application");
    
    // 1. Get input
    var name = InputHelpers.ReadLine("Enter name: ");
    var value = InputHelpers.ReadInt("Enter value: ");
    
    // 2. Create model
    var model = new MyModel { Name = name, Value = value };
    
    // 3. Call service
    var service = new MyService();
    var result = service.Process(model);
    
    // 4. Display result
    OutPutHelpers.WriteLine($"Result: {result}");
    
    OutPutHelpers.WaitForKeyPress();
}
```

Happy coding!
