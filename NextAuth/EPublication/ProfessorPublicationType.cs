using System;
using System.Collections.Generic;

namespace NextAuth.EPublication
{
    public partial class ProfessorPublicationType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public int OrderNumber { get; set; }
    }
}
