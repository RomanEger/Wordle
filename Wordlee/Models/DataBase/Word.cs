using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordlee.DataBase
{
    class Word
    {
        public int Id { get; set; }
        public string WordName { get; set; }
        public List<Resolved> Resolveds { get; set; } = new List<Resolved>();
    }
}
