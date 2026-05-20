using Microsoft.AspNetCore.Identity;	

namespace ThesisDB.Models	
{
    public class ThesisDbUser : IdentityUser	
    {
        public string? FirstName { get; set; }	
        public string? LastName { get; set; }	
    }
}