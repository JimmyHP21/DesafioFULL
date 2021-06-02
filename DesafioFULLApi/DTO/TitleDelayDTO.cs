﻿
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DesafioFULLApi.Models
{
    public class TitleDelayDTO
    {
        public int Id { get; set; }
        [Required]
        public long TitleNumber { get; set; }
        [StringLength(80)]
        public string NameDebtor { get; set; }
        public string CpfDebtor { get; set; }
        public int PercentFees { get; set; }
        public int PercentFine { get; set; }
        public ICollection<DebtInstallmentDTO> DebtInstallments { get; set; }

        public TitleDelayDTO() { }

        public TitleDelayDTO(int id, long titleNumber, string nameDebtor, string cpfDebtor, int percentFees, int percentFine, ICollection<DebtInstallmentDTO> debtInstallments)
        {
            Id = id;
            TitleNumber = titleNumber;
            NameDebtor = nameDebtor;
            CpfDebtor = cpfDebtor;
            PercentFees = percentFees;
            PercentFine = percentFine;
            DebtInstallments = debtInstallments;
        }
    }
}
