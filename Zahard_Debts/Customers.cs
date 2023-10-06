using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Zahard_Debts
{
    public class Customers
    {

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
       
            /*public Customers()
            {
                this.Debts = new HashSet<Debts>();
            }*/

            [Key]
            public int Customer_id { get; set; }
            public string Customer_name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public int total_debts { get; set; }

           
    }

}
