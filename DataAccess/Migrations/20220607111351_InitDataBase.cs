using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class InitDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    company_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.company_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_auth0_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    user_first_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    _user_last_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    user_email = table.Column<string>(type: "varchar(max)", nullable: true),
                    _user_company_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => new { x.user_id, x.user_auth0_id });
                    table.ForeignKey(
                        name: "FK_users_companies__user_company_id",
                        column: x => x._user_company_id,
                        principalTable: "companies",
                        principalColumn: "company_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_companies_company_id",
                table: "companies",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_users__user_company_id",
                table: "users",
                column: "_user_company_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_user_auth0_id",
                table: "users",
                column: "user_auth0_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_user_id",
                table: "users",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "companies");
        }
    }
}
