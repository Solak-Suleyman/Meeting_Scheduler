using WebApplication4.Models.Entity;

namespace WebApplication4.Models.DTO
{
    public class UserMeetingDTO
    {
        public int Id { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
        public int MeetingId { get; set; }
        public int UserId { get; set; }
    }
}
