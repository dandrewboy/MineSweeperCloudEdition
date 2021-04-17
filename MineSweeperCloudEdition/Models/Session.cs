using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeperCloudEdition.Models
{
    //session class to establish user authentification.
    public class Session
    {
        public int CurrentUserId { get; set; }

        public Session(int currentUserId)
        {
            CurrentUserId = currentUserId;
        }
    }
}
