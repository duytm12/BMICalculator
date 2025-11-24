# BMI Calculator - Requirements

## 1. User Input Collection

### 1.1 Basic Information
- **Gender**: Male/Female/Other (used for BMR calculation)
- **Age**: In years (used for BMR calculation)
- **Weight**: In pounds (lbs)
- **Height**: In feet and inches (convert to total inches for calculation)

### 1.2 BMI Calculation Formula
- **Imperial**: `BMI = (weight in lbs / (height in inches)²) × 703`
- **Metric**: `BMI = weight in kg / (height in meters)²`

---

## 2. BMI Ranges and Level Indicators

| BMI Range | Category | Level Indicator |
|-----------|----------|-----------------|
| **Below 18.5** | Underweight | ⚠️ Below normal |
| **18.5 - 24.9** | Normal weight | ✅ Healthy |
| **25.0 - 29.9** | Overweight | ⚠️ Above normal |
| **30.0 - 34.9** | Obesity Class I | 🔴 Moderate risk |
| **35.0 - 39.9** | Obesity Class II | 🔴 High risk |
| **40.0 and above** | Obesity Class III | 🔴 Very high risk |

### 2.1 Display BMI Results
- Show calculated BMI number (rounded to 1 decimal place)
- Display category and level indicator
- Show warning message with suggested action based on BMI:
  - **Underweight (BMI < 18.5)**: Suggest increasing weight
  - **Overweight/Obesity (BMI ≥ 25.0)**: Suggest decreasing weight
  - **Normal weight (18.5 - 24.9)**: Suggest maintaining weight

---

## 3. Goal Setting

### 3.1 User Goal Selection
Ask user what they want to achieve:
1. **Increase Weight** (recommended for underweight)
2. **Decrease Weight** (recommended for overweight/obese)
3. **Maintain Weight** (recommended for normal weight)

**Note**: Auto-suggest based on BMI, but allow user to override.

### 3.2 Goal Details
- **Goal Weight**: User input in pounds (lbs)
- **Timeline**: User input in weeks

### 3.3 Validation Rules
- Validate goal weight is realistic:
  - For weight loss: Maximum 2 lbs per week (safe rate)
  - For weight gain: Maximum 1-2 lbs per week (safe rate)
  - Show warning if timeline is too aggressive
- Validate goal weight is within reasonable range (not extreme)

---

## 4. Calorie Tracking

### 4.1 Calorie Input (Food Consumption)
Collect daily calorie intake:
- **Breakfast**: Food items and quantities (convert to calories)
- **Lunch**: Food items and quantities (convert to calories)
- **Dinner**: Food items and quantities (convert to calories)
- **Snacks** (optional): Food items and quantities (convert to calories)

**Note**: Requires food calorie database or lookup mechanism to convert food type + quantity to calories.

### 4.2 Calorie Output (Energy Expenditure)

#### 4.2.1 Basal Metabolic Rate (BMR)
Calculate BMR using **Mifflin-St Jeor Equation**:
- **Men**: `BMR = 10 × weight(kg) + 6.25 × height(cm) - 5 × age(years) + 5`
- **Women**: `BMR = 10 × weight(kg) + 6.25 × height(cm) - 5 × age(years) - 161`

#### 4.2.2 Activity Tracking
- **Steps per day**: Convert to calories burned (approximate: 0.04 calories per step)
- **Specific Activities**: 
  - Dog walk (duration in minutes)
  - Gym workout (duration in minutes, type of exercise)
  - Other activities with duration
- **Activity Calories**: Use MET (Metabolic Equivalent) values or lookup table

#### 4.2.3 Total Daily Energy Expenditure (TDEE)
`TDEE = BMR + Activity Calories`

### 4.3 Calorie Balance Calculation
- **Calorie Balance** = Calorie Input - TDEE
- **Calorie Deficit** (negative balance): When Input < TDEE (for weight loss)
- **Calorie Surplus** (positive balance): When Input > TDEE (for weight gain)
- **Calorie Balance** (zero): When Input = TDEE (for weight maintenance)

Calculate and display:
- Daily calorie balance/deficit/surplus
- Weekly calorie balance/deficit/surplus
- Monthly calorie balance/deficit/surplus

