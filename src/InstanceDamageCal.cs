using Foundation.Core.Databinding;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstanceDamageCal : UIBase, ListViewInterface
{
	public const float paddingRow = 2f;

	public const float paddingLevel1 = 135f;

	public const float paddingLevel2 = 100f;

	public const float paddingLevel3 = 85f;

	public const float minPercent = 0.03f;

	private ListView ListViewLeft;

	private ListView ListViewRight;

	private Text TextDamageNumLeft;

	private Text TextDamageNumRight;

	private ButtonCustom BtnTypeDamage;

	private ButtonCustom BtnTypeHit;

	private Text TextTime;

	private Text TextDamageLeft;

	private Text TextHealLeft;

	private Text TextDamageRight;

	private Text TextHealRight;

	public DamageHealCalModeEnum currentDamageCalMode;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		this.isMask = true;
		this.isClick = true;
		this.alpha = 0.75f;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.ListViewLeft = base.FindTransform("ListViewLeft").GetComponent<ListView>();
		this.ListViewRight = base.FindTransform("ListViewRight").GetComponent<ListView>();
		this.TextDamageNumLeft = base.FindTransform("TextDamageNumLeft").GetComponent<Text>();
		this.TextDamageNumRight = base.FindTransform("TextDamageNumRight").GetComponent<Text>();
		this.BtnTypeDamage = base.FindTransform("BtnTypeDamage").GetComponent<ButtonCustom>();
		this.BtnTypeHit = base.FindTransform("BtnTypeHit").GetComponent<ButtonCustom>();
		this.TextTime = base.FindTransform("TextTime").GetComponent<Text>();
		this.TextDamageLeft = base.FindTransform("TextDamageLeft").GetComponent<Text>();
		this.TextHealLeft = base.FindTransform("TextHealLeft").GetComponent<Text>();
		this.TextDamageRight = base.FindTransform("TextDamageRight").GetComponent<Text>();
		this.TextHealRight = base.FindTransform("TextHealRight").GetComponent<Text>();
		this.ListViewLeft.manager = this;
		this.ListViewLeft.Init(ListView.ListViewScrollStyle.Up);
		this.ListViewRight.manager = this;
		this.ListViewRight.Init(ListView.ListViewScrollStyle.Up);
	}

	private void Start()
	{
		this.BtnTypeDamage.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnTypeDamage);
		this.BtnTypeHit.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnTypeHit);
	}

	protected override void OnEnable()
	{
		base.GetComponent<RectTransform>().SetAsLastSibling();
	}

	private void OnClickBtnTypeDamage(GameObject sender)
	{
		this.currentDamageCalMode = DamageHealCalModeEnum.Active;
		this.ResetUI();
	}

	private void OnClickBtnTypeHit(GameObject sender)
	{
		this.currentDamageCalMode = DamageHealCalModeEnum.InActive;
		this.ResetUI();
	}

	private void OnClickBtnClose(GameObject sender)
	{
		this.Show(false);
	}

	private void OnClickLv1(GameObject sender)
	{
		InstanceDamageCalItem component = sender.get_transform().get_parent().get_parent().get_parent().GetComponent<InstanceDamageCalItem>();
		component.treeItem.open = !component.treeItem.open;
		component.listView.Refresh();
	}

	private void OnClickLv2(GameObject sender)
	{
		DamageCalItemLevel2 component = sender.get_transform().get_parent().get_parent().GetComponent<DamageCalItemLevel2>();
		component.treeItem.open = !component.treeItem.open;
		component.listView.Refresh();
	}

	public void ResetUI()
	{
		this.ListViewLeft.Refresh();
		this.ListViewRight.Refresh();
		this.TextTime.set_text(TimeConverter.ChangeSecsToString(InstanceManager.CurUsedTime));
		if (this.currentDamageCalMode == DamageHealCalModeEnum.Active)
		{
			this.TextDamageNumLeft.set_text(BattleDmgCollectManager.Instance.campSelfTotalActive.ToString());
			this.TextDamageNumRight.set_text(BattleDmgCollectManager.Instance.campEnemyTotalActive.ToString());
			this.BtnTypeDamage.get_transform().FindChild("Image1").get_gameObject().SetActive(true);
			this.BtnTypeDamage.get_transform().FindChild("Image2").get_gameObject().SetActive(false);
			this.BtnTypeHit.get_transform().FindChild("Image1").get_gameObject().SetActive(false);
			this.BtnTypeHit.get_transform().FindChild("Image2").get_gameObject().SetActive(true);
			this.TextDamageLeft.get_gameObject().SetActive(true);
			this.TextDamageRight.get_gameObject().SetActive(true);
			this.TextHealLeft.get_gameObject().SetActive(false);
			this.TextHealRight.get_gameObject().SetActive(false);
		}
		else
		{
			this.TextDamageNumLeft.set_text(BattleDmgCollectManager.Instance.campSelfTotalInActive.ToString());
			this.TextDamageNumRight.set_text(BattleDmgCollectManager.Instance.campEnemyTotalInActive.ToString());
			this.BtnTypeDamage.get_transform().FindChild("Image1").get_gameObject().SetActive(false);
			this.BtnTypeDamage.get_transform().FindChild("Image2").get_gameObject().SetActive(true);
			this.BtnTypeHit.get_transform().FindChild("Image1").get_gameObject().SetActive(true);
			this.BtnTypeHit.get_transform().FindChild("Image2").get_gameObject().SetActive(false);
			this.TextDamageLeft.get_gameObject().SetActive(false);
			this.TextDamageRight.get_gameObject().SetActive(false);
			this.TextHealLeft.get_gameObject().SetActive(true);
			this.TextHealRight.get_gameObject().SetActive(true);
		}
	}

	private float GetTotalNum(ListView listview, DamageHealCalModeEnum damageCalMode)
	{
		float num;
		float num2;
		if (damageCalMode == DamageHealCalModeEnum.Active)
		{
			num = (float)BattleDmgCollectManager.Instance.campSelfTotalActive;
			num2 = (float)BattleDmgCollectManager.Instance.campEnemyTotalActive;
		}
		else
		{
			num = (float)BattleDmgCollectManager.Instance.campSelfTotalInActive;
			num2 = (float)BattleDmgCollectManager.Instance.campEnemyTotalInActive;
		}
		return (num <= num2) ? num2 : num;
	}

	private List<DamageCalModel> GetList(DamageHealCalModeEnum damageCalMode, ListView listview)
	{
		List<DamageCalModel> result;
		if (damageCalMode == DamageHealCalModeEnum.Active)
		{
			if (listview == this.ListViewLeft)
			{
				result = BattleDmgCollectManager.Instance.listActiveModeDataCampSelf;
			}
			else
			{
				result = BattleDmgCollectManager.Instance.listActiveModeDataCampEnemy;
			}
		}
		else if (listview == this.ListViewLeft)
		{
			result = BattleDmgCollectManager.Instance.listInActiveModeDataCampSelf;
		}
		else
		{
			result = BattleDmgCollectManager.Instance.listInActiveModeDataCampEnemy;
		}
		return result;
	}

	public Cell CellForRow(ListView listView, int row)
	{
		string text;
		if (listView == this.ListViewLeft)
		{
			text = "cellLeft";
		}
		else
		{
			text = "cellRight";
		}
		Cell cell = listView.CellForReuseIndentify(text);
		if (cell == null)
		{
			cell = new Cell(listView);
			cell.identify = text;
			string name;
			if (listView == this.ListViewLeft)
			{
				name = "InstanceDamageCalItemLeft";
			}
			else
			{
				name = "InstanceDamageCalItemRight";
			}
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab(name);
			cell.content = instantiate2Prefab;
			instantiate2Prefab.GetComponent<RectTransform>().set_localScale(new Vector3(1f, 1f, 1f));
		}
		InstanceDamageCalItem component = cell.content.GetComponent<InstanceDamageCalItem>();
		component.BtnOpenLevel1.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickLv1);
		List<DamageCalModel> list = this.GetList(this.currentDamageCalMode, listView);
		DamageCalModel damageCalModel = list.get_Item(row);
		component.treeItem = damageCalModel;
		component.listView = listView;
		float num = 0f;
		float num2 = 135f;
		float totalNum = this.GetTotalNum(listView, this.currentDamageCalMode);
		component.ResetAll();
		component.SetUI(damageCalModel, totalNum);
		if (damageCalModel.open)
		{
			int leftOrRight;
			if (listView == this.ListViewLeft)
			{
				leftOrRight = 0;
			}
			else
			{
				leftOrRight = 1;
			}
			for (int i = 0; i < damageCalModel.listChildren.get_Count(); i++)
			{
				DamageCalItemLevel2 damageCalItemLevel = component.SetChild(component.Level2, -50f - num, leftOrRight);
				damageCalItemLevel.BtnOpenLevel2.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickLv2);
				damageCalItemLevel.ResetAll();
				num += 100f;
				num2 += 100f;
				DamageCalModel damageCalModel2 = damageCalModel.listChildren.get_Item(i);
				damageCalItemLevel.SetUI(damageCalModel2, totalNum);
				damageCalItemLevel.treeItem = damageCalModel2;
				damageCalItemLevel.listView = listView;
				if (damageCalModel2.open)
				{
					for (int j = 0; j < damageCalModel2.listChildren.get_Count(); j++)
					{
						num += 85f;
						num2 += 85f;
						float y = -50f - 85f * (float)j;
						DamageCalItemLevel3 damageCalItemLevel2 = damageCalItemLevel.SetChild(damageCalItemLevel.Level3, y, leftOrRight);
						DamageCalModel model = damageCalModel2.listChildren.get_Item(j);
						damageCalItemLevel2.SetUI(model, totalNum);
					}
				}
			}
		}
		return cell;
	}

	public float SpacingForRow(ListView listView, int row)
	{
		List<DamageCalModel> list = this.GetList(this.currentDamageCalMode, listView);
		DamageCalModel damageCalModel = list.get_Item(row);
		float num = 135f;
		if (damageCalModel.open)
		{
			for (int i = 0; i < damageCalModel.listChildren.get_Count(); i++)
			{
				num += 100f;
				DamageCalModel damageCalModel2 = damageCalModel.listChildren.get_Item(i);
				if (damageCalModel2.open)
				{
					for (int j = 0; j < damageCalModel2.listChildren.get_Count(); j++)
					{
						num += 85f;
					}
				}
			}
		}
		return num + 2f;
	}

	public uint CountOfRows(ListView listView)
	{
		List<DamageCalModel> list = this.GetList(this.currentDamageCalMode, listView);
		return (uint)list.get_Count();
	}
}
