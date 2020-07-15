using System;
using System.Collections.Generic;
using System.Text;
using FineOnlinePaymentSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FineOnlinePaymentSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser,IdentityRole,string>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Models that will be used as tables
        public DbSet<Offender> Offenders { get; set; }
        public DbSet<Relative> Relatives { get; set; }
        public DbSet<Officer> Officers { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<CaseOffender> CaseOffenders { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<FinePayment> FinePayments { get; set; }
        public DbSet<Amortization> Amortizations { get; set; }
        public DbSet<AmortizationSettings> AmortizationSettings { get; set; }
        public DbSet<Offence> Offences { get; set; }
        public DbSet<OffenderStatus> OffenderStatuses { get; set; }
        public DbSet<CaseStatus> CaseStatuses { get; set; }
        public DbSet<FineStatus> FineStatuses { get; set; }
        public DbSet<FinePaymentStatus> FinePaymentStatuses { get; set; }



        protected  async override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Setting up fields that will be Unique in the database
            builder.Entity<Offender>().HasIndex(o => o.PIN).IsUnique();
            builder.Entity<Relative>().HasIndex(r => r.PIN).IsUnique();
            builder.Entity<Officer>().HasIndex(ofc => ofc.ForceNumber).IsUnique();
            builder.Entity<Case>().HasIndex(c => c.CaseNumber).IsUnique();


            //Case Offender relationships
            builder.Entity<CaseOffender>().HasKey(co => new { co.CaseID, co.OffenderID });

            //Many To Many relationship configuration for CaseOffender Table
            builder.Entity<CaseOffender>().HasOne(co => co.Case).WithMany(co => co.CaseOffenders).HasForeignKey(co => co.CaseID);
            builder.Entity<CaseOffender>().HasOne(co => co.Offender).WithMany(co => co.CaseOffenders).HasForeignKey(co => co.OffenderID);


            //Seeding offences
            builder.Entity<Offence>().HasData(
            new Offence
            {
                OffenseID = 1,
                Name = "Robbery"
            },
            new Offence
            {
                OffenseID = 2,
                Name = "Assault"
            },
            new Offence
            {
                OffenseID = 3,
                Name = "Harassment"
            },
            new Offence
            {
                OffenseID = 4,
                Name = "Vandalism"
            },
            new Offence
            {
                OffenseID = 5,
                Name = "Theft"
            },
            new Offence
            {
                OffenseID = 6,
                Name = "Public Indecency(Indecent Exposure)"
            },
            new Offence
            {
                OffenseID = 7,
                Name = "Public Indecency(Open Container)"
            },
            new Offence
            {
                OffenseID = 8,
                Name = "Bribery"
            },
            new Offence
            {
                OffenseID = 9,
                Name = "Stalking"
            }
            );




            //seeding OffenderStatus
            builder.Entity<OffenderStatus>().HasData(
                new OffenderStatus
                {
                    StatusID = 1,
                    Name ="In Custody"
                },
                new Offender
                {
                    StatusID = 2,
                    Name = "Released"
                }
                );


            //seeding caseStatus
            builder.Entity<CaseStatus>().HasData(
                new CaseStatus
                {
                    CaseStatusID = 1,
                    Name = "Pending"
                },
                new CaseStatus
                {
                    CaseStatusID = 2,
                    Name = "Closed"
                }
                );

            //seeding AmortizationSettings
            builder.Entity<AmortizationSettings>().HasData(
                new AmortizationSettings
                {
                    AmortizationSettingsID = 1,
                    PercentPerDay = 6,
                    DaysBeforeAmortization = 3
                }
                );



            //seeding FineStatuses
            builder.Entity<FineStatus>().HasData(
                new FineStatus
                {
                    FineStatusID = 1,
                    Name = "Pending"
                },
                 new FineStatus
                 {
                     FineStatusID = 2,
                     Name = "Paid"
                 }
                );

            //seeding finePayment status
            builder.Entity<FinePaymentStatus>().HasData(
                new FinePaymentStatus
                {
                    ID = 1,
                    Name = "Pending"
                },
                 new FinePaymentStatus
                 {
                     ID = 2,
                     Name = "Paid"
                 }
                );


          await  _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
          await  _roleManager.CreateAsync(new IdentityRole { Name = "Officer" });
          await  _roleManager.CreateAsync(new IdentityRole { Name = "Relative" });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
