using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_ItemData : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public int index;
		public string name;
		public int type;
		public int stat_01;
		public int stat_02;
		public int stat_03;
		public int stat_04;
	}
}

