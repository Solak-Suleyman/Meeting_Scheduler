using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Models.Entity
{
    [Table("meetings")]
    public class Meeting
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set;}
        public Room Room { get; set; }
        public int RoomId { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
        //public UserMeeting UserMeeting { get; set; }
        public ICollection<UserMeeting> UserMeeting { get; set; }

    }
}
