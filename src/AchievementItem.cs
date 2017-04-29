using GameData;
using Package;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementItem : MonoBehaviour
{
	public Text Name;

	public Text NameDes;

	public Text ProgressText;

	public Transform RewardItems;

	public ButtonCustom RewardBtn;

	public ButtonCustom GotoBtn;

	public GameObject itemPrefab;

	public Image Icon;

	public Image FinishImg;

	private int state;

	private int id;

	private Achievement data;

	private void Start()
	{
		ButtonCustom expr_06 = this.RewardBtn;
		expr_06.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_06.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickGetReward));
		ButtonCustom expr_2D = this.GotoBtn;
		expr_2D.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Combine(expr_2D.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickGoTo));
	}

	private void OnDestory()
	{
		ButtonCustom expr_06 = this.RewardBtn;
		expr_06.onClickCustom = (ButtonCustom.VoidDelegateObj)Delegate.Remove(expr_06.onClickCustom, new ButtonCustom.VoidDelegateObj(this.OnClickGetReward));
	}

	private void OnClickGetReward(GameObject go)
	{
		base.get_transform().get_parent().GetComponent<ListView2>().RemoveRow(int.Parse(base.get_name()));
		NetworkManager.Send(new acceptAchievementAwardReq
		{
			achievementId = this.id
		}, ServerType.Data);
	}

	private void OnClickGoTo(GameObject go)
	{
		SourceReferenceUI.GoTo(this.data.go);
	}

	public void UpdateItem(int id)
	{
		this.RewardItems.get_gameObject().SetActive(true);
		this.id = id;
		this.data = DataReader<Achievement>.Get(id);
		AchievementItemInfo achievementItemInfo = AchievementManager.Instance.AllIdList.get_Item(id);
		this.state = achievementItemInfo.isAccept;
		if (this.data.icon > 0)
		{
			ResourceManager.SetSprite(this.Icon, GameDataUtils.GetIcon(this.data.icon));
		}
		this.Name.set_text(GameDataUtils.GetChineseContent(this.data.name, false));
		string text = GameDataUtils.GetChineseContent(this.data.introduction, false);
		int linkSystem = this.data.linkSystem;
		switch (linkSystem)
		{
		case 5:
			goto IL_E8;
		case 6:
		case 7:
			IL_C5:
			switch (linkSystem)
			{
			case 15:
			case 19:
				goto IL_E8;
			case 17:
				text = "配置表被删，找策划哥！（通关主线副本第X章第X节XXX）";
				goto IL_158;
			}
			text = string.Format(text, this.data.size.get_Item(0));
			goto IL_158;
		case 8:
		case 9:
			text = "配置表被删，找策划哥！（通关精英副本第X章第X节XXX）";
			goto IL_158;
		}
		goto IL_C5;
		IL_E8:
		text = string.Format(text, this.data.size.get_Item(0), this.data.size.get_Item(1));
		IL_158:
		this.NameDes.set_text(text);
		int num = achievementItemInfo.completeProgress.get_Item(0);
		if (this.data.schedule == -1)
		{
			this.ProgressText.get_gameObject().SetActive(false);
		}
		else
		{
			this.ProgressText.get_gameObject().SetActive(true);
			this.ProgressText.set_text(string.Concat(new object[]
			{
				string.Empty,
				num,
				"/",
				this.data.schedule
			}));
		}
		IEnumerator enumerator = this.RewardItems.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.get_Current();
				Object.Destroy(transform.get_gameObject());
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
		if (this.data.dropId > 0)
		{
			List<DiaoLuoZu> itemList = DropUtil.GetItemList(this.data.dropId);
			for (int i = 0; i < itemList.get_Count(); i++)
			{
				DiaoLuoZu diaoLuoZu = itemList.get_Item(i);
				GameObject gameObject = Object.Instantiate<GameObject>(this.itemPrefab);
				ResourceManager.SetInstantiateUIRef(gameObject, null);
				gameObject.SetActive(true);
				gameObject.get_transform().SetParent(this.RewardItems);
				gameObject.get_transform().set_localScale(new Vector3(1f, 1f, 1f));
				gameObject.get_transform().set_localPosition(new Vector3(0f, 0f, 0f));
				gameObject.get_transform().set_localEulerAngles(new Vector3(0f, 0f, 0f));
				ResourceManager.SetSprite(gameObject.get_transform().FindChild("Frame").GetComponent<Image>(), GameDataUtils.GetItemFrame(diaoLuoZu.itemId));
				ResourceManager.SetSprite(gameObject.get_transform().FindChild("Icon").GetComponent<Image>(), GameDataUtils.GetIcon(DataReader<Items>.Get(diaoLuoZu.itemId).littleIcon));
				gameObject.get_transform().FindChild("Num").GetComponent<Text>().set_text("x" + diaoLuoZu.maxNum.ToString());
			}
		}
		this.FinishImg.get_gameObject().SetActive(false);
		this.RewardBtn.get_gameObject().SetActive(false);
		this.GotoBtn.get_gameObject().SetActive(false);
		if (this.data.go != -1 && this.state == 0)
		{
			this.GotoBtn.get_gameObject().SetActive(true);
		}
		else if (this.state == 1)
		{
			this.RewardBtn.get_gameObject().SetActive(true);
		}
		else if (this.state == 2)
		{
			this.FinishImg.get_gameObject().SetActive(true);
			this.RewardItems.get_gameObject().SetActive(false);
		}
	}
}
