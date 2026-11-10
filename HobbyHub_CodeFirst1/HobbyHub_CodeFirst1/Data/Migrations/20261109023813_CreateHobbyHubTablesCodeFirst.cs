using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyHub_CodeFirst1.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateHobbyHubTablesCodeFirst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slug = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IconKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TintColor = table.Column<string>(type: "char(7)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.UniqueConstraint("AK_Categories_Name", x => x.Name);
                    table.UniqueConstraint("AK_Categories_Slug", x => x.Slug);
                    table.CheckConstraint("CK_Categories_SortOrder", "SortOrder >= 0");
                });

            migrationBuilder.CreateTable(
                name: "Hobbies",
                columns: table => new
                {
                    HobbyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Definition = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    StarterStep = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    StartingRequirements = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    IconKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CanStartAtHome = table.Column<bool>(type: "bit", nullable: false),
                    NeedsOtherPeople = table.Column<bool>(type: "bit", nullable: false),
                    IsCompetitive = table.Column<bool>(type: "bit", nullable: false),
                    NeedsLongFirstSession = table.Column<bool>(type: "bit", nullable: false),
                    SupportsRegularPractice = table.Column<bool>(type: "bit", nullable: false),
                    SupportsOccasionalPractice = table.Column<bool>(type: "bit", nullable: false),
                    SupportsOutingPractice = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hobbies", x => x.HobbyId);
                    table.UniqueConstraint("AK_Hobbies_Name", x => x.Name);
                    table.UniqueConstraint("AK_Hobbies_Slug", x => x.Slug);
                    table.CheckConstraint("CK_Hobbies_Name", "Name <> ''");
                    table.ForeignKey(
                        name: "FK_Hobbies_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Communities",
                columns: table => new
                {
                    CommunityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HobbyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Status = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communities", x => x.CommunityId);
                    table.UniqueConstraint("AK_Communities_HobbyId", x => x.HobbyId);
                    table.CheckConstraint("CK_Communities_Status", "Status IN ('Active','Archived')");
                    table.ForeignKey(
                        name: "FK_Communities_Hobbies_HobbyId",
                        column: x => x.HobbyId,
                        principalTable: "Hobbies",
                        principalColumn: "HobbyId");
                });

            migrationBuilder.CreateTable(
                name: "IdeaTemplates",
                columns: table => new
                {
                    TemplateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HobbyId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ExperienceLevel = table.Column<string>(type: "varchar(20)", nullable: true),
                    SuggestedMinutes = table.Column<short>(type: "smallint", nullable: true),
                    PlaceText = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeaTemplates", x => x.TemplateId);
                    table.CheckConstraint("CK_IdeaTemplates_SuggestedMinutes", "SuggestedMinutes IS NULL OR SuggestedMinutes BETWEEN 5 AND 480");
                    table.ForeignKey(
                        name: "FK_IdeaTemplates_Hobbies_HobbyId",
                        column: x => x.HobbyId,
                        principalTable: "Hobbies",
                        principalColumn: "HobbyId");
                });

            migrationBuilder.CreateTable(
                name: "IdeaTemplateSteps",
                columns: table => new
                {
                    TemplateStepId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdeaTemplateSteps", x => x.TemplateStepId);
                    table.UniqueConstraint("AK_IdeaTemplateSteps_TemplateId_SortOrder", x => new { x.TemplateId, x.SortOrder });
                    table.CheckConstraint("CK_IdeaTemplateSteps_SortOrder", "SortOrder >= 0");
                    table.ForeignKey(
                        name: "FK_IdeaTemplateSteps_IdeaTemplates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "IdeaTemplates",
                        principalColumn: "TemplateId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hobbies_CategoryId",
                table: "Hobbies",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IdeaTemplates_HobbyId",
                table: "IdeaTemplates",
                column: "HobbyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Communities");

            migrationBuilder.DropTable(
                name: "IdeaTemplateSteps");

            migrationBuilder.DropTable(
                name: "IdeaTemplates");

            migrationBuilder.DropTable(
                name: "Hobbies");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
