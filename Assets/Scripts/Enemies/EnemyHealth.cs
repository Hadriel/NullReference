using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public int startingHealth = 100;            
	public int currentHealth;                   
	public float sinkSpeed = 2.5f;             
	public int scoreValue = 10; 

	bool isDead = false;
	ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void TakeDamage (int amount, Vector3 hitPoint)
	{
		// If the enemy is dead...
		if(isDead)
			// ... no need to take damage so exit the function.
			return;

		// Reduce the current health by the amount of damage sustained.
		currentHealth -= amount;
		// Set the position of the particle system to where the hit was sustained.
		//hitParticles.transform.position = hitPoint;
		
		// And play the particles.
		//hitParticles.Play();
		// If the current health is less than or equal to zero...
		if(currentHealth <= 0)
		{
			// ... the enemy is dead.
			Death ();
		}
	}
	
	
	void Death ()
	{
		// The enemy is dead.
		isDead = true;
		Destroy (transform.gameObject);

	}
}
