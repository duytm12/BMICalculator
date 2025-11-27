using ConsoleApp.Enums;
using ConsoleApp.Models;

namespace ConsoleApp.Services;

public class BMIService
{
    public static BMIResult CalculateBMI(UserProfile userProfile)
    {
        decimal height = userProfile.Height;
        decimal weight = userProfile.Weight;
        decimal bmi = Math.Round(weight / (height * height) * 703, 1);

        BMICategory cat = bmi switch
        {
            < 18.5m => BMICategory.UnderWeight,
            < 25m => BMICategory.NormalWeight,
            < 30m => BMICategory.OverWeight,
            < 35m => BMICategory.ObesityClassI,
            < 40m => BMICategory.ObesityClassII,
            _ => BMICategory.ObesityClassIII
        };

        string levelIndicator = cat switch
        {
            BMICategory.UnderWeight => "Slightly Thin",
            BMICategory.NormalWeight => "Healthy",
            BMICategory.OverWeight => "Slightly Overweight",
            BMICategory.ObesityClassI => "Warning! Obesity Class I",
            BMICategory.ObesityClassII => "Cautious!! Obesity Class II",
            _ => "Serious!!! Obesity Class III"
        };


        string recommendation = cat switch
        {
            BMICategory.UnderWeight => "You need to increase your body weight.",
            BMICategory.NormalWeight => "Good job. You should maintain your body weight.",
            BMICategory.OverWeight => "You are slightly Overfit. That's alright.",
            BMICategory.ObesityClassI => "Warning! You have Obesity Class I. Start eating healthy or working out more.",
            BMICategory.ObesityClassII => "Cautious!! You have Obesity Class II. You have to take a look of your health seriously.",
            _ => "Serious!!! You have Obesity Class III. You need to see a Doctor ASAP to get the instructions."
        };

        BMIResult bmiResult = new()
        {
            BMI = bmi,
            Category = cat,
            LevelIndicator = levelIndicator,
            Recommendation = recommendation
        };
        return bmiResult;
    }

    public static BMRResult CalculateBMR(UserProfile userProfile)
    {
        // Get values from userProfile
        Gender? gender = userProfile.Gender;
        int age = userProfile.Age;
        decimal weight = userProfile.Weight;
        decimal height = userProfile.Height;

        // Calculate BMR using Mifflin-St Jeor Equation (Imperial version)
        // Men: BMR = 66 + (6.23 × weight in pounds) + (12.7 × height in inches) - (6.76 × age in years)
        // Women: BMR = 655 + (4.35 × weight in pounds) + (4.7 × height in inches) - (4.7 × age in years)
        decimal bmr = gender switch
        {
            Gender.Male => 66 + (6.23m * weight) + (12.7m * height) - (6.76m * age),
            Gender.Female => 655 + (4.35m * weight) + (4.7m * height) - (4.7m * age),
            _ => 0
        };

        // Round to 1 decimal place
        bmr = Math.Round(bmr, 1);

        BMRResult bmrResult = new()
        {
            BMR = bmr
        };
        return bmrResult;
    }

    public static TDEEResult CalculateTDEE(UserProfile userProfile)
    {
        BMRResult bmrResult = CalculateBMR(userProfile);
        decimal bmr = bmrResult.BMR;

        ActivityLevel activityLevel = userProfile.ActivityLevel ?? ActivityLevel.Sedentary; // Default to Sedentary if null

        decimal activityMultiplier = activityLevel switch
        {
            ActivityLevel.Sedentary => 1.2m,
            ActivityLevel.LightlyActive => 1.375m,
            ActivityLevel.ModerateActive => 1.55m,
            ActivityLevel.VeryActive => 1.725m,
            ActivityLevel.ExtraActive => 1.9m,
            _ => 1.2m  // Default to Sedentary if unknown
        };

        decimal tdee = bmr * activityMultiplier;

        decimal activityCalories = tdee - bmr;

        tdee = Math.Round(tdee, 1);
        activityCalories = Math.Round(activityCalories, 1);

        TDEEResult tdeeResult = new()
        {
            BMR = bmr,
            ActivityCalories = activityCalories,
            TDEE = tdee
        };
        return tdeeResult;
    }

    public static WeightSuggestion SetWeightSuggestion(BMIResult bmiResult)
    {
        WeightSuggestion weightSuggestion = bmiResult.Category switch
        {
            BMICategory.UnderWeight => WeightSuggestion.IncreaseWeight,
            BMICategory.NormalWeight => WeightSuggestion.MaintainWeight,
            _ => WeightSuggestion.DecreaseWeight
        };
        return weightSuggestion;
    }

    public static bool ValidateGoal(decimal bodyWeight, WeightSuggestion goalType, decimal goalWeight, int weeks)
    {
        // Step 1: Calculate weight change
        decimal weightChange = Math.Abs(goalWeight - bodyWeight);

        // Step 2: Calculate weekly rate
        decimal weeklyRate = weightChange / weeks;

        // Step 3: Validate based on goal type
        bool isSafe = goalType switch
        {
            WeightSuggestion.DecreaseWeight => weeklyRate <= 2.0m,    // Max 2 lbs/week for weight loss
            WeightSuggestion.IncreaseWeight => weeklyRate <= 2.0m,    // Max 2 lbs/week for weight gain
            _ => false
        };
        // Step 4: Show warnings if needed
        if (!isSafe)
        {
            Console.WriteLine("Your goal is unhealthy and very risky");
            Console.WriteLine("Please consult to a Healthcare provider");
        }

        if (isSafe)
        {
            Console.WriteLine("Your goal is healthy and reasonable.");
            Console.WriteLine("Now let's take a look of your Goal summary.");
        }

        // Step 5: Validate goal weight is reasonable
        if (goalType == WeightSuggestion.DecreaseWeight && goalWeight >= bodyWeight)
        {
            Console.WriteLine();
            Console.WriteLine($"[WARNING] For weight loss, your goal ({goalWeight} lbs) should be less than your current weight ({bodyWeight} lbs).");
        }
        else if (goalType == WeightSuggestion.IncreaseWeight && goalWeight <= bodyWeight)
        {
            Console.WriteLine();
            Console.WriteLine($"[WARNING] For weight gain, your goal ({goalWeight} lbs) should be greater than your current weight ({bodyWeight} lbs).");
        }
        return isSafe;
    }

    public static decimal CalculateCaloriesDeficit(decimal tdee, WeightSuggestion goalType, decimal goalWeight, decimal bodyWeight, int weeks)
    {
        decimal weightChange = Math.Abs(bodyWeight - goalWeight);
        decimal weeklyWeightChange = Math.Round(weightChange / weeks, 2);
        decimal dailyCaloriesAdjustment = weeklyWeightChange * 3500 / 7;

        decimal targetDailyCalories = goalType switch
        {
            WeightSuggestion.IncreaseWeight => tdee + dailyCaloriesAdjustment,
            WeightSuggestion.DecreaseWeight => tdee - dailyCaloriesAdjustment,
            _ => tdee
        };     
        return Math.Round(targetDailyCalories, 0);
    }
}
        