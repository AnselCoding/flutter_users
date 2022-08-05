using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace certExam.Entities
{
    [Table("CA_certificate", Schema = "dbo")]
    public class CA_certificate
    {
        [Key]
        public int quesId { get; set; }
        public string certItem { get; set; }
        public string certModule { get; set; }
        public string certUnit { get; set; }
        public int passScore { get; set; }
        public int perfectScore { get; set; }
        public int drawAmount { get; set; }
        public int examSeconds { get; set; }
        public decimal passRate { get; set; }
        public string statusType { get; set; }
        public string statusId { get; set; }
        public string memo { get; set; }
        public int creatUser { get; set; }
        public DateTime creatTime { get; set; }
        public int actUser { get; set; }
        public DateTime actTime { get; set; }
        public int appScore { get; set; }
    }
}