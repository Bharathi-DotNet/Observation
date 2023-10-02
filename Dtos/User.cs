using System.Security.Claims;

namespace Enquiry.Web.Dtos
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmpId { get; set; }
        public string Email { get; set; }
        public string EmployeeName { get; set; }
        public string Dept { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public IEnumerable<Claim> Claims()
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, Username),
                new Claim("EmpId", EmpId),
                new Claim(ClaimTypes.Email, Email),
                new Claim("EmployeeName", EmployeeName),
                new Claim("Dept", Dept)
            };
            claims.AddRange(Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }
    }
}
