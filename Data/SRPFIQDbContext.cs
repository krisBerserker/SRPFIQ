using Microsoft.EntityFrameworkCore;
using System;
using WebApplication_SRPFIQ.Models;

namespace WebApplication_SRPFIQ.Data
{
    public class SRPFIQDbContext : DbContext
    {
        public SRPFIQDbContext(DbContextOptions<SRPFIQDbContext> options) :base(options) { }
      
        public DbSet<Users> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<UserPermissions> UserPermissions { get; set; }
        public DbSet<UserAssignedRequests> UserAssignedRequests { get; set; }
        public DbSet<Requests> Requests { get; set; }
        public DbSet<RequestNotes> RequestNotes { get; set; }
        public DbSet<Meetings> Meetings { get; set; }
        public DbSet<Resources> Resources { get; set; }
        public DbSet<ResourceCities> ResourceCities { get; set; }
        public DbSet<ResourceCategories> ResourceCategories { get; set; }
        public DbSet<ResourceBusinessHours> ResourceBusinessHours { get; set; }
        public DbSet<Resources_ResourceCategories> Resources_ResourceCategories { get; set; }
        public DbSet<Questionnaires> Questionnaires { get; set; }
        public DbSet<QuestionnaireQuestions> QuestionnaireQuestions { get; set; }
        public DbSet<QuestionnaireDataSources> QuestionnaireDataSources { get; set; }
        public DbSet<QuestionnaireDataSourceOptions> QuestionnaireDataSourceOptions { get; set; }
        public DbSet<QuestionnaireAnswers> QuestionnaireAnswers { get; set; }
        public DbSet<QuestionnaireAnswerResults> QuestionnaireAnswerResults { get; set; }
        public DbSet<BirthPlaces> BirthPlaces { get; set; }
        public DbSet<MaternalExperiences> MaternalExperiences { get; set; }
        public DbSet<MaternalExperiencesThemes> MaternalExperiencesThemes { get; set; }
        public DbSet<MaternalExperiences_MaternalExperienceThemes> MaternalExperiences_MaternalExperienceThemes { get; set; }
        public DbSet<MedicalNotes> MedicalNotes { get; set; }
        public DbSet<MedicalTransferReason> MedicalTransferReasons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<UserRoles>().ToTable("UserRoles");
            modelBuilder.Entity<UserPermissions>().ToTable("UserPermissions");
            modelBuilder.Entity<UserAssignedRequests>().ToTable("UserAssignedRequests");
            modelBuilder.Entity<Requests>().ToTable("Requests");
            modelBuilder.Entity<RequestNotes>().ToTable("RequestNotes");

            modelBuilder.Entity<Meetings>().ToTable("Meetings");
            modelBuilder.Entity<Resources>().ToTable("Resources");
            modelBuilder.Entity<ResourceCities>().ToTable("ResourceCities");
            modelBuilder.Entity<ResourceCategories>().ToTable("ResourceCategories");
            modelBuilder.Entity<ResourceBusinessHours>().ToTable("ResourceBusinessHours");
            modelBuilder.Entity<Resources_ResourceCategories>().ToTable("Resources_ResourceCategories");

            modelBuilder.Entity<Questionnaires>().ToTable("Questionnaires");
            modelBuilder.Entity<QuestionnaireQuestions>().ToTable("QuestionnaireQuestions");
            modelBuilder.Entity<QuestionnaireDataSources>().ToTable("QuestionnaireDataSources");
            modelBuilder.Entity<QuestionnaireDataSourceOptions>().ToTable("QuestionnaireDataSourceOptions");
            modelBuilder.Entity<QuestionnaireAnswers>().ToTable("QuestionnaireAnswers");
            modelBuilder.Entity<QuestionnaireAnswerResults>().ToTable("QuestionnaireAnswerResults");

            modelBuilder.Entity<MaternalExperiences>().ToTable("MaternalExperiences");
            modelBuilder.Entity<MaternalExperiencesThemes>().ToTable("MaternalExperiencesThemes");
            modelBuilder.Entity<MaternalExperiences_MaternalExperienceThemes>().ToTable("MaternalExperiences_MaternalExperienceThemes");
            modelBuilder.Entity<BirthPlaces>().ToTable("BirthPlaces");
            modelBuilder.Entity<MedicalNotes>().ToTable("MedicalNotes");
            modelBuilder.Entity<MedicalTransferReason>().ToTable("MedicalTransferReason");




