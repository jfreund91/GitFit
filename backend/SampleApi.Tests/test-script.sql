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
INSERT INTO users VALUES (1, 'KennyPowers', 'Eastbound&Down', 'laskdjf;laskjf', 'user');
SET IDENTITY_INSERT users OFF;
INSERT INTO user_profiles VALUES (1, 'KennyPowers', 180, 200, 1980, 6, 2, 1.55,'M', 


---- INSERT sample survey
--INSERT INTO survey_result VALUES (@parkCode,'email@email.com','PA','active')
--DECLARE @surveyId int = (SELECT @@IDENTITY);

------ INSERT sample weather
--INSERT INTO weather VALUES (@parkCode,1,20,25,'rain')
--DECLARE @weather varchar(4) = (@parkCode);

---- Return reservation ID
--SELECT @parkCode as parkCode, @surveyId as surveyId