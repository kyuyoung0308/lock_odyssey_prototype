using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collide
{
    // Start is called before the first frame update
    public int damagePoint = 1;
    public float pushForce = 2.0f;

    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    private Animator ani;
    private float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start(){
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
    }
    protected override void Update(){
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space)){
            if(Time.time - lastSwing > cooldown){
                lastSwing = Time.time;
                Swing();
            }
        }
    }
    protected override void OnCollide(Collider2D coll){
        if (coll.tag == "Fighter"){
            if (coll.name == "Player" || coll.name == "key")
                return;

            Damage dmg = new Damage{
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };


            coll.SendMessage("RecieveDamage", dmg);           
        }
        
    }
    private void Swing(){
        ani.SetTrigger("Swing");
    }
}
