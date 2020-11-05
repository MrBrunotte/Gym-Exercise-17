using System;
using System.Collections.Generic;
using System.Text;

namespace Booking.Core.Entities
{
    // punkt 2 i uppgiften
    public class GymClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        // har endast en get metod!
        public DateTime EndTime => StartTime + Duration;
        public string Description { get; set; }

        // punkt 7 i uppgiften
        // Navigation property many to many så vi behöver en collection
        public ICollection<ApplicationUserGymClass> AttendedMembers { get; set; }

    }
}
