using NinjaDomain.Classes;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NinjaDomain.DataModel
{
    public class DisconnectedRepository
    {
        public List<Ninja> GetNinjasWithClan()
        {
            using (var context = new NinjaContext())
            {
                return context.Ninjas.AsNoTracking().Include(n => n.Clan).ToList();
            }
        }

        public Ninja GetNinjaWithEquipment(int id)
        {
            using (var context = new NinjaContext())
            {
                return context.Ninjas.AsNoTracking().AsNoTracking().Include(n => n.EquipmentOwned).FirstOrDefault(a => a.Id == id);
            }
        }

        public Ninja GetNinjaWithEquipmentAndClan(int id)
        {
            using (var context = new NinjaContext())
            {
                return context.Ninjas.AsNoTracking().Include(n => n.EquipmentOwned).Include(c => c.Clan).FirstOrDefault(e => e.Id == id);
            }
        }

        public IEnumerable GetClanList()
        {
            using (var context = new NinjaContext())
            {
                return context.Clans.AsNoTracking().OrderBy(c => c.ClanName).Select(c => new { c.Id, c.ClanName }).ToList();
            }
        }

        public Ninja GetNinjaById(int id)
        {
            using (var context = new NinjaContext())
            {
                return context.Ninjas.AsNoTracking().SingleOrDefault(n => n.Id == id);
            }
        }

        public void SaveUpdatedNinja(Ninja ninja)
        {
            using (var context = new NinjaContext())
            {
                context.Entry(ninja).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void SaveNewNinja(Ninja ninja)
        {
            using (var context = new NinjaContext())
            {
                context.Ninjas.Add(ninja);
                context.SaveChanges();
            }
        }

        public void DeleteNinja(int id)
        {
            using (var context = new NinjaContext())
            {
                var ninja = context.Ninjas.Find(id);

                context.Entry(ninja).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void SaveNewEquipment(NinjaEquipment equipment, int ninjaId)
        {
            using (var context = new NinjaContext())
            {
                var ninja = context.Ninjas.Find(ninjaId);
                ninja.EquipmentOwned.Add(equipment);

                context.SaveChanges();
            }
        
        }

        public void SaveUpdatedEquipment(NinjaEquipment equipment, int ninjaId)
        {
            using (var context = new NinjaContext())
            {
                var equipmentWithNinjaFromDatabase = context.Equipment.Include(a => a.Ninja).FirstOrDefault(e => e.Id == equipment.Id);

                context.Entry(equipmentWithNinjaFromDatabase).CurrentValues.SetValues(equipment);

                context.SaveChanges();
            }
        }

    }
}
