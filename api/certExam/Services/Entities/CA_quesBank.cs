using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace certExam.Entities
{
    [Table("CA_quesBank", Schema = "dbo")]
    public class CA_quesBank
    {
        public int quesId { get; set; }
        public int sourceQuesId { get; set; }
        public int parentQuesId { get; set; }
        public string bookId { get; set; }
        public string chapId { get; set; }
        public string statusType1 { get; set; }
        public string statusId1 { get; set; }
        public string statusType2 { get; set; }
        public string statusId2 { get; set; }
        public string quesRef { get; set; }
        public string difcltLevelCode { get; set; }
        public int weight { get; set; }
        public string namedPerson { get; set; }
        public string checkedPerson { get; set; }
        public string section { get; set; }
        public string page { get; set; }
        public string quesDesc { get; set; }
        public byte[] quesPic { get; set; }
        public string quesAnswer { get; set; }
        public string version { get; set; }
        public string quesSource { get; set; }
        public string statusType3 { get; set; }
        public string statusId3 { get; set; }
        public int complainCount { get; set; }
        public string memo { get; set; }
        public string otherMemo1 { get; set; }
        public string otherMemo2 { get; set; }
        public string otherMemo3 { get; set; }
        public DateTime startDate { get; set; }
        public DateTime stopDate { get; set; }
        public DateTime suggestStopDate { get; set; }

        public int creatUser { get; set; }
        public DateTime creatTime { get; set; }
        public int actUser { get; set; }
        public DateTime actTime { get; set; }
    }
}