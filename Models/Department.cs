using System;
using System.Collections.Generic;

namespace Kosta_Task.Models;

public partial class Department
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Code { get; set; }

    public Guid? ParentDepartmentId { get; set; }

    public virtual ICollection<Employee> Empoyees { get; set; } = new List<Employee>();

    public virtual ICollection<Department> InverseParentDepartment { get; set; } = new List<Department>();

    public virtual Department? ParentDepartment { get; set; }
}
