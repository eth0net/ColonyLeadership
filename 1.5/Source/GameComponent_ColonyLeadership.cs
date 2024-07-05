using Verse;

namespace ColonyLeadership;

public class GameComponent_ColonyLeadership : GameComponent
{
    private Pawn _leader;

    public GameComponent_ColonyLeadership(Game game)
    {
    }

    public Pawn Leader
    {
        get => _leader;
        set => _leader = value;
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_References.Look(ref _leader, "leader");
    }
}
