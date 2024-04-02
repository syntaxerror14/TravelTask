using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EmployeeTravelTask.Models
{
    public partial class TravelPlannerContext : DbContext
    {
        public TravelPlannerContext()
        {
        }

        public TravelPlannerContext(DbContextOptions<TravelPlannerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<GradesHistory> GradesHistories { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<TravelBudgetAllocation> TravelBudgetAllocations { get; set; } = null!;
        public virtual DbSet<TravelRequest> TravelRequests { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("server=LTIN215334\\SQLEXPRESS19; database=TravelPlanner;TrustServerCertificate=True; trusted_connection=true;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grade>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(25).IsUnicode(false); }); 

            modelBuilder.Entity<GradesHistory>(entity =>
            {
                entity.ToTable("GradesHistory");
                entity.Property(e => e.AssignedOn).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.GradesHistories)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__GradesHis__Emplo__35BCFE0A");

                entity.HasOne(d => d.Grades)
                    .WithMany(p => p.GradesHistories)
                    .HasForeignKey(d => d.GradeId)
                    .HasConstraintName("FK__GradesHis__Grade__34C8D9D1");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TravelBudgetAllocation>(entity =>
            {
                entity.Property(e => e.ApprovedHotelStarRating)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovedModeOfTravel)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.TravelRequest)
                    .WithMany(p => p.TravelBudgetAllocations)
                    .HasForeignKey(d => d.TravelRequestId)
                    .HasConstraintName("FK__TravelBud__Trave__2B3F6F97");
            });

            modelBuilder.Entity<TravelRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__TravelRe__33A8517A59D76985");

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.Priority)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.PurposeOfTravel)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RequestApprovedOn).HasColumnType("date");

                entity.Property(e => e.RequestRaisedOn).HasColumnType("date").HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RequestStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ToBeApprovedByHrid).HasColumnName("ToBeApprovedByHRId");

                entity.Property(e => e.ToDate).HasColumnType("date");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.TravelRequests)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__TravelReq__Locat__276EDEB3");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK__Users__7AD04F11C5DAD8BC");

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                  
                entity.HasOne(d => d.CurrentGrade)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CurrentGradeId)
                    .HasConstraintName("User_fk");
            });

            modelBuilder.Entity<TravelRequest>()
                .HasCheckConstraint("CK__TravelRequest__1234","ToDate > FromDate");

            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1, Name = "Goa" },
                new Location { Id = 2, Name = "Shimla" },
                new Location { Id = 3, Name = "Manali" }
                );
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
