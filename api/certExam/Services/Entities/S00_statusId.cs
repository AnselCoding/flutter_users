using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace certExam.Entities
{
    [Table("S00_statusId", Schema = "dbo")]
    public class S00_statusId
    {
        public string statusType { get; set; }
        public string statusId { get; set; }
        public string statusName { get; set; }
        public bool isFixed { get; set; }
        public bool isUse { get; set; }
        public int creatUser { get; set; }
        public DateTime creatTime { get; set; }
        public int actUser { get; set; }
        public DateTime actTime { get; set; }
    }
}