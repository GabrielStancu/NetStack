using System.Text;

namespace SendService.Content;

public class XmlContent : StringContent
{
    public XmlContent(string content, Encoding encoding) : base(content, encoding, "application/xml")
    {
    }

    public XmlContent(string content) : this(content, Encoding.UTF8)
    {
    }
}
