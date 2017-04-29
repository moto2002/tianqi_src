using GameData;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MonthSignInChildItem : MonoBehaviour
{
	public enum MonthSignInChildItemState
	{
		None,
		HaveSign,
		CanSign,
		CanResign
	}

	public ButtonCustom BtnRewardDetail;

	private Image ImageFrame;

	private Image ImageIcon;

	private Text TextNum;

	private Transform Vip;

	private Text VipText;

	private Transform ItemSelect;

	public ButtonCustom BtnReSignIn;

	public int dayindex;

	private Transform Fx;

	private Transform ResignIn;

	private GameObject CanSignObj;

	public MonthSign monthSignCache;

	public MonthSignInChildItem.MonthSignInChildItemState state;

	private Text TextDay;

	private int m_spine_id_01;

	private int m_spine_id_02;

	private void Awake()
	{
		this.Vip = base.get_transform().FindChild("Vip");
		this.Vip.GetComponent<DepthOfUINoCollider>().SortingOrder = 2001;
		this.VipText = this.Vip.FindChild("VipText").GetComponent<Text>();
		ResourceManager.SetTextToStencil(ref this.VipText);
		Image component = this.Vip.FindChild("ImageBG2").GetComponent<Image>();
		ResourceManager.SetImageToStencil(ref component, 0);
		this.BtnRewardDetail = base.get_transform().FindChild("BtnRewardDetail").GetComponent<ButtonCustom>();
		this.ImageFrame = this.BtnRewardDetail.get_transform().FindChild("ImageFrame").GetComponent<Image>();
		this.ImageIcon = this.BtnRewardDetail.get_transform().FindChild("ImageIcon").GetComponent<Image>();
		this.TextNum = this.BtnRewardDetail.get_transform().FindChild("TextNum").GetComponent<Text>();
		this.ItemSelect = base.get_transform().FindChild("ItemSelect");
		this.ResignIn = base.get_transform().FindChild("ResignIn");
		this.BtnReSignIn = this.ResignIn.FindChild("BtnReSignIn").GetComponent<ButtonCustom>();
		this.Fx = base.get_transform().FindChild("Fx");
		this.TextDay = this.BtnRewardDetail.get_transform().FindChild("TextItemName").GetComponent<Text>();
		this.CanSignObj = base.get_transform().FindChild("BtnRewardDetail").FindChild("ImageFrameCanSign").get_gameObject();
	}

	public void SetUI(MonthSign monthSign, bool haveSign, bool canSign, bool canResign, int day)
	{
		this.monthSignCache = monthSign;
		this.dayindex = day + 1;
		this.TextDay.set_text(string.Format("第{0}天", this.dayindex.ToString()));
		Items items = DataReader<Items>.Get(monthSign.itemId);
		ResourceManager.SetSprite(this.ImageIcon, GameDataUtils.GetIcon(items.icon));
		this.ImageIcon.SetNativeSize();
		ResourceManager.SetSprite(this.ImageFrame, GameDataUtils.GetItemFrame(items.id));
		if (monthSign.itemNum <= 1)
		{
			this.TextNum.get_gameObject().SetActive(false);
		}
		else
		{
			this.TextNum.get_gameObject().SetActive(true);
			this.TextNum.set_text(monthSign.itemNum.ToString());
		}
		if (monthSign.doubleMinVip != 0)
		{
			this.Vip.get_gameObject().SetActive(true);
			string text = "V" + monthSign.doubleMinVip + GameDataUtils.GetChineseContent(502207, false);
			this.VipText.set_text(text);
		}
		else
		{
			this.Vip.get_gameObject().SetActive(false);
		}
		this.state = MonthSignInChildItem.MonthSignInChildItemState.None;
		this.BtnRewardDetail.set_enabled(true);
		this.ResignIn.get_gameObject().SetActive(false);
		this.ItemSelect.get_gameObject().SetActive(false);
		this.CanSignObj.SetActive(false);
		if (haveSign)
		{
			this.ItemSelect.get_gameObject().SetActive(true);
			this.state = MonthSignInChildItem.MonthSignInChildItemState.HaveSign;
		}
		else if (canSign)
		{
			this.state = MonthSignInChildItem.MonthSignInChildItemState.CanSign;
		}
		else if (canResign)
		{
			this.ResignIn.get_gameObject().SetActive(true);
			this.state = MonthSignInChildItem.MonthSignInChildItemState.CanResign;
		}
		if (canSign)
		{
			this.ShowFx(true);
			this.CanSignObj.SetActive(true);
		}
		else
		{
			this.ShowFx(false);
		}
	}

	private void ShowFx(bool isShow)
	{
		this.Fx.get_gameObject().SetActive(isShow);
		if (isShow)
		{
			this.PlaySpine();
		}
	}

	private void PlaySpine()
	{
		FXSpineManager.Instance.DeleteSpine(this.m_spine_id_01, true);
		FXSpineManager.Instance.DeleteSpine(this.m_spine_id_02, true);
		this.m_spine_id_01 = FXSpineManager.Instance.PlaySpine(601, this.Fx, "SignInUI", 3001, null, "UI", 0f, 0f, 1.25f, 1.25f, true, FXMaskLayer.MaskState.None);
		this.m_spine_id_02 = FXSpineManager.Instance.PlaySpine(604, this.Fx, "SignInUI", 3001, null, "UI", 0f, -8f, 1.25f, 1.25f, true, FXMaskLayer.MaskState.None);
	}
}
