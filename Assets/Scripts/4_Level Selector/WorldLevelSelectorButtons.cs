using UnityEngine;
using System.Collections;

public class WorldLevelSelectorButtons : MonoBehaviour 
{
	public string worldSelectorScene;

	public void Return()
	{
		Application.LoadLevel(worldSelectorScene);
	}
}
