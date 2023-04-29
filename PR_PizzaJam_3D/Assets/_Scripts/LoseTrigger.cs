using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTrigger : MonoBehaviour
{
	[SerializeField] private HumanoidAnimationManager animations;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Snot")
        {
            Gameover();
			animations.PlayDeath();

		}
    }
                

    public void Gameover()
    {
		ServiceLocator.SceneManager.LoadSceneByName("Lose Scene", 5);
		animations.PlayDeath();

	}
}
