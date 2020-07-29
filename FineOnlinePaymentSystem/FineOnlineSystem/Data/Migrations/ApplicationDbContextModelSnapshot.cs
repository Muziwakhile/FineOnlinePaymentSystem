﻿// <auto-generated />
using System;
using FineOnlinePaymentSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FineOnlinePaymentSystem.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.Amortization", b =>
                {
                    b.Property<int>("AmortizationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AmortizationAmount")
                        .HasColumnType("Decimal(10,2)");

                    b.Property<decimal>("AmortizationAmountPerDay")
                        .HasColumnType("Decimal(10,2)");

                    b.Property<int>("CaseID")
                        .HasColumnType("int");

                    b.Property<int>("DaysOverstayed")
                        .HasColumnType("int");

                    b.Property<int>("FineID")
                        .HasColumnType("int");

                    b.Property<int>("Percent")
                        .HasColumnType("int");

                    b.HasKey("AmortizationID");

                    b.HasIndex("CaseID");

                    b.HasIndex("FineID");

                    b.ToTable("Amortizations");
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.AmortizationSettings", b =>
                {
                    b.Property<int>("AmortizationSettingsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DaysBeforeAmortization")
                        .HasColumnType("int");

                    b.Property<int>("DaysToBeInJail")
                        .HasColumnType("int");

                    b.Property<int>("PercentPerDay")
                        .HasColumnType("int");

                    b.HasKey("AmortizationSettingsID");

                    b.ToTable("AmortizationSettings");

                    b.HasData(
                        new
                        {
                            AmortizationSettingsID = 1,
                            DaysBeforeAmortization = 3,
                            DaysToBeInJail = 270,
                            PercentPerDay = 2
                        });
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.Case", b =>
                {
                    b.Property<int>("CaseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CaseDescription")
                        .IsRequired()
                        .HasColumnType("varchar(max)")
                        .HasMaxLength(50000);

                    b.Property<int>("CaseNumber")
                        .HasColumnType("int");

                    b.Property<int>("CaseStatusID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CourtDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CrimeLocation")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("DateOfArrest")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfCrime")
                        .HasColumnType("datetime2");

                    b.Property<int>("OffenceID")
                        .HasColumnType("int");

                    b.Property<int>("OfficerID")
                        .HasColumnType("int");

                    b.HasKey("CaseID");

                    b.HasIndex("CaseNumber")
                        .IsUnique();

                    b.HasIndex("CaseStatusID");

                    b.HasIndex("OffenceID");

                    b.HasIndex("OfficerID");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.CaseOffender", b =>
                {
                    b.Property<int>("CaseID")
                        .HasColumnType("int");

                    b.Property<int>("OffenderID")
                        .HasColumnType("int");

                    b.HasKey("CaseID", "OffenderID");

                    b.HasIndex("OffenderID");

                    b.ToTable("CaseOffenders");
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.CaseStatus", b =>
                {
                    b.Property<int>("CaseStatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.HasKey("CaseStatusID");

                    b.ToTable("CaseStatuses");

                    b.HasData(
                        new
                        {
                            CaseStatusID = 1,
                            Name = "Pending"
                        },
                        new
                        {
                            CaseStatusID = 2,
                            Name = "Closed"
                        });
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.Fine", b =>
                {
                    b.Property<int>("FineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("Decimal(10,2)");

                    b.Property<int>("CaseID")
                        .HasColumnType("int");

                    b.Property<int>("FineStatusID")
                        .HasColumnType("int");

                    b.Property<int>("OffenderID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("FineID");

                    b.HasIndex("CaseID");

                    b.HasIndex("FineStatusID");

                    b.HasIndex("OffenderID");

                    b.ToTable("Fines");
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.FinePayment", b =>
                {
                    b.Property<int>("FinePaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AmortizationAmount")
                        .HasColumnType("Decimal(10,2)");

                    b.Property<int>("AmortizationID")
                        .HasColumnType("int");

                    b.Property<decimal>("AmountPayable")
                        .HasColumnType("Decimal(10,2)");

                    b.Property<byte[]>("Attachment")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("FineID")
                        .HasColumnType("int");

                    b.Property<DateTime>("FinePaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("FinePaymentStatusID")
                        .HasColumnType("int");

                    b.Property<int>("RelativeID")
                        .HasColumnType("int");

                    b.HasKey("FinePaymentID");

                    b.HasIndex("AmortizationID");

                    b.HasIndex("FineID");

                    b.HasIndex("FinePaymentStatusID");

                    b.HasIndex("RelativeID");

                    b.ToTable("FinePayments");
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.FinePaymentStatus", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("FinePaymentStatuses");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Pending"
                        },
                        new
                        {
                            ID = 2,
                            Name = "Paid"
                        });
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.FineStatus", b =>
                {
                    b.Property<int>("FineStatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FineStatusID");

                    b.ToTable("FineStatuses");

                    b.HasData(
                        new
                        {
                            FineStatusID = 1,
                            Name = "Pending"
                        },
                        new
                        {
                            FineStatusID = 2,
                            Name = "Paid"
                        });
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.Offence", b =>
                {
                    b.Property<int>("OffenseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("OffenseID");

                    b.ToTable("Offences");

                    b.HasData(
                        new
                        {
                            OffenseID = 1,
                            Name = "Robbery"
                        },
                        new
                        {
                            OffenseID = 2,
                            Name = "Assault"
                        },
                        new
                        {
                            OffenseID = 3,
                            Name = "Harassment"
                        },
                        new
                        {
                            OffenseID = 4,
                            Name = "Vandalism"
                        },
                        new
                        {
                            OffenseID = 5,
                            Name = "Theft"
                        },
                        new
                        {
                            OffenseID = 6,
                            Name = "Public Indecency(Indecent Exposure)"
                        },
                        new
                        {
                            OffenseID = 7,
                            Name = "Public Indecency(Open Container)"
                        },
                        new
                        {
                            OffenseID = 8,
                            Name = "Bribery"
                        },
                        new
                        {
                            OffenseID = 9,
                            Name = "Stalking"
                        });
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.Offender", b =>
                {
                    b.Property<int>("OffenderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("HomeAddress")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.Property<string>("PIN")
                        .IsRequired()
                        .HasColumnType("varchar(18)")
                        .HasMaxLength(14);

                    b.Property<int>("StatusID")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.HasKey("OffenderID");

                    b.HasIndex("PIN")
                        .IsUnique();

                    b.HasIndex("StatusID");

                    b.ToTable("Offenders");
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.OffenderStatus", b =>
                {
                    b.Property<int>("StatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.HasKey("StatusID");

                    b.ToTable("OffenderStatuses");

                    b.HasData(
                        new
                        {
                            StatusID = 1,
                            Name = "In Custody"
                        },
                        new
                        {
                            StatusID = 2,
                            Name = "Released"
                        });
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.Officer", b =>
                {
                    b.Property<int>("OfficerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Contact")
                        .HasColumnType("bigint");

                    b.Property<int>("ForceNumber")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("OfficerID");

                    b.HasIndex("ForceNumber")
                        .IsUnique();

                    b.HasIndex("UserID");

                    b.ToTable("Officers");
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.Relative", b =>
                {
                    b.Property<int>("RelativeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Contact")
                        .HasColumnType("bigint");

                    b.Property<string>("IdentityUserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.Property<string>("PIN")
                        .IsRequired()
                        .HasColumnType("varchar(18)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.HasKey("RelativeID");

                    b.HasIndex("IdentityUserID");

                    b.HasIndex("PIN")
                        .IsUnique();

                    b.ToTable("Relatives");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.Amortization", b =>
                {
                    b.HasOne("FineOnlinePaymentSystem.Models.Case", "Case")
                        .WithMany("Amortizations")
                        .HasForeignKey("CaseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FineOnlinePaymentSystem.Models.Fine", "Fine")
                        .WithMany("Amortizations")
                        .HasForeignKey("FineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.Case", b =>
                {
                    b.HasOne("FineOnlinePaymentSystem.Models.CaseStatus", "CaseStatus")
                        .WithMany("Cases")
                        .HasForeignKey("CaseStatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FineOnlinePaymentSystem.Models.Offence", "Offence")
                        .WithMany("Cases")
                        .HasForeignKey("OffenceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FineOnlinePaymentSystem.Models.Officer", "Officer")
                        .WithMany("Cases")
                        .HasForeignKey("OfficerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.CaseOffender", b =>
                {
                    b.HasOne("FineOnlinePaymentSystem.Models.Case", "Case")
                        .WithMany("CaseOffenders")
                        .HasForeignKey("CaseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FineOnlinePaymentSystem.Models.Offender", "Offender")
                        .WithMany("CaseOffenders")
                        .HasForeignKey("OffenderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.Fine", b =>
                {
                    b.HasOne("FineOnlinePaymentSystem.Models.Case", "Case")
                        .WithMany("Fines")
                        .HasForeignKey("CaseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FineOnlinePaymentSystem.Models.FineStatus", "FineStatus")
                        .WithMany("Fines")
                        .HasForeignKey("FineStatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FineOnlinePaymentSystem.Models.Offender", "Offender")
                        .WithMany("Fines")
                        .HasForeignKey("OffenderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.FinePayment", b =>
                {
                    b.HasOne("FineOnlinePaymentSystem.Models.Amortization", "Amortization")
                        .WithMany("FinePayments")
                        .HasForeignKey("AmortizationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FineOnlinePaymentSystem.Models.Fine", "Fine")
                        .WithMany("FinePayments")
                        .HasForeignKey("FineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FineOnlinePaymentSystem.Models.FinePaymentStatus", "FinePaymentStatus")
                        .WithMany("FinePayments")
                        .HasForeignKey("FinePaymentStatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FineOnlinePaymentSystem.Models.Relative", "Relative")
                        .WithMany("FinePayments")
                        .HasForeignKey("RelativeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.Offender", b =>
                {
                    b.HasOne("FineOnlinePaymentSystem.Models.OffenderStatus", "Status")
                        .WithMany("Offenders")
                        .HasForeignKey("StatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.Officer", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("FineOnlinePaymentSystem.Models.Relative", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "IdentityUser")
                        .WithMany()
                        .HasForeignKey("IdentityUserID");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
