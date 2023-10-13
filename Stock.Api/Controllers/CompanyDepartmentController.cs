using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using Stock.Model;
using Stock.Repository;
using System.ComponentModel.Design;

namespace Stock.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CompanyDepartmentController : BaseController
	{
		  private readonly CompanyDepartmentRepository _repository;
		public CompanyDepartmentController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
		{

		}
		[HttpGet("GetCompanyDepartments")]
		public async Task<ActionResult<IEnumerable<object>>> GetCompanyDepartmentAsync()
		{
			var companyDepartments = await repo.CompanyDepartmentRepository.GetCompanyDeparmentsAsync();
			if (companyDepartments == null || !companyDepartments.Any())
			{
				return NotFound("Departman Bulunamadı");
			}
			return Ok(companyDepartments);
		}
        [HttpPost("Save")]
        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());
            CompanyDepartment companyDepartman = new CompanyDepartment()
            {
                Id = json.Id,
                DepartmentName = json.DepartmanName,
                CompanyId = json.CompanyId,



            };
            if (companyDepartman.Id > 0)
            {
                repo.CompanyDepartmentRepository.Update(companyDepartman);
            }
            else
            {
                repo.CompanyDepartmentRepository.Create(companyDepartman);
            }

            repo.SaveChanges();

            return new
            {

                success = true,
                message = "Property saved successfully"
            };


        }
        [HttpPost("DeleteCompanyDepartment")]
		public async Task<ActionResult> DeleteCompanyDepartment(int companyDepartmentId)
		{
			var companyDepartment = await repo.CompanyDepartmentRepository.FindByIdAsync(companyDepartmentId);
			if (companyDepartment == null)
				return NotFound("Birim Bulunamadı");
			repo.CompanyDepartmentRepository.Delete(companyDepartment);
			 repo.SaveChanges();
			return Ok("Kategori başarı ile silindi");
		}
		[HttpGet("GetDepartmentsByCompanyId/{companyId}")]
		public async Task<ActionResult<IEnumerable<object>>> GetDepartmentsByCompanyId(int companyId)
		{
			var companyDepartments = await repo.CompanyDepartmentRepository.GetCompanyDepartmentsByCompanyIdAsync(companyId);

			if (companyDepartments == null || !companyDepartments.Any())
			{
				return NotFound("Departman Bulunamadı");
			}
			return Ok(companyDepartments);
		}



	}


}

