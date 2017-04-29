using System;
using System.Collections.Generic;
using UnityEngine;
using XEngine;

namespace XEngineCommand
{
	public class HitFXCmd : BaseCommand
	{
		public int fxID;

		public float scale;

		public Transform caster;

		public List<int> offsets;
	}
}
