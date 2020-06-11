using System.Collections;
using System.Collections.Generic;
using Unity.UIWidgets.animation;
using UnityEngine;

public class Slime : Monster
{
    public float atkSpeed;
    public float atkSpeedNow;
    public override void attackHandle()
    {

    }
    public new void DieHandle()
    {
        Instantiate(transform.parent.parent.GetComponent<RoomScript>().get_hp(), transform.position,Quaternion.identity,transform.parent.parent);
        CapsuleCollider2D[] components = GetComponents<CapsuleCollider2D>();
        foreach (var x in components)
            x.enabled = false;
        MoveHandle(Vector2.zero,1);
        SetAnimator("Die");
        Invoke("Destroy", 0f);
    }

    void Update()
    {
        if (hp <= 0f)
        {
            DieHandle();
        }
        //CD
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            if (waitTime > stiff)
                return;
        }
        if (atkSpeedNow > 0)
            atkSpeedNow -= Time.deltaTime;
        //move
        if ((GameManager.gameManager.player.transform.position - transform.position).sqrMagnitude < area * area)
        {
            MoveHandle(GameManager.gameManager.player.transform.position - transform.position,1);
        }
 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && atkSpeedNow <= 0f)
        {
            GameManager.gameManager.player.GetComponent<Entity>().HPManager.TakeDamage(atk);
            atkSpeedNow += atkSpeed;
        }
    }
}
