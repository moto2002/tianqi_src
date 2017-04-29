using Foundation.Core.Databinding;
using Package;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class PetTaskUIView : UIBase
{
	public static PetTaskUIView Instance;

	private ListPool mTaskListPool;

	protected override void Preprocessing()
	{
		base.Preprocessing();
		base.hideMainCamera = false;
		this.isInterruptStick = true;
		this.isMask = true;
		this.alpha = 0.7f;
		this.isClick = false;
	}

	private void Awake()
	{
		PetTaskUIView.Instance = this;
		base.AwakeBase(BindingContext.BindingContextMode.MonoBinding, false);
	}

	protected override void InitUI()
	{
		base.InitUI();
		this.mTaskListPool = base.FindTransform("TaskList").GetComponent<ListPool>();
		this.mTaskListPool.SetItem("PetTaskItem");
		base.FindTransform("BtnClose").GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnClickBtnClose));
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		PetTaskManager.Instance.CheckTaskIsAchieve(true);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
	}

	protected override void ReleaseSelf(bool calledDestroy)
	{
		if (SystemConfig.IsReleaseResourceOn)
		{
			PetTaskUIView.Instance = null;
			PetTaskUIViewModel.Instance = null;
			base.ReleaseSelf(true);
		}
	}

	private void OnClickBtnClose()
	{
		this.Show(false);
		UIStackManager.Instance.PopUIPrevious(base.uiType);
	}

	protected override void AddListeners()
	{
		base.AddListeners();
	}

	protected override void RemoveListeners()
	{
		base.RemoveListeners();
	}

	protected override void DataBinding()
	{
		base.DataBinding();
		ListBinder listBinder = base.FindTransform("ButtonSubs").get_gameObject().AddComponent<ListBinder>();
		listBinder.BindingProxy = base.get_gameObject();
		listBinder.SourceBinding.MemberName = "Subs";
		listBinder.PrefabName = "ButtonToggle2GuildStoveSub";
	}

	public void RefreshUI()
	{
		bool isUnPickUp = false;
		if (PetTaskUIViewModel.Instance.CurrentSubIndex == 0)
		{
			isUnPickUp = true;
		}
		List<PetTaskInfo> list = PetTaskManager.Instance.GetPetTaskInfos(isUnPickUp);
		this.mTaskListPool.Create(list.get_Count(), delegate(int index)
		{
			if (index < this.mTaskListPool.Items.get_Count() && index < list.get_Count())
			{
				PetTaskItem component = this.mTaskListPool.Items.get_Item(index).GetComponent<PetTaskItem>();
				component.Refresh(list.get_Item(index));
			}
		});
	}
}
