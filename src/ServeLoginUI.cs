using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ServeLoginUI : UIBase, ListViewInterface
{
	private ListView ListViewServe;

	private List<EveryDayInfo> listOpenSever = new List<EveryDayInfo>();

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		this.ListViewServe = base.FindTransform("ListViewServe").GetComponent<ListView>();
		this.ListViewServe.manager = this;
		this.ListViewServe.Init(ListView.ListViewScrollStyle.Up);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshUI();
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.OnLoginWelfareUpdate, new Callback(this.OnGetSignChangedNty));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.OnLoginWelfareUpdate, new Callback(this.OnGetSignChangedNty));
	}

	private void OnGetSignChangedNty()
	{
		this.RefreshUI();
	}

	private void RefreshUI()
	{
		this.CollectListOpenSever();
		this.ListViewServe.Refresh();
	}

	private void CollectListOpenSever()
	{
		this.listOpenSever.Clear();
		this.listOpenSever.AddRange(SignInManager.Instance.loginWelfareList);
	}

	private void OnClickBtnGetSeverSign(GameObject sender)
	{
		ItemServerSignIn component = sender.get_transform().get_parent().get_parent().GetComponent<ItemServerSignIn>();
		if (component.itemServerSignInState == ItemServerSignIn.ItemServerSignInState.CanGetReward)
		{
			SignInManager.Instance.SendGetLoginWelfareReq(component.everydayinfoCache.loginDays);
		}
		else
		{
			string text = GameDataUtils.GetChineseContent(502215, false);
			text = text.Replace("xx", component.everydayinfoCache.loginDays.ToString());
			UIManagerControl.Instance.ShowToastText(text);
		}
	}

	public Cell CellForRow(ListView listView, int row)
	{
		return this.CellForRow_SeverSign(listView, row);
	}

	public float SpacingForRow(ListView listView, int row)
	{
		return 130f;
	}

	public uint CountOfRows(ListView listView)
	{
		return this.CountOfRows_SeverSign();
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
		component.SetUI(this.listOpenSever.get_Item(row));
		component.BtnGet.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnGetSeverSign);
		return cell;
	}

	private uint CountOfRows_SeverSign()
	{
		return (uint)this.listOpenSever.get_Count();
	}
}
