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
        Console.WriteLine("⭐ What do you want to do today?");
        PrintLine('-', 80);
        Console.WriteLine("1 : 📊 View BMI Chart");
        Console.WriteLine("2 : 📏 Calculate BMI (Body Mass Index)");
        Console.WriteLine("3 : 🔥 Calculate BMR (Basel Metabolic Rate)");
        Console.WriteLine("4 : ⚡ Calculate TDEE (Total Daily Energy Expenditure)");
        Console.WriteLine("5 : 🎯 Set goals");
        Console.WriteLine("6 : 🏃 Recommend Personalized Activity Plan");
        Console.WriteLine("7 : 🍎 Recommend Food Based on Your Preferences");
        Console.WriteLine("0 : 🚪 Exit");
        PrintLine('*', 80);
    }

    public static void PrintBMIResult(BMIResult bmiResult)
    {
        Console.WriteLine();
        PrintLine('=', 80);
        Console.WriteLine("📊 BMI CALCULATION RESULTS");
        PrintLine('=', 80);
        Console.WriteLine($"📏 BMI: {bmiResult.BMI:F1}");
        Console.WriteLine($"📋 Category: {bmiResult.Category}");
        Console.WriteLine($"💡 Status: {bmiResult.LevelIndicator}");
        Console.WriteLine($"💬 Recommendation: {bmiResult.Recommendation}");
        PrintLine('=', 80);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public static void PrintBMRResult(BMRResult bmrResult)
    {
        Console.WriteLine();
        PrintLine('=', 80);
        Console.WriteLine("🔥 BMR CALCULATION RESULTS");
        PrintLine('=', 80);
        Console.WriteLine($"🔥 BMR (Basal Metabolic Rate): {bmrResult.BMR:F1} calories/day");
        Console.WriteLine();
        Console.WriteLine("💤 This is the number of calories your body burns at rest.");
        Console.WriteLine("⚡ This is the minimum calories needed to maintain basic bodily functions.");
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
        Console.WriteLine("🏃 ACTIVITY LEVEL REFERENCE CHART");
        PrintLine('=', 80);
        Console.WriteLine();
        Console.WriteLine("┌──────────────────────┬──────────────────────────────────────┬────────────┐");
        Console.WriteLine("│   Activity Level     │           Description                │ Multiplier │");
        Console.WriteLine("├──────────────────────┼──────────────────────────────────────┼────────────┤");
        Console.WriteLine("│  🛋️  Sedentary        │  Little to no exercise               │    1.2     │");
        Console.WriteLine("├──────────────────────┼──────────────────────────────────────┼────────────┤");
        Console.WriteLine("│  🚶 Lightly Active   │  Light Exercise 1-3 days/week        │   1.375    │");
        Console.WriteLine("├──────────────────────┼──────────────────────────────────────┼────────────┤");
        Console.WriteLine("│  🏃 Moderately Active│  Moderate Exercise 3-5 days/week     │    1.55    │");
        Console.WriteLine("├──────────────────────┼──────────────────────────────────────┼────────────┤");
        Console.WriteLine("│  💪 Very Active      │  Hard Exercise 6-7 days/week         │   1.725    │");
        Console.WriteLine("├──────────────────────┼──────────────────────────────────────┼────────────┤");
        Console.WriteLine("│  🔥 Extra Active     │  Very hard exercise + physical job   │    1.9     │");
        Console.WriteLine("└──────────────────────┴──────────────────────────────────────┴────────────┘");
        Console.WriteLine();
        Console.WriteLine("📝 Note: Multiplier is used to calculate TDEE = BMR × Activity Multiplier");
        Console.WriteLine();
        PrintLine('=', 80);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public static void PrintTDEEResult(TDEEResult tdeeResult)
    {
        Console.WriteLine();
        PrintLine('=', 80);
        Console.WriteLine("⚡ TDEE CALCULATION RESULTS");
        PrintLine('=', 80);
        Console.WriteLine($"🔥 BMR (Basal Metabolic Rate): {tdeeResult.BMR:F1} calories/day");
        Console.WriteLine("   💤 BMR = Calories burned at rest (doing nothing)");
        Console.WriteLine();
        Console.WriteLine($"🏃 Activity Calories: {tdeeResult.ActivityCalories:F1} calories/day");
        Console.WriteLine("   💪 Activity Calories = Calories burned from physical activities");
        Console.WriteLine();
        Console.WriteLine($"⚡ TDEE (Total Daily Energy Expenditure): {tdeeResult.TDEE:F1} calories/day");
        Console.WriteLine();
        Console.WriteLine($"✅ To maintain your current weight, you should consume approximately {tdeeResult.TDEE:F0} calories per day.");
        PrintLine('=', 80);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public static void PrintMaintainWeightMessage()
    {
        Console.WriteLine();
        PrintLine('=', 80);
        Console.WriteLine("✅ You've chosen to maintain your current weight.");
        Console.WriteLine("🎉 No goal setting needed. Keep up the good work!");
        Console.WriteLine("💪 Continue your healthy lifestyle!");
        PrintLine('=', 80);
        Console.WriteLine("Press any key to return to main menu...");
        Console.ReadKey();
        return;
    }

    public static void PrintGoalSummary(UserProfile userProfile, decimal goalWeight, int weeks, bool isSafe)
    {
        Console.WriteLine();
        PrintLine('=', 80);
        Console.WriteLine("🎯 GOAL SUMMARY:");
        Console.WriteLine($"Current weight: {userProfile.Weight} lbs.");
        Console.WriteLine($"Goal weight: {goalWeight} lbs.");
        decimal weightChange = Math.Abs(goalWeight - userProfile.Weight);
        if (userProfile.Weight < goalWeight)
        {
            Console.WriteLine($"📈 Weight to increase: {weightChange} lbs.");
        }
        else if (userProfile.Weight > goalWeight)
        {
            Console.WriteLine($"📉 Weight to decrease: {weightChange} lbs.");
        }
        else
        {
            Console.WriteLine("✅ You are already in good shape and healthy.");
            Console.WriteLine("🎉 Keep up the good work.");
        }
        Console.WriteLine($"Timeline to change your body weight: {weeks} weeks");
        Console.WriteLine($"Weekly weight change: {Math.Round(weightChange / weeks, 2)} lbs/week");
        string status = isSafe ? "✅ Safe and Healthy" : "⚠️ Unsafe - Consult Healthcare Provider";
        Console.WriteLine($"Validation Status: {status}");
        PrintLine('=', 80);
    }

    public static void PrintGoalPlan(WeightSuggestion goalType, decimal targetDailyCalories, decimal currentTDEE, int weeks, decimal goalWeight)
    {
        Console.WriteLine();
        PrintLine('=', 80);
        Console.WriteLine("🎯 YOUR PERSONALIZED GOAL PLAN");
        PrintLine('=', 80);
        
        decimal calorieAdjustment = Math.Abs(targetDailyCalories - currentTDEE);
        
        Console.WriteLine($"\n⚡ Your TDEE (maintenance): {currentTDEE:F0} calories/day");
        Console.WriteLine($"🎯 Target Daily Calories: {targetDailyCalories:F0} calories/day");
        
        if (goalType == WeightSuggestion.DecreaseWeight)
        {
            Console.WriteLine($"🔻 Daily Deficit: {calorieAdjustment:F0} calories");
            Console.WriteLine($"\n📉 WEIGHT LOSS PLAN");
            Console.WriteLine($"   Goal: Decrease to {goalWeight} lbs in {weeks} weeks");
            Console.WriteLine("\n💡 RECOMMENDATIONS:");
            Console.WriteLine("   ✅ Best Approach: Combine both strategies below");
            Console.WriteLine("      1. 🍎 Reduce Food Intake:");
            Console.WriteLine($"         - Eat approximately {targetDailyCalories:F0} calories per day");
            Console.WriteLine("         - 🥗 Focus on nutrient-dense, low-calorie foods");
            Console.WriteLine("         - 🚫 Avoid sugary drinks and processed foods");
            Console.WriteLine("      2. 🏃 Increase Physical Activity:");
            Console.WriteLine("         - 🚶 Walk more (aim for 10,000+ steps/day)");
            Console.WriteLine("         - 🏊 Add cardio exercises (running, cycling, swimming)");
            Console.WriteLine("         - 💪 Include strength training to maintain muscle");
            Console.WriteLine("         - ⛰️  Walk on incline surfaces for more calorie burn");
            Console.WriteLine("\n   ⚠️  Alternative (Less Recommended):");
            Console.WriteLine("      - Only reducing food intake may slow metabolism");
            Console.WriteLine("      - Exercise helps maintain muscle and overall health");
        }
        else if (goalType == WeightSuggestion.IncreaseWeight)
        {
            Console.WriteLine($"🔺 Daily Surplus: {calorieAdjustment:F0} calories");
            Console.WriteLine($"\n📈 WEIGHT GAIN PLAN");
            Console.WriteLine($"   Goal: Increase to {goalWeight} lbs in {weeks} weeks");
            Console.WriteLine("\n💡 RECOMMENDATIONS:");
            Console.WriteLine("   ✅ Best Approach: Increase food intake (Recommended)");
            Console.WriteLine("      1. 🍗 Eat More Healthy Foods:");
            Console.WriteLine($"         - Consume approximately {targetDailyCalories:F0} calories per day");
            Console.WriteLine("         - 🍗 Focus on protein-rich foods (chicken, fish, eggs, nuts)");
            Console.WriteLine("         - 🥑 Add healthy fats (avocado, olive oil, nuts)");
            Console.WriteLine("         - 🍚 Eat complex carbs (rice, oats, whole grains)");
            Console.WriteLine("         - 🍽️  Eat more frequently (5-6 meals per day)");
            Console.WriteLine("      2. 💪 Strength Training:");
            Console.WriteLine("         - 🏋️  Build muscle mass with weight training");
            Console.WriteLine("         - 🏆 Focus on compound exercises (squats, deadlifts, bench press)");
            Console.WriteLine("\n   ⚠️  Not Recommended:");
            Console.WriteLine("      - Reducing activity alone may decrease overall fitness");
            Console.WriteLine("      - Focus on eating more nutritious food instead");
        }
        
        PrintLine('=', 80);
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}

