/** Author's Name:          Han Bi
 *  Last Modified By:       Han Bi
 *  Date Last Modified:     October 12, 2023
 *  Program Description:    Parent class used to handle enemy behaviour
 *  Revision History:       October 12, 2023: Initial Script
 */

using System.Collections;
using UnityEngine;

public abstract class EnemyBehaviours : MonoBehaviour
{
    protected enum States
    {
        Idle,
        Move,
        Attack,
    }

    [SerializeField]
    protected States currentState = States.Idle;

    protected Enemy data;

    //implements behaviour when chasing player
    protected virtual void MoveBehaviour() 
    { 
        MoveToTarget(); 
    }

    private void MoveToTarget()
    {
        if (data.GetTarget() != null)
        {
            Vector2 newPos = Vector2.MoveTowards(transform.position, data.GetTarget().transform.position, data.moveSpeed);
            transform.position = newPos;
        }
    }

    //implements behaviour when reach/touching player
    protected virtual void AttackBehaviour() { }

    //a default behaviour, acts as a catch all
    protected virtual void IdleBehaviour() { }

    //observer
    protected abstract void HandleTargetChange(GameObject newTarget);

    protected virtual void Start()
    {
        data = GetComponent<Enemy>();
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case States.Idle:
                IdleBehaviour();
                break;
            case States.Move:
                MoveBehaviour();
                break;
            case States.Attack:
                AttackBehaviour();
                break;
            default:
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

}
