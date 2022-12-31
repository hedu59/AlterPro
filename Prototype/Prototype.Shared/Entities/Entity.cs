using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Shared.Entities
{
    public class Entity
    {
        public Entity()
        {
            CreatedDate = DateTime.UtcNow;
            Active = true;
        }

        public long Id { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public bool Active { get; private set; }

        public void Disable()
        {
            Active = false;
        }
    }
}
