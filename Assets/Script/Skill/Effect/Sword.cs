using System.Collections;
using System.Collections.Generic;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;
using UnityEngine;

public class Sword : SkillEffect
{
    public bool attacking;
    public float swordSpeed = -360f;

    private float angle;
    public Collider2D collider2d;

    private void FixedUpdate()
    {
        if (attacking && angle < 110f)
        {
            if (collider2d.enabled == false)
                collider2d.enabled = true;
            transform.Rotate(Vector3.forward, swordSpeed * Time.fixedDeltaTime);
            angle += 360f * Time.fixedDeltaTime;
        }
        else if (attacking)
        {
            collider2d.enabled = false;
            angle = 0f;
            attacking = false;
            if (Mathf.Abs(GameManager.gameManager.player.transform.rotation.y) < 0.1)
                transform.rotation = new Quaternion(0f, 0f, 0f, 1f);
            else
                transform.rotation = new Quaternion(0f, 180f, 0f, 1f);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Monster"))
        {
            Monster monster = collision.GetComponent<Monster>();
            monster.Hurt(atk * playerAtk);
            monster.rigidbody2D.AddForce((monster.transform.position - transform.position).normalized * force);
            Debug.Log((monster.transform.position - transform.position).normalized * force);
            //collision.gameObject.GetComponent<Monster>();
        }
    }
    public void Attack()
    {
        attacking = true;
    }
}
