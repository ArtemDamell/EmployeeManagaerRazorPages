using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstRazorApp.AppRepository;
using FirstRazorApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstRazorApp.ViewComponents
{
    public class HeadCountViewComponent : ViewComponent
    {
        private readonly IEmpoyeeRepository _empoyeeRepository;

        public HeadCountViewComponent(IEmpoyeeRepository empoyeeRepository)
        {
            _empoyeeRepository = empoyeeRepository;
        }

        /// <summary>
        /// Invokes the view component to get the employee count by department.
        /// </summary>
        /// <param name="department">The department.</param>
        /// <returns>
        /// The view component result.
        /// </returns>
        public IViewComponentResult Invoke(Dept? department = null)
        {
            var result = _empoyeeRepository.EmployeeCountByDept(department);
            return View(result);
        }
    }
}
