using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using ConsoleApp.Enums;

namespace ConsoleApp.Helpers;

public static class InputHelpers
{
    public static MenuChoice GetMenuChoice()
    {
        while (true)
        {
            Console.WriteLine("Please enter your selection:");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;

            if (input == string.Empty)
            {
                Console.WriteLine("Input cannot be empty. Please try again");
                continue;
            }

            if (!int.TryParse(input, out int val))
            {
                Console.WriteLine("Invalid input. Please try again");
                continue;
            }

            if (Enum.IsDefined(typeof(MenuChoice), val)) return (MenuChoice)val;

            Console.WriteLine("Invalid input. Please try again)");
        }
    }

    public static Gender GetGender()
    {
        while (true)
        {
            Console.Write("Enter your gender. 0 for Female, 1 for Male : ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;

            if (input == string.Empty)
            {
                Console.WriteLine("Input cannot be empty. Please try again (0 / 1)");
                continue;
            }

            if (!int.TryParse(input, out int val))
            {
                Console.WriteLine("Invalid Input. Please try again.");
                continue;
            }

            if (Enum.IsDefined(typeof(Gender), val)) return (Gender)val;

            Console.WriteLine("Invalid Input. Please try again.");
        }
    }

    public static int GetAge()
    {
        while (true)
        {
            Console.Write("Enter your age: ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;
            if (input == string.Empty)
            {
                Console.WriteLine("Input cannot be empty. Please try again");
                continue;
            }

            if (!int.TryParse(input, out int val))
            {
                Console.WriteLine("Invalid input. Please try again");
                continue;
            }

            if (val <1 || val >=120)
            {
                Console.WriteLine("Age must be between 1 and 120. Please try again");
                continue;
            }
            return val;
        }
    }

    public static decimal GetHeight()
    {
        while (true)
        {
            Console.WriteLine("Enter your height");
            Console.Write("Feet: ");
            string ftInput = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Inches: ");
            string inInput = Console.ReadLine()?.Trim() ?? string.Empty;

            // Check if inputs are empty
            if (ftInput == string.Empty || inInput == string.Empty)
            {
                Console.WriteLine("Input cannot be empty. Please try again.");
                continue;
            }

            // Try to parse feet
            if (!int.TryParse(ftInput, out int feet))
            {
                Console.WriteLine("Invalid input for feet. Please enter a valid number.");
                continue;
            }

 

            // Try to parse inches
            if (!int.TryParse(inInput, out int inches))
            {
                Console.WriteLine("Invalid input for inches. Please enter a valid number.");
                continue;
            }

            // Validate ranges (reasonable height)
            if (feet < 0 || feet > 10)
            {
                Console.WriteLine("Feet must be between 0 and 10. Please try again.");
                continue;
            }

            if (inches < 0 || inches >= 12)
            {
                Console.WriteLine("Inches must be between 0 and 11. Please try again.");
                continue;
            }

            // Convert feet and inches to total inches
            // Formula: total inches = (feet × 12) + inches
            decimal totalInches = (feet * 12) + inches;

            // Validate total height is reasonable (e.g., between 24 and 96 inches)
            if (totalInches < 24 || totalInches > 96)
            {
                Console.WriteLine("Height seems unrealistic. Please check your input and try again.");
                continue;
            }

            return totalInches;
        }
    }

    public static decimal GetWeight()
    {
        while (true)
        {
            Console.Write("Enter weight in lbs: ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;

            if (input == string.Empty)
            {
                Console.WriteLine("Input cannot be empty. Please try again");
                continue;
            }

            if (!decimal.TryParse(input, out decimal val))
            {
                Console.WriteLine("Invalid Input. Please try again");
                continue;
            }

            if (val <= 1 || val >= 500)
            {
                Console.WriteLine("Weight should be in range 1lbs - 500lbs. Please try again");
                continue;
            }
            return val;
        }
    }

    public static ActivityLevel GetActivityLevel()
    {
        while (true)
        {
            Console.WriteLine("Please enter your level of activity (0-Sedentary, 1-Lightly Active, 2-Moderate Active, 3-Very Active, 4-Extra Active)");
            string? input = Console.ReadLine()?.Trim() ?? string.Empty;

            if (input == string.Empty)
            {
                Console.WriteLine("Input cannot be empty. Please try again.");
                continue;
            }

            if (!int.TryParse(input, out int val))
            {
                Console.WriteLine("Invalid input. Please try again");
                continue;
            }

            if (Enum.IsDefined(typeof(ActivityLevel), val)) return (ActivityLevel)val;
        }
    }

    public static int GetWeeks()
    {
        while (true)
        {
            Console.Write("In how many weeks do you want to reach this goal? ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;
            if (input == string.Empty)
            {
                Console.WriteLine("Input cannot be empty. Please try again");
                continue;
            }

            if (!int.TryParse(input, out int val))
            {
                Console.WriteLine("Invalid input. Please try again");
                continue;
            }

            if (val < 1 || val >= 120)
            {
                Console.WriteLine("Month must be between 1 and 120. Please try again");
                continue;
            }
            return val;
        }
    }
    
    public static WeightSuggestion GetGoalType(WeightSuggestion weightSuggestion)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("What is your weight goal?");
            
            // Show auto-suggestion
            string suggestionText = weightSuggestion switch
            {
                WeightSuggestion.MaintainWeight => "Maintain your body weight",
                WeightSuggestion.IncreaseWeight => "Increase your body weight",
                WeightSuggestion.DecreaseWeight => "Decrease your body weight",
                _ => ""
            };
            Console.WriteLine($"💡 Suggested: {suggestionText} (based on your BMI)");
            Console.WriteLine();
            
            Console.WriteLine("0 - Maintain your body weight");
            Console.WriteLine("1 - Increase your body weight");
            Console.WriteLine("2 - Decrease your body weight");
            Console.Write("Your choice (0-2): ");
            string? input = Console.ReadLine()?.Trim() ?? string.Empty;

            if (input == string.Empty)
            {
                Console.WriteLine("Input cannot be empty. Please try again");
                continue;
            }

            if (!int.TryParse(input, out int val))
            {
                Console.WriteLine("Invalid input. Please try again.");
                continue;
            }

            if (Enum.IsDefined(typeof(WeightSuggestion), val))
            {
                WeightSuggestion userChoice = (WeightSuggestion)val;
                
                // Warn if user's choice differs from suggestion
                if (userChoice != weightSuggestion)
                {
                    Console.WriteLine();
                    Console.WriteLine("⚠️  Warning: Your choice differs from the BMI-based suggestion.");
                    Console.WriteLine("   Please consult with a healthcare professional if you're unsure.");
                    Console.WriteLine("   Press any key to continue with your choice...");
                    Console.ReadKey();
                }
                
                return userChoice;
            }
            
            Console.WriteLine("Invalid input. Please try again.");
        }
    }
}

