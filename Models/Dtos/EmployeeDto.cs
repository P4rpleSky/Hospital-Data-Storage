using Kosta_Task.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Kosta_Task.Models.Dtos
{
	public class EmployeeDto
	{
		[Required(ErrorMessage = "Это поле обязательно для заполнения!")]
		public decimal Id { get; set; }

		[Required(ErrorMessage = "Это поле обязательно для заполнения!")]
		[MaxLength(50, ErrorMessage = "Имя не должно превышать 50 символов!")]
		public string FirstName { get; set; } = null!;

		[Required(ErrorMessage = "Это поле обязательно для заполнения!")]
		[MaxLength(50, ErrorMessage = "Фамилия не должна превышать 50 символов!")]
		public string SurName { get; set; } = null!;

		[MaxLength(50, ErrorMessage = "Отчество не должно превышать 50 символов!")]
		public string? Patronymic { get; set; }

		public int Age { get; set; }

		[Required(ErrorMessage = "Это поле обязательно для заполнения!")]
		[Date("1923-01-01", "2023-01-01", "yyyy-MM-dd", ErrorMessage = "Введена недопустимая дата рождения!")]
		public DateTime DateOfBirth { get; set; }

        [DocumentData]
        [MaxLength(4, ErrorMessage = "Серия документа не должна превышать 4 символов!")]
		public string? DocSeries { get; set; }

		[DocumentData]
		[MaxLength(6, ErrorMessage = "Номер документа не должен превышать 6 символов!")]
		public string? DocNumber { get; set; }

		[Required(ErrorMessage = "Это поле обязательно для заполнения!")]
		[MaxLength(50, ErrorMessage = "Должность не должна превышать 10 символов!")]
		public string Position { get; set; } = null!;

		[Required(ErrorMessage = "Это поле обязательно для заполнения!")]
		public Guid DepartmentId { get; set; }

		//public virtual Department Department { get; set; } = null!;
	}
}
