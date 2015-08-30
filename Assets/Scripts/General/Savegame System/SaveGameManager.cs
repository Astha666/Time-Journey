using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

public class SaveGameManager : MonoBehaviour 
{
	public List<SaveGame> saveGames;
	public string levelSelectorScene;
	public string directory;

	private Canvas canvas;
	private Sprite image;
	private int buttonSpace;

	void Awake() 
	{
		directory = "/Resources/SaveGames";
		saveGames = new List<SaveGame>();
		canvas = GetComponentInChildren<Canvas>();
		image = canvas.GetComponentInChildren<Image>().sprite;
		buttonSpace = 8;

		if(!Directory.Exists(Application.dataPath + directory))
		{
			Directory.CreateDirectory(Application.dataPath + directory);
		}

		DirectoryInfo dir = new DirectoryInfo(Application.dataPath + directory);
		FileInfo[] info = dir.GetFiles("*.xml");
		
		foreach(FileInfo fileInfo in info)
		{
			SaveGame saveGame = new SaveGame(fileInfo.Name.Replace(".xml", ""), fileInfo.FullName, fileInfo.DirectoryName + "/" + fileInfo.Name.Replace(".xml", ""));
			saveGames.Add(saveGame);
			int ID = System.Array.IndexOf(info, fileInfo);
			CreateSaveGameButtons(saveGame, ID);
		}
	}

	void CreateSaveGameButtons(SaveGame saveGame, int ID)
	{
		GameObject loadSaveGameButtonObject = new GameObject("SaveGame " + saveGame.SaveGameName);
		Button loadButton = loadSaveGameButtonObject.AddComponent<Button>();
		Image loadButtonImage = loadSaveGameButtonObject.AddComponent<Image>();
		
		GameObject loadSaveGameTextObject = new GameObject("SaveGameText " + saveGame.SaveGameName);
		Text loadButtonText = loadSaveGameTextObject.AddComponent<Text>();
		Outline loadButtonTextOutline = loadSaveGameTextObject.AddComponent<Outline>();

		loadButtonImage.type = Image.Type.Sliced;
		loadButtonImage.sprite = image;

		loadSaveGameButtonObject.transform.SetParent(canvas.transform);
		loadSaveGameTextObject.transform.SetParent(loadSaveGameButtonObject.transform);
		loadSaveGameTextObject.GetComponent<RectTransform>().localPosition = new Vector2(0.0f, -6.5f);

		loadSaveGameButtonObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(450, (ID * (150 + buttonSpace)) - 362);
		loadSaveGameButtonObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
		loadSaveGameButtonObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);

		loadButtonText.text = saveGame.SaveGameName.ToUpper();
		loadButtonText.alignment = TextAnchor.MiddleCenter;
		loadButtonText.font = GameManager.gameFont;
		loadButtonText.fontSize = 38;
		loadButtonText.color = Color.white;

		loadButtonTextOutline.effectColor = Color.black;
		loadButtonTextOutline.effectDistance = new Vector2(3.0f, 3.0f);

		loadSaveGameButtonObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		loadSaveGameTextObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

		loadSaveGameButtonObject.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 150);
		loadSaveGameTextObject.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 150);
		loadSaveGameTextObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

		loadSaveGameButtonObject.layer = LayerMask.NameToLayer("UI");
		loadSaveGameTextObject.layer = LayerMask.NameToLayer("UI");

		loadButton.targetGraphic = loadButton.GetComponent<Image>();
		loadButton.colors = GameManager.colorBlock;
		loadButton.onClick.AddListener(delegate {LoadSaveGame(saveGame);});

		// **********************************************************************************************

		GameObject deleteSaveGameButtonObject = new GameObject("Delete " + saveGame.SaveGameName);
		Button deleteButton = deleteSaveGameButtonObject.AddComponent<Button>();
		Image deleteButtonImage = deleteSaveGameButtonObject.AddComponent<Image>();

		GameObject deleteSaveGameTextObject = new GameObject("DeleteText " + saveGame.SaveGameName);
		Text deleteButtonText = deleteSaveGameTextObject.AddComponent<Text>();
		Outline deleteButtonTextOutline = deleteSaveGameTextObject.AddComponent<Outline>();
		
		deleteButtonImage.type = Image.Type.Sliced;
		deleteButtonImage.sprite = image;

		deleteSaveGameButtonObject.transform.SetParent(canvas.transform);
		deleteSaveGameTextObject.transform.SetParent(deleteSaveGameButtonObject.transform);
		deleteSaveGameTextObject.GetComponent<RectTransform>().localPosition = new Vector2(0.0f, -6.5f);

		deleteSaveGameButtonObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(800, (ID * (150 + buttonSpace)) - 362);
		deleteSaveGameButtonObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
		deleteSaveGameButtonObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);

		deleteButtonText.text = "X";
		deleteButtonText.alignment = TextAnchor.MiddleCenter;
		deleteButtonText.font = GameManager.gameFont;
		deleteButtonText.fontSize = 38;
		deleteButtonText.color = Color.white;
		
		deleteButtonTextOutline.effectColor = Color.black;
		deleteButtonTextOutline.effectDistance = new Vector2(3.0f, 3.0f);

		deleteSaveGameButtonObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		deleteSaveGameTextObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		
		deleteSaveGameButtonObject.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150);
		deleteSaveGameTextObject.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150);
		deleteSaveGameTextObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
		
		deleteSaveGameButtonObject.layer = LayerMask.NameToLayer("UI");
		deleteSaveGameTextObject.layer = LayerMask.NameToLayer("UI");

		deleteSaveGameButtonObject.transform.SetParent(loadSaveGameButtonObject.transform);

		deleteButton.targetGraphic = deleteButton.GetComponent<Image>();
		deleteButton.colors = GameManager.colorBlock;
		deleteButton.onClick.AddListener(delegate {DeleteSaveGame(saveGame, loadSaveGameButtonObject);});
	}

	void LoadSaveGame(SaveGame saveGame)
	{
		GameManager.actualSaveGame = saveGame;
		Application.LoadLevel(levelSelectorScene);
	}

	void DeleteSaveGame(SaveGame saveGame, GameObject button)
	{
		File.Delete(saveGame.SaveGameFileName);
		Directory.Delete(saveGame.SaveGameDirectory, true);
		button.SetActive(false);
	}
}
