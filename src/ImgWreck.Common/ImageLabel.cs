namespace ImgWreck.Common;

public class ImageLabel
{
    public ImageLabel(string key)
    {
        Key = key;
    }

    public string Key { get; set; } = null!;
    public Dictionary<string, float>? Labels { get; set; }
}
