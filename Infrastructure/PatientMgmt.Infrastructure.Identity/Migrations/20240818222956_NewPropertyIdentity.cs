using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientMgmt.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class NewPropertyIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserType",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserType",
                schema: "Identity",
                table: "User");
        }
    }
}
