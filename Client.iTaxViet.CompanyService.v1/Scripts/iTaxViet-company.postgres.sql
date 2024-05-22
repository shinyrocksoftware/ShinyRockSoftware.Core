/*
 Navicat Premium Data Transfer

 Source Server         : OCI Dev Citus
 Source Server Type    : PostgreSQL
 Source Server Version : 150003 (150003)
 Source Host           : 129.213.138.210:5432
 Source Catalog        : iTaxViet
 Source Schema         : public

 Target Server Type    : PostgreSQL
 Target Server Version : 150003 (150003)
 File Encoding         : 65001

 Date: 24/07/2023 16:47:35
*/
-- ----------------------------
-- Table structure for company
-- ----------------------------
DROP TABLE IF EXISTS "public"."company";
CREATE TABLE "public"."company" (
    "id" uuid NOT NULL,
    "name" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
    "code" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
    "tax_identification_number" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
    "establishment_at" timestamptz(6) NOT NULL,
    "is_active" bool NOT NULL,
    "created_at" timestamptz(6) NOT NULL,
    "created_by" uuid,
    "last_modified_at" timestamptz(6),
    "last_modified_by" uuid,
    "deleted_at" timestamptz(6),
    "deleted_by" uuid
)
;

-- ----------------------------
-- Uniques structure for table company
-- ----------------------------
ALTER TABLE "public"."company" ADD CONSTRAINT "ix_tax_identification_number" UNIQUE ("tax_identification_number");
ALTER TABLE "public"."company" ADD CONSTRAINT "ix_code" UNIQUE ("code");

-- ----------------------------
-- Primary Key structure for table company
-- ----------------------------
ALTER TABLE "public"."company" ADD CONSTRAINT "company_pkey" PRIMARY KEY ("id");
