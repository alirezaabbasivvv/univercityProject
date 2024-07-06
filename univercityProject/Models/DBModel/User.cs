namespace univercityProject.Models.DBModel
{
    public class User
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public Rules Rule { get; set; }
        public bool IsRemoved { get; set; }

    }
    public enum Rules
    {
        Employ = 1,
        ProjectManager = 2,
        CEO = 3
    }
}
