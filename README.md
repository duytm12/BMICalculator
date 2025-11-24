# BMI Calculator Application

A comprehensive console-based BMI (Body Mass Index) calculator application built with C# 10.0 and .NET 10.0. This application helps users calculate their BMI, BMR (Basal Metabolic Rate), TDEE (Total Daily Energy Expenditure), and set personalized weight goals with validation and recommendations.

## Features

### Core Calculations
- **BMI Calculation**: Calculate Body Mass Index using imperial formula (weight in lbs, height in inches)
- **BMR Calculation**: Calculate Basal Metabolic Rate using Mifflin-St Jeor Equation (Imperial)
- **TDEE Calculation**: Calculate Total Daily Energy Expenditure based on BMR and activity level

### Goal Setting
- **Smart Goal Suggestions**: Auto-suggests weight goals based on BMI category
- **Goal Validation**: Validates goal weight and timeline for safety (max 2 lbs/week for loss/gain, 1 lb/week for maintenance)
- **Progressive Data Collection**: Stores user data (height, weight, age, gender, activity level) for reuse across calculations

### User Experience
- **Interactive Menu System**: Easy navigation with enum-based menu choices
- **Input Validation**: Comprehensive validation for all user inputs
- **Formatted Output**: Clean, formatted display of results with charts and recommendations
- **Data Persistence**: Stores user profile data throughout the session

## Project Structure

```
ConsoleApp/
├── Application.cs              # Main application orchestrator
├── Program.cs                  # Entry point
├── Models/                     # Data models
│   ├── UserProfile.cs         # User's basic information
│   ├── BMIResult.cs           # BMI calculation results
│   ├── BMRResult.cs           # BMR calculation results
│   └── TDEEResult.cs          # TDEE calculation results
├── Services/                   # Business logic layer
│   └── BMIService.cs          # BMI, BMR, TDEE calculations and goal validation
├── Helpers/                    # Utility classes
│   ├── InputHelpers.cs       # Input collection and validation
│   └── OutPutHelpers.cs      # Output formatting and display
├── Enums/                      # Type-safe enumerations
│   ├── MenuChoice.cs         # Main menu options
│   ├── Gender.cs             # User gender options
│   ├── BMICategory.cs        # BMI category classifications
│   ├── ActivityLevel.cs       # Physical activity levels
│   └── WeightSuggestion.cs    # Weight goal types
└── Requirements.md            # Detailed project requirements
```

## Architecture Pattern

This application follows a **top-down, layered architecture** with clear separation of concerns:

