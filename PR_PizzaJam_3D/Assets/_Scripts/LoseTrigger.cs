using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTrigger : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Snot")
        {
            Gameover();
        }
    }
                

    public void Gameover()
    {
        Debug.Log("GAMEOVER");
    }
}
