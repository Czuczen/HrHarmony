using HrHarmony.Consts;
using HrHarmony.Data.Database;
using HrHarmony.Data.Database.SeedData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HrHarmony.Controllers
{
    [AllowAnonymous]
    public class AnonymousController : Controller
    {
        private readonly ApplicationDbContext _ctx;

        public AnonymousController(ApplicationDbContext context)
        {
            _ctx = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateSampleObjects(string accessKey, int? maritalStatusesCount = null,
            int? addressesCount = null, int? educationLevelsCount = null, int? experiencesCount = null,
            int? employeesCount = null, int? contractTypesCount = null, int? leaveTypesCount = null,
            int? absenceTypesCount = null, int? employmentContractsCount = null, int? leavesCount = null,
            int? absencesCount = null, int? salariesCount = null)
        {
            if (accessKey == AccessKeys.CreateSampleObjectsKey)
            {
                SeedData.MaritalStatusesCount = maritalStatusesCount;
                SeedData.AddressesCount = addressesCount;
                SeedData.EducationLevelsCount = educationLevelsCount;
                SeedData.ExperiencesCount = experiencesCount;
                SeedData.EmployeesCount = employeesCount;
                SeedData.ContractTypesCount = contractTypesCount;
                SeedData.LeaveTypesCount = leaveTypesCount;
                SeedData.AbsenceTypesCount = absenceTypesCount;
                SeedData.EmploymentContractsCount = employmentContractsCount;
                SeedData.LeavesCount = leavesCount;
                SeedData.AbsencesCount = absencesCount;
                SeedData.SalariesCount = salariesCount;

                SeedData.Initialize(_ctx);
            }
            else
            {
                return Unauthorized();
            }

            return Ok();
        }
    }
}
