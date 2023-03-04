namespace FoodService.Data;

public interface IDatabaseSettings
{
    string DatabaseName { get; set; }
    public string ConnectionString { get; set; }
    public string FoodCategoriesCollectionName { get; set; }
    public string FoodCollectionName { get; set; }
}
