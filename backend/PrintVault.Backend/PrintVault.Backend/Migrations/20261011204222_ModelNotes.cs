using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrintVault.Backend.Migrations
{
    /// <inheritdoc />
    public partial class ModelNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "PrintModels",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "PrintModels");
        }
    }
}
