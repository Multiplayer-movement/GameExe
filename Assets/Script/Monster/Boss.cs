using System.Collections;
using System.Collections.Generic;
using Unity.UIWidgets.animation;
using UnityEngine;

public class Boss : Monster
{
    private float skillCD = 4f;
    private float nowSkillCD = 0f;
    public GameObject monsterList;
    public GameObject monster;
    public override void attackHandle()
    {
    }
    public override void DieHandle()
    {
        Destroy();
    }

    public void skill0()
    {
        Instantiate(monster, (transform.position + GameManager.gameManager.player.transform.position) / 2, Quaternion.identity, monsterList.transform);
    }
    void Update()
    {
        if (hp <= 0f)
        {
            DieHandle();
        }
        if (nowSkillCD >= 0f)
            nowSkillCD -= Time.deltaTime;
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            if (waitTime > stiff)
                return;
        }
        if (nowSkillCD <= 0)
        {
            skill0();
            nowSkillCD += skillCD + hp/maxhp * skillCD;
            waitTime += 1f;
            Debug.Log("技能动画");
        }
        if ((GameManager.gameManager.player.transform.position - transform.position).sqrMagnitude < area * area)
        {
            MoveHandle(-GameManager.gameManager.player.transform.position + transform.position,1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.gameManager.player.GetComponent<Entity>().HPManager.TakeDamage(atk);
        }
    }
}
