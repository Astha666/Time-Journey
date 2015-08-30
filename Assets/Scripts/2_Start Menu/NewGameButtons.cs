using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

public class NewGameButtons : MonoBehaviour 
{
	public string gameIntroScene;
	public string startGameScene;
	public InputField input;

	private string directory;

	public void StartGame() 
	{
		directory = Application.dataPath + "/Resources/SaveGames/";

		if(input.text != "" && input.text != "Enter Name here...")
		{
			if(!Directory.Exists(directory + input.text))
			{
				CreateSaveGame(input.text);
				Application.LoadLevel(gameIntroScene);
			}
			else
			{
				Debug.Log ("Player exist already");
			}
		}
		else
		{
			Debug.Log("Enter a name");
		}

	}

	public void Return()
	{
		Application.LoadLevel(startGameScene);
	}

	void CreateSaveGame(string playerName)
	{
		Directory.CreateDirectory(directory + playerName);

		string saveGameName = playerName;
		string saveGameFileName = directory + playerName + ".xml";
		string saveGameDirectory = directory + playerName + "/";

		Debug.Log ("NAM: " + saveGameName);
		Debug.Log ("FIL: " + saveGameFileName);
		Debug.Log ("DIR: " + saveGameDirectory);

		GameManager.actualSaveGame = new SaveGame(saveGameName, saveGameFileName, saveGameDirectory);
	}
}
