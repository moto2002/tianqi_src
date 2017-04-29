using GameData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LinePool : MonoBehaviour
{
	public GameObject golight;

	public GameObject gogray;

	[HideInInspector]
	private Dictionary<int, LineData> Items = new Dictionary<int, LineData>();

	[HideInInspector]
	private List<XianLuShengCheng> line;

	[HideInInspector]
	private List<int> pipleIds;

	private List<int> pointId;

	private LightLineLoad final;

	private int stage;

	public List<List<LightLineLoad>> listLoad = new List<List<LightLineLoad>>();

	private List<GameObject> listGameObj = new List<GameObject>();

	private void Start()
	{
		this.golight.SetActive(false);
		this.gogray.SetActive(false);
	}

	private GameObject CreatLineType(int id, LineType lineType)
	{
		GameObject gameObject = UGUITools.AddChild(base.get_gameObject(), this.golight, false);
		LightLineLoad component = gameObject.GetComponent<LightLineLoad>();
		component.SetLine(lineType);
		component.Id = id;
		this.Items.set_Item(id, new LineData
		{
			id = id,
			type = lineType,
			go = gameObject
		});
		return gameObject;
	}

	public List<int> CreateAllLine(int state, out List<int> nextPoints, bool isAction = true)
	{
		this.stage = state;
		nextPoints = new List<int>();
		List<int> list = new List<int>();
		List<int> listIds = CharacterManager.Instance.GetListIds(state);
		if (this.line == null)
		{
			this.line = CharacterManager.Instance.GetStateLine(state);
		}
		LinePool.<CreateAllLine>c__AnonStorey330 <CreateAllLine>c__AnonStorey = new LinePool.<CreateAllLine>c__AnonStorey330();
		<CreateAllLine>c__AnonStorey.<>f__this = this;
		<CreateAllLine>c__AnonStorey.i = 0;
		while (<CreateAllLine>c__AnonStorey.i < this.line.get_Count())
		{
			LinePool.<CreateAllLine>c__AnonStorey331 <CreateAllLine>c__AnonStorey2 = new LinePool.<CreateAllLine>c__AnonStorey331();
			<CreateAllLine>c__AnonStorey2.<>f__ref$816 = <CreateAllLine>c__AnonStorey;
			<CreateAllLine>c__AnonStorey2.<>f__this = this;
			<CreateAllLine>c__AnonStorey2.points = this.line.get_Item(<CreateAllLine>c__AnonStorey.i).end;
			bool flag = listIds.Exists((int e) => e == <CreateAllLine>c__AnonStorey2.<>f__this.line.get_Item(<CreateAllLine>c__AnonStorey2.<>f__ref$816.i).begin) || this.line.get_Item(<CreateAllLine>c__AnonStorey.i).begin == 0;
			bool flag2 = this.line.get_Item(<CreateAllLine>c__AnonStorey.i).begin == CharacterManager.Instance.NewBrightPoint && this.line.get_Item(<CreateAllLine>c__AnonStorey.i).begin != 0;
			int j;
			for (j = 0; j < <CreateAllLine>c__AnonStorey2.points.get_Count(); j++)
			{
				bool flag3 = false;
				if (isAction)
				{
					if (this.pipleIds == null)
					{
						this.pipleIds = CharacterManager.Instance.GetPiplelienPoints(state);
					}
					flag3 = ((this.line.get_Item(<CreateAllLine>c__AnonStorey.i).begin == 0 || this.pipleIds.Exists((int e) => e == this.line.get_Item(<CreateAllLine>c__AnonStorey.i).begin)) && (<CreateAllLine>c__AnonStorey2.points.get_Item(j) == 1 || this.pipleIds.Exists((int e) => e == <CreateAllLine>c__AnonStorey2.points.get_Item(j))));
				}
				bool flag4 = CharacterManager.Instance.NewBrightPoint == <CreateAllLine>c__AnonStorey2.points.get_Item(j);
				bool flag5 = listIds.Exists((int e) => e == <CreateAllLine>c__AnonStorey2.points.get_Item(j)) || <CreateAllLine>c__AnonStorey2.points.get_Item(j) == 1;
				bool flag6 = (flag && flag4) || (flag2 && flag5);
				if (!isAction && flag6)
				{
					Debug.LogError("确定要走的线路" + <CreateAllLine>c__AnonStorey2.points.get_Item(j));
					this.PointToPoint(this.line.get_Item(<CreateAllLine>c__AnonStorey.i).line.get_Item(j), <CreateAllLine>c__AnonStorey2.points.get_Item(j) == 1);
					if (flag4)
					{
						list.Add(this.line.get_Item(<CreateAllLine>c__AnonStorey.i).begin);
					}
					if (flag2)
					{
						list.Add(<CreateAllLine>c__AnonStorey2.points.get_Item(j));
					}
				}
				else
				{
					LineType lineType;
					if (!isAction)
					{
						if (flag && flag5)
						{
							lineType = LineType.Blue;
						}
						else
						{
							lineType = LineType.Norml;
							if (flag && <CreateAllLine>c__AnonStorey2.points.get_Item(j) != 1)
							{
								nextPoints.Add(<CreateAllLine>c__AnonStorey2.points.get_Item(j));
							}
						}
					}
					else
					{
						lineType = ((!flag3) ? LineType.Blue : LineType.Yellow);
					}
					this.PointToPoint(this.line.get_Item(<CreateAllLine>c__AnonStorey.i).line.get_Item(j), lineType);
				}
			}
			<CreateAllLine>c__AnonStorey.i++;
		}
		return list;
	}

	private void PointToPoint(int line, LineType lineType)
	{
		XianLuZuoBiao xianLuZuoBiao = DataReader<XianLuZuoBiao>.Get(line);
		for (int i = 1; i < xianLuZuoBiao.line.get_Count(); i++)
		{
			GameObject go = this.CreateOneLine(xianLuZuoBiao.line.get_Item(i - 1), lineType);
			LinePool.SetLinePos(xianLuZuoBiao.line.get_Item(i - 1), xianLuZuoBiao.line.get_Item(i), go);
		}
	}

	private void PointToPoint(int line, bool isFinal)
	{
		XianLuZuoBiao xianLuZuoBiao = DataReader<XianLuZuoBiao>.Get(line);
		List<LightLineLoad> list = new List<LightLineLoad>();
		for (int i = 1; i < xianLuZuoBiao.line.get_Count(); i++)
		{
			int num = xianLuZuoBiao.line.get_Item(i - 1);
			if (this.Items.ContainsKey(num))
			{
				LineData lineData = this.Items.get_Item(num);
				lineData.type = LineType.Blue;
				list.Add(lineData.go.GetComponent<LightLineLoad>());
			}
		}
		if (list.get_Count() > 0)
		{
			if (isFinal)
			{
				this.final = list.get_Item(0);
			}
			else
			{
				this.listLoad.Add(list);
			}
		}
	}

	private GameObject CreateOneLine(int startId, LineType lienType)
	{
		if (!this.Items.ContainsKey(startId))
		{
			return this.CreatLineType(startId, lienType);
		}
		LineData lineData = this.Items.get_Item(startId);
		if (lienType == lineData.type)
		{
			return null;
		}
		lineData.type = lienType;
		lineData.go.GetComponent<LightLineLoad>().SetLine(lienType);
		return null;
	}

	private static void SetLinePos(int startId, int endId, GameObject go)
	{
		if (go != null)
		{
			Vector2 vector = new Vector2((float)DataReader<XianLuDian>.Get(startId).coordinate.get_Item(0), (float)DataReader<XianLuDian>.Get(startId).coordinate.get_Item(1));
			Vector2 vector2 = new Vector2((float)DataReader<XianLuDian>.Get(endId).coordinate.get_Item(0), (float)DataReader<XianLuDian>.Get(endId).coordinate.get_Item(1));
			go.SetActive(true);
			float num = Vector2.Distance(vector, vector2);
			float num2 = Vector2.Angle(Vector2.get_right(), vector2 - vector);
			if (vector2.y < vector.y)
			{
				num2 *= -1f;
			}
			float num3 = 1f;
			float num4 = 2f;
			RectTransform component = go.GetComponent<RectTransform>();
			component.set_anchoredPosition(new Vector2(vector.x + num3, vector.y));
			component.set_sizeDelta(new Vector2(num + num4, component.get_sizeDelta().y));
			component.set_localRotation(Quaternion.Euler(new Vector3(0f, 0f, num2)));
			RectTransform component2 = component.FindChild("Lightbg").GetComponent<RectTransform>();
			component2.set_sizeDelta(new Vector2(num + num4, component2.get_sizeDelta().y));
		}
	}

	public void PointToPointAnim(bool isfinal)
	{
		base.StartCoroutine(this.StartLoad(this.listLoad, isfinal));
	}

	private void PointToPointHandle()
	{
		CharacterManager.Instance.SendRise();
		this.listLoad.Clear();
		this.ClearObj();
	}

	public void AllLineAnim()
	{
		List<int> list = new List<int>();
		List<LightLineLoad> list2 = new List<LightLineLoad>();
		List<int> piplelienPoints = CharacterManager.Instance.GetPiplelienPoints(this.stage);
		int i;
		for (i = 0; i < this.line.get_Count(); i++)
		{
			int j;
			for (j = 0; j < this.line.get_Item(i).end.get_Count(); j++)
			{
				bool flag = (this.line.get_Item(i).begin == 0 || piplelienPoints.Exists((int e) => e == this.line.get_Item(i).begin)) && (this.line.get_Item(i).end.get_Item(j) == 1 || piplelienPoints.Exists((int e) => e == this.line.get_Item(i).end.get_Item(j)));
				if (flag)
				{
					XianLuZuoBiao xianLuZuoBiao = DataReader<XianLuZuoBiao>.Get(this.line.get_Item(i).line.get_Item(j));
					int id = xianLuZuoBiao.line.get_Item(0);
					if (!list.Exists((int e) => e == id) && this.Items.ContainsKey(id))
					{
						int num = (this.line.get_Item(i).end.get_Item(j) != 1) ? piplelienPoints.FindIndex((int e) => e == this.line.get_Item(i).end.get_Item(j)) : piplelienPoints.get_Count();
						if (num < 0)
						{
							num = piplelienPoints.FindIndex((int e) => e == this.line.get_Item(i).begin);
						}
						LightLineLoad component = this.Items.get_Item(id).go.GetComponent<LightLineLoad>();
						component.sort = num;
						list2.Add(component);
						list.Add(id);
					}
					else
					{
						Debug.LogError("没有对应的关键组合ID:" + this.line.get_Item(i).line.get_Item(j));
					}
				}
			}
		}
		list2.Sort((LightLineLoad a, LightLineLoad b) => a.sort.CompareTo(b.sort));
		base.StartCoroutine(this.StartLoad(list2, piplelienPoints));
	}

	private LightLineLoad GetLineIdByPointID(int id)
	{
		XianLuZuoBiao xianLuZuoBiao = DataReader<XianLuZuoBiao>.Get(id);
		int num = xianLuZuoBiao.line.get_Item(0);
		if (this.Items.ContainsKey(num))
		{
			return this.Items.get_Item(num).go.GetComponent<LightLineLoad>();
		}
		Debug.LogError("没有对应的关键组合ID:");
		return null;
	}

	private void AllLineHandle()
	{
	}

	[DebuggerHidden]
	private IEnumerator StartLoad(List<LightLineLoad> list, List<int> ids)
	{
		LinePool.<StartLoad>c__Iterator5F <StartLoad>c__Iterator5F = new LinePool.<StartLoad>c__Iterator5F();
		<StartLoad>c__Iterator5F.list = list;
		<StartLoad>c__Iterator5F.ids = ids;
		<StartLoad>c__Iterator5F.<$>list = list;
		<StartLoad>c__Iterator5F.<$>ids = ids;
		return <StartLoad>c__Iterator5F;
	}

	[DebuggerHidden]
	private IEnumerator StartLoad(List<List<LightLineLoad>> list, bool isfinal)
	{
		LinePool.<StartLoad>c__Iterator60 <StartLoad>c__Iterator = new LinePool.<StartLoad>c__Iterator60();
		<StartLoad>c__Iterator.list = list;
		<StartLoad>c__Iterator.isfinal = isfinal;
		<StartLoad>c__Iterator.<$>list = list;
		<StartLoad>c__Iterator.<$>isfinal = isfinal;
		<StartLoad>c__Iterator.<>f__this = this;
		return <StartLoad>c__Iterator;
	}

	[DebuggerHidden]
	private IEnumerator StartLoadEnd()
	{
		LinePool.<StartLoadEnd>c__Iterator61 <StartLoadEnd>c__Iterator = new LinePool.<StartLoadEnd>c__Iterator61();
		<StartLoadEnd>c__Iterator.<>f__this = this;
		return <StartLoadEnd>c__Iterator;
	}

	private void ClearObj()
	{
		for (int i = 0; i < this.listGameObj.get_Count(); i++)
		{
			Object.DestroyImmediate(this.listGameObj.get_Item(i));
		}
		this.listGameObj.Clear();
	}
}
