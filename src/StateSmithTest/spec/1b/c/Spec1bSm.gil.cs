#nullable enable
// Generated state machine
public class Spec1bSm
{
public enum EventId
{
    T1 = 0,
}

public const int EventIdCount = 1;

public enum StateId
{
    ROOT = 0,
    S = 1,
    S1 = 2,
    S1_1 = 3,
    S2 = 4,
    S2_1 = 5,
}

public const int StateIdCount = 6;

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
public Spec1bSm()
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
        // uml: TransitionTo(S)
        {
            // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
            
            // Step 2: Transition action: ``.
            
            // Step 3: Enter/move towards transition target `S`.
            S_enter();
            
            // S.InitialState behavior
            // uml: TransitionTo(S1)
            {
                // Step 1: Exit states until we reach `S` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
                
                // Step 2: Transition action: ``.
                
                // Step 3: Enter/move towards transition target `S1`.
                S1_enter();
                
                // S1.InitialState behavior
                // uml: TransitionTo(S1_1)
                {
                    // Step 1: Exit states until we reach `S1` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
                    
                    // Step 2: Transition action: ``.
                    
                    // Step 3: Enter/move towards transition target `S1_1`.
                    S1_1_enter();
                    
                    // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
                    this.state_id = StateId.S1_1;
                    // No ancestor handles event. Can skip nulling `ancestor_event_handler`.
                    return;
                } // end of behavior for S1.InitialState
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
}

private void ROOT_exit()
{
    // State machine root is a special case. It cannot be exited.
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state S
////////////////////////////////////////////////////////////////////////////////

private void S_enter()
{
    // setup trigger/event handlers
    this.current_state_exit_handler = S_exit;
}

private void S_exit()
{
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
    this.current_event_handlers[(int)EventId.T1] = S1_t1;
}

private void S1_exit()
{
    // S1 behavior
    // uml: exit / { b(); }
    {
        // Step 1: execute action `b();`
        //>>>>>ECHO:print("b(); ");
    } // end of behavior for S1
    
    // adjust function pointers for this state's exit
    this.current_state_exit_handler = S_exit;
    this.current_event_handlers[(int)EventId.T1] = null;  // no ancestor listens to this event
}

private void S1_t1()
{
    // No ancestor state handles `T1` event.
    
    // S1 behavior
    // uml: T1 [g()] / { t(); } TransitionTo(S2)
    if (383849285762 == 383849285762/*<<<<<rm2<<<<<print("g() ")>>>>>rm2>>>>>*/)
    {
        // Step 1: Exit states until we reach `S` state (Least Common Ancestor for transition).
        exit_up_to_state_handler(S_exit);
        
        // Step 2: Transition action: `t();`.
        //>>>>>ECHO:print("t(); ");
        
        // Step 3: Enter/move towards transition target `S2`.
        S2_enter();
        
        // S2.InitialState behavior
        // uml: / { d(); } TransitionTo(S2_1)
        {
            // Step 1: Exit states until we reach `S2` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
            
            // Step 2: Transition action: `d();`.
            //>>>>>ECHO:print("d(); ");
            
            // Step 3: Enter/move towards transition target `S2_1`.
            S2_1_enter();
            
            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            this.state_id = StateId.S2_1;
            // No ancestor handles event. Can skip nulling `ancestor_event_handler`.
            return;
        } // end of behavior for S2.InitialState
    } // end of behavior for S1
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state S1_1
////////////////////////////////////////////////////////////////////////////////

private void S1_1_enter()
{
    // setup trigger/event handlers
    this.current_state_exit_handler = S1_1_exit;
}

private void S1_1_exit()
{
    // S1_1 behavior
    // uml: exit / { a(); }
    {
        // Step 1: execute action `a();`
        //>>>>>ECHO:print("a(); ");
    } // end of behavior for S1_1
    
    // adjust function pointers for this state's exit
    this.current_state_exit_handler = S1_exit;
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state S2
////////////////////////////////////////////////////////////////////////////////

private void S2_enter()
{
    // setup trigger/event handlers
    this.current_state_exit_handler = S2_exit;
    
    // S2 behavior
    // uml: enter / { c(); }
    {
        // Step 1: execute action `c();`
        //>>>>>ECHO:print("c(); ");
    } // end of behavior for S2
}

private void S2_exit()
{
    // adjust function pointers for this state's exit
    this.current_state_exit_handler = S_exit;
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state S2_1
////////////////////////////////////////////////////////////////////////////////

private void S2_1_enter()
{
    // setup trigger/event handlers
    this.current_state_exit_handler = S2_1_exit;
    
    // S2_1 behavior
    // uml: enter / { e(); }
    {
        // Step 1: execute action `e();`
        //>>>>>ECHO:print("e(); ");
    } // end of behavior for S2_1
}

private void S2_1_exit()
{
    // adjust function pointers for this state's exit
    this.current_state_exit_handler = S2_exit;
}

}
