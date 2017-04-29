using System;
using UnityEngine.UI;

public class SkillButton : ButtonCustom
{
	protected int keyIndex;

	protected override void OnDisable()
	{
		if (this.currentSeletionState == 2)
		{
			XInputManager.Instance.OnSkillBtnUp(this.keyIndex);
		}
		this.currentSeletionState = 0;
		base.OnDisable();
	}

	public void Init(int index)
	{
		this.keyIndex = index;
	}

	protected override void DoStateTransition(Selectable.SelectionState state, bool instant)
	{
		Selectable.SelectionState currentSeletionState = this.currentSeletionState;
		base.DoStateTransition(state, instant);
		if (this.keyIndex != 0)
		{
			if (currentSeletionState == 2 && state != 2)
			{
				XInputManager.Instance.OnSkillBtnUp(this.keyIndex);
			}
			else if (currentSeletionState != 2 && state == 2)
			{
				XInputManager.Instance.OnSkillBtnDown(this.keyIndex, true);
			}
		}
	}
}
