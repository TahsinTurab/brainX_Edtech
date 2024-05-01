using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace brainX.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTestTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizes_Tests_TestId",
                table: "Quizes");

            migrationBuilder.DropIndex(
                name: "IX_Quizes_TestId",
                table: "Quizes");

            migrationBuilder.DropColumn(
                name: "PracticalTask",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "QuizTime",
                table: "Tests");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Tests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PracticalTask1",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PracticalTask2",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PracticalTask3",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "PracticalTask1",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "PracticalTask2",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "PracticalTask3",
                table: "Tests");

            migrationBuilder.AddColumn<string>(
                name: "PracticalTask",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "QuizTime",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Quizes_TestId",
                table: "Quizes",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizes_Tests_TestId",
                table: "Quizes",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
