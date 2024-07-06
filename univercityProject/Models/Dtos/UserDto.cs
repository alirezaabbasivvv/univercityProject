using univercityProject.Models.DBModel;

namespace univercityProject.Models.Dtos
{
    public class UserDto
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public Rules Rule { get; set; }
        public string Password { get; set; }
    }
}
