-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 20-01-2017 a las 15:25:05
-- Versión del servidor: 10.1.19-MariaDB
-- Versión de PHP: 5.6.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `testcsharp`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `sms`
--

CREATE TABLE `sms` (
  `phone_id` int(10) NOT NULL,
  `date_in` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `date_out` datetime DEFAULT NULL,
  `number` varchar(15) NOT NULL,
  `message` varchar(1000) NOT NULL,
  `state` varchar(10) NOT NULL DEFAULT 'FALSE'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `sms`
--

INSERT INTO `sms` (`phone_id`, `date_in`, `date_out`, `number`, `message`, `state`) VALUES
(505, '2017-01-19 20:37:35', NULL, '2215732981', 'Mensaje 1/15 - Federico 1/3', 'FALSE'),
(508, '2017-01-19 20:40:36', NULL, '2215732981', 'Mensaje 2/15 - Federico 2/3', 'FALSE'),
(509, '2017-01-19 20:40:36', NULL, '2215732981', 'Mensaje 3/15 - Federico 3/3', 'FALSE'),
(510, '2017-01-19 20:40:36', NULL, '2214558121', 'Mensaje 4/15 - Tatiana 1/3', 'FALSE'),
(511, '2017-01-19 20:40:36', NULL, '2214558121', 'Mensaje 5/15 - Tatiana 2/3', 'FALSE'),
(512, '2017-01-19 20:40:36', NULL, '2214558121', 'Mensaje 6/15 - Tatiana 3/3', 'FALSE'),
(513, '2017-01-19 20:44:54', NULL, '2215089712', 'Mensaje 7/15 - Juan 1/3', 'FALSE'),
(514, '2017-01-19 20:44:54', NULL, '2215089712', 'Mensaje 8/15 - Juan 2/3', 'FALSE'),
(517, '2017-01-19 20:45:09', NULL, '2215089712', 'Mensaje 9/15 - Juan 3/3', 'FALSE'),
(518, '2017-01-20 14:22:03', NULL, '2215928660 ', 'Mensaje 10/15 - Felicitas60 1/3\r\n', 'FALSE'),
(519, '2017-01-20 14:22:03', NULL, '2215928660 ', 'Mensaje 11/15 - Felicitas60 2/3\r\n', 'FALSE'),
(520, '2017-01-20 14:22:03', NULL, '2215928660 ', 'Mensaje 12/15 - Felicitas60 3/3\r\n', 'FALSE'),
(521, '2017-01-20 14:23:48', NULL, '2216694731', 'Mensaje 13/15 - Felicitas31 1/3\r\n', 'FALSE'),
(522, '2017-01-20 14:23:48', NULL, '2216694731', 'Mensaje 14/15 - Felicitas31 2/3', 'FALSE'),
(523, '2017-01-20 14:23:48', NULL, '2216694731', 'Mensaje 15/15 - Felicitas31 3/3', 'FALSE');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `sms`
--
ALTER TABLE `sms`
  ADD PRIMARY KEY (`phone_id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `sms`
--
ALTER TABLE `sms`
  MODIFY `phone_id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=526;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
