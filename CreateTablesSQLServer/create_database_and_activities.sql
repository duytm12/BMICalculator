-- Create Database for BMI and Activity Recommendation System

-- Drop database if it exists (use with caution!)
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'BMIActivityDB')
BEGIN
    ALTER DATABASE BMIActivityDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE BMIActivityDB;
END


CREATE DATABASE BMIActivityDB;


USE BMIActivityDB;


-- Verify we're in the correct database
SELECT DB_NAME() AS CurrentDatabase;


-- Drop table if it exists
IF OBJECT_ID('Activities', 'U') IS NOT NULL
    DROP TABLE Activities;


-- Create Activities Table - Optimized for 3 Plan Recommendations
CREATE TABLE Activities (
    activity_id INT IDENTITY(1,1) PRIMARY KEY,
    activity_name NVARCHAR(100) NOT NULL,
    activity_type NVARCHAR(50) NOT NULL,
    
    -- METs Value (PRIMARY for calculations - weight-independent)
    mets_value DECIMAL(4,2) NOT NULL,
    
    -- Intensity Classification (for filtering)
    intensity_level NVARCHAR(20) NOT NULL,
    
    -- MET Range Category (for easy filtering by plan)
    -- Low: < 5.0, Moderate: 5.0 - 7.9, High: >= 8.0
    mets_category AS (
        CASE 
            WHEN mets_value < 5.0 THEN 'Low'
            WHEN mets_value >= 5.0 AND mets_value < 8.0 THEN 'Moderate'
            WHEN mets_value >= 8.0 THEN 'High'
        END
    ),
    
    -- Typical Duration (reference only - can be adjusted in C#)
    duration_minutes INT NULL,
    
    -- Additional Info
    description NVARCHAR(500) NULL,
    difficulty_level INT NULL,
    is_indoor BIT NULL,
    
    -- Reference calories (for 70kg person - display only, NOT for calculations)
    calories_per_minute_reference DECIMAL(5,2) NULL,
    
    -- Timestamps
    created_at DATETIME2 DEFAULT GETDATE(),
    updated_at DATETIME2 DEFAULT GETDATE(),
    
    -- Constraints
    CONSTRAINT CK_IntensityLevel CHECK (intensity_level IN ('Low', 'Moderate', 'High', 'Very High')),
    CONSTRAINT CK_DifficultyLevel CHECK (difficulty_level IS NULL OR (difficulty_level BETWEEN 1 AND 5))
);


-- Verify table was created
IF OBJECT_ID('Activities', 'U') IS NOT NULL
    PRINT 'Activities table created successfully!';
ELSE
    PRINT 'ERROR: Activities table was NOT created!';


-- Insert Activity Data with Accurate METs and Reference Calories
-- Note: Use mets_value for all calculations in C# (weight-independent)
-- calories_per_minute_reference is for display/reference only (70kg person)
-- Formula: Calories per minute = (MET × Weight in kg × 3.5) / 200
-- METs values from Compendium of Physical Activities and Harvard Health Publishing
INSERT INTO Activities (activity_name, activity_type, mets_value, intensity_level, duration_minutes, description, difficulty_level, is_indoor, calories_per_minute_reference)
VALUES
-- HIGH MET Activities (>= 8.0) - For Plan 1 & Plan 3
('Running (6 mph)', 'Cardio', 9.8, 'High', 30, 'Moderate pace running', 3, 0, 12.0),
('Running (8 mph)', 'Cardio', 11.5, 'Very High', 30, 'Fast pace running', 4, 0, 14.1),
('Running (10 mph)', 'Cardio', 13.5, 'Very High', 30, 'Very fast running', 5, 0, 16.5),
('Swimming (freestyle, moderate)', 'Cardio', 8.3, 'High', 30, 'Freestyle swimming', 3, 1, 10.2),
('Swimming (backstroke)', 'Cardio', 8.0, 'High', 30, 'Backstroke swimming', 3, 1, 9.8),
('Jump Rope', 'Cardio', 12.3, 'High', 20, 'Jump rope exercise', 3, 1, 15.1),
('Rowing Machine (moderate)', 'Cardio', 8.5, 'High', 30, 'Indoor rowing', 3, 1, 10.4),
('Rowing Machine (vigorous)', 'Cardio', 10.0, 'High', 30, 'Vigorous indoor rowing', 4, 1, 12.3),
('Stair Climbing', 'Cardio', 8.0, 'High', 20, 'Climbing stairs', 3, 1, 9.8),
('Cycling (vigorous, 16-19 mph)', 'Cardio', 10.0, 'High', 45, 'High intensity cycling', 3, 0, 12.3),
('Aerobics (high impact)', 'Cardio', 8.3, 'High', 45, 'High impact aerobics', 3, 1, 10.2),
('Cross-Country Skiing', 'Cardio', 10.0, 'High', 60, 'Cross-country skiing', 4, 0, 12.3),
('Basketball (game)', 'Sports', 8.0, 'High', 60, 'Playing basketball', 4, 0, 9.8),
('Soccer', 'Sports', 7.8, 'High', 90, 'Playing soccer', 4, 0, 9.6),
('Tennis (singles)', 'Sports', 8.0, 'High', 60, 'Tennis singles match', 4, 0, 9.8),
('Squash', 'Sports', 10.0, 'High', 60, 'Playing squash', 4, 1, 12.3),
('Circuit Training', 'Strength', 7.0, 'High', 45, 'Circuit training workout', 3, 1, 8.6),

-- MODERATE MET Activities (5.0 - 7.9) - For Plan 2
('Walking (4.5 mph)', 'Cardio', 5.0, 'Moderate', 45, 'Power walking', 2, 0, 6.1),
('Cycling (moderate, 12-14 mph)', 'Cardio', 7.0, 'Moderate', 45, 'Moderate intensity cycling', 2, 0, 8.6),
('Swimming (breaststroke)', 'Cardio', 7.0, 'Moderate', 30, 'Breaststroke swimming', 2, 1, 8.6),
('Elliptical Trainer', 'Cardio', 7.5, 'Moderate', 30, 'Elliptical machine workout', 2, 1, 9.2),
('Dancing (vigorous)', 'Cardio', 7.0, 'High', 45, 'High intensity dancing', 3, 1, 8.6),
('Hiking', 'Cardio', 6.0, 'Moderate', 60, 'Hiking on trails', 3, 0, 7.4),
('Tennis (doubles)', 'Sports', 6.0, 'Moderate', 60, 'Tennis doubles match', 3, 0, 7.4),
('Weight Lifting (vigorous)', 'Strength', 6.0, 'High', 45, 'Intense weight training', 4, 1, 7.4),
('Shoveling Snow', 'Daily Activity', 6.0, 'Moderate', 30, 'Shoveling snow', 3, 0, 7.4),

-- LOW/MODERATE MET Activities (< 8.0) - For Plan 2
('Walking (3.5 mph)', 'Cardio', 3.8, 'Low', 45, 'Brisk walking', 1, 0, 4.7),
('Dancing (moderate)', 'Cardio', 5.0, 'Moderate', 45, 'Moderate intensity dancing', 2, 1, 6.1),
('Aerobics (low impact)', 'Cardio', 5.0, 'Moderate', 45, 'Low impact aerobics', 2, 1, 6.1),
('Basketball (shooting baskets)', 'Sports', 5.0, 'Moderate', 30, 'Shooting baskets', 2, 0, 6.1),
('Volleyball (competitive)', 'Sports', 5.0, 'Moderate', 60, 'Competitive volleyball', 4, 0, 6.1),
('Badminton', 'Sports', 5.0, 'Moderate', 60, 'Playing badminton', 3, 0, 6.1),
('Weight Lifting (moderate)', 'Strength', 4.5, 'Moderate', 45, 'Moderate weight training', 3, 1, 5.5),
('Bodyweight Exercises', 'Strength', 5.0, 'Moderate', 30, 'Push-ups, squats, planks', 2, 1, 6.1),
('Calisthenics', 'Strength', 5.0, 'Moderate', 30, 'Calisthenics exercises', 2, 1, 6.1),
('Yoga (Power)', 'Flexibility', 5.0, 'Moderate', 60, 'Power yoga practice', 4, 1, 6.1),
('Mowing Lawn (walking)', 'Daily Activity', 4.5, 'Moderate', 30, 'Mowing lawn with push mower', 2, 0, 5.5),
('Golf (walking)', 'Sports', 4.0, 'Low', 120, 'Golf while walking', 2, 0, 4.9),
('House Cleaning (vigorous)', 'Daily Activity', 4.0, 'Moderate', 60, 'Vigorous house cleaning', 2, 1, 4.9),
('Gardening', 'Daily Activity', 3.5, 'Moderate', 60, 'Gardening work', 2, 0, 4.3),
('Volleyball (recreational)', 'Sports', 3.5, 'Moderate', 60, 'Playing volleyball', 3, 0, 4.3),
('House Cleaning (moderate)', 'Daily Activity', 3.0, 'Moderate', 60, 'General house cleaning', 1, 1, 3.7),
('Raking Leaves', 'Daily Activity', 3.0, 'Moderate', 30, 'Raking leaves', 1, 0, 3.7),
('Yoga (Vinyasa)', 'Flexibility', 4.0, 'Moderate', 60, 'Flow yoga practice', 3, 1, 4.9),
('Pilates', 'Flexibility', 3.8, 'Moderate', 45, 'Pilates workout', 2, 1, 4.7),
('Yoga (Hatha)', 'Flexibility', 2.5, 'Low', 60, 'Gentle yoga practice', 1, 1, 3.1),
('Tai Chi', 'Flexibility', 2.5, 'Low', 60, 'Tai Chi practice', 1, 1, 3.1),
('Stretching', 'Flexibility', 1.5, 'Low', 20, 'General stretching', 1, 1, 1.8),
('Golf (cart)', 'Sports', 2.0, 'Low', 120, 'Golf using cart', 1, 0, 2.5);


-- Create Indexes for Efficient Filtering by Plan
CREATE INDEX IX_Activities_METS ON Activities(mets_value);
CREATE INDEX IX_Activities_Type ON Activities(activity_type);
CREATE INDEX IX_Activities_Intensity ON Activities(intensity_level);
CREATE INDEX IX_Activities_Type_METS ON Activities(activity_type, mets_value);  -- Composite for filtered queries
-- Note: mets_category is a computed column (non-persisted) so it cannot be indexed
-- Filter by mets_value ranges directly: WHERE mets_value >= 8.0 for High, etc.


-- IMPORTANT: Use mets_value for all calorie calculations in C#
-- Formula: Calories per minute = (MET × Weight in kg × 3.5) / 200
-- Example: For a 70kg person doing running (9.8 METs):
-- Calories per minute = (9.8 × 70 × 3.5) / 200 = 12.0 calories/minute
-- 
-- calories_per_minute_reference is for display/reference only (70kg person)
-- Always calculate calories dynamically using customer's actual weight in C#
--
-- MET Category Guide:
-- Low: < 5.0 (e.g., Walking, Yoga, Stretching)
-- Moderate: 5.0 - 7.9 (e.g., Cycling moderate, Dancing, Hiking)
-- High: >= 8.0 (e.g., Running, Jump Rope, Swimming, High-intensity sports)


PRINT 'Database and Activities table created successfully!';
PRINT 'Total activities inserted: ' + CAST(@@ROWCOUNT AS NVARCHAR(10));


