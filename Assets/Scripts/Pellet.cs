using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int points = 10;


    //We want to be able to override it in PowerPellet
    protected virtual void Eat(){
        FindObjectOfType<GameManager>().PelletEaten(this);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Pacman")){
            Eat();
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
