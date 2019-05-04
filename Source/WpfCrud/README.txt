*This is an example program that demonstrates CRUD operations with MySQL,C# WPF, and Telerik's controls*

For this program to function correctly. A few things need to happened first...

1. Create a MySql Server either local or AWS RDS for a cloud solution

2. Create the Employees Table (here is a quick copy/paste to match what mine was)
CREATE TABLE `DemoDB`.`Employees` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `FirstName` VARCHAR(45) NULL,
  `LastName` VARCHAR(45) NULL,
  `Title` VARCHAR(45) NULL,
  `Salary` INT NULL,
  PRIMARY KEY (`ID`));

3. Create the Products Table
CREATE TABLE `DemoDB`.`Products` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NULL,
  `Price` DOUBLE NULL,
  `ReleaseDate` DATETIME NULL,
  PRIMARY KEY (`ID`));

4. Change the connection string in App.config to match your server