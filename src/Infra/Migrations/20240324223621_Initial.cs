using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntryTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TotalHours = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EmployeeId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeRecord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Intervals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IntervalType = table.Column<int>(type: "integer", nullable: false),
                    EntryTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExitTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TimeRecordId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intervals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intervals_TimeRecord_TimeRecordId",
                        column: x => x.TimeRecordId,
                        principalTable: "TimeRecord",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Intervals_TimeRecordId",
                table: "Intervals",
                column: "TimeRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Intervals");

            migrationBuilder.DropTable(
                name: "TimeRecord");
        }
    }
}
