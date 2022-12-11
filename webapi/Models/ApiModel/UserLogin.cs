namespace webapi.Models.ApiModel
{
    public class UserLogin
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string SessionToken { get; set; }
    }
}
