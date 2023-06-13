namespace EqualExpertsShoppingCart.Models
{
    public class Cart
    {
        private readonly List<CartItem> _items;
        public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

        public decimal Subtotal => Math.Round(Items.Sum(item => item.TotalPrice), 2);
        public decimal Tax => Math.Round(Subtotal * 0.125m, 2);
        public decimal Total => Math.Round(Subtotal + Tax, 2);


        public Cart()
        {
            _items = new List<CartItem>();
        }

        public void AddItemByQuantity(Product product, int quantity)
        {
            if (quantity < 1)
            {
                throw new ArgumentException("Quantity must be greater than or equal to 1.");
            }

            if (product == null)
            {
                throw new ArgumentException("No Product is present.");
            }

            var existingItem = _items.FirstOrDefault(item => item.Product.Title == product.Title);

            if (existingItem != null)
            {
                existingItem.IncreaseQuantity(quantity);
            }
            else
            {
                var newItem = new CartItem(product, quantity);
                _items.Add(newItem);
            }
        }

        public void RemoveItemByQuantity(Product product, int quantity)
        {
            throw new NotImplementedException();
        }

        public void RemoveItemByProduct(CartItem product)
        {
            throw new NotImplementedException();
        }

        public class CartItem
        {
            public Product Product { get; private set; }
            public int Quantity { get; private set; }
            public decimal TotalPrice => Math.Round(Product.ProductPrice.Amount * Quantity, 2);

            internal CartItem(Product product, int quantity)
            {
                Product = product;
                Quantity = quantity;
            }

            public void IncreaseQuantity(int quantity)
            {
                Quantity += quantity;
            }

            public void DecreaseQuantity(int quantity)
            {
                throw new NotImplementedException();
            }
        }

    }
}
