using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MWTDbContext.Models
{
    public enum TypeOfOP
    {
        Delete = -1,
        Update = 0,
        Insert = 1
    }

    public enum ColumnType
    {
        Row = 0,
        Company = 1,
        Model = 2,
        Price  = 3,
        Stock = 4
    }

    public class LogProduct
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public ColumnType Column { get; set; }
        [Required]
        public TypeOfOP TypeOp { get; set; }

        [MaxLength(65)]
        #nullable enable
        public string? Oldval { get; set; }
        
        [MaxLength(65)]
        #nullable enable
        public string? Newval { get; set; }


    }
}
