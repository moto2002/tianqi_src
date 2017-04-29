using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleLogUI : UIBase, ListViewInterface
{
	private ListView ListViewLog;

	private List<string> listLog = new List<string>();

	private Transform NoLog;

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
		this.ListViewLog = base.FindTransform("ListViewLog").GetComponent<ListView>();
		this.NoLog = base.FindTransform("NoLog");
		this.ListViewLog.manager = this;
		this.ListViewLog.Init(ListView.ListViewScrollStyle.Up);
	}

	public void RefreshUI()
	{
		this.ResetData();
		this.NoLog.get_gameObject().SetActive(this.listLog.get_Count() == 0);
		this.ListViewLog.Refresh();
	}

	private void ResetData()
	{
		this.listLog.Clear();
		for (int i = GangFightManager.Instance.FightRecord.get_Count() - 1; i >= 0; i--)
		{
			FightRecord fightRecord = GangFightManager.Instance.FightRecord.get_Item(i);
			string text;
			string text2;
			int num;
			int num2;
			if (fightRecord.fromId == fightRecord.winnerId)
			{
				text = fightRecord.fromName;
				text2 = fightRecord.toName;
				num = fightRecord.fromWinCount;
				num2 = fightRecord.toWinCount;
			}
			else
			{
				text2 = fightRecord.fromName;
				text = fightRecord.toName;
				num2 = fightRecord.fromWinCount;
				num = fightRecord.toWinCount;
			}
			string text3 = "<color=#A75A42>(" + fightRecord.time + ")</color>";
			string text4 = string.Empty;
			string text5 = string.Empty;
			if (num == 0)
			{
				text4 = GameDataUtils.GetChineseContent(505013, false);
			}
			else
			{
				string chineseContent = GameDataUtils.GetChineseContent(505011, false);
				text4 = chineseContent.Replace("xx", " " + num.ToString() + " ");
			}
			if (num >= num2)
			{
				text5 = string.Concat(new string[]
				{
					text3,
					"<color=#b600a7>[",
					text,
					"]</color><color=#4A3C31>",
					GameDataUtils.GetChineseContent(505010, false),
					"</color><color=#b600a7>[",
					text2,
					"]</color><color=#4A3C31>",
					text4,
					"</color>"
				});
			}
			else
			{
				text5 = string.Concat(new object[]
				{
					text3,
					"<color=#b600a7>[",
					text,
					"]</color><color=#4A3C31>",
					GameDataUtils.GetChineseContent(505021, false),
					"</color><color=#b600a7>[",
					text2,
					"]</color><color=#4A3C31>",
					GameDataUtils.GetChineseContent(505022, false),
					" ",
					num2,
					" ",
					GameDataUtils.GetChineseContent(505004, false),
					"，",
					text4,
					"</color>"
				});
			}
			this.listLog.Add(text5);
		}
	}

	public Cell CellForRow(ListView listView, int row)
	{
		Cell cell = listView.CellForReuseIndentify("cell");
		if (cell == null)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("BattleLogItem");
			instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
			instantiate2Prefab.SetActive(true);
			cell = new Cell(listView);
			cell.identify = "cell";
			cell.content = instantiate2Prefab;
		}
		cell.content.get_transform().FindChild("Text").GetComponent<Text>().set_text(this.listLog.get_Item(row));
		return cell;
	}

	public float SpacingForRow(ListView listView, int row)
	{
		return 70f;
	}

	public uint CountOfRows(ListView listView)
	{
		return (uint)this.listLog.get_Count();
	}
}
