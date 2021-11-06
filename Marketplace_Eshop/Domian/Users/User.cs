using System;
using Domain.Attributes;

namespace Domain.Users
{

    [Auditable]
    public class User
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
    }
}
