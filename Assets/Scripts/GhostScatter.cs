using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostScatter : GhostBehavior
{

    private void OnDisable()
    {
       
        
        this.ghost.chase.Enable();

        
        
    }



    //Actually random movement 
    private void OnTriggerEnter2D(Collider2D collision)
    {   
      
        this.onCollisionNode= collision.GetComponent<Node>();

        //we want to check for the script not to start when we doesn't want
        if (onCollisionNode != null && this.enabled && !this.ghost.frightened.enabled)
        {
            this.goToTarget(ghost.scatterTarget); 
            
            /*
            int nbDirection = node.availableDirections.Count;
            int index = Random.Range(0, nbDirection);
            if (nbDirection > 1 && (node.availableDirections[index] == -this.ghost.movement.direction)) 
            {
                index++;
                if (index >= nbDirection) index = 0;
            }
            this.ghost.movement.SetDirection(node.availableDirections[index]);*/
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
