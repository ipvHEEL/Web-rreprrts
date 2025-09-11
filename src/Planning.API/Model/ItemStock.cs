namespace Planning.API.Model;

public class ItemStock
{
    public decimal Item { get; set; }
    public string Description { get; set; }
    public decimal BalanceQuantity { get; set; }
    public string UnitOfMeasure { get; set; }
}
