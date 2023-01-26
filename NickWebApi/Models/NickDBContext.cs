using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace NickWebApi.Models
{
    public partial class NickDBContext : DbContext
    {
        public NickDBContext()
        {
        }
        public IConfiguration Configuration { get; }  //Added this line to hide Database Connection
        public NickDBContext(DbContextOptions<NickDBContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<Incident> Incidents { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
              
                optionsBuilder.UseSqlServer(Configuration["AppSettings:ConnectionString"]);
               // optionsBuilder.UseSqlServer("Server=DESKTOP-4KU4P0A;Database=NickDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Incident>(entity =>
            {
                entity.HasKey(e => e.IncidentCode)
                    .HasName("PK_Orders");

                entity.ToTable("Incident");

                entity.Property(e => e.IncidentCode).HasMaxLength(50);

                entity.Property(e => e.IncidentDate).HasColumnType("datetime");

                entity.Property(e => e.IncidentLocation).HasMaxLength(50);

                entity.Property(e => e.IncidentName).HasMaxLength(50);

                entity.Property(e => e.IncidentRecordBy).HasMaxLength(50);
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.UserAccountCode);

                entity.ToTable("UserAccount");

                entity.Property(e => e.UserAccountCode).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PasswordHash).HasMaxLength(50);

                entity.Property(e => e.PasswordSalt).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
