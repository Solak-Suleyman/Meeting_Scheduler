using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Models.Entity
{
    [Table("rooms")]
    public class Room
    {
        public int Id { get; set; }
        public string name { get; set; }
        public char status { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
        //public Meeting Meeting { get; set; }
        ICollection<Meeting> Meeting { get; set; }

    }
}
