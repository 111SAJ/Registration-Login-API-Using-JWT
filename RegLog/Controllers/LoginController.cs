using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegLog.Data;
using RegLog.Model;
using RegLog.Service;

namespace RegLog.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ErrorResponseService _errorResponseService;
        private readonly JwtService _jwtService;

        public LoginController(ApplicationDbContext context, ErrorResponseService errorResponseService, JwtService jwtService)
        {
            _context = context;
            _errorResponseService = errorResponseService;
            _jwtService = jwtService;
        }

        //Login
        [HttpPost("login")]
        public async Task<ActionResult<Login>> Login(Login login)
        {
            try
            {
                var loggedIn = await _context.Employee.FirstOrDefaultAsync(e => e.EmployeeEmail == login.EmployeeEmail && e.Password == login.Password);
                if (loggedIn == null)
                {
                    login.isLoggedIn = false;
                    var errorResponse = _errorResponseService.CreateErrorResponse(400, "Username or Password is invalid");
                    return BadRequest(errorResponse);
                }

                login.isLoggedIn = true;
                var token = _jwtService.GenerateToken(loggedIn);

                var response = new
                {
                    Status = 200,
                    Message = "Logged in successfully",
                    Data = new
                    {
                        isLoggedIn = true,
                        Token = token
                    }
                };

                return Created("", response);

            }

            catch (Exception)
            {
                var errorResponse = _errorResponseService.CreateErrorResponse(500, "Internal Server Error");
                return StatusCode(500, errorResponse);
            }
            

        }
    }
}
