using LitJson;
using System;
using System.IO;
using UnityEngine;

public class EditorConfig : Singleton<EditorConfig>
{
	public class EditorConfigData
	{
		public bool IsLoadRecordEnable;

		public string AssetBundlePath = string.Empty;
	}

	private string ConfigFile;

	public EditorConfig.EditorConfigData Data
	{
		get;
		private set;
	}

	public void Init()
	{
		if (!Application.get_isEditor())
		{
			return;
		}
		if (this.Data != null)
		{
			return;
		}
		this.ConfigFile = Path.Combine(Environment.get_CurrentDirectory(), "Config/EditorConfig.json");
		if (File.Exists(this.ConfigFile))
		{
			this.Data = JsonMapper.ToObject<EditorConfig.EditorConfigData>(File.ReadAllText(this.ConfigFile));
		}
		else
		{
			this.Data = new EditorConfig.EditorConfigData();
		}
	}

	public void Save()
	{
		if (this.Data == null)
		{
			return;
		}
		if (!File.Exists(this.ConfigFile))
		{
			Debug.LogError("文件不存在 :" + this.ConfigFile);
			return;
		}
		File.WriteAllText(this.ConfigFile, JsonMapper.ToJson(this.Data));
	}
}
