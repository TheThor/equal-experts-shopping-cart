namespace EqualExpertsShoppingCart.Models
{
    public class Product
    {
        public string Title { get; }
        public Price ProductPrice { get; }

        public Product(string title, decimal priceAmount)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Name cannot be null or empty");
            }

            if (priceAmount < 0)
            {
                throw new ArgumentException("Price amount cannot be negative");
            }

            Title = title;
            ProductPrice = new Price(priceAmount);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            var otherProduct = (Product)obj;
            return Title.Equals(otherProduct.Title) && ProductPrice.Equals(otherProduct.ProductPrice);
        }

        public static bool operator ==(Product left, Product right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Product left, Product right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Title.GetHashCode();
                hash = hash * 23 + ProductPrice.GetHashCode();
                return hash;
            }
        }

        public sealed class Price
        {
            public decimal Amount { get; }

            internal Price(decimal amount)
            {
                Amount = Decimal.Round(amount, 2);
            }

            public override bool Equals(object? obj)
            {
                if (obj is null || obj.GetType() != GetType())
                {
                    return false;
                }

                var valueObject = (Price)obj;
                return Amount.Equals(valueObject.Amount);
            }

            public static bool operator ==(Price left, Price right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(Price left, Price right)
            {
                return !Equals(left, right);
            }

            public override int GetHashCode()
            {
                return Amount.GetHashCode();
            }
        }
    }
}
