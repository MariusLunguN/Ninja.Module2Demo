using NinjaDomain.Classes;
using NinjaDomain.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace NinjaDomain.DataModel
{
    public class NinjaContext : DbContext
    {
        public DbSet<Ninja> Ninjas { get; set; }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<NinjaEquipment> Equipment { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types().Configure(c => c.Ignore("IsDirty"));
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            foreach (var history in this.ChangeTracker.Entries()
                .Where(e => e.Entity is IModificationHistory && (e.State == EntityState.Added || e.State == EntityState.Modified))
                .Select(e => e.Entity as IModificationHistory))
            {
                history.DateModified = System.DateTime.Now;

                if (history.DateCreated == System.DateTime.MinValue)
                {
                    history.DateCreated = System.DateTime.Now;
                }

            }

            int result = base.SaveChanges();

            foreach (var item in this.ChangeTracker.Entries().Where(e => e.Entity is IModificationHistory).Select(e => e.Entity as IModificationHistory))
            {
                item.IsDirty = false;
            }

            return result;
        }
    }
}
