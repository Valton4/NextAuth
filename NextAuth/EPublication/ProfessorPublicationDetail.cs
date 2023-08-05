using System;
using System.Collections.Generic;

namespace NextAuth.EPublication
{
    public class ProfessorPublicationDetail
    {
        public int Id { get; set; }
        public int PublicationId { get; set; }
        public int PublicationFormId { get; set; }
        public string? Value { get; set; }
        public bool IsActive { get; set; }
        public virtual ProfessorPublication Publication { get; set; } = null!;
        public virtual ProfessorPublicationForm PublicationForm { get; set; } = null!;
    }
}
