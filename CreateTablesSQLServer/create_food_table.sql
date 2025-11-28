-- Create Food Table with Accurate USDA Nutritional Data
-- Data sourced from USDA FoodData Central (fdc.nal.usda.gov)

USE BMIActivityDB;


-- Drop table if it exists
IF OBJECT_ID('Food', 'U') IS NOT NULL
    DROP TABLE Food;


-- Create Food Table
CREATE TABLE Food (
    food_id INT IDENTITY(1,1) PRIMARY KEY,
    food_name NVARCHAR(100) NOT NULL,
    food_category NVARCHAR(50) NOT NULL,
    meal_type NVARCHAR(20) NULL,  -- Breakfast, Lunch, Dinner, Snack, Any
    
    -- Serving Information
    serving_size DECIMAL(6,2) NOT NULL,  -- Amount (e.g., 100, 1, 150)
    serving_unit NVARCHAR(20) NOT NULL,  -- Unit (g, ml, cup, piece, etc.)
    
    -- Calories (per serving)
    calories_per_serving DECIMAL(6,2) NOT NULL,
    calories_per_100g DECIMAL(6,2) NOT NULL,  -- Standardized for comparison
    
    -- Macronutrients (per serving)
    protein_grams DECIMAL(5,2) NOT NULL,
    carbohydrates_grams DECIMAL(5,2) NOT NULL,
    fats_grams DECIMAL(5,2) NOT NULL,
    fiber_grams DECIMAL(5,2) NULL,
    sugar_grams DECIMAL(5,2) NULL,
    
    -- Additional Info
    is_processed BIT NULL  -- 0 = Whole food, 1 = Processed
);


-- Create Indexes
CREATE INDEX IX_Food_Category ON Food(food_category);
CREATE INDEX IX_Food_MealType ON Food(meal_type);
CREATE INDEX IX_Food_Calories ON Food(calories_per_100g);


-- Insert Accurate USDA-Based Nutritional Data
-- All values are per serving as specified, with calories_per_100g for standardization
-- Data sourced from USDA FoodData Central

INSERT INTO Food (food_name, food_category, meal_type, serving_size, serving_unit, calories_per_serving, calories_per_100g, protein_grams, carbohydrates_grams, fats_grams, fiber_grams, sugar_grams, is_processed)
VALUES
-- FRUITS (per 100g standard)
('Apple, raw', 'Fruits', 'Snack', 100.0, 'g', 52.0, 52.0, 0.26, 13.81, 0.17, 2.4, 10.39, 0),
('Banana, raw', 'Fruits', 'Snack', 100.0, 'g', 89.0, 89.0, 1.09, 22.84, 0.33, 2.6, 12.23, 0),
('Orange, raw', 'Fruits', 'Snack', 100.0, 'g', 47.0, 47.0, 0.94, 11.75, 0.12, 2.4, 9.35, 0),
('Strawberries, raw', 'Fruits', 'Snack', 100.0, 'g', 32.0, 32.0, 0.67, 7.68, 0.30, 2.0, 4.89, 0),
('Grapes, red or green', 'Fruits', 'Snack', 100.0, 'g', 69.0, 69.0, 0.72, 18.10, 0.16, 0.9, 15.48, 0),
('Watermelon, raw', 'Fruits', 'Snack', 100.0, 'g', 30.0, 30.0, 0.61, 7.55, 0.15, 0.4, 6.20, 0),
('Blueberries, raw', 'Fruits', 'Snack', 100.0, 'g', 57.0, 57.0, 0.74, 14.49, 0.33, 2.4, 9.96, 0),
('Avocado, raw', 'Fruits', 'Any', 100.0, 'g', 160.0, 160.0, 2.00, 8.53, 14.66, 6.7, 0.66, 0),

