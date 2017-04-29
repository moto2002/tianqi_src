using Foundation.Core.Databinding;
using GameData;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignInUI : UIBase, ListViewInterface
{
	public enum SignInUIState
	{
		MonthSign,
		SeverSign
	}

	public const int COUNT_LINE_MonthSign = 5;

	private Text TextSignNum;

	private Text TextRemainAddSignNum;

	private ListView ListViewMonthSignIn;

	private ListView ListViewServe;

	private Transform MonthSignIn;

	private Transform Serve;

	private ButtonCustom BtnSignIn;

	private ButtonCustom BtnServe;

	public static SignInUI.SignInUIState currentState;

	private List<MonthSign> listMonthSign = new List<MonthSign>();

	private List<OpenServer> listOpenSever = new List<OpenServer>();

	private VerticalLayoutGroup m_awardlist;

	protected override void Preprocessing()
	{
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = true;
	}

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.TextSignNum = base.FindTransform("TextSignNum").GetComponent<Text>();
		this.TextRemainAddSignNum = base.FindTransform("TextRemainAddSignNum").GetComponent<Text>();
		this.ListViewMonthSignIn = base.FindTransform("ListViewMonthSignIn").GetComponent<ListView>();
		this.ListViewServe = base.FindTransform("ListViewServe").GetComponent<ListView>();
		this.MonthSignIn = base.FindTransform("MonthSignIn");
		this.Serve = base.FindTransform("Serve");
		this.BtnSignIn = base.FindTransform("BtnSignIn").GetComponent<ButtonCustom>();
		this.BtnServe = base.FindTransform("BtnServe").GetComponent<ButtonCustom>();
		this.BtnSignIn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnSignIn);
		this.BtnServe.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnServe);
		this.ListViewMonthSignIn = base.FindTransform("ListViewMonthSignIn").GetComponent<ListView>();
		this.ListViewMonthSignIn.manager = this;
		this.ListViewMonthSignIn.Init(ListView.ListViewScrollStyle.Up);
		this.ListViewServe = base.FindTransform("ListViewServe").GetComponent<ListView>();
		this.ListViewServe.manager = this;
		this.ListViewServe.Init(ListView.ListViewScrollStyle.Up);
		this.m_awardlist = base.FindTransform("SignList").GetComponent<VerticalLayoutGroup>();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshUI(SignInUI.currentState);
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void ActionClose()
	{
		base.ActionClose();
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnGetSignChangedNty, new Callback(this.OnGetSignChangedNty));
		EventDispatcher.AddListener(EventNames.OnGetMonthTotalChangeNty, new Callback(this.OnGetMonthTotalChangeNty));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnGetSignChangedNty, new Callback(this.OnGetSignChangedNty));
		EventDispatcher.RemoveListener(EventNames.OnGetMonthTotalChangeNty, new Callback(this.OnGetMonthTotalChangeNty));
	}

	private void OnGetSignChangedNty()
	{
		this.RefreshUI(SignInUI.currentState);
	}

	private void OnGetMonthTotalChangeNty()
	{
		this.RefreshUI(SignInUI.currentState);
	}

	private void OnClickBtnSignIn(GameObject sender)
	{
		this.RefreshUI(SignInUI.SignInUIState.MonthSign);
	}

	private void OnClickBtnServe(GameObject sender)
	{
		this.RefreshUI(SignInUI.SignInUIState.SeverSign);
	}

	private void OnClickBtnRewardDetail(GameObject sender)
	{
		MonthSignInChildItem item = sender.get_transform().get_parent().GetComponent<MonthSignInChildItem>();
		if (item.state == MonthSignInChildItem.MonthSignInChildItemState.CanSign)
		{
			SignInManager.Instance.SendSignReq(0, item.monthSignCache);
		}
		else if (item.state == MonthSignInChildItem.MonthSignInChildItemState.CanResign)
		{
			string chineseContent = GameDataUtils.GetChineseContent(502212, false);
			string text = GameDataUtils.GetChineseContent(502213, false);
			text = text.Replace("xx", (item.monthSignCache.resignCost + DataReader<Resign>.Get(SignInManager.Instance.monthSignInfo.repairUsedNum + 1).cost).ToString());
			DialogBoxUIViewModel.Instance.ShowAsOKCancel(chineseContent, text, delegate
			{
			}, delegate
			{
				SignInManager.Instance.SendSignReq(1, item.monthSignCache);
			}, GameDataUtils.GetChineseContent(505113, false), GameDataUtils.GetChineseContent(505114, false), "button_orange_1", "button_yellow_1", null, true, true);
		}
		else if (item.state == MonthSignInChildItem.MonthSignInChildItemState.HaveSign)
		{
			ItemTipUIViewModel.ShowItem(item.monthSignCache.itemId, null);
		}
		else if (item.state == MonthSignInChildItem.MonthSignInChildItemState.None)
		{
			ItemTipUIViewModel.ShowItem(item.monthSignCache.itemId, null);
		}
	}

	private void OnClickBtnGetSeverSign(GameObject sender)
	{
		ItemServerSignIn component = sender.get_transform().get_parent().get_parent().GetComponent<ItemServerSignIn>();
		if (component.itemServerSignInState == ItemServerSignIn.ItemServerSignInState.CanGetReward)
		{
			SignInManager.Instance.SendAcceptOpenAwardsReq(component.openServerCache.time, component.openServerCache);
		}
		else
		{
			string text = GameDataUtils.GetChineseContent(502215, false);
			text = text.Replace("xx", component.openServerCache.time.ToString());
			UIManagerControl.Instance.ShowToastText(text);
		}
	}

	private void OnClickBtnReSignIn(GameObject sender)
	{
		this.OnClickBtnRewardDetail(sender.get_transform().get_parent().get_parent().FindChild("BtnRewardDetail").get_gameObject());
	}

	private void RefreshUI(SignInUI.SignInUIState state)
	{
		this.RefreshUIState(state);
		this.RefreshImageBadage();
	}

	private void RefreshUIState(SignInUI.SignInUIState state)
	{
		this.SetButtonCheck(this.BtnSignIn.get_transform(), false);
		this.SetButtonCheck(this.BtnServe.get_transform(), false);
		SignInUI.currentState = state;
		if (SignInUI.currentState == SignInUI.SignInUIState.MonthSign)
		{
			this.SetButtonCheck(this.BtnSignIn.get_transform(), true);
			this.MonthSignIn.get_gameObject().SetActive(true);
			this.Serve.get_gameObject().SetActive(false);
			this.CollectThisMonthSignData();
			this.SetMonthSignNum();
			this.ListViewMonthSignIn.Refresh();
			this.UpdateAccumulateList();
		}
		else if (SignInUI.currentState == SignInUI.SignInUIState.SeverSign)
		{
			this.SetButtonCheck(this.BtnServe.get_transform(), true);
			this.MonthSignIn.get_gameObject().SetActive(false);
			this.Serve.get_gameObject().SetActive(true);
			this.CollectListOpenSever();
			this.ListViewServe.Refresh();
		}
	}

	private void SetButtonCheck(Transform btn, bool isCheck)
	{
		btn.get_transform().FindChild("Image1").get_gameObject().SetActive(isCheck);
		btn.get_transform().FindChild("Text1").get_gameObject().SetActive(isCheck);
		btn.get_transform().FindChild("Image2").get_gameObject().SetActive(!isCheck);
		btn.get_transform().FindChild("Text2").get_gameObject().SetActive(!isCheck);
	}

	private void RefreshImageBadage()
	{
	}

	private void SetMonthSignNum()
	{
		string text = GameDataUtils.GetChineseContent(502216, false);
		text = text.Replace("{0}", "<color=#EF7E0C> " + SignInManager.Instance.monthSignInfo.signDays.ToString() + " </color>");
		this.TextSignNum.set_text(text);
		this.TextRemainAddSignNum.set_text(string.Format("本月剩余补签次数 <color=#EF7E0C>{0}</color> 次", SignInManager.Instance.monthSignInfo.repairNum));
	}

	private void CollectThisMonthSignData()
	{
		if (SignInManager.Instance.monthSignInfo == null)
		{
			return;
		}
		this.listMonthSign.Clear();
		List<MonthSign> dataList = DataReader<MonthSign>.DataList;
		int year = SignInManager.Instance.monthSignInfo.year;
		int month = SignInManager.Instance.monthSignInfo.month;
		MonthSign monthSign = dataList.Find((MonthSign a) => a.date.get_Item(0) == year && a.date.get_Item(1) == month && a.date.get_Item(2) == 1);
		while (monthSign != null && monthSign.date.get_Item(0) == year && monthSign.date.get_Item(1) == month)
		{
			this.listMonthSign.Add(monthSign);
			if (monthSign.id == monthSign.nextID)
			{
				break;
			}
			monthSign = ((!DataReader<MonthSign>.Contains(monthSign.nextID)) ? null : DataReader<MonthSign>.Get(monthSign.nextID));
		}
	}

	private Cell CellForRow_MonthSign(ListView listView, int row)
	{
		string text = "cell";
		Cell cell = listView.CellForReuseIndentify(text);
		if (cell == null)
		{
			cell = new Cell(listView);
			cell.identify = text;
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("ItemMonthSignIn");
			cell.content = instantiate2Prefab;
			instantiate2Prefab.set_name("ItemMonthSignIn" + row);
			instantiate2Prefab.GetComponent<RectTransform>().set_localScale(new Vector3(1f, 1f, 1f));
		}
		if (SignInManager.Instance.monthSignInfo == null)
		{
			return cell;
		}
		ItemMonthSignIn component = cell.content.GetComponent<ItemMonthSignIn>();
		for (int i = 0; i < 5; i++)
		{
			int num = row * 5 + i;
			MonthSignInChildItem monthSignInChildItem = component.childItems.get_Item(i);
			if (row * 5 + i >= this.listMonthSign.get_Count())
			{
				monthSignInChildItem.get_gameObject().SetActive(false);
			}
			else
			{
				bool canSign = false;
				bool canResign = false;
				bool haveSign = false;
				if (num + 1 - SignInManager.Instance.monthSignInfo.signDays <= 0)
				{
					haveSign = true;
				}
				else if (num - SignInManager.Instance.monthSignInfo.signDays == 0 && !SignInManager.Instance.monthSignInfo.isSign)
				{
					canSign = true;
				}
				else if (num - SignInManager.Instance.monthSignInfo.signDays == 0 && SignInManager.Instance.monthSignInfo.repairNum > 0)
				{
					canResign = true;
				}
				monthSignInChildItem.get_gameObject().SetActive(true);
				MonthSign monthSign = this.listMonthSign.get_Item(row * 5 + i);
				monthSignInChildItem.SetUI(monthSign, haveSign, canSign, canResign, num);
				monthSignInChildItem.BtnRewardDetail.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnRewardDetail);
				monthSignInChildItem.BtnReSignIn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnReSignIn);
			}
		}
		return cell;
	}

	private uint CountOfRows_MonthSign()
	{
		if (SignInManager.Instance.monthSignInfo == null)
		{
			return 0u;
		}
		int num = this.listMonthSign.get_Count() / 5;
		if (this.listMonthSign.get_Count() % 5 != 0)
		{
			num++;
		}
		return (uint)num;
	}

	private void CollectListOpenSever()
	{
		this.listOpenSever.Clear();
		this.listOpenSever.AddRange(DataReader<OpenServer>.DataList);
	}

	private Cell CellForRow_SeverSign(ListView listView, int row)
	{
		string text = "cell1";
		Cell cell = listView.CellForReuseIndentify(text);
		if (cell == null)
		{
			cell = new Cell(listView);
			cell.identify = text;
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("ItemServerSignIn");
			cell.content = instantiate2Prefab;
			instantiate2Prefab.GetComponent<RectTransform>().set_localScale(new Vector3(1f, 1f, 1f));
		}
		if (SignInManager.Instance.monthSignInfo == null)
		{
			return cell;
		}
		ItemServerSignIn component = cell.content.GetComponent<ItemServerSignIn>();
		ItemServerSignIn.ItemServerSignInState state = ItemServerSignIn.ItemServerSignInState.CanGetReward;
		if (SignInManager.Instance.monthSignInfo.serialDays.get_Item(row) == 0)
		{
			state = ItemServerSignIn.ItemServerSignInState.CanNotGetReward;
		}
		else if (SignInManager.Instance.monthSignInfo.serialDays.get_Item(row) == 1)
		{
			state = ItemServerSignIn.ItemServerSignInState.CanGetReward;
		}
		else if (SignInManager.Instance.monthSignInfo.serialDays.get_Item(row) == 2)
		{
			state = ItemServerSignIn.ItemServerSignInState.HaveGot;
		}
		component.SetUI(this.listOpenSever.get_Item(row), state);
		component.BtnGet.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnGetSeverSign);
		return cell;
	}

	private uint CountOfRows_SeverSign()
	{
		return (uint)this.listOpenSever.get_Count();
	}

	public Cell CellForRow(ListView listView, int row)
	{
		if (SignInUI.currentState == SignInUI.SignInUIState.MonthSign)
		{
			return this.CellForRow_MonthSign(listView, row);
		}
		if (SignInUI.currentState == SignInUI.SignInUIState.SeverSign)
		{
			return this.CellForRow_SeverSign(listView, row);
		}
		return null;
	}

	public float SpacingForRow(ListView listView, int row)
	{
		if (SignInManager.Instance.monthSignInfo == null)
		{
			return 0f;
		}
		if (SignInUI.currentState == SignInUI.SignInUIState.MonthSign)
		{
			return 162f;
		}
		if (SignInUI.currentState == SignInUI.SignInUIState.SeverSign)
		{
			return 130f;
		}
		return 100f;
	}

	public uint CountOfRows(ListView listView)
	{
		if (SignInUI.currentState == SignInUI.SignInUIState.MonthSign)
		{
			return this.CountOfRows_MonthSign();
		}
		if (SignInUI.currentState == SignInUI.SignInUIState.SeverSign)
		{
			return this.CountOfRows_SeverSign();
		}
		return 0u;
	}

	public void UpdateAccumulateList()
	{
		if (SignInManager.Instance.monthToalInfo == null)
		{
			return;
		}
		this.ClearScroll();
		List<MonthTotalInfo> monthToalInfo = SignInManager.Instance.monthToalInfo;
		int num = 1;
		for (int i = 0; i < monthToalInfo.get_Count(); i++)
		{
			checkInReward checkInReward = DataReader<checkInReward>.Get(monthToalInfo.get_Item(i).id);
			if (checkInReward != null)
			{
				this.UpdateAccumulateItemInfo(num, checkInReward.dropId, checkInReward.text1, monthToalInfo.get_Item(i).flag);
				num++;
			}
		}
	}

	private void ClearScroll()
	{
		for (int i = 0; i < this.m_awardlist.get_transform().get_childCount(); i++)
		{
			this.m_awardlist.get_transform().GetChild(i).get_gameObject().SetActive(false);
		}
	}

	private void UpdateAccumulateItemInfo(int index, int id, int title, int flag)
	{
		Transform transform = this.m_awardlist.get_transform().FindChild("AccumulateSignItem" + index);
		if (transform != null)
		{
			transform.get_gameObject().SetActive(true);
			transform.GetComponent<AccumulateSignItem>().UpdateItemState(flag);
		}
		else
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("AccumulateSignItem");
			instantiate2Prefab.get_transform().SetParent(this.m_awardlist.get_transform(), false);
			instantiate2Prefab.set_name("AccumulateSignItem" + index);
			instantiate2Prefab.get_gameObject().SetActive(true);
			instantiate2Prefab.GetComponent<AccumulateSignItem>().UpdateItem(index, id, title, flag);
		}
	}
}
