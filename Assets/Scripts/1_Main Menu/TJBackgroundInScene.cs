using UnityEngine;
using System.Collections;

public class TJBackgroundInScene : MonoBehaviour 
{
	public float speed;

	private float y;
	private bool immediatelyMenu;

	void Awake()
	{
		y = transform.position.y;
	}

	void Update() 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			immediatelyMenu = true;
		}

		if(!immediatelyMenu && !GameManager.FirstTimeOptionsMenu)
		{
			y += Time.deltaTime * speed;

			if(y >= 8)
			{
				y = 8;
			}
			
			transform.position = new Vector2(transform.position.x, y);
		}
		else
		{
			transform.position = new Vector2(transform.position.x, 8);
		}
	}
}
