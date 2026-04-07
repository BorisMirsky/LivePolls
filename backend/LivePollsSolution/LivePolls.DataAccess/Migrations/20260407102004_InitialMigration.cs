using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivePolls.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "polls",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    creatorid = table.Column<Guid>(type: "TEXT", nullable: false),
                    createdat = table.Column<DateTime>(type: "TEXT", nullable: true),
                    enddate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    isactive = table.Column<bool>(type: "INTEGER", nullable: true),
                    question = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_polls", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false, defaultValueSql: "NEWID()"),
                    name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    login = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    password = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "polloptions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    pollid = table.Column<Guid>(type: "TEXT", nullable: false),
                    text = table.Column<string>(type: "TEXT", nullable: false),
                    order = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_polloptions", x => x.id);
                    table.ForeignKey(
                        name: "FK_polloptions_polls_pollid",
                        column: x => x.pollid,
                        principalTable: "polls",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userconnections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false, defaultValueSql: "NEWID()"),
                    userid = table.Column<Guid>(type: "TEXT", nullable: false),
                    connectionid = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    pollid = table.Column<Guid>(type: "TEXT", nullable: true),
                    connectedat = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    lastactivity = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userconnections", x => x.id);
                    table.ForeignKey(
                        name: "FK_userconnections_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "votes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false, defaultValueSql: "NEWID()"),
                    pollid = table.Column<Guid>(type: "TEXT", nullable: false),
                    optionid = table.Column<Guid>(type: "TEXT", nullable: false),
                    userid = table.Column<Guid>(type: "TEXT", nullable: false),
                    votedat = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_votes", x => x.id);
                    table.ForeignKey(
                        name: "FK_votes_polloptions_optionid",
                        column: x => x.optionid,
                        principalTable: "polloptions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_votes_polls_pollid",
                        column: x => x.pollid,
                        principalTable: "polls",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_votes_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_polloptions_pollid",
                table: "polloptions",
                column: "pollid");

            migrationBuilder.CreateIndex(
                name: "IX_userconnections_userid",
                table: "userconnections",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_votes_optionid",
                table: "votes",
                column: "optionid");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_PollId_UserId",
                table: "votes",
                columns: new[] { "pollid", "userid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_votes_userid",
                table: "votes",
                column: "userid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userconnections");

            migrationBuilder.DropTable(
                name: "votes");

            migrationBuilder.DropTable(
                name: "polloptions");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "polls");
        }
    }
}
