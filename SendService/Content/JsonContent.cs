using System.Text;

namespace SendService.Content;

public class JsonContent : StringContent
{
    public JsonContent(string content, Encoding encoding) : base(content, encoding, "application/json")
    {
    }

    public JsonContent(string content) : this(content, Encoding.UTF8)
    {
    }
}
