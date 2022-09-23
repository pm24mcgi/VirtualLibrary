using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtualLibrary.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "Author", "CheckedOut", "Description", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, "Author1", false, "This is a lovely book.", new DateTime(2008, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Book1" },
                    { 2, "Author2", false, "This is a lovely book.", new DateTime(2010, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Book2" },
                    { 3, "Author3", false, "This is a lovely book.", new DateTime(2008, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Book3" },
                    { 4, "Author4", false, "This is a lovely book.", new DateTime(2011, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Book4" },
                    { 5, "Author5", false, "This is a lovely book.", new DateTime(1992, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Book5" },
                    { 6, "Author6", false, "This is a lovely book.", new DateTime(1964, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Book6" },
                    { 7, "Author7", false, "This is a lovely book.", new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Book7" },
                    { 8, "Author8", false, "This is a lovely book.", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Book8" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
