using LuaInterface;
using System;
using UnityEngine;

namespace LuaFramework
{
	public class PanelManager : Manager
	{
		private Transform parent;

		private Transform Parent
		{
			get
			{
				if (this.parent == null)
				{
					GameObject gameObject = GameObject.Find("UGUI Root/UICanvas");
					if (gameObject != null)
					{
						this.parent = gameObject.get_transform();
					}
				}
				return this.parent;
			}
		}

		public void CreatePanel(string name, LuaFunction func = null)
		{
		}
	}
}
