using StateSmith.Common;
using System.Linq;
using System.Collections.Generic;
using StateSmith.Output.Algos.Balanced1;

#nullable enable

namespace StateSmith.SmGraph;

/// <summary>
/// https://github.com/StateSmith/StateSmith/issues/63
/// </summary>
public class HistoryProcessor
{
    readonly StateMachine sm;
    readonly NameMangler mangler;

    public HistoryProcessor(StateMachine sm, NameMangler mangler)
    {
        this.sm = sm;
        this.mangler = mangler;
    }

    public static void Process(StateMachine sm, NameMangler mangler)
    {
        var processor = new HistoryProcessor(sm, mangler);
        processor.Process();
    }

    public void Process()
    {
        Visit(sm);
    }

    public static void ValidateHistoryState(HistoryVertex h)
    {
        int count = h.TransitionBehaviors().Count();
        if (count > 1)
        {
            throw new VertexValidationException(h, $"A history state can only have a single drawn transition for its default. Found {count}.");
        }
    }

    private void Visit(NamedVertex v)
    {
        // process history continue vertex children first
        var hc = v.SingleChildOrNull<HistoryContinueVertex>();
        if (hc != null)
        {
            ProcessHistoryContinue(hc);
        }

        var h = v.SingleChildOrNull<HistoryVertex>();
        ProcessHistory(h);

        foreach (var c in v.Children<NamedVertex>())
        {
            Visit(c);
        }

        if (hc != null)
        {
            // has to be done after kid states are visited as nested History Continue states may access this one
            hc.RemoveSelf();
        }
    }

    private void ProcessHistoryContinue(HistoryContinueVertex hc)
    {
        HistoryStateValidator.ValidateBeforeTransforming(hc);

        var parent = hc.NonNullParent;

        var ancestorHc = parent.SingleSiblingOrNull<HistoryContinueVertex>();
        if (ancestorHc != null)
        {
            hc.historyVertices.AddRange(ancestorHc.historyVertices);
        }

        var ancestorHistory = parent.SingleSiblingOrNull<HistoryVertex>();
        if (ancestorHistory != null)
        {
            hc.historyVertices.Add(ancestorHistory);
        }

        if (hc.historyVertices.Count == 0)
        {
            throw new VertexValidationException(hc, $"HistoryContinue vertex expects to find a History and/or HistoryContinue vertex two levels up.");
        }

        AddHistoryContinueBehaviors(hc);
    }

    private void AddHistoryContinueBehaviors(HistoryContinueVertex hc)
    {
        var statesToTrack = GetStatesToTrack(hc);

        foreach (var h in hc.historyVertices)
        {
            AddTrackingBehaviors(h, null, statesToTrack);
        }
    }

    private static List<NamedVertex> GetStatesToTrack(Vertex v)
    {
        return v.Siblings<NamedVertex>().Where(s => s.IncomingTransitions.Any()).ToList();
    }

    private void ProcessHistory(HistoryVertex? historyState)
    {
        if (historyState == null)
        {
            return;
        }

        HistoryStateValidator.ValidateBeforeTransforming(historyState);

        sm.historyStates.Add(historyState);
        historyState.stateTrackingVarName = mangler.HistoryVarName(historyState);
        Behavior defaultTransition = historyState.Behaviors.Single();
        defaultTransition.order = Behavior.ELSE_ORDER;

        var statesToTrack = GetStatesToTrack(historyState);
        AddTrackingBehaviors(historyState, defaultTransition, statesToTrack);
    }

    private void AddTrackingBehaviors(HistoryVertex historyState, Behavior? defaultTransition, List<NamedVertex> statesToTrack)
    {
        foreach (var stateToTrack in statesToTrack)
        {
            bool isDefaultTransition = stateToTrack == defaultTransition?.TransitionTarget && defaultTransition.HasActionCode() == false;

            string enumName = mangler.HistoryVarEnumName(historyState);
            string enumValueName = enumName + "." + mangler.HistoryVarEnumValueName(historyState, stateToTrack);

            {
                Behavior enterTrackingBehavior = new(trigger: TriggerHelper.TRIGGER_ENTER, actionCode: $"this.vars.{historyState.stateTrackingVarName} = {enumValueName};");
                enterTrackingBehavior.isGilCode = true;
                stateToTrack.AddBehavior(enterTrackingBehavior);
            }

            if (!isDefaultTransition)
            {
                Behavior historyTransitionBehavior = new(guardCode: $"this.vars.{historyState.stateTrackingVarName} == {enumValueName}", transitionTarget: stateToTrack);
                historyTransitionBehavior.isGilCode = true;
                historyState.AddBehavior(historyTransitionBehavior);
            }
        }
    }
}
