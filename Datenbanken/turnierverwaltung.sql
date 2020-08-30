-- phpMyAdmin SQL Dump
-- version 4.9.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 30. Aug 2020 um 12:16
-- Server-Version: 10.4.11-MariaDB
-- PHP-Version: 7.3.12

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

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `accounts`
--

CREATE TABLE `accounts` (
  `ID` int(11) NOT NULL,
  `Person` int(11) NOT NULL,
  `Benutzername` text COLLATE latin1_german1_ci NOT NULL,
  `Passwort` text COLLATE latin1_german1_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_german1_ci;

--
-- Daten für Tabelle `accounts`
--

INSERT INTO `accounts` (`ID`, `Person`, `Benutzername`, `Passwort`) VALUES
(3, 23, 'user', 'user'),
(4, 24, 'admin', 'admin');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `andereaufgaben`
--

CREATE TABLE `andereaufgaben` (
  `ID` int(11) NOT NULL,
  `Einsatz` text NOT NULL,
  `person` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `fussballspieler`
--

CREATE TABLE `fussballspieler` (
  `ID` int(11) NOT NULL,
  `geschossenetore` int(11) NOT NULL,
  `anzahlspiele` int(11) NOT NULL,
  `position` text NOT NULL,
  `person` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gruppen`
--

CREATE TABLE `gruppen` (
  `ID` int(11) NOT NULL,
  `Name` text NOT NULL,
  `sportart` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `gruppenmitglieder`
--

CREATE TABLE `gruppenmitglieder` (
  `ID` int(11) NOT NULL,
  `Person` int(11) NOT NULL,
  `Gruppe` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `handballspieler`
--

CREATE TABLE `handballspieler` (
  `ID` int(11) NOT NULL,
  `geworfenetore` int(11) NOT NULL,
  `anzahlspiele` int(11) NOT NULL,
  `einsatzbereich` text NOT NULL,
  `person` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `mannschaften`
--

CREATE TABLE `mannschaften` (
  `ID` int(11) NOT NULL,
  `Name` text NOT NULL,
  `sportart` int(11) NOT NULL,
  `punkte` int(11) NOT NULL,
  `toreplus` int(11) NOT NULL,
  `toreminus` int(11) NOT NULL,
  `anzahlspiele` int(11) NOT NULL,
  `gewonnenespiele` int(11) NOT NULL,
  `verlorenespiele` int(11) NOT NULL,
  `unentschieden` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `mannschaften`
--

INSERT INTO `mannschaften` (`ID`, `Name`, `sportart`, `punkte`, `toreplus`, `toreminus`, `anzahlspiele`, `gewonnenespiele`, `verlorenespiele`, `unentschieden`) VALUES
(1, 'Bayern München', 1, 0, 0, 0, 0, 0, 0, 0),
(8, 'Borussia Dortmund', 1, 0, 0, 0, 0, 0, 0, 0);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `mannschaftsmitglieder`
--

CREATE TABLE `mannschaftsmitglieder` (
  `ID` int(11) NOT NULL,
  `Person` int(11) NOT NULL,
  `Mannschaft` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `personen`
--

CREATE TABLE `personen` (
  `ID` int(11) NOT NULL,
  `Name` text NOT NULL,
  `Vorname` text NOT NULL,
  `Geburtsdatum` text NOT NULL,
  `Sportart` int(11) NOT NULL,
  `Details` int(11) DEFAULT NULL,
  `typ` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `personen`
--

INSERT INTO `personen` (`ID`, `Name`, `Vorname`, `Geburtsdatum`, `Sportart`, `Details`, `typ`) VALUES
(23, 'user', 'user', '1.1.2000', 1, 0, 8),
(24, 'admin', 'admin', '1.1.2000', 1, 0, 8);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `personentypen`
--

CREATE TABLE `personentypen` (
  `ID` int(11) NOT NULL,
  `bezeichnung` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

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

CREATE TABLE `physiotherapeut` (
  `ID` int(11) NOT NULL,
  `anzahljahre` int(11) NOT NULL,
  `person` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `ranking`
--

CREATE TABLE `ranking` (
  `ID` int(11) NOT NULL,
  `Turnier` int(11) NOT NULL,
  `AnzahlTeilnehmer` int(11) NOT NULL,
  `MaxSpiele` int(11) NOT NULL,
  `AnzahlSpiele` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `spiele`
--

CREATE TABLE `spiele` (
  `ID` int(11) NOT NULL,
  `Turnier` int(11) NOT NULL,
  `Spieltag` int(11) NOT NULL,
  `Teilnehmer1` int(11) NOT NULL,
  `Teilnehmer2` int(11) NOT NULL,
  `Ergebnis1` int(11) NOT NULL,
  `Ergebnis2` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `sportarten`
--

CREATE TABLE `sportarten` (
  `ID` int(11) NOT NULL,
  `Bezeichnung` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

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

CREATE TABLE `tennisspieler` (
  `ID` int(11) NOT NULL,
  `gewonnenespiele` int(11) NOT NULL,
  `anzahlspiele` int(11) NOT NULL,
  `person` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `trainer`
--

CREATE TABLE `trainer` (
  `ID` int(11) NOT NULL,
  `anzahlvereine` int(11) NOT NULL,
  `person` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `turnier`
--

CREATE TABLE `turnier` (
  `ID` int(11) NOT NULL,
  `Bezeichnung` text NOT NULL,
  `Sportart` int(11) NOT NULL,
  `Typ` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `turnier`
--

INSERT INTO `turnier` (`ID`, `Bezeichnung`, `Sportart`, `Typ`) VALUES
(6, 'test1', 1, 0);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `turnierteilnehmer`
--

CREATE TABLE `turnierteilnehmer` (
  `ID` int(11) NOT NULL,
  `Mannschaft` int(11) DEFAULT NULL,
  `Gruppe` int(11) DEFAULT NULL,
  `Turnier` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Daten für Tabelle `turnierteilnehmer`
--

INSERT INTO `turnierteilnehmer` (`ID`, `Mannschaft`, `Gruppe`, `Turnier`) VALUES
(13, 1, NULL, 6),
(14, 8, NULL, 6);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `weitererspieler`
--

CREATE TABLE `weitererspieler` (
  `ID` int(11) NOT NULL,
  `gewonnenespiele` int(11) NOT NULL,
  `anzahlspiele` int(11) NOT NULL,
  `person` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `accounts`
--
ALTER TABLE `accounts`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `Person` (`Person`);

--
-- Indizes für die Tabelle `andereaufgaben`
--
ALTER TABLE `andereaufgaben`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `person` (`person`);

--
-- Indizes für die Tabelle `fussballspieler`
--
ALTER TABLE `fussballspieler`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `person` (`person`);

--
-- Indizes für die Tabelle `gruppen`
--
ALTER TABLE `gruppen`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `sportart` (`sportart`);

--
-- Indizes für die Tabelle `gruppenmitglieder`
--
ALTER TABLE `gruppenmitglieder`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `Person` (`Person`),
  ADD KEY `Gruppe` (`Gruppe`);

--
-- Indizes für die Tabelle `handballspieler`
--
ALTER TABLE `handballspieler`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `person` (`person`);

--
-- Indizes für die Tabelle `mannschaften`
--
ALTER TABLE `mannschaften`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `sportart` (`sportart`);

--
-- Indizes für die Tabelle `mannschaftsmitglieder`
--
ALTER TABLE `mannschaftsmitglieder`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `Mannschaft` (`Mannschaft`),
  ADD KEY `Person` (`Person`);

--
-- Indizes für die Tabelle `personen`
--
ALTER TABLE `personen`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `Sportart` (`Sportart`),
  ADD KEY `typ` (`typ`);

--
-- Indizes für die Tabelle `personentypen`
--
ALTER TABLE `personentypen`
  ADD PRIMARY KEY (`ID`);

--
-- Indizes für die Tabelle `physiotherapeut`
--
ALTER TABLE `physiotherapeut`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `person` (`person`);

--
-- Indizes für die Tabelle `ranking`
--
ALTER TABLE `ranking`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `Turnier` (`Turnier`);

--
-- Indizes für die Tabelle `spiele`
--
ALTER TABLE `spiele`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `Turnier` (`Turnier`);

--
-- Indizes für die Tabelle `sportarten`
--
ALTER TABLE `sportarten`
  ADD PRIMARY KEY (`ID`);

--
-- Indizes für die Tabelle `tennisspieler`
--
ALTER TABLE `tennisspieler`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `person` (`person`);

--
-- Indizes für die Tabelle `trainer`
--
ALTER TABLE `trainer`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `person` (`person`);

--
-- Indizes für die Tabelle `turnier`
--
ALTER TABLE `turnier`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `Sportart` (`Sportart`);

--
-- Indizes für die Tabelle `turnierteilnehmer`
--
ALTER TABLE `turnierteilnehmer`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `Turnier` (`Turnier`),
  ADD KEY `Mannschaft` (`Mannschaft`),
  ADD KEY `Gruppe` (`Gruppe`);

--
-- Indizes für die Tabelle `weitererspieler`
--
ALTER TABLE `weitererspieler`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `person` (`person`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `accounts`
--
ALTER TABLE `accounts`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT für Tabelle `andereaufgaben`
--
ALTER TABLE `andereaufgaben`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `fussballspieler`
--
ALTER TABLE `fussballspieler`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT für Tabelle `gruppen`
--
ALTER TABLE `gruppen`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT für Tabelle `gruppenmitglieder`
--
ALTER TABLE `gruppenmitglieder`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- AUTO_INCREMENT für Tabelle `handballspieler`
--
ALTER TABLE `handballspieler`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `mannschaften`
--
ALTER TABLE `mannschaften`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT für Tabelle `mannschaftsmitglieder`
--
ALTER TABLE `mannschaftsmitglieder`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT für Tabelle `personen`
--
ALTER TABLE `personen`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT für Tabelle `personentypen`
--
ALTER TABLE `personentypen`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT für Tabelle `physiotherapeut`
--
ALTER TABLE `physiotherapeut`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `ranking`
--
ALTER TABLE `ranking`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `spiele`
--
ALTER TABLE `spiele`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `sportarten`
--
ALTER TABLE `sportarten`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT für Tabelle `tennisspieler`
--
ALTER TABLE `tennisspieler`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `trainer`
--
ALTER TABLE `trainer`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `turnier`
--
ALTER TABLE `turnier`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT für Tabelle `turnierteilnehmer`
--
ALTER TABLE `turnierteilnehmer`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT für Tabelle `weitererspieler`
--
ALTER TABLE `weitererspieler`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

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
  ADD CONSTRAINT `gruppenmitglieder_ibfk_1` FOREIGN KEY (`Person`) REFERENCES `personen` (`ID`),
  ADD CONSTRAINT `gruppenmitglieder_ibfk_2` FOREIGN KEY (`Gruppe`) REFERENCES `gruppen` (`ID`);

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
-- Constraints der Tabelle `ranking`
--
ALTER TABLE `ranking`
  ADD CONSTRAINT `ranking_ibfk_1` FOREIGN KEY (`Turnier`) REFERENCES `turnier` (`ID`);

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
  ADD CONSTRAINT `turnier_ibfk_1` FOREIGN KEY (`Sportart`) REFERENCES `sportarten` (`ID`);

--
-- Constraints der Tabelle `turnierteilnehmer`
--
ALTER TABLE `turnierteilnehmer`
  ADD CONSTRAINT `turnierteilnehmer_ibfk_1` FOREIGN KEY (`Turnier`) REFERENCES `turnier` (`ID`),
  ADD CONSTRAINT `turnierteilnehmer_ibfk_2` FOREIGN KEY (`Mannschaft`) REFERENCES `mannschaften` (`ID`),
  ADD CONSTRAINT `turnierteilnehmer_ibfk_3` FOREIGN KEY (`Gruppe`) REFERENCES `gruppen` (`ID`);

--
-- Constraints der Tabelle `weitererspieler`
--
ALTER TABLE `weitererspieler`
  ADD CONSTRAINT `weitererspieler_ibfk_1` FOREIGN KEY (`person`) REFERENCES `personen` (`ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
