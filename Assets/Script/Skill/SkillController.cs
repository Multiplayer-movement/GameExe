using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public GameObject skillStats;
    public GameObject[] skillIcon;
    public GameObject[] skill;
    public Transform SkillParent;
    public int nowSkill = -1;
    public Sword sword;

    public Entity player;

    int[] chose  = new int[3];
    void Start()
    {
        player = GameManager.gameManager.player.GetComponent<Entity>();
        Show(getSkill());
        SkillParent.gameObject.SetActive(false);
    }
    private int[] getSkill()
    {
        chose[0] = Random.Range(0, skillIcon.Length);
        chose[1] = Random.Range(0, skillIcon.Length);
        chose[2] = Random.Range(0, skillIcon.Length);
        while (chose[1]==chose[0])
            chose[1] = Random.Range(0, skillIcon.Length);
        while (chose[2] == chose[0] || chose[2]==chose[1])
            chose[2] = Random.Range(0, skillIcon.Length);
        return chose;
    }

    public void Show(int[] skillId)
    {
        foreach (Transform child in SkillParent)
            Destroy(child.gameObject);
        for (int i = 0; i < skillId.Length; ++i)
        {
            Instantiate(skillIcon[skillId[i]], SkillParent);
        }
    }

   public void ChooseSkill(int id)
    {
        nowSkill = chose[id];
        Show(getSkill());
    }

    public void Attack(Vector2 dir)
    {
        if (sword.attacking == false)
        {
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y)-0.05f)
                sword.swordSpeed = -360f;
            else
                sword.swordSpeed = 360f;
            sword.Attack();

            if(nowSkill == 0)
            {
                Shot(player.moveDirection, 800f, 0);
            }
            if(nowSkill == 1)
            {
                Shot(player.moveDirection, 800f, 0);
            }
            if(nowSkill == 2)
            {

            }
        }
    }

    public void Shot(Vector2 skillDir, float speed, int skillId)
    {
        skillDir.Normalize();
        GameObject temp = Instantiate(skill[skillId], GameManager.gameManager.player.transform.position, Quaternion.LookRotation(skillDir));
        temp.GetComponent<Rigidbody2D>().AddForce(skillDir * speed);
    }


}
