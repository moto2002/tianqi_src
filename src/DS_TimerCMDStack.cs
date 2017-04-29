using System;
using System.Collections.Generic;

internal class DS_TimerCMDStack
{
	private uint m_checkPointTimer;

	private Action m_defaultFunc;

	private Stack<FuncWithEndTime> commands = new Stack<FuncWithEndTime>();

	public DS_TimerCMDStack(Action _defaultFunc)
	{
		this.m_defaultFunc = _defaultFunc;
	}

	public void Push(FuncWithEndTime timer)
	{
		this.commands.Push(timer);
		this.execute(timer);
	}

	public void Pop()
	{
		this.commands.Pop();
		while (this.commands.get_Count() > 0)
		{
			FuncWithEndTime timer = this.commands.Pop();
			if (timer.endTime > DateTime.get_Now())
			{
				this.Push(timer);
				return;
			}
		}
		this.m_defaultFunc.Invoke();
	}

	private void execute(FuncWithEndTime timer)
	{
		TimerHeap.DelTimer(this.m_checkPointTimer);
		timer.doFunc.Invoke();
		this.m_checkPointTimer = TimerHeap.AddTimer((uint)(1000.0 * (timer.endTime - DateTime.get_Now()).get_TotalSeconds()), 0, new Action(this.Pop));
	}
}
