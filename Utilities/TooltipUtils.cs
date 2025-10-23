namespace AccessoriesPlus.Utilities;

public static class TooltipUtils
{
    /// <summary>
    /// Finds the index of <paramref name="tooltipName" /> in <paramref name="tooltips" />.
    /// </summary>
    /// <param name="tooltips">The list of tooltips to search through.</param>
    /// <param name="tooltipName">The tooltipname to find.</param>
    /// <returns>The index of <paramref name="tooltipName" /> in <paramref name="tooltips" /> if it is found, otherwise -1.</returns>
    /// TODO: move to accessories+
    public static int FindIndexOfTooltipName(this List<TooltipLine> tooltips, string tooltipName)
    {
        return tooltips.IndexOf(tooltips.Where(t => t.Name == tooltipName).FirstOrDefault());
    }

    /// <summary>
    /// Inserts <paramref name="tooltipsToInsert" /> before or after the <paramref name="name" /> in <paramref name="tooltips" />.
    /// </summary>
    /// <param name="tooltips">The tooltips list to insert into.</param>
    /// <param name="name">The tooltip name to insert around.</param>
    /// <param name="after">Whether the tooltips should be inserted before or after <paramref name="name" />.</param>
    /// <param name="tooltipsToInsert">The tooltips to insert.</param>
    /// TODO: move to accessories+
    public static void InsertTooltips(this List<TooltipLine> tooltips, string name, bool after, params TooltipLine[] tooltipsToInsert)
    {
        int index = tooltips.FindIndexOfTooltipName(name);
        if (index != -1)
            tooltips.InsertRange(after ? index + 1 : index, tooltipsToInsert);
    }

    /// <summary>
    /// Hides all <paramref name="tooltipNames" /> found in <paramref name="tooltips" />.
    /// </summary>
    /// <param name="tooltips">The tooltips that will be modified.</param>
    /// <param name="tooltipNames">The tooltip names to hide.</param>
    /// TODO: move to accessories+
    public static void RemoveTooltips(this List<TooltipLine> tooltips, params string[] tooltipNames)
    {
        foreach (var tooltip in tooltips)
        {
            if (tooltipNames.Contains(tooltip.Name))
                tooltip.Hide();
        }
    }

    /// <summary>
    /// Gets a localized <see cref="TooltipLine" />.
    /// </summary>
    /// <param name="name">The name of the <see cref="TooltipLine" />.</param>
    /// <param name="stringFormat">String format arguments for the tooltip.</param>
    /// <returns>A <see cref="TooltipLine" /> that has the given <paramref name="name" /> and an automatic key such as <c>Mods.{ModName}.Tooltips.{TooltipName}</c>.</returns>
    /// TODO: move to accessories+
    public static TooltipLine GetTooltipLine(string name, params object[] stringFormat)
    {
        return new TooltipLine(AccessoriesPlusMod.Instance, AccessoriesPlusMod.Instance.Name + ":" + name, AccessoriesPlusMod.Instance.GetLocalization("Tooltips." + name).Format(stringFormat));
    }
}
