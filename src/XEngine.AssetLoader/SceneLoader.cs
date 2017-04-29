using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace XEngine.AssetLoader
{
	internal class SceneLoader
	{
		private SingleLoader coroutineQueue;

		private bool isLoading;

		private Action<bool> m_currentCallback;

		public void AsyncLoadScene(string resName, Action<bool> callback, bool isAdditive)
		{
			if (this.coroutineQueue == null)
			{
				this.coroutineQueue = new SingleLoader(new Func<IEnumerator, Coroutine>(GameManager.Instance.StartCoroutine));
			}
			this.m_currentCallback = callback;
			this.coroutineQueue.Enqueue(this.AsyncOnDoing(SceneManager.LoadSceneAsync(Path.GetFileNameWithoutExtension(resName), (!isAdditive) ? 0 : 1)));
		}

		[DebuggerHidden]
		private IEnumerator AsyncOnDoing(AsyncOperation async)
		{
			SceneLoader.<AsyncOnDoing>c__Iterator21 <AsyncOnDoing>c__Iterator = new SceneLoader.<AsyncOnDoing>c__Iterator21();
			<AsyncOnDoing>c__Iterator.async = async;
			<AsyncOnDoing>c__Iterator.<$>async = async;
			<AsyncOnDoing>c__Iterator.<>f__this = this;
			return <AsyncOnDoing>c__Iterator;
		}

		private bool SyncLoadScene(string resName)
		{
			SceneManager.LoadScene(Path.GetFileNameWithoutExtension(resName), 0);
			return true;
		}

		public void UnloadScene(string resName, bool isAdditive)
		{
			if (isAdditive)
			{
				SceneManager.UnloadScene(Path.GetFileNameWithoutExtension(resName));
			}
		}
	}
}
