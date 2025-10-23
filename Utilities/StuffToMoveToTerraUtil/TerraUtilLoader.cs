namespace AccessoriesPlus.Utilities.StuffToMoveToTerraUtil;

public abstract class TerraUtilLoader<T> : ModSystem where T : IModType, ILoadable
{
    public static IList<T> Content = null!;

    /// <summary>
    /// The instance of this loader.
    /// </summary>
    public static TerraUtilLoader<T> Instance => ModContent.GetInstance<TerraUtilLoader<T>>();

    public override void Load()
    {
        Content = Mod.GetContent<T>().ToList();

        foreach (var content in Content)
        {
            AddContent(content);
        }
    }

    /// <summary>
    /// Called when the given content is registered.
    /// </summary>
    /// <param name="content">The content to register.</param>
    public virtual void AddContent(T content) { }
}
