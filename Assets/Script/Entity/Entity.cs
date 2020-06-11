using UnityEngine;
using System.Collections;
using System;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;

public class Entity : MonoBehaviour
{
    public PlayerStats HPManager;
    public SkillController skillController;

    public Animator animator;
    private new Rigidbody2D rigidbody2D;

    public float atk;
    public enum State
    {
        Idle,
        Move,
        Attack,
        Skill,
        Die,
        TakeDamage,
    }

    public float speed;
    public Vector2 moveDirection = new Vector2(-1, 0);
    private State state = State.Idle;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void EntityMove(Vector2 dir)
    {
        dir *= speed * Time.deltaTime;
        rigidbody2D.MovePosition(transform.position + new Vector3(dir.x, dir.y, 0));
        //rigidbody2D.AddForce(dir*300);
    }
    void AttackHandle()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            skillController.Attack(moveDirection);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (skillController.SkillParent.gameObject.activeSelf)
                skillController.SkillParent.gameObject.SetActive(false);
            else
                skillController.SkillParent.gameObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            skillController.ChooseSkill(0);
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            skillController.ChooseSkill(1);
        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            skillController.ChooseSkill(2);
        }


        /*if (Input.GetKeyUp(KeyCode.U))
        {
            skillController.Shot(moveDirection, 10f, 1);
        }*/

    }
    void SetAnimoterTrue()
    {
        animator.SetBool("Moving", true);
    }
    void SetAnimatorFalse()
    {
        animator.SetBool("Moving", false);
    }
    void MoveHandle(bool isTurn)
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        if (Mathf.Abs(moveX) > float.Epsilon || Mathf.Abs(moveY) > float.Epsilon)
        {
            SetAnimoterTrue();
            moveDirection = new Vector2(moveX, moveY);
            if (moveX > 0 && isTurn)
                transform.rotation = Quaternion.Euler(Vector3.zero);
            else if (isTurn)
                transform.rotation = Quaternion.Euler(Vector3.down * 180f);
            EntityMove(moveDirection);
        }
        else
        {
            SetAnimatorFalse();
            EntityMove(Vector2.zero);
        }
        /*八方控制器
        if (Input.GetKey(KeyCode.W))
            moveY = 1;
        else if (Input.GetKey(KeyCode.S))
            moveY = -1;
        else
            moveY = 0;
        if (Input.GetKey(KeyCode.A))
            moveX = -1;
        else if (Input.GetKey(KeyCode.D))
            moveX = 1;
        else
            moveX = 0;
        if (Math.Abs(moveX) + Math.Abs(moveY) > 0)
        {
            moveDirection = new Vector2(moveX, moveY);
            moveDirection.Normalize();
            EntityMove(moveDirection);
        }
        else
        {
            EntityMove(Vector2.zero);
        }
        */
    }
    bool isre = false;
    void Update()
    {
        if (HPManager.health < 0.1f)
        {
            if (isre == false)
            {
                Invoke("diediedie", 2f);
                isre = true;
            }
            return;
        }
        AttackHandle();
        MoveHandle(skillController.sword.attacking == false);

        if (Input.GetKeyUp(KeyCode.F1))
            HPManager.AddMaxHealth(7);
        if (Input.GetKeyUp(KeyCode.F2))
            HPManager.TakeDamage(5.5f);
        if (Input.GetKeyUp(KeyCode.F3))
            HPManager.Heal(3.25f);
    }
    public void diediedie()
    {
        GameManager.gameManager.LevelUp();
        HPManager.health = HPManager.maxHealth;
    }
}