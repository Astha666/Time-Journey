using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

public class SaveGame
{
	private string playerName;
	private string saveGameName;
	private string saveGameFileName;
	private string saveGameDirectory;

	private string[] itemInventory;
	private string[] equipInventory;

	public SaveGame(string saveGameName, string saveGameFileName, string saveGameDirectory)
	{
		this.saveGameName = saveGameName;
		this.saveGameFileName = saveGameFileName;
		this.saveGameDirectory = saveGameDirectory;
	}

	public void Load()
	{

	}

	public void Save()
	{
		
	}
	
	public string PlayerName{get{return playerName;}}
	public string SaveGameName{get{return saveGameName;}}
	public string SaveGameFileName{get{return saveGameFileName;}}
	public string SaveGameDirectory{get{return saveGameDirectory;}}
}
