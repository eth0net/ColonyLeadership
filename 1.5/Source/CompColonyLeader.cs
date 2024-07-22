// using System.Collections.Generic;
// using System.Diagnostics.CodeAnalysis;
// using RimWorld;
// using Verse;
//
// namespace ColonyLeadership;
//
// [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
// public class CompColonyLeader : ThingComp
// {
//     private GameComponent_ColonyLeadership _gameComponent;
//
//     public bool IsLeader => _gameComponent.Leader == parent;
//
//     public CompProperties_ColonyLeader Props => (CompProperties_ColonyLeader)props;
//
//     [SuppressMessage("ReSharper", "ParameterHidesMember")]
//     public override void Initialize(CompProperties props)
//     {
//         base.Initialize(props);
//         _gameComponent = Current.Game.GetComponent<GameComponent_ColonyLeadership>();
//     }
//
//     public override float GetStatFactor(StatDef stat)
//     {
//         var factor = base.GetStatFactor(stat);
//         if (!IsLeader) return factor;
//         return factor * Props.statFactors.GetStatFactorFromList(stat);
//     }
// }
//
// [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
// [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
// [SuppressMessage("ReSharper", "InconsistentNaming")]
// public class CompProperties_ColonyLeader : CompProperties
// {
//     public List<StatModifier> statFactors = [];
//
//     public CompProperties_ColonyLeader()
//     {
//         compClass = typeof(CompColonyLeader);
//     }
// }


