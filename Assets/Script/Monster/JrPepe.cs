using UnityEngine;

public class JrPepe : Monster
{
    public float atkSpeed;
    public float atkSpeedNow;
    public override void attackHandle()
    {

    }
    public override void DieHandle()
    {
        base.DieHandle();
        //DropItem
    }
    new public void Hurt(float atk)
    {
        base.Hurt(atk);
        state = State.Hurt;
    }
    void Update()
    {
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            if (waitTime > stiff)
                return;
        }
        if (hp <= 0f)
        {
            DieHandle();
            state = State.Die;
        }
        if (atkSpeedNow >= 0f)
            atkSpeedNow -= Time.deltaTime;

        switch (state)
        {
            case State.Idle:
                if ((GameManager.gameManager.player.transform.position - transform.position).sqrMagnitude < area * area)
                {
                    MoveHandle(GameManager.gameManager.player.transform.position - new Vector3(0f, 0.3f, 0f) - transform.position,-1);
                    state = State.Moving;
                }
                break;
            case State.Moving:
                if ((GameManager.gameManager.player.transform.position - transform.position).sqrMagnitude < area * area)
                {
                    state = State.Moving;
                    MoveHandle(GameManager.gameManager.player.transform.position - new Vector3(0f, 0.3f, 0f) - transform.position,-1);
                }
                else
                {
                    state = State.Idle;
                    IdleHandle();
                }
                break;
            case State.Hurt:
                state = State.Idle;
                IdleHandle();
                break;
            case State.attack:

                break;
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
