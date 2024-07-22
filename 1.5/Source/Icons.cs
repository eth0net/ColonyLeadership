using UnityEngine;
using Verse;

namespace ColonyLeadership;

[StaticConstructorOnStartup]
public static class Icons
{
    public static readonly Texture2D Star = ContentFinder<Texture2D>.Get("UI/CL_Star");
    public static readonly Texture2D Triangle = ContentFinder<Texture2D>.Get("UI/CL_Triangle");
}
