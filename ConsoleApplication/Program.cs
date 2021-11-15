using NinjaDomain.Classes;
using NinjaDomain.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //InsertNinja();

            //InsertMultipleNinjas();

            //SimpleNinjaQuery();

            //QueryAndUpdateNinja();

            //QueryAndUpdateNinjaDisconnected();

            //RetrieveDataWithFind();

            //SimpleNinjaGraphQuery() ;

            //DeleteNinja();

            InsertNinjaWithEquipment();

            Console.ReadKey();
        }

        private static void InsertNinjaWithEquipment()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = new Ninja()
                {
                    Name = "Kacy Catanzoro",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1990, 1, 14),
                    ClanId = 1
                };

                var muscles = new NinjaEquipment
                {
                    Name = "Muscles",
                    Type = EquipmentType.Tool
                };

                var spunk = new NinjaEquipment
                {
                    Name = "Spunk (adica curaj)",
                    Type = EquipmentType.Weapon
                };

                context.Ninjas.Add(ninja);
                ninja.EquipmentOwned.Add(muscles);
                ninja.EquipmentOwned.Add(spunk);

                context.SaveChanges();

            }
        }

        private static void DeleteNinja()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.FirstOrDefault();
                context.Ninjas.Remove(ninja);
                context.SaveChanges();

            }
        }

        private static void RetrieveDataWithFind()
        {
            var keyVal = 2;
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.Find(keyVal);
                Console.WriteLine($"After find #1: {ninja.Name}");

                context.Database.Log = Console.WriteLine;
                var ninja2 = context.Ninjas.Find(keyVal);
                Console.WriteLine($"After find #1: {ninja2.Name}");
            }

        }

        private static void QueryAndUpdateNinjaDisconnected()
        {
            Ninja ninja;

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                ninja = context.Ninjas.FirstOrDefault();
            }

            ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Entry(ninja).State = EntityState.Modified;
                context.SaveChanges();
            }

        }

        private static void QueryAndUpdateNinja()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.FirstOrDefault();
                ninja.ServedInOniwaban = (!ninja.ServedInOniwaban);
                context.SaveChanges();
            }
        }

        private static void SimpleNinjaQuery()
        {
            using (var context = new NinjaContext())
            {
                var ninja = context.Ninjas
                    .Where(n => n.Name.Contains("M"))
                    .OrderBy(n => n.Name)
                    .Skip(1)
                    .Take(1)
                    .FirstOrDefault();

                Console.WriteLine(ninja?.Name);
            }
        }

        private static void InsertNinja()
        {
            var ninja = new Ninja
            {
                Name = "JulieSan",
                ServedInOniwaban = false,
                DateOfBirth = new System.DateTime(1980, 1, 1),
                ClanId = 1
            };

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.Add(ninja);
                context.SaveChanges();
            }
        }

        private static void InsertMultipleNinjas()
        {
            var ninja2 = new Ninja
            {
                Name = "Marius",
                ServedInOniwaban = false,
                DateOfBirth = new System.DateTime(1990, 1, 1),
                ClanId = 1
            };

            var ninja3 = new Ninja
            {
                Name = "Samson",
                ServedInOniwaban = false,
                DateOfBirth = new System.DateTime(1986, 1, 1),
                ClanId = 1
            };

            var ninja4 = new Ninja
            {
                Name = "Leonardo",
                ServedInOniwaban = false,
                DateOfBirth = new System.DateTime(1988, 1, 1),
                ClanId = 1
            };

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.AddRange(new List<Ninja> { ninja2, ninja3, ninja4 });
                context.SaveChanges();
            }

        }
    }
}
