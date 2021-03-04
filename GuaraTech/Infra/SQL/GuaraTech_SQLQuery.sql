DROP TABLE LESSON
DROP TABLE USER_COURSES
DROP TABLE COURSE
DROP TABLE ACCOUNT


CREATE TABLE  ACCOUNT(
    ID Uniqueidentifier  PRIMARY KEY,
    EmailUser varchar(50) ,
    PasswordUser varchar(100) ,
    FullName VARCHAR(50),
    Document VARCHAR(25),
    RoleId varchar(30),
    StateAccount VARCHAR(2)
);



CREATE TABLE COURSE(
     ID Uniqueidentifier PRIMARY KEY,
     Title VARCHAR(50),
     Details VARCHAR(220),
     StateCourse int, 
     Tag int
)

CREATE TABLE LESSON(
     ID Uniqueidentifier PRIMARY KEY,
     IdCourse Uniqueidentifier  FOREIGN KEY REFERENCES COURSE(ID),
     Title VARCHAR(50),
     OrderVideo Float,
     UrlVideo VARCHAR(300),
     Duration VARCHAR(220),
     StateLesson varchar(2)

)

CREATE TABLE  USER_COURSES(
    ID Uniqueidentifier  PRIMARY KEY,
    IdCourse Uniqueidentifier  FOREIGN KEY REFERENCES COURSE(ID),
    IdUser Uniqueidentifier  FOREIGN KEY REFERENCES ACCOUNT(ID),
    PurchaseDate DATEtIME,

     
)


CREATE TABLE CANVAS (
    ID UNIQUEIDENTIFIER PRIMARY KEY,
    IdUser Uniqueidentifier  FOREIGN KEY REFERENCES ACCOUNT(ID),
    Title VARCHAR(65),
    IsPrivate bit,
    CanvasState int
)

CREATE TABLE CANVAS_POSTIT (
    ID UNIQUEIDENTIFIER PRIMARY KEY,
    IdCanvas Uniqueidentifier  FOREIGN KEY REFERENCES CANVAS(ID),
    DescriptionPostit VARCHAR(230),
    ColorPostit VARCHAR(10),
    CanvasTypeBlock int,
)

CREATE TABLE TEAM_CANVAS (
    ID UNIQUEIDENTIFIER PRIMARY KEY,
    IdCanvas Uniqueidentifier  FOREIGN KEY REFERENCES CANVAS(ID),
    IdUserGuests Uniqueidentifier  FOREIGN KEY REFERENCES ACCOUNT(ID)
)
