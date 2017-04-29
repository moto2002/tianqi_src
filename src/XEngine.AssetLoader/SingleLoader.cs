using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace XEngine.AssetLoader
{
	public class SingleLoader
	{
		private readonly Func<IEnumerator, Coroutine> StartCoroutine;

		private readonly Queue<IEnumerator> queue;

		private bool isLoading;

		public SingleLoader(Func<IEnumerator, Coroutine> coroutineStarter)
		{
			this.StartCoroutine = coroutineStarter;
			this.queue = new Queue<IEnumerator>();
			this.isLoading = false;
		}

		public bool isBusy()
		{
			return this.isLoading;
		}

		public void Enqueue(IEnumerator coroutine)
		{
			if (!this.isLoading)
			{
				this.StartCoroutine.Invoke(this.execute(coroutine));
			}
			else
			{
				this.queue.Enqueue(coroutine);
			}
		}

		[DebuggerHidden]
		private IEnumerator execute(IEnumerator coroutine)
		{
			SingleLoader.<execute>c__Iterator22 <execute>c__Iterator = new SingleLoader.<execute>c__Iterator22();
			<execute>c__Iterator.coroutine = coroutine;
			<execute>c__Iterator.<$>coroutine = coroutine;
			<execute>c__Iterator.<>f__this = this;
			return <execute>c__Iterator;
		}
	}
}
