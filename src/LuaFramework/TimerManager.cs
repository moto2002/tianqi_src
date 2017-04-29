using System;
using System.Collections.Generic;

namespace LuaFramework
{
	public class TimerManager : Manager
	{
		private float interval;

		private List<TimerInfo> objects = new List<TimerInfo>();

		public float Interval
		{
			get
			{
				return this.interval;
			}
			set
			{
				this.interval = value;
			}
		}

		private void Start()
		{
			this.StartTimer(1f);
		}

		public void StartTimer(float value)
		{
			this.interval = value;
			base.InvokeRepeating("Run", 0f, this.interval);
		}

		public void StopTimer()
		{
			base.CancelInvoke("Run");
		}

		public void AddTimerEvent(TimerInfo info)
		{
			if (!this.objects.Contains(info))
			{
				this.objects.Add(info);
			}
		}

		public void RemoveTimerEvent(TimerInfo info)
		{
			if (this.objects.Contains(info) && info != null)
			{
				info.delete = true;
			}
		}

		public void StopTimerEvent(TimerInfo info)
		{
			if (this.objects.Contains(info) && info != null)
			{
				info.stop = true;
			}
		}

		public void ResumeTimerEvent(TimerInfo info)
		{
			if (this.objects.Contains(info) && info != null)
			{
				info.delete = false;
			}
		}

		private void Run()
		{
			if (this.objects.get_Count() == 0)
			{
				return;
			}
			for (int i = 0; i < this.objects.get_Count(); i++)
			{
				TimerInfo timerInfo = this.objects.get_Item(i);
				if (!timerInfo.delete && !timerInfo.stop)
				{
					ITimerBehaviour timerBehaviour = timerInfo.target as ITimerBehaviour;
					timerBehaviour.TimerUpdate();
					timerInfo.tick += 1L;
				}
			}
			for (int j = this.objects.get_Count() - 1; j >= 0; j--)
			{
				if (this.objects.get_Item(j).delete)
				{
					this.objects.Remove(this.objects.get_Item(j));
				}
			}
		}
	}
}
