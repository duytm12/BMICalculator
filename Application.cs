using OH = ConsoleApp.Helpers.OutPutHelpers;
using IH = ConsoleApp.Helpers.InputHelpers;
using ConsoleApp.Enums;
using ConsoleApp.Models;
using ConsoleApp.Services;
using System.Threading.Tasks.Dataflow;

namespace ConsoleApp;
public class Application
{
    private UserProfile userProfile = new();
    private BMIResult? bmiResult = null; // Nullable - null means BMI hasn't been calculated yet
    private TDEEResult tdeeResult = new();
    public void Run()
    {
        Console.WriteLine("\nWelcome to the BMI Calculator");
        while (true)
        {
            // 1. Print Start Menu
            OH.PrintStartMenu();

            MenuChoice choice = IH.GetMenuChoice();

            switch (choice)
            {
                case MenuChoice.Exit:
                    Console.WriteLine("Thank you for using the BMI Calculator app.");
                    return;
                case MenuChoice.ViewBMIChart:
                    OH.PrintBMIChart();
                    break;
                case MenuChoice.CalculateBMI:
                    HandleBMICalculation();
                    break;
                case MenuChoice.CalculateBMR:
                    HandleBMRCalculation();
                    break;
                case MenuChoice.CalculateTDEE:
                    HandleTDEECalculation();
                    break;
                case MenuChoice.SetGoal:
                    HandleSetGoal();
                    break;
                // case MenuChoice.RecommendActivity:
                //     HandleRecommendActivity();
                //     break;
                // case MenuChoice.RecommendFood:
                //     HandleRecommendFood();
                //     break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

    }

    private void EnsureHeight()
    {
        if (userProfile.Height == 0)
        {
            userProfile.Height = IH.GetHeight();
        }
        else
        {
            Console.WriteLine($"Using stored height: {userProfile.Height} inches");
        }
    }

    private void EnsureWeight()
    {
        if (userProfile.Weight == 0)
        {
            userProfile.Weight = IH.GetWeight();
        }
        else
        {
            Console.WriteLine($"Using stored weight: {userProfile.Weight} lbs");
        }
    }

    private void EnsureGender()
    {
        if (userProfile.Gender == null)
        {
            userProfile.Gender = IH.GetGender();
        }
        else
        {
            Console.WriteLine($"Using stored gender: {userProfile.Gender}");
        }
    }

    private void EnsureAge()
    {
        if (userProfile.Age == 0)
        {
            userProfile.Age = IH.GetAge();
        }
        else
        {
            Console.WriteLine($"Using stored age: {userProfile.Age}");
        }
    }

    private void EnsureActivityLevel()
    {
        OH.PrintActivityChart();
        if (userProfile.ActivityLevel == null)
        {
            userProfile.ActivityLevel = IH.GetActivityLevel();
        }
        else
        {
            Console.WriteLine($"Using stored activity level: {userProfile.ActivityLevel}");
        }
    }

    private void EnsureTDEE()
    {
        if (tdeeResult.TDEE == 0)
        {
            // Ensure all required data for TDEE calculation
            EnsureHeight();
            EnsureWeight();
            EnsureGender();
            EnsureAge();
            
            tdeeResult = BMIService.CalculateTDEE(userProfile);
        }
        else
        {
            Console.WriteLine($"Using stored TDEE: {tdeeResult.TDEE}");
        }
    }

    private void HandleBMICalculation()
    {
        Console.WriteLine("\nBMI - Body Mass Index is: ");
        Console.WriteLine("1.  A measure of body fat based on height and weight");
        Console.WriteLine("2.  It does not measure body composition\n");

        // Ensure required data is collected
        EnsureHeight();
        EnsureWeight();

        // Display what you're using
        Console.WriteLine($"\nUsing Height: {userProfile.Height} inches and Weight: {userProfile.Weight} lbs to calculate your BMI");

        // Calculate BMI
        bmiResult = BMIService.CalculateBMI(userProfile);

        // Display results
        OH.PrintBMIResult(bmiResult);
    }
    
    private void HandleBMRCalculation()
    {
        Console.WriteLine("\nBMR - Basal Metabolic Rate is: ");
        Console.WriteLine("1.  The number of calories your body burns at rest (doing nothing)");
        Console.WriteLine("2.  The minimum calories needed to stay alive\n");
        
        // Ensure required data is collected
        EnsureHeight();
        EnsureWeight();
        EnsureGender();
        EnsureAge();

        // Display what you're using
        Console.WriteLine($"\nUsing Height: {userProfile.Height} inches, Weight: {userProfile.Weight} lbs, Gender: {userProfile.Gender}, Age: {userProfile.Age} to calculate your BMR");
        
        // Calculate BMR
        BMRResult bmrResult = BMIService.CalculateBMR(userProfile);
        
        // Display results
        Console.WriteLine();
        OH.PrintBMRResult(bmrResult);
    }
    private void HandleTDEECalculation()
    {
        Console.WriteLine("\nTDEE - Total Daily Energy Expenditure: ");
        Console.WriteLine("1. The number of calories your body burns in a day. Including all activities and bodily functions such as breathing, moving, excercising, digesting food, and even fidgeting.");
        Console.WriteLine("2. A person with higher activity level needs more energy to stay active and functions well, and vice versa.");

        // Ensure required data is collected
        EnsureActivityLevel();
        EnsureHeight();
        EnsureWeight();
        EnsureGender();
        EnsureAge();

        // Display what you're using
        Console.WriteLine($"\nUsing Level of Activity: {userProfile.ActivityLevel}, Height: {userProfile.Height} inches, Weight: {userProfile.Weight} lbs, Gender: {userProfile.Gender}, Age: {userProfile.Age} to calculate your TDEE");

        // Calculate TDEE
        TDEEResult tdeeResult = BMIService.CalculateTDEE(userProfile);

        // Display results
        OH.PrintTDEEResult(tdeeResult);
    }

    private void HandleSetGoal()
    {
        // Step 1: Check if we have BMI (calculate if needed)
        if (bmiResult != null)
        {
            Console.WriteLine("\nUsing your calculated BMI to set goal.");
        }
        else
        {
            Console.WriteLine("\nWe need to calculate your BMI first");
            EnsureHeight();
            EnsureWeight();
            bmiResult = BMIService.CalculateBMI(userProfile);
            OH.PrintBMIResult(bmiResult);
        }
        // Step 2: Determine suggested goal from BMI category
        WeightSuggestion goalType = BMIService.SetWeightSuggestion(bmiResult);

        // Step 3: Get goal type (with auto-suggestion)
        goalType = IH.GetGoalType(goalType);
        if (goalType == WeightSuggestion.MaintainWeight)
        {
            OH.PrintMaintainWeightMessage();
            return;
        }

        // Steps 4-6: Loop until user is satisfied with their goal
        decimal goalWeight;
        int weeks;
        bool isSafe;
        int userChoice;

        do
        {
            // Step 4: Get goal weight
            Console.WriteLine("\nEnter your targeted weight you want to achieve: ");
            goalWeight = IH.GetWeight();

            // Step 5: Get timeline
            Console.WriteLine();
            weeks = IH.GetWeeks();

            // Validate the goal
            Console.WriteLine();
            isSafe = BMIService.ValidateGoal(userProfile.Weight, goalType, goalWeight, weeks);

            // Step 6: Display goal summary
            OH.PrintGoalSummary(userProfile, goalWeight, weeks, isSafe);

            // Ask if user wants to adjust their goal
            userChoice = IH.ReenterOrExit();

        } while (userChoice == 1);


        // Step 7: Calculate and recommend a plan to achieve goal
        // Calculate calorie requirements:
        //   - Calculate TDEE (need BMR + ActivityLevel - use existing CalculateTDEE if available)
        //   - Calculate daily calorie deficit/surplus needed based on weekly rate
        //     (1 lb = 3,500 calories, so weekly deficit/surplus = weeklyRate * 3500 / 7 days)
        //   - Calculate target daily calories: TDEE ± daily deficit/surplus
        EnsureActivityLevel();
        EnsureTDEE();
        Console.WriteLine("\nCalculating calories deficit and recommend a personalized goal plan .... ");
        Console.ReadKey();

        decimal targetDailyCalories = BMIService.CalculateCaloriesDeficit(tdeeResult.TDEE, goalType, goalWeight, userProfile.Weight, weeks);
        
        // Display the personalized goal plan
        OH.PrintGoalPlan(goalType, targetDailyCalories, tdeeResult.TDEE, weeks, goalWeight);

        // Step 8: Store goal (optional)
        // Create a Goal model to store:
        //   - GoalType (WeightSuggestion enum)
        //   - CurrentWeight (userProfile.Weight)
        //   - GoalWeight (goalWeight)
        //   - Timeline (weeks)
        //   - WeeklyRate (calculated)
        //   - TargetDailyCalories (calculated)
        //   - CreatedDate (DateTime)
        // Store in Application class as: private Goal? currentGoal = null;
        // This allows user to view/update goal later
    }
}


