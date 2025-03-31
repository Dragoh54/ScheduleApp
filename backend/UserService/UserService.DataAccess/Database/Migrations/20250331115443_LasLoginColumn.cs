using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.DataAccess.Database.Migrations
{
    /// <inheritdoc />
    public partial class LasLoginColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c9c04b56-2b3b-4757-b26d-d67b89850011"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("60a966ca-ea80-44e7-bd36-1ac541986bb7"), new Guid("4d4be795-eec7-4185-a02a-0f99e9c6f6db") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("60a966ca-ea80-44e7-bd36-1ac541986bb7"), new Guid("82cae4d7-21aa-4597-a9e6-0718abc7a049") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c6044bf6-c950-4ba0-b99e-3d49b371b35a"), new Guid("82cae4d7-21aa-4597-a9e6-0718abc7a049") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("4ae70275-7715-4e64-b549-8f5ad76bb6fb"), new Guid("863af892-cd9d-414b-b985-a218e0762949") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("60a966ca-ea80-44e7-bd36-1ac541986bb7"), new Guid("863af892-cd9d-414b-b985-a218e0762949") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c6044bf6-c950-4ba0-b99e-3d49b371b35a"), new Guid("863af892-cd9d-414b-b985-a218e0762949") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4ae70275-7715-4e64-b549-8f5ad76bb6fb"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("60a966ca-ea80-44e7-bd36-1ac541986bb7"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c6044bf6-c950-4ba0-b99e-3d49b371b35a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4d4be795-eec7-4185-a02a-0f99e9c6f6db"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("82cae4d7-21aa-4597-a9e6-0718abc7a049"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("863af892-cd9d-414b-b985-a218e0762949"));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { new Guid("2ef4db94-34d6-4264-aebc-eeb0d2908a97"), 1 },
                    { new Guid("af7175c3-92c2-484b-81a6-547cfeb18ec8"), 0 },
                    { new Guid("d089bf57-5915-418c-bbbf-f19e453aee79"), 2 },
                    { new Guid("da138ce8-397b-4c68-98eb-87c211c12883"), 3 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "FirstName", "IsConfirmed", "IsDeleted", "LastLoginAt", "LastName", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("1ba32b68-fb3a-499e-a784-8cd1bba940da"), new DateTime(2025, 3, 31, 11, 54, 43, 385, DateTimeKind.Utc).AddTicks(7331), null, "admin@example.com", "Admin", true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", "$2a$11$fMotE2uso.Vl/dInRwOYE.CVTYpLLW9bRv/JxwOcFuISgyclU/XKS", new DateTime(2025, 3, 31, 11, 54, 43, 385, DateTimeKind.Utc).AddTicks(7439), "admin" },
                    { new Guid("76faed9e-cca9-4e5b-98bf-837fa3bdd501"), new DateTime(2025, 3, 31, 11, 54, 43, 385, DateTimeKind.Utc).AddTicks(7728), null, "user2@example.com", "Jane", false, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doe", "$2a$11$fP8SA4QPz5bfUeIS5ZH8LOE8VXKYY1JH6JYfIk5iCCqs6PyV6d8La", new DateTime(2025, 3, 31, 11, 54, 43, 385, DateTimeKind.Utc).AddTicks(7729), "user2" },
                    { new Guid("d724d7e7-b0df-47a7-989c-c373eff365a8"), new DateTime(2025, 3, 31, 11, 54, 43, 385, DateTimeKind.Utc).AddTicks(7726), null, "user1@example.com", "John", true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doe", "$2a$11$Vmq/dCwgmETEykLr31YlZez.vX2akujpnGBJoXsRFSL9n112KwOLK", new DateTime(2025, 3, 31, 11, 54, 43, 385, DateTimeKind.Utc).AddTicks(7726), "user1" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("2ef4db94-34d6-4264-aebc-eeb0d2908a97"), new Guid("1ba32b68-fb3a-499e-a784-8cd1bba940da") },
                    { new Guid("d089bf57-5915-418c-bbbf-f19e453aee79"), new Guid("1ba32b68-fb3a-499e-a784-8cd1bba940da") },
                    { new Guid("da138ce8-397b-4c68-98eb-87c211c12883"), new Guid("1ba32b68-fb3a-499e-a784-8cd1bba940da") },
                    { new Guid("2ef4db94-34d6-4264-aebc-eeb0d2908a97"), new Guid("76faed9e-cca9-4e5b-98bf-837fa3bdd501") },
                    { new Guid("2ef4db94-34d6-4264-aebc-eeb0d2908a97"), new Guid("d724d7e7-b0df-47a7-989c-c373eff365a8") },
                    { new Guid("d089bf57-5915-418c-bbbf-f19e453aee79"), new Guid("d724d7e7-b0df-47a7-989c-c373eff365a8") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("af7175c3-92c2-484b-81a6-547cfeb18ec8"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("2ef4db94-34d6-4264-aebc-eeb0d2908a97"), new Guid("1ba32b68-fb3a-499e-a784-8cd1bba940da") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d089bf57-5915-418c-bbbf-f19e453aee79"), new Guid("1ba32b68-fb3a-499e-a784-8cd1bba940da") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("da138ce8-397b-4c68-98eb-87c211c12883"), new Guid("1ba32b68-fb3a-499e-a784-8cd1bba940da") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("2ef4db94-34d6-4264-aebc-eeb0d2908a97"), new Guid("76faed9e-cca9-4e5b-98bf-837fa3bdd501") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("2ef4db94-34d6-4264-aebc-eeb0d2908a97"), new Guid("d724d7e7-b0df-47a7-989c-c373eff365a8") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d089bf57-5915-418c-bbbf-f19e453aee79"), new Guid("d724d7e7-b0df-47a7-989c-c373eff365a8") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2ef4db94-34d6-4264-aebc-eeb0d2908a97"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d089bf57-5915-418c-bbbf-f19e453aee79"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("da138ce8-397b-4c68-98eb-87c211c12883"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1ba32b68-fb3a-499e-a784-8cd1bba940da"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("76faed9e-cca9-4e5b-98bf-837fa3bdd501"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d724d7e7-b0df-47a7-989c-c373eff365a8"));

            migrationBuilder.DropColumn(
                name: "LastLoginAt",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { new Guid("4ae70275-7715-4e64-b549-8f5ad76bb6fb"), 3 },
                    { new Guid("60a966ca-ea80-44e7-bd36-1ac541986bb7"), 1 },
                    { new Guid("c6044bf6-c950-4ba0-b99e-3d49b371b35a"), 2 },
                    { new Guid("c9c04b56-2b3b-4757-b26d-d67b89850011"), 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "FirstName", "IsConfirmed", "IsDeleted", "LastName", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("4d4be795-eec7-4185-a02a-0f99e9c6f6db"), new DateTime(2025, 3, 31, 7, 16, 9, 299, DateTimeKind.Utc).AddTicks(4068), null, "user2@example.com", "Jane", false, false, "Doe", "$2a$11$fP8SA4QPz5bfUeIS5ZH8LOE8VXKYY1JH6JYfIk5iCCqs6PyV6d8La", new DateTime(2025, 3, 31, 7, 16, 9, 299, DateTimeKind.Utc).AddTicks(4068), "user2" },
                    { new Guid("82cae4d7-21aa-4597-a9e6-0718abc7a049"), new DateTime(2025, 3, 31, 7, 16, 9, 299, DateTimeKind.Utc).AddTicks(4066), null, "user1@example.com", "John", true, false, "Doe", "$2a$11$Vmq/dCwgmETEykLr31YlZez.vX2akujpnGBJoXsRFSL9n112KwOLK", new DateTime(2025, 3, 31, 7, 16, 9, 299, DateTimeKind.Utc).AddTicks(4066), "user1" },
                    { new Guid("863af892-cd9d-414b-b985-a218e0762949"), new DateTime(2025, 3, 31, 7, 16, 9, 299, DateTimeKind.Utc).AddTicks(3666), null, "admin@example.com", "Admin", true, false, "User", "$2a$11$fMotE2uso.Vl/dInRwOYE.CVTYpLLW9bRv/JxwOcFuISgyclU/XKS", new DateTime(2025, 3, 31, 7, 16, 9, 299, DateTimeKind.Utc).AddTicks(3779), "admin" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("60a966ca-ea80-44e7-bd36-1ac541986bb7"), new Guid("4d4be795-eec7-4185-a02a-0f99e9c6f6db") },
                    { new Guid("60a966ca-ea80-44e7-bd36-1ac541986bb7"), new Guid("82cae4d7-21aa-4597-a9e6-0718abc7a049") },
                    { new Guid("c6044bf6-c950-4ba0-b99e-3d49b371b35a"), new Guid("82cae4d7-21aa-4597-a9e6-0718abc7a049") },
                    { new Guid("4ae70275-7715-4e64-b549-8f5ad76bb6fb"), new Guid("863af892-cd9d-414b-b985-a218e0762949") },
                    { new Guid("60a966ca-ea80-44e7-bd36-1ac541986bb7"), new Guid("863af892-cd9d-414b-b985-a218e0762949") },
                    { new Guid("c6044bf6-c950-4ba0-b99e-3d49b371b35a"), new Guid("863af892-cd9d-414b-b985-a218e0762949") }
                });
        }
    }
}
