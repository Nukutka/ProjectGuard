using Microsoft.EntityFrameworkCore;
using ProjectGuard.Ef.Entities;

namespace ProjectGuard.Ef.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeqStartAt<TEntity>(this ModelBuilder modelBuilder, int idStartValue)
           where TEntity : BaseEntity
        {
            var seqName = $"{typeof(TEntity).Name}_seq";

            modelBuilder.HasSequence<int>(seqName)
                .StartsAt(idStartValue)
                .IncrementsBy(1);

            modelBuilder.Entity<TEntity>()
                .Property(p => p.Id)
                .HasDefaultValueSql($"nextval('\"{seqName}\"')");
        }
    }
}
