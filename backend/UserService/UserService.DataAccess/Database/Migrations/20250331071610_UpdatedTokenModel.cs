using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.DataAccess.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTokenModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d372d466-79f6-421a-a7b7-54131879b0ab"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("89d4c108-fda5-4bc3-9488-59fc6c4efbe4"), new Guid("0892b660-7edb-412e-b566-3d4d3889c7db") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c2da42d2-87f6-44b8-9a7e-14aa17b4a778"), new Guid("0892b660-7edb-412e-b566-3d4d3889c7db") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c2da42d2-87f6-44b8-9a7e-14aa17b4a778"), new Guid("37d94130-e8b0-471c-af66-6ad1a1856b00") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7784bd26-1569-428c-aadf-a7ca77ebab48"), new Guid("5203f229-bbd6-47ef-8020-40467e9841bc") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("89d4c108-fda5-4bc3-9488-59fc6c4efbe4"), new Guid("5203f229-bbd6-47ef-8020-40467e9841bc") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c2da42d2-87f6-44b8-9a7e-14aa17b4a778"), new Guid("5203f229-bbd6-47ef-8020-40467e9841bc") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7784bd26-1569-428c-aadf-a7ca77ebab48"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("89d4c108-fda5-4bc3-9488-59fc6c4efbe4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c2da42d2-87f6-44b8-9a7e-14aa17b4a778"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0892b660-7edb-412e-b566-3d4d3889c7db"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("37d94130-e8b0-471c-af66-6ad1a1856b00"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5203f229-bbd6-47ef-8020-40467e9841bc"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Tokens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Tokens");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { new Guid("7784bd26-1569-428c-aadf-a7ca77ebab48"), 3 },
                    { new Guid("89d4c108-fda5-4bc3-9488-59fc6c4efbe4"), 2 },
                    { new Guid("c2da42d2-87f6-44b8-9a7e-14aa17b4a778"), 1 },
                    { new Guid("d372d466-79f6-421a-a7b7-54131879b0ab"), 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "FirstName", "IsConfirmed", "IsDeleted", "LastName", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("0892b660-7edb-412e-b566-3d4d3889c7db"), new DateTime(2025, 3, 28, 7, 38, 15, 710, DateTimeKind.Utc).AddTicks(3951), null, "user1@example.com", "John", true, false, "Doe", "$2a$11$Vmq/dCwgmETEykLr31YlZez.vX2akujpnGBJoXsRFSL9n112KwOLK", new DateTime(2025, 3, 28, 7, 38, 15, 710, DateTimeKind.Utc).AddTicks(3952), "user1" },
                    { new Guid("37d94130-e8b0-471c-af66-6ad1a1856b00"), new DateTime(2025, 3, 28, 7, 38, 15, 710, DateTimeKind.Utc).AddTicks(3974), null, "user2@example.com", "Jane", false, false, "Doe", "$2a$11$fP8SA4QPz5bfUeIS5ZH8LOE8VXKYY1JH6JYfIk5iCCqs6PyV6d8La", new DateTime(2025, 3, 28, 7, 38, 15, 710, DateTimeKind.Utc).AddTicks(3974), "user2" },
                    { new Guid("5203f229-bbd6-47ef-8020-40467e9841bc"), new DateTime(2025, 3, 28, 7, 38, 15, 710, DateTimeKind.Utc).AddTicks(3497), null, "admin@example.com", "Admin", true, false, "User", "$2a$11$fMotE2uso.Vl/dInRwOYE.CVTYpLLW9bRv/JxwOcFuISgyclU/XKS", new DateTime(2025, 3, 28, 7, 38, 15, 710, DateTimeKind.Utc).AddTicks(3620), "admin" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("89d4c108-fda5-4bc3-9488-59fc6c4efbe4"), new Guid("0892b660-7edb-412e-b566-3d4d3889c7db") },
                    { new Guid("c2da42d2-87f6-44b8-9a7e-14aa17b4a778"), new Guid("0892b660-7edb-412e-b566-3d4d3889c7db") },
                    { new Guid("c2da42d2-87f6-44b8-9a7e-14aa17b4a778"), new Guid("37d94130-e8b0-471c-af66-6ad1a1856b00") },
                    { new Guid("7784bd26-1569-428c-aadf-a7ca77ebab48"), new Guid("5203f229-bbd6-47ef-8020-40467e9841bc") },
                    { new Guid("89d4c108-fda5-4bc3-9488-59fc6c4efbe4"), new Guid("5203f229-bbd6-47ef-8020-40467e9841bc") },
                    { new Guid("c2da42d2-87f6-44b8-9a7e-14aa17b4a778"), new Guid("5203f229-bbd6-47ef-8020-40467e9841bc") }
                });
        }
    }
}
