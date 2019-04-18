--This page will contain the sql statements and data for testing 
-- DELETE DATABASE DATA
DELETE FROM quick_meal_food_items;
DELETE FROM quick_meals;
DELETE FROM food_entries;
DELETE FROM water_entries;
DELETE FROM user_profiles;
DELETE FROM users;


---- INSERT sample user
SET IDENTITY_INSERT users ON;
INSERT INTO users ([id]
	  ,[username]
	  ,[password]
	  ,[salt]
	  ,[role]) VALUES (1, 'Ted', 'Eastbound&Down', 'laskdjf;laskjf', null);
SET IDENTITY_INSERT users OFF;

INSERT INTO user_profiles VALUES (1, 'TedKennedy', 180, 200, '1980-01-01', 6, 2, '1.55','M', '1.55', null);

SET IDENTITY_INSERT food_entries ON;

INSERT INTO food_entries ([id]
		,[userId]
		,[name]
		,[calories]
		,[fat]
		,[protein]
		,[carbohydrates]
		,[meal_type]
		,[meal_date]
		,[servings]
		,[ndbno]) VALUES (1, 1, 'TedKennedy', 1000, 100, 20, 1, 'Snack', getdate(), 1, 3);  

SET IDENTITY_INSERT food_entries OFF;

SET IDENTITY_INSERT favorites ON;

INSERT INTO favorites ([id]
		,[userId]
		,[name]
		,[calories]
		,[fat]
		,[protein]
		,[carbs]
		,[ndbno]) VALUES (1,1,'Bacon', 600, 20, 20, 1,1);

SET IDENTITY_INSERT favorites OFF;
