using System;
using UnityEngine.EventSystems;

namespace XEngine
{
	public abstract class BaseCommand : BaseEventData
	{
		public BaseCommand() : base(CommandCenter.current)
		{
		}
	}
}
