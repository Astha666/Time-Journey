using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	// SINGLETON
	private static GameManager instance = null;

	// SAVEGAME
	public static SaveGame actualSaveGame;

	// MAIN MENU
	public static bool FirstTimeOptionsMenu;

	// FONT
	public static Font gameFont;

	void Awake()
	{
		gameFont = (Font)Resources.Load("Fonts/FFFFORWA");

		if (instance != null && instance != this) 
		{
			Destroy(this.gameObject);
		}
		
		instance = this;
		DontDestroyOnLoad( this.gameObject );
	}

	public static GameManager Instance{get{return instance;}}
}
