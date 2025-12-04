using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyrtilleRDPManager.Data
{
    // 1. KULLANICI TABLOSU
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public bool IsAdmin { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // EKSİK OLAN KISIM: İlişkiler
        public virtual ICollection<UserConnectionPermission> Permissions { get; set; } = new List<UserConnectionPermission>();
    }

    // 2. BAĞLANTI TABLOSU
    public class Connection
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Host { get; set; }

        public string Protocol { get; set; } = "rdp";
        public int Port { get; set; } = 3389;

        public string RemoteUsername { get; set; }

        [Required]
        public string EncryptedPassword { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // EKSİK OLAN KISIM: İlişkiler
        public virtual ICollection<UserConnectionPermission> Permissions { get; set; } = new List<UserConnectionPermission>();
    }

    // 3. YETKİ TABLOSU (İŞTE EKSİK OLAN SINIF BU)
    public class UserConnectionPermission
    {
        [Key]
        public int Id { get; set; }

        // Hangi Kullanıcı?
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        // Hangi Bilgisayar?
        public int ConnectionId { get; set; }
        [ForeignKey("ConnectionId")]
        public virtual Connection Connection { get; set; }
    }
}