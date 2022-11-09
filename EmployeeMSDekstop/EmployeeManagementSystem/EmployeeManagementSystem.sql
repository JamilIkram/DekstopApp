drop database EmployeeManagementSystem
Create database EmployeeManagementSystem

use EmployeeManagementSystem

Create table Depertment
(
Dep_ID int primary key not null,
Dep_Name varchar(40)
)
select*from Depertment;

Create table Designation
(
Des_ID int primary key not null,
Des_Name varchar(40),
Salary int 
)
select*from Designation;
drop table Designation;

Create table EmployeeInfo
(
Emp_ID int Primary key not null,
Emp_Name varchar(50),
Emp_Address varchar(50),
Emp_Contact varchar(50),
Emp_DOB datetime,
Emp_Gender varchar(50),
Emp_image nvarchar(500),
Dep_ID int references Depertment(Dep_ID),
Des_ID int references Designation(Des_ID)
)
select*from EmployeeInfo;