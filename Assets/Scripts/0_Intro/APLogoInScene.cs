using UnityEngine;
using System.Collections;

public class APLogoInScene : MonoBehaviour 
{
	public string nextScene;
	public float speed;
	public float timeToNextScene;

	private float x;
	private bool changeToNextScene;

	void Awake()
	{
		x = transform.position.x;
	}

	void Update() 
	{
		x -= Time.deltaTime * speed;

		if(x <= 0)
		{
			x = 0;
			timeToNextScene -= Time.deltaTime;
		}

		if(timeToNextScene <= 0)
		{
			changeToNextScene = true;
		}

		if(changeToNextScene || Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel(nextScene);
		}
		
		transform.position = new Vector2(x, transform.position.y);
	}
}
