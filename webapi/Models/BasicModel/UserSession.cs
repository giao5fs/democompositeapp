using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models.BasicModel
{
    [Table("UserSessions")]
    public class UserSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
        [Required]
        public Guid SessionToken { get; private set; }
        [Required]
        public DateTime CreateAt { get; private set; }

        public void GenerateNewSessionToken()
        {
            SessionToken = Guid.NewGuid();
            CreateAt = DateTime.Now;
        }

        public static UserSession CreateSessionTokenForUser(int _userId)
        {
            var session = new UserSession { UserId = _userId };
            session.GenerateNewSessionToken();
            return session;
        }
    }
}