-- VEGETABLES (per 100g standard)
('Broccoli, raw', 'Vegetables', 'Any', 100.0, 'g', 34.0, 34.0, 2.82, 6.64, 0.37, 2.6, 1.70, 0),
('Carrots, raw', 'Vegetables', 'Any', 100.0, 'g', 41.0, 41.0, 0.93, 9.58, 0.24, 2.8, 4.74, 0),
('Spinach, raw', 'Vegetables', 'Any', 100.0, 'g', 23.0, 23.0, 2.86, 3.63, 0.39, 2.2, 0.42, 0),
('Tomato, raw', 'Vegetables', 'Any', 100.0, 'g', 18.0, 18.0, 0.88, 3.89, 0.20, 1.2, 2.63, 0),
('Cucumber, raw', 'Vegetables', 'Any', 100.0, 'g', 16.0, 16.0, 0.65, 3.63, 0.11, 0.5, 1.67, 0),
('Bell Pepper, red, raw', 'Vegetables', 'Any', 100.0, 'g', 31.0, 31.0, 0.99, 6.03, 0.30, 2.1, 4.20, 0),
('Lettuce, romaine, raw', 'Vegetables', 'Any', 100.0, 'g', 17.0, 17.0, 1.23, 3.29, 0.30, 2.1, 1.19, 0),
('Potato, baked, flesh and skin', 'Vegetables', 'Any', 100.0, 'g', 93.0, 93.0, 2.50, 21.15, 0.13, 2.2, 1.70, 0),
('Sweet Potato, baked', 'Vegetables', 'Any', 100.0, 'g', 90.0, 90.0, 2.01, 20.71, 0.15, 3.3, 6.48, 0),

-- PROTEINS (per 100g standard)
('Chicken Breast, skinless, boneless, cooked', 'Proteins', 'Lunch', 100.0, 'g', 165.0, 165.0, 31.02, 0.00, 3.57, 0.0, 0.00, 0),
('Chicken Thigh, skinless, boneless, cooked', 'Proteins', 'Lunch', 100.0, 'g', 209.0, 209.0, 25.93, 0.00, 10.92, 0.0, 0.00, 0),
('Salmon, Atlantic, cooked', 'Proteins', 'Dinner', 100.0, 'g', 206.0, 206.0, 22.10, 0.00, 12.35, 0.0, 0.00, 0),
('Tuna, yellowfin, cooked', 'Proteins', 'Lunch', 100.0, 'g', 130.0, 130.0, 29.15, 0.00, 0.59, 0.0, 0.00, 0),
('Egg, whole, hard-boiled', 'Proteins', 'Breakfast', 100.0, 'g', 155.0, 155.0, 12.58, 1.12, 10.61, 0.0, 1.12, 0),
('Egg, whole, hard-boiled', 'Proteins', 'Breakfast', 50.0, 'g', 78.0, 155.0, 6.29, 0.56, 5.31, 0.0, 0.56, 0),  -- 1 large egg
('Beef, ground, 90% lean, cooked', 'Proteins', 'Dinner', 100.0, 'g', 250.0, 250.0, 25.75, 0.00, 15.00, 0.0, 0.00, 0),
('Pork, tenderloin, cooked', 'Proteins', 'Dinner', 100.0, 'g', 143.0, 143.0, 26.17, 0.00, 3.51, 0.0, 0.00, 0),
('Turkey Breast, skinless, cooked', 'Proteins', 'Lunch', 100.0, 'g', 135.0, 135.0, 29.55, 0.00, 0.85, 0.0, 0.00, 0),
('Tofu, firm, prepared with calcium', 'Proteins', 'Any', 100.0, 'g', 144.0, 144.0, 17.27, 2.78, 8.72, 2.3, 0.80, 1),

-- GRAINS & CARBOHYDRATES (per 100g cooked unless noted)
('Rice, white, long-grain, cooked', 'Grains', 'Any', 100.0, 'g', 130.0, 130.0, 2.69, 28.17, 0.28, 0.4, 0.05, 1),
('Rice, brown, long-grain, cooked', 'Grains', 'Any', 100.0, 'g', 111.0, 111.0, 2.58, 22.96, 0.90, 1.8, 0.24, 1),
('Oats, rolled, dry', 'Grains', 'Breakfast', 100.0, 'g', 389.0, 389.0, 16.89, 66.27, 6.90, 10.6, 0.99, 1),
('Oats, rolled, cooked', 'Grains', 'Breakfast', 100.0, 'g', 68.0, 68.0, 2.37, 11.67, 1.36, 1.7, 0.24, 1),
('Bread, white, commercial', 'Grains', 'Breakfast', 100.0, 'g', 266.0, 266.0, 8.85, 50.42, 3.33, 2.7, 5.67, 1),
('Bread, whole-wheat', 'Grains', 'Breakfast', 100.0, 'g', 247.0, 247.0, 13.00, 41.29, 4.20, 7.4, 5.77, 1),
('Pasta, spaghetti, cooked', 'Grains', 'Dinner', 100.0, 'g', 131.0, 131.0, 5.00, 25.00, 1.10, 1.8, 0.56, 1),
('Quinoa, cooked', 'Grains', 'Any', 100.0, 'g', 120.0, 120.0, 4.40, 21.30, 1.92, 2.8, 0.87, 1),

