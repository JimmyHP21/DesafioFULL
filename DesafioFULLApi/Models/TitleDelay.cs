
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioFULLApi.Models
{
    public class TitleDelay
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public long TitleNumber { get; set; }
        [StringLength(80)]
        public string NameDebtor { get; set; }
        public string CpfDebtor { get; set; }
        public int PercentFees { get; set; }
        public int PercentFine { get; set; }
        public virtual ICollection<DebtInstallment> DebtInstallments { get; set; }

        public TitleDelay() { }

        public TitleDelay(long titleNumber, string nameDebtor, string cpfDebtor, int percentFees, int percentFine, ICollection<DebtInstallment> debtInstallments)
        {
            TitleNumber = titleNumber;
            NameDebtor = nameDebtor;
            CpfDebtor = cpfDebtor;
            PercentFees = percentFees;
            PercentFine = percentFine;
            DebtInstallments = debtInstallments;
        }
    }
}
