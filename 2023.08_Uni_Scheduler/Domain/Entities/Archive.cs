using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023._08_Uni_Scheduler.Domain.Entities
{
    public class Archive
    {
        public int Id { get; set; }
        public string description { get; set; }
        public string titleReport { get; set; }
        public string query { get; set; }
        public string format { get; set; }        
        public DataTable data { get; set; }
    }
}
