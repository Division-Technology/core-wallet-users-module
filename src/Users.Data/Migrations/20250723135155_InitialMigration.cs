using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Language = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    RegistrationStatus = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    IsBlocked = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    HasVehicle = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TelegramId = table.Column<long>(type: "bigint", nullable: true),
                    ChatId = table.Column<long>(type: "bigint", nullable: true),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "now()"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "idx_users_block",
                table: "Users",
                column: "IsBlocked");

            migrationBuilder.CreateIndex(
                name: "idx_users_chat_id",
                table: "Users",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "idx_users_phone_number",
                table: "Users",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "idx_users_registration_status",
                table: "Users",
                column: "RegistrationStatus");

            migrationBuilder.CreateIndex(
                name: "idx_users_telegram_id",
                table: "Users",
                column: "TelegramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
