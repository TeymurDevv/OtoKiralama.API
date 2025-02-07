using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoKiralama.Application.Dtos.Reservation
{
    public class ReservationGetByEmailAndNumberDto
    {
        public string ReservationNumber { get; set; }
        public string Email { get; set; }
    }
}