- **Program.cs**: Entry point - minimal code, instantiates and runs Application
- **Application.cs**: Orchestrator - coordinates application flow, manages user profile state, calls services
- **Services/**: Business logic - contains all calculation formulas and validation logic (unit testable)
- **Models/**: Data structures - simple DTOs with properties only, no business logic
- **Helpers/**: Utilities - reusable helper functions for I/O operations
- **Enums/**: Type-safe enumerations - prevents invalid values, improves code readability

### Key Design Patterns

1. **Progressive Data Collection**: User data is collected only when needed and stored in `UserProfile` for reuse
2. **Separation of Concerns**: UI (Application), Business Logic (Services), Data (Models), Utilities (Helpers)
3. **Type Safety**: Extensive use of enums for menu choices, categories, and options
4. **Nullable Types**: Used to distinguish "not set" from default enum values (e.g., `Gender?`, `ActivityLevel?`)

## Getting Started

### Prerequisites

- .NET 10.0 SDK or later
- Windows, macOS, or Linux

### Running the Application

```bash
# Navigate to project directory
cd ConsoleApp

# Run the application
dotnet run
```

### Building the Application

```bash
# Build the project
dotnet build

# Build in Release mode
dotnet build --configuration Release
```

## Usage Guide

### Main Menu Options

1. **View BMI Chart**: Display BMI reference chart with categories
2. **Calculate BMI**: Calculate and display BMI with category and recommendations
3. **Calculate BMR**: Calculate Basal Metabolic Rate (calories burned at rest)
4. **Calculate TDEE**: Calculate Total Daily Energy Expenditure (calories burned per day)
5. **Set Goal**: Set weight goals with validation and recommendations
6. **Exit**: Close the application

### Calculation Flow

#### BMI Calculation
1. Enter height (feet and inches) and weight (lbs)
2. BMI is calculated using: `BMI = (weight / (height²)) × 703`
3. Results include: BMI value, category, status indicator, and recommendations

#### BMR Calculation
1. Enter height, weight, gender, and age (if not already stored)
2. BMR is calculated using Mifflin-St Jeor Equation (Imperial)
3. Results show calories burned at rest per day

#### TDEE Calculation
1. Select activity level (1-5: Sedentary to Extra Active)
2. Enter height, weight, gender, and age (if not already stored)
3. TDEE = BMR × Activity Multiplier
4. Results show BMR, activity calories, and total TDEE

#### Goal Setting
1. BMI is calculated/retrieved
2. System suggests goal type based on BMI category
3. User selects goal type (Increase/Decrease/Maintain Weight)
4. User enters target weight and timeline (in weeks)
5. System validates goal for safety (weekly rate limits)
6. Goal summary and recommendations are displayed

## Key Features Explained

### Progressive Data Collection

The application stores user data in a `UserProfile` object throughout the session:
- **First calculation**: User enters all required data
- **Subsequent calculations**: System reuses stored data, only asks for missing information
- **User-friendly**: Displays "Using stored [data]" when reusing values

### Goal Validation

The system validates goals for safety:
- **Weight Loss**: Maximum 2 lbs per week
- **Weight Gain**: Maximum 2 lbs per week  
- **Weight Maintenance**: Maximum 1 lb per week fluctuation
- **Direction Check**: Validates goal weight direction matches goal type
- **Extreme Change Warning**: Warns if change exceeds 50% of current weight

### Formulas Used

#### BMI (Imperial)
```
BMI = (weight in lbs / (height in inches)²) × 703
```

#### BMR - Mifflin-St Jeor Equation (Imperial)
```
Men:   BMR = 66 + (6.23 × weight) + (12.7 × height) - (6.76 × age)
Women: BMR = 655 + (4.35 × weight) + (4.7 × height) - (4.7 × age)
```

#### TDEE
```
TDEE = BMR × Activity Multiplier

Activity Multipliers:
- Sedentary: 1.2
- Lightly Active: 1.375
- Moderately Active: 1.55
- Very Active: 1.725
- Extra Active: 1.9
```

## Code Examples

### Calculating BMI
```csharp
// In Application.cs
EnsureHeight();
EnsureWeight();
BMIResult result = BMIService.CalculateBMI(userProfile);
OH.PrintBMIResult(result);
```

### Progressive Data Collection
```csharp
// In Application.cs
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
```

### Goal Validation
```csharp
// In BMIService.cs
decimal weightChange = Math.Abs(goalWeight - bodyWeight);
decimal weeklyRate = weightChange / weeks;
bool isSafe = weeklyRate <= 2.0m; // Max 2 lbs/week
```

## Testing Strategy

- **Application.cs**: Integration tests (test the complete flow)
- **Services**: Unit tests (test calculation formulas and validation logic)
- **Helpers**: Unit tests (test input validation and output formatting)
- **Models**: Usually no tests needed (just data structures)

## Best Practices Implemented

✅ **Separation of Concerns**: Clear boundaries between UI, business logic, and data  
✅ **DRY Principle**: Reusable helper methods, no code duplication  
✅ **Type Safety**: Extensive use of enums instead of magic numbers/strings  
✅ **Progressive Enhancement**: Data collected only when needed  
✅ **User Experience**: Clear prompts, formatted output, helpful messages  
✅ **Validation**: Comprehensive input validation at multiple levels  
✅ **Maintainability**: Well-organized code structure, clear naming conventions  

## Future Enhancements

- [ ] Goal storage and retrieval
- [ ] Calorie tracking and food logging
- [ ] Activity recommendations based on goal
- [ ] Food recommendations based on goal
- [ ] Progress tracking over time
- [ ] Data persistence (save/load user profiles)

## Requirements

- .NET 10.0 SDK or later

## License

This project is for educational purposes.

## Contributing

This is a learning project. Feel free to use it as a reference for building similar applications.

---

**Note**: This application provides general health information and should not replace professional medical advice. Always consult with healthcare professionals for personalized health guidance.
