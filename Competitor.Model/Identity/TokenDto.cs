using System.ComponentModel.DataAnnotations;

namespace Competitor.Model.Identity
{
    public class TokenDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
