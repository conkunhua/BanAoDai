using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Model
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }
        public string ReceiveAddress { get; set; }
        public string ReceivePhone { get; set; }
        public string ReceiveEmail { get; set; }
        public string ReceiveName { get; set; }
        public string Note { get; set; }
        public DateTime? CreatedBy_At { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? Status { get; set; }


    }
}
