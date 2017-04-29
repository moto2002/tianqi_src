using System;
using System.Collections.Generic;
using XEngine;
using XEngineActor;

namespace XEngineCommand
{
	public class BulletFXCmd : BaseCommand
	{
		public int fxID;

		public float scale;

		public XPoint point;

		public int bulletLife;

		public Action<Actor, XPoint, ActorParent> collisionCallback;

		public bool useY;

		public List<int> offset;
	}
}
