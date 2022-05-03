using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rds_ataskaitos
{
    public class RDS_model
    {
        public string databaseTimestampCreated { get; set; }
        public string rdsTimestampCreated { get; set; }
        public string senderId { get; set; }
        public string senderName { get; set; }
        public string title { get; set; }
        public bool archive { get; set; }
        public int unixStart { get; set; }
        public int unixEnd { get; set; }
        public string titleId { get; set; }
        public string artist { get; set; }
        public string rating { get; set; }
        public string description { get; set; }
        public string channel_id { get; set; }
        public string showTitle { get; set; }
        public string year { get; set; }
    }
}