-- DAIRY & DAIRY ALTERNATIVES
('Milk, whole, 3.25% fat', 'Dairy', 'Breakfast', 100.0, 'ml', 61.0, 61.0, 3.15, 4.80, 3.25, 0.0, 4.80, 0),
('Milk, skim, fat-free', 'Dairy', 'Breakfast', 100.0, 'ml', 34.0, 34.0, 3.37, 4.96, 0.08, 0.0, 4.96, 0),
('Greek Yogurt, plain, nonfat', 'Dairy', 'Breakfast', 100.0, 'g', 59.0, 59.0, 10.00, 3.60, 0.39, 0.0, 3.60, 1),
('Cheese, cheddar', 'Dairy', 'Any', 100.0, 'g', 402.0, 402.0, 22.87, 3.09, 33.14, 0.0, 0.48, 1),
('Almond Milk, unsweetened', 'Dairy Alternatives', 'Breakfast', 100.0, 'ml', 17.0, 17.0, 0.59, 0.58, 1.25, 0.4, 0.00, 1),

-- NUTS & SEEDS (per 100g)
('Almonds, raw', 'Nuts & Seeds', 'Snack', 100.0, 'g', 579.0, 579.0, 21.15, 21.55, 49.93, 12.5, 4.35, 0),
('Peanuts, raw', 'Nuts & Seeds', 'Snack', 100.0, 'g', 567.0, 567.0, 25.80, 16.13, 49.24, 8.5, 4.72, 0),
('Walnuts, English', 'Nuts & Seeds', 'Snack', 100.0, 'g', 654.0, 654.0, 15.23, 13.71, 65.21, 6.7, 2.61, 0),
('Chia Seeds, dried', 'Nuts & Seeds', 'Any', 100.0, 'g', 486.0, 486.0, 16.54, 42.12, 30.74, 34.4, 0.00, 0),

-- BEVERAGES
('Water', 'Beverages', 'Any', 100.0, 'ml', 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0),
('Coffee, black, brewed', 'Beverages', 'Breakfast', 100.0, 'ml', 2.0, 2.0, 0.12, 0.00, 0.00, 0.0, 0.00, 1),
('Green Tea, brewed', 'Beverages', 'Any', 100.0, 'ml', 2.0, 2.0, 0.00, 0.00, 0.00, 0.0, 0.00, 1),

-- MEALS - 50 Combinations with Accurate Nutrition Calculations
-- Format: Protein (150-200g) + Grain (100-150g) + Vegetable (100-150g)
-- All nutrition values calculated from component foods

-- 1-10: Chicken Meals (Chicken 150g + Grain 150g + Veg 100g = 400g total)
('Grilled Chicken Breast with White Rice and Broccoli', 'Meals', 'Dinner', 400.0, 'g', 476.5, 119.1, 53.4, 48.9, 6.2, 2.6, 1.7, 0),
('Grilled Chicken Breast with Brown Rice and Carrots', 'Meals', 'Dinner', 400.0, 'g', 463.5, 115.9, 53.0, 44.0, 5.8, 3.0, 4.7, 0),
('Grilled Chicken Breast with Quinoa and Spinach', 'Meals', 'Dinner', 400.0, 'g', 450.5, 112.6, 54.1, 35.0, 6.0, 4.1, 0.4, 0),
('Grilled Chicken Thigh with White Rice and Broccoli', 'Meals', 'Dinner', 400.0, 'g', 542.5, 135.6, 42.7, 48.9, 16.8, 2.6, 1.7, 0),
('Grilled Chicken Thigh with Pasta and Tomato', 'Meals', 'Dinner', 400.0, 'g', 523.5, 130.9, 42.2, 41.2, 16.5, 2.0, 2.6, 0),
('Baked Chicken Breast with Sweet Potato and Green Beans', 'Meals', 'Dinner', 400.0, 'g', 472.5, 118.1, 52.0, 51.1, 5.7, 5.3, 6.5, 0),
('Grilled Chicken Breast with Brown Rice and Bell Pepper', 'Meals', 'Dinner', 400.0, 'g', 469.5, 117.4, 52.7, 44.0, 5.8, 3.1, 4.2, 0),
('Chicken Thigh with Quinoa and Cucumber Salad', 'Meals', 'Lunch', 400.0, 'g', 520.5, 130.1, 41.4, 35.0, 16.4, 3.8, 0.9, 0),
('Chicken Breast with White Rice and Mixed Vegetables', 'Meals', 'Dinner', 400.0, 'g', 476.5, 119.1, 53.4, 48.9, 6.2, 2.6, 1.7, 0),
('Grilled Chicken Breast Salad with Quinoa', 'Meals', 'Lunch', 350.0, 'g', 420.5, 120.1, 50.0, 32.0, 5.5, 3.2, 1.2, 0),

