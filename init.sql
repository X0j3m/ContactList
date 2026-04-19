DROP DATABASE IF EXISTS contact_list_db;
CREATE DATABASE contact_list_db;

USE contact_list_db;

CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;
CREATE TABLE `Categories` (
    `Id` char(36) NOT NULL,
    `Name` varchar(100) NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `SubCategories` (
    `Id` char(36) NOT NULL,
    `Name` varchar(100) NOT NULL,
    `CategoryId` char(36) NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_SubCategories_Categories_CategoryId` FOREIGN KEY (`CategoryId`) REFERENCES `Categories` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Contacts` (
    `Id` char(36) NOT NULL,
    `Name` varchar(100) NOT NULL,
    `Surname` varchar(100) NOT NULL,
    `Email` varchar(200) NOT NULL,
    `Password` longtext NOT NULL,
    `CategoryId` char(36) NULL,
    `SubCategoryId` char(36) NULL,
    `CustomSubCategory` longtext NULL,
    `Phone` varchar(50) NOT NULL,
    `BirthDate` datetime(6) NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Contacts_Categories_CategoryId` FOREIGN KEY (`CategoryId`) REFERENCES `Categories` (`Id`) ON DELETE SET NULL,
    CONSTRAINT `FK_Contacts_SubCategories_SubCategoryId` FOREIGN KEY (`SubCategoryId`) REFERENCES `SubCategories` (`Id`) ON DELETE SET NULL
);

INSERT INTO `Categories` (`Id`, `Name`)
VALUES ('020b977f-e9f0-4310-af8e-1be21fab0a77', 'Prywatny');
SELECT ROW_COUNT();

INSERT INTO `Categories` (`Id`, `Name`)
VALUES ('462cdacf-1d2e-49d7-af10-aefad2d29459', 'Służbowy');
SELECT ROW_COUNT();


INSERT INTO `SubCategories` (`Id`, `CategoryId`, `Name`)
VALUES ('b1a7c6d8-3f44-4c9a-9f1a-111111111111', '462cdacf-1d2e-49d7-af10-aefad2d29459', 'Szef');
SELECT ROW_COUNT();

INSERT INTO `SubCategories` (`Id`, `CategoryId`, `Name`)
VALUES ('b2a7c6d8-3f44-4c9a-9f1a-222222222222', '462cdacf-1d2e-49d7-af10-aefad2d29459', 'Sprzedaż');
SELECT ROW_COUNT();

INSERT INTO `SubCategories` (`Id`, `CategoryId`, `Name`)
VALUES ('b3a7c6d8-3f44-4c9a-9f1a-333333333333', '462cdacf-1d2e-49d7-af10-aefad2d29459', 'Kontrahent');
SELECT ROW_COUNT();

INSERT INTO `SubCategories` (`Id`, `CategoryId`, `Name`)
VALUES ('b4a7c6d8-3f44-4c9a-9f1a-444444444444', '462cdacf-1d2e-49d7-af10-aefad2d29459', 'Dział IT');
SELECT ROW_COUNT();

INSERT INTO `SubCategories` (`Id`, `CategoryId`, `Name`)
VALUES ('c1a7c6d8-3f44-4c9a-9f1a-555555555555', '020b977f-e9f0-4310-af8e-1be21fab0a77', 'Rodzina');
SELECT ROW_COUNT();

INSERT INTO `SubCategories` (`Id`, `CategoryId`, `Name`)
VALUES ('c2a7c6d8-3f44-4c9a-9f1a-666666666666', '020b977f-e9f0-4310-af8e-1be21fab0a77', 'Przyjaciele');
SELECT ROW_COUNT();

INSERT INTO `SubCategories` (`Id`, `CategoryId`, `Name`)
VALUES ('c3a7c6d8-3f44-4c9a-9f1a-777777777777', '020b977f-e9f0-4310-af8e-1be21fab0a77', 'Znajomi');
SELECT ROW_COUNT();

INSERT INTO `SubCategories` (`Id`, `CategoryId`, `Name`)
VALUES ('c4a7c6d8-3f44-4c9a-9f1a-888888888888', '020b977f-e9f0-4310-af8e-1be21fab0a77', 'Sąsiedzi');
SELECT ROW_COUNT();


CREATE INDEX `IX_Contacts_CategoryId` ON `Contacts` (`CategoryId`);

CREATE UNIQUE INDEX `IX_Contacts_Email` ON `Contacts` (`Email`);

CREATE INDEX `IX_Contacts_SubCategoryId` ON `Contacts` (`SubCategoryId`);

CREATE INDEX `IX_SubCategories_CategoryId` ON `SubCategories` (`CategoryId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20260419083800_InitialCreate', '10.0.6');

COMMIT;

