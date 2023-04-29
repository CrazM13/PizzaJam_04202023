using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snot_Launcher : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private float snotFireTimer;
    public GameObject Snot;
    public bool isSneezing = true;

	// Animation Settings
	[Header("Animations")]
	[SerializeField] private HumanoidAnimationManager animations;
	[SerializeField] private float windupTime;

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
        if (snotFireTimer <= windupTime && isSneezing == true)
        {
            isSneezing = false;
			animations.PlayAction();
			ServiceLocator.AudioManager.PlayRandomLocal(transform.position, "SneezeSFX");
		}

        if (snotFireTimer <= 0f){
            snotFireTimer = 20f;
            Instantiate(Snot,this.transform.position,this.transform.rotation);
        }
    }

    public void Assault()
    {
		animations.PlayDeath();
		ServiceLocator.SceneManager.LoadSceneByName("Lose Scene", 5);
	}
}
