using System;
using UnityEngine;
using UnityEngine.UI;

public class GrowUpPlanItem : MonoBehaviour
{
	private Image imgIcon;

	public Text textCondition;

	public Text textCount;

	private RankDataUnite data;

	private Transform btnGetPanel;

	private ButtonCustom btnGet;

	private Transform icon;

	private Text textBtnGet;

	private Image imgGrey;

	private Image imgHaveGet;

	private int typeId;

	private int roleLv;

	private void Awake()
	{
		this.icon = base.get_transform().FindChild("ItemGoods");
		this.imgIcon = this.icon.FindChild("GoodsIcon").GetComponent<Image>();
		this.textCondition = base.get_transform().FindChild("Condition").GetComponent<Text>();
		this.textCount = base.get_transform().FindChild("Count").GetComponent<Text>();
		this.btnGetPanel = base.get_transform().FindChild("BtnGet");
		this.btnGet = this.btnGetPanel.GetComponent<ButtonCustom>();
		this.textBtnGet = this.btnGetPanel.FindChild("BtnText").GetComponent<Text>();
		this.imgGrey = this.btnGetPanel.FindChild("ImageGrey").GetComponent<Image>();
		this.imgHaveGet = base.get_transform().FindChild("ImageHaveGet").GetComponent<Image>();
	}

	private void Start()
	{
	}

	private void OnClickGet(GameObject go)
	{
		GrowUpPlanManager.Instance.SendGetRewardReq(this.typeId, this.roleLv);
	}

	public void UpdateItem(GrowUpPlanDataUnite itemData)
	{
		this.typeId = itemData.typeId;
		this.roleLv = itemData.condition;
		ResourceManager.SetSprite(this.imgIcon, GameDataUtils.GetItemIcon(itemData.itemId));
		string text = "x" + itemData.count;
		this.textCount.set_text(text);
		string text2 = string.Format(GameDataUtils.GetChineseContent(513175, false), itemData.condition);
		this.textCondition.set_text(text2);
		int state = itemData.state;
		if (state == 1)
		{
			this.btnGet.get_gameObject().SetActive(true);
			this.btnGet.set_enabled(false);
			this.imgGrey.get_gameObject().SetActive(true);
			this.imgHaveGet.get_gameObject().SetActive(false);
			this.textBtnGet.set_text(GameDataUtils.GetChineseContent(513176, false));
		}
		else if (state == 2)
		{
			this.btnGet.get_gameObject().SetActive(true);
			this.btnGet.set_enabled(true);
			this.imgGrey.get_gameObject().SetActive(false);
			this.imgHaveGet.get_gameObject().SetActive(false);
			this.textBtnGet.set_text(GameDataUtils.GetChineseContent(513177, false));
		}
		else
		{
			this.btnGet.get_gameObject().SetActive(false);
			this.imgHaveGet.get_gameObject().SetActive(true);
		}
		base.get_transform().FindChild("BtnGet").GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickGet);
	}

	private void SetButtonState(int showState)
	{
	}
}
