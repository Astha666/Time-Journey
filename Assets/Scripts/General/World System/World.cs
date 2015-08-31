using UnityEngine;
using System.Collections;

public class World
{
	private string levelSelectorScene;
	private WorldType worldType;
	private int levelAmount;

	public World(string levelSelectorScene, WorldType worldType, int levelAmount)
	{
		this.levelSelectorScene = levelSelectorScene;
		this.worldType = worldType;
		this.levelAmount = levelAmount;
	}

	public string LevelSelectorScene{get{return levelSelectorScene;}}
	public WorldType WorldType{get{return worldType;}}
	public int LevelAmount{get{return levelAmount;}}
}
