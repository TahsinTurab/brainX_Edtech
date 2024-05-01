using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace brainX.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTutotial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Course",
                table: "Tutorials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Course",
                table: "Tutorials");
        }
    }
}
