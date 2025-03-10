/** Author's Name:          Han Bi
 *  Last Modified By:       Han Bi
 *  Date Last Modified:     November 2, 2023
 *  Program Description:    Used for the 'container' that holds the swarm of enemies
 *  Revision History:       November 2, 2023: Initial Script
 */

using UnityEngine;

public class SwarmBehaviour : EnemyBehaviours
{
    protected override void HandleTargetChange(GameObject newTarget)
    {
        if(newTarget != null)
        {
            currentState = States.Move;
        }
    }

    protected override void MoveBehaviour()
    {
        base.MoveBehaviour();
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (data.GetTarget() != null)
        {
            Vector2 newPos = Vector2.MoveTowards(transform.position, data.GetTarget().transform.position, data.moveSpeed);
            transform.position = newPos;
        }

        if (TargetReached())
        {
            Destroy(data.GetTarget());
            Destroy(gameObject);
        }

    }

    private bool TargetReached()
    {
        if(Vector2.Distance(transform.position, data.GetTarget().transform.position) < 0.01)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



}
