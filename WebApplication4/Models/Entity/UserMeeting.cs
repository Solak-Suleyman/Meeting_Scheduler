using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Models.Entity
{
    [Table("user_meetings")]
    public class UserMeeting
    {
        public int Id { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
        [ForeignKey("MeetingId")]
        public  Meeting? Meeting { get; set; }
        public int MeetingId { get; set; }
        
        [ForeignKey(nameof(UserId))]

        public virtual User User { get; set; }
        public int UserId { get; set; }

        



    }
}