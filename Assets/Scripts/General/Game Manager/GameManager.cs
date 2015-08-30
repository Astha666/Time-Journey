using UnityEngine;
using UnityEngine.UI;
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

	// BUTTON COLORS
	public static ColorBlock colorBlock;

	void Awake()
	{
		colorBlock.normalColor = GameTools.HexToColor("FFFFFF");
		colorBlock.highlightedColor = GameTools.HexToColor("DAFF74");
		colorBlock.pressedColor = GameTools.HexToColor("9CFF00");
		colorBlock.disabledColor = GameTools.HexToColor("C8C8C8");
		colorBlock.fadeDuration = 0.1f;
		colorBlock.colorMultiplier = 1.0f;

		gameFont = (Font)Resources.Load("Fonts/FFFFORWA");

		if (instance != null && instance != this) 
		{
			Destroy(this.gameObject);
		}
		
		instance = this;
		DontDestroyOnLoad( this.gameObject );
	}

	public void LoadDefaultSaveGame()
	{
		// actualSaveGame = SetADefaultSaveGame;
	}

	public static GameManager Instance{get{return instance;}}
}
