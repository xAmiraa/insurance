using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InsuranceClaims.Data.Migrations
{
    public partial class AddTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Customers");

            migrationBuilder.EnsureSchema(
                name: "Payments");

            migrationBuilder.EnsureSchema(
                name: "Claims");

            migrationBuilder.EnsureSchema(
                name: "Lookups");

            migrationBuilder.EnsureSchema(
                name: "Companies");

            migrationBuilder.EnsureSchema(
                name: "Policies");

            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.CreateTable(
                name: "ClaimStatuses",
                schema: "Lookups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CoverageTypes",
                schema: "Lookups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonalImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangePassword = table.Column<bool>(type: "bit", nullable: false),
                    CallingCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextPasswordExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailVerifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Security",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "Lookups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NativeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CallingCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Countries_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                schema: "Lookups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Currencies_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Currencies_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentificationTypes",
                schema: "Lookups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentificationTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentificationTypes_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdentificationTypes_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PolicyInsurers",
                schema: "Lookups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyInsurers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyInsurers_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyInsurers_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "Security",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId1 = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Security",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId1",
                        column: x => x.RoleId1,
                        principalSchema: "Security",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "Security",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumOfDaysToChangePassword = table.Column<int>(type: "int", nullable: false),
                    AccountLoginAttempts = table.Column<int>(type: "int", nullable: false),
                    PasswordExpiryTime = table.Column<int>(type: "int", nullable: false),
                    UserPhotosize = table.Column<double>(type: "float", nullable: false),
                    AttachmentsMaxSize = table.Column<double>(type: "float", nullable: false),
                    TimesCountBeforePasswordReuse = table.Column<int>(type: "int", nullable: false),
                    TimeToSessionTimeOut = table.Column<int>(type: "int", nullable: false),
                    BusinessRegisterationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxRegisterationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeginingReceiptNumber = table.Column<long>(type: "bigint", nullable: false),
                    AutoPaymentOnAddingNewPrepayment = table.Column<bool>(type: "bit", nullable: false),
                    ShowCashAndChangePage = table.Column<bool>(type: "bit", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "Lookups",
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "Lookups",
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PolicyTypes",
                schema: "Lookups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PolicyInsurerId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyTypes_PolicyInsurers_PolicyInsurerId",
                        column: x => x.PolicyInsurerId,
                        principalSchema: "Lookups",
                        principalTable: "PolicyInsurers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyAttachmentTypes",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAttachmentTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyAttachmentTypes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Companies",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyAttachmentTypes_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyAttachmentTypes_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCollectionMethods",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCollectionMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyCollectionMethods_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Companies",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyCollectionMethods_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyCollectionMethods_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyContacts",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CellPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BusinessCountryId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyContacts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Companies",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyContacts_Countries_BusinessCountryId",
                        column: x => x.BusinessCountryId,
                        principalSchema: "Lookups",
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyContacts_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyContacts_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDenominations",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DenominationValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDenominations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDenominations_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Companies",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyDenominations_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyDenominations_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyEmployers",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyEmployers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyEmployers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Companies",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyEmployers_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyEmployers_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyMinorAges",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgeValue = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyMinorAges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyMinorAges_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Companies",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyMinorAges_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyMinorAges_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPaymentMethods",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPaymentMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPaymentMethods_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Companies",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyPaymentMethods_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyPaymentMethods_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPolicyTypes",
                schema: "Companies",
                columns: table => new
                {
                    CompaniesId = table.Column<int>(type: "int", nullable: false),
                    PolicyTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPolicyTypes", x => new { x.CompaniesId, x.PolicyTypesId });
                    table.ForeignKey(
                        name: "FK_CompanyPolicyTypes_Companies_CompaniesId",
                        column: x => x.CompaniesId,
                        principalSchema: "Companies",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyPolicyTypes_PolicyTypes_PolicyTypesId",
                        column: x => x.PolicyTypesId,
                        principalSchema: "Lookups",
                        principalTable: "PolicyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsBusiness = table.Column<bool>(type: "bit", nullable: false),
                    TamisNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NisNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisterationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Minor = table.Column<bool>(type: "bit", nullable: true),
                    ParentGuardian = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateEmployed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    EmployerId = table.Column<int>(type: "int", nullable: true),
                    AccountManagerId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Companies",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_CompanyEmployers_EmployerId",
                        column: x => x.EmployerId,
                        principalSchema: "Companies",
                        principalTable: "CompanyEmployers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Users_AccountManagerId",
                        column: x => x.AccountManagerId,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customers_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Beneficiaries",
                schema: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Allocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beneficiaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beneficiaries_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customers",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Beneficiaries_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Beneficiaries_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContacts",
                schema: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CellPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondaryEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "Lookups",
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customers",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dependents",
                schema: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dependents_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customers",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dependents_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dependents_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                schema: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceiptNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaidBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customers",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                schema: "Policies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SumInsured = table.Column<double>(type: "float", nullable: true),
                    CriticalIllness = table.Column<double>(type: "float", nullable: true),
                    DependentCover = table.Column<double>(type: "float", nullable: true),
                    LevelOfBenefit = table.Column<double>(type: "float", nullable: true),
                    AgeOfMaturity = table.Column<int>(type: "int", nullable: true),
                    IsRegistered = table.Column<bool>(type: "bit", nullable: true),
                    DepositAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InstallmentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LengthOfTerm = table.Column<int>(type: "int", nullable: true),
                    EngineNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChasisNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeatCapacity = table.Column<int>(type: "int", nullable: true),
                    EngineCC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Construction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Foundation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mortagagee = table.Column<bool>(type: "bit", nullable: true),
                    WhoMortagagee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaximumPerOccurence = table.Column<int>(type: "int", nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: true),
                    MachineryType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineryValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineryOwned = table.Column<bool>(type: "bit", nullable: true),
                    WhoMachinery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Itinerary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModeOfTravel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommercialOwned = table.Column<bool>(type: "bit", nullable: true),
                    WhoCommercial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PolicyTypeId = table.Column<int>(type: "int", nullable: false),
                    CoverageTypeId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Policies_CoverageTypes_CoverageTypeId",
                        column: x => x.CoverageTypeId,
                        principalSchema: "Lookups",
                        principalTable: "CoverageTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policies_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customers",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policies_PolicyTypes_PolicyTypeId",
                        column: x => x.PolicyTypeId,
                        principalSchema: "Lookups",
                        principalTable: "PolicyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policies_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Policies_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CashDenominationReturns",
                schema: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Denomination = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    CompanyDenominationId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashDenominationReturns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashDenominationReturns_CompanyDenominations_CompanyDenominationId",
                        column: x => x.CompanyDenominationId,
                        principalSchema: "Companies",
                        principalTable: "CompanyDenominations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashDenominationReturns_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalSchema: "Payments",
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashDenominationReturns_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashDenominationReturns_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CashDenominations",
                schema: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Denomination = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    CompanyDenominationId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashDenominations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashDenominations_CompanyDenominations_CompanyDenominationId",
                        column: x => x.CompanyDenominationId,
                        principalSchema: "Companies",
                        principalTable: "CompanyDenominations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashDenominations_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalSchema: "Payments",
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashDenominations_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CashDenominations_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                schema: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PolicyId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customers",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalSchema: "Policies",
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                schema: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateReceived = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateSentToInsurance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateReturnedFromInsurance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateChequeIssued = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChequeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChequeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CollectionMethodId = table.Column<int>(type: "int", nullable: false),
                    ClaimStatusId = table.Column<int>(type: "int", nullable: false),
                    PolicyId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claims_ClaimStatuses_ClaimStatusId",
                        column: x => x.ClaimStatusId,
                        principalSchema: "Lookups",
                        principalTable: "ClaimStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Claims_CompanyCollectionMethods_CollectionMethodId",
                        column: x => x.CollectionMethodId,
                        principalSchema: "Companies",
                        principalTable: "CompanyCollectionMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Claims_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customers",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Claims_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalSchema: "Policies",
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Claims_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Claims_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerBeneficiaryPolicies",
                schema: "Policies",
                columns: table => new
                {
                    CustomerBeneficiariesId = table.Column<int>(type: "int", nullable: false),
                    PoliciesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerBeneficiaryPolicies", x => new { x.CustomerBeneficiariesId, x.PoliciesId });
                    table.ForeignKey(
                        name: "FK_CustomerBeneficiaryPolicies_Beneficiaries_CustomerBeneficiariesId",
                        column: x => x.CustomerBeneficiariesId,
                        principalSchema: "Customers",
                        principalTable: "Beneficiaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerBeneficiaryPolicies_Policies_PoliciesId",
                        column: x => x.PoliciesId,
                        principalSchema: "Policies",
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDependentPolicies",
                schema: "Policies",
                columns: table => new
                {
                    CustomerDependentsId = table.Column<int>(type: "int", nullable: false),
                    PoliciesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDependentPolicies", x => new { x.CustomerDependentsId, x.PoliciesId });
                    table.ForeignKey(
                        name: "FK_CustomerDependentPolicies_Dependents_CustomerDependentsId",
                        column: x => x.CustomerDependentsId,
                        principalSchema: "Customers",
                        principalTable: "Dependents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerDependentPolicies_Policies_PoliciesId",
                        column: x => x.PoliciesId,
                        principalSchema: "Policies",
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PolicyContents",
                schema: "Policies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyContents_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalSchema: "Policies",
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyContents_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyContents_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PolicyInsuredDrivers",
                schema: "Policies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyId = table.Column<int>(type: "int", nullable: false),
                    IdentificationTypeId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyInsuredDrivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyInsuredDrivers_IdentificationTypes_IdentificationTypeId",
                        column: x => x.IdentificationTypeId,
                        principalSchema: "Lookups",
                        principalTable: "IdentificationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyInsuredDrivers_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalSchema: "Policies",
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyInsuredDrivers_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyInsuredDrivers_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prepayments",
                schema: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prepayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prepayments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customers",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prepayments_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalSchema: "Policies",
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prepayments_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prepayments_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClaimAttachments",
                schema: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttachmentPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClaimAttachments_Claims_ClaimId",
                        column: x => x.ClaimId,
                        principalSchema: "Claims",
                        principalTable: "Claims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClaimAttachments_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClaimAttachments_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentAllocations",
                schema: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaidTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    PolicyId = table.Column<int>(type: "int", nullable: true),
                    BillId = table.Column<int>(type: "int", nullable: true),
                    PrepaymentId = table.Column<int>(type: "int", nullable: true),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentAllocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentAllocations_Bills_BillId",
                        column: x => x.BillId,
                        principalSchema: "Payments",
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentAllocations_CompanyPaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalSchema: "Companies",
                        principalTable: "CompanyPaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentAllocations_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalSchema: "Payments",
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentAllocations_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalSchema: "Policies",
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentAllocations_Prepayments_PrepaymentId",
                        column: x => x.PrepaymentId,
                        principalSchema: "Payments",
                        principalTable: "Prepayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentAllocations_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentAllocations_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalSchema: "Security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_CreatedBy",
                schema: "Customers",
                table: "Beneficiaries",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_CustomerId",
                schema: "Customers",
                table: "Beneficiaries",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiaries_UpdatedBy",
                schema: "Customers",
                table: "Beneficiaries",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CreatedBy",
                schema: "Payments",
                table: "Bills",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CustomerId",
                schema: "Payments",
                table: "Bills",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PolicyId",
                schema: "Payments",
                table: "Bills",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_UpdatedBy",
                schema: "Payments",
                table: "Bills",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CashDenominationReturns_CompanyDenominationId",
                schema: "Payments",
                table: "CashDenominationReturns",
                column: "CompanyDenominationId");

            migrationBuilder.CreateIndex(
                name: "IX_CashDenominationReturns_CreatedBy",
                schema: "Payments",
                table: "CashDenominationReturns",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CashDenominationReturns_PaymentId",
                schema: "Payments",
                table: "CashDenominationReturns",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_CashDenominationReturns_UpdatedBy",
                schema: "Payments",
                table: "CashDenominationReturns",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CashDenominations_CompanyDenominationId",
                schema: "Payments",
                table: "CashDenominations",
                column: "CompanyDenominationId");

            migrationBuilder.CreateIndex(
                name: "IX_CashDenominations_CreatedBy",
                schema: "Payments",
                table: "CashDenominations",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CashDenominations_PaymentId",
                schema: "Payments",
                table: "CashDenominations",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_CashDenominations_UpdatedBy",
                schema: "Payments",
                table: "CashDenominations",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimAttachments_ClaimId",
                schema: "Claims",
                table: "ClaimAttachments",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimAttachments_CreatedBy",
                schema: "Claims",
                table: "ClaimAttachments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimAttachments_UpdatedBy",
                schema: "Claims",
                table: "ClaimAttachments",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ClaimStatusId",
                schema: "Claims",
                table: "Claims",
                column: "ClaimStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_CollectionMethodId",
                schema: "Claims",
                table: "Claims",
                column: "CollectionMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_CreatedBy",
                schema: "Claims",
                table: "Claims",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_CustomerId",
                schema: "Claims",
                table: "Claims",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_PolicyId",
                schema: "Claims",
                table: "Claims",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_UpdatedBy",
                schema: "Claims",
                table: "Claims",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CountryId",
                schema: "Companies",
                table: "Companies",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CreatedBy",
                schema: "Companies",
                table: "Companies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CurrencyId",
                schema: "Companies",
                table: "Companies",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UpdatedBy",
                schema: "Companies",
                table: "Companies",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAttachmentTypes_CompanyId",
                schema: "Companies",
                table: "CompanyAttachmentTypes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAttachmentTypes_CreatedBy",
                schema: "Companies",
                table: "CompanyAttachmentTypes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAttachmentTypes_UpdatedBy",
                schema: "Companies",
                table: "CompanyAttachmentTypes",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCollectionMethods_CompanyId",
                schema: "Companies",
                table: "CompanyCollectionMethods",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCollectionMethods_CreatedBy",
                schema: "Companies",
                table: "CompanyCollectionMethods",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCollectionMethods_UpdatedBy",
                schema: "Companies",
                table: "CompanyCollectionMethods",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContacts_BusinessCountryId",
                schema: "Companies",
                table: "CompanyContacts",
                column: "BusinessCountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContacts_CompanyId",
                schema: "Companies",
                table: "CompanyContacts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContacts_CreatedBy",
                schema: "Companies",
                table: "CompanyContacts",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContacts_UpdatedBy",
                schema: "Companies",
                table: "CompanyContacts",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDenominations_CompanyId",
                schema: "Companies",
                table: "CompanyDenominations",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDenominations_CreatedBy",
                schema: "Companies",
                table: "CompanyDenominations",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDenominations_UpdatedBy",
                schema: "Companies",
                table: "CompanyDenominations",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmployers_CompanyId",
                schema: "Companies",
                table: "CompanyEmployers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmployers_CreatedBy",
                schema: "Companies",
                table: "CompanyEmployers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmployers_UpdatedBy",
                schema: "Companies",
                table: "CompanyEmployers",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMinorAges_CompanyId",
                schema: "Companies",
                table: "CompanyMinorAges",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMinorAges_CreatedBy",
                schema: "Companies",
                table: "CompanyMinorAges",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMinorAges_UpdatedBy",
                schema: "Companies",
                table: "CompanyMinorAges",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPaymentMethods_CompanyId",
                schema: "Companies",
                table: "CompanyPaymentMethods",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPaymentMethods_CreatedBy",
                schema: "Companies",
                table: "CompanyPaymentMethods",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPaymentMethods_UpdatedBy",
                schema: "Companies",
                table: "CompanyPaymentMethods",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPolicyTypes_PolicyTypesId",
                schema: "Companies",
                table: "CompanyPolicyTypes",
                column: "PolicyTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CreatedBy",
                schema: "Lookups",
                table: "Countries",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_UpdatedBy",
                schema: "Lookups",
                table: "Countries",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_CreatedBy",
                schema: "Lookups",
                table: "Currencies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_UpdatedBy",
                schema: "Lookups",
                table: "Currencies",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerBeneficiaryPolicies_PoliciesId",
                schema: "Policies",
                table: "CustomerBeneficiaryPolicies",
                column: "PoliciesId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_CountryId",
                schema: "Customers",
                table: "CustomerContacts",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_CreatedBy",
                schema: "Customers",
                table: "CustomerContacts",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_CustomerId",
                schema: "Customers",
                table: "CustomerContacts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_UpdatedBy",
                schema: "Customers",
                table: "CustomerContacts",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDependentPolicies_PoliciesId",
                schema: "Policies",
                table: "CustomerDependentPolicies",
                column: "PoliciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AccountManagerId",
                schema: "Customers",
                table: "Customers",
                column: "AccountManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CompanyId",
                schema: "Customers",
                table: "Customers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatedBy",
                schema: "Customers",
                table: "Customers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_EmployerId",
                schema: "Customers",
                table: "Customers",
                column: "EmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UpdatedBy",
                schema: "Customers",
                table: "Customers",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Dependents_CreatedBy",
                schema: "Customers",
                table: "Dependents",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Dependents_CustomerId",
                schema: "Customers",
                table: "Dependents",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Dependents_UpdatedBy",
                schema: "Customers",
                table: "Dependents",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IdentificationTypes_CreatedBy",
                schema: "Lookups",
                table: "IdentificationTypes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_IdentificationTypes_UpdatedBy",
                schema: "Lookups",
                table: "IdentificationTypes",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAllocations_BillId",
                schema: "Payments",
                table: "PaymentAllocations",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAllocations_CreatedBy",
                schema: "Payments",
                table: "PaymentAllocations",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAllocations_PaymentId",
                schema: "Payments",
                table: "PaymentAllocations",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAllocations_PaymentMethodId",
                schema: "Payments",
                table: "PaymentAllocations",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAllocations_PolicyId",
                schema: "Payments",
                table: "PaymentAllocations",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAllocations_PrepaymentId",
                schema: "Payments",
                table: "PaymentAllocations",
                column: "PrepaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentAllocations_UpdatedBy",
                schema: "Payments",
                table: "PaymentAllocations",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CreatedBy",
                schema: "Payments",
                table: "Payments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CustomerId",
                schema: "Payments",
                table: "Payments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UpdatedBy",
                schema: "Payments",
                table: "Payments",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_CoverageTypeId",
                schema: "Policies",
                table: "Policies",
                column: "CoverageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_CreatedBy",
                schema: "Policies",
                table: "Policies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_CustomerId",
                schema: "Policies",
                table: "Policies",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_PolicyTypeId",
                schema: "Policies",
                table: "Policies",
                column: "PolicyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Policies_UpdatedBy",
                schema: "Policies",
                table: "Policies",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyContents_CreatedBy",
                schema: "Policies",
                table: "PolicyContents",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyContents_PolicyId",
                schema: "Policies",
                table: "PolicyContents",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyContents_UpdatedBy",
                schema: "Policies",
                table: "PolicyContents",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyInsuredDrivers_CreatedBy",
                schema: "Policies",
                table: "PolicyInsuredDrivers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyInsuredDrivers_IdentificationTypeId",
                schema: "Policies",
                table: "PolicyInsuredDrivers",
                column: "IdentificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyInsuredDrivers_PolicyId",
                schema: "Policies",
                table: "PolicyInsuredDrivers",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyInsuredDrivers_UpdatedBy",
                schema: "Policies",
                table: "PolicyInsuredDrivers",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyInsurers_CreatedBy",
                schema: "Lookups",
                table: "PolicyInsurers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyInsurers_UpdatedBy",
                schema: "Lookups",
                table: "PolicyInsurers",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyTypes_PolicyInsurerId",
                schema: "Lookups",
                table: "PolicyTypes",
                column: "PolicyInsurerId");

            migrationBuilder.CreateIndex(
                name: "IX_Prepayments_CreatedBy",
                schema: "Payments",
                table: "Prepayments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Prepayments_CustomerId",
                schema: "Payments",
                table: "Prepayments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Prepayments_PolicyId",
                schema: "Payments",
                table: "Prepayments",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Prepayments_UpdatedBy",
                schema: "Payments",
                table: "Prepayments",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "Security",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Security",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "Security",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "Security",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Security",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId1",
                schema: "Security",
                table: "UserRoles",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                schema: "Security",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Security",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Security",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashDenominationReturns",
                schema: "Payments");

            migrationBuilder.DropTable(
                name: "CashDenominations",
                schema: "Payments");

            migrationBuilder.DropTable(
                name: "ClaimAttachments",
                schema: "Claims");

            migrationBuilder.DropTable(
                name: "CompanyAttachmentTypes",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "CompanyContacts",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "CompanyMinorAges",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "CompanyPolicyTypes",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "CustomerBeneficiaryPolicies",
                schema: "Policies");

            migrationBuilder.DropTable(
                name: "CustomerContacts",
                schema: "Customers");

            migrationBuilder.DropTable(
                name: "CustomerDependentPolicies",
                schema: "Policies");

            migrationBuilder.DropTable(
                name: "PaymentAllocations",
                schema: "Payments");

            migrationBuilder.DropTable(
                name: "PolicyContents",
                schema: "Policies");

            migrationBuilder.DropTable(
                name: "PolicyInsuredDrivers",
                schema: "Policies");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "CompanyDenominations",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "Claims",
                schema: "Claims");

            migrationBuilder.DropTable(
                name: "Beneficiaries",
                schema: "Customers");

            migrationBuilder.DropTable(
                name: "Dependents",
                schema: "Customers");

            migrationBuilder.DropTable(
                name: "Bills",
                schema: "Payments");

            migrationBuilder.DropTable(
                name: "CompanyPaymentMethods",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "Payments",
                schema: "Payments");

            migrationBuilder.DropTable(
                name: "Prepayments",
                schema: "Payments");

            migrationBuilder.DropTable(
                name: "IdentificationTypes",
                schema: "Lookups");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "ClaimStatuses",
                schema: "Lookups");

            migrationBuilder.DropTable(
                name: "CompanyCollectionMethods",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "Policies",
                schema: "Policies");

            migrationBuilder.DropTable(
                name: "CoverageTypes",
                schema: "Lookups");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "Customers");

            migrationBuilder.DropTable(
                name: "PolicyTypes",
                schema: "Lookups");

            migrationBuilder.DropTable(
                name: "CompanyEmployers",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "PolicyInsurers",
                schema: "Lookups");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "Lookups");

            migrationBuilder.DropTable(
                name: "Currencies",
                schema: "Lookups");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Security");
        }
    }
}
