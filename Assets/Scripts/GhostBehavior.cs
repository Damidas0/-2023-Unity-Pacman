using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[RequireComponent(typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour
{
    public Ghost ghost { get; private set; }
    public float duration;
    public Node onCollisionNode;

    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
        this.enabled = false;
    }

    public void Enable()
    {
        Enable(duration);
    }

    public virtual void Enable(float duration)
    {
        this.enabled = true;
        CancelInvoke(); 
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        this.enabled = false;
    }

    public void goToTarget(Transform target)
    {
        goToTarget(target, false);
    }


    public void goToTarget(Transform target, bool frightened)
    {
        Vector2 direction = Vector2.zero;
        float minDistance = float.MaxValue;

        //We take all the avaliable directions and pop the one behind 
        //Bc Ghosts can't make 180° 
        List<Vector2> takeableDirections = new List<Vector2>(this.onCollisionNode.availableDirections);
        takeableDirections.Remove(-this.ghost.movement.direction);


        // Among all takeable directions, find the one that 
        //is the closest to our target
        foreach (Vector2 availableDirection in takeableDirections)
        {

            Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
            float distance = (target.position - newPosition).sqrMagnitude;

            if (distance < minDistance)
            {
                direction = availableDirection;
                minDistance = distance;
            }
        }

        ghost.movement.SetDirection(direction);
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        this.onCollisionNode = null;
    }
}
