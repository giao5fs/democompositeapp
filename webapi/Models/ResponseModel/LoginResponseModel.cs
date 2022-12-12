namespace webapi.Models.ResponseModel
{
    public class LoginResponseModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string SessionToken { get; set; }
    }
}