---

## 5. Weight Change Projections

### 5.1 Weight Change Formula
- **1 pound of body weight** ≈ **3,500 calories**
- **Weight Change (lbs)** = (Calorie Balance × Days) / 3,500
- **Time to Goal** = (Weight Difference × 3,500) / Daily Calorie Balance

### 5.2 Projections
For each scenario, calculate:
1. **Time to reach Normal BMI range (18.5 - 24.9)**:
   - Calculate target BMI (use midpoint 21.7 or user preference)
   - Calculate required weight change
   - Calculate time needed based on current calorie balance

2. **Future Projection**:
   - If user maintains current habits (same calorie balance):
   - Project weight and BMI after the same time period
   - Show potential outcomes

---

## 6. Healthy Plan Recommendations

### 6.1 For Underweight Users (Goal: Increase Weight - Calorie Surplus)

#### 6.1.1 Options to Create Calorie Surplus:
1. **Increase Food Intake** (Recommended)
   - Add more calories through healthy foods
   - Increase meal portions
   - Add healthy snacks

2. **Reduce Activity** (Not Recommended)
   - Less physical activity = lower TDEE
   - Warning: Not healthy for overall fitness

3. **Both Approaches** (Moderate)
   - Eat more food AND reduce activity slightly
   - Warning: Calculate time to reach normal BMI
   - Project future weight/BMI if habits remain the same

### 6.2 For Overweight/Obese Users (Goal: Decrease Weight - Calorie Deficit)

#### 6.2.1 Options to Create Calorie Deficit:
1. **Reduce Food Intake** (Partially Recommended)
   - Eat less calories
   - Reduce portion sizes
   - Choose lower-calorie foods

2. **Increase Activity** (Highly Recommended)
   - Walk more steps
   - Walk on incline surfaces
   - More gym time
   - Increase workout intensity/weight
   - More physical activities

3. **Both Approaches** (Best Recommended)
   - Eat less calories AND increase activity
   - Best for overall health
   - Calculate time to reach normal BMI
   - Project future weight/BMI if habits remain the same

### 6.3 For Normal Weight Users (Goal: Maintain Weight - Calorie Balance)

#### 6.3.1 Maintenance Plan
- **Message**: "You are in good shape. No need to change habits."
- Maintain current calorie balance (Input ≈ TDEE)
- Continue current eating and activity patterns

---

## 7. Technical Requirements

### 7.1 Data Sources Needed
- **Food Calorie Database**: Lookup table or API to convert food items + quantities to calories
- **Activity MET Values**: Database of calories burned per minute for various activities
- **Common Activities**: Predefined list with calorie burn rates

### 7.2 Calculations Summary
1. BMI = (weight / height²) × conversion factor
2. BMR = Mifflin-St Jeor formula (based on gender, age, weight, height)
3. TDEE = BMR + Activity Calories
4. Calorie Balance = Input - TDEE
5. Weight Change = (Calorie Balance × Days) / 3,500
6. Time to Goal = (Weight Difference × 3,500) / Daily Calorie Balance

### 7.3 Error Handling
- Validate all numeric inputs (positive numbers, reasonable ranges)
- Handle unrealistic goals (show warnings)
- Handle division by zero (when calorie balance is zero)
- Provide helpful error messages

### 7.4 Output Format
- Display all calculations clearly
- Show BMI with category and indicator
- Display calorie balance/deficit/surplus clearly
- Show projections in easy-to-understand format
- Include warnings and recommendations

---

## 8. Implementation Notes

### 8.1 User Flow
1. Collect basic info (Gender, Age, Weight, Height)
2. Calculate and display BMI with category
3. Show recommendation based on BMI
4. Ask user for goal (Increase/Decrease/Maintain)
5. Collect goal weight and timeline
6. Validate goal is realistic
7. Collect daily calorie input (meals)
8. Collect daily activity (steps, exercises)
9. Calculate BMR, TDEE, and calorie balance
10. Show current calorie balance/deficit/surplus
11. Provide recommendations based on goal
12. Calculate and display projections
13. Show healthy plan options

### 8.2 Data Storage (Optional)
- Consider saving user data for tracking over time
- Allow users to update progress
- Track BMI changes over time
