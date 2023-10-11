using System.ComponentModel.DataAnnotations;

namespace eAuction.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public DateTime StartingDate { get; set; }

        public DateTime EndingDate { get; set; }

        public string? SellerName { get; set; }

        public int ? SellerId { get; set; }

        public int StartingBid { get; set; }

        public int HighestBid { get; set; }

        public string? HighestBidName { get; set; }
    }
}
