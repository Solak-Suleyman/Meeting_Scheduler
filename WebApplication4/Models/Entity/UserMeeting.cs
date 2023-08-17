using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Models.Entity
{
    [Table("user_meetings")]
    public class UserMeeting
    {
        public int Id { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
        public Meeting Meeting { get; set;}
        public User User { get; set;}       
    }
}
