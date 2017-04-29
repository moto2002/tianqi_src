using System;
using System.Collections.Generic;

public class TimerHeapCustom
{
	public class TimerData
	{
		public int start;

		public Action hander;
	}

	private List<TimerHeapCustom.TimerData> m_list = new List<TimerHeapCustom.TimerData>();

	private List<TimerHeapCustom.TimerData> m_listDelete = new List<TimerHeapCustom.TimerData>();

	public TimerHeapCustom()
	{
		EventDispatcher.AddListener<int>("GuideManager.InstanceOfTime", new Callback<int>(this.OnInstanceOfTime));
		EventDispatcher.AddListener<int, int>(SceneManagerEvent.UnloadScene, new Callback<int, int>(this.OnUnloadScene));
	}

	private void OnInstanceOfTime(int CurrentEscapeTime)
	{
		this.m_listDelete.Clear();
		for (int i = 0; i < this.m_list.get_Count(); i++)
		{
			this.m_list.get_Item(i).start = this.m_list.get_Item(i).start - 1000;
			if (this.m_list.get_Item(i).start <= 0 && this.m_list.get_Item(i).hander != null)
			{
				this.m_list.get_Item(i).hander.Invoke();
				this.m_list.get_Item(i).hander = null;
				this.m_listDelete.Add(this.m_list.get_Item(i));
			}
		}
		for (int j = 0; j < this.m_listDelete.get_Count(); j++)
		{
			this.m_list.Remove(this.m_listDelete.get_Item(j));
		}
		this.m_listDelete.Clear();
	}

	private void OnUnloadScene(int a, int b)
	{
		this.m_list.Clear();
	}

	public void AddTimer(int start, Action handler)
	{
		TimerHeapCustom.TimerData timerData = new TimerHeapCustom.TimerData();
		timerData.start = start;
		timerData.hander = handler;
		this.m_list.Add(timerData);
	}
}
