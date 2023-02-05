using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    public int ghostMultiplier{get; private set;}
    public int score{get; private set;} //=> public getter, private setter
    public int lives{get; private set;} 


    //Fonction de démarrage
    private void Start(){
        NewGame();
    }

    private void Update(){
        if(this.lives <= 0){
            //if q => quit if r => restart
            if(Input.GetKeyDown("r")){
                NewGame();
        
            }//else TODO
        }
    }

    private void NewGame(){
        SetScore(0);
        SetLives(3);
        NewRound();

    }

    private void NewRound(){
        foreach(Transform pellet in this.pellets){
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState(){
        ResetGhostMult();
        for(int i = 0; i<this.ghosts.Length; i++){
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();
    }


    private void GameOver(){
        //TODO UI
        for(int i = 0; i<this.ghosts.Length; i++){
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
    }

    private void SetScore(int score){
        this.score = score;
    }

    private void SetLives(int lives){
        this.lives = lives;
    }

    public void GhostEaten(Ghost ghost){
        SetScore(this.score + this.ghostMultiplier * ghost.points); 
        this.ghostMultiplier++;
    }

    public void PacmanEaten(){
        this.pacman.gameObject.SetActive(false);

        SetLives(this.lives - 1);
        //If pacman still have lives, reset ghost & pacman, else GameOver
        if(this.lives > 0){
            Invoke(nameof(ResetState), 3.0f); //set a delay of 3sec
        }else{
            GameOver();
        }
    }

    public void PelletEaten(Pellet pellet){
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);
        
        if(!HasRemainingPellets()) {
            Debug.Log(true);
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }
    
    public void PowerPelletEaten(PowerPellet pellet){

        for(int i = 0; i < this.ghosts.Length; i++)
        {
            if (!this.ghosts[i].frightened.eaten) this.ghosts[i].frightened.Enable(pellet.duration);
        }
        PelletEaten(pellet);
        CancelInvoke(); // allow to reset timer if 2 power pellets are eaten
        Invoke(nameof(ResetGhostMult), pellet.duration);
    }

    private bool HasRemainingPellets(){
        foreach(Transform pellet in this.pellets){
            if(pellet.gameObject.activeSelf) return true;
        }
        return false;
    }

    private void ResetGhostMult(){
        this.ghostMultiplier = 1;
    }
}
