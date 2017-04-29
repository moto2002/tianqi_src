using System;

public class TimerData : AbsTimerData
{
	private Action m_action;

	public override Delegate Action
	{
		get
		{
			return this.m_action;
		}
		set
		{
			this.m_action = (value as Action);
		}
	}

	public override void DoAction()
	{
		this.m_action.Invoke();
	}
}
public class TimerData<T> : AbsTimerData
{
	private Action<T> m_action;

	private T m_arg1;

	public override Delegate Action
	{
		get
		{
			return this.m_action;
		}
		set
		{
			this.m_action = (value as Action<T>);
		}
	}

	public T Arg1
	{
		get
		{
			return this.m_arg1;
		}
		set
		{
			this.m_arg1 = value;
		}
	}

	public override void DoAction()
	{
		this.m_action.Invoke(this.m_arg1);
	}
}
public class TimerData<T, U> : AbsTimerData
{
	private Action<T, U> m_action;

	private T m_arg1;

	private U m_arg2;

	public override Delegate Action
	{
		get
		{
			return this.m_action;
		}
		set
		{
			this.m_action = (value as Action<T, U>);
		}
	}

	public T Arg1
	{
		get
		{
			return this.m_arg1;
		}
		set
		{
			this.m_arg1 = value;
		}
	}

	public U Arg2
	{
		get
		{
			return this.m_arg2;
		}
		set
		{
			this.m_arg2 = value;
		}
	}

	public override void DoAction()
	{
		this.m_action.Invoke(this.m_arg1, this.m_arg2);
	}
}
public class TimerData<T, U, V> : AbsTimerData
{
	private Action<T, U, V> m_action;

	private T m_arg1;

	private U m_arg2;

	private V m_arg3;

	public override Delegate Action
	{
		get
		{
			return this.m_action;
		}
		set
		{
			this.m_action = (value as Action<T, U, V>);
		}
	}

	public T Arg1
	{
		get
		{
			return this.m_arg1;
		}
		set
		{
			this.m_arg1 = value;
		}
	}

	public U Arg2
	{
		get
		{
			return this.m_arg2;
		}
		set
		{
			this.m_arg2 = value;
		}
	}

	public V Arg3
	{
		get
		{
			return this.m_arg3;
		}
		set
		{
			this.m_arg3 = value;
		}
	}

	public override void DoAction()
	{
		this.m_action.Invoke(this.m_arg1, this.m_arg2, this.m_arg3);
	}
}
