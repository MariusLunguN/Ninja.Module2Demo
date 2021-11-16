using NinjaDomain.Classes;
using NinjaDomain.Interfaces;
using System;
using System.Collections.Generic;

namespace NinjaDomain
{
    public class Clan : IModificationHistory
    {
        public Clan()
        {
            this.Ninjas = new List<Ninja>();
        }

        public int Id { get; set; }
        public string ClanName { get; set; }
        public List<Ninja> Ninjas { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDirty { get; set; }
    }
}
