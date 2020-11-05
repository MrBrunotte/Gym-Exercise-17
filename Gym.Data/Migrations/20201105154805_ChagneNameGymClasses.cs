using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gym.Data.Migrations
{
    public partial class ChagneNameGymClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserGymClass_GymClass_GymClassId",
                table: "ApplicationUserGymClass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GymClass",
                table: "GymClass");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "GymClass");

            migrationBuilder.RenameTable(
                name: "GymClass",
                newName: "GymClasses");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "GymClasses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_GymClasses",
                table: "GymClasses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserGymClass_GymClasses_GymClassId",
                table: "ApplicationUserGymClass",
                column: "GymClassId",
                principalTable: "GymClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserGymClass_GymClasses_GymClassId",
                table: "ApplicationUserGymClass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GymClasses",
                table: "GymClasses");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "GymClasses");

            migrationBuilder.RenameTable(
                name: "GymClasses",
                newName: "GymClass");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "GymClass",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_GymClass",
                table: "GymClass",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserGymClass_GymClass_GymClassId",
                table: "ApplicationUserGymClass",
                column: "GymClassId",
                principalTable: "GymClass",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
