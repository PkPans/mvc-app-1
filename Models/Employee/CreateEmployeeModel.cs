using System;
using System.Collections.Generic;
using mymvc1.Models.Company;

namespace mymvc1.Models.Employee
{
    public class CreateEmployeeModel : EmployeeModel
    {
        public int? CompanyId {get; set;}
        public ICollection<DetailsCompanyModel> AllCompanies {get; set;}
    }
}