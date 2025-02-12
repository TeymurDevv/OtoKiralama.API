using System;
using FluentValidation;
using OtoKiralama.Application.Dtos.IndividualInvoice;
using OtoKiralama.Domain.Enums;

namespace OtoKiralama.Application.Validators.Invoice
{
    public class InvoiceCreateDtoValidator : AbstractValidator<InvoiceCreateDto>
    {
        public InvoiceCreateDtoValidator()
        {
            RuleFor(x => x.AppUserId)
                .NotEmpty().WithMessage("Kullanıcı kimliği gereklidir.")
                .Must(x => Guid.TryParse(x, out _))
                .WithMessage("Kullanıcı kimliği geçerli bir GUID olmalıdır.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık gereklidir.")
                .MaximumLength(100).WithMessage("Başlık 100 karakteri geçmemelidir.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres gereklidir.")
                .MaximumLength(250).WithMessage("Adres 250 karakteri geçmemelidir.");

            RuleFor(x => x.CountryId)
                .GreaterThan(0).WithMessage("Ülke kimliği pozitif bir sayı olmalıdır.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Şehir gereklidir.")
                .MaximumLength(50).WithMessage("Şehir 50 karakteri geçmemelidir.");

            RuleFor(x => x.Area)
                .NotEmpty().WithMessage("İlçe gereklidir.")
                .MaximumLength(50).WithMessage("İlçe 50 karakteri geçmemelidir.");

            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("Posta kodu gereklidir.")
                .MaximumLength(20).WithMessage("Posta kodu 20 karakteri geçmemelidir.");

            RuleFor(x => x.InvoiceType)
                .IsInEnum().WithMessage("Geçersiz fatura türü.");

            RuleFor(x => x.TaxCompany)
                .NotEmpty()
                .When(x => x.InvoiceType == InvoiceType.CompanyInvoice)
                .WithMessage("Kurumsal faturalar için Vergi Firması gereklidir.")
                .MaximumLength(150).WithMessage("Vergi Firması 150 karakteri geçmemelidir.");

            RuleFor(x => x.TaxNumber)
                .NotEmpty()
                .When(x => x.InvoiceType == InvoiceType.CompanyInvoice)
                .WithMessage("Kurumsal faturalar için Vergi Numarası gereklidir.")
                .Matches(@"^\d+$")
                .When(x => !string.IsNullOrWhiteSpace(x.TaxNumber))
                .WithMessage("Vergi Numarası yalnızca rakamlardan oluşmalıdır.")
                .MaximumLength(50).WithMessage("Vergi Numarası 50 karakteri geçmemelidir.");
        }
    }
}
