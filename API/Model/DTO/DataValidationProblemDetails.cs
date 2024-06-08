using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace API.Model.DTO
{
    public class DataValidationProblemDetails : ProblemDetails
    {
        [JsonPropertyName("reasons")]
        public List<string> Reasons { get; set; }
    }
}
