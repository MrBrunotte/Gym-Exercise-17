using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.Entities
{
    // punkt 6 i övningen
    public class ApplicationUserGymClass
    {
        public int GymClassId { get; set; }
        // string eftersom Guide är string i databasen
        public string ApplicationUserId { get; set; }

        //Navigation properties
        public GymClass GymClass { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
