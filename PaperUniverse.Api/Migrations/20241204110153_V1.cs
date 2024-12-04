using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaperUniverse.Api.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "NVARCHAR(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    VerificationCode = table.Column<string>(type: "CHAR(6)", maxLength: 6, nullable: false),
                    VerificationCodeExpiresAt = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    VerificationCodeVerifiedAt = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    Password = table.Column<string>(type: "NVARCHAR(255)", maxLength: 255, nullable: false),
                    ResetPasswordCode = table.Column<string>(type: "CHAR(6)", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
