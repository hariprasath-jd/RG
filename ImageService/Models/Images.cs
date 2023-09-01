namespace ImageService.Models
{
    public class Images
    {
        public int Id { get; set; }

        public int Description { get; set; }

        public string Filename { get; set; }

        public byte[] Photos { get; set; }
    }
}
