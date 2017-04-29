using System;
using System.Collections.Generic;
using XEngine;

namespace XEngineCommand
{
	public class PlayBuffFXCmd : BaseCommand
	{
		public List<int> fxID;

		public float scale;

		public int buffID;

		public int time;
	}
}
