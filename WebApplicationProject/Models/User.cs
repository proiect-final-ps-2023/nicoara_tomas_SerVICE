namespace WebApplicationProject.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public DateTime UserDate  { get; set; }
        public string UserAdress { get; set; }
        //public string UserNickname {  get; set; }
        public User()
        {

        }
    }
}
