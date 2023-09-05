drop database LodgeSystem

create database LodgeSystemManagement

use LodgeSystemManagement

CREATE TABLE UserList (
  UserID INT PRIMARY KEY IDENTITY(1, 1),
  FullName NVARCHAR(100) NOT NULL,
  Address NVARCHAR(200) NULL,
  PhoneNo NVARCHAR(20) UNIQUE NOT NULL,
  Email NVARCHAR(100) NULL,
  Password NVARCHAR(100) NOT NULL,
  Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Customer', 'Admin', 'Manager', 'Staff')) DEFAULT 'Customer'
);


CREATE TABLE Booking (
  BookingID INT PRIMARY KEY IDENTITY(1, 1),
  BookingDate DATETIME NOT NULL,
  check_in_date DATETIME NOT NULL,
  check_out_date DATETIME NOT NULL,
  actual_check_in_date DATETIME NULL,
  actual_check_out_date DATETIME NULL,
  no_Of_Person INT NOT NULL,
  total_Rooms INT NOT NULL,
  Deposit_amount DECIMAL(10, 2) NOT NULL,
  UserId INT NOT NULL,
  check_in_by INT NULL,
  check_out_by INT NULL,
  FOREIGN KEY (UserId) REFERENCES UserList (UserID),
  FOREIGN KEY (check_in_by) REFERENCES UserList (UserID),
  FOREIGN KEY (check_out_by) REFERENCES UserList (UserID)
);

CREATE TABLE RoomCategory (
  CategoryID INT PRIMARY KEY IDENTITY(1, 1),
  name NVARCHAR(100) NOT NULL,
  rate DECIMAL(10, 2) NULL
);

CREATE TABLE Rooms (
  RoomID INT PRIMARY KEY IDENTITY(1, 1),
  Title NVARCHAR(100) NOT NULL,
  Description NVARCHAR(MAX) NOT NULL,
  image_path NVARCHAR(200) NULL,
  capacity INT,
  no_of_bed INT,
  price_per_day DECIMAL(10, 2) NOT NULL,
  status NVARCHAR(50) NULL,
  CategoryID INT NOT NULL,
  FOREIGN KEY (CategoryID) REFERENCES RoomCategory (CategoryID)
);

CREATE TABLE Cart (
  CartID INT PRIMARY KEY IDENTITY(1, 1),
  UserId INT NOT NULL,
  RoomId INT UNIQUE NOT NULL,
  check_in_date DATETIME NOT NULL,
  check_out_date DATETIME NOT NULL,
  "TimeStamp" DATETIME DEFAULT GETDATE(),
  FOREIGN KEY (UserId) REFERENCES UserList (UserId),
  FOREIGN KEY (RoomId) REFERENCES Rooms (RoomId)
);

CREATE TABLE Booking_Detail (
  ID INT PRIMARY KEY IDENTITY(1, 1),
  RoomID INT NOT NULL,
  BookingID INT NOT NULL,
  FOREIGN KEY (RoomID) REFERENCES Rooms (RoomID),
  FOREIGN KEY (BookingID) REFERENCES Booking (BookingID)
);




CREATE TABLE Foods (
  FoodID INT PRIMARY KEY IDENTITY(1, 1),
  Name NVARCHAR(100) NOT NULL,
  Description NVARCHAR(MAX) NULL,
  Price DECIMAL(10, 2) NOT NULL,
  Category NVARCHAR(50) NOT NULL,
  image_path NVARCHAR(200) NULL
);

CREATE TABLE FoodDetail (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BookingId INT,
    Date DATETIME,
    Quantity INT,
    Price DECIMAL(18, 2),
    AmountPaid DECIMAL(18, 2),
    FoodId INT,
    FOREIGN KEY (BookingId) REFERENCES Booking(BookingId),
    FOREIGN KEY (FoodId) REFERENCES Foods(FoodId)
);


CREATE TABLE Invoice (
  InvoiceId INT PRIMARY KEY IDENTITY(1, 1),
  PaymentStatus NVARCHAR(50) NULL,
  InvoiceDate DATE NOT NULL,
  TotalAmount DECIMAL(10, 2) NOT NULL,
  Billing_By INT NOT NULL,
  BookingID INT NOT NULL,
  FOREIGN KEY (Billing_By) REFERENCES UserList (UserID),
  FOREIGN KEY (BookingID) REFERENCES Booking (BookingID)
);


CREATE TABLE Feedback (
  ID INT PRIMARY KEY IDENTITY(1, 1),
  Name NVARCHAR(100) NOT NULL,
  Date DATETIME NOT NULL DEFAULT GETDATE(),
  Message NVARCHAR(MAX) NOT NULL,
  Email NVARCHAR(100) NOT NULL,
  PhoneNo NVARCHAR(20) NULL,
  "Read" INT NOT NULL DEFAULT 0
);



CREATE VIEW RoomInvoice as 
SELECT 
    Booking_Detail.ID,
    R.RoomId, 
    U.FullName,
    B.BookingId, 
    R.Title,
    R.Price_per_day,
	B.actual_check_in_date, B.actual_check_out_date,
    CASE WHEN DATEDIFF(DAY, B.actual_check_in_date, B.actual_check_out_date) < 1 THEN 1
         ELSE CONVERT(decimal(10, 2), DATEDIFF(MINUTE, B.actual_check_in_date, B.actual_check_out_date)/CAST(60 * 24 AS decimal(10, 2)))
    END AS no_of_days,
    B.Deposit_amount,
   CAST((R.Price_per_day * CASE WHEN DATEDIFF(DAY, B.actual_check_in_date, B.actual_check_out_date) < 1 THEN 1
                            ELSE CONVERT(decimal(10, 2), DATEDIFF(MINUTE, B.actual_check_in_date, B.actual_check_out_date) / CAST(60 * 24 AS decimal(10, 2)))
                       END) AS decimal(18, 2)) AS Total_Price

FROM 
    Booking B
JOIN 
    Booking_Detail ON B.BookingId = Booking_Detail.BookingID
JOIN 
    Rooms R ON R.RoomId = Booking_Detail.RoomId
JOIN
    UserList U ON B.UserId = U.UserId;


CREATE VIEW FoodInvoice as 
select  fd.Id, U.FullName, B.BookingId, f.Name, f.Price, fd.Quantity, fd.AmountPaid,((f.Price* fd.Quantity)-fd.AmountPaid) as Total_Price
from Booking B join FoodDetail fd on B.BookingId = fd.BookingID
join Foods f on f.FoodId = fd.FoodId join
UserList U on B.UserId = U.UserId 

create view RoomCart as
Select C.CartId, C.UserId, C."TimeStamp", R.Title, R.RoomID, R.price_per_day, C.check_in_date, C.check_out_date from Cart C join Rooms R 
on R.RoomID = C.RoomId






 Scaffold-dbcontext -Connection "name=con" Microsoft.entityframeworkcore.sqlserver -outputdir Models -f
