-- Switch to the system (aka master) database
USE master;
GO

-- Delete the DemoDB Database (IF EXISTS)
IF EXISTS(select * from sys.databases where name='DemoDB')
DROP DATABASE DemoDB;
GO

-- Create a new DemoDB Database
CREATE DATABASE DemoDB;
GO

-- Switch to the DemoDB Database
USE DemoDB
GO

BEGIN TRANSACTION;

CREATE TABLE users
(
	id			int				identity(1,1),
	username	varchar(50)		not null,
	password	nvarchar(50)	not null,
	salt		varchar(50)		not null,
	role		varchar(50)		default('user'),

	constraint pk_users primary key (id)
);

CREATE TABLE user_profiles
(
	userId			int			not null,
	[name]			varchar(50)	not null,
	currentWeight	decimal		not null,
	goalWeight		decimal		not null,
	birthDate		date		not null,
	feet			int			not null,
	inches			int			not null,
	activityLevel	varchar(25) not null,
	gender			char		check(gender = 'M' OR gender='F'),
	timeline		varchar(10)	not null,
	[image]			varchar(200) default('https://res.cloudinary.com/gitfit/image/upload/v1555524869/true_gpo4zq.png')
	
	constraint pk_user_profile primary key (userId),
	constraint fk_user_id foreign key (userId) REFERENCES users(id)
);

CREATE TABLE food_entries
(
	id				int			identity(1,1),
	userId			int			not null,
	[name]			varchar(300) not null,
	calories		decimal		not null,
	fat				decimal		not null,
	protein			decimal		not null,
	carbohydrates	decimal		not null,
	meal_type		varchar(25)	not null,
	meal_date		date	not null,
	servings		int			not null,
	ndbno			int			not null,

	constraint pk_food_entry primary key (id),
	constraint fk_userId_food_entry foreign key (userId) references users(id)
);

CREATE TABLE water_entries
(
	id				int			identity(1,1),
	userId			int			not null,
	entry_date		date		not null,

	constraint pk_water_entry primary key (id),
	constraint fk_user_water_entry foreign key (userId) references users(id)
);

CREATE TABLE quick_meals
(
	id				int				identity(1,1),
	userId			int					not null,
	[name]			varchar(50)			not null,

	constraint pk_quick_meal primary key (id),
	constraint fk_userId_quick_meal foreign key (userId) references users(id)
);

CREATE TABLE quick_meal_food_items
(
	id				int				identity(1,1),
	meal_id			int					not null,
	[name]			varchar(300)			not null,
	calories		decimal				not null,
	fat				decimal				not null,
	protein			decimal				not null,
	carbs			decimal				not null,
	servings		decimal				not null,

	constraint pk_quick_meal_food_items primary key (id),
	constraint fk_meal_id foreign key (meal_id) references quick_meals(id)
);

CREATE TABLE favorites
(
	id				int				identity(1,1),
	userId			int				not null,
	[name]			varchar(300)	not null,
	calories		decimal			not null,
	fat				decimal			not null,
	protein			decimal			not null,
	carbs			decimal			not null,
	ndbno			int				not null,

	constraint pk_favorites	primary key (id),
	constraint fk_user_favorites foreign key (userId) references users(id)
);

--Seed the data------------
INSERT INTO users VALUES('bob','6XBYZ2aLj4ZuTkhdIh9cxtGTel4=','+gmsp403F6Q=','User');
INSERT INTO user_profiles VALUES
(1, 'Bob', 350, 501, '2000-10-10', 6, 7, '1.375', 'F', '+998', 'https://res.cloudinary.com/gitfit/image/upload/v1555628872/fitbar_pruoo9.jpg');
INSERT INTO food_entries VALUES
(1,'TACO BELL, Taco Salad',906,49,36,80,'Snack','2019-04-16',1,21270),
(1,'TACO BELL, Taco Salad',906,49,36,80,'Snack','2019-04-15',1,21270),
(1,'TACO BELL, Taco Salad',906,49,36,80,'Snack','2019-04-14',1,21270),
(1,'TACO BELL, Taco Salad',906,49,36,80,'Snack','2019-04-13',1,21270),
(1,'TACO BELL, Taco Salad',906,49,36,80,'Snack','2019-04-12',1,21270),
(1,'TACO BELL, Taco Salad',906,49,36,80,'Snack','2019-04-11',1,21270);

COMMIT TRANSACTION;
