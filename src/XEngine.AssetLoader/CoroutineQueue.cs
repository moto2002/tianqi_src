using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace XEngine.AssetLoader
{
	public class CoroutineQueue
	{
		private readonly uint maxActive;

		private readonly Func<IEnumerator, Coroutine> coroutineStarter;

		private readonly Queue<IEnumerator> queue;

		private uint numActive;

		public CoroutineQueue(uint maxActive, Func<IEnumerator, Coroutine> coroutineStarter)
		{
			if (maxActive == 0u)
			{
				throw new ArgumentException("Must be at least one", "maxActive");
			}
			this.maxActive = maxActive;
			this.coroutineStarter = coroutineStarter;
			this.queue = new Queue<IEnumerator>();
		}

		public void Run(IEnumerator coroutine)
		{
			if (this.numActive < this.maxActive)
			{
				IEnumerator enumerator = this.CoroutineRunner(coroutine);
				this.coroutineStarter.Invoke(enumerator);
			}
			else
			{
				this.queue.Enqueue(coroutine);
			}
		}

		[DebuggerHidden]
		private IEnumerator CoroutineRunner(IEnumerator coroutine)
		{
			CoroutineQueue.<CoroutineRunner>c__Iterator1F <CoroutineRunner>c__Iterator1F = new CoroutineQueue.<CoroutineRunner>c__Iterator1F();
			<CoroutineRunner>c__Iterator1F.coroutine = coroutine;
			<CoroutineRunner>c__Iterator1F.<$>coroutine = coroutine;
			<CoroutineRunner>c__Iterator1F.<>f__this = this;
			return <CoroutineRunner>c__Iterator1F;
		}
	}
}
