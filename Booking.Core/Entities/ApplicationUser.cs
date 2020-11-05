using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace Booking.Core.Entities
{
    // punkt 5 i uppgiften
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $" {FirstName} {LastName}";
        public DateTime TimeOfRegistration { get; set; }

        // punkt 8 i uppgiften
        // Navigation property many to many så vi behöver en collection
        public ICollection<ApplicationUserGymClass> AttendedClasses { get; set; }
    }
}