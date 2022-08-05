using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace certExam.Entities
{
    [Table("CA_erpexamPersons", Schema = "dbo")]
    public class CA_erpexamPerson
    {
        public int outId { get; set; }
        public string pId { get; set; }
        public string personCName { get; set; }
        public string personEName { get; set; }
        public bool chkPId { get; set; }
        public string memo1 { get; set; }
        public string memo2 { get; set; }
        public int deduction { get; set; }
        public int originScore { get; set; }
        public int score { get; set; }
        public bool chkPass { get; set; }
        public string statusType { get; set; }
        public string statusId { get; set; }
        public int remainSeconds { get; set; }
        public int answeredQues { get; set; }
        public DateTime examStartTime { get; set; }
        public DateTime examEndTime { get; set; }
    }
}