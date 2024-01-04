using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameManagerService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteColumnInPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Players");
        }
    }
}
