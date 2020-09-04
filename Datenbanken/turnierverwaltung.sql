-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Erstellungszeit: 04. Sep 2020 um 12:36
-- Server-Version: 10.1.37-MariaDB
-- PHP-Version: 7.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `turnierverwaltung`
--
CREATE DATABASE IF NOT EXISTS `turnierverwaltung` DEFAULT CHARACTER SET latin1 COLLATE latin1_german1_ci;
USE `turnierverwaltung`;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `accounts`
--

DROP TABLE IF EXISTS `accounts`;
CREATE TABLE IF NOT EXISTS `accounts` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Person` int(11) NOT NULL,
  `Benutzername` text COLLATE latin1_german1_ci NOT NULL,
  `Passwort` text COLLATE latin1_german1_ci NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `Person` (`Person`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;

--
-- TRUNCATE Tabelle vor dem Einfügen `accounts`
--

TRUNCATE TABLE `accounts`;
--
-- Daten für Tabelle `accounts`
--

INSERT INTO `accounts` (`ID`, `Person`, `Benutzername`, `Passwort`) VALUES
(1, 1, 'user', 'user'),
(2, 2, 'admin', 'admin');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `andereaufgaben`
--

DROP TABLE IF EXISTS `andereaufgaben`;
CREATE TABLE IF NOT EXISTS `andereaufgaben` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Einsatz` text NOT NULL,
  `person` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `person` (`person`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- TRUNCATE Tabelle vor dem Einfügen `andereaufgaben`
--

TRUNCATE TABLE `andereaufgaben`;
-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `fussballspieler`
--

DROP TABLE IF EXISTS `fussballspieler`;
CREATE TABLE IF NOT EXISTS `fussballspieler` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `geschossenetore` int(11) NOT NULL,
  `anzahlspiele` int(11) NOT NULL,
  `position` text NOT NULL,
  `person` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `person` (`person`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4;

--
-- TRUNCATE Tabelle vor dem Einfügen `fussballspieler`
--

TRUNCATE TABLE `fussballspieler`;
--
-- Daten für Tabelle `fussballspieler`
--

INSERT INTO `fussballspieler` (`ID`, `geschossenetore`, `anzahlspiele`, `position`, `person`) VALUES
(5, 498, 348, 'Stürmer', 7);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gruppen`
--

DROP TABLE IF EXISTS `gruppen`;
CREATE TABLE IF NOT EXISTS `gruppen` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` text NOT NULL,
  `sportart` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `sportart` (`sportart`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- TRUNCATE Tabelle vor dem Einfügen `gruppen`
--

TRUNCATE TABLE `gruppen`;
-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gruppenmitglieder`
--

DROP TABLE IF EXISTS `gruppenmitglieder`;
CREATE TABLE IF NOT EXISTS `gruppenmitglieder` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Person` int(11) NOT NULL,
  `Gruppe` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `Person` (`Person`),
  KEY `Gruppe` (`Gruppe`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- TRUNCATE Tabelle vor dem Einfügen `gruppenmitglieder`
--

TRUNCATE TABLE `gruppenmitglieder`;
-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `handballspieler`
--

DROP TABLE IF EXISTS `handballspieler`;
CREATE TABLE IF NOT EXISTS `handballspieler` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `geworfenetore` int(11) NOT NULL,
  `anzahlspiele` int(11) NOT NULL,
  `einsatzbereich` text NOT NULL,
  `person` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `person` (`person`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- TRUNCATE Tabelle vor dem Einfügen `handballspieler`
--

TRUNCATE TABLE `handballspieler`;
-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `mannschaften`
--

DROP TABLE IF EXISTS `mannschaften`;
CREATE TABLE IF NOT EXISTS `mannschaften` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` text NOT NULL,
  `sportart` int(11) NOT NULL,
  `punkte` int(11) NOT NULL,
  `toreplus` int(11) NOT NULL,
  `toreminus` int(11) NOT NULL,
  `anzahlspiele` int(11) NOT NULL,
  `gewonnenespiele` int(11) NOT NULL,
  `verlorenespiele` int(11) NOT NULL,
  `unentschieden` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `sportart` (`sportart`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4;

--
-- TRUNCATE Tabelle vor dem Einfügen `mannschaften`
--

TRUNCATE TABLE `mannschaften`;
--
-- Daten für Tabelle `mannschaften`
--

INSERT INTO `mannschaften` (`ID`, `Name`, `sportart`, `punkte`, `toreplus`, `toreminus`, `anzahlspiele`, `gewonnenespiele`, `verlorenespiele`, `unentschieden`) VALUES
(2, 'FC Augsburg', 1, 0, 0, 0, 0, 0, 0, 0),
(3, 'Hertha BSC', 1, 0, 0, 0, 0, 0, 0, 0),
(4, 'Union Berlin', 1, 0, 0, 0, 0, 0, 0, 0),
(5, 'Arminia Bielefeld', 1, 0, 0, 0, 0, 0, 0, 0),
(6, 'Werder Bremen', 1, 0, 0, 0, 0, 0, 0, 0),
(7, 'Borussia Dortmund', 1, 0, 0, 0, 0, 0, 0, 0),
(8, 'Eintracht Frankfurt', 1, 0, 0, 0, 0, 0, 0, 0),
(9, 'SC Freiburg', 1, 0, 0, 0, 0, 0, 0, 0),
(10, 'TSG Hoffenheim', 1, 0, 0, 0, 0, 0, 0, 0),
(11, '1.FC Köln', 1, 0, 0, 0, 0, 0, 0, 0),
(12, 'RB Leibzig', 1, 0, 0, 0, 0, 0, 0, 0),
(13, 'Bayer 04 Leverkusen', 1, 0, 0, 0, 0, 0, 0, 0),
(14, 'FSV Mainz 05', 1, 0, 0, 0, 0, 0, 0, 0),
(15, 'Borussia Mönchengladbach', 1, 0, 0, 0, 0, 0, 0, 0),
(16, 'FC Bayern München', 1, 0, 0, 0, 0, 0, 0, 0),
(17, 'FC Schalke 04', 1, 0, 0, 0, 0, 0, 0, 0),
(18, 'VFB Stuttgart', 1, 0, 0, 0, 0, 0, 0, 0),
(19, 'VFL Wolfsburg', 1, 0, 0, 0, 0, 0, 0, 0);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `mannschaftsmitglieder`
--

DROP TABLE IF EXISTS `mannschaftsmitglieder`;
CREATE TABLE IF NOT EXISTS `mannschaftsmitglieder` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Person` int(11) NOT NULL,
  `Mannschaft` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `Mannschaft` (`Mannschaft`),
  KEY `Person` (`Person`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

--
-- TRUNCATE Tabelle vor dem Einfügen `mannschaftsmitglieder`
--

TRUNCATE TABLE `mannschaftsmitglieder`;
--
-- Daten für Tabelle `mannschaftsmitglieder`
--

INSERT INTO `mannschaftsmitglieder` (`ID`, `Person`, `Mannschaft`) VALUES
(2, 7, 16);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `personen`
--

DROP TABLE IF EXISTS `personen`;
CREATE TABLE IF NOT EXISTS `personen` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` text NOT NULL,
  `Vorname` text NOT NULL,
  `Geburtsdatum` text NOT NULL,
  `Sportart` int(11) NOT NULL,
  `Details` int(11) DEFAULT NULL,
  `typ` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `Sportart` (`Sportart`),
  KEY `typ` (`typ`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4;

--
-- TRUNCATE Tabelle vor dem Einfügen `personen`
--

TRUNCATE TABLE `personen`;
--
-- Daten für Tabelle `personen`
--

INSERT INTO `personen` (`ID`, `Name`, `Vorname`, `Geburtsdatum`, `Sportart`, `Details`, `typ`) VALUES
(1, 'user', 'user', '1.1.2000', 1, NULL, 8),
(2, 'admin', 'admin', '1.1.2000', 1, NULL, 8),
(7, 'Müller', 'Gerd', '03.12.1945', 1, 5, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `personentypen`
--

DROP TABLE IF EXISTS `personentypen`;
CREATE TABLE IF NOT EXISTS `personentypen` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `bezeichnung` text NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4;

--
-- TRUNCATE Tabelle vor dem Einfügen `personentypen`
--

TRUNCATE TABLE `personentypen`;
--
-- Daten für Tabelle `personentypen`
--

INSERT INTO `personentypen` (`ID`, `bezeichnung`) VALUES
(1, 'Fussballspieler'),
(2, 'Handballspieler'),
(3, 'Tennisspieler'),
(4, 'WeitererSpieler'),
(5, 'Physiotherapeut'),
(6, 'Trainer'),
(7, 'AndereAufgaben'),
(8, 'Allgemeine Useraccount');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `physiotherapeut`
--

DROP TABLE IF EXISTS `physiotherapeut`;
CREATE TABLE IF NOT EXISTS `physiotherapeut` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `anzahljahre` int(11) NOT NULL,
  `person` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `person` (`person`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- TRUNCATE Tabelle vor dem Einfügen `physiotherapeut`
--

TRUNCATE TABLE `physiotherapeut`;
-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `spiele`
--

DROP TABLE IF EXISTS `spiele`;
CREATE TABLE IF NOT EXISTS `spiele` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Turnier` int(11) NOT NULL,
  `Spieltag` int(11) NOT NULL,
  `Teilnehmer1` int(11) NOT NULL,
  `Teilnehmer2` int(11) NOT NULL,
  `Ergebnis1` int(11) NOT NULL,
  `Ergebnis2` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `Turnier` (`Turnier`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- TRUNCATE Tabelle vor dem Einfügen `spiele`
--

TRUNCATE TABLE `spiele`;
-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `sportarten`
--

DROP TABLE IF EXISTS `sportarten`;
CREATE TABLE IF NOT EXISTS `sportarten` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Bezeichnung` text NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;

--
-- TRUNCATE Tabelle vor dem Einfügen `sportarten`
--

TRUNCATE TABLE `sportarten`;
--
-- Daten für Tabelle `sportarten`
--

INSERT INTO `sportarten` (`ID`, `Bezeichnung`) VALUES
(1, 'Fussball'),
(2, 'Handball'),
(3, 'Tennis'),
(4, 'Tischtennis');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `tennisspieler`
--

DROP TABLE IF EXISTS `tennisspieler`;
CREATE TABLE IF NOT EXISTS `tennisspieler` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `gewonnenespiele` int(11) NOT NULL,
  `anzahlspiele` int(11) NOT NULL,
  `person` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `person` (`person`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- TRUNCATE Tabelle vor dem Einfügen `tennisspieler`
--

TRUNCATE TABLE `tennisspieler`;
-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `trainer`
--

DROP TABLE IF EXISTS `trainer`;
CREATE TABLE IF NOT EXISTS `trainer` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `anzahlvereine` int(11) NOT NULL,
  `person` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `person` (`person`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- TRUNCATE Tabelle vor dem Einfügen `trainer`
--

TRUNCATE TABLE `trainer`;
-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `turnier`
--

DROP TABLE IF EXISTS `turnier`;
CREATE TABLE IF NOT EXISTS `turnier` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Bezeichnung` text NOT NULL,
  `Sportart` int(11) NOT NULL,
  `Typ` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `Sportart` (`Sportart`),
  KEY `Typ` (`Typ`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;

--
-- TRUNCATE Tabelle vor dem Einfügen `turnier`
--

TRUNCATE TABLE `turnier`;
--
-- Daten für Tabelle `turnier`
--

INSERT INTO `turnier` (`ID`, `Bezeichnung`, `Sportart`, `Typ`) VALUES
(3, 'Fussball Bundesliga 2020/21', 1, 1);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `turnierteilnehmer`
--

DROP TABLE IF EXISTS `turnierteilnehmer`;
CREATE TABLE IF NOT EXISTS `turnierteilnehmer` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Mannschaft` int(11) DEFAULT NULL,
  `Gruppe` int(11) DEFAULT NULL,
  `Turnier` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `Gruppe` (`Gruppe`),
  KEY `Mannschaft` (`Mannschaft`),
  KEY `Turnier` (`Turnier`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4;

--
-- TRUNCATE Tabelle vor dem Einfügen `turnierteilnehmer`
--

TRUNCATE TABLE `turnierteilnehmer`;
--
-- Daten für Tabelle `turnierteilnehmer`
--

INSERT INTO `turnierteilnehmer` (`ID`, `Mannschaft`, `Gruppe`, `Turnier`) VALUES
(1, 2, NULL, 3),
(2, 3, NULL, 3),
(3, 4, NULL, 3),
(4, 5, NULL, 3),
(5, 6, NULL, 3),
(6, 7, NULL, 3),
(7, 8, NULL, 3),
(8, 9, NULL, 3),
(9, 10, NULL, 3),
(10, 11, NULL, 3),
(11, 12, NULL, 3),
(12, 13, NULL, 3),
(13, 14, NULL, 3),
(14, 15, NULL, 3),
(15, 16, NULL, 3),
(16, 17, NULL, 3),
(17, 18, NULL, 3),
(18, 19, NULL, 3);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `turniertyp`
--

DROP TABLE IF EXISTS `turniertyp`;
CREATE TABLE IF NOT EXISTS `turniertyp` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Bezeichnung` text COLLATE latin1_german1_ci NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;

--
-- TRUNCATE Tabelle vor dem Einfügen `turniertyp`
--

TRUNCATE TABLE `turniertyp`;
--
-- Daten für Tabelle `turniertyp`
--

INSERT INTO `turniertyp` (`ID`, `Bezeichnung`) VALUES
(1, 'Mannschaftsturnier'),
(2, 'Gruppenturnier');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `weitererspieler`
--

DROP TABLE IF EXISTS `weitererspieler`;
CREATE TABLE IF NOT EXISTS `weitererspieler` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `gewonnenespiele` int(11) NOT NULL,
  `anzahlspiele` int(11) NOT NULL,
  `person` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `person` (`person`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- TRUNCATE Tabelle vor dem Einfügen `weitererspieler`
--

TRUNCATE TABLE `weitererspieler`;
--
-- Constraints der exportierten Tabellen
--

--
-- Constraints der Tabelle `accounts`
--
ALTER TABLE `accounts`
  ADD CONSTRAINT `accounts_ibfk_1` FOREIGN KEY (`Person`) REFERENCES `personen` (`ID`);

--
-- Constraints der Tabelle `andereaufgaben`
--
ALTER TABLE `andereaufgaben`
  ADD CONSTRAINT `andereaufgaben_ibfk_1` FOREIGN KEY (`person`) REFERENCES `personen` (`ID`);

--
-- Constraints der Tabelle `fussballspieler`
--
ALTER TABLE `fussballspieler`
  ADD CONSTRAINT `fussballspieler_ibfk_1` FOREIGN KEY (`person`) REFERENCES `personen` (`ID`);

--
-- Constraints der Tabelle `gruppen`
--
ALTER TABLE `gruppen`
  ADD CONSTRAINT `gruppen_ibfk_1` FOREIGN KEY (`sportart`) REFERENCES `sportarten` (`ID`);

--
-- Constraints der Tabelle `gruppenmitglieder`
--
ALTER TABLE `gruppenmitglieder`
  ADD CONSTRAINT `gruppenmitglieder_ibfk_1` FOREIGN KEY (`Gruppe`) REFERENCES `gruppen` (`ID`),
  ADD CONSTRAINT `gruppenmitglieder_ibfk_2` FOREIGN KEY (`Person`) REFERENCES `personen` (`ID`);

--
-- Constraints der Tabelle `handballspieler`
--
ALTER TABLE `handballspieler`
  ADD CONSTRAINT `handballspieler_ibfk_1` FOREIGN KEY (`person`) REFERENCES `personen` (`ID`);

--
-- Constraints der Tabelle `mannschaften`
--
ALTER TABLE `mannschaften`
  ADD CONSTRAINT `mannschaften_ibfk_1` FOREIGN KEY (`sportart`) REFERENCES `sportarten` (`ID`);

--
-- Constraints der Tabelle `mannschaftsmitglieder`
--
ALTER TABLE `mannschaftsmitglieder`
  ADD CONSTRAINT `mannschaftsmitglieder_ibfk_1` FOREIGN KEY (`Mannschaft`) REFERENCES `mannschaften` (`ID`),
  ADD CONSTRAINT `mannschaftsmitglieder_ibfk_2` FOREIGN KEY (`Person`) REFERENCES `personen` (`ID`);

--
-- Constraints der Tabelle `personen`
--
ALTER TABLE `personen`
  ADD CONSTRAINT `personen_ibfk_1` FOREIGN KEY (`Sportart`) REFERENCES `sportarten` (`ID`),
  ADD CONSTRAINT `personen_ibfk_2` FOREIGN KEY (`typ`) REFERENCES `personentypen` (`ID`);

--
-- Constraints der Tabelle `physiotherapeut`
--
ALTER TABLE `physiotherapeut`
  ADD CONSTRAINT `physiotherapeut_ibfk_1` FOREIGN KEY (`person`) REFERENCES `personen` (`ID`);

--
-- Constraints der Tabelle `spiele`
--
ALTER TABLE `spiele`
  ADD CONSTRAINT `spiele_ibfk_1` FOREIGN KEY (`Turnier`) REFERENCES `turnier` (`ID`);

--
-- Constraints der Tabelle `tennisspieler`
--
ALTER TABLE `tennisspieler`
  ADD CONSTRAINT `tennisspieler_ibfk_1` FOREIGN KEY (`person`) REFERENCES `personen` (`ID`);

--
-- Constraints der Tabelle `trainer`
--
ALTER TABLE `trainer`
  ADD CONSTRAINT `trainer_ibfk_1` FOREIGN KEY (`person`) REFERENCES `personen` (`ID`);

--
-- Constraints der Tabelle `turnier`
--
ALTER TABLE `turnier`
  ADD CONSTRAINT `turnier_ibfk_1` FOREIGN KEY (`Sportart`) REFERENCES `sportarten` (`ID`),
  ADD CONSTRAINT `turnier_ibfk_2` FOREIGN KEY (`Typ`) REFERENCES `turniertyp` (`ID`);

--
-- Constraints der Tabelle `turnierteilnehmer`
--
ALTER TABLE `turnierteilnehmer`
  ADD CONSTRAINT `turnierteilnehmer_ibfk_1` FOREIGN KEY (`Gruppe`) REFERENCES `gruppen` (`ID`),
  ADD CONSTRAINT `turnierteilnehmer_ibfk_2` FOREIGN KEY (`Mannschaft`) REFERENCES `mannschaften` (`ID`),
  ADD CONSTRAINT `turnierteilnehmer_ibfk_3` FOREIGN KEY (`Turnier`) REFERENCES `turnier` (`ID`);

--
-- Constraints der Tabelle `weitererspieler`
--
ALTER TABLE `weitererspieler`
  ADD CONSTRAINT `weitererspieler_ibfk_1` FOREIGN KEY (`person`) REFERENCES `personen` (`ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
