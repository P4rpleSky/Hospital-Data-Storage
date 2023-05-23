namespace Kosta_Task.Models.Dtos
{
	public class DepartmentDto
	{
		public Guid Id { get; set; }

		public string Name { get; set; } = null!;

		public string? Code { get; set; }

		public Guid? ParentDepartmentId { get; set; }

        public string? ParentDepartmentName { get; set; }

        //public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        //public virtual ICollection<Department> InverseParentDepartment { get; set; } = new List<Department>();

        //public virtual Department? ParentDepartment { get; set; }
    }
}
