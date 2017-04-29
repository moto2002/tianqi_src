using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsManager : BaseSubSystemManager
{
	private Dictionary<string, bool> ModuleFlagMap = new Dictionary<string, bool>();

	private Dictionary<string, string[]> IconHasModuleMap = new Dictionary<string, string[]>();

	private Dictionary<string, string> ModuleIconMap = new Dictionary<string, string>();

	private static TipsManager instance;

	public static TipsManager Instance
	{
		get
		{
			if (TipsManager.instance == null)
			{
				TipsManager.instance = new TipsManager();
			}
			return TipsManager.instance;
		}
	}

	private TipsManager()
	{
	}

	public override void Init()
	{
		base.Init();
		this.AddMultiModuleControlIcon(TipsEvents.ButtonTipsTownUiTask, new string[]
		{
			TipsEvents.ButtonTipsModuleDailyTask
		});
	}

	public override void Release()
	{
	}

	protected override void AddListener()
	{
		EventDispatcher.AddListener<UIBase>(EventNames.RefreshTipsButtonStateInUIBase, new Callback<UIBase>(this.OnRefreshTipsState));
		EventDispatcher.AddListener<string>(EventNames.RefreshTipsButtonStateByName, new Callback<string>(this.OnRefreshTipsState));
		EventDispatcher.AddListener<string, bool>(EventNames.OnTipsStateChange, new Callback<string, bool>(this.OnTipsStateChange));
	}

	public void OnRefreshTipsState(UIBase parentUI)
	{
		using (Dictionary<string, bool>.Enumerator enumerator = this.ModuleFlagMap.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, bool> current = enumerator.get_Current();
				Dictionary<string, bool> iconNameByModule = this.GetIconNameByModule(current.get_Key());
				using (Dictionary<string, bool>.Enumerator enumerator2 = iconNameByModule.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<string, bool> current2 = enumerator2.get_Current();
						Transform transform = parentUI.FindTransform(current2.get_Key());
						if (transform != null)
						{
							transform.get_gameObject().SetActive(current2.get_Value());
						}
					}
				}
			}
		}
	}

	private void OnRefreshTipsState(string module)
	{
		Dictionary<string, bool> iconNameByModule = this.GetIconNameByModule(module);
		using (Dictionary<string, bool>.Enumerator enumerator = iconNameByModule.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, bool> current = enumerator.get_Current();
				Transform transform = this.Find(UINodesManager.UIRoot, current.get_Key());
				if (transform != null)
				{
					transform.get_gameObject().SetActive(current.get_Value());
				}
			}
		}
	}

	private void OnTipsStateChange(string name, bool state)
	{
		this.ModuleFlagMap.set_Item(name, state);
		this.OnRefreshTipsState(name);
	}

	private void AddMultiModuleControlIcon(string iconName, string[] module)
	{
		this.IconHasModuleMap.set_Item(iconName, module);
		for (int i = 0; i < module.Length; i++)
		{
			string text = module[i];
			this.ModuleIconMap.set_Item(text, iconName);
		}
	}

	private Transform Find(Transform parent, string name)
	{
		Transform result = null;
		IEnumerator enumerator = parent.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.get_Current();
				if (name.Equals(transform.get_name()))
				{
					result = transform;
					break;
				}
				Transform transform2 = this.Find(transform, name);
				if (transform2 != null && name.Equals(transform2.get_name()))
				{
					result = transform2;
					break;
				}
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		return result;
	}

	private Dictionary<string, bool> GetIconNameByModule(string module)
	{
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		dictionary.Add(module, this.ModuleFlagMap.get_Item(module));
		if (this.ModuleIconMap.ContainsKey(module))
		{
			bool flag = false;
			string[] array = this.IconHasModuleMap.get_Item(this.ModuleIconMap.get_Item(module));
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (this.ModuleFlagMap.ContainsKey(text) && this.ModuleFlagMap.get_Item(text))
				{
					flag = true;
					break;
				}
			}
			dictionary.Add(this.ModuleIconMap.get_Item(module), flag);
		}
		return dictionary;
	}
}
