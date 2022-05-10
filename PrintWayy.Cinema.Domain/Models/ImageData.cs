namespace PrintWayy.Cinema.Domain.Models
{
    public class ImageData
    {
        public string ContentType { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string ImageBase64 { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"data:{ContentType};base64,{ImageBase64}";
        }
    }
}
