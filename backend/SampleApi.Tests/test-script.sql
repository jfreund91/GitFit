--This page will contain the sql statements and data for testing 
-- DELETE DATABASE DATA
DELETE FROM quick_meal_food_items;
DELETE FROM quick_meals;
DELETE FROM food_entries;
DELETE FROM water_entries;
DELETE FROM user_profiles;
DELETE FROM users;


---- INSERT sample park
--INSERT INTO park VALUES ('CVNP','name','st',10,11,12,13,'tropical',2000,100,'look out','abraham lincoln','oh what a park',6000,1)
--DECLARE @parkCode varchar(4) = ('CVNP')

---- INSERT sample survey
--INSERT INTO survey_result VALUES (@parkCode,'email@email.com','PA','active')
--DECLARE @surveyId int = (SELECT @@IDENTITY);

------ INSERT sample weather
--INSERT INTO weather VALUES (@parkCode,1,20,25,'rain')
--DECLARE @weather varchar(4) = (@parkCode);

---- Return reservation ID
--SELECT @parkCode as parkCode, @surveyId as surveyId