/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 80028
 Source Host           : localhost:43306
 Source Schema         : itaxviet_company

 Target Server Type    : MySQL
 Target Server Version : 80028
 File Encoding         : 65001

 Date: 30/01/2022 23:51:18
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for company
-- ----------------------------
DROP TABLE IF EXISTS `company`;
CREATE TABLE `company`  (
  `id` binary(16) NOT NULL,
  `name` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `code` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `tax_identification_number` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `establishment_date_time` datetime NOT NULL,
  `created_at` datetime NOT NULL,
  `created_by` binary(16) NOT NULL,
  `last_modified_at` datetime NULL,
  `last_modified_by` binary(16) NULL,
  PRIMARY KEY (`id`) USING BTREE,
  UNIQUE INDEX `ix_code`(`code` ASC) USING BTREE,
  UNIQUE INDEX `ix_tax_identification_number`(`tax_identification_number` ASC) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;