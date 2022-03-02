using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    private RaycastHit2D hit;
    private Vector3 moveDelta;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    protected virtual void UpdateMotor(Vector3 input){
        moveDelta = new Vector3(input.x*xSpeed, input.y * ySpeed,0);

        //set direction
        if(moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1,1,1);

        moveDelta += pushDirection;
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecover);

        hit = Physics2D.BoxCast(transform.position, boxCollider.size,0, new Vector2(0,moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Acting", "Blocking"));
        if (hit.collider == null){
                    //move the character
        transform.Translate(0, moveDelta.y *Time.deltaTime,0);
        }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size,0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Acting", "Blocking"));
        if (hit.collider == null){
                    //move the character
        transform.Translate(moveDelta.x *Time.deltaTime,0,0);
        }
    }
}
