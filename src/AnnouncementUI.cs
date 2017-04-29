using Foundation.Core.Databinding;
using LitJson;
using LuaFramework;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AnnouncementUI : UIBase
{
	public static AnnouncementUI Instance;

	private ButtonCustom BtnConfirm;

	private Text content;

	private Text title;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.55f;
		this.isClick = false;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		AnnouncementUI.Instance = this;
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.BtnConfirm = base.FindTransform("Confirm").GetComponent<ButtonCustom>();
		this.content = base.FindTransform("ContentText").GetComponent<Text>();
		this.title = base.FindTransform("TitleText").GetComponent<Text>();
		this.BtnConfirm.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnConfirm);
	}

	protected override void OnEnable()
	{
		this.RefleshContent(true);
	}

	protected override void OnDisable()
	{
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			AnnouncementUI.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener<bool>(EventNames.Download_AnnouncementFile_Finish, new Callback<bool>(this.RefleshContent));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener<bool>(EventNames.Download_AnnouncementFile_Finish, new Callback<bool>(this.RefleshContent));
	}

	protected void OnClickBtnConfirm(GameObject sender)
	{
		this.Show(false);
	}

	public void RefleshContent(bool status)
	{
		if (status)
		{
			this.RefreshJson();
		}
		else
		{
			this.Retry();
		}
	}

	private void RefreshTxt()
	{
		string text = Path.Combine(Util.DataPath, "Announcement.txt");
		if (File.Exists(text))
		{
			StreamReader streamReader = File.OpenText(text);
			string text2 = streamReader.ReadToEnd();
			streamReader.Close();
			string text3;
			string text4;
			if (string.IsNullOrEmpty(text2))
			{
				text3 = string.Empty;
				text4 = string.Empty;
			}
			else
			{
				text3 = text2.Trim(new char[]
				{
					'{',
					'}'
				}).Split(new char[]
				{
					",".get_Chars(0)
				})[0].Split(new char[]
				{
					":".get_Chars(0)
				})[1].Trim(new char[]
				{
					'"',
					'"'
				});
				text4 = text2.Trim(new char[]
				{
					'{',
					'}'
				}).Split(new char[]
				{
					",".get_Chars(0)
				})[1].Split(new char[]
				{
					":".get_Chars(0)
				})[1].Trim(new char[]
				{
					'"',
					'"'
				});
				text4 = text4.Replace("\\r\\n", Environment.get_NewLine());
			}
			this.title.set_text(text3);
			this.content.set_text(text4);
			this.content.get_rectTransform().set_sizeDelta(new Vector2(this.content.get_rectTransform().get_sizeDelta().x, this.content.get_preferredHeight()));
			this.content.get_rectTransform().set_localPosition(new Vector3(0f, 120f - this.content.get_preferredHeight() / 2f));
		}
	}

	private void RefreshJson()
	{
		try
		{
			string text = Path.Combine(Util.DataPath, "notice.json");
			if (File.Exists(text))
			{
				StreamReader streamReader = File.OpenText(text);
				string text2 = streamReader.ReadToEnd();
				streamReader.Close();
				string text3 = string.Empty;
				string text4 = string.Empty;
				if (!string.IsNullOrEmpty(text2))
				{
					JsonData jsonData = JsonMapper.ToObject(text2);
					if (jsonData.Count > 0)
					{
						JsonData jsonData2 = jsonData[0];
						if (jsonData2.Inst_Object != null)
						{
							if (jsonData2.Inst_Object.ContainsKey("title"))
							{
								text3 = (string)jsonData2["title"];
							}
							if (jsonData2.Inst_Object.ContainsKey("content"))
							{
								text4 = (string)jsonData2["content"];
							}
						}
					}
				}
				this.title.set_text(text3);
				this.content.set_text(text4);
				this.content.get_rectTransform().set_sizeDelta(new Vector2(this.content.get_rectTransform().get_sizeDelta().x, this.content.get_preferredHeight()));
				this.content.get_rectTransform().set_localPosition(new Vector3(0f, 120f - this.content.get_preferredHeight() / 2f));
			}
		}
		catch
		{
			this.Show(false);
			Debug.LogError("公告文件格式有误!!!");
		}
	}

	private void Retry()
	{
		this.title.set_text(string.Empty);
		this.content.set_text(string.Empty);
		DialogBoxUIViewModel.Instance.ShowAsOKCancel(GameDataUtils.GetChineseContent(621264, false), GameDataUtils.GetChineseContent(621309, false), delegate
		{
			ClientApp.Instance.ReInit();
		}, delegate
		{
			DownloadAnnouncementFile.Instance.Down();
		}, "取 消", "确 定", "button_orange_1", "button_yellow_1", null, true, true);
	}
}
