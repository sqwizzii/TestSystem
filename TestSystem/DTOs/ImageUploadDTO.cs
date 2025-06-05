namespace TestSystemAPI.DTOs
{
    public class ImageUploadDTO
    {
        public string FileName { get; set; } = null!;
        public string Base64Data { get; set; } = null!;
        public int? TestId { get; set; }
        public int? QuestionId { get; set; }
    }
}
