using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks1to3.Models
{
    public class UserFriend
    {
        //[Key, Column(Order = 0)]
        public int UserId { get; set; }
        public User User { get; set; }

        //[Key, Column(Order = 1)]
        public int FriendId { get; set; }
        public User Friend { get; set; }
    }
}
