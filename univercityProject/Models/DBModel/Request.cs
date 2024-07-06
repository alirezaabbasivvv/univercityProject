using System;
using System.ComponentModel.DataAnnotations;

namespace univercityProject.Models.DBModel
{
    public class Request
    {
        [Key]
        public int Req_ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public bool ManagerAccepted { get; set; }
        public bool CEOIsAccepted { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Doscription { get; set; }
        public User User { get; set; }

        public int User_id { get; set; }
    }
}
