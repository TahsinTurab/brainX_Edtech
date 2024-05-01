using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace brainX.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TutotialUrl",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TutotialUrl",
                table: "Contents");
        }
    }
}
