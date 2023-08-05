using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextAuth.Models
{
    public class Publications
    {
        public Guid Id { get; set; }
        public string Publication { get; set; }
        public string Title { get; set; }
        public string userId { get; set; }
        [ForeignKey("userId")]
        [ValidateNever]
        public ProfessorUser User { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

    }
}
