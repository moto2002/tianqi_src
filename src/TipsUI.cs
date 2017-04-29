using System;
using UnityEngine;
using UnityEngine.UI;

public class TipsUI : UIBase
{
	private const float HalfOfWidth = 150f;

	private const float HalfOfHeigh = 75f;

	private static TipsUI m_instance;

	private Text Top;

	private Text Body;

	public static TipsUI Instance
	{
		get
		{
			if (TipsUI.m_instance == null)
			{
				TipsUI.m_instance = (UIManagerControl.Instance.OpenUI("TipsUI", UINodesManager.MiddleUIRoot, false, UIType.NonPush) as TipsUI);
			}
			return TipsUI.m_instance;
		}
	}

	private void Awake()
	{
		this.Top = base.get_transform().FindChild("Top").GetComponent<Text>();
		this.Body = base.get_transform().FindChild("Body").GetComponent<Text>();
		EventDispatcher.AddListener("UIManagerControl.HideTipsUI", new Callback(this.HideTipsUI));
	}

	private void HideTipsUI()
	{
		this.Show(false);
	}

	public void ShowAttribute(int id, Vector3 pos)
	{
		this.ShowTip("属性加成", CharacterManager.Instance.GetPointAttr(id), pos);
	}

	public void ShowTip(string title, string content, Vector3 pos)
	{
		this.Show(true);
		base.get_transform().set_position(pos);
		this.CheckBound();
		this.Top.set_text(title);
		this.Body.set_text(content);
	}

	private void CheckBound()
	{
		float x = (base.get_transform() as RectTransform).get_anchoredPosition().x;
		float num = (base.get_transform() as RectTransform).get_anchoredPosition().y + 75f + 35f;
		UIUtils.GetBound(ref x, ref num, 300f, 150f, new Vector2(0.5f, 0.5f));
		(base.get_transform() as RectTransform).set_anchoredPosition(new Vector2(x, num));
	}
}
