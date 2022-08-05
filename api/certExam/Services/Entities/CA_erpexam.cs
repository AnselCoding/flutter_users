using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace certExam.Entities
{
    /// <summary>
    /// 認證考試場次資料檔
    /// </summary>
    [Table("CA_erpexam", Schema = "dbo")]
    public class CA_erpexam
    {
        public int outId { get; set; }
        public string examNo { get; set; }
        public DateTime examDate { get; set; }
        public string examTimeStr { get; set; }
        public int examSeconds { get; set; }
        public string examPlace { get; set; }
        public string examAddr { get; set; }
        public string company { get; set; }
        public string certId { get; set; }
        public string placeSerial { get; set; }
        public string examSerial { get; set; }
        public string statusType { get; set; }
        public string statusId { get; set; }
        public string memo { get; set; }
        public string proctorMemo1 { get; set; }
        public string proctorMemo2 { get; set; }
        public string statusType2 { get; set; }
        public string statusId2 { get; set; }

        public int creatUser { get; set; }
        public DateTime creatTime { get; set; }
        public int actUser { get; set; }
        public DateTime actTime { get; set; }
    }
}