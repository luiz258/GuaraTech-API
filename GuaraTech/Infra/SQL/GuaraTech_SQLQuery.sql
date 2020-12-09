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
     StateCourse varchar(2)
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
    ValueOffer VARCHAR(600),
    CustomerSegment VARCHAR(600),
    CommunicationChannels VARCHAR(600),
    CustomerRelationship VARCHAR(600),
    KeyFeatures VARCHAR(600),
    MainActivities VARCHAR(600),
    Partnerships VARCHAR(600),
    Recipe VARCHAR(600),
    Cost VARCHAR(600),
)


CREATE TABLE TEAM_CANVAS (
    ID UNIQUEIDENTIFIER PRIMARY KEY,
    IdCanvas Uniqueidentifier  FOREIGN KEY REFERENCES CANVAS(ID),
    IdUserGuests Uniqueidentifier  FOREIGN KEY REFERENCES ACCOUNT(ID)
)
