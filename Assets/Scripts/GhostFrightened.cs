using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;


    public bool eaten { get; private set; }


    public override void Enable(float duration)
    {
        

        base.Enable(duration);

        this.body.enabled = false;
        this.eyes.enabled = false;
        this.blue.enabled = true;
        this.white.enabled = true;

        Invoke(nameof(Flash), this.duration / 2.0f);
    }

    public override void Disable()
    {
        base.Disable();
        this.body.enabled = true;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled= false;
    }

    private void Flash()
    {
        if (!this.eaten)
        {
            this.blue.enabled = false;
            this.white.enabled= true;
            this.white.GetComponent<AnimatedSprite>().Restart();
        }
    }

    private void OnEnable()
    {
        this.ghost.movement.speedMultiplier = 0.5f;
        this.eaten = false;
    }

    private void OnDisable()
    {
        this.ghost.movement.speedMultiplier = 1.0f;
        this.eaten = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman")){
            if (this.enabled) Eaten();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the ghost is "home" after being eaten 
        if (this.enabled && this.eaten && (collision.gameObject.layer == LayerMask.NameToLayer("Entry"))) {
            this.ghost.SetPosition(ghost.eaten.inside.position);
            ghost.eaten.Enable(duration);
            this.Disable();
            this.ghost.eaten.Enable(this.duration);

            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), FindObjectOfType<GameManager>().pacman.GetComponent<Collider2D>(), false);


        }
        else if (this.enabled && (collision.gameObject.layer == LayerMask.NameToLayer("Node"))){

            this.onCollisionNode = collision.gameObject.GetComponent<Node>();
            if (this.eaten)
            {
                this.goToTarget(this.ghost.entryTarget);
            }

            else Escape(collision);
        }
    }

    private void Escape(Collider2D collision) {
        //hardcode for ghosts to move away from pacman when they're frightened
        Node node = collision.gameObject.GetComponent<Node>();

        Vector2 direction = Vector2.zero;
        float maxDistance = float.MinValue;

        // Among all takeable directions, find the one that 
        //is the farest from pacman
        foreach (Vector2 availableDirection in node.availableDirections)
        {

            Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
            float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

            if (distance > maxDistance)
            {
                direction = availableDirection;
                maxDistance = distance;
            }
        }

        ghost.movement.SetDirection(direction);
    }

    private void Eaten()
    {
        CancelInvoke();
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), FindObjectOfType<GameManager>().pacman.GetComponent<Collider2D>());
        //CancelInvoke(nameof(Disable));
        this.ghost.movement.speedMultiplier = 1.5f;
        this.eaten = true;

        //update
        this.body.enabled = false;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
    }

}
