using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegLog.Data;
using RegLog.Model;
using RegLog.Service;

namespace RegLog.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorResponseService _errorResponseService;
        private readonly JwtService _jwtService;

        public EmployeeController(ApplicationDbContext context, ErrorResponseService errorResponseService, JwtService jwtService)
        {
            _context = context;
            _errorResponseService = errorResponseService;
            _jwtService = jwtService;
        }

        //Get Employee
        [HttpGet("index")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            try
            {
                var employeeList = await _context.Employee.ToListAsync();
                if (employeeList == null || employeeList.Count == 0)
                {
                    var errorResponse = _errorResponseService.CreateErrorResponse(404, "No Employee Found");
                    return BadRequest(errorResponse);
                }

                var response = new
                {
                    Status = 200,
                    Message = "Action Performed Successfully",
                    Data = employeeList
                };

                return Created("", response);
            }
            catch (Exception)
            {
                var errorResponse = _errorResponseService.CreateErrorResponse(500, "Internal Server Error");
                return StatusCode(500, errorResponse);
            }
        }

        //Create Employee
        [HttpPost("create")]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //check if employee already registered
                    var existEmployee = await _context.Employee.FirstOrDefaultAsync(e => e.EmployeeEmail == employee.EmployeeEmail);
                    if (existEmployee != null)
                    {
                        var errorResponse = _errorResponseService.CreateErrorResponse(400, "Employee already exist");
                        return BadRequest(errorResponse);
                    }

                    _context.Employee.Add(employee);
                    await _context.SaveChangesAsync();

                    //var token = _jwtService.GenerateToken(employee);

                    var response = new
                    {
                        Status = 200,
                        Message = "Employee registered successfully",
                        Data = new
                        {
                            employee.EmployeeId,
                            employee.EmployeeName,
                            employee.EmployeeEmail
                            //Token = token
                        }
                    };

                    return Created("", response);
                }
                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                var errorResponse = _errorResponseService.CreateErrorResponse(500, "Internal Server Error");
                return StatusCode(500, errorResponse);
            }
        }
    }
}
