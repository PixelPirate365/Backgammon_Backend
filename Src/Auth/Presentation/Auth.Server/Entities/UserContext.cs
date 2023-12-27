using Auth.Server.Expressions;
using Auth.Server.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using System.Reflection;

namespace Auth.Server.Entities {
    public class UserContext : IdentityDbContext<User> {
        private readonly ICurrentUserService _currentUserService;
        protected virtual bool IsSoftDeleteFilterEnabled => true;
        private static readonly MethodInfo ConfigureGlobalFiltersMethodInfo = 
            typeof(UserContext).GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic);
        public UserContext(DbContextOptions<UserContext> options, ICurrentUserService currentUserService) : base(options) {
            _currentUserService = currentUserService;
        }
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
        }
        private static void CancelDeletionForSoftDelete(EntityEntry entry) {
            if (entry.Entity is not ISoftDelete) {
                return;
            }

            entry.Reload();
            entry.State = EntityState.Modified;
            entry.Entity.As<ISoftDelete>().IsDeleted = true;
        }
        private static bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity))) {
                return true;
            }

            return false;
        }
        private Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
            where TEntity : class {
            Expression<Func<TEntity, bool>> expression = null;
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
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()) {
            foreach (var entry in ChangeTracker.Entries().ToList()) {
                switch (entry.State) {
                    case EntityState.Added:
                        SetCreationAuditProperties(entry.Entity, _currentUserService.UserId);
                        break;
                    case EntityState.Modified:
                        SetModificationAuditProperties(entry.Entity, _currentUserService.UserId);
                        break;
                    case EntityState.Deleted:
                        CancelDeletionForSoftDelete(entry);
                            break;

                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        private static void SetCreationAuditProperties(object entityAsObj, string userId) {
            var entityWithCreationTime = entityAsObj as IHasCreationTime;
            if (entityWithCreationTime == null) {
                return;
            }
            if (entityWithCreationTime.CreatedDate == default(DateTime)) {
                entityWithCreationTime.CreatedDate = DateTime.UtcNow;
            }
            if (!(entityAsObj is ICreationAudited)) {

                return;
            }
            if (string.IsNullOrWhiteSpace(userId)) {

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
                entityAsObj.As<IHasModificationTime>().ModifiedDate = DateTime.UtcNow;
            }
            if (!(entityAsObj is IModificationAudited)) {
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
    }
}
