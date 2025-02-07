using FluentValidation;
using OtoKiralama.Application.Dtos.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoKiralama.Application.Validators.ReservationValidator
{
    public class ReservationGetByEmailAndNumberDtoValidator : AbstractValidator<ReservationGetByEmailAndNumberDto>
    {
        public ReservationGetByEmailAndNumberDtoValidator()
        {
            RuleFor(s => s.Email).EmailAddress().NotEmpty();
            RuleFor(s=>s.ReservationNumber).NotEmpty();
        }
    }
}
