using UnityEngine;
using Verse;

namespace ColonyLeadership;

/// <summary>
///     The main class for the mod
/// </summary>
public class ColonyLeadership : Mod
{
    /// <summary>
    ///     The settings for the mod
    /// </summary>
    public readonly ColonyLeadershipSettings Settings;

    /// <summary>
    ///     Constructor for the mod class to get the settings
    /// </summary>
    /// <param name="content"></param>
    public ColonyLeadership(ModContentPack content) : base(content)
    {
        Settings = GetSettings<ColonyLeadershipSettings>();
    }

    /// <summary>
    ///     Draw the settings window
    /// </summary>
    /// <param name="inRect"></param>
    public override void DoSettingsWindowContents(Rect inRect)
    {
        ColonyLeadershipSettings.DoSettingsWindowContents(inRect);
        base.DoSettingsWindowContents(inRect);
    }

    /// <summary>
    ///     Add the settings category
    /// </summary>
    /// <returns></returns>
    public override string SettingsCategory()
    {
        return "ColonyLeadership".Translate();
    }
}
