using UnityEngine;
using System.Collections;

public class StartGameButtons : MonoBehaviour 
{
	public string mainMenuScene;
	public string newGameScene;
	public string levelSelectorScene;
	
	public void NewGame()
	{
		Application.LoadLevel(newGameScene);
	}
	
	public void LoadGame()
	{
		gameObject.AddComponent<SaveGameManager>().levelSelectorScene = levelSelectorScene;
	}
	
	public void Return()
	{
		Application.LoadLevel(mainMenuScene);
	}
}
