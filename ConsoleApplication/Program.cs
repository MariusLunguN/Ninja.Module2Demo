using NinjaDomain.Classes;
using NinjaDomain.DataModel;
using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //InsertNinja();

            //InsertMultipleNinjas();

            SimpleNinjaQuery();

            SimpleNinjaGraphQuery();

            QueryAndUpdateNinja();

            QueryAndUpdateNinjaDisconnected();

            DeleteNinja();




            Console.ReadKey();
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
