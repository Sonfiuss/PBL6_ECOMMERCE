namespace Website_Ecommerce.API.ModelDtos
{
    public class CommentDto
    {
        public int? Id { get; set; }
        public int ProductId { get; set; }
        public string Content { get; set; }
        // public int State { get; set; } // 0 is delete, 1 is ton tai
    }
}