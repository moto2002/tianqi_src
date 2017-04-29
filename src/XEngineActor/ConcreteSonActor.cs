using Foundation.EF;
using System;

namespace XEngineActor
{
	public class ConcreteSonActor : Actor
	{
		private class ASubsystem : AbstractColleague
		{
			public override void OnCreate()
			{
				Debuger.Info(base.GetType().get_Name() + " OnCreate", new object[0]);
			}

			public override void OnDestroy()
			{
				Debuger.Info(base.GetType().get_Name() + " OnDestroy", new object[0]);
			}
		}

		private class BSubsystem : AbstractColleague
		{
			public override void OnCreate()
			{
				Debuger.Info(base.GetType().get_Name() + " OnCreate", new object[0]);
			}

			public override void OnDestroy()
			{
				Debuger.Info(base.GetType().get_Name() + " OnDestroy", new object[0]);
			}
		}

		protected override void OnCreate()
		{
			base.addColleague(new ConcreteSonActor.ASubsystem());
			base.addColleague(new ConcreteSonActor.BSubsystem());
			base.OnCreate();
		}

		protected override void OnDestroy()
		{
			base.deleteColleague(typeof(ConcreteSonActor.ASubsystem));
			base.deleteColleague(typeof(ConcreteSonActor.BSubsystem));
			base.OnDestroy();
		}
	}
}
