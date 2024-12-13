using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class AboutValidator : AbstractValidator<About>
    {
        public AboutValidator()
        {
            RuleFor(x=>x.Description1).NotEmpty().WithMessage("Açıklama kısmını boş geçemzsiniz...!");
            RuleFor(x=>x.Image1).NotEmpty().WithMessage("Lütfen görsel seçiniz...!");
            RuleFor(x => x.Description1).MinimumLength(50).WithMessage("En az 50 karakterlik açıklama giriniz...!");
            RuleFor(x => x.Description1).MaximumLength(1500).WithMessage("Lütfen açıklamayı kısaltın...!");
        }
    }
}
