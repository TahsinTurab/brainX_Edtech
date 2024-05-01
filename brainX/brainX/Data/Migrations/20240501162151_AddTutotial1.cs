using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace brainX.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTutotial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tutorials",
                newName: "Topic");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Topic",
                table: "Tutorials",
                newName: "Name");
        }
    }
}
