using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordlee.DataBase
{
    public class Resolved
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WordId { get; set; }
        public User? User { get; set; }
        public Word? Word { get; set; }
    }
}
