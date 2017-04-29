using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListPool : MonoBehaviour
{
	[HideInInspector]
	private int MAX = 100;

	private int mLoadNumberFrame = 100;

	[HideInInspector]
	public float LoadInterval;

	public GameObject temp;

	private string temp_prefabname;

	public bool isAnimation;

	public int maxShow = 6;

	public int lineContainNum = 1;

	public float spacing = 1f;

	public float padding = 1f;

	public float duration = 1f;

	public ListDir Dir = ListDir.Down;

	public Vector2 moveing = Vector2.get_right();

	public Vector2 offsets = Vector2.get_zero();

	[HideInInspector]
	public List<GameObject> Items = new List<GameObject>();

	[HideInInspector]
	public bool isAutoCalcalateLayout = true;

	private bool IsContainLayoutWidget;

	private int mNum;

	private int mIndex;

	private float mDeltaTime;

	private Action<int> LoadSuccess;

	public int LoadNumberFrame
	{
		get
		{
			return this.mLoadNumberFrame;
		}
		set
		{
			this.mLoadNumberFrame = Mathf.Clamp(value, 1, this.MAX);
		}
	}

	private void Start()
	{
		if (this.temp != null)
		{
			this.temp.SetActive(false);
		}
	}

	public void SetItem(string prefab_name)
	{
		this.temp_prefabname = prefab_name;
	}

	public void Create(int num, Action<int> loadSuccess)
	{
		if (base.GetComponent<LayoutGroup>() != null)
		{
			this.IsContainLayoutWidget = true;
		}
		this.LoadSuccess = loadSuccess;
		this.mNum = num;
		this.mIndex = 0;
		this.HideRedundance();
	}

	public void Clear()
	{
		this.mNum = 0;
		this.mIndex = 0;
		this.HideRedundance();
	}

	public void Release()
	{
		this.mNum = 0;
		for (int i = 0; i < this.Items.get_Count(); i++)
		{
			Object.DestroyImmediate(this.Items.get_Item(i));
		}
		this.Items.Clear();
	}

	private void DoAnimation(int index)
	{
		if (this.isAnimation)
		{
			if (index >= this.maxShow * this.lineContainNum)
			{
				return;
			}
			RectTransform component = this.Items.get_Item(index).GetComponent<RectTransform>();
			component.set_anchoredPosition(this.GetPos(index) + this.moveing);
			Vector3 vector = component.get_anchoredPosition() - this.moveing;
			base.StartCoroutine(component.MoveToAnchoredPosition(vector, 0.3f + this.duration * this.duration * this.duration * (float)index, EaseType.CubeOut, null));
		}
	}

	private Vector2 GetPos(int i)
	{
		Vector2 dir = this.GetDir();
		return dir * this.spacing * (float)(i / this.lineContainNum) + dir * this.padding + this.offsets;
	}

	private Vector2 GetDir()
	{
		Vector2 result;
		switch (this.Dir)
		{
		case ListDir.Right:
			result = Vector2.get_right();
			break;
		case ListDir.Down:
			result = Vector2.get_down();
			break;
		case ListDir.Left:
			result = Vector2.get_left();
			break;
		case ListDir.Up:
			result = Vector2.get_up();
			break;
		default:
			result = Vector2.get_down();
			break;
		}
		return result;
	}

	private void Update()
	{
		if (this.LoadInterval <= 0f)
		{
			while (this.mIndex < this.mNum && this.IsInPoolWithActive(this.mIndex))
			{
				this.DoInstantiateJustOne();
			}
			this.LoadOnce();
		}
		else
		{
			this.mDeltaTime += Time.get_deltaTime();
			if (this.mDeltaTime <= this.LoadInterval)
			{
				return;
			}
			this.mDeltaTime = 0f;
			this.LoadOnce();
		}
	}

	private void LoadOnce()
	{
		int num = Mathf.Min(this.LoadNumberFrame, this.mNum - this.mIndex);
		for (int i = 0; i < num; i++)
		{
			this.DoInstantiateJustOne();
		}
	}

	private void DoInstantiateJustOne()
	{
		this.GetInstantiate(this.mIndex);
		this.SetContentSize(this.mIndex + 1);
		this.mIndex++;
	}

	private bool IsInPoolWithActive(int index)
	{
		return index < this.Items.get_Count() && this.Items.get_Item(index).get_activeSelf();
	}

	private void GetInstantiate(int index)
	{
		if (index < this.Items.get_Count())
		{
			this.Items.get_Item(index).SetActive(true);
		}
		else
		{
			GameObject gameObject = this.DoInstantiate(index);
			if (gameObject != null)
			{
				gameObject.SetActive(true);
				this.Items.Add(gameObject);
			}
		}
		this.InstantiateSuccess(index);
	}

	private void InstantiateSuccess(int index)
	{
		this.DoAnimation(index);
		if (this.LoadSuccess != null)
		{
			this.LoadSuccess.Invoke(this.mIndex);
		}
	}

	private void HideRedundance()
	{
		for (int i = this.mNum; i < this.Items.get_Count(); i++)
		{
			GameObject gameObject = this.Items.get_Item(i);
			if (gameObject != null)
			{
				gameObject.SetActive(false);
			}
		}
	}

	private GameObject DoInstantiate(int index)
	{
		GameObject gameObject;
		if (this.temp != null)
		{
			gameObject = UGUITools.AddChild(base.get_gameObject(), this.temp, false);
			gameObject.set_name(this.temp.get_name() + index.ToString());
		}
		else
		{
			gameObject = ResourceManager.GetInstantiate2Prefab(this.temp_prefabname);
			UGUITools.SetParent(base.get_gameObject(), gameObject, false);
			gameObject.set_name(this.temp_prefabname + index.ToString());
		}
		if (this.isAnimation)
		{
			gameObject.GetComponent<RectTransform>().set_anchoredPosition((index < this.maxShow * this.lineContainNum) ? (this.GetPos(index) + this.moveing) : this.GetPos(index));
		}
		else if (!this.IsContainLayoutWidget)
		{
			gameObject.GetComponent<RectTransform>().set_anchoredPosition(this.GetPos(index));
		}
		return gameObject;
	}

	private void SetContentSize(int num)
	{
		if (!this.isAutoCalcalateLayout)
		{
			return;
		}
		RectTransform component = base.GetComponent<RectTransform>();
		if (this.Dir == ListDir.Right || this.Dir == ListDir.Up)
		{
			component.set_sizeDelta(1f * this.GetDir() * (float)this.GetLines(num) * this.spacing);
		}
		else
		{
			component.set_sizeDelta(-1f * this.GetDir() * (float)this.GetLines(num) * this.spacing);
		}
	}

	private int GetLines(int num)
	{
		return Mathf.CeilToInt((float)num / (float)this.lineContainNum);
	}
}
