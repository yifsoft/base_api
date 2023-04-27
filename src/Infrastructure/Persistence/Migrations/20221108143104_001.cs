using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    PhoneVerified = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshTokenExpiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserOperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OperationClaimId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperationClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserOperationClaimId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationClaims_UserOperationClaims_UserOperationClaimId",
                        column: x => x.UserOperationClaimId,
                        principalTable: "UserOperationClaims",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Name", "UserOperationClaimId" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Admin", null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "User", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "EmailHash", "EmailVerified", "FirstName", "IsActive", "LastName", "PasswordHash", "PasswordSalt", "PhoneHash", "PhoneVerified", "RefreshToken", "RefreshTokenExpiration", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2022, 11, 8, 17, 31, 4, 322, DateTimeKind.Local).AddTicks(4482), null, "XmdBrE/TwVfC1R5blXnrzWF6Ajw11AQaQ6P2wsj4iK0RCGUF4WM0iKbEgtDO9YMc", false, "Furkan", false, "Yılmaz", new byte[] { 109, 89, 33, 136, 43, 43, 89, 192, 179, 26, 32, 70, 127, 94, 34, 166, 244, 88, 210, 162, 59, 9, 16, 74, 173, 208, 128, 31, 214, 111, 39, 67, 135, 27, 146, 152, 148, 43, 96, 232, 249, 76, 179, 20, 154, 137, 12, 199, 211, 108, 136, 97, 53, 67, 82, 111, 24, 135, 173, 68, 149, 189, 245, 3 }, new byte[] { 213, 159, 14, 248, 205, 160, 143, 69, 172, 83, 2, 68, 33, 71, 204, 221, 12, 94, 94, 74, 182, 160, 65, 75, 129, 226, 16, 252, 240, 38, 252, 243, 250, 147, 116, 179, 133, 131, 246, 183, 36, 35, 158, 182, 80, 183, 189, 142, 166, 74, 236, 146, 129, 156, 216, 161, 80, 132, 196, 47, 111, 210, 73, 113, 17, 215, 3, 217, 182, 28, 163, 231, 74, 108, 97, 162, 163, 130, 126, 41, 192, 24, 238, 130, 54, 108, 227, 1, 64, 2, 168, 127, 66, 166, 172, 153, 237, 125, 88, 160, 72, 228, 219, 105, 200, 105, 51, 114, 232, 16, 16, 96, 96, 206, 117, 238, 32, 188, 190, 117, 217, 30, 13, 169, 47, 2, 90, 182 }, "/+SsGevv8ctONg99z8OKvqMGROjKWlzKQOP9pj33R34=", false, "", null, null });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "OperationClaimId", "UserId" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_OperationClaims_UserOperationClaimId",
                table: "OperationClaims",
                column: "UserOperationClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_UserId",
                table: "UserOperationClaims",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationClaims");

            migrationBuilder.DropTable(
                name: "UserOperationClaims");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
