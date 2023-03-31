using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeFixer.Classes
{
    public interface IOreder
    {
        public Client? client { get; set; }
        public ModelClock? clock { get; set; }
    }
}
