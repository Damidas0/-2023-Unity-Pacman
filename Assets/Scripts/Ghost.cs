using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement movement { get; private set; }

    public int points = 200;
    public GhostEaten eaten { get; private set; }   
    public GhostScatter scatter { get; private set; }  
    public GhostChase chase { get; private set; }
    public GhostFrightened frightened { get; private set; }

    //for the initialisation 
    [field: SerializeField] //force unity to expose the property
    public GhostBehavior initialBehavior { get; private set; }
    
    //All the different targetting we want
    public Transform target;
    public Transform scatterTarget;
    public Transform entryTarget;


    public Vector3 ScatterTarge;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.eaten= GetComponent<GhostEaten>();
        this.scatter= GetComponent<GhostScatter>();
        this.chase = GetComponent<GhostChase>();  
        this.frightened= GetComponent<GhostFrightened>(); 
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.frightened.Disable();
        this.chase.Disable();
        this.scatter.Enable();

        if(this.eaten != this.initialBehavior)
        {
            this.eaten.Disable();
        }
        if (this.initialBehavior != null)
        {
            this.initialBehavior.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.frightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }

    public void SetPosition(Vector3 position)
    {
        // Keep the z-position the same since it determines draw depth
        position.z = transform.position.z;
        transform.position = position;
    }
}
