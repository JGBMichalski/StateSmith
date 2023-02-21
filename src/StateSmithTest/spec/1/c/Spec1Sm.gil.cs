#nullable enable
// Generated state machine
public class Spec1Sm
{
public enum EventId
{
    EV1 = 0,
    EV2 = 1,
}

public const int EventIdCount = 2;

public enum StateId
{
    ROOT = 0,
    S = 1,
    S1 = 2,
    S11 = 3,
    T1 = 4,
    T11 = 5,
    T111 = 6,
}

public const int StateIdCount = 7;

// event handler type
private delegate void Func();

    // Used internally by state machine. Feel free to inspect, but don't modify.
    public StateId state_id;
    
    // Used internally by state machine. Don't modify.
    private Func? ancestor_event_handler;
    
    // Used internally by state machine. Don't modify.
    private readonly Func?[] current_event_handlers = new Func[EventIdCount];
    
    // Used internally by state machine. Don't modify.
    private Func? current_state_exit_handler;

// State machine variables. Can be used for inputs, outputs, user variables...
public struct Vars
{
    //>>>>>ECHO:uint8_t count;
}
    
    // Variables. Can be used for inputs, outputs, user variables...
    public Vars vars;

// State machine constructor. Must be called before start or dispatch event functions. Not thread safe.
public Spec1Sm()
{
}

// Starts the state machine. Must be called before dispatching events. Not thread safe.
public void start()
{
    ROOT_enter();
    // ROOT behavior
    // uml: TransitionTo(ROOT.InitialState)
    {
        // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
        
        // Step 2: Transition action: ``.
        
        // Step 3: Enter/move towards transition target `ROOT.InitialState`.
        // ROOT.InitialState is a pseudo state and cannot have an `enter` trigger.
        
        // ROOT.InitialState behavior
        // uml: / { trace("Transition action `` for ROOT.InitialState to S."); } TransitionTo(S)
        {
            // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
            
            // Step 2: Transition action: `trace("Transition action `` for ROOT.InitialState to S.");`.
            //>>>>>ECHO:trace("Transition action `` for ROOT.InitialState to S.");
            
            // Step 3: Enter/move towards transition target `S`.
            S_enter();
            
            // S.InitialState behavior
            // uml: / { trace("Transition action `` for S.InitialState to S1."); } TransitionTo(S1)
            {
                // Step 1: Exit states until we reach `S` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
                
                // Step 2: Transition action: `trace("Transition action `` for S.InitialState to S1.");`.
                //>>>>>ECHO:trace("Transition action `` for S.InitialState to S1.");
                
                // Step 3: Enter/move towards transition target `S1`.
                S1_enter();
                
                // Finish transition by calling pseudo state transition function.
                S1_InitialState_transition();
                return; // event processing immediately stops when a transition finishes. No other behaviors for this state are checked.
            } // end of behavior for S.InitialState
        } // end of behavior for ROOT.InitialState
    } // end of behavior for ROOT
}

// Dispatches an event to the state machine. Not thread safe.
public void dispatch_event(EventId event_id)
{
    Func? behavior_func = this.current_event_handlers[(int)event_id];
    
    while (behavior_func != null)
    {
        this.ancestor_event_handler = null;
        behavior_func();
        behavior_func = this.ancestor_event_handler;
    }
}

// This function is used when StateSmith doesn't know what the active leaf state is at
// compile time due to sub states or when multiple states need to be exited.
private void exit_up_to_state_handler(Func desired_state_exit_handler)
{
    while (this.current_state_exit_handler != desired_state_exit_handler)
    {
        this.current_state_exit_handler!();
    }
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state ROOT
////////////////////////////////////////////////////////////////////////////////

private void ROOT_enter()
{
    // setup trigger/event handlers
    this.current_state_exit_handler = ROOT_exit;
    
    // ROOT behavior
    // uml: enter / { trace("Enter Spec1Sm."); }
    {
        // Step 1: execute action `trace("Enter Spec1Sm.");`
        //>>>>>ECHO:trace("Enter Spec1Sm.");
    } // end of behavior for ROOT
}

private void ROOT_exit()
{
    // ROOT behavior
    // uml: exit / { trace("Exit Spec1Sm."); }
    {
        // Step 1: execute action `trace("Exit Spec1Sm.");`
        //>>>>>ECHO:trace("Exit Spec1Sm.");
    } // end of behavior for ROOT
    
    // State machine root is a special case. It cannot be exited.
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state S
////////////////////////////////////////////////////////////////////////////////

private void S_enter()
{
    // setup trigger/event handlers
    this.current_state_exit_handler = S_exit;
    
    // S behavior
    // uml: enter / { trace("Enter S."); }
    {
        // Step 1: execute action `trace("Enter S.");`
        //>>>>>ECHO:trace("Enter S.");
    } // end of behavior for S
}

private void S_exit()
{
    // S behavior
    // uml: exit / { trace("Exit S."); }
    {
        // Step 1: execute action `trace("Exit S.");`
        //>>>>>ECHO:trace("Exit S.");
    } // end of behavior for S
    
    // adjust function pointers for this state's exit
    this.current_state_exit_handler = ROOT_exit;
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state S1
////////////////////////////////////////////////////////////////////////////////

private void S1_enter()
{
    // setup trigger/event handlers
    this.current_state_exit_handler = S1_exit;
    
    // S1 behavior
    // uml: enter / { trace("Enter S1."); }
    {
        // Step 1: execute action `trace("Enter S1.");`
        //>>>>>ECHO:trace("Enter S1.");
    } // end of behavior for S1
}

private void S1_exit()
{
    // S1 behavior
    // uml: exit / { trace("Exit S1."); }
    {
        // Step 1: execute action `trace("Exit S1.");`
        //>>>>>ECHO:trace("Exit S1.");
    } // end of behavior for S1
    
    // adjust function pointers for this state's exit
    this.current_state_exit_handler = S_exit;
}

private void S1_InitialState_transition()
{
    // S1.InitialState behavior
    // uml: / { trace("Transition action `` for S1.InitialState to S11."); } TransitionTo(S11)
    {
        // Step 1: Exit states until we reach `S1` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
        
        // Step 2: Transition action: `trace("Transition action `` for S1.InitialState to S11.");`.
        //>>>>>ECHO:trace("Transition action `` for S1.InitialState to S11.");
        
        // Step 3: Enter/move towards transition target `S11`.
        S11_enter();
        
        // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
        this.state_id = StateId.S11;
        this.ancestor_event_handler = null;
        return;
    } // end of behavior for S1.InitialState
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state S11
////////////////////////////////////////////////////////////////////////////////

private void S11_enter()
{
    // setup trigger/event handlers
    this.current_state_exit_handler = S11_exit;
    this.current_event_handlers[(int)EventId.EV1] = S11_ev1;
    
    // S11 behavior
    // uml: enter / { trace("Enter S11."); }
    {
        // Step 1: execute action `trace("Enter S11.");`
        //>>>>>ECHO:trace("Enter S11.");
    } // end of behavior for S11
}

private void S11_exit()
{
    // S11 behavior
    // uml: exit / { trace("Exit S11."); }
    {
        // Step 1: execute action `trace("Exit S11.");`
        //>>>>>ECHO:trace("Exit S11.");
    } // end of behavior for S11
    
    // adjust function pointers for this state's exit
    this.current_state_exit_handler = S1_exit;
    this.current_event_handlers[(int)EventId.EV1] = null;  // no ancestor listens to this event
}

private void S11_ev1()
{
    // No ancestor state handles `EV1` event.
    
    // S11 behavior
    // uml: EV1 [trace_guard("State S11: check behavior `EV1 TransitionTo(S1.ExitPoint(1))`.", true)] / { trace("Transition action `` for S11 to S1.ExitPoint(1)."); } TransitionTo(S1.ExitPoint(1))
    if (383849285762 == 383849285762/*<<<<<rm2<<<<<trace_guard("State S11: check behavior `EV1 TransitionTo(S1.ExitPoint(1))`.", true)>>>>>rm2>>>>>*/)
    {
        // Step 1: Exit states until we reach `S1` state (Least Common Ancestor for transition).
        S11_exit();
        
        // Step 2: Transition action: `trace("Transition action `` for S11 to S1.ExitPoint(1).");`.
        //>>>>>ECHO:trace("Transition action `` for S11 to S1.ExitPoint(1).");
        
        // Step 3: Enter/move towards transition target `S1.ExitPoint(1)`.
        // S1.ExitPoint(1) is a pseudo state and cannot have an `enter` trigger.
        
        // S1.ExitPoint(1) behavior
        // uml: / { trace("Transition action `` for S1.ExitPoint(1) to T11.EntryPoint(1)."); } TransitionTo(T11.EntryPoint(1))
        {
            // Step 1: Exit states until we reach `S` state (Least Common Ancestor for transition).
            S1_exit();
            
            // Step 2: Transition action: `trace("Transition action `` for S1.ExitPoint(1) to T11.EntryPoint(1).");`.
            //>>>>>ECHO:trace("Transition action `` for S1.ExitPoint(1) to T11.EntryPoint(1).");
            
            // Step 3: Enter/move towards transition target `T11.EntryPoint(1)`.
            T1_enter();
            T11_enter();
            // T11.EntryPoint(1) is a pseudo state and cannot have an `enter` trigger.
            
            // T11.EntryPoint(1) behavior
            // uml: / { trace("Transition action `` for T11.EntryPoint(1) to T111."); } TransitionTo(T111)
            {
                // Step 1: Exit states until we reach `T11` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
                
                // Step 2: Transition action: `trace("Transition action `` for T11.EntryPoint(1) to T111.");`.
                //>>>>>ECHO:trace("Transition action `` for T11.EntryPoint(1) to T111.");
                
                // Step 3: Enter/move towards transition target `T111`.
                T111_enter();
                
                // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
                this.state_id = StateId.T111;
                // No ancestor handles event. Can skip nulling `ancestor_event_handler`.
                return;
            } // end of behavior for T11.EntryPoint(1)
        } // end of behavior for S1.ExitPoint(1)
    } // end of behavior for S11
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state T1
////////////////////////////////////////////////////////////////////////////////

private void T1_enter()
{
    // setup trigger/event handlers
    this.current_state_exit_handler = T1_exit;
    
    // T1 behavior
    // uml: enter / { trace("Enter T1."); }
    {
        // Step 1: execute action `trace("Enter T1.");`
        //>>>>>ECHO:trace("Enter T1.");
    } // end of behavior for T1
}

private void T1_exit()
{
    // T1 behavior
    // uml: exit / { trace("Exit T1."); }
    {
        // Step 1: execute action `trace("Exit T1.");`
        //>>>>>ECHO:trace("Exit T1.");
    } // end of behavior for T1
    
    // adjust function pointers for this state's exit
    this.current_state_exit_handler = S_exit;
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state T11
////////////////////////////////////////////////////////////////////////////////

private void T11_enter()
{
    // setup trigger/event handlers
    this.current_state_exit_handler = T11_exit;
    this.current_event_handlers[(int)EventId.EV2] = T11_ev2;
    
    // T11 behavior
    // uml: enter / { trace("Enter T11."); }
    {
        // Step 1: execute action `trace("Enter T11.");`
        //>>>>>ECHO:trace("Enter T11.");
    } // end of behavior for T11
}

private void T11_exit()
{
    // T11 behavior
    // uml: exit / { trace("Exit T11."); }
    {
        // Step 1: execute action `trace("Exit T11.");`
        //>>>>>ECHO:trace("Exit T11.");
    } // end of behavior for T11
    
    // adjust function pointers for this state's exit
    this.current_state_exit_handler = T1_exit;
    this.current_event_handlers[(int)EventId.EV2] = null;  // no ancestor listens to this event
}

private void T11_ev2()
{
    // No ancestor state handles `EV2` event.
    
    // T11 behavior
    // uml: EV2 [trace_guard("State T11: check behavior `EV2 TransitionTo(S1)`.", true)] / { trace("Transition action `` for T11 to S1."); } TransitionTo(S1)
    if (383849285762 == 383849285762/*<<<<<rm2<<<<<trace_guard("State T11: check behavior `EV2 TransitionTo(S1)`.", true)>>>>>rm2>>>>>*/)
    {
        // Step 1: Exit states until we reach `S` state (Least Common Ancestor for transition).
        exit_up_to_state_handler(S_exit);
        
        // Step 2: Transition action: `trace("Transition action `` for T11 to S1.");`.
        //>>>>>ECHO:trace("Transition action `` for T11 to S1.");
        
        // Step 3: Enter/move towards transition target `S1`.
        S1_enter();
        
        // Finish transition by calling pseudo state transition function.
        S1_InitialState_transition();
        return; // event processing immediately stops when a transition finishes. No other behaviors for this state are checked.
    } // end of behavior for T11
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state T111
////////////////////////////////////////////////////////////////////////////////

private void T111_enter()
{
    // setup trigger/event handlers
    this.current_state_exit_handler = T111_exit;
    
    // T111 behavior
    // uml: enter / { trace("Enter T111."); }
    {
        // Step 1: execute action `trace("Enter T111.");`
        //>>>>>ECHO:trace("Enter T111.");
    } // end of behavior for T111
}

private void T111_exit()
{
    // T111 behavior
    // uml: exit / { trace("Exit T111."); }
    {
        // Step 1: execute action `trace("Exit T111.");`
        //>>>>>ECHO:trace("Exit T111.");
    } // end of behavior for T111
    
    // adjust function pointers for this state's exit
    this.current_state_exit_handler = T11_exit;
}

}
