using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEaten : GhostBehavior
{
    public Transform inside;
    public Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        // Check for active self to prevent error when object is destroyed
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ExitTransition());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reverse direction everytime the ghost hits a wall to create the
        // effect of the ghost bouncing around the home
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            ghost.movement.SetDirection(-ghost.movement.direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        //We want to turn force 
        this.ghost.movement.SetDirection(Vector2.up, true);
        this.ghost.movement.rigidbody.isKinematic = true;
        this.ghost.movement.enabled = false;

        Vector3 position = transform.position;

        
        float duration = 0.5f; //how long is the animation
        float elapsed = 0f; //how much time has passed

        //We gonna make an interpolation
        //starting position 
        while (elapsed < duration)
        {
            this.ghost.SetPosition(Vector3.Lerp(position, this.inside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null; 
        }

        elapsed = 0f;

        // Animate exiting the ghost home
        while (elapsed < duration)
        {
            this.ghost.SetPosition(Vector3.Lerp(inside.position, outside.position, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Pick a random direction left or right and re-enable movement
        float startDir = Random.value;
        if (startDir < 0.5f) startDir = 1;
        else startDir = -1f;
        this.ghost.movement.SetDirection(new Vector2(startDir, 0f), true);
        this.ghost.movement.rigidbody.isKinematic = false;
        this.ghost.movement.enabled = true;
    }
}
