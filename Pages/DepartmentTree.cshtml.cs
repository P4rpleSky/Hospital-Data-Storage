using Kosta_Task.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kosta_Task.Pages
{
    public class DepartmentTreeModel : PageModel
    {
        public List<DepartmentDto> DepartmentDtos { get; set; }
        
        public void OnGet(List<DepartmentDto> departmentDtos)
        {
            DepartmentDtos = departmentDtos;
        }
    }
}
