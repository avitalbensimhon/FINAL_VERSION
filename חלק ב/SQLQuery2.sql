use project_hadasim

CREATE TABLE Family (
Person_Id INT PRIMARY KEY,
Personal_Name NVARCHAR(50),
Family_Name NVARCHAR(50),
Gender NVARCHAR(10),
Father_Id INT,
Mother_Id INT,
Spouse_Id INT
);

INSERT INTO Family (Person_Id, Personal_Name, Family_Name, Gender, Father_Id, Mother_Id, Spouse_Id) VALUES
(111, 'יוסי', 'לוי', 'זכר', NULL, NULL, 222),
(222, 'מיכל', 'כהן', 'נקבה', NULL, NULL, 111),
(333, 'דניאל', 'לוי', 'זכר', 111, 222, NULL),
(444, 'מאיה', 'לוי', 'נקבה', 111, 222, NULL);

CREATE TABLE Family_Relationship (
Person_Id INT,
Relative_Id INT,
Connection_Type NVARCHAR(20),
PRIMARY KEY (Person_Id, Relative_Id),
FOREIGN KEY (Person_Id) REFERENCES Family(Person_Id),
FOREIGN KEY (Relative_Id) REFERENCES Family(Person_Id)
);

INSERT INTO Family_Relationship (Person_Id, Relative_Id, Connection_Type) VALUES
(111, 222, 'בת זוג'),
(222, 111, 'בן זוג'),
(111, 333, 'בן'),
(111, 444, 'בת'),
(222, 333, 'בן'),
(222, 444, 'בת'),
(333, 444, 'אחות'),
(444, 333, 'אח');


INSERT INTO Family_Relationship (Person_Id, Relative_Id, Connection_Type)
SELECT f1.Person_Id, f1.Spouse_Id, 
       CASE 
           WHEN f1.Gender = 'זכר' THEN 'בן זוג'
           WHEN f1.Gender = 'נקבה' THEN 'בת זוג'
           ELSE 'לא ידוע' 
       END AS Connection_Type
FROM Family f1
WHERE f1.Spouse_Id IS NOT NULL
  AND f1.Person_Id != f1.Spouse_Id  -- מוודא שאדם לא מתווסף כבן זוג לעצמו
  AND NOT EXISTS (
    SELECT 1
    FROM Family_Relationship fr
    WHERE fr.Person_Id = f1.Person_Id AND fr.Relative_Id = f1.Spouse_Id
);



