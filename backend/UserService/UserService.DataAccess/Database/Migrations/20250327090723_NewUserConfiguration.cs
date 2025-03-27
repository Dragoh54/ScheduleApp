using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserService.DataAccess.Database.Migrations
{
    /// <inheritdoc />
    public partial class NewUserConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    { new Guid("0c58d927-a59c-4982-b635-5b0b87c57b25"), 3 },
                    { new Guid("6ca81f82-533a-40c5-9f47-b6f970ad5f0f"), 1 },
                    { new Guid("7ff18186-3b3d-4d87-8728-66261e65c7ce"), 0 },
                    { new Guid("abc1cb8d-96c5-4d2e-ae46-71df55c9953a"), 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "FirstName", "IsConfirmed", "IsDeleted", "LastName", "PasswordHash", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("a1202621-c0a9-49a4-b294-eb2236e4ab4a"), new DateTime(2025, 3, 27, 9, 7, 22, 985, DateTimeKind.Utc).AddTicks(204), null, "user1@example.com", "John", true, false, "Doe", "$2a$11$Vmq/dCwgmETEykLr31YlZez.vX2akujpnGBJoXsRFSL9n112KwOLK", new DateTime(2025, 3, 27, 9, 7, 22, 985, DateTimeKind.Utc).AddTicks(204), "user1" },
                    { new Guid("a7b34380-830f-4cbf-a758-16fe3e92e7e0"), new DateTime(2025, 3, 27, 9, 7, 22, 984, DateTimeKind.Utc).AddTicks(9771), null, "admin@example.com", "Admin", true, false, "User", "$2a$11$fMotE2uso.Vl/dInRwOYE.CVTYpLLW9bRv/JxwOcFuISgyclU/XKS", new DateTime(2025, 3, 27, 9, 7, 22, 984, DateTimeKind.Utc).AddTicks(9888), "admin" },
                    { new Guid("d01a1ce4-12c6-4acc-9bb1-50f85164acfe"), new DateTime(2025, 3, 27, 9, 7, 22, 985, DateTimeKind.Utc).AddTicks(206), null, "user2@example.com", "Jane", false, false, "Doe", "$2a$11$fP8SA4QPz5bfUeIS5ZH8LOE8VXKYY1JH6JYfIk5iCCqs6PyV6d8La", new DateTime(2025, 3, 27, 9, 7, 22, 985, DateTimeKind.Utc).AddTicks(206), "user2" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("6ca81f82-533a-40c5-9f47-b6f970ad5f0f"), new Guid("a1202621-c0a9-49a4-b294-eb2236e4ab4a") },
                    { new Guid("abc1cb8d-96c5-4d2e-ae46-71df55c9953a"), new Guid("a1202621-c0a9-49a4-b294-eb2236e4ab4a") },
                    { new Guid("0c58d927-a59c-4982-b635-5b0b87c57b25"), new Guid("a7b34380-830f-4cbf-a758-16fe3e92e7e0") },
                    { new Guid("6ca81f82-533a-40c5-9f47-b6f970ad5f0f"), new Guid("a7b34380-830f-4cbf-a758-16fe3e92e7e0") },
                    { new Guid("abc1cb8d-96c5-4d2e-ae46-71df55c9953a"), new Guid("a7b34380-830f-4cbf-a758-16fe3e92e7e0") },
                    { new Guid("6ca81f82-533a-40c5-9f47-b6f970ad5f0f"), new Guid("d01a1ce4-12c6-4acc-9bb1-50f85164acfe") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7ff18186-3b3d-4d87-8728-66261e65c7ce"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("6ca81f82-533a-40c5-9f47-b6f970ad5f0f"), new Guid("a1202621-c0a9-49a4-b294-eb2236e4ab4a") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("abc1cb8d-96c5-4d2e-ae46-71df55c9953a"), new Guid("a1202621-c0a9-49a4-b294-eb2236e4ab4a") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("0c58d927-a59c-4982-b635-5b0b87c57b25"), new Guid("a7b34380-830f-4cbf-a758-16fe3e92e7e0") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("6ca81f82-533a-40c5-9f47-b6f970ad5f0f"), new Guid("a7b34380-830f-4cbf-a758-16fe3e92e7e0") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("abc1cb8d-96c5-4d2e-ae46-71df55c9953a"), new Guid("a7b34380-830f-4cbf-a758-16fe3e92e7e0") });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("6ca81f82-533a-40c5-9f47-b6f970ad5f0f"), new Guid("d01a1ce4-12c6-4acc-9bb1-50f85164acfe") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0c58d927-a59c-4982-b635-5b0b87c57b25"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6ca81f82-533a-40c5-9f47-b6f970ad5f0f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("abc1cb8d-96c5-4d2e-ae46-71df55c9953a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1202621-c0a9-49a4-b294-eb2236e4ab4a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a7b34380-830f-4cbf-a758-16fe3e92e7e0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d01a1ce4-12c6-4acc-9bb1-50f85164acfe"));

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
        }
    }
}
