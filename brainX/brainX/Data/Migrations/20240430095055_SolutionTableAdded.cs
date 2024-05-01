using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace brainX.Data.Migrations
{
    /// <inheritdoc />
    public partial class SolutionTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Solutions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Attemp = table.Column<int>(type: "int", nullable: false),
                    Solution1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Solution2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Solution3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    verdict = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solutions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Solutions");
        }
    }
}
