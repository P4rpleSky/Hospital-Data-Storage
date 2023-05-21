﻿namespace Kosta_Task.Models.Dtos
{
	public class EmployeeDto
	{
		public decimal Id { get; set; }

		public string FirstName { get; set; } = null!;

		public string SurName { get; set; } = null!;

		public string? Patronymic { get; set; }

		public int Age { get; set; }

		public DateTime DateOfBirth { get; set; }

		public string? DocSeries { get; set; }

		public string? DocNumber { get; set; }

		public string Position { get; set; } = null!;

		public Guid DepartmentId { get; set; }

		//public virtual Department Department { get; set; } = null!;
	}
}
