using DesafioFULLApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioFULLApi.Helper
{
    public static class ExtHelper
    {
        public static TitleDelay ToTitleDelay(this TitleDelayDTO dto)
        {
            //TODO: alterar para autoMapper
            return new TitleDelay(
                dto.TitleNumber,
                dto.NameDebtor,
                dto.CpfDebtor,
                dto.PercentFees,
                dto.PercentFine,
                dto.DebtInstallments.Select(x => new DebtInstallment(x.NumberPart, x.DueDate, x.ValuePart)).ToList()
            );
        }

        public static TitleDelayDTO ToTitleDelayDTO(this TitleDelay model)
        {
            //TODO: alterar para autoMapper
            return new TitleDelayDTO(
                model.Id,
                model.TitleNumber,
                model.NameDebtor,
                model.CpfDebtor,
                model.PercentFees,
                model.PercentFine,
                model.DebtInstallments.Select(x => new DebtInstallmentDTO(x.Id, x.NumberPart, x.DueDate, x.ValuePart, x.TitleDelayId)).ToList()
            );
        }
    }
}
