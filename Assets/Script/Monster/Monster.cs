using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    public float hp;
    public float maxhp;
    public float speed;
    public float atk;
    public float area;
    public float waitTime;
    public float stiff;
    public enum State
    {
        Idle,
        Moving,
        attack,
        Hurt,
        Die,
    }
    public Animator animator;
    public new Rigidbody2D rigidbody2D;
    public State state = State.Idle;

    public void SetAnimator(string name)
    {
        animator.SetBool("Hurt", false);
        animator.SetBool("Moving", false);
        if (!name.Equals("Idle"))
            animator.SetBool(name, true);
    }
    public void IdleHandle()
    {
        SetAnimator("Idle");
    }
    public void MoveHandle(Vector2 dir,int reversal)
    {
        SetAnimator("Moving");
        dir.x *= Random.Range(0.1f, 10f);
        dir.y *= Random.Range(0.1f, 10f);
        dir.Normalize();
        dir *= speed * Time.deltaTime;
        if (reversal*dir.x > 0)
            turn(true);
        else
            turn(false);
        rigidbody2D.MovePosition(transform.position + new Vector3(dir.x, dir.y, 0));
    }
    public abstract void attackHandle();
    public virtual void DieHandle()
    {
        CapsuleCollider2D[] components = GetComponents<CapsuleCollider2D>();
        foreach(var x in components)
            x.enabled = false;
        MoveHandle(Vector2.zero,1);
        SetAnimator("Die");
        Invoke("Destroy", 1f);
    }

    public void Hurt(float atk)
    {
        SetAnimator("Hurt");
        hp -= atk;
        if (waitTime <= 0)
            waitTime += stiff * 2;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void turn(bool isRight)
    {
        if (isRight)
            transform.rotation = Quaternion.Euler(Vector3.zero);
        else
            transform.rotation = Quaternion.Euler(Vector3.down * 180f);
    }
}
