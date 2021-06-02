using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioFULLApi.Models
{
    public class DebtInstallmentDTO
    {
        public int Id { get; set; }

        public long NumberPart { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }

        public float ValuePart { get; set; }

        public int TitleDelayId { get; set; }

        public TitleDelayDTO TitleDelay;

        public DebtInstallmentDTO() { }
        public DebtInstallmentDTO(int id, long numberPart, DateTime dueDate, float valuePart, int titleDelayId)
        {
            Id = id;
            NumberPart = numberPart;
            DueDate = dueDate;
            ValuePart = valuePart;
            TitleDelayId = titleDelayId;
        }
    }
}
