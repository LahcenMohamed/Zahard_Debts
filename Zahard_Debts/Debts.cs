using System.ComponentModel.DataAnnotations;
namespace Zahard_Debts
{
    public class Debts
    {
        [Key]
        public int Debt_id { get; set; }
        public int Customer_id { get; set; }
        public int size { get; set; }
        public string Debt_type { get; set; }
        public System.DateTime Debt_date { get; set; }
        public string note { get; set; }

       
    }
}