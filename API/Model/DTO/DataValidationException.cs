namespace API.Model.DTO
{
    public class DataValidationException : Exception
    {
        public string Type { get; set; }
        public string Detail { get; set; }
        public string Title { get; set; }
        public string Instance {  get; set; }
        public List<string> Reasons { get; set; }
        public DataValidationException(string Instance, List<string> Reasons)
        {
            Type = "API.Models.DTO.DataValidationException";
            Detail = "The call has invalid parameters or data input";
            Title = "Data Validation Exception";
            this.Reasons = Reasons;
            this.Instance = Instance;
        }
    }
}
