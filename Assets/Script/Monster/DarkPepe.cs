using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkPepe : Monster
{
    public GameObject monsterList;
    public GameObject[] monster;
    public float atkSpeed;
    public float atkSpeedNow;
    public override void attackHandle()
    {

    }
    public override void DieHandle()
    {
        base.DieHandle();
        Instantiate(transform.parent.parent.GetComponent<RoomScript>().get_item(), transform.position, Quaternion.identity, transform.parent.parent);
    }
    private void Start()
    {
        InvokeRepeating("skill0", 4f, 4f);
    }
    public void skill0()
    {
        int num = Random.Range(1, 3);
        for(int i = 0; i < num; ++i)
        {
            Debug.Log(Random.Range(0, 100));
            if (Random.Range(0, 100) < 80)
            {
                Instantiate(monster[0], (transform.position + GameManager.gameManager.player.transform.position) / 2, Quaternion.identity, monsterList.transform);
            }
            else
            {
                Instantiate(monster[1], (transform.position + GameManager.gameManager.player.transform.position) / 2, Quaternion.identity, monsterList.transform);
            }
        }
    }
    bool isdie = false;
    void Update()
    {
        if (hp <= 0f)
        {
            if (isdie == false)
            {
                isdie = true;
                DieHandle();
            }
            return;
        }
        if (atkSpeedNow >= 0f)
            atkSpeedNow -= Time.deltaTime;

        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            if (waitTime > stiff)
                return;
        }


        if ((GameManager.gameManager.player.transform.position - transform.position).sqrMagnitude < area * area)
        {
            MoveHandle(-(GameManager.gameManager.player.transform.position.normalized-new Vector3(0f,0.5f,0f)),-1);
        }
        else
        {
            SetAnimator("Idle");
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
