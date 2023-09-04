using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Models.Entity
{
    [Table("users")]
    public class User
    {

        public int id { get; set; }
        public string user_name { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string password { get; set; }
        public char status { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
        //public UserMeeting UserMeeting { get; set; }

        public ICollection<UserMeeting>? UserMeeting { get; set; }=new List<UserMeeting>();
    }
}
