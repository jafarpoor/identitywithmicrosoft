using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjctModel.VeiwModel
{
  public  class UserLogin
    {
        public string Role { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int Id { get; set; }
        public Guid UId { get; set; }
        public bool IsActive { get; set; }
    }
}
