using System;
using System.Collections.Generic;
using XEngine;

namespace XEngineCommand
{
	public class FrozeFrameCmd : BaseCommand
	{
		public float rate = 1f;

		public int time;

		public Action callback;

		public int count = 1;

		public int interval;

		public List<float> timeRateList;
	}
}
