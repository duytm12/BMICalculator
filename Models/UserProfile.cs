using ConsoleApp.Enums;

namespace ConsoleApp.Models;

/// <summary>
/// Example model class. Replace with your own models.
/// Models should contain only data properties, no business logic.
/// </summary>
public class UserProfile
{
    public Gender? Gender { get; set; }
    public int Age { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public ActivityLevel? ActivityLevel { get; set; } = null; // Nullable - null means not set yet 
}

