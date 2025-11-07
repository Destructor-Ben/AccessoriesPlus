using ReLogic.Content.Sources;

namespace AccessoriesPlus.Utilities.StuffToMoveToTerraUtil;

public sealed class CustomContentSource(IContentSource contentSource, IReadOnlyDictionary<string, string> redirects) : IContentSource
{
    public IContentValidator ContentValidator { get => contentSource.ContentValidator; set => contentSource.ContentValidator = value; }

    public RejectedAssetCollection Rejections => contentSource.Rejections;

    public IEnumerable<string> EnumerateAssets()
    {
        return contentSource.EnumerateAssets().Select(RewritePath);
    }

    public string GetExtension(string assetName)
    {
        return contentSource.GetExtension(RewritePath(assetName));
    }

    public Stream OpenStream(string fullAssetName)
    {
        return contentSource.OpenStream(RewritePath(fullAssetName));
    }

    private string RewritePath(string path)
    {
        foreach ((string from, string to) in redirects)
        {
            if (path.StartsWith(from, StringComparison.Ordinal))
                return path.Replace(from, to);
        }

        return path;
    }
}
