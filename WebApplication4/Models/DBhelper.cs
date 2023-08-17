
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models.Context;
using WebApplication4.Models.DTO;
using WebApplication4.Models.Entity;

namespace WebApplication4.Models
{
    public class DBhelper
    {
        private MeetingSchedulerContext _context;
        public  DBhelper(MeetingSchedulerContext context)
        {
            _context = context;
        }
        //Get all users from database
        public List<UsersDTO> GetUsers()
        {
            List<UsersDTO> response = new List<UsersDTO>();
            var dataList=_context.Users.ToList();
            dataList.ForEach(row => response.Add(new UsersDTO()
            {
                id=row.id,
                user_name=row.user_name,
                name=row.name,
                surname=row.surname,
                password=row.password,
                status=row.status,

            }));
            return response;
        }
        public UsersDTO GetUserById(int id)
        {
            UsersDTO response = new UsersDTO();
            var row = _context.Users.Where(d => d.id.Equals(id)).FirstOrDefault();
            return new UsersDTO()
            {
                id = row.id,
                user_name = row.user_name,
                name = row.name,
                surname = row.surname,
                password = row.password,
                status = row.status,
            };
        }
        public UsersDTO GetUserByUserName(string user_name) { 
            UsersDTO response=new UsersDTO();
            var row =_context.Users.Where(d=>d.user_name.Equals(user_name)).FirstOrDefault();
            return new UsersDTO()
            {
                id = row.id,
                user_name = row.user_name,
                name = row.name,
                surname = row.surname,
                password = row.password,
                status = row.status,
            };
        }
        public void AddUser(UsersDTO user)
        {
            User dbTable = new User();
            if (user.id > 0)
            {//PUT
                dbTable = _context.Users.Where(d => d.id.Equals(user.id)).FirstOrDefault();
                if (dbTable != null)
                {

                }
            }
            else
            {//Post
                dbTable.id = user.id;
                dbTable.name = user.name;
                dbTable.user_name = user.user_name;
                dbTable.surname = user.surname;
                dbTable.password = user.password;
                dbTable.status = user.status;
                dbTable.created_time=DateTime.UtcNow;
                dbTable.updated_time=DateTime.UtcNow;
                _context.Users.Add(dbTable);
            }
            _context.SaveChanges();

        }
    }
}
