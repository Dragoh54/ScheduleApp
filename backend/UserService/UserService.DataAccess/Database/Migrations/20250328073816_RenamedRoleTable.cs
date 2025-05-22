using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.DataAccess.Database.Migrations
{
    /// <inheritdoc />
    public partial class RenamedRoleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8d95b8d6-f349-4ddb-9a09-0fc416d38132"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("69eedaee-d2e6-4493-a4ee-8d2d7eb9310d"), new Guid("64d71b71-6617-47f4-83bd-db4d97928ee5") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7cd45859-e536-4e85-af8b-95be916f6c6f"), new Guid("64d71b71-6617-47f4-83bd-db4d97928ee5") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("90fb01b5-4a28-4b05-bf15-61b1b176b314"), new Guid("64d71b71-6617-47f4-83bd-db4d97928ee5") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7cd45859-e536-4e85-af8b-95be916f6c6f"), new Guid("67b8e8ef-1518-4560-9748-0e9dffb939d8") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7cd45859-e536-4e85-af8b-95be916f6c6f"), new Guid("e947d533-1dfe-465b-bc2c-f1dae8410063") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("90fb01b5-4a28-4b05-bf15-61b1b176b314"), new Guid("e947d533-1dfe-465b-bc2c-f1dae8410063") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("69eedaee-d2e6-4493-a4ee-8d2d7eb9310d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7cd45859-e536-4e85-af8b-95be916f6c6f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("90fb01b5-4a28-4b05-bf15-61b1b176b314"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("64d71b71-6617-47f4-83bd-db4d97928ee5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("67b8e8ef-1518-4560-9748-0e9dffb939d8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e947d533-1dfe-465b-bc2c-f1dae8410063"));

            migrationBuilder.RenameColumn(
                name: "RolesName",
                table: "Roles",
                newName: "RoleName");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "Roles",
                newName: "RolesName");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RolesName" },
                values: new object[,]
                {
                    { new Guid("69eedaee-d2e6-4493-a4ee-8d2d7eb9310d"), 3 },
                    { new Guid("7cd45859-e536-4e85-af8b-95be916f6c6f"), 1 },
                    { new Guid("8d95b8d6-f349-4ddb-9a09-0fc416d38132"), 0 },
                    { new Guid("90fb01b5-4a28-4b05-bf15-61b1b176b314"), 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "FirstName", "IsConfirmed", "IsDeleted", "LastName", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("64d71b71-6617-47f4-83bd-db4d97928ee5"), new DateTime(2025, 3, 28, 7, 35, 27, 308, DateTimeKind.Utc).AddTicks(8711), null, "admin@example.com", "Admin", true, false, "User", "$2a$11$fMotE2uso.Vl/dInRwOYE.CVTYpLLW9bRv/JxwOcFuISgyclU/XKS", new DateTime(2025, 3, 28, 7, 35, 27, 308, DateTimeKind.Utc).AddTicks(8824), "admin" },
                    { new Guid("67b8e8ef-1518-4560-9748-0e9dffb939d8"), new DateTime(2025, 3, 28, 7, 35, 27, 308, DateTimeKind.Utc).AddTicks(9126), null, "user2@example.com", "Jane", false, false, "Doe", "$2a$11$fP8SA4QPz5bfUeIS5ZH8LOE8VXKYY1JH6JYfIk5iCCqs6PyV6d8La", new DateTime(2025, 3, 28, 7, 35, 27, 308, DateTimeKind.Utc).AddTicks(9126), "user2" },
                    { new Guid("e947d533-1dfe-465b-bc2c-f1dae8410063"), new DateTime(2025, 3, 28, 7, 35, 27, 308, DateTimeKind.Utc).AddTicks(9115), null, "user1@example.com", "John", true, false, "Doe", "$2a$11$Vmq/dCwgmETEykLr31YlZez.vX2akujpnGBJoXsRFSL9n112KwOLK", new DateTime(2025, 3, 28, 7, 35, 27, 308, DateTimeKind.Utc).AddTicks(9115), "user1" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("69eedaee-d2e6-4493-a4ee-8d2d7eb9310d"), new Guid("64d71b71-6617-47f4-83bd-db4d97928ee5") },
                    { new Guid("7cd45859-e536-4e85-af8b-95be916f6c6f"), new Guid("64d71b71-6617-47f4-83bd-db4d97928ee5") },
                    { new Guid("90fb01b5-4a28-4b05-bf15-61b1b176b314"), new Guid("64d71b71-6617-47f4-83bd-db4d97928ee5") },
                    { new Guid("7cd45859-e536-4e85-af8b-95be916f6c6f"), new Guid("67b8e8ef-1518-4560-9748-0e9dffb939d8") },
                    { new Guid("7cd45859-e536-4e85-af8b-95be916f6c6f"), new Guid("e947d533-1dfe-465b-bc2c-f1dae8410063") },
                    { new Guid("90fb01b5-4a28-4b05-bf15-61b1b176b314"), new Guid("e947d533-1dfe-465b-bc2c-f1dae8410063") }
                });
        }
    }
}
