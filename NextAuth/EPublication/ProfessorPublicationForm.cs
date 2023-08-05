using System;
using System.Collections.Generic;

namespace NextAuth.EPublication
{
    public partial class ProfessorPublicationForm
    {
        public ProfessorPublicationForm()
        {
            ProfessorPublicationDetails = new HashSet<ProfessorPublicationDetail>();
        }

        public int Id { get; set; }
        public int PublicationTypeId { get; set; }
        public string Field { get; set; } = null!;
        public bool IsActive { get; set; }
        public bool Shown { get; set; }
        public int? OrderNumber { get; set; }
        public string? Type { get; set; }
        public string? ValidationRegex { get; set; }
        public string? ValidationMessage { get; set; }

        public virtual ICollection<ProfessorPublicationDetail> ProfessorPublicationDetails { get; set; }
    }
}
