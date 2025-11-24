using ConsoleApp.Enums;
namespace ConsoleApp.Models;

public class BMIResult
{
    public decimal BMI { get; set; }
    public BMICategory Category { get; set; }
    public string LevelIndicator { get; set; } = string.Empty;
    public string Recommendation { get; set; } = string.Empty;
}