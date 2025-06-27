using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication_SRPFIQ.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BirthPlaces",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirthPlaces", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MaternalExperiencesThemes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsPrenatal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaternalExperiencesThemes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MedicalTransferReason",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalTransferReason", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireDataSources",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireDataSources", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Questionnaires",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaires", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ResourceCategories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ResourceCities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceCities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MustChangePassword = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireDataSourceOptions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdQuestionnaireDataSource = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    DisplayText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireDataSourceOptions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireDataSourceOptions_QuestionnaireDataSources_IdQuestionnaireDataSource",
                        column: x => x.IdQuestionnaireDataSource,
                        principalTable: "QuestionnaireDataSources",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireQuestions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdQuestionnaire = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ShortTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Instructions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IdMainDataType = table.Column<int>(type: "int", nullable: false),
                    IdMainDataSource = table.Column<int>(type: "int", nullable: true),
                    IdSubDataType = table.Column<int>(type: "int", nullable: true),
                    IdSubDataSource = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireQuestions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireQuestions_QuestionnaireDataSources_IdMainDataSource",
                        column: x => x.IdMainDataSource,
                        principalTable: "QuestionnaireDataSources",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_QuestionnaireQuestions_Questionnaires_IdQuestionnaire",
                        column: x => x.IdQuestionnaire,
                        principalTable: "Questionnaires",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdResourceCity = table.Column<int>(type: "int", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusNearBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Resources_ResourceCities_IdResourceCity",
                        column: x => x.IdResourceCity,
                        principalTable: "ResourceCities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FolioNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MedicalCoverage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ReceivedRequestAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Adresse = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EstimatedDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NbPregnancy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsMonoparental = table.Column<bool>(type: "bit", nullable: false),
                    SpokenLanguage = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImmigrationStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NbChilds = table.Column<int>(type: "int", nullable: true),
                    ChildsAge = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IdUserClosedBy = table.Column<int>(type: "int", nullable: true),
                    ClosedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Requests_Users_IdUserClosedBy",
                        column: x => x.IdUserClosedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUserRole = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserPermissions_UserRoles_IdUserRole",
                        column: x => x.IdUserRole,
                        principalTable: "UserRoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceBusinessHours",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdResource = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    OpeningTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ClosingTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceBusinessHours", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ResourceBusinessHours_Resources_IdResource",
                        column: x => x.IdResource,
                        principalTable: "Resources",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resources_ResourceCategories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdResourceCategory = table.Column<int>(type: "int", nullable: false),
                    IdResource = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources_ResourceCategories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Resources_ResourceCategories_ResourceCategories_IdResourceCategory",
                        column: x => x.IdResourceCategory,
                        principalTable: "ResourceCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resources_ResourceCategories_Resources_IdResource",
                        column: x => x.IdResource,
                        principalTable: "Resources",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaternalExperiences",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRequest = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SizeAtBirthDays = table.Column<int>(type: "int", nullable: true),
                    SizeAtBithWeeks = table.Column<int>(type: "int", nullable: true),
                    BabyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BabyGender = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    IdBirthPlace = table.Column<int>(type: "int", nullable: true),
                    BirthPlaceOther = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsNaturalDelivery = table.Column<bool>(type: "bit", nullable: false),
                    HadInductionLabor = table.Column<bool>(type: "bit", nullable: false),
                    HadNaturalReliefs = table.Column<bool>(type: "bit", nullable: false),
                    HadPsychologicalSupport = table.Column<bool>(type: "bit", nullable: false),
                    HadMembranesRupture = table.Column<bool>(type: "bit", nullable: false),
                    HadEpidural = table.Column<bool>(type: "bit", nullable: false),
                    HadOtherAnesthetic = table.Column<bool>(type: "bit", nullable: false),
                    HadEpisiotomy = table.Column<bool>(type: "bit", nullable: false),
                    HadSuctionCupsForceps = table.Column<bool>(type: "bit", nullable: false),
                    HadPlannedCesarean = table.Column<bool>(type: "bit", nullable: false),
                    HadUnPlannedCesarean = table.Column<bool>(type: "bit", nullable: false),
                    HadDeceased = table.Column<bool>(type: "bit", nullable: false),
                    HasBeenTranfered = table.Column<bool>(type: "bit", nullable: false),
                    IdMedicalTransferReason = table.Column<int>(type: "int", nullable: false),
                    IsBreastFeedingAtBirth = table.Column<bool>(type: "bit", nullable: false),
                    IsBreastFeedingSixWeeks = table.Column<bool>(type: "bit", nullable: false),
                    BreastFeedingNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaternalExperiences", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaternalExperiences_BirthPlaces_IdBirthPlace",
                        column: x => x.IdBirthPlace,
                        principalTable: "BirthPlaces",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_MaternalExperiences_MedicalTransferReason_IdMedicalTransferReason",
                        column: x => x.IdMedicalTransferReason,
                        principalTable: "MedicalTransferReason",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaternalExperiences_Requests_IdRequest",
                        column: x => x.IdRequest,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalNotes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRequest = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalNotes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MedicalNotes_Requests_IdRequest",
                        column: x => x.IdRequest,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalNotes_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRequest = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    MeetingNumber = table.Column<int>(type: "int", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdMeetingType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Delay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Meetings_Requests_IdRequest",
                        column: x => x.IdRequest,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meetings_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireAnswers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdQuestionnaire = table.Column<int>(type: "int", nullable: false),
                    IdRequest = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdStatuts = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireAnswers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireAnswers_Questionnaires_IdQuestionnaire",
                        column: x => x.IdQuestionnaire,
                        principalTable: "Questionnaires",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionnaireAnswers_Requests_IdRequest",
                        column: x => x.IdRequest,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionnaireAnswers_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestNotes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRequest = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestNotes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RequestNotes_Requests_IdRequest",
                        column: x => x.IdRequest,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestNotes_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAssignedRequests",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRequest = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssignedRequests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserAssignedRequests_Requests_IdRequest",
                        column: x => x.IdRequest,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAssignedRequests_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaternalExperiences_MaternalExperienceThemes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMaternalExperience = table.Column<int>(type: "int", nullable: false),
                    IdMaternalExperienceTheme = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaternalExperiences_MaternalExperienceThemes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaternalExperiences_MaternalExperienceThemes_MaternalExperiencesThemes_IdMaternalExperienceTheme",
                        column: x => x.IdMaternalExperienceTheme,
                        principalTable: "MaternalExperiencesThemes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaternalExperiences_MaternalExperienceThemes_MaternalExperiences_IdMaternalExperience",
                        column: x => x.IdMaternalExperience,
                        principalTable: "MaternalExperiences",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireAnswerResults",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdQuestionnaireAnswer = table.Column<int>(type: "int", nullable: false),
                    IdQuestionnaireQuestion = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireAnswerResults", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireAnswerResults_QuestionnaireAnswers_IdQuestionnaireAnswer",
                        column: x => x.IdQuestionnaireAnswer,
                        principalTable: "QuestionnaireAnswers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireAnswerResults_QuestionnaireQuestions_IdQuestionnaireQuestion",
                        column: x => x.IdQuestionnaireQuestion,
                        principalTable: "QuestionnaireQuestions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaternalExperiences_IdBirthPlace",
                table: "MaternalExperiences",
                column: "IdBirthPlace");

            migrationBuilder.CreateIndex(
                name: "IX_MaternalExperiences_IdMedicalTransferReason",
                table: "MaternalExperiences",
                column: "IdMedicalTransferReason");

            migrationBuilder.CreateIndex(
                name: "IX_MaternalExperiences_IdRequest",
                table: "MaternalExperiences",
                column: "IdRequest");

            migrationBuilder.CreateIndex(
                name: "IX_MaternalExperiences_MaternalExperienceThemes_IdMaternalExperience",
                table: "MaternalExperiences_MaternalExperienceThemes",
                column: "IdMaternalExperience");

            migrationBuilder.CreateIndex(
                name: "IX_MaternalExperiences_MaternalExperienceThemes_IdMaternalExperienceTheme",
                table: "MaternalExperiences_MaternalExperienceThemes",
                column: "IdMaternalExperienceTheme");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalNotes_IdRequest",
                table: "MedicalNotes",
                column: "IdRequest");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalNotes_IdUser",
                table: "MedicalNotes",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_IdRequest",
                table: "Meetings",
                column: "IdRequest");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_IdUser",
                table: "Meetings",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireAnswerResults_IdQuestionnaireAnswer",
                table: "QuestionnaireAnswerResults",
                column: "IdQuestionnaireAnswer");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireAnswerResults_IdQuestionnaireQuestion",
                table: "QuestionnaireAnswerResults",
                column: "IdQuestionnaireQuestion");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireAnswers_IdQuestionnaire",
                table: "QuestionnaireAnswers",
                column: "IdQuestionnaire");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireAnswers_IdRequest",
                table: "QuestionnaireAnswers",
                column: "IdRequest");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireAnswers_IdUser",
                table: "QuestionnaireAnswers",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireDataSourceOptions_IdQuestionnaireDataSource",
                table: "QuestionnaireDataSourceOptions",
                column: "IdQuestionnaireDataSource");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireQuestions_IdMainDataSource",
                table: "QuestionnaireQuestions",
                column: "IdMainDataSource");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireQuestions_IdQuestionnaire",
                table: "QuestionnaireQuestions",
                column: "IdQuestionnaire");

            migrationBuilder.CreateIndex(
                name: "IX_RequestNotes_IdRequest",
                table: "RequestNotes",
                column: "IdRequest");

            migrationBuilder.CreateIndex(
                name: "IX_RequestNotes_IdUser",
                table: "RequestNotes",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_IdUserClosedBy",
                table: "Requests",
                column: "IdUserClosedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceBusinessHours_IdResource",
                table: "ResourceBusinessHours",
                column: "IdResource");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_IdResourceCity",
                table: "Resources",
                column: "IdResourceCity");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ResourceCategories_IdResource",
                table: "Resources_ResourceCategories",
                column: "IdResource");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ResourceCategories_IdResourceCategory",
                table: "Resources_ResourceCategories",
                column: "IdResourceCategory");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignedRequests_IdRequest",
                table: "UserAssignedRequests",
                column: "IdRequest");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignedRequests_IdUser",
                table: "UserAssignedRequests",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_IdUser",
                table: "UserPermissions",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_IdUserRole",
                table: "UserPermissions",
                column: "IdUserRole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaternalExperiences_MaternalExperienceThemes");

            migrationBuilder.DropTable(
                name: "MedicalNotes");

            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "QuestionnaireAnswerResults");

            migrationBuilder.DropTable(
                name: "QuestionnaireDataSourceOptions");

            migrationBuilder.DropTable(
                name: "RequestNotes");

            migrationBuilder.DropTable(
                name: "ResourceBusinessHours");

            migrationBuilder.DropTable(
                name: "Resources_ResourceCategories");

            migrationBuilder.DropTable(
                name: "UserAssignedRequests");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "MaternalExperiencesThemes");

            migrationBuilder.DropTable(
                name: "MaternalExperiences");

            migrationBuilder.DropTable(
                name: "QuestionnaireAnswers");

            migrationBuilder.DropTable(
                name: "QuestionnaireQuestions");

            migrationBuilder.DropTable(
                name: "ResourceCategories");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "BirthPlaces");

            migrationBuilder.DropTable(
                name: "MedicalTransferReason");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "QuestionnaireDataSources");

            migrationBuilder.DropTable(
                name: "Questionnaires");

            migrationBuilder.DropTable(
                name: "ResourceCities");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
