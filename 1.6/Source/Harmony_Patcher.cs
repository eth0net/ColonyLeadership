using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using Verse;

namespace ColonyLeadership;

[StaticConstructorOnStartup]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
public static class Harmony_Patcher
{
    static Harmony_Patcher()
    {
#if DEBUG
        Harmony.DEBUG = true;
#endif
        Harmony harmony = new("eth0net.ColonyLeadership");
        harmony.PatchAll();
    }
}
