using System;

namespace Application.DTOs
{
    public class AddContactInfoRequest
    {
        public Guid PersonId { get; set; }
        public string InfoType { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}