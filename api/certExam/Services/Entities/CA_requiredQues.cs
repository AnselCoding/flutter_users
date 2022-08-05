using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace certExam.Entities
{
    [Table("CA_examQues", Schema = "dbo")]
    public class CA_examQue
    {
        public string certId { get; set; }

        public int quesId { get; set; }

        public int creatUser { get; set; }
        public DateTime creatTime { get; set; }
        public int actUser { get; set; }
        public DateTime actTime { get; set; }
    }
}