            modelBuilder.Entity<UserPermissions>()
                 .HasOne(up => up.Users)
                 .WithMany(u => u.Permissions)
                 .HasForeignKey(up => up.IdUser);

            modelBuilder.Entity<UserPermissions>()
                .HasOne(up => up.UserRole)
                .WithMany(r => r.Permissions)
                .HasForeignKey(up => up.IdUserRole);
            modelBuilder.Entity<UserAssignedRequests>()
                .HasOne(ua => ua.Users)
                .WithMany(u => u.AssignedRequests)
                .HasForeignKey(ua => ua.IdUser);

            modelBuilder.Entity<UserAssignedRequests>()
                .HasOne(ua => ua.Requests)
                .WithMany(r => r.AssignedUsers)
                .HasForeignKey(ua => ua.IdRequest);
            modelBuilder.Entity<RequestNotes>()
                .HasOne(rn => rn.User)
                .WithMany(u => u.RequestNotes)
                .HasForeignKey(rn => rn.IdUser);

            modelBuilder.Entity<RequestNotes>()
                .HasOne(rn => rn.Request)
                .WithMany(r => r.Notes)
                .HasForeignKey(rn => rn.IdRequest);
            modelBuilder.Entity<MedicalNotes>()
                .HasOne(mn => mn.Users)
                .WithMany(u => u.MedicalNotes)
                .HasForeignKey(mn => mn.IdUser);

            modelBuilder.Entity<MedicalNotes>()
                .HasOne(mn => mn.Requests)
                .WithMany(r => r.MedicalNotes)
                .HasForeignKey(mn => mn.IdRequest);

            modelBuilder.Entity<Resources_ResourceCategories>()
                .HasOne(rc => rc.Resource)
                .WithMany(r => r.Resources_ResourceCategories)
                .HasForeignKey(rc => rc.IdResource);
            modelBuilder.Entity<Resources_ResourceCategories>()
                .HasOne(rc => rc.ResourceCategory)
                .WithMany(r => r.Resources_ResourceCategories)
                .HasForeignKey(rc => rc.IdResourceCategory);
            modelBuilder.Entity<MaternalExperiences_MaternalExperienceThemes>()
                .HasOne(me => me.MaternalExperiences)
                .WithMany(m => m.MaternalExperiences_MaternalExperiencesThemes)
                .HasForeignKey(me => me.IdMaternalExperience);
            modelBuilder.Entity<MaternalExperiences_MaternalExperienceThemes>()
                .HasOne(me => me.MaternalExperiencesThemes)
                .WithMany(m => m.MaternalExperiences_MaternalExperiencesThemes)
                .HasForeignKey(me => me.IdMaternalExperienceTheme);

            modelBuilder.Entity<QuestionnaireAnswerResults>()
                .HasOne(qr => qr.QuestionnaireQuestions)
                .WithMany(q => q.AnswerResults)
                .HasForeignKey(qr => qr.IdQuestionnaireQuestion);
            modelBuilder.Entity<QuestionnaireAnswerResults>()
                .HasOne(qr => qr.QuestionnaireAnswers)
                .WithMany(q => q.AnswerResults)
                .HasForeignKey(qr => qr.IdQuestionnaireAnswer)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuestionnaireAnswers>()
                .HasOne(q => q.Users)
                .WithMany(u => u.QuestionnaireAnswers)
                .HasForeignKey(q => q.IdUser);

            modelBuilder.Entity<QuestionnaireAnswers>()
                .HasOne(q => q.Requests)
                .WithMany(r => r.QuestionnaireAnswers)
                .HasForeignKey(q => q.IdRequest);

            modelBuilder.Entity<QuestionnaireAnswers>()
                .HasOne(q => q.Questionnaires)
                .WithMany(qn => qn.Answers)
                .HasForeignKey(q => q.IdQuestionnaire);

            modelBuilder.Entity<Meetings>()
                .Property(m => m.MeetingNumber)
                .ValueGeneratedNever();
        }
    }
}
