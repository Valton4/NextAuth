using System;
using System.Collections.Generic;

namespace NextAuth.EPublication
{
    public class ProfessorPublication
    {
        public ProfessorPublication()
        {
            ProfessorPublicationDetails = new HashSet<ProfessorPublicationDetail>();
        }

        public int Id { get; set; }
        public int ProfessorId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<ProfessorPublicationDetail> ProfessorPublicationDetails { get; set; }
    }
}
