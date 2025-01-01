using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoKiralama.Application.Dtos.User
{
    public class ChangeSubscribtionStatusDto
    {
        public bool IsEmailSubscribed { get; set; }
      public  bool IsSmsSubscribed { get; set; }
    }
}
