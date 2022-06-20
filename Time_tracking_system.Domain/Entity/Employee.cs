using Microsoft.AspNetCore.Identity;

namespace Time_tracking_system.Domain.Entity
{
    //Пользователь
    public class Employee : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Post { get; set; }
    }
}
