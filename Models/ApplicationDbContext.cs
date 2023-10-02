using System;
using System.Collections.Generic;
using Enquiry.Web.Extensions;
using Enquiry.Web.Models.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Enquiry.Web.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual DbSet<Clients> Clients { get; set; } = null!;
        public virtual DbSet<Projects> Projects { get; set; } = null!;
        public virtual DbSet<Phase> Phase { get; set; } = null!;
        public virtual DbSet<Payments> Payments { get; set; } = null!;
        public virtual DbSet<WorkStatus> WorkStatus { get; set; } = null!;
        public virtual DbSet<PaymentMode> PaymentMode { get; set; } = null!;
        public virtual DbSet<History> History { get; set; } = null!;
        public virtual DbSet<Employees> Employees { get; set; } = null!;
        public virtual DbSet<Department> Department { get; set; } = null!;
        public virtual DbSet<Roles> Roles { get; set; } = null!;
        public virtual DbSet<Publication> Publication { get; set; } = null!;
        public virtual DbSet<Visitors> Visitors { get; set; } = null!;
        public virtual DbSet<JournalStatus> JournalStatus { get; set; } = null!;
        public virtual DbSet<PhaseHistory> PhaseHistory { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
        .HasData(
            new Department { DeptId = 1, DeptName = "BDA", IsActive = true },
            new Department { DeptId = 2, DeptName = "Tech", IsActive = true },
            new Department { DeptId = 3, DeptName = "Programming", IsActive = true },
            new Department { DeptId = 4, DeptName = "Publication", IsActive = true }
        );

            modelBuilder.Entity<Roles>()
        .HasData(
            new Roles { RoleId = 1, RoleName = "Manager", IsActive = true },
            new Roles { RoleId = 2, RoleName = "Team Lead", IsActive = true },
            new Roles { RoleId = 3, RoleName = "Executive", IsActive = true }
        );

            modelBuilder.Entity<WorkStatus>()
        .HasData(
            new WorkStatus { WorkStatusId = 1, WorkStatusName = "Assigned", IsActive = true },
            new WorkStatus { WorkStatusId = 2, WorkStatusName = "Progress", IsActive = true },
            new WorkStatus { WorkStatusId = 3, WorkStatusName = "Hold", IsActive = true },
            new WorkStatus { WorkStatusId = 4, WorkStatusName = "Publication", IsActive = true },
            new WorkStatus { WorkStatusId = 5, WorkStatusName = "Revision", IsActive = true },
            new WorkStatus { WorkStatusId = 6, WorkStatusName = "Accepted", IsActive = true },
            new WorkStatus { WorkStatusId = 7, WorkStatusName = "Rejected", IsActive = true },
            new WorkStatus { WorkStatusId = 8, WorkStatusName = "Completed", IsActive = true }
        );

            modelBuilder.Entity<PaymentMode>()
        .HasData(
            new PaymentMode { PaymentId = 1, PaymentType = "Cash", IsActive = true },
            new PaymentMode { PaymentId = 2, PaymentType = "Other", IsActive = true },
            new PaymentMode { PaymentId = 3, PaymentType = "Bank", IsActive = true },
            new PaymentMode { PaymentId = 4, PaymentType = "Link", IsActive = true }
        );
            modelBuilder.Entity<JournalStatus>()
        .HasData(
            new JournalStatus { Id = 1, Name = "Submitted", IsActive = true },
            new JournalStatus { Id = 2, Name = "Rejected", IsActive = true },
            new JournalStatus { Id = 3, Name = "Major", IsActive = true },
            new JournalStatus { Id = 4, Name = "Minor", IsActive = true },
            new JournalStatus { Id = 5, Name = "Accepted", IsActive = true },
            new JournalStatus { Id = 6, Name = "Published", IsActive = true },
            new JournalStatus { Id = 7, Name = "MailAccount", IsActive = true },
            new JournalStatus { Id = 8, Name = "ToApply", IsActive = true }
        );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        private void UpdateAuditEntities()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));


            foreach (var entry in modifiedEntries)
            {
                var entity = (IAuditableEntity)entry.Entity;
                DateTime now = DatetimeExtension.IndiaStandardTimeNow();

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                    if (_httpContextAccessor.HttpContext is not null && _httpContextAccessor.HttpContext.Session.GetInt32("CurrentUserId") is not null)
                        entity.CreatedBy = (int)_httpContextAccessor.HttpContext.Session.GetInt32("CurrentUserId");
                }
                else
                {
                    base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                entity.UpdatedDate = now;
                if (_httpContextAccessor.HttpContext is not null && _httpContextAccessor.HttpContext.Session.GetInt32("CurrentUserId") is not null)
                    entity.UpdatedBy = (int)_httpContextAccessor.HttpContext.Session.GetInt32("CurrentUserId");
            }
        }

        public override int SaveChanges()
        {
            UpdateAuditEntities();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateAuditEntities();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            UpdateAuditEntities();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

    }
}
