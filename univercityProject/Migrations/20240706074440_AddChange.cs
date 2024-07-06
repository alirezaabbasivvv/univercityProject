using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace univercityProject.Migrations
{
    public partial class AddChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TeacherAccepted",
                table: "Requests",
                newName: "ManagerAccepted");

            migrationBuilder.RenameColumn(
                name: "OperatorIsAccepted",
                table: "Requests",
                newName: "CEOIsAccepted");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "ManagerAccepted",
                table: "Requests",
                newName: "TeacherAccepted");

            migrationBuilder.RenameColumn(
                name: "CEOIsAccepted",
                table: "Requests",
                newName: "OperatorIsAccepted");
        }
    }
}
