using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TechZone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_roleid",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_userid",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_userid",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_roleid",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_userid",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_userid",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_AspNetUsers_applicationuserid",
                table: "AuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_AspNetUsers_applicationuserid",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_LaptopVariants_laptopvariantid",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_EmailQueues_AspNetUsers_userid",
                table: "EmailQueues");

            migrationBuilder.DropForeignKey(
                name: "FK_LaptopImages_Laptops_laptopid",
                table: "LaptopImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Laptops_Brands_brandid",
                table: "Laptops");

            migrationBuilder.DropForeignKey(
                name: "FK_Laptops_Categories_categoryid",
                table: "Laptops");

            migrationBuilder.DropForeignKey(
                name: "FK_LaptopVariants_Discounts_discountid",
                table: "LaptopVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_LaptopVariants_Laptops_laptopid",
                table: "LaptopVariants");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_LaptopVariants_laptopvariantid",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_orderid",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_userid",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Orders_orderid",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_userid",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Laptops_laptopid",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_refreshtoken_AspNetUsers_applicationuserid",
                table: "refreshtoken");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequests_AspNetUsers_applicationuserid",
                table: "RepairRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequests_Laptops_laptopid",
                table: "RepairRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequests_RepairServiceItems_itemid",
                table: "RepairRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_VerificationCodes_AspNetUsers_userid",
                table: "VerificationCodes");

            migrationBuilder.DropTable(
                name: "Shippings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VerificationCodes",
                table: "VerificationCodes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_VerificationCodes_Attempt_NonNegative",
                table: "VerificationCodes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_VerificationCodes_Expiry_After_Created",
                table: "VerificationCodes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_VerificationCodes_MaxAttempts_Positive",
                table: "VerificationCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairServiceItems",
                table: "RepairServiceItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairRequests",
                table: "RepairRequests");

            migrationBuilder.DropIndex(
                name: "IX_RepairRequests_applicationuserid",
                table: "RepairRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_refreshtoken",
                table: "refreshtoken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_userid",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LaptopVariants",
                table: "LaptopVariants");

            migrationBuilder.DropIndex(
                name: "IX_LaptopVariants_discountid",
                table: "LaptopVariants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Laptops",
                table: "Laptops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LaptopImages",
                table: "LaptopImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailQueues",
                table: "EmailQueues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_applicationuserid",
                table: "CartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brands",
                table: "Brands");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuditLogs",
                table: "AuditLogs");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_applicationuserid",
                table: "AuditLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "estimatedtime",
                table: "RepairServiceItems");

            migrationBuilder.DropColumn(
                name: "applicationuserid",
                table: "refreshtoken");

            migrationBuilder.DropColumn(
                name: "discountid",
                table: "LaptopVariants");

            migrationBuilder.DropColumn(
                name: "notes",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "ports",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "percentage",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "categoryimageurl",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "isactive",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "phone",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "role",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "VerificationCodes",
                newName: "verificationcodes");

            migrationBuilder.RenameTable(
                name: "RepairServiceItems",
                newName: "repairserviceitems");

            migrationBuilder.RenameTable(
                name: "RepairRequests",
                newName: "repairrequests");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "ratings");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "payments");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "orders");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "orderitems");

            migrationBuilder.RenameTable(
                name: "LaptopVariants",
                newName: "laptopvariants");

            migrationBuilder.RenameTable(
                name: "Laptops",
                newName: "laptops");

            migrationBuilder.RenameTable(
                name: "LaptopImages",
                newName: "laptopimages");

            migrationBuilder.RenameTable(
                name: "EmailQueues",
                newName: "emailqueues");

            migrationBuilder.RenameTable(
                name: "Discounts",
                newName: "discounts");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "categories");

            migrationBuilder.RenameTable(
                name: "CartItems",
                newName: "cartitems");

            migrationBuilder.RenameTable(
                name: "Brands",
                newName: "brands");

            migrationBuilder.RenameTable(
                name: "AuditLogs",
                newName: "auditlogs");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "aspnetusertokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "aspnetusers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "aspnetuserroles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "aspnetuserlogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "aspnetuserclaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "aspnetroles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "aspnetroleclaims");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "repairserviceitems",
                newName: "baseprice");

            migrationBuilder.RenameColumn(
                name: "itemid",
                table: "repairserviceitems",
                newName: "estimateddays");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "repairrequests",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "itemid",
                table: "repairrequests",
                newName: "serviceitemid");

            migrationBuilder.RenameColumn(
                name: "applicationuserid",
                table: "repairrequests",
                newName: "issuedescription");

            migrationBuilder.RenameColumn(
                name: "requestid",
                table: "repairrequests",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_RepairRequests_laptopid",
                table: "repairrequests",
                newName: "IX_repairrequests_laptopid");

            migrationBuilder.RenameIndex(
                name: "IX_RepairRequests_itemid",
                table: "repairrequests",
                newName: "IX_repairrequests_serviceitemid");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_laptopid",
                table: "ratings",
                newName: "IX_ratings_laptopid");

            migrationBuilder.RenameColumn(
                name: "paidat",
                table: "payments",
                newName: "updatedat");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_orderid",
                table: "payments",
                newName: "IX_payments_orderid");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_userid",
                table: "orders",
                newName: "IX_orders_userid");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_orderid",
                table: "orderitems",
                newName: "IX_orderitems_orderid");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_laptopvariantid",
                table: "orderitems",
                newName: "IX_orderitems_laptopvariantid");

            migrationBuilder.RenameColumn(
                name: "storage",
                table: "laptopvariants",
                newName: "storagecapacitygb");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "laptopvariants",
                newName: "currentprice");

            migrationBuilder.RenameIndex(
                name: "IX_LaptopVariants_laptopid",
                table: "laptopvariants",
                newName: "IX_laptopvariants_laptopid");

            migrationBuilder.RenameColumn(
                name: "warranty",
                table: "laptops",
                newName: "storelocation");

            migrationBuilder.RenameIndex(
                name: "IX_Laptops_categoryid",
                table: "laptops",
                newName: "IX_laptops_categoryid");

            migrationBuilder.RenameIndex(
                name: "IX_Laptops_brandid",
                table: "laptops",
                newName: "IX_laptops_brandid");

            migrationBuilder.RenameIndex(
                name: "IX_LaptopImages_laptopid",
                table: "laptopimages",
                newName: "IX_laptopimages_laptopid");

            migrationBuilder.RenameIndex(
                name: "IX_EmailQueues_userid",
                table: "emailqueues",
                newName: "IX_emailqueues_userid");

            migrationBuilder.RenameColumn(
                name: "applicationuserid",
                table: "cartitems",
                newName: "userid");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_laptopvariantid",
                table: "cartitems",
                newName: "IX_cartitems_laptopvariantid");

            migrationBuilder.RenameColumn(
                name: "entity",
                table: "auditlogs",
                newName: "entitytype");

            migrationBuilder.RenameColumn(
                name: "details",
                table: "auditlogs",
                newName: "oldvalues");

            migrationBuilder.RenameColumn(
                name: "applicationuserid",
                table: "auditlogs",
                newName: "newvalues");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_roleid",
                table: "aspnetuserroles",
                newName: "IX_aspnetuserroles_roleid");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_userid",
                table: "aspnetuserlogins",
                newName: "IX_aspnetuserlogins_userid");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_userid",
                table: "aspnetuserclaims",
                newName: "IX_aspnetuserclaims_userid");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_roleid",
                table: "aspnetroleclaims",
                newName: "IX_aspnetroleclaims_roleid");

            migrationBuilder.AlterColumn<string>(
                name: "userid",
                table: "verificationcodes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(450)",
                oldMaxLength: 450);

            migrationBuilder.AlterColumn<DateTime>(
                name: "expirydate",
                table: "verificationcodes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW() + INTERVAL '60 minutes'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now() + interval '60 minutes'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdat",
                table: "verificationcodes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<int>(
                name: "estimateddays",
                table: "repairserviceitems",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "repairserviceitems",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdat",
                table: "repairserviceitems",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedat",
                table: "repairserviceitems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "repairserviceitems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isactive",
                table: "repairserviceitems",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "repairserviceitems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedat",
                table: "repairserviceitems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "requestdate",
                table: "repairrequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AddColumn<DateTime>(
                name: "completeddate",
                table: "repairrequests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdat",
                table: "repairrequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedat",
                table: "repairrequests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deviceserial",
                table: "repairrequests",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "diagnosisnotes",
                table: "repairrequests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "repairrequests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "laptopvariantid",
                table: "repairrequests",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "priority",
                table: "repairrequests",
                type: "text",
                nullable: false,
                defaultValue: "Normal");

            migrationBuilder.AddColumn<decimal>(
                name: "quotedprice",
                table: "repairrequests",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "requestnumber",
                table: "repairrequests",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedat",
                table: "repairrequests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userid",
                table: "refreshtoken",
                type: "character varying(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "laptopid",
                table: "ratings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdat",
                table: "ratings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AddColumn<int>(
                name: "accessoryid",
                table: "ratings",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedat",
                table: "ratings",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "ratings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isverifiedpurchase",
                table: "ratings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "productid",
                table: "ratings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "producttype",
                table: "ratings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedat",
                table: "ratings",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "paymentstatus",
                table: "payments",
                type: "text",
                nullable: false,
                defaultValue: "PENDING",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "Pending");

            migrationBuilder.AlterColumn<string>(
                name: "paymentmethod",
                table: "payments",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<decimal>(
                name: "amountcents",
                table: "payments",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "billingdata",
                table: "payments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "capturedamountcents",
                table: "payments",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdat",
                table: "payments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AddColumn<string>(
                name: "currency",
                table: "payments",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "EGP");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedat",
                table: "payments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "errorcode",
                table: "payments",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "errormessage",
                table: "payments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "integrationid",
                table: "payments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is3ds",
                table: "payments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "iscaptured",
                table: "payments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "payments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isrefunded",
                table: "payments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "paymobcreatedat",
                table: "payments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "paymoborderid",
                table: "payments",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "refundedamountcents",
                table: "payments",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "transactionfingerprint",
                table: "payments",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "Pending",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "orderdate",
                table: "orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AddColumn<string>(
                name: "cancelreason",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "createdat",
                table: "orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedat",
                table: "orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "deliveryaddress",
                table: "orders",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "deliverycost",
                table: "orders",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "deliveryinstructions",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "discountamount",
                table: "orders",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isreservationcompleted",
                table: "orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ordernumber",
                table: "orders",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ordertype",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "Reservation");

            migrationBuilder.AddColumn<decimal>(
                name: "reservationamount",
                table: "orders",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "reservationexpirydate",
                table: "orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "shippingcost",
                table: "orders",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "subtotalamount",
                table: "orders",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "taxamount",
                table: "orders",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedat",
                table: "orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "laptopvariantid",
                table: "orderitems",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "accessoryid",
                table: "orderitems",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdat",
                table: "orderitems",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedat",
                table: "orderitems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "discountamount",
                table: "orderitems",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "orderitems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "productid",
                table: "orderitems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "productname",
                table: "orderitems",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "producttype",
                table: "orderitems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "sku",
                table: "orderitems",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "totalprice",
                table: "orderitems",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedat",
                table: "orderitems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "stockquantity",
                table: "laptopvariants",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "createdat",
                table: "laptopvariants",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedat",
                table: "laptopvariants",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isactive",
                table: "laptopvariants",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "laptopvariants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "reorderlevel",
                table: "laptopvariants",
                type: "integer",
                nullable: false,
                defaultValue: 5);

            migrationBuilder.AddColumn<int>(
                name: "reservedquantity",
                table: "laptopvariants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "sku",
                table: "laptopvariants",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "storagetype",
                table: "laptopvariants",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedat",
                table: "laptopvariants",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdat",
                table: "laptops",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedat",
                table: "laptops",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isactive",
                table: "laptops",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "laptops",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "releaseyear",
                table: "laptops",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "storecontact",
                table: "laptops",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedat",
                table: "laptops",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "uploadedat",
                table: "laptopimages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AddColumn<DateTime>(
                name: "createdat",
                table: "laptopimages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedat",
                table: "laptopimages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "displayorder",
                table: "laptopimages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "laptopimages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedat",
                table: "laptopimages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "discounts",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "createdat",
                table: "discounts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedat",
                table: "discounts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "discounttype",
                table: "discounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "discounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "maxdiscountamount",
                table: "discounts",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "minimumpurchase",
                table: "discounts",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedat",
                table: "discounts",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "usagecount",
                table: "discounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "usagelimit",
                table: "discounts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "value",
                table: "discounts",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdat",
                table: "categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedat",
                table: "categories",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "displayorder",
                table: "categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "imageurl",
                table: "categories",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isactive",
                table: "categories",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "parentcategoryid",
                table: "categories",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedat",
                table: "categories",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "laptopvariantid",
                table: "cartitems",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "addedat",
                table: "cartitems",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AddColumn<int>(
                name: "accessoryid",
                table: "cartitems",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdat",
                table: "cartitems",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedat",
                table: "cartitems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "cartitems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "productid",
                table: "cartitems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "producttype",
                table: "cartitems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedat",
                table: "cartitems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "createdat",
                table: "brands",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedat",
                table: "brands",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isactive",
                table: "brands",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "brands",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedat",
                table: "brands",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "timestamp",
                table: "auditlogs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<string>(
                name: "entityid",
                table: "auditlogs",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "createdat",
                table: "auditlogs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedat",
                table: "auditlogs",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ipaddress",
                table: "auditlogs",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "auditlogs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedat",
                table: "auditlogs",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "useragent",
                table: "auditlogs",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "userid",
                table: "auditlogs",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdat",
                table: "aspnetusers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "TIMEZONE('utc', NOW())",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AddColumn<string>(
                name: "addressline",
                table: "aspnetusers",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "aspnetusers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "aspnetusers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "Egypt");

            migrationBuilder.AddColumn<bool>(
                name: "isdeleted",
                table: "aspnetusers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "profileimageurl",
                table: "aspnetusers",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "state",
                table: "aspnetusers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedat",
                table: "aspnetusers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_verificationcodes",
                table: "verificationcodes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_repairserviceitems",
                table: "repairserviceitems",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_repairrequests",
                table: "repairrequests",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_refreshtoken",
                table: "refreshtoken",
                columns: new[] { "userid", "id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ratings",
                table: "ratings",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_payments",
                table: "payments",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orders",
                table: "orders",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orderitems",
                table: "orderitems",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_laptopvariants",
                table: "laptopvariants",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_laptops",
                table: "laptops",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_laptopimages",
                table: "laptopimages",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_emailqueues",
                table: "emailqueues",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_discounts",
                table: "discounts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cartitems",
                table: "cartitems",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_brands",
                table: "brands",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_auditlogs",
                table: "auditlogs",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetusertokens",
                table: "aspnetusertokens",
                columns: new[] { "userid", "loginprovider", "name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetusers",
                table: "aspnetusers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetuserroles",
                table: "aspnetuserroles",
                columns: new[] { "userid", "roleid" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetuserlogins",
                table: "aspnetuserlogins",
                columns: new[] { "loginprovider", "providerkey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetuserclaims",
                table: "aspnetuserclaims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetroles",
                table: "aspnetroles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_aspnetroleclaims",
                table: "aspnetroleclaims",
                column: "id");

            migrationBuilder.CreateTable(
                name: "accessorytypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    categoryimageurl = table.Column<string>(type: "text", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('utc', NOW())"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accessorytypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "laptopports",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    laptopid = table.Column<int>(type: "integer", nullable: false),
                    porttype = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('utc', NOW())"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_laptopports", x => x.id);
                    table.ForeignKey(
                        name: "FK_laptopports_laptops_laptopid",
                        column: x => x.laptopid,
                        principalTable: "laptops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "laptopwarranties",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    laptopid = table.Column<int>(type: "integer", nullable: false),
                    durationmonths = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    coverage = table.Column<string>(type: "text", nullable: false),
                    provider = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('utc', NOW())"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_laptopwarranties", x => x.id);
                    table.ForeignKey(
                        name: "FK_laptopwarranties_laptops_laptopid",
                        column: x => x.laptopid,
                        principalTable: "laptops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productviews",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    producttype = table.Column<string>(type: "text", nullable: false),
                    productid = table.Column<int>(type: "integer", nullable: false),
                    userid = table.Column<string>(type: "text", nullable: true),
                    ipaddress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    viewedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('utc', NOW())"),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('utc', NOW())"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productviews", x => x.id);
                    table.ForeignKey(
                        name: "FK_productviews_aspnetusers_userid",
                        column: x => x.userid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "shipments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    orderid = table.Column<int>(type: "integer", nullable: false),
                    carrier = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    trackingnumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Preparing"),
                    shippedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    estimateddelivery = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deliveredat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    failedreason = table.Column<string>(type: "text", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('utc', NOW())"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipments", x => x.id);
                    table.ForeignKey(
                        name: "FK_shipments_orders_orderid",
                        column: x => x.orderid,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stockalerts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<string>(type: "text", nullable: false),
                    producttype = table.Column<string>(type: "text", nullable: false),
                    productid = table.Column<int>(type: "integer", nullable: false),
                    isnotified = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    notifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('utc', NOW())"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stockalerts", x => x.id);
                    table.ForeignKey(
                        name: "FK_stockalerts_aspnetusers_userid",
                        column: x => x.userid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userdiscountusages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<string>(type: "text", nullable: false),
                    discountid = table.Column<int>(type: "integer", nullable: false),
                    orderid = table.Column<int>(type: "integer", nullable: false),
                    usedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('utc', NOW())"),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('utc', NOW())"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userdiscountusages", x => x.id);
                    table.ForeignKey(
                        name: "FK_userdiscountusages_aspnetusers_userid",
                        column: x => x.userid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_userdiscountusages_discounts_discountid",
                        column: x => x.discountid,
                        principalTable: "discounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_userdiscountusages_orders_orderid",
                        column: x => x.orderid,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "accessories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sku = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    accessorytypeid = table.Column<int>(type: "integer", nullable: false),
                    currentprice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    stockquantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    reservedquantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    reorderlevel = table.Column<int>(type: "integer", nullable: false, defaultValue: 5),
                    isactive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('utc', NOW())"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accessories", x => x.id);
                    table.ForeignKey(
                        name: "FK_accessories_accessorytypes_accessorytypeid",
                        column: x => x.accessorytypeid,
                        principalTable: "accessorytypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "accessoryattributes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    accessoryid = table.Column<int>(type: "integer", nullable: false),
                    attributekey = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    attributevalue = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('utc', NOW())"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accessoryattributes", x => x.id);
                    table.ForeignKey(
                        name: "FK_accessoryattributes_accessories_accessoryid",
                        column: x => x.accessoryid,
                        principalTable: "accessories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "accessorycompatibilities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    accessoryid = table.Column<int>(type: "integer", nullable: false),
                    laptopid = table.Column<int>(type: "integer", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('utc', NOW())"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accessorycompatibilities", x => x.id);
                    table.ForeignKey(
                        name: "FK_accessorycompatibilities_accessories_accessoryid",
                        column: x => x.accessoryid,
                        principalTable: "accessories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_accessorycompatibilities_laptops_laptopid",
                        column: x => x.laptopid,
                        principalTable: "laptops",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "accessoryimages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    accessoryid = table.Column<int>(type: "integer", nullable: false),
                    imageurl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ismain = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    displayorder = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    uploadedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('utc', NOW())"),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('utc', NOW())"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accessoryimages", x => x.id);
                    table.ForeignKey(
                        name: "FK_accessoryimages_accessories_accessoryid",
                        column: x => x.accessoryid,
                        principalTable: "accessories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pricehistories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    producttype = table.Column<string>(type: "text", nullable: false),
                    productid = table.Column<int>(type: "integer", nullable: false),
                    oldprice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    newprice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    changereason = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    effectivefrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('utc', NOW())"),
                    effectiveto = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    changedbyuserid = table.Column<string>(type: "text", nullable: false),
                    accessoryid = table.Column<int>(type: "integer", nullable: true),
                    laptopvariantid = table.Column<int>(type: "integer", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('utc', NOW())"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pricehistories", x => x.id);
                    table.ForeignKey(
                        name: "FK_pricehistories_accessories_accessoryid",
                        column: x => x.accessoryid,
                        principalTable: "accessories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_pricehistories_aspnetusers_changedbyuserid",
                        column: x => x.changedbyuserid,
                        principalTable: "aspnetusers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_pricehistories_laptopvariants_laptopvariantid",
                        column: x => x.laptopvariantid,
                        principalTable: "laptopvariants",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "productdiscounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    discountid = table.Column<int>(type: "integer", nullable: false),
                    producttype = table.Column<string>(type: "text", nullable: false),
                    productid = table.Column<int>(type: "integer", nullable: false),
                    accessoryid = table.Column<int>(type: "integer", nullable: true),
                    laptopvariantid = table.Column<int>(type: "integer", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "TIMEZONE('utc', NOW())"),
                    updatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deletedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productdiscounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_productdiscounts_accessories_accessoryid",
                        column: x => x.accessoryid,
                        principalTable: "accessories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_productdiscounts_discounts_discountid",
                        column: x => x.discountid,
                        principalTable: "discounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productdiscounts_laptopvariants_laptopvariantid",
                        column: x => x.laptopvariantid,
                        principalTable: "laptopvariants",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_repairserviceitems_isactive",
                table: "repairserviceitems",
                column: "isactive");

            migrationBuilder.CreateIndex(
                name: "IX_repairserviceitems_repairtype",
                table: "repairserviceitems",
                column: "repairtype");

            migrationBuilder.CreateIndex(
                name: "IX_repairrequests_laptopvariantid",
                table: "repairrequests",
                column: "laptopvariantid");

            migrationBuilder.CreateIndex(
                name: "IX_repairrequests_requestdate",
                table: "repairrequests",
                column: "requestdate");

            migrationBuilder.CreateIndex(
                name: "IX_repairrequests_requestnumber",
                table: "repairrequests",
                column: "requestnumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_repairrequests_status",
                table: "repairrequests",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_repairrequests_userid",
                table: "repairrequests",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_ratings_accessoryid",
                table: "ratings",
                column: "accessoryid");

            migrationBuilder.CreateIndex(
                name: "IX_ratings_producttype_productid",
                table: "ratings",
                columns: new[] { "producttype", "productid" });

            migrationBuilder.CreateIndex(
                name: "IX_ratings_stars",
                table: "ratings",
                column: "stars");

            migrationBuilder.CreateIndex(
                name: "IX_ratings_userid_producttype_productid",
                table: "ratings",
                columns: new[] { "userid", "producttype", "productid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payments_paymentstatus",
                table: "payments",
                column: "paymentstatus");

            migrationBuilder.CreateIndex(
                name: "IX_payments_paymoborderid",
                table: "payments",
                column: "paymoborderid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payments_transactionid",
                table: "payments",
                column: "transactionid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_orderdate",
                table: "orders",
                column: "orderdate");

            migrationBuilder.CreateIndex(
                name: "IX_orders_ordernumber",
                table: "orders",
                column: "ordernumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_ordertype",
                table: "orders",
                column: "ordertype");

            migrationBuilder.CreateIndex(
                name: "IX_orders_reservationexpirydate",
                table: "orders",
                column: "reservationexpirydate");

            migrationBuilder.CreateIndex(
                name: "IX_orders_status",
                table: "orders",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_orderitems_accessoryid",
                table: "orderitems",
                column: "accessoryid");

            migrationBuilder.CreateIndex(
                name: "IX_orderitems_producttype_productid",
                table: "orderitems",
                columns: new[] { "producttype", "productid" });

            migrationBuilder.CreateIndex(
                name: "IX_discounts_code",
                table: "discounts",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_discounts_isactive_startdate_enddate",
                table: "discounts",
                columns: new[] { "isactive", "startdate", "enddate" });

            migrationBuilder.CreateIndex(
                name: "IX_categories_parentcategoryid",
                table: "categories",
                column: "parentcategoryid");

            migrationBuilder.CreateIndex(
                name: "IX_cartitems_accessoryid",
                table: "cartitems",
                column: "accessoryid");

            migrationBuilder.CreateIndex(
                name: "IX_cartitems_userid_producttype_productid",
                table: "cartitems",
                columns: new[] { "userid", "producttype", "productid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_auditlogs_entitytype_entityid",
                table: "auditlogs",
                columns: new[] { "entitytype", "entityid" });

            migrationBuilder.CreateIndex(
                name: "IX_auditlogs_timestamp",
                table: "auditlogs",
                column: "timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_auditlogs_userid",
                table: "auditlogs",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_accessories_accessorytypeid",
                table: "accessories",
                column: "accessorytypeid");

            migrationBuilder.CreateIndex(
                name: "IX_accessories_sku",
                table: "accessories",
                column: "sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_accessoryattributes_accessoryid_attributekey",
                table: "accessoryattributes",
                columns: new[] { "accessoryid", "attributekey" });

            migrationBuilder.CreateIndex(
                name: "IX_accessorycompatibilities_accessoryid_laptopid",
                table: "accessorycompatibilities",
                columns: new[] { "accessoryid", "laptopid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_accessorycompatibilities_laptopid",
                table: "accessorycompatibilities",
                column: "laptopid");

            migrationBuilder.CreateIndex(
                name: "IX_accessoryimages_accessoryid",
                table: "accessoryimages",
                column: "accessoryid");

            migrationBuilder.CreateIndex(
                name: "IX_accessorytypes_name",
                table: "accessorytypes",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_laptopports_laptopid",
                table: "laptopports",
                column: "laptopid");

            migrationBuilder.CreateIndex(
                name: "IX_laptopwarranties_laptopid",
                table: "laptopwarranties",
                column: "laptopid");

            migrationBuilder.CreateIndex(
                name: "IX_pricehistories_accessoryid",
                table: "pricehistories",
                column: "accessoryid");

            migrationBuilder.CreateIndex(
                name: "IX_pricehistories_changedbyuserid",
                table: "pricehistories",
                column: "changedbyuserid");

            migrationBuilder.CreateIndex(
                name: "IX_pricehistories_effectivefrom",
                table: "pricehistories",
                column: "effectivefrom");

            migrationBuilder.CreateIndex(
                name: "IX_pricehistories_laptopvariantid",
                table: "pricehistories",
                column: "laptopvariantid");

            migrationBuilder.CreateIndex(
                name: "IX_pricehistories_producttype_productid_effectivefrom",
                table: "pricehistories",
                columns: new[] { "producttype", "productid", "effectivefrom" });

            migrationBuilder.CreateIndex(
                name: "IX_productdiscounts_accessoryid",
                table: "productdiscounts",
                column: "accessoryid");

            migrationBuilder.CreateIndex(
                name: "IX_productdiscounts_discountid_producttype_productid",
                table: "productdiscounts",
                columns: new[] { "discountid", "producttype", "productid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_productdiscounts_laptopvariantid",
                table: "productdiscounts",
                column: "laptopvariantid");

            migrationBuilder.CreateIndex(
                name: "IX_productviews_producttype_productid_viewedat",
                table: "productviews",
                columns: new[] { "producttype", "productid", "viewedat" });

            migrationBuilder.CreateIndex(
                name: "IX_productviews_userid",
                table: "productviews",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_shipments_orderid",
                table: "shipments",
                column: "orderid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_shipments_status",
                table: "shipments",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_shipments_trackingnumber",
                table: "shipments",
                column: "trackingnumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_stockalerts_isnotified",
                table: "stockalerts",
                column: "isnotified");

            migrationBuilder.CreateIndex(
                name: "IX_stockalerts_userid_producttype_productid",
                table: "stockalerts",
                columns: new[] { "userid", "producttype", "productid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userdiscountusages_discountid",
                table: "userdiscountusages",
                column: "discountid");

            migrationBuilder.CreateIndex(
                name: "IX_userdiscountusages_orderid",
                table: "userdiscountusages",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_userdiscountusages_userid_discountid",
                table: "userdiscountusages",
                columns: new[] { "userid", "discountid" });

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetroleclaims_aspnetroles_roleid",
                table: "aspnetroleclaims",
                column: "roleid",
                principalTable: "aspnetroles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetuserclaims_aspnetusers_userid",
                table: "aspnetuserclaims",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetuserlogins_aspnetusers_userid",
                table: "aspnetuserlogins",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetuserroles_aspnetroles_roleid",
                table: "aspnetuserroles",
                column: "roleid",
                principalTable: "aspnetroles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetuserroles_aspnetusers_userid",
                table: "aspnetuserroles",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_aspnetusertokens_aspnetusers_userid",
                table: "aspnetusertokens",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_auditlogs_aspnetusers_userid",
                table: "auditlogs",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cartitems_accessories_accessoryid",
                table: "cartitems",
                column: "accessoryid",
                principalTable: "accessories",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_cartitems_aspnetusers_userid",
                table: "cartitems",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cartitems_laptopvariants_laptopvariantid",
                table: "cartitems",
                column: "laptopvariantid",
                principalTable: "laptopvariants",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_categories_parentcategoryid",
                table: "categories",
                column: "parentcategoryid",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_emailqueues_aspnetusers_userid",
                table: "emailqueues",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_laptopimages_laptops_laptopid",
                table: "laptopimages",
                column: "laptopid",
                principalTable: "laptops",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_laptops_brands_brandid",
                table: "laptops",
                column: "brandid",
                principalTable: "brands",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_laptops_categories_categoryid",
                table: "laptops",
                column: "categoryid",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_laptopvariants_laptops_laptopid",
                table: "laptopvariants",
                column: "laptopid",
                principalTable: "laptops",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderitems_accessories_accessoryid",
                table: "orderitems",
                column: "accessoryid",
                principalTable: "accessories",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_orderitems_laptopvariants_laptopvariantid",
                table: "orderitems",
                column: "laptopvariantid",
                principalTable: "laptopvariants",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_orderitems_orders_orderid",
                table: "orderitems",
                column: "orderid",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_aspnetusers_userid",
                table: "orders",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_orders_orderid",
                table: "payments",
                column: "orderid",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_accessories_accessoryid",
                table: "ratings",
                column: "accessoryid",
                principalTable: "accessories",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_aspnetusers_userid",
                table: "ratings",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ratings_laptops_laptopid",
                table: "ratings",
                column: "laptopid",
                principalTable: "laptops",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_refreshtoken_aspnetusers_userid",
                table: "refreshtoken",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_repairrequests_aspnetusers_userid",
                table: "repairrequests",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_repairrequests_laptops_laptopid",
                table: "repairrequests",
                column: "laptopid",
                principalTable: "laptops",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_repairrequests_laptopvariants_laptopvariantid",
                table: "repairrequests",
                column: "laptopvariantid",
                principalTable: "laptopvariants",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_repairrequests_repairserviceitems_serviceitemid",
                table: "repairrequests",
                column: "serviceitemid",
                principalTable: "repairserviceitems",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_verificationcodes_aspnetusers_userid",
                table: "verificationcodes",
                column: "userid",
                principalTable: "aspnetusers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_aspnetroleclaims_aspnetroles_roleid",
                table: "aspnetroleclaims");

            migrationBuilder.DropForeignKey(
                name: "FK_aspnetuserclaims_aspnetusers_userid",
                table: "aspnetuserclaims");

            migrationBuilder.DropForeignKey(
                name: "FK_aspnetuserlogins_aspnetusers_userid",
                table: "aspnetuserlogins");

            migrationBuilder.DropForeignKey(
                name: "FK_aspnetuserroles_aspnetroles_roleid",
                table: "aspnetuserroles");

            migrationBuilder.DropForeignKey(
                name: "FK_aspnetuserroles_aspnetusers_userid",
                table: "aspnetuserroles");

            migrationBuilder.DropForeignKey(
                name: "FK_aspnetusertokens_aspnetusers_userid",
                table: "aspnetusertokens");

            migrationBuilder.DropForeignKey(
                name: "FK_auditlogs_aspnetusers_userid",
                table: "auditlogs");

            migrationBuilder.DropForeignKey(
                name: "FK_cartitems_accessories_accessoryid",
                table: "cartitems");

            migrationBuilder.DropForeignKey(
                name: "FK_cartitems_aspnetusers_userid",
                table: "cartitems");

            migrationBuilder.DropForeignKey(
                name: "FK_cartitems_laptopvariants_laptopvariantid",
                table: "cartitems");

            migrationBuilder.DropForeignKey(
                name: "FK_categories_categories_parentcategoryid",
                table: "categories");

            migrationBuilder.DropForeignKey(
                name: "FK_emailqueues_aspnetusers_userid",
                table: "emailqueues");

            migrationBuilder.DropForeignKey(
                name: "FK_laptopimages_laptops_laptopid",
                table: "laptopimages");

            migrationBuilder.DropForeignKey(
                name: "FK_laptops_brands_brandid",
                table: "laptops");

            migrationBuilder.DropForeignKey(
                name: "FK_laptops_categories_categoryid",
                table: "laptops");

            migrationBuilder.DropForeignKey(
                name: "FK_laptopvariants_laptops_laptopid",
                table: "laptopvariants");

            migrationBuilder.DropForeignKey(
                name: "FK_orderitems_accessories_accessoryid",
                table: "orderitems");

            migrationBuilder.DropForeignKey(
                name: "FK_orderitems_laptopvariants_laptopvariantid",
                table: "orderitems");

            migrationBuilder.DropForeignKey(
                name: "FK_orderitems_orders_orderid",
                table: "orderitems");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_aspnetusers_userid",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_orders_orderid",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_ratings_accessories_accessoryid",
                table: "ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_ratings_aspnetusers_userid",
                table: "ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_ratings_laptops_laptopid",
                table: "ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_refreshtoken_aspnetusers_userid",
                table: "refreshtoken");

            migrationBuilder.DropForeignKey(
                name: "FK_repairrequests_aspnetusers_userid",
                table: "repairrequests");

            migrationBuilder.DropForeignKey(
                name: "FK_repairrequests_laptops_laptopid",
                table: "repairrequests");

            migrationBuilder.DropForeignKey(
                name: "FK_repairrequests_laptopvariants_laptopvariantid",
                table: "repairrequests");

            migrationBuilder.DropForeignKey(
                name: "FK_repairrequests_repairserviceitems_serviceitemid",
                table: "repairrequests");

            migrationBuilder.DropForeignKey(
                name: "FK_verificationcodes_aspnetusers_userid",
                table: "verificationcodes");

            migrationBuilder.DropTable(
                name: "accessoryattributes");

            migrationBuilder.DropTable(
                name: "accessorycompatibilities");

            migrationBuilder.DropTable(
                name: "accessoryimages");

            migrationBuilder.DropTable(
                name: "laptopports");

            migrationBuilder.DropTable(
                name: "laptopwarranties");

            migrationBuilder.DropTable(
                name: "pricehistories");

            migrationBuilder.DropTable(
                name: "productdiscounts");

            migrationBuilder.DropTable(
                name: "productviews");

            migrationBuilder.DropTable(
                name: "shipments");

            migrationBuilder.DropTable(
                name: "stockalerts");

            migrationBuilder.DropTable(
                name: "userdiscountusages");

            migrationBuilder.DropTable(
                name: "accessories");

            migrationBuilder.DropTable(
                name: "accessorytypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_verificationcodes",
                table: "verificationcodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_repairserviceitems",
                table: "repairserviceitems");

            migrationBuilder.DropIndex(
                name: "IX_repairserviceitems_isactive",
                table: "repairserviceitems");

            migrationBuilder.DropIndex(
                name: "IX_repairserviceitems_repairtype",
                table: "repairserviceitems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_repairrequests",
                table: "repairrequests");

            migrationBuilder.DropIndex(
                name: "IX_repairrequests_laptopvariantid",
                table: "repairrequests");

            migrationBuilder.DropIndex(
                name: "IX_repairrequests_requestdate",
                table: "repairrequests");

            migrationBuilder.DropIndex(
                name: "IX_repairrequests_requestnumber",
                table: "repairrequests");

            migrationBuilder.DropIndex(
                name: "IX_repairrequests_status",
                table: "repairrequests");

            migrationBuilder.DropIndex(
                name: "IX_repairrequests_userid",
                table: "repairrequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_refreshtoken",
                table: "refreshtoken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ratings",
                table: "ratings");

            migrationBuilder.DropIndex(
                name: "IX_ratings_accessoryid",
                table: "ratings");

            migrationBuilder.DropIndex(
                name: "IX_ratings_producttype_productid",
                table: "ratings");

            migrationBuilder.DropIndex(
                name: "IX_ratings_stars",
                table: "ratings");

            migrationBuilder.DropIndex(
                name: "IX_ratings_userid_producttype_productid",
                table: "ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_payments",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_payments_paymentstatus",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_payments_paymoborderid",
                table: "payments");

            migrationBuilder.DropIndex(
                name: "IX_payments_transactionid",
                table: "payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orders",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_orderdate",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_ordernumber",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_ordertype",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_reservationexpirydate",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_status",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orderitems",
                table: "orderitems");

            migrationBuilder.DropIndex(
                name: "IX_orderitems_accessoryid",
                table: "orderitems");

            migrationBuilder.DropIndex(
                name: "IX_orderitems_producttype_productid",
                table: "orderitems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_laptopvariants",
                table: "laptopvariants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_laptops",
                table: "laptops");

            migrationBuilder.DropPrimaryKey(
                name: "PK_laptopimages",
                table: "laptopimages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_emailqueues",
                table: "emailqueues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_discounts",
                table: "discounts");

            migrationBuilder.DropIndex(
                name: "IX_discounts_code",
                table: "discounts");

            migrationBuilder.DropIndex(
                name: "IX_discounts_isactive_startdate_enddate",
                table: "discounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.DropIndex(
                name: "IX_categories_parentcategoryid",
                table: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cartitems",
                table: "cartitems");

            migrationBuilder.DropIndex(
                name: "IX_cartitems_accessoryid",
                table: "cartitems");

            migrationBuilder.DropIndex(
                name: "IX_cartitems_userid_producttype_productid",
                table: "cartitems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_brands",
                table: "brands");

            migrationBuilder.DropPrimaryKey(
                name: "PK_auditlogs",
                table: "auditlogs");

            migrationBuilder.DropIndex(
                name: "IX_auditlogs_entitytype_entityid",
                table: "auditlogs");

            migrationBuilder.DropIndex(
                name: "IX_auditlogs_timestamp",
                table: "auditlogs");

            migrationBuilder.DropIndex(
                name: "IX_auditlogs_userid",
                table: "auditlogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetusertokens",
                table: "aspnetusertokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetusers",
                table: "aspnetusers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetuserroles",
                table: "aspnetuserroles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetuserlogins",
                table: "aspnetuserlogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetuserclaims",
                table: "aspnetuserclaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetroles",
                table: "aspnetroles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_aspnetroleclaims",
                table: "aspnetroleclaims");

            migrationBuilder.DropColumn(
                name: "id",
                table: "repairserviceitems");

            migrationBuilder.DropColumn(
                name: "createdat",
                table: "repairserviceitems");

            migrationBuilder.DropColumn(
                name: "deletedat",
                table: "repairserviceitems");

            migrationBuilder.DropColumn(
                name: "description",
                table: "repairserviceitems");

            migrationBuilder.DropColumn(
                name: "isactive",
                table: "repairserviceitems");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "repairserviceitems");

            migrationBuilder.DropColumn(
                name: "updatedat",
                table: "repairserviceitems");

            migrationBuilder.DropColumn(
                name: "completeddate",
                table: "repairrequests");

            migrationBuilder.DropColumn(
                name: "createdat",
                table: "repairrequests");

            migrationBuilder.DropColumn(
                name: "deletedat",
                table: "repairrequests");

            migrationBuilder.DropColumn(
                name: "deviceserial",
                table: "repairrequests");

            migrationBuilder.DropColumn(
                name: "diagnosisnotes",
                table: "repairrequests");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "repairrequests");

            migrationBuilder.DropColumn(
                name: "laptopvariantid",
                table: "repairrequests");

            migrationBuilder.DropColumn(
                name: "priority",
                table: "repairrequests");

            migrationBuilder.DropColumn(
                name: "quotedprice",
                table: "repairrequests");

            migrationBuilder.DropColumn(
                name: "requestnumber",
                table: "repairrequests");

            migrationBuilder.DropColumn(
                name: "updatedat",
                table: "repairrequests");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "refreshtoken");

            migrationBuilder.DropColumn(
                name: "accessoryid",
                table: "ratings");

            migrationBuilder.DropColumn(
                name: "deletedat",
                table: "ratings");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "ratings");

            migrationBuilder.DropColumn(
                name: "isverifiedpurchase",
                table: "ratings");

            migrationBuilder.DropColumn(
                name: "productid",
                table: "ratings");

            migrationBuilder.DropColumn(
                name: "producttype",
                table: "ratings");

            migrationBuilder.DropColumn(
                name: "updatedat",
                table: "ratings");

            migrationBuilder.DropColumn(
                name: "amountcents",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "billingdata",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "capturedamountcents",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "createdat",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "currency",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "deletedat",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "errorcode",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "errormessage",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "integrationid",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "is3ds",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "iscaptured",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "isrefunded",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "paymobcreatedat",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "paymoborderid",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "refundedamountcents",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "transactionfingerprint",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "cancelreason",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "createdat",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "deletedat",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "deliveryaddress",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "deliverycost",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "deliveryinstructions",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "discountamount",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "isreservationcompleted",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ordernumber",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ordertype",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "reservationamount",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "reservationexpirydate",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "shippingcost",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "subtotalamount",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "taxamount",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "updatedat",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "accessoryid",
                table: "orderitems");

            migrationBuilder.DropColumn(
                name: "createdat",
                table: "orderitems");

            migrationBuilder.DropColumn(
                name: "deletedat",
                table: "orderitems");

            migrationBuilder.DropColumn(
                name: "discountamount",
                table: "orderitems");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "orderitems");

            migrationBuilder.DropColumn(
                name: "productid",
                table: "orderitems");

            migrationBuilder.DropColumn(
                name: "productname",
                table: "orderitems");

            migrationBuilder.DropColumn(
                name: "producttype",
                table: "orderitems");

            migrationBuilder.DropColumn(
                name: "sku",
                table: "orderitems");

            migrationBuilder.DropColumn(
                name: "totalprice",
                table: "orderitems");

            migrationBuilder.DropColumn(
                name: "updatedat",
                table: "orderitems");

            migrationBuilder.DropColumn(
                name: "createdat",
                table: "laptopvariants");

            migrationBuilder.DropColumn(
                name: "deletedat",
                table: "laptopvariants");

            migrationBuilder.DropColumn(
                name: "isactive",
                table: "laptopvariants");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "laptopvariants");

            migrationBuilder.DropColumn(
                name: "reorderlevel",
                table: "laptopvariants");

            migrationBuilder.DropColumn(
                name: "reservedquantity",
                table: "laptopvariants");

            migrationBuilder.DropColumn(
                name: "sku",
                table: "laptopvariants");

            migrationBuilder.DropColumn(
                name: "storagetype",
                table: "laptopvariants");

            migrationBuilder.DropColumn(
                name: "updatedat",
                table: "laptopvariants");

            migrationBuilder.DropColumn(
                name: "createdat",
                table: "laptops");

            migrationBuilder.DropColumn(
                name: "deletedat",
                table: "laptops");

            migrationBuilder.DropColumn(
                name: "isactive",
                table: "laptops");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "laptops");

            migrationBuilder.DropColumn(
                name: "releaseyear",
                table: "laptops");

            migrationBuilder.DropColumn(
                name: "storecontact",
                table: "laptops");

            migrationBuilder.DropColumn(
                name: "updatedat",
                table: "laptops");

            migrationBuilder.DropColumn(
                name: "createdat",
                table: "laptopimages");

            migrationBuilder.DropColumn(
                name: "deletedat",
                table: "laptopimages");

            migrationBuilder.DropColumn(
                name: "displayorder",
                table: "laptopimages");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "laptopimages");

            migrationBuilder.DropColumn(
                name: "updatedat",
                table: "laptopimages");

            migrationBuilder.DropColumn(
                name: "code",
                table: "discounts");

            migrationBuilder.DropColumn(
                name: "createdat",
                table: "discounts");

            migrationBuilder.DropColumn(
                name: "deletedat",
                table: "discounts");

            migrationBuilder.DropColumn(
                name: "discounttype",
                table: "discounts");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "discounts");

            migrationBuilder.DropColumn(
                name: "maxdiscountamount",
                table: "discounts");

            migrationBuilder.DropColumn(
                name: "minimumpurchase",
                table: "discounts");

            migrationBuilder.DropColumn(
                name: "updatedat",
                table: "discounts");

            migrationBuilder.DropColumn(
                name: "usagecount",
                table: "discounts");

            migrationBuilder.DropColumn(
                name: "usagelimit",
                table: "discounts");

            migrationBuilder.DropColumn(
                name: "value",
                table: "discounts");

            migrationBuilder.DropColumn(
                name: "createdat",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "deletedat",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "displayorder",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "imageurl",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "isactive",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "parentcategoryid",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "updatedat",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "accessoryid",
                table: "cartitems");

            migrationBuilder.DropColumn(
                name: "createdat",
                table: "cartitems");

            migrationBuilder.DropColumn(
                name: "deletedat",
                table: "cartitems");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "cartitems");

            migrationBuilder.DropColumn(
                name: "productid",
                table: "cartitems");

            migrationBuilder.DropColumn(
                name: "producttype",
                table: "cartitems");

            migrationBuilder.DropColumn(
                name: "updatedat",
                table: "cartitems");

            migrationBuilder.DropColumn(
                name: "createdat",
                table: "brands");

            migrationBuilder.DropColumn(
                name: "deletedat",
                table: "brands");

            migrationBuilder.DropColumn(
                name: "isactive",
                table: "brands");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "brands");

            migrationBuilder.DropColumn(
                name: "updatedat",
                table: "brands");

            migrationBuilder.DropColumn(
                name: "createdat",
                table: "auditlogs");

            migrationBuilder.DropColumn(
                name: "deletedat",
                table: "auditlogs");

            migrationBuilder.DropColumn(
                name: "ipaddress",
                table: "auditlogs");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "auditlogs");

            migrationBuilder.DropColumn(
                name: "updatedat",
                table: "auditlogs");

            migrationBuilder.DropColumn(
                name: "useragent",
                table: "auditlogs");

            migrationBuilder.DropColumn(
                name: "userid",
                table: "auditlogs");

            migrationBuilder.DropColumn(
                name: "addressline",
                table: "aspnetusers");

            migrationBuilder.DropColumn(
                name: "city",
                table: "aspnetusers");

            migrationBuilder.DropColumn(
                name: "country",
                table: "aspnetusers");

            migrationBuilder.DropColumn(
                name: "isdeleted",
                table: "aspnetusers");

            migrationBuilder.DropColumn(
                name: "profileimageurl",
                table: "aspnetusers");

            migrationBuilder.DropColumn(
                name: "state",
                table: "aspnetusers");

            migrationBuilder.DropColumn(
                name: "updatedat",
                table: "aspnetusers");

            migrationBuilder.RenameTable(
                name: "verificationcodes",
                newName: "VerificationCodes");

            migrationBuilder.RenameTable(
                name: "repairserviceitems",
                newName: "RepairServiceItems");

            migrationBuilder.RenameTable(
                name: "repairrequests",
                newName: "RepairRequests");

            migrationBuilder.RenameTable(
                name: "ratings",
                newName: "Ratings");

            migrationBuilder.RenameTable(
                name: "payments",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "orders",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "orderitems",
                newName: "OrderItems");

            migrationBuilder.RenameTable(
                name: "laptopvariants",
                newName: "LaptopVariants");

            migrationBuilder.RenameTable(
                name: "laptops",
                newName: "Laptops");

            migrationBuilder.RenameTable(
                name: "laptopimages",
                newName: "LaptopImages");

            migrationBuilder.RenameTable(
                name: "emailqueues",
                newName: "EmailQueues");

            migrationBuilder.RenameTable(
                name: "discounts",
                newName: "Discounts");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "cartitems",
                newName: "CartItems");

            migrationBuilder.RenameTable(
                name: "brands",
                newName: "Brands");

            migrationBuilder.RenameTable(
                name: "auditlogs",
                newName: "AuditLogs");

            migrationBuilder.RenameTable(
                name: "aspnetusertokens",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "aspnetusers",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "aspnetuserroles",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "aspnetuserlogins",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "aspnetuserclaims",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "aspnetroles",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "aspnetroleclaims",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameColumn(
                name: "estimateddays",
                table: "RepairServiceItems",
                newName: "itemid");

            migrationBuilder.RenameColumn(
                name: "baseprice",
                table: "RepairServiceItems",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "RepairRequests",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "serviceitemid",
                table: "RepairRequests",
                newName: "itemid");

            migrationBuilder.RenameColumn(
                name: "issuedescription",
                table: "RepairRequests",
                newName: "applicationuserid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "RepairRequests",
                newName: "requestid");

            migrationBuilder.RenameIndex(
                name: "IX_repairrequests_laptopid",
                table: "RepairRequests",
                newName: "IX_RepairRequests_laptopid");

            migrationBuilder.RenameIndex(
                name: "IX_repairrequests_serviceitemid",
                table: "RepairRequests",
                newName: "IX_RepairRequests_itemid");

            migrationBuilder.RenameIndex(
                name: "IX_ratings_laptopid",
                table: "Ratings",
                newName: "IX_Ratings_laptopid");

            migrationBuilder.RenameColumn(
                name: "updatedat",
                table: "Payments",
                newName: "paidat");

            migrationBuilder.RenameIndex(
                name: "IX_payments_orderid",
                table: "Payments",
                newName: "IX_Payments_orderid");

            migrationBuilder.RenameIndex(
                name: "IX_orders_userid",
                table: "Orders",
                newName: "IX_Orders_userid");

            migrationBuilder.RenameIndex(
                name: "IX_orderitems_orderid",
                table: "OrderItems",
                newName: "IX_OrderItems_orderid");

            migrationBuilder.RenameIndex(
                name: "IX_orderitems_laptopvariantid",
                table: "OrderItems",
                newName: "IX_OrderItems_laptopvariantid");

            migrationBuilder.RenameColumn(
                name: "storagecapacitygb",
                table: "LaptopVariants",
                newName: "storage");

            migrationBuilder.RenameColumn(
                name: "currentprice",
                table: "LaptopVariants",
                newName: "price");

            migrationBuilder.RenameIndex(
                name: "IX_laptopvariants_laptopid",
                table: "LaptopVariants",
                newName: "IX_LaptopVariants_laptopid");

            migrationBuilder.RenameColumn(
                name: "storelocation",
                table: "Laptops",
                newName: "warranty");

            migrationBuilder.RenameIndex(
                name: "IX_laptops_categoryid",
                table: "Laptops",
                newName: "IX_Laptops_categoryid");

            migrationBuilder.RenameIndex(
                name: "IX_laptops_brandid",
                table: "Laptops",
                newName: "IX_Laptops_brandid");

            migrationBuilder.RenameIndex(
                name: "IX_laptopimages_laptopid",
                table: "LaptopImages",
                newName: "IX_LaptopImages_laptopid");

            migrationBuilder.RenameIndex(
                name: "IX_emailqueues_userid",
                table: "EmailQueues",
                newName: "IX_EmailQueues_userid");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "CartItems",
                newName: "applicationuserid");

            migrationBuilder.RenameIndex(
                name: "IX_cartitems_laptopvariantid",
                table: "CartItems",
                newName: "IX_CartItems_laptopvariantid");

            migrationBuilder.RenameColumn(
                name: "oldvalues",
                table: "AuditLogs",
                newName: "details");

            migrationBuilder.RenameColumn(
                name: "newvalues",
                table: "AuditLogs",
                newName: "applicationuserid");

            migrationBuilder.RenameColumn(
                name: "entitytype",
                table: "AuditLogs",
                newName: "entity");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetuserroles_roleid",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_roleid");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetuserlogins_userid",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_userid");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetuserclaims_userid",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_userid");

            migrationBuilder.RenameIndex(
                name: "IX_aspnetroleclaims_roleid",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_roleid");

            migrationBuilder.AlterColumn<string>(
                name: "userid",
                table: "VerificationCodes",
                type: "character varying(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "expirydate",
                table: "VerificationCodes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now() + interval '60 minutes'",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW() + INTERVAL '60 minutes'");

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdat",
                table: "VerificationCodes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AlterColumn<int>(
                name: "itemid",
                table: "RepairServiceItems",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "estimatedtime",
                table: "RepairServiceItems",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "requestdate",
                table: "RepairRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AddColumn<string>(
                name: "applicationuserid",
                table: "refreshtoken",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "laptopid",
                table: "Ratings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdat",
                table: "Ratings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AlterColumn<string>(
                name: "paymentstatus",
                table: "Payments",
                type: "text",
                nullable: false,
                defaultValue: "Pending",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "PENDING");

            migrationBuilder.AlterColumn<string>(
                name: "paymentmethod",
                table: "Payments",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "Orders",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "Pending");

            migrationBuilder.AlterColumn<DateTime>(
                name: "orderdate",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AlterColumn<int>(
                name: "laptopvariantid",
                table: "OrderItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "stockquantity",
                table: "LaptopVariants",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "discountid",
                table: "LaptopVariants",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "notes",
                table: "Laptops",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ports",
                table: "Laptops",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "uploadedat",
                table: "LaptopImages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AddColumn<decimal>(
                name: "percentage",
                table: "Discounts",
                type: "numeric(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "categoryimageurl",
                table: "Categories",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "laptopvariantid",
                table: "CartItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "addedat",
                table: "CartItems",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AlterColumn<DateTime>(
                name: "timestamp",
                table: "AuditLogs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AlterColumn<int>(
                name: "entityid",
                table: "AuditLogs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "createdat",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "TIMEZONE('utc', NOW())");

            migrationBuilder.AddColumn<bool>(
                name: "isactive",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "phone",
                table: "AspNetUsers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "role",
                table: "AspNetUsers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VerificationCodes",
                table: "VerificationCodes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairServiceItems",
                table: "RepairServiceItems",
                column: "itemid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairRequests",
                table: "RepairRequests",
                column: "requestid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_refreshtoken",
                table: "refreshtoken",
                columns: new[] { "applicationuserid", "id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LaptopVariants",
                table: "LaptopVariants",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Laptops",
                table: "Laptops",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LaptopImages",
                table: "LaptopImages",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailQueues",
                table: "EmailQueues",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brands",
                table: "Brands",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuditLogs",
                table: "AuditLogs",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "userid", "loginprovider", "name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "userid", "roleid" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "loginprovider", "providerkey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Shippings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    orderid = table.Column<int>(type: "integer", nullable: false),
                    address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    deliveredat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    postalcode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    shippedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    trackingnumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippings", x => x.id);
                    table.ForeignKey(
                        name: "FK_Shippings_Orders_orderid",
                        column: x => x.orderid,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_VerificationCodes_Attempt_NonNegative",
                table: "VerificationCodes",
                sql: "AttemptCount >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_VerificationCodes_Expiry_After_Created",
                table: "VerificationCodes",
                sql: "ExpiryDate > CreatedAt");

            migrationBuilder.AddCheckConstraint(
                name: "CK_VerificationCodes_MaxAttempts_Positive",
                table: "VerificationCodes",
                sql: "MaxAttempts > 0");

            migrationBuilder.CreateIndex(
                name: "IX_RepairRequests_applicationuserid",
                table: "RepairRequests",
                column: "applicationuserid");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_userid",
                table: "Ratings",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_LaptopVariants_discountid",
                table: "LaptopVariants",
                column: "discountid");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_applicationuserid",
                table: "CartItems",
                column: "applicationuserid");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_applicationuserid",
                table: "AuditLogs",
                column: "applicationuserid");

            migrationBuilder.CreateIndex(
                name: "IX_Shippings_orderid",
                table: "Shippings",
                column: "orderid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_roleid",
                table: "AspNetRoleClaims",
                column: "roleid",
                principalTable: "AspNetRoles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_userid",
                table: "AspNetUserClaims",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_userid",
                table: "AspNetUserLogins",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_roleid",
                table: "AspNetUserRoles",
                column: "roleid",
                principalTable: "AspNetRoles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_userid",
                table: "AspNetUserRoles",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_userid",
                table: "AspNetUserTokens",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_AspNetUsers_applicationuserid",
                table: "AuditLogs",
                column: "applicationuserid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_AspNetUsers_applicationuserid",
                table: "CartItems",
                column: "applicationuserid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_LaptopVariants_laptopvariantid",
                table: "CartItems",
                column: "laptopvariantid",
                principalTable: "LaptopVariants",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmailQueues_AspNetUsers_userid",
                table: "EmailQueues",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LaptopImages_Laptops_laptopid",
                table: "LaptopImages",
                column: "laptopid",
                principalTable: "Laptops",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Laptops_Brands_brandid",
                table: "Laptops",
                column: "brandid",
                principalTable: "Brands",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Laptops_Categories_categoryid",
                table: "Laptops",
                column: "categoryid",
                principalTable: "Categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LaptopVariants_Discounts_discountid",
                table: "LaptopVariants",
                column: "discountid",
                principalTable: "Discounts",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LaptopVariants_Laptops_laptopid",
                table: "LaptopVariants",
                column: "laptopid",
                principalTable: "Laptops",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_LaptopVariants_laptopvariantid",
                table: "OrderItems",
                column: "laptopvariantid",
                principalTable: "LaptopVariants",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_orderid",
                table: "OrderItems",
                column: "orderid",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_userid",
                table: "Orders",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Orders_orderid",
                table: "Payments",
                column: "orderid",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_userid",
                table: "Ratings",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Laptops_laptopid",
                table: "Ratings",
                column: "laptopid",
                principalTable: "Laptops",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_refreshtoken_AspNetUsers_applicationuserid",
                table: "refreshtoken",
                column: "applicationuserid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequests_AspNetUsers_applicationuserid",
                table: "RepairRequests",
                column: "applicationuserid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequests_Laptops_laptopid",
                table: "RepairRequests",
                column: "laptopid",
                principalTable: "Laptops",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequests_RepairServiceItems_itemid",
                table: "RepairRequests",
                column: "itemid",
                principalTable: "RepairServiceItems",
                principalColumn: "itemid",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VerificationCodes_AspNetUsers_userid",
                table: "VerificationCodes",
                column: "userid",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
