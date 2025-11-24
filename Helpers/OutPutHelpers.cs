using ConsoleApp.Enums;
using ConsoleApp.Models;

namespace ConsoleApp.Helpers;

public static class OutPutHelpers
{
    public static void PrintLine(char c, int i)
    {
        Console.WriteLine(new string(c, i));
    }

    public static void PrintStartMenu()
    {
        Console.WriteLine();
        PrintLine('*', 80);
        Console.WriteLine("What do you want to do today?");
        PrintLine('-', 80);
        Console.WriteLine("1 : View BMI Chart");
        Console.WriteLine("2 : Calculate BMI (Body Mass Index)");
        Console.WriteLine("3 : Calculate BMR (Basel Metabolic Rate)");
        Console.WriteLine("4 : Calculate TDEE (Total Daily Energy Expenditure)");
        Console.WriteLine("5 : Set goals");
        Console.WriteLine("6 : Recommend Activity");
        Console.WriteLine("7 : Recommend Food");
        Console.WriteLine("0 : Exit");
        PrintLine('*', 80);
    }

    public static void PrintBMIResult(BMIResult bmiResult)
    {
        Console.WriteLine();
        PrintLine('=', 80);
        Console.WriteLine("BMI CALCULATION RESULTS");
        PrintLine('=', 80);
        Console.WriteLine($"BMI: {bmiResult.BMI:F1}");
        Console.WriteLine($"Category: {bmiResult.Category}");
        Console.WriteLine($"Status: {bmiResult.LevelIndicator}");
        Console.WriteLine($"Recommendation: {bmiResult.Recommendation}");
        PrintLine('=', 80);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public static void PrintBMRResult(BMRResult bmrResult)
    {
        Console.WriteLine();
        PrintLine('=', 80);
        Console.WriteLine("BMR CALCULATION RESULTS");
        PrintLine('=', 80);
        Console.WriteLine($"BMR (Basal Metabolic Rate): {bmrResult.BMR:F1} calories/day");
        Console.WriteLine();
        Console.WriteLine("This is the number of calories your body burns at rest.");
        Console.WriteLine("This is the minimum calories needed to maintain basic bodily functions.");
        PrintLine('=', 80);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public static void PrintBMIChart()
    {
        Console.WriteLine();
        PrintLine('=', 80);
        Console.WriteLine("BMI REFERENCE CHART");
        PrintLine('=', 80);
        Console.WriteLine();
        Console.WriteLine("┌─────────────────┬──────────────────┬─────────────────────┐");
        Console.WriteLine("│   BMI Range     │     Category     │   Level Indicator   │");
        Console.WriteLine("├─────────────────┼──────────────────┼─────────────────────┤");
        Console.WriteLine("│  Below 18.5     │ Underweight      │  ⚠️  Below normal    │");
        Console.WriteLine("├─────────────────┼──────────────────┼─────────────────────┤");
        Console.WriteLine("│  18.5 - 24.9    │ Normal weight    │  ✅  Healthy        │");
        Console.WriteLine("├─────────────────┼──────────────────┼─────────────────────┤");
        Console.WriteLine("│  25.0 - 29.9    │ Overweight       │  ⚠️  Above normal    │");
        Console.WriteLine("├─────────────────┼──────────────────┼─────────────────────┤");
        Console.WriteLine("│  30.0 - 34.9    │ Obesity Class I  │  🔴  Moderate risk  │");
        Console.WriteLine("├─────────────────┼──────────────────┼─────────────────────┤");
        Console.WriteLine("│  35.0 - 39.9    │ Obesity Class II │  🔴  High risk      │");
        Console.WriteLine("├─────────────────┼──────────────────┼─────────────────────┤");
        Console.WriteLine("│  40.0 and above │ Obesity Class III│  🔴  Very high risk │");
        Console.WriteLine("└─────────────────┴──────────────────┴─────────────────────┘");
        Console.WriteLine();
        PrintLine('=', 80);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public static void PrintActivityChart()
    {
        Console.WriteLine();
        PrintLine('=', 80);
        Console.WriteLine("ACTIVITY LEVEL REFERENCE CHART");
        PrintLine('=', 80);
        Console.WriteLine();
        Console.WriteLine("┌──────────────────────┬──────────────────────────────────────┬────────────┐");
        Console.WriteLine("│   Activity Level     │           Description                │ Multiplier │");
        Console.WriteLine("├──────────────────────┼──────────────────────────────────────┼────────────┤");
        Console.WriteLine("│  Sedentary           │  Little to no exercise               │    1.2     │");
        Console.WriteLine("├──────────────────────┼──────────────────────────────────────┼────────────┤");
        Console.WriteLine("│  Lightly Active      │  Light Exercise 1-3 days/week        │   1.375    │");
        Console.WriteLine("├──────────────────────┼──────────────────────────────────────┼────────────┤");
        Console.WriteLine("│  Moderately Active   │  Moderate Exercise 3-5 days/week     │    1.55    │");
        Console.WriteLine("├──────────────────────┼──────────────────────────────────────┼────────────┤");
        Console.WriteLine("│  Very Active         │  Hard Exercise 6-7 days/week         │   1.725    │");
        Console.WriteLine("├──────────────────────┼──────────────────────────────────────┼────────────┤");
        Console.WriteLine("│  Extra Active        │  Very hard exercise + physical job   │    1.9     │");
        Console.WriteLine("└──────────────────────┴──────────────────────────────────────┴────────────┘");
        Console.WriteLine();
        Console.WriteLine("Note: Multiplier is used to calculate TDEE = BMR × Activity Multiplier");
        Console.WriteLine();
        PrintLine('=', 80);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public static void PrintTDEEResult(TDEEResult tdeeResult)
    {
        Console.WriteLine();
        PrintLine('=', 80);
        Console.WriteLine("TDEE CALCULATION RESULTS");
        PrintLine('=', 80);
        Console.WriteLine($"BMR (Basal Metabolic Rate): {tdeeResult.BMR:F1} calories/day");
        Console.WriteLine("BMR = Calories burned at rest (doing nothing)");
        Console.WriteLine();
        Console.WriteLine($"Activity Calories: {tdeeResult.ActivityCalories:F1} calories/day");
        Console.WriteLine("Activity Calories = Calories burned from physical activities");
        Console.WriteLine();
        Console.WriteLine($"TDEE (Total Daily Energy Expenditure): {tdeeResult.TDEE:F1} calories/day");
        Console.WriteLine();
        Console.WriteLine($"To maintain your current weight, you should consume approximately {tdeeResult.TDEE:F0} calories per day.");
        PrintLine('=', 80);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public static void PrintMaintainWeightMessage()
    {
        Console.WriteLine();
        PrintLine('=', 80);
        Console.WriteLine("You've chosen to maintain your current weight.");
        Console.WriteLine("No goal setting needed. Keep up the good work!");
        PrintLine('=', 80);
        Console.WriteLine("Press any key to return to main menu...");
        Console.ReadKey();
        return;
    }
}

