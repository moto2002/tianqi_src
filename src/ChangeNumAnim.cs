using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNumAnim : MonoBehaviour
{
	public enum AnimType
	{
		Normal,
		Exp,
		Time
	}

	private const int TIMES_BEFORE = 1;

	private const int TIMES_ROLLING = 35;

	private const int TIMES_AFTER = 1;

	private Text m_lblNum;

	private long m_iDstNum;

	private long m_iSrcNum;

	private int m_iDstNumLength;

	private long m_iDelta;

	private ChangeNumAnim.AnimType m_type;

	private bool m_beforeRolling;

	private bool m_needRolling;

	private bool m_afterRolling;

	private int Frame_Times = 1;

	private int m_frametimes;

	private Action m_actionBeforeFinished;

	private Action<string> m_actionRollingChange;

	private Action m_actionAfterFinished;

	public void ShowChangeNumAnim(ChangeNumAnim.AnimType type, Text text, long srcNum, long dstNum, Action actionBeforeFinished = null, Action<string> actionRolling = null, Action actionAfterFinished = null)
	{
		this.m_actionBeforeFinished = actionBeforeFinished;
		this.m_actionRollingChange = actionRolling;
		this.m_actionAfterFinished = actionAfterFinished;
		this.m_type = type;
		this.m_lblNum = text;
		this.m_iSrcNum = ((srcNum >= dstNum) ? dstNum : srcNum);
		this.m_iDstNum = dstNum;
		this.SetText();
		this.m_iDstNumLength = dstNum.ToString().get_Length();
		if (dstNum > 0L && dstNum > srcNum)
		{
			long num = dstNum - srcNum;
			if (num < 35L)
			{
				this.Frame_Times = 2;
			}
			else
			{
				this.Frame_Times = 1;
			}
			this.m_iDelta = num / 35L;
			if (this.m_iDelta < 1L)
			{
				this.m_iDelta = 1L;
			}
			this.SetRolling(true);
		}
		else
		{
			this.DoActionAfterFinished();
		}
	}

	public void SetRolling(bool rolling)
	{
		this.m_needRolling = rolling;
		this.m_beforeRolling = rolling;
		this.m_afterRolling = rolling;
	}

	private void Update()
	{
		if (this.m_beforeRolling)
		{
			this.m_frametimes++;
			if (this.m_frametimes >= 1)
			{
				this.m_beforeRolling = false;
				this.m_frametimes = 0;
				this.DoActionBeforeFinished();
			}
		}
		else if (this.m_needRolling)
		{
			this.m_frametimes++;
			if (this.m_frametimes < this.Frame_Times)
			{
				return;
			}
			this.m_frametimes = 0;
			this.m_iSrcNum += this.m_iDelta;
			if (this.m_iSrcNum >= this.m_iDstNum)
			{
				this.m_iSrcNum = this.m_iDstNum;
				this.m_needRolling = false;
			}
			this.SetText();
		}
		else if (this.m_afterRolling)
		{
			this.m_frametimes++;
			if (this.m_frametimes >= 1)
			{
				this.m_afterRolling = false;
				this.m_frametimes = 0;
				this.DoActionAfterFinished();
			}
		}
	}

	private void SetText()
	{
		string text = string.Empty;
		switch (this.m_type)
		{
		case ChangeNumAnim.AnimType.Normal:
			text = this.m_iSrcNum.ToString();
			break;
		case ChangeNumAnim.AnimType.Exp:
		{
			string text2 = this.m_iSrcNum.ToString();
			text = "+" + text2;
			break;
		}
		case ChangeNumAnim.AnimType.Time:
			text = this.m_iSrcNum.ToString("d" + this.m_iDstNumLength);
			break;
		}
		this.DoActionChanged(text);
	}

	private void DoActionBeforeFinished()
	{
		if (this.m_actionBeforeFinished != null)
		{
			this.m_actionBeforeFinished.Invoke();
			this.m_actionBeforeFinished = null;
		}
	}

	private void DoActionChanged(string text)
	{
		if (this.m_actionRollingChange != null)
		{
			this.m_actionRollingChange.Invoke(text);
		}
		else
		{
			this.m_lblNum.set_text(text);
		}
	}

	private void DoActionAfterFinished()
	{
		if (this.m_actionAfterFinished != null)
		{
			this.m_actionAfterFinished.Invoke();
			this.m_actionAfterFinished = null;
		}
	}
}
