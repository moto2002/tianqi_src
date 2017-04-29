using System;
using XEngine;

namespace XEngineCommand
{
	public class NotifyPropChangedCmd : BaseCommand
	{
		public string propName;

		public float propValue;

		public string propTag;
	}
}
