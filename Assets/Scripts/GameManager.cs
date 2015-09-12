using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject GameCanvas;
    public GameObject PauseCanvas;
    public GameObject GameOverCanvas;
    public Slider LifeSlider;
    public Text ScoreText;
    public int Score = 0;
    public Camera Camera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    //CameraRaycast();
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }


    void Shoot()
    {
        Ray ray = Camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            
            string tag = hit.transform.tag;
            if (tag == "Enemy")
            {
                var enemy = hit.transform.gameObject.GetComponent<EnemyHealth>();
                enemy.TakeDamage(20, hit.point);
                Debug.Log("Enemy shoot!!!");
            }
            else
            {
                //Play explotion here
            }
            
        }
            

    }
    void CameraRaycast()
    {
        Ray ray = Camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            print("I'm looking at " + hit.transform.name);
    }

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 10, 10), "");
    }

    public void PauseGame()
    {
        GameCanvas.SetActive(false);
        PauseCanvas.SetActive(true);
    }

    public void ResumeGame()
    {
        PauseCanvas.SetActive(false);
        GameCanvas.SetActive(true);        
    }

    public void ReturnToTitle()
    {
        Application.LoadLevel("Menu");
    }

    void UpdateScore(int points)
    {
        Score += points;

        ScoreText.text = "Score: " + Score;
    }

    public void TowerTakeDamage(float damage)
    {
        float current = LifeSlider.value;

        if (current - damage > 0)
        {
            LifeSlider.value = (current - damage);
        }
        else
        {
            GameOver();
        }
    }

    void GameOver()
    {
        GameCanvas.SetActive(false);
        PauseCanvas.SetActive(false);
        GameOverCanvas.SetActive(true);
    }

    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
