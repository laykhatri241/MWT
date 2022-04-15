using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MWTWebApi.Model.Custom
{
    public class ChangePassword
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string OldPass{ get; set; }
        [Required]
        public string NewPass { get; set; }
    }
}
