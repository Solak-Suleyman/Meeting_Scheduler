using WebApplication4.Models.Entity;

namespace WebApplication4.Models.DTO
{
    public class MeetingDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
        public Room Room { get; set; }

    }
}
