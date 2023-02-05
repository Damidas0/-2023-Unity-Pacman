using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehavior
{
    private void OnDisable()
    {
        /*if (this.onCollisionNode == null)
        {
            this.ghost.movement.SetDirection(-this.ghost.movement.direction);
        }*/
        ghost.scatter.Enable();

    }



    private void OnTriggerEnter2D(Collider2D collider)
    {
        this.onCollisionNode= collider.GetComponent<Node>();

        //Activation conditions
        if (this.onCollisionNode != null && this.enabled && !ghost.frightened.enabled)
        {
            this.goToTarget(this.ghost.target);
        }
    }
}
