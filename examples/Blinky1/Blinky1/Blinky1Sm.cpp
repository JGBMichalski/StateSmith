// Autogenerated with StateSmith
#include "Blinky1Sm.h"
// this ends up in the generated .cpp file
#include "Arduino.h"
#include <stdbool.h> // required for `consume_event` flag
#include <string.h> // for memset

static void ROOT_enter(Blinky1Sm* self);
static void ROOT_exit(Blinky1Sm* self);

static void LED_OFF_enter(Blinky1Sm* self);
static void LED_OFF_exit(Blinky1Sm* self);
static void LED_OFF_do(Blinky1Sm* self);

static void LED_ON_enter(Blinky1Sm* self);
static void LED_ON_exit(Blinky1Sm* self);
static void LED_ON_do(Blinky1Sm* self);

// This function is used when StateSmith doesn't know what the active leaf state is at compile time due to sub states
// or when multiple states need to be exited.
static void exit_up_to_state_handler(Blinky1Sm* self, const Blinky1Sm_Func desired_state_exit_handler);


void Blinky1Sm_ctor(Blinky1Sm* self)
{
    memset(self, 0, sizeof(*self));
}

static void exit_up_to_state_handler(Blinky1Sm* self, const Blinky1Sm_Func desired_state_exit_handler)
{
    while (self->current_state_exit_handler != desired_state_exit_handler)
    {
        self->current_state_exit_handler(self);
    }
}

void Blinky1Sm_start(Blinky1Sm* self)
{
    ROOT_enter(self);
    // ROOT behavior
    // uml: TransitionTo(ROOT.InitialState)
    {
        // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
        
        // Step 2: Transition action: ``.
        
        // Step 3: Enter/move towards transition target `ROOT.InitialState`.
        // ROOT.InitialState is a pseudo state and cannot have an `enter` trigger.
        
        // ROOT.InitialState behavior
        // uml: TransitionTo(LED_OFF)
        {
            // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition). Already at LCA, no exiting required.
            
            // Step 2: Transition action: ``.
            
            // Step 3: Enter/move towards transition target `LED_OFF`.
            LED_OFF_enter(self);
            
            // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
            self->state_id = Blinky1Sm_StateId_LED_OFF;
            // No ancestor handles event. Can skip nulling `self->ancestor_event_handler`.
            return;
        } // end of behavior for ROOT.InitialState
    } // end of behavior for ROOT
}

void Blinky1Sm_dispatch_event(Blinky1Sm* self, enum Blinky1Sm_EventId event_id)
{
    Blinky1Sm_Func behavior_func = self->current_event_handlers[event_id];
    
    while (behavior_func != NULL)
    {
        self->ancestor_event_handler = NULL;
        behavior_func(self);
        behavior_func = self->ancestor_event_handler;
    }
}

const char* Blinky1Sm_state_id_to_string(const enum Blinky1Sm_StateId id)
{
    switch (id)
    {
        case Blinky1Sm_StateId_ROOT: return "ROOT";
        case Blinky1Sm_StateId_LED_OFF: return "LED_OFF";
        case Blinky1Sm_StateId_LED_ON: return "LED_ON";
        default: return "?";
    }
}

////////////////////////////////////////////////////////////////////////////////
// event handlers for state ROOT
////////////////////////////////////////////////////////////////////////////////

static void ROOT_enter(Blinky1Sm* self)
{
    // setup trigger/event handlers
    self->current_state_exit_handler = ROOT_exit;
}

static void ROOT_exit(Blinky1Sm* self)
{
    // State machine root is a special case. It cannot be exited.
    (void)self;  // nothing to see here compiler. move along!
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state LED_OFF
////////////////////////////////////////////////////////////////////////////////

static void LED_OFF_enter(Blinky1Sm* self)
{
    // setup trigger/event handlers
    self->current_state_exit_handler = LED_OFF_exit;
    self->current_event_handlers[Blinky1Sm_EventId_DO] = LED_OFF_do;
    
    // LED_OFF behavior
    // uml: enter / { turn_led_off(); }
    {
        // Step 1: execute action `turn_led_off();`
        digitalWrite(LED_BUILTIN, LOW);;
    } // end of behavior for LED_OFF
    
    // LED_OFF behavior
    // uml: enter / { reset_timer(); }
    {
        // Step 1: execute action `reset_timer();`
        self->vars.timer_started_at_ms = millis();
    } // end of behavior for LED_OFF
}

static void LED_OFF_exit(Blinky1Sm* self)
{
    // adjust function pointers for this state's exit
    self->current_state_exit_handler = ROOT_exit;
    self->current_event_handlers[Blinky1Sm_EventId_DO] = NULL;  // no ancestor listens to this event
}

static void LED_OFF_do(Blinky1Sm* self)
{
    // No ancestor state handles `do` event.
    
    // LED_OFF behavior
    // uml: do [after_ms(500)] TransitionTo(LED_ON)
    if (( (millis() - self->vars.timer_started_at_ms) >= 500 ))
    {
        // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition).
        LED_OFF_exit(self);
        
        // Step 2: Transition action: ``.
        
        // Step 3: Enter/move towards transition target `LED_ON`.
        LED_ON_enter(self);
        
        // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
        self->state_id = Blinky1Sm_StateId_LED_ON;
        // No ancestor handles event. Can skip nulling `self->ancestor_event_handler`.
        return;
    } // end of behavior for LED_OFF
}


////////////////////////////////////////////////////////////////////////////////
// event handlers for state LED_ON
////////////////////////////////////////////////////////////////////////////////

static void LED_ON_enter(Blinky1Sm* self)
{
    // setup trigger/event handlers
    self->current_state_exit_handler = LED_ON_exit;
    self->current_event_handlers[Blinky1Sm_EventId_DO] = LED_ON_do;
    
    // LED_ON behavior
    // uml: enter / { turn_led_on();\nreset_timer(); }
    {
        // Step 1: execute action `turn_led_on();\nreset_timer();`
        digitalWrite(LED_BUILTIN, HIGH);;
        self->vars.timer_started_at_ms = millis();
    } // end of behavior for LED_ON
}

static void LED_ON_exit(Blinky1Sm* self)
{
    // adjust function pointers for this state's exit
    self->current_state_exit_handler = ROOT_exit;
    self->current_event_handlers[Blinky1Sm_EventId_DO] = NULL;  // no ancestor listens to this event
}

static void LED_ON_do(Blinky1Sm* self)
{
    // No ancestor state handles `do` event.
    
    // LED_ON behavior
    // uml: do [elapsed_ms > 1000] TransitionTo(LED_OFF)
    if ((millis() - self->vars.timer_started_at_ms) > 1000)
    {
        // Step 1: Exit states until we reach `ROOT` state (Least Common Ancestor for transition).
        LED_ON_exit(self);
        
        // Step 2: Transition action: ``.
        
        // Step 3: Enter/move towards transition target `LED_OFF`.
        LED_OFF_enter(self);
        
        // Step 4: complete transition. Ends event dispatch. No other behaviors are checked.
        self->state_id = Blinky1Sm_StateId_LED_OFF;
        // No ancestor handles event. Can skip nulling `self->ancestor_event_handler`.
        return;
    } // end of behavior for LED_ON
}


