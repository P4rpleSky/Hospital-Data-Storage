using Kosta_Task.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Kosta_Task.Models.Dtos
{
	public class DepartmentDto
	{
		[Required(ErrorMessage = "Это поле обязательно для заполнения!")]
		public Guid Id { get; set; }

        [DepartmentData]
        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
		[MaxLength(50, ErrorMessage = "Наименование не должно превышать 50 символов!")]
		public string Name { get; set; } = null!;

		[Сode]
		[MaxLength(10, ErrorMessage = "Мнемокод не должен превышать 10 символов!")]
		public string? Code { get; set; }

		public Guid? ParentDepartmentId { get; set; }

		[DepartmentData]
		public string? ParentDepartmentName { get; set; }

        //public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        //public virtual ICollection<Department> InverseParentDepartment { get; set; } = new List<Department>();

        //public virtual Department? ParentDepartment { get; set; }
    }
}
