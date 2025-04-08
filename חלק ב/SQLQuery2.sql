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
(111, '����', '���', '���', NULL, NULL, 222),
(222, '����', '���', '����', NULL, NULL, 111),
(333, '�����', '���', '���', 111, 222, NULL),
(444, '����', '���', '����', 111, 222, NULL);

CREATE TABLE Family_Relationship (
Person_Id INT,
Relative_Id INT,
Connection_Type NVARCHAR(20),
PRIMARY KEY (Person_Id, Relative_Id),
FOREIGN KEY (Person_Id) REFERENCES Family(Person_Id),
FOREIGN KEY (Relative_Id) REFERENCES Family(Person_Id)
);

INSERT INTO Family_Relationship (Person_Id, Relative_Id, Connection_Type) VALUES
(111, 222, '�� ���'),
(222, 111, '�� ���'),
(111, 333, '��'),
(111, 444, '��'),
(222, 333, '��'),
(222, 444, '��'),
(333, 444, '����'),
(444, 333, '��');


INSERT INTO Family_Relationship (Person_Id, Relative_Id, Connection_Type)
SELECT f1.Person_Id, f1.Spouse_Id, 
       CASE 
           WHEN f1.Gender = '���' THEN '�� ���'
           WHEN f1.Gender = '����' THEN '�� ���'
           ELSE '�� ����' 
       END AS Connection_Type
FROM Family f1
WHERE f1.Spouse_Id IS NOT NULL
  AND f1.Person_Id != f1.Spouse_Id  -- ����� ���� �� ������ ��� ��� �����
  AND NOT EXISTS (
    SELECT 1
    FROM Family_Relationship fr
    WHERE fr.Person_Id = f1.Person_Id AND fr.Relative_Id = f1.Spouse_Id
);



