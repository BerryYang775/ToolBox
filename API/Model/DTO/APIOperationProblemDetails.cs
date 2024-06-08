using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace API.Model.DTO
{
    public class APIOperationProblemDetails : ProblemDetails
    {
        [JsonPropertyName("process")]
        public string Process { get; set; }
    }
}
