using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP;

    void Start()
    {
        
    }

    public void TakeDamage(float atk)
    {
        HP -= atk;
        if (HP <= 0)
        {
            Debug.Log("动画，掉落（砍掉）");
            Destroy(transform.parent.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
