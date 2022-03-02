using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : Collide
{
    public int damage =1 ;
    public float pushForce= 3;

    protected override void OnCollide(Collider2D coll){
        if(coll.tag == "Fighter" && coll.name == "Player"){
            Damage dmg = new Damage{
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("RecieveDamage", dmg);
        }
    }
}
