using Unity.UIWidgets.foundation;
using UnityEngine;

public class Fireball : SkillEffect
{
    public GameObject boom;
    new void Start()
    {
        base.Start();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.name);
        if (collision.tag.Equals("Monster"))
        {
            Monster monster = collision.GetComponent<Monster>();
            monster.Hurt(atk * playerAtk);
            monster.rigidbody2D.AddForce((monster.transform.position - transform.position).normalized * force);
            Effort();
        }
        if (!collision.tag.Equals("Player"))
        {
            Effort();
        }
    }
    public void Effort()
    {
        Destroy();
        Instantiate(boom, transform.position, Quaternion.identity, transform.parent.parent);
    }
    public void Destroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
