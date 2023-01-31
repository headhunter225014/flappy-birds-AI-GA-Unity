//Damir Zababuryn ID : 22022843
//Marko Sudar ID: 22022844

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour
{
    public DNA dna;
    //object for raycast
    public GameObject eyes;
    Vector3 startPosition;
    Rigidbody2D rb;  
	int DNALength = 5;
    //boolean values that represent collision with specific element
    bool seeDownWall; 
    bool seeUpWall; 
    bool seeBottom;
    bool seeTop;
    public float timeAlive = 0;
    public float distTravelled = 0;
    public int crash = 0;
    bool alive = true;
    
    //inititalizing DNA 
	public void Init()
    {
        
        dna = new DNA(DNALength,200);
        //birds init with small differences in the position
        this.transform.Translate(Random.Range(-1.5f,1.5f),Random.Range(-1.5f,1.5f),0);
        startPosition = this.transform.position;
        //gets the objects rigidbody
        rb = this.GetComponent<Rigidbody2D>();
	}
    
    void Update()
    {
        //if bird is dead do nothing
        if(!alive) return;
        
        //all bools are false 
        seeUpWall = false;
        seeDownWall = false;
        seeTop = false;
        seeBottom = false;
        
        //debugging just to check how rays are working
        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 1.0f, Color.red);
        Debug.DrawRay(eyes.transform.position, eyes.transform.up* 1.0f, Color.red);
        Debug.DrawRay(eyes.transform.position, -eyes.transform.up* 1.0f, Color.red);
        
        //casting rays from the eyes of birds
        RaycastHit2D hit = Physics2D.Raycast(eyes.transform.position, 
            eyes.transform.forward, 1.0f);
        //if hits any wall change see prop to true
        if (hit.collider != null)
        {
            if(hit.collider.gameObject.tag == "upwall")
            {
                seeUpWall = true;
            }
            else if(hit.collider.gameObject.tag == "downwall")
            {
                seeDownWall = true;
            }
        }
        //casting rays to chekc the top wall
		hit = Physics2D.Raycast(eyes.transform.position, eyes.transform.up, 1.0f);
		if (hit.collider != null)
        {
            if(hit.collider.gameObject.tag == "top")
            {
                seeTop = true;
            }
        }
        
        //casting rays to see the bottom wall
        hit = Physics2D.Raycast(eyes.transform.position, -eyes.transform.up, 1.0f);
		if (hit.collider != null)
        {    
            if(hit.collider.gameObject.tag == "bottom")
            {
                seeBottom = true;
            }
        }
        timeAlive = PopManager.elapsed;
    }
    
    
    //used to move birds
    void FixedUpdate()
    {
        if(!alive) return;
        
        // read DNA
        float up = 0;
        float forward = 1.0f;

        //upforce = 
        if(seeUpWall)
        { 
            up = dna.GetGene(0);
        }
        else if(seeDownWall)
        {
        	up = dna.GetGene(1);
        }
        else if(seeTop)
        {
        	up = dna.GetGene(2);
        }        
        else if(seeBottom)
        {
        	up = dna.GetGene(3);
        }
        else
        {
        	up = dna.GetGene(4);
        }
        
        //always moving forward
        rb.AddForce(this.transform.right * forward);
        //upforce to upvector
        rb.AddForce(this.transform.up * up * 0.1f);
        distTravelled = Vector3.Distance(startPosition,this.transform.position);
    }
    
    //checks if an object hit smth
    /*void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("dead") || 
           col.gameObject.CompareTag("top") ||
           col.gameObject.CompareTag("bottom") ||
           col.gameObject.CompareTag("upwall") ||
           col.gameObject.CompareTag("downwall"))
        {
            crash++;
        }
        else if (col.gameObject.CompareTag("dead"))
            alive = false;
    }
    */
}

