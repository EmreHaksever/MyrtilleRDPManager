using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyrtilleRDPManager.Data
{
    // 1. KULLANICI TABLOSU
    public class User
    {
        [Key]
        public int Id { get; set; }

        // DÜZELTME: 'required' eklendi. Artık bu nesne oluşturulurken Username verilmek ZORUNDA.
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string PasswordHash { get; set; }

        public bool IsAdmin { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // İlişkiler
        public virtual ICollection<UserConnectionPermission> Permissions { get; set; } = new List<UserConnectionPermission>();
    }

    // 2. BAĞLANTI TABLOSU
    public class Connection
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Host { get; set; }

        // Varsayılan değerlerin var, bunlar sorun çıkarmaz.
        public string Protocol { get; set; } = "rdp";
        public int Port { get; set; } = 3389;

        // DÜZELTME: Bu alanlar null olamaz diye tanımlanmış, o yüzden 'required' şart.
        // Eğer boş kalabilirlerse 'string?' yapmalıydın. Ama RDP için bunlar lazım.
        public required string RemoteUsername { get; set; }

        public required string EncryptedPassword { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // İlişkiler
        public virtual ICollection<UserConnectionPermission> Permissions { get; set; } = new List<UserConnectionPermission>();
    }

    // 3. YETKİ TABLOSU
    public class UserConnectionPermission
    {
        [Key]
        public int Id { get; set; }

        // Hangi Kullanıcı?
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        // DÜZELTME: "= null!;" eklendi. 
        // Bu, derleyiciye "Bunun null olmayacağına söz veriyorum (çünkü EF Core dolduracak), kapa çeneni" demektir.
        public virtual User User { get; set; } = null!;

        // Hangi Bilgisayar?
        public int ConnectionId { get; set; }

        [ForeignKey("ConnectionId")]
        // Aynı şekilde burada da null-forgiving operator (!) kullanıyoruz.
        public virtual Connection Connection { get; set; } = null!;
    }
}