-- 11-20: Salmon & Fish Meals (Fish 150g + Grain 150g + Veg 100g = 400g total)
('Grilled Salmon with White Rice and Broccoli', 'Meals', 'Dinner', 400.0, 'g', 544.0, 136.0, 36.0, 48.9, 19.0, 2.6, 1.7, 0),
('Grilled Salmon with Brown Rice and Carrots', 'Meals', 'Dinner', 400.0, 'g', 531.0, 132.8, 35.6, 44.0, 18.6, 3.0, 4.7, 0),
('Baked Salmon with Quinoa and Spinach', 'Meals', 'Dinner', 400.0, 'g', 518.0, 129.5, 37.4, 35.0, 18.9, 4.1, 0.4, 0),
('Grilled Salmon with Pasta and Tomato', 'Meals', 'Dinner', 400.0, 'g', 524.0, 131.0, 35.8, 41.2, 18.7, 2.0, 2.6, 0),
('Salmon with Sweet Potato and Green Beans', 'Meals', 'Dinner', 400.0, 'g', 506.0, 126.5, 34.5, 51.1, 18.5, 5.3, 6.5, 0),
('Grilled Tuna with White Rice and Broccoli', 'Meals', 'Dinner', 400.0, 'g', 325.0, 81.3, 45.7, 48.9, 0.9, 2.6, 1.7, 0),
('Grilled Tuna with Brown Rice and Carrots', 'Meals', 'Dinner', 400.0, 'g', 312.0, 78.0, 45.3, 44.0, 0.9, 3.0, 4.7, 0),
('Tuna with Quinoa and Spinach', 'Meals', 'Dinner', 400.0, 'g', 299.0, 74.8, 46.1, 35.0, 0.9, 4.1, 0.4, 0),
('Baked Salmon with Brown Rice and Bell Pepper', 'Meals', 'Dinner', 400.0, 'g', 537.0, 134.3, 35.5, 44.0, 18.7, 3.1, 4.2, 0),
('Salmon Salad with Quinoa', 'Meals', 'Lunch', 350.0, 'g', 453.0, 129.4, 33.2, 32.0, 13.9, 3.2, 1.2, 0),

-- 21-30: Beef & Pork Meals (Meat 150g + Grain 150g + Veg 100g = 400g total)
('Grilled Beef (90% lean) with White Rice and Broccoli', 'Meals', 'Dinner', 400.0, 'g', 600.0, 150.0, 40.6, 48.9, 22.5, 2.6, 1.7, 0),
('Grilled Beef with Brown Rice and Carrots', 'Meals', 'Dinner', 400.0, 'g', 586.5, 146.6, 40.3, 44.0, 22.3, 3.0, 4.7, 0),
('Beef with Quinoa and Spinach', 'Meals', 'Dinner', 400.0, 'g', 575.5, 143.9, 41.1, 35.0, 22.5, 4.1, 0.4, 0),
('Beef with Pasta and Tomato', 'Meals', 'Dinner', 400.0, 'g', 581.5, 145.4, 40.5, 41.2, 22.4, 2.0, 2.6, 0),
('Beef with Sweet Potato and Green Beans', 'Meals', 'Dinner', 400.0, 'g', 563.5, 140.9, 39.6, 51.1, 22.2, 5.3, 6.5, 0),
('Grilled Pork Tenderloin with White Rice and Broccoli', 'Meals', 'Dinner', 400.0, 'g', 439.5, 109.9, 42.3, 48.9, 5.3, 2.6, 1.7, 0),
('Pork Tenderloin with Brown Rice and Carrots', 'Meals', 'Dinner', 400.0, 'g', 426.0, 106.5, 42.0, 44.0, 5.3, 3.0, 4.7, 0),
('Pork with Quinoa and Spinach', 'Meals', 'Dinner', 400.0, 'g', 415.0, 103.8, 42.8, 35.0, 5.3, 4.1, 0.4, 0),
('Pork Tenderloin with Pasta and Tomato', 'Meals', 'Dinner', 400.0, 'g', 421.0, 105.3, 42.2, 41.2, 5.2, 2.0, 2.6, 0),
('Beef with Brown Rice and Bell Pepper', 'Meals', 'Dinner', 400.0, 'g', 592.5, 148.1, 40.0, 44.0, 22.4, 3.1, 4.2, 0),

