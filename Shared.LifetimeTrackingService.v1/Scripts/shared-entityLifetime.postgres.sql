-- ----------------------------
-- Table structure for entityLifetime
-- ----------------------------
DROP TABLE IF EXISTS "public"."entityLifetime";
CREATE TABLE "public"."entityLifetime" (
    "id" uuid NOT NULL,
    "app" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
    "version" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
    "entity_id" uuid NOT NULL,
    "content" text COLLATE "pg_catalog"."default" NOT NULL,
    "changed_type" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
    "changed_by" varchar(255) COLLATE "pg_catalog"."default",
    "changed_at" timestamptz(6) NOT NULL
)
;

-- ----------------------------
-- Primary Key structure for table entityLifetime
-- ----------------------------
ALTER TABLE "public"."entityLifetime" ADD CONSTRAINT "entityLifetime_pkey" PRIMARY KEY ("id");
