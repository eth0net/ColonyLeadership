using System.Diagnostics.CodeAnalysis;
using RimWorld;
using Verse;

namespace ColonyLeadership;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class GameComponent_ColonyLeadership(Game game) : GameComponent
{
    private const int TicksWithoutLeaderForThought = GenDate.TicksPerQuadrum;

    private readonly TickManager _tickManager = game.tickManager;

    private int _lastTickWithActiveLeader;

    private Pawn _leader;

    public Pawn Leader
    {
        get => _leader;
        set
        {
            TryRemoveLeaderEffects();
            var comp = LeaderComp ?? new CompColonyLeader();
            _leader = value;
            Faction.OfPlayer.leader = value;
            TryAddLeaderEffects();
            LeaderComp = comp;
        }
    }

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public CompColonyLeader LeaderComp
    {
        get => Leader.GetComp<CompColonyLeader>();
        set
        {
            if (Leader?.HasComp<CompColonyLeader>() == true) return;
            Leader?.comps?.Add(value);
        }
    }

    private bool HasActiveLeader => _leader is { Dead: false, Downed: false, IsPrisoner: false, IsSlave: false };

    public bool NoLeaderThoughtActive =>
        _tickManager.TicksGame - _lastTickWithActiveLeader >= TicksWithoutLeaderForThought;

    private void TryAddLeaderEffects()
    {
        if (_leader == null) return;

        // _leader.health.AddHediff(HediffDefOf.ColonyLeader);

        var memories = _leader.needs.mood.thoughts.memories;
        memories.TryGainMemory(ThoughtMaker.MakeThought(ThoughtDefOf.ColonyLeadershipGained, 0));
        memories.RemoveMemoriesOfDef(ThoughtDefOf.ColonyLeadershipLost);
    }

    private void TryRemoveLeaderEffects()
    {
        if (_leader == null) return;

        // var hediff = _leader.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.ColonyLeader);
        // if (hediff != null) _leader.health.RemoveHediff(hediff);

        var memories = _leader.needs.mood.thoughts.memories;
        memories.TryGainMemory(ThoughtMaker.MakeThought(ThoughtDefOf.ColonyLeadershipLost, 0));
        memories.RemoveMemoriesOfDef(ThoughtDefOf.ColonyLeadershipGained);
    }

    public void Notify_LeaderDied()
    {
        var memory = ThoughtMaker.MakeThought(ThoughtDefOf.ColonyLeaderDied, 0);
        Find.CurrentMap?.mapPawns?.FreeColonistsAndPrisonersSpawned?.ForEach(
            pawn =>
            {
                if (pawn.Faction is { IsPlayer: false }) return;
                if (pawn == Leader) return;
                pawn.needs.mood.thoughts.memories.TryGainMemory(memory);
            }
        );
    }

    public override void GameComponentTick()
    {
        base.GameComponentTick();

        // Limit to every 60 ticks to reduce performance impact
        // Offset by a few ticks as 60 ticks is probably popular
        if (!GenTicks.IsTickInterval(5, 60)) return;

        // If there is an active leader, update the last tick with an active leader
        if (HasActiveLeader) _lastTickWithActiveLeader = _tickManager.TicksGame;
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_References.Look(ref _leader, "leader");
    }
}
