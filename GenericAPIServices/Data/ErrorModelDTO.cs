using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericAPIServices.Data
{
    public class ErrorModelDTO
    {
        public int Id { get; set; } // Unique identifier for the error
        public int? StatusCode { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Title { get; set; }
        public DateTime? Timestamp { get; set; } // When the error occurred
        public string? Message { get; set; } // Description of the error
        public string? Type { get; set; } // Type or category of the error
        public string? Source { get; set; } // Source of the error (e.g., class, method)
        public string? StackTrace { get; set; } // Stack trace at the point of error
        public string? Severity { get; set; } // Severity level of the error
        public string? UserId { get; set; } // User ID associated with the error (if applicable)
        public string? AdditionalDetails { get; set; } // Any additional information
        public string? Status { get; set; } // Status of the error (Resolved, Pending, etc.)
        public string? ErrorCode { get; set; }
    }
}
