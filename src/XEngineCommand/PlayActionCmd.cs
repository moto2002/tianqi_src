using System;
using XEngine;

namespace XEngineCommand
{
	public class PlayActionCmd : BaseCommand
	{
		public string actName;

		public bool jumpToPlay;

		public float percent;

		public bool isBreak;

		public float tempSpeed;

		public int skillID;

		public int skillComboID;

		public string skillTag;
	}
}
