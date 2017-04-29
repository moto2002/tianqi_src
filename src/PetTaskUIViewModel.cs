using Foundation.Core;
using System;

public class PetTaskUIViewModel : ViewModelBase
{
	public class Names
	{
		public const string Subs = "Subs";
	}

	public const int INDEX_TO_TASK01 = 0;

	public const int INDEX_TO_TASK02 = 1;

	public static PetTaskUIViewModel Instance;

	public ObservableCollection<OOButtonToggle2SubUI> Subs = new ObservableCollection<OOButtonToggle2SubUI>();

	public int CurrentSubIndex;

	protected override void Awake()
	{
		PetTaskUIViewModel.Instance = this;
		base.Awake();
		this.SetSubs();
	}

	private void OnEnable()
	{
		PetTaskUIView.Instance.RefreshUI();
	}

	private void OnDisable()
	{
	}

	private void SetSubs()
	{
		this.Subs.Clear();
		this.Subs.Add(this.GetSub(0, "可用任务", true));
		this.Subs.Add(this.GetSub(1, "进行中", false));
	}

	private OOButtonToggle2SubUI GetSub(int index, string name, bool toggleOn)
	{
		OOButtonToggle2SubUI oOButtonToggle2SubUI = new OOButtonToggle2SubUI();
		oOButtonToggle2SubUI.ToggleIndex = index;
		oOButtonToggle2SubUI.Action2CallBack = new Action<int>(this.OnSubClick);
		oOButtonToggle2SubUI.Name = name;
		oOButtonToggle2SubUI.IsTip = false;
		oOButtonToggle2SubUI.SetIsToggleOn(toggleOn);
		return oOButtonToggle2SubUI;
	}

	private void OnSubClick(int index)
	{
		for (int i = 0; i < this.Subs.Count; i++)
		{
			this.Subs[i].SetIsToggleOn(i == index);
		}
		if (this.CurrentSubIndex != index)
		{
			this.CurrentSubIndex = index;
			PetTaskUIView.Instance.RefreshUI();
		}
	}
}
