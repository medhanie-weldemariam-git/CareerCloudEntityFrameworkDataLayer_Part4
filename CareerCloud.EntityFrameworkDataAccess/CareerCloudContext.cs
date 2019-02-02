using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
    class CareerCloudContext : DbContext
    {
        //protected string connString;
        //connString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public CareerCloudContext() : base(@"Data Source=DESKTOP-UCS764M\HUMBERBRIDGING;Initial Catalog=JOB_PORTAL_DB;Integrated Security=True;")
        {
            //Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*Tell EF here, how database tables relate each other*/
            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(a => a.ApplicantEducation)
                .WithRequired(e => e.ApplicantProfile)
                .HasForeignKey(e => e.Applicant);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(a => a.ApplicantResume)
                .WithRequired(e => e.ApplicantProfile)
                .HasForeignKey(e => e.Applicant);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(a => a.ApplicantWorkHistory)
                .WithRequired(e => e.ApplicantProfile)
                .HasForeignKey(e => e.Applicant);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(a => a.ApplicantSkill)
                .WithRequired(e => e.ApplicantProfile)
                .HasForeignKey(e => e.Applicant);

            modelBuilder.Entity<SystemCountryCodePoco>()
                .HasMany(a => a.ApplicantProfile)
                .WithRequired(e => e.SystemCountryCode)
                .HasForeignKey(e => e.Country);

            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(a => a.ApplicantProfile)
                .WithRequired(e => e.SecurityLogin)
                .HasForeignKey(e => e.Login);

            modelBuilder.Entity<SystemCountryCodePoco>()
                .HasMany(a => a.ApplicantWorkHistory)
                .WithRequired(e => e.SystemCountryCode)
                .HasForeignKey(e => e.CountryCode);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(a => a.ApplicantJobApplication)
                .WithRequired(e => e.CompanyJobs)
                .HasForeignKey(e => e.Job);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(a => a.ApplicantJobApplication)
                .WithRequired(e => e.ApplicantProfile)
                .HasForeignKey(e => e.Applicant);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(a => a.CompanyJobDescription)
                .WithRequired(e => e.CompanyJob)
                .HasForeignKey(e => e.Job);

            modelBuilder.Entity<CompanyProfilePoco>()
                .HasMany(a => a.CompanyJob)
                .WithRequired(e => e.CompanyProfile)
                .HasForeignKey(e => e.Company);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(a => a.CompanyJobEducation)
                .WithRequired(e => e.CompanyJob)
                .HasForeignKey(e => e.Job);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(a => a.CompanyJobSkill)
                .WithRequired(e => e.CompanyJob)
                .HasForeignKey(e => e.Job);

            modelBuilder.Entity<CompanyProfilePoco>()
               .HasMany(a => a.CompanyLocation)
               .WithRequired(e => e.CompanyProfile)
               .HasForeignKey(e => e.Company);

            modelBuilder.Entity<CompanyProfilePoco>()
               .HasMany(a => a.CompanyDescription)
               .WithRequired(e => e.CompanyProfile)
               .HasForeignKey(e => e.Company);

            modelBuilder.Entity<SecurityLoginPoco>()
               .HasMany(a => a.SecurityLoginsLog)
               .WithRequired(e => e.SecurityLogin)
               .HasForeignKey(e => e.Login);

            modelBuilder.Entity<SecurityLoginPoco>()
               .HasMany(a => a.SecurityLoginsRole)
               .WithRequired(e => e.SecurityLogin)
               .HasForeignKey(e => e.Login);

            modelBuilder.Entity<SecurityRolePoco>()
               .HasMany(a => a.SecurityLoginsRole)
               .WithRequired(e => e.SecurityRole)
               .HasForeignKey(e => e.Role);

            modelBuilder.Entity<SystemLanguageCodePoco>()
               .HasMany(a => a.CompanyDescription)
               .WithRequired(e => e.SystemLanguageCode)
               .HasForeignKey(e => e.LanguageId);


            Database.SetInitializer<CareerCloudContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistorys { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }

    }
}
