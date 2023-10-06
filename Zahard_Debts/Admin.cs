using System.ComponentModel.DataAnnotations;
namespace Zahard_Debts
{
    public class Admins
    {
        [Key]
        public int Admin_id { get; set; }
        public string Admin_name { get; set; }
        public string Admin_password { get; set; }
    }
}