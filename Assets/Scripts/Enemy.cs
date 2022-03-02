using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public int expVal = 1;

    public float triggerLen = 1;
    public float chaseLen = 5;

    private bool chase;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start(){
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    protected void FixedUpdate(){
        //check if player in range
        if(Vector3.Distance(playerTransform.position, startingPosition) < chaseLen){
            
            if(Vector3.Distance(playerTransform.position, startingPosition) < triggerLen)
                chase = true;
            if(chase){
                if(!collidingWithPlayer)
                    UpdateMotor((playerTransform.position - transform.position).normalized);
            }
            else
                UpdateMotor(startingPosition - transform.position);
        }
        else{
            UpdateMotor(startingPosition - transform.position);
            chase = false;
        }
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if(hits[i]==null){
                continue;
            }

            if(hits[i].tag == "Fighter" && hits[i].name == "Player"){
                collidingWithPlayer = true;
            }

            hits[i]=null;
        }

    }
    protected override void Death(){
        Destroy(gameObject);
        GameManager.instance.experience += expVal;
        GameManager.instance.ShowText("+"+expVal+"exp",35, Color.blue, transform.position, Vector3.up*40, 1.0f);
    }

}
