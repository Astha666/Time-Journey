using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class LevelWorldButtons : MonoBehaviour 
{
	public Sprite image;

	private Canvas canvas;
	private List<WorldType> allWorlds;

	void Awake() 
	{
		canvas = GetComponentInChildren<Canvas>();
		allWorlds = GameTools.EnumToList<WorldType>();

		GridLayoutGroup canvasLayout = canvas.gameObject.AddComponent<GridLayoutGroup>();
		canvasLayout.cellSize = new Vector2(500, 200);
		canvasLayout.childAlignment = TextAnchor.MiddleCenter;
		canvasLayout.spacing = new Vector2(1,0);
	}

	void Start()
	{
		foreach(WorldType world in allWorlds)
		{
			int ID = allWorlds.IndexOf(world);
			CreateWorldSelectButtons(world, ID);
		}
	}

	void CreateWorldSelectButtons(WorldType worldType, int ID)
	{
		string worldName = worldType.ToString().Replace("_", " ");

		GameObject worldButtonObject = new GameObject("Button " + worldName);
		Button worldButton = worldButtonObject.AddComponent<Button>();
		Image worldButtonImage = worldButtonObject.AddComponent<Image>();
		
		GameObject worldButtonTextObject = new GameObject("Text " + worldName);
		Text worldButtonText = worldButtonTextObject.AddComponent<Text>();
		Outline worldButtonTextOutline = worldButtonTextObject.AddComponent<Outline>();
		
		worldButtonImage.type = Image.Type.Sliced;
		worldButtonImage.sprite = image;
		
		worldButtonObject.transform.SetParent(canvas.transform);
		worldButtonTextObject.transform.SetParent(worldButtonObject.transform);
		worldButtonTextObject.GetComponent<RectTransform>().localPosition = new Vector2(0.0f, -6.5f);

		worldButtonObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.0f, 1.0f);
		worldButtonObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.0f, 1.0f);
		
		worldButtonText.text = worldName.ToUpper();
		worldButtonText.alignment = TextAnchor.MiddleCenter;
		worldButtonText.font = GameManager.gameFont;
		worldButtonText.fontSize = 38;
		worldButtonText.color = Color.white;
		
		worldButtonTextOutline.effectColor = Color.black;
		worldButtonTextOutline.effectDistance = new Vector2(3.0f, 3.0f);
		
		worldButtonObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		worldButtonTextObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		
		worldButtonObject.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 150);
		worldButtonTextObject.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 150);
		worldButtonTextObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
		
		worldButtonObject.layer = LayerMask.NameToLayer("UI");
		worldButtonTextObject.layer = LayerMask.NameToLayer("UI");

		worldButton.targetGraphic = worldButton.GetComponent<Image>();
		worldButton.colors = GameManager.colorBlock;

		World thisWorld = new World(ID + 1 + "_" + worldName + " Level Selector", worldType, 10);
		GameManager.allWorlds.Add(thisWorld);

		worldButton.onClick.AddListener(delegate {GoToWorld(thisWorld);});
	}

	void GoToWorld(World world)
	{
		Application.LoadLevel(world.LevelSelectorScene);
	}
}
