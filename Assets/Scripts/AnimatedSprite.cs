using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))] 
//constraint for having sprite renderer for animeted object
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer{get; private set;}
    public Sprite[] sprites;
    public float animationTime = 0.25f;
    public int currentFrame{get; private set;}

    public bool loop = true; //Default we want to loop, but we can change if needed

    private void Awake() {
        this.spriteRenderer = GetComponent<SpriteRenderer>();

    }


    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime);
    }

    private void Advance(){
        //checking if sprite renderer is correct
        if(!this.spriteRenderer.enabled) return;

        this.currentFrame++;
        if(this.currentFrame>=this.sprites.Length && this.loop){
            this.currentFrame = 0;
        }

        //si ça déconne mettre une sécurité
        this.spriteRenderer.sprite = this.sprites[this.currentFrame];
    }

    public void Restart(){
        //-1 bc advance 
        this.currentFrame = -1;
        Advance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
