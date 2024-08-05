﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientMgmt.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdPropertyPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Patients");
        }
    }
}
