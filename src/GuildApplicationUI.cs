using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GuildApplicationUI : UIBase
{
	private ButtonCustom refuseToAllBtn;

	private void Awake()
	{
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
		base.SetMask(0f, true, true);
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		this.RefreshUI();
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.refuseToAllBtn = base.FindTransform("BtnRefuseAll").GetComponent<ButtonCustom>();
		this.refuseToAllBtn.onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickRefuseBtn);
	}

	protected override void ReleaseSelf(bool destroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			base.ReleaseSelf(true);
		}
	}

	protected override void AddListeners()
	{
		base.AddListeners();
		EventDispatcher.AddListener(EventNames.UpdateGuildApplication, new Callback(this.RefreshUI));
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
		EventDispatcher.RemoveListener(EventNames.UpdateGuildApplication, new Callback(this.RefreshUI));
	}

	private void RefreshUI()
	{
		if (GuildManager.Instance.ApplicationPlayers == null || GuildManager.Instance.ApplicationPlayers.get_Count() <= 0)
		{
			this.Show(false);
			return;
		}
		Transform transform = base.FindTransform("Contair");
		List<ApplicantInfo> sortApplicationPlayers = GuildManager.Instance.SortApplicationPlayers;
		int i;
		for (i = 0; i < sortApplicationPlayers.get_Count(); i++)
		{
			if (transform.get_childCount() > i)
			{
				GameObject gameObject = transform.GetChild(i).get_gameObject();
				if (gameObject != null && gameObject.GetComponent<GuildApplicationItem>() != null)
				{
					gameObject.SetActive(true);
					gameObject.GetComponent<GuildApplicationItem>().RefreshUI(sortApplicationPlayers.get_Item(i));
				}
			}
			else
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("GuildApplicationItem");
				instantiate2Prefab.set_name("GuildApplicationItem_" + i);
				GuildApplicationItem component = instantiate2Prefab.GetComponent<GuildApplicationItem>();
				instantiate2Prefab.get_transform().SetParent(transform);
				instantiate2Prefab.GetComponent<RectTransform>().set_localScale(Vector3.get_one());
				component.RefreshUI(sortApplicationPlayers.get_Item(i));
			}
		}
		for (int j = i; j < transform.get_childCount(); j++)
		{
			GameObject gameObject2 = transform.GetChild(j).get_gameObject();
			if (gameObject2 != null)
			{
				gameObject2.SetActive(false);
			}
		}
	}

	private void OnClickRefuseBtn(GameObject go)
	{
		List<long> list = new List<long>();
		if (GuildManager.Instance.ApplicationPlayers != null && GuildManager.Instance.ApplicationPlayers.get_Count() > 0)
		{
			for (int i = 0; i < GuildManager.Instance.ApplicationPlayers.get_Count(); i++)
			{
				list.Add(GuildManager.Instance.ApplicationPlayers.get_Item(i).roleId);
			}
			GuildManager.Instance.SendRefuseGuildApplicant(list);
		}
	}
}
