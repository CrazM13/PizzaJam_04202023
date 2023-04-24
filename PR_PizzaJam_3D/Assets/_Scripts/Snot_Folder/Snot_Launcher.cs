using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snot_Launcher : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private float snotFireTimer;
    public GameObject Snot;
    public bool isSneezing = true;

    void Start()
    {
        snotFireTimer = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        snotFireTimer-= Time.deltaTime;
        if(snotFireTimer > 10f)
        {
            isSneezing = true;
        }
        //Change 2f to whatever seconds necessary.
        if (snotFireTimer <= 2f && isSneezing == true)
        {
            isSneezing = false;
            //Play animation here
        }

        if (snotFireTimer <= 0f){
            snotFireTimer = 20f;
            Instantiate(Snot,this.transform.position,this.transform.rotation);
			ServiceLocator.AudioManager.PlayRandomLocal(transform.position, "SneezeSFX");
        }
    }

    public void Assault()
    {
        Debug.Log("Game Over.");
    }
}
