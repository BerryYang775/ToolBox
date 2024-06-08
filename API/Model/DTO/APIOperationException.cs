namespace API.Model.DTO
{
    public class APIOperationException : Exception
    {
        public string Type { get; set; }
        public string Detail { get; set; }
        public string Title { get; set; }
        public string Instance { get; set; }
        public string Process { get; set; }

        public APIOperationException(string Instance, string Process, string Detail)
        {
            Type = "API.Models.DTO.APIOperationException";
            this.Detail = Detail;
            Title = "API Operation Exception";
            this.Process = Process;
            this.Instance = Instance;
        }
    }
}
