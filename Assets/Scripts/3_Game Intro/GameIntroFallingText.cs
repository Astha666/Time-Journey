using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameIntroFallingText : MonoBehaviour 
{
	public TextAsset introTextFile;
	public string levelSelectorScene;
	public float fallSpeed;

	private Text introText;
	private RectTransform trans;
	private float y;

	void Start() 
	{
		introText = GetComponent<Text>();
		introText.text = introTextFile.text;
		trans = GetComponent<RectTransform>();
		y = trans.anchoredPosition.y;
	}

	void Update() 
	{
		y -= Time.deltaTime * fallSpeed;
		trans.anchoredPosition = new Vector2(trans.anchoredPosition.x, y);

		if(y >= 2460)
		{
			Application.LoadLevel(levelSelectorScene);
		}
	}
}
