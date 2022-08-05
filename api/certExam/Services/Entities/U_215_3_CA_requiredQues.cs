using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace certExam.Entities
{
    [Table("U_215_3_CA_requiredQues", Schema = "dbo")]
    public class U_215_3_CA_requiredQue
    {
        public string certId { get; set; }

        public int quesId { get; set; }
        public string bookId { get; set; }
        public string chapId { get; set; }
        public string quesRef { get; set; }
        public string statusId2 { get; set; }
        public string difcltLevelCode { get; set; }

        public int creatUser { get; set; }
        public DateTime creatTime { get; set; }
        public int actUser { get; set; }
        public DateTime actTime { get; set; }
    }
}