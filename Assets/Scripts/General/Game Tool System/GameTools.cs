using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GameTools
{
	public static Color HexToColor(string hex)
	{
		byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r,g,b, 255);
	}

	public static List<T> EnumToList<T>()
	{
		Type enumType = typeof (T);
		
		if (enumType.BaseType != typeof(Enum))
		{
			throw new ArgumentException("T must be of type System.Enum");
		}
		
		Array enumValArray = Enum.GetValues(enumType);
		List<T> enumValList = new List<T>(enumValArray.Length);
		
		foreach (int val in enumValArray) 
		{
			enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
		}
		
		return enumValList;
	}
}
