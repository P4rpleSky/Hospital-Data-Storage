using Microsoft.AspNetCore.Mvc;

namespace Kosta_Task.Models.Dtos
{
    public class DepartmentRazorDto
    {
        public DepartmentDto DepartmentDto { get; set; } = null!;

		public string DepartmentsListJson { get; set; } = null!;
    }
}
