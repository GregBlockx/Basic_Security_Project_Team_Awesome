-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Gegenereerd op: 23 mrt 2017 om 11:58
-- Serverversie: 10.1.21-MariaDB
-- PHP-versie: 5.6.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `basic security`
--

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `allconversations`
--

CREATE TABLE `allconversations` (
  `ID` int(10) NOT NULL,
  `LoginUserID1` int(10) NOT NULL,
  `LoginUserID2` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `conversation`
--

CREATE TABLE `conversation` (
  `Conversation_ID` int(10) NOT NULL,
  `Conversation_All` int(10) NOT NULL,
  `Conversation_Message_ID` int(10) NOT NULL,
  `Conversation_Date` date NOT NULL,
  `Conversation_IsRead` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `keys`
--

CREATE TABLE `keys` (
  `Keys_ID` int(10) NOT NULL,
  `Keys_Login_ID` int(10) NOT NULL,
  `Keys_Public` int(100) NOT NULL,
  `Keys_Private` int(100) NOT NULL,
  `Keys_AES` int(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `login`
--

CREATE TABLE `login` (
  `Login_ID` int(10) NOT NULL,
  `Login_Username` varchar(25) NOT NULL,
  `Login_Password` varchar(25) NOT NULL,
  `Login_Salt` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `message`
--

CREATE TABLE `message` (
  `Message_ID` int(10) NOT NULL,
  `Message_Content` varchar(2500) NOT NULL,
  `Message_Conv_ID` int(10) NOT NULL,
  `Message_Date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `allconversations`
--
ALTER TABLE `allconversations`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `FK_Login_1` (`LoginUserID1`),
  ADD KEY `FK_Login_2` (`LoginUserID2`);

--
-- Indexen voor tabel `conversation`
--
ALTER TABLE `conversation`
  ADD PRIMARY KEY (`Conversation_ID`),
  ADD KEY `FK_Conversation_Message` (`Conversation_Message_ID`),
  ADD KEY `FK_Conversations` (`Conversation_All`);

--
-- Indexen voor tabel `keys`
--
ALTER TABLE `keys`
  ADD PRIMARY KEY (`Keys_ID`),
  ADD KEY `FK_Login` (`Keys_Login_ID`);

--
-- Indexen voor tabel `login`
--
ALTER TABLE `login`
  ADD PRIMARY KEY (`Login_ID`);

--
-- Indexen voor tabel `message`
--
ALTER TABLE `message`
  ADD PRIMARY KEY (`Message_ID`);

--
-- AUTO_INCREMENT voor geëxporteerde tabellen
--

--
-- AUTO_INCREMENT voor een tabel `allconversations`
--
ALTER TABLE `allconversations`
  MODIFY `ID` int(10) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT voor een tabel `conversation`
--
ALTER TABLE `conversation`
  MODIFY `Conversation_ID` int(10) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT voor een tabel `keys`
--
ALTER TABLE `keys`
  MODIFY `Keys_ID` int(10) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT voor een tabel `login`
--
ALTER TABLE `login`
  MODIFY `Login_ID` int(10) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT voor een tabel `message`
--
ALTER TABLE `message`
  MODIFY `Message_ID` int(10) NOT NULL AUTO_INCREMENT;
--
-- Beperkingen voor geëxporteerde tabellen
--

--
-- Beperkingen voor tabel `allconversations`
--
ALTER TABLE `allconversations`
  ADD CONSTRAINT `FK_Login_1` FOREIGN KEY (`LoginUserID1`) REFERENCES `login` (`Login_ID`),
  ADD CONSTRAINT `FK_Login_2` FOREIGN KEY (`LoginUserID2`) REFERENCES `login` (`Login_ID`);

--
-- Beperkingen voor tabel `conversation`
--
ALTER TABLE `conversation`
  ADD CONSTRAINT `FK_Conversation_Message` FOREIGN KEY (`Conversation_Message_ID`) REFERENCES `message` (`Message_ID`),
  ADD CONSTRAINT `FK_Conversations` FOREIGN KEY (`Conversation_All`) REFERENCES `allconversations` (`ID`);

--
-- Beperkingen voor tabel `keys`
--
ALTER TABLE `keys`
  ADD CONSTRAINT `FK_Login` FOREIGN KEY (`Keys_Login_ID`) REFERENCES `login` (`Login_ID`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
