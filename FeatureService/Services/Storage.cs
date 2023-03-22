namespace FeatureService.Services;

public class Storage : IWriteStorage, IReadStorage
{
    public object Read()
    {
        var message = $"Reading from {nameof(Storage)}";
        Console.WriteLine(message);
        return message;
    }

    public void Write(object data)
    {
        Console.WriteLine($"Writing in {nameof(Storage)}, data: {data}");
    }
}
