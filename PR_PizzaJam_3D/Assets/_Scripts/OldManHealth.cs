using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldManHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public GameObject healthBarUI;
    public Slider slider;

	[Header("Animations")]
	[SerializeField] private HumanoidAnimationManager animations;

    void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();
    }

    void Update()
    {
        slider.value = CalculateHealth();
        
        if(health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }

        if(health <=0)
        {
            
            Destroy(gameObject);
        }

        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Snot")
        {
            OldmanHealth();
            Debug.Log("OUCH");

            // Update health prioir to animation play
            health = health - 1;

			// Play animation
			if (health <= 0) animations.PlayDeath();
			else animations.PlayHit();

		}
    }

    public void OldmanHealth()
    {
        Debug.Log("GAMEOVER");
        animations.PlayHit();

    }
}


