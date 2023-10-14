using UnityEngine;

namespace CastleEscape.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;
        public void StartAction(IAction action)
        {
            // There will be no cancel if action is not changed or null.
            
            if (action == currentAction) return;
           
            if (currentAction != null)
            {
                currentAction.Cancel(); 
            }
            currentAction = action;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}
