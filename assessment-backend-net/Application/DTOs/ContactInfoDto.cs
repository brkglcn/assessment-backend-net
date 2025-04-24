using System;

namespace Application.DTOs
{
    public class ContactInfoDto
    {
        public Guid Id { get; set; }
        public string InfoType { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}