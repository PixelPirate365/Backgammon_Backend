using AuthService.Application.Interfaces;
using AuthService.Domain.Common;
using AuthService.Domain.Entities;
using AuthService.Identity.Expressions;
using FluentAssertions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using System.Reflection;

namespace AuthService.Persistence.Data {
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        private readonly ICurrentUserService _currentUserService;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService) : base(options) {
            _currentUserService = currentUserService;
        }
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }

        protected virtual bool IsSoftDeleteFilterEnabled => true;

        private static readonly MethodInfo ConfigureGlobalFiltersMethodInfo = 
            typeof(ApplicationDbContext).GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic);

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()) {
            foreach (var entry in ChangeTracker.Entries().ToList()) {
                switch (entry.State) {
                    case EntityState.Added:
                        SetCreationAuditProperties(entry.Entity, _currentUserService.UserId!);
                        break;

                    case EntityState.Modified:
                        SetModificationAuditProperties(entry.Entity, _currentUserService.UserId!);
                        break;

                    case EntityState.Deleted:
                        CancelDeletionForSoftDelete(entry);
                        SetModificationAuditProperties(entry.Entity, _currentUserService.UserId!);
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder) {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
            foreach (var entityType in builder.Model.GetEntityTypes()) {
                ConfigureGlobalFiltersMethodInfo
                    .MakeGenericMethod(entityType.ClrType)
                    .Invoke(this, new object[] { builder, entityType });
            }
        }
        private static bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity))) {
                return true;
            }
            return false;
        }
        private Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
            where TEntity : class {
            Expression<Func<TEntity, bool>> expression = null!;
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity))) {
                Expression<Func<TEntity, bool>> softDeleteFilter = e => !IsSoftDeleteFilterEnabled || !((ISoftDelete)e).IsDeleted;
                expression = expression == null ? softDeleteFilter : CombineExpressions(expression, softDeleteFilter);
            }
            return expression!;
        }
        private static Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2) {
            return ExpressionCombiner.Combine(expression1, expression2);
        }
        private void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType)
            where TEntity : class {
            if (entityType.BaseType == null && ShouldFilterEntity<TEntity>(entityType)) {
                var filterExpression = CreateFilterExpression<TEntity>();
                if (filterExpression != null) {
                    modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);
                }
            }
        }
        private static void SetCreationAuditProperties(object entityAsObj, string userId) {
            var entityWithCreationTime = entityAsObj as IHasCreationTime;

            if (entityWithCreationTime == null) {
                //Object does not implement IHasCreationTime
                return;
            }
            if (entityWithCreationTime.CreatedAt == default(DateTime)) {
                entityWithCreationTime.CreatedAt = DateTime.UtcNow;
            }
            if (entityAsObj is not ICreationAudited) {
                //Object does not implement ICreationAudited
                return;
            }
            if (string.IsNullOrWhiteSpace(userId)) {
                //Unknown user
                return;
            }
            var entity = entityAsObj as ICreationAudited;
            if (entity.CreatedBy != null) {
                return;
            }
            entity.CreatedBy = userId;
        }
        private static void SetModificationAuditProperties(object entityAsObj, string userId) {
            if (entityAsObj is IHasModificationTime) {
                entityAsObj.As<IHasModificationTime>().ModifiedAt = DateTime.UtcNow;
            }
            if (entityAsObj is not IModificationAudited) {
                //Entity does not implement IModificationAudited
                return;
            }
            var entity = entityAsObj.As<IModificationAudited>();

            if (userId == null) {
                entity.ModifiedBy = null;
                return;
            }
            entity.ModifiedBy = userId;
        }
        private static void CancelDeletionForSoftDelete(EntityEntry entry) {
            if (entry.Entity is not ISoftDelete) {
                return;
            }
            entry.Reload();
            entry.State = EntityState.Modified;
            entry.Entity.As<ISoftDelete>().IsDeleted = true;
        }
    }
}
