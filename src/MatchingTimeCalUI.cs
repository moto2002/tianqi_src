using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchingTimeCalUI : UIBase
{
	private float timeCal;

	private int lastCalNum;

	private bool caling;

	public ButtonCustom BtnCancel;

	private List<GameObject> numsObjs = new List<GameObject>();

	public Transform TimeNum;

	public Action actionCancel;

	private List<int> nums = new List<int>();

	private void Start()
	{
		this.BtnCancel.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickBtnCancel);
	}

	private void Update()
	{
		if (this.caling)
		{
			this.timeCal += Time.get_deltaTime();
			if (this.timeCal > (float)this.lastCalNum)
			{
				this.lastCalNum++;
				this.SetTimeImages();
			}
		}
	}

	protected override void OnEnable()
	{
		base.SetAsLastSibling();
	}

	private void OnClickBtnCancel(GameObject sender)
	{
		Debuger.Error("OnClickBtnCancel", new object[0]);
		this.Cancel();
		GangFightManager.Instance.CancelGangFighting();
		if (GangFightManager.Instance.gangFightBattleResult != null && GangFightManager.Instance.gangFightBattleResult.winnerId == EntityWorld.Instance.EntSelf.ID)
		{
			GangFightManager.Instance.SendExitFromGangFightFieldReq();
		}
	}

	public void Cancel()
	{
		if (this.actionCancel != null)
		{
			this.actionCancel.Invoke();
			this.actionCancel = null;
		}
		this.ResetTimeCal();
		this.Show(false);
	}

	private void SetTimeImages()
	{
		for (int i = 0; i < this.numsObjs.get_Count(); i++)
		{
			GameObject gameObject = this.numsObjs.get_Item(i);
			Object.Destroy(gameObject);
		}
		this.numsObjs.Clear();
		int num = (int)this.timeCal;
		this.nums.Clear();
		int num2 = 0;
		while (true)
		{
			this.nums.Add(num % 10);
			if (num < 10)
			{
				break;
			}
			num /= 10;
			num2++;
		}
		for (int j = this.nums.get_Count() - 1; j >= 0; j--)
		{
			GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("MatchingNum");
			ResourceManager.SetSprite(instantiate2Prefab.GetComponent<Image>(), ResourceManager.GetIconSprite("peipeishuzi_" + this.nums.get_Item(j)));
			instantiate2Prefab.get_transform().SetParent(this.TimeNum);
			instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
			Vector3 localPosition = instantiate2Prefab.GetComponent<RectTransform>().get_localPosition();
			localPosition.z = 0f;
			instantiate2Prefab.GetComponent<RectTransform>().set_localPosition(localPosition);
			this.numsObjs.Add(instantiate2Prefab);
		}
	}

	public void ResetTimeCal()
	{
		this.timeCal = 0f;
		this.lastCalNum = 0;
		this.caling = false;
	}

	public void StartCal()
	{
		this.ResetTimeCal();
		this.caling = true;
	}
}