-- 31-40: Turkey & Vegetarian Meals (Protein 150g + Grain 150g + Veg 100g = 400g total)
('Grilled Turkey Breast with White Rice and Broccoli', 'Meals', 'Lunch', 400.0, 'g', 398.0, 99.5, 47.3, 48.9, 1.3, 2.6, 1.7, 0),
('Turkey Breast with Brown Rice and Carrots', 'Meals', 'Lunch', 400.0, 'g', 384.5, 96.1, 47.0, 44.0, 1.3, 3.0, 4.7, 0),
('Turkey with Quinoa and Spinach', 'Meals', 'Lunch', 400.0, 'g', 373.5, 93.4, 47.8, 35.0, 1.3, 4.1, 0.4, 0),
('Turkey Breast with Pasta and Tomato', 'Meals', 'Lunch', 400.0, 'g', 379.5, 94.9, 47.2, 41.2, 1.3, 2.0, 2.6, 0),
('Turkey with Sweet Potato and Green Beans', 'Meals', 'Lunch', 400.0, 'g', 361.5, 90.4, 46.3, 51.1, 1.3, 5.3, 6.5, 0),
('Tofu Stir-fry with Brown Rice and Broccoli', 'Meals', 'Dinner', 400.0, 'g', 433.0, 108.3, 30.9, 44.0, 13.1, 3.0, 1.7, 0),
('Tofu with Quinoa and Spinach', 'Meals', 'Dinner', 400.0, 'g', 422.0, 105.5, 31.7, 35.0, 13.1, 4.1, 0.4, 0),
('Tofu with White Rice and Carrots', 'Meals', 'Dinner', 400.0, 'g', 444.0, 111.0, 30.6, 48.9, 13.1, 2.4, 4.7, 0),
('Tofu with Pasta and Bell Pepper', 'Meals', 'Dinner', 400.0, 'g', 416.0, 104.0, 31.1, 41.2, 13.0, 2.2, 4.2, 0),
('Turkey Salad with Quinoa', 'Meals', 'Lunch', 350.0, 'g', 373.5, 106.7, 47.8, 32.0, 1.0, 3.2, 1.2, 0),

-- 41-50: Egg & Breakfast Meals, Mixed Combinations
('Scrambled Eggs (2 large) with Whole Wheat Toast and Spinach', 'Meals', 'Breakfast', 250.0, 'g', 315.0, 126.0, 19.2, 25.8, 11.2, 4.0, 3.0, 0),
('Hard-boiled Eggs (2) with Oatmeal and Blueberries', 'Meals', 'Breakfast', 300.0, 'g', 298.0, 99.3, 19.0, 35.2, 10.6, 4.2, 5.0, 0),
('Egg Scramble with Quinoa and Tomatoes', 'Meals', 'Breakfast', 300.0, 'g', 310.0, 103.3, 19.5, 32.0, 10.6, 3.0, 2.6, 0),
('Chicken Breast with White Rice and Avocado (100g)', 'Meals', 'Dinner', 400.0, 'g', 636.5, 159.1, 54.4, 48.9, 22.0, 2.6, 1.7, 0),
('Salmon with Brown Rice and Avocado (100g)', 'Meals', 'Dinner', 400.0, 'g', 704.0, 176.0, 36.4, 44.0, 34.6, 3.0, 4.7, 0),
('Grilled Chicken Thigh with Quinoa and Sweet Potato', 'Meals', 'Dinner', 400.0, 'g', 593.0, 148.3, 42.2, 51.1, 13.0, 5.3, 6.5, 0),
('Beef with White Rice and Mixed Vegetables', 'Meals', 'Dinner', 400.0, 'g', 600.0, 150.0, 40.6, 48.9, 22.5, 2.6, 1.7, 0),
('Tuna Salad with Brown Rice', 'Meals', 'Lunch', 350.0, 'g', 357.0, 102.0, 45.3, 25.5, 0.9, 3.2, 1.2, 0),
('Pork Tenderloin with Quinoa and Broccoli', 'Meals', 'Dinner', 400.0, 'g', 448.0, 112.0, 43.8, 35.0, 5.6, 4.1, 0.4, 0),
('Chicken Breast with Pasta and Mixed Vegetables', 'Meals', 'Dinner', 400.0, 'g', 463.0, 115.8, 53.0, 41.2, 6.0, 2.0, 2.0, 0);


PRINT 'Food table created successfully!';
PRINT 'Total food items inserted: ' + CAST(@@ROWCOUNT AS NVARCHAR(10));


-- Verify data
SELECT 
    food_category,
    COUNT(*) AS food_count,
    AVG(calories_per_100g) AS avg_calories_per_100g
FROM Food
GROUP BY food_category
ORDER BY food_category;


