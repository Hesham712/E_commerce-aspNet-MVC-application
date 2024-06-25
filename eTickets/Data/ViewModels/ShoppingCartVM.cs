using eTickets.Data.Base;
using eTickets.Data.Cart;
using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ShoppingCartVM
{
    public ShoppingCart ShoppingCart { get; set; }
    public double ShoppigCartTotal { get; set; }
}
