using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioFULLApi.Models
{
    public class DebtInstallment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key, ForeignKey("TitleDelay")]
        public int TitleDelayId { get; set; }

        public long NumberPart { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }

        public float ValuePart { get; set; }

        public TitleDelay TitleDelay;

        public DebtInstallment() { }

        public DebtInstallment(long numberPart, DateTime dueDate, float valuePart)
        {
            NumberPart = numberPart;
            DueDate = dueDate;
            ValuePart = valuePart;
        }
    }
}
