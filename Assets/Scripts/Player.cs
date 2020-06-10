using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public int maxHealth = 100;
	public int currentHealth;
	public GameObject instructions;
	public HealthBar healthBar;

	// Start is called before the first frame update
	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}

	// Update is called once per frame
	void Update()
	{
		//if (Input.GetKeyDown(KeyCode.H))
		//{
		//	Debug.Log("takedamage");
		//	TakeDamage(20);
		//}
	}
    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {
			//Debug.Log("takedamage");
			TakeDamage(20);
			if (currentHealth <= 0)
			{
				Debug.Log("ik ben dood");
				instructions.SetActive(true);
				StartCoroutine(screenDelay());
				
			}
		}
		
    }
	IEnumerator screenDelay()
	{
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene("MainMenu");
	}

	void TakeDamage(int damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);
	}
}
