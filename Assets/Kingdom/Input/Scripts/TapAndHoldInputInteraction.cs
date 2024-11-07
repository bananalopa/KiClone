
//todo: reenable after Unity 6 stabilization
// now registering interaction not working properly

/*

using UnityEngine;
using UnityEngine.InputSystem;

namespace Kingdom.Input
{

 public class TapAndHoldInputInteraction : IInputInteraction
 {
     public float maxTapDuration = 0.2f;
     public float holdTime = 0.4f;

     private StatusEnum status = StatusEnum.reset;
     private bool isTapped;

     [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
     private static void Initialize()
     {
         //InputSystem.RegisterInteraction<TapAndHoldInputInteraction>();
         // Will execute the static constructor as a side effect.
     }

     public void Process(ref InputInteractionContext context)
     {
         switch (status)
         {
             case StatusEnum.reset:
                 if (context.action.ReadValue<float>() == 0)
                     status = StatusEnum.waiting;
                 break;
             case StatusEnum.waiting:
                 if (context.action.ReadValue<float>() == 1)
                 {
                     status = StatusEnum.tapStarted;
                     context.Started();
                     context.SetTimeout(maxTapDuration);
                 }

                 if (context.timerHasExpired)
                     ResetState(ref context);
                 break;
             case StatusEnum.tapStarted:
                 if (context.action.ReadValue<float>() == 0)
                 {
                     status = StatusEnum.tapPeformed;
                     context.SetTimeout(maxTapDuration);
                 }

                 if (context.timerHasExpired)
                     ResetState(ref context);
                 break;
             case StatusEnum.tapPeformed:
                 if (context.action.ReadValue<float>() == 1)
                 {
                     status = StatusEnum.holdStarted;
                     context.SetTimeout(holdTime);
                 }

                 if (context.timerHasExpired)
                     ResetState(ref context);
                 break;
             case StatusEnum.holdStarted:
                 if (context.action.ReadValue<float>() != 1)
                     ResetState(ref context);
                 if (context.timerHasExpired)
                 {
                     status = StatusEnum.holdPerformed;
                     context.Performed();
                 }

                 break;
             case StatusEnum.holdPerformed:
                 if (context.action.ReadValue<float>() != 1)
                     ResetState(ref context);
                 break;
         }
     }

     void ResetState(ref InputInteractionContext context)
     {
         status = StatusEnum.reset;
         if (context.phase != InputActionPhase.Canceled)
             context.Canceled();
     }

     enum StatusEnum
     {
         reset,
         waiting,
         tapStarted,
         tapPeformed,
         holdStarted,
         holdPerformed
     }

     // Unlike processors, Interactions can be stateful, meaning that you can keep a
     // local state that mutates over time as input is received. The system might
     // invoke the Reset() method to ask Interactions to reset to the local state
     // at certain points.
     public void Reset()
     {
     }
 }
}
*/