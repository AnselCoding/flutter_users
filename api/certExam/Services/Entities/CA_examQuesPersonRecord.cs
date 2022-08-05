using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace certExam.Entities
{
    [Table("CA_examQuesPersonRecord", Schema = "dbo")]
    public class CA_examQuesPersonRecord
    {
        public int outId { get; set; }
        public string pId { get; set; }
        public int personSerial { get; set; }
        public int quesSerial { get; set; }
        public string personAnswer { get; set; }
        public bool chkCorrect { get; set; }
        public int appScoreSingle { get; set; }
        public bool quesMark { get; set; }
    }
}