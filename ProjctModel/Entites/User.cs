using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjctModel.Entites
{
   public class User :IdentityUser<int>
    {
        public Guid UId { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
    }
}
