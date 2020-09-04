using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Competitor.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Identity_Role",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identity_User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(maxLength: 450, nullable: true),
                    Email = table.Column<string>(maxLength: 450, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    SerialNumber = table.Column<string>(maxLength: 450, nullable: true),
                    LastLoggedIn = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Identity_UserRole",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_Identity_UserRole_Identity_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Identity_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Identity_UserRole_Identity_User_UserId",
                        column: x => x.UserId,
                        principalTable: "Identity_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identity_UserToken",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessTokenHash = table.Column<string>(nullable: true),
                    AccessTokenExpiresDateTime = table.Column<DateTimeOffset>(nullable: false),
                    RefreshTokenIdHash = table.Column<string>(maxLength: 450, nullable: false),
                    RefreshTokenIdHashSource = table.Column<string>(maxLength: 450, nullable: true),
                    RefreshTokenExpiresDateTime = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identity_UserToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identity_UserToken_Identity_User_UserId",
                        column: x => x.UserId,
                        principalTable: "Identity_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Identity_Role_Name",
                table: "Identity_Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Identity_User_Email",
                table: "Identity_User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Identity_UserRole_RoleId",
                table: "Identity_UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_UserRole_UserId",
                table: "Identity_UserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Identity_UserToken_UserId",
                table: "Identity_UserToken",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Identity_UserRole");

            migrationBuilder.DropTable(
                name: "Identity_UserToken");

            migrationBuilder.DropTable(
                name: "Identity_Role");

            migrationBuilder.DropTable(
                name: "Identity_User");
        }
    }
}
