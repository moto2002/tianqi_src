using LuaInterface;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LuaFramework
{
	public class LuaBehaviour : View
	{
		private Dictionary<string, LuaFunction> buttons = new Dictionary<string, LuaFunction>();

		protected void Awake()
		{
			Util.CallMethod(base.get_name(), "Awake", new object[]
			{
				base.get_gameObject()
			});
		}

		protected void Start()
		{
			Util.CallMethod(base.get_name(), "Start", new object[0]);
		}

		protected void OnDestroy()
		{
			this.ClearClick();
			string text = base.get_name().ToLower().Replace("panel", string.Empty);
			Util.ClearMemory();
			Debug.Log("~" + base.get_name() + " was destroy!");
		}

		protected void OnClick()
		{
			Util.CallMethod(base.get_name(), "OnClick", new object[0]);
		}

		protected void OnClickEvent(GameObject go)
		{
			Util.CallMethod(base.get_name(), "OnClick", new object[]
			{
				go
			});
		}

		public void AddClick(GameObject go, LuaFunction luafunc)
		{
			if (go == null || luafunc == null)
			{
				return;
			}
			this.buttons.Add(go.get_name(), luafunc);
			go.GetComponent<Button>().get_onClick().AddListener(delegate
			{
				luafunc.Call(new object[]
				{
					go
				});
			});
		}

		public void RemoveClick(GameObject go)
		{
			if (go == null)
			{
				return;
			}
			LuaFunction luaFunction = null;
			if (this.buttons.TryGetValue(go.get_name(), ref luaFunction))
			{
				luaFunction.Dispose();
				luaFunction = null;
				this.buttons.Remove(go.get_name());
			}
		}

		public void ClearClick()
		{
			using (Dictionary<string, LuaFunction>.Enumerator enumerator = this.buttons.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, LuaFunction> current = enumerator.get_Current();
					if (current.get_Value() != null)
					{
						current.get_Value().Dispose();
					}
				}
			}
			this.buttons.Clear();
		}
	}
}
