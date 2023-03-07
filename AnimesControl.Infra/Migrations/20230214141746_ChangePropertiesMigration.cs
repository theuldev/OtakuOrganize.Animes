using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AnimesControl.Infra.Migrations
{
    public partial class ChangePropertiesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "tb_customers");

            migrationBuilder.DropColumn(
                name: "LastLogin",
                table: "tb_customers");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "tb_customers",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "tb_customers",
                newName: "LastName");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "tb_customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "tb_customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    LastLogin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_customers_UserId",
                table: "tb_customers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_customers_Users_UserId",
                table: "tb_customers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_customers_Users_UserId",
                table: "tb_customers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_tb_customers_UserId",
                table: "tb_customers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "tb_customers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tb_customers");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "tb_customers",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "tb_customers",
                newName: "Password");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "tb_customers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogin",
                table: "tb_customers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
