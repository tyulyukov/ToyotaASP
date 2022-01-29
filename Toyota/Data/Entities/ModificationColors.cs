using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Toyota.Data.Entities
{
    public class ModificationColors
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String Slug { get; set; }
        public Modification Modification { get; set; }
        public Guid ModificationId { get; set; }
        public Color Color { get; set; }
        public Guid ColorId { get; set; }
        public String ImgUrl { get; set; }
    }
}
