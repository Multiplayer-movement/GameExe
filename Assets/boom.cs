using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", 0.3f);
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
