using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Toyota.Data.Entities
{
    public class CallBack
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        [NotMapped]
        public String Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
