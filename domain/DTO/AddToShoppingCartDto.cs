namespace domain.DTO
{
    public class AddToShoppingCartDto
    {
        public string? SelectedProductName { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
