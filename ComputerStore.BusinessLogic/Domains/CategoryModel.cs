namespace ComputerStore.BusinessLogic.Domains
{
    public class CategoryModel
    {
        public string? Id { get; set; } = string.Empty;
        public string? Name { get; set; } = string.Empty;
        public ImageModel? Thumbnail { get; set; }
    }
}
