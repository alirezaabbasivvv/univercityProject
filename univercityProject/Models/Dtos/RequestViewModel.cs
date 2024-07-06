using System;

namespace univercityProject.Models.Dtos
{
    public class RequestListViewModel
    {
        public int UserID { get; set; }
        public string  User_LastName { get; set; }
        public string  User_FirstName { get; set; }
        public int Req_ID { get; set; }
        public string Title { get; set; }
    }
    public class RequestViewModel
    {
        public int UserID { get; set; }
        public string User_LastName { get; set; }
        public string User_FirstName { get; set; }
        public int Req_ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public bool ManagerAccepted { get; set; }
        public bool CEOIsAccepted { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Doscription { get; set; }
    }
}
