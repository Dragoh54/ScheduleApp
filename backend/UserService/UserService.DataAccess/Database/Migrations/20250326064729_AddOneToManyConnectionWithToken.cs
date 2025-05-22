using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.DataAccess.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddOneToManyConnectionWithToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b92bca83-5a80-4323-8c93-635e3486998c"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("b865f0c0-e730-49a5-8c80-9b9b4f5b8d56"), new Guid("5d360b8e-d55d-45dc-86a0-994062809dab") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("b9042180-949b-470c-9ebb-4fb6b1b27404"), new Guid("5d360b8e-d55d-45dc-86a0-994062809dab") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c7ed4f3b-e0e3-40e1-8dda-0d5c7b201a0c"), new Guid("5d360b8e-d55d-45dc-86a0-994062809dab") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c7ed4f3b-e0e3-40e1-8dda-0d5c7b201a0c"), new Guid("727dd265-0f1a-48f3-bce0-7fe085e92310") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("b865f0c0-e730-49a5-8c80-9b9b4f5b8d56"), new Guid("8f841611-a026-48a1-aee8-669d5760a45f") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c7ed4f3b-e0e3-40e1-8dda-0d5c7b201a0c"), new Guid("8f841611-a026-48a1-aee8-669d5760a45f") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b865f0c0-e730-49a5-8c80-9b9b4f5b8d56"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b9042180-949b-470c-9ebb-4fb6b1b27404"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c7ed4f3b-e0e3-40e1-8dda-0d5c7b201a0c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5d360b8e-d55d-45dc-86a0-994062809dab"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("727dd265-0f1a-48f3-bce0-7fe085e92310"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8f841611-a026-48a1-aee8-669d5760a45f"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { new Guid("17c36ce2-6b5a-4de3-bb16-3d3083b70721"), 2 },
                    { new Guid("3237da66-7d56-4d72-adcf-05387e6a201c"), 0 },
                    { new Guid("51d33d94-bf90-4cac-82cb-a3d00e7413f6"), 3 },
                    { new Guid("c59ca3a9-7975-41fa-ac23-35ce8ed395ee"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "FirstName", "IsConfirmed", "IsDeleted", "LastName", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("0c2052f6-c3d5-425b-bf51-08aff04dc281"), new DateTime(2025, 3, 26, 6, 47, 28, 221, DateTimeKind.Utc).AddTicks(3054), null, "admin@example.com", "Admin", true, false, "User", "$2a$11$fMotE2uso.Vl/dInRwOYE.CVTYpLLW9bRv/JxwOcFuISgyclU/XKS", new DateTime(2025, 3, 26, 6, 47, 28, 221, DateTimeKind.Utc).AddTicks(3162), "admin" },
                    { new Guid("c8ce159b-36c8-4ca1-988c-69feac103bb1"), new DateTime(2025, 3, 26, 6, 47, 28, 221, DateTimeKind.Utc).AddTicks(3467), null, "user2@example.com", "Jane", false, false, "Doe", "$2a$11$fP8SA4QPz5bfUeIS5ZH8LOE8VXKYY1JH6JYfIk5iCCqs6PyV6d8La", new DateTime(2025, 3, 26, 6, 47, 28, 221, DateTimeKind.Utc).AddTicks(3468), "user2" },
                    { new Guid("f787e458-93e0-41ef-9ba5-2f6bf8ee5a6f"), new DateTime(2025, 3, 26, 6, 47, 28, 221, DateTimeKind.Utc).AddTicks(3465), null, "user1@example.com", "John", true, false, "Doe", "$2a$11$Vmq/dCwgmETEykLr31YlZez.vX2akujpnGBJoXsRFSL9n112KwOLK", new DateTime(2025, 3, 26, 6, 47, 28, 221, DateTimeKind.Utc).AddTicks(3466), "user1" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("17c36ce2-6b5a-4de3-bb16-3d3083b70721"), new Guid("0c2052f6-c3d5-425b-bf51-08aff04dc281") },
                    { new Guid("51d33d94-bf90-4cac-82cb-a3d00e7413f6"), new Guid("0c2052f6-c3d5-425b-bf51-08aff04dc281") },
                    { new Guid("c59ca3a9-7975-41fa-ac23-35ce8ed395ee"), new Guid("0c2052f6-c3d5-425b-bf51-08aff04dc281") },
                    { new Guid("c59ca3a9-7975-41fa-ac23-35ce8ed395ee"), new Guid("c8ce159b-36c8-4ca1-988c-69feac103bb1") },
                    { new Guid("17c36ce2-6b5a-4de3-bb16-3d3083b70721"), new Guid("f787e458-93e0-41ef-9ba5-2f6bf8ee5a6f") },
                    { new Guid("c59ca3a9-7975-41fa-ac23-35ce8ed395ee"), new Guid("f787e458-93e0-41ef-9ba5-2f6bf8ee5a6f") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tokens_Users_UserId",
                table: "Tokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tokens_Users_UserId",
                table: "Tokens");

            migrationBuilder.DropIndex(
                name: "IX_Tokens_UserId",
                table: "Tokens");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3237da66-7d56-4d72-adcf-05387e6a201c"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("17c36ce2-6b5a-4de3-bb16-3d3083b70721"), new Guid("0c2052f6-c3d5-425b-bf51-08aff04dc281") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("51d33d94-bf90-4cac-82cb-a3d00e7413f6"), new Guid("0c2052f6-c3d5-425b-bf51-08aff04dc281") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c59ca3a9-7975-41fa-ac23-35ce8ed395ee"), new Guid("0c2052f6-c3d5-425b-bf51-08aff04dc281") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c59ca3a9-7975-41fa-ac23-35ce8ed395ee"), new Guid("c8ce159b-36c8-4ca1-988c-69feac103bb1") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("17c36ce2-6b5a-4de3-bb16-3d3083b70721"), new Guid("f787e458-93e0-41ef-9ba5-2f6bf8ee5a6f") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c59ca3a9-7975-41fa-ac23-35ce8ed395ee"), new Guid("f787e458-93e0-41ef-9ba5-2f6bf8ee5a6f") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("17c36ce2-6b5a-4de3-bb16-3d3083b70721"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("51d33d94-bf90-4cac-82cb-a3d00e7413f6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c59ca3a9-7975-41fa-ac23-35ce8ed395ee"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0c2052f6-c3d5-425b-bf51-08aff04dc281"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c8ce159b-36c8-4ca1-988c-69feac103bb1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f787e458-93e0-41ef-9ba5-2f6bf8ee5a6f"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { new Guid("b865f0c0-e730-49a5-8c80-9b9b4f5b8d56"), 2 },
                    { new Guid("b9042180-949b-470c-9ebb-4fb6b1b27404"), 3 },
                    { new Guid("b92bca83-5a80-4323-8c93-635e3486998c"), 0 },
                    { new Guid("c7ed4f3b-e0e3-40e1-8dda-0d5c7b201a0c"), 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "FirstName", "IsConfirmed", "IsDeleted", "LastName", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("5d360b8e-d55d-45dc-86a0-994062809dab"), new DateTime(2025, 3, 25, 9, 11, 52, 147, DateTimeKind.Utc).AddTicks(6294), null, "admin@example.com", "Admin", true, false, "User", "$2a$11$fMotE2uso.Vl/dInRwOYE.CVTYpLLW9bRv/JxwOcFuISgyclU/XKS", new DateTime(2025, 3, 25, 9, 11, 52, 147, DateTimeKind.Utc).AddTicks(6400), "admin" },
                    { new Guid("727dd265-0f1a-48f3-bce0-7fe085e92310"), new DateTime(2025, 3, 25, 9, 11, 52, 147, DateTimeKind.Utc).AddTicks(6704), null, "user2@example.com", "Jane", false, false, "Doe", "$2a$11$fP8SA4QPz5bfUeIS5ZH8LOE8VXKYY1JH6JYfIk5iCCqs6PyV6d8La", new DateTime(2025, 3, 25, 9, 11, 52, 147, DateTimeKind.Utc).AddTicks(6704), "user2" },
                    { new Guid("8f841611-a026-48a1-aee8-669d5760a45f"), new DateTime(2025, 3, 25, 9, 11, 52, 147, DateTimeKind.Utc).AddTicks(6701), null, "user1@example.com", "John", true, false, "Doe", "$2a$11$Vmq/dCwgmETEykLr31YlZez.vX2akujpnGBJoXsRFSL9n112KwOLK", new DateTime(2025, 3, 25, 9, 11, 52, 147, DateTimeKind.Utc).AddTicks(6702), "user1" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("b865f0c0-e730-49a5-8c80-9b9b4f5b8d56"), new Guid("5d360b8e-d55d-45dc-86a0-994062809dab") },
                    { new Guid("b9042180-949b-470c-9ebb-4fb6b1b27404"), new Guid("5d360b8e-d55d-45dc-86a0-994062809dab") },
                    { new Guid("c7ed4f3b-e0e3-40e1-8dda-0d5c7b201a0c"), new Guid("5d360b8e-d55d-45dc-86a0-994062809dab") },
                    { new Guid("c7ed4f3b-e0e3-40e1-8dda-0d5c7b201a0c"), new Guid("727dd265-0f1a-48f3-bce0-7fe085e92310") },
                    { new Guid("b865f0c0-e730-49a5-8c80-9b9b4f5b8d56"), new Guid("8f841611-a026-48a1-aee8-669d5760a45f") },
                    { new Guid("c7ed4f3b-e0e3-40e1-8dda-0d5c7b201a0c"), new Guid("8f841611-a026-48a1-aee8-669d5760a45f") }
                });
        }
    }
}
