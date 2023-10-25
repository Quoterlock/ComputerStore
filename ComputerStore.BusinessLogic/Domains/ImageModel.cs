namespace ComputerStore.BusinessLogic.Domains
{
    public class ImageModel
    {
        public string? Id { get; set; }
        public string? Alt { get; set; } = string.Empty;
        public byte[]? Bytes { get; set; } = new byte[0];
    }
}
