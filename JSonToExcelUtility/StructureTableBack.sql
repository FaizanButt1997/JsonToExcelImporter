-- --------------------------------------------------------
-- Host:                         dev-database.intelligize.net
-- Server version:               8.0.35 - Source distribution
-- Server OS:                    Linux
-- HeidiSQL Version:             11.1.0.6116
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

-- Dumping structure for table dev_db_lsa.tbl_documents_v1
CREATE TABLE IF NOT EXISTS `tbl_documents_v1` (
  `pk_id` int NOT NULL AUTO_INCREMENT,
  `doc_type` varchar(200) DEFAULT NULL,
  `doc_type_id` int DEFAULT NULL,
  `doc_title` varchar(250) DEFAULT NULL,
  `doc_id` int DEFAULT NULL,
  `doc_name` varchar(300) DEFAULT NULL,
  `version_id` int DEFAULT NULL,
  PRIMARY KEY (`pk_id`) USING BTREE,
  KEY `Index 1` (`pk_id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

-- Dumping data for table dev_db_lsa.tbl_documents_v1: ~59 rows (approximately)
/*!40000 ALTER TABLE `tbl_documents_v1` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_documents_v1` ENABLE KEYS */;

-- Dumping structure for table dev_db_lsa.tbl_exhibit_section_v1
CREATE TABLE IF NOT EXISTS `tbl_exhibit_section_v1` (
  `document_id` int NOT NULL,
  `clause_index` smallint NOT NULL,
  `parent_index` smallint NOT NULL,
  `heading_name` varchar(200) DEFAULT NULL,
  `char_start_index` int DEFAULT NULL,
  `char_end_index` int DEFAULT NULL,
  `is_definition` tinyint DEFAULT NULL,
  `clause_level` tinyint DEFAULT NULL,
  `count` int DEFAULT NULL,
  `version_id` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

-- Dumping data for table dev_db_lsa.tbl_exhibit_section_v1: ~0 rows (approximately)
/*!40000 ALTER TABLE `tbl_exhibit_section_v1` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_exhibit_section_v1` ENABLE KEYS */;

-- Dumping structure for table dev_db_lsa.tbl_exhibit_to_agreement_specific_value_v1
CREATE TABLE IF NOT EXISTS `tbl_exhibit_to_agreement_specific_value_v1` (
  `value_id` int NOT NULL AUTO_INCREMENT,
  `document_id` int NOT NULL,
  `value_type_id` smallint DEFAULT NULL,
  `type` varchar(300) DEFAULT NULL,
  `agreement_id` smallint DEFAULT NULL,
  `start_index` int DEFAULT NULL,
  `end_index` int DEFAULT NULL,
  `value` varchar(300) DEFAULT NULL,
  `rule_number` smallint DEFAULT NULL,
  `normalize_value` varchar(150) DEFAULT NULL,
  `version_id` int DEFAULT NULL,
  PRIMARY KEY (`value_id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

-- Dumping data for table dev_db_lsa.tbl_exhibit_to_agreement_specific_value_v1: ~1,059 rows (approximately)
/*!40000 ALTER TABLE `tbl_exhibit_to_agreement_specific_value_v1` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_exhibit_to_agreement_specific_value_v1` ENABLE KEYS */;

-- Dumping structure for table dev_db_lsa.tbl_exhibit_to_governing_law_v1
CREATE TABLE IF NOT EXISTS `tbl_exhibit_to_governing_law_v1` (
  `document_id` int NOT NULL,
  `law_id` smallint NOT NULL,
  `law_start_index` int DEFAULT NULL,
  `law_end_index` int DEFAULT NULL,
  `version_id` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

-- Dumping data for table dev_db_lsa.tbl_exhibit_to_governing_law_v1: ~71 rows (approximately)
/*!40000 ALTER TABLE `tbl_exhibit_to_governing_law_v1` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_exhibit_to_governing_law_v1` ENABLE KEYS */;

-- Dumping structure for table dev_db_lsa.tbl_exhibit_to_location_jurisdiction_v1
CREATE TABLE IF NOT EXISTS `tbl_exhibit_to_location_jurisdiction_v1` (
  `document_id` int NOT NULL,
  `jurisdiction_id` smallint NOT NULL,
  `jurisdiction_start_index` int DEFAULT NULL,
  `jurisdiction_end_index` int DEFAULT NULL,
  `version_id` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

-- Dumping data for table dev_db_lsa.tbl_exhibit_to_location_jurisdiction_v1: ~157 rows (approximately)
/*!40000 ALTER TABLE `tbl_exhibit_to_location_jurisdiction_v1` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_exhibit_to_location_jurisdiction_v1` ENABLE KEYS */;

-- Dumping structure for table dev_db_lsa.tbl_exhibit_to_party_involved_v1
CREATE TABLE IF NOT EXISTS `tbl_exhibit_to_party_involved_v1` (
  `document_id` int NOT NULL,
  `party_id` int DEFAULT NULL,
  `party_startIndex` mediumint DEFAULT NULL,
  `party_endIndex` mediumint DEFAULT NULL,
  `ref_party_name` varchar(150) DEFAULT NULL,
  `version_id` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 ROW_FORMAT=DYNAMIC;

-- Dumping data for table dev_db_lsa.tbl_exhibit_to_party_involved_v1: ~233 rows (approximately)
/*!40000 ALTER TABLE `tbl_exhibit_to_party_involved_v1` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_exhibit_to_party_involved_v1` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
