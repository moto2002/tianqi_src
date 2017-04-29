using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

namespace Foundation
{
	[AddComponentMenu("Foundation/Core/TaskManager"), ExecuteInEditMode]
	public class TaskManager : MonoBehaviour
	{
		public struct TaskLog
		{
			public LogType Type;

			public object Message;
		}

		public struct CoroutineInfo
		{
			public IEnumerator Coroutine;

			public Action OnComplete;
		}

		private static TaskManager _instance;

		private static object syncRoot = new object();

		protected static readonly List<TaskManager.CoroutineInfo> PendingCoroutineInfo = new List<TaskManager.CoroutineInfo>();

		protected static readonly List<IEnumerator> PendingAdd = new List<IEnumerator>();

		protected static readonly List<IEnumerator> PendingRemove = new List<IEnumerator>();

		protected static readonly List<Action> PendingActions = new List<Action>();

		protected static readonly List<TaskManager.TaskLog> PendingLogs = new List<TaskManager.TaskLog>();

		public static bool IsMainThread
		{
			get
			{
				return !Thread.get_CurrentThread().get_IsBackground() && !Thread.get_CurrentThread().get_IsThreadPoolThread();
			}
		}

		public static TaskManager Instance
		{
			get
			{
				TaskManager.ConfirmInit();
				return TaskManager._instance;
			}
		}

		public static void ConfirmInit()
		{
			if (TaskManager._instance == null)
			{
				TaskManager[] array = Object.FindObjectsOfType<TaskManager>();
				TaskManager[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					TaskManager taskManager = array2[i];
					if (Application.get_isEditor())
					{
						Object.DestroyImmediate(taskManager.get_gameObject());
					}
					else
					{
						Object.Destroy(taskManager.get_gameObject());
					}
				}
				GameObject gameObject = new GameObject("_TaskManager");
				Object.DontDestroyOnLoad(gameObject);
				TaskManager._instance = gameObject.AddComponent<TaskManager>();
			}
		}

		public static Coroutine StartRoutine(IEnumerator coroutine)
		{
			if (!TaskManager.IsMainThread)
			{
				object obj = TaskManager.syncRoot;
				lock (obj)
				{
					TaskManager.PendingAdd.Add(coroutine);
					return null;
				}
			}
			return TaskManager.Instance.StartCoroutine(coroutine);
		}

		public static void StartRoutine(TaskManager.CoroutineInfo info)
		{
			if (!TaskManager.IsMainThread)
			{
				object obj = TaskManager.syncRoot;
				lock (obj)
				{
					TaskManager.PendingCoroutineInfo.Add(info);
				}
			}
			else
			{
				TaskManager.Instance.StartCoroutine(TaskManager.Instance.RunCoroutineInfo(info));
			}
		}

		public static void StopRoutine(IEnumerator coroutine)
		{
			if (!TaskManager.IsMainThread)
			{
				object obj = TaskManager.syncRoot;
				lock (obj)
				{
					TaskManager.PendingRemove.Add(coroutine);
				}
			}
			else
			{
				TaskManager.Instance.StartCoroutine(coroutine);
			}
		}

		public static void RunOnMainThread(Action action)
		{
			if (!TaskManager.IsMainThread)
			{
				object obj = TaskManager.syncRoot;
				lock (obj)
				{
					TaskManager.PendingActions.Add(action);
				}
			}
			else
			{
				action.Invoke();
			}
		}

		public static void Log(TaskManager.TaskLog m)
		{
			if (!TaskManager.IsMainThread)
			{
				object obj = TaskManager.syncRoot;
				lock (obj)
				{
					TaskManager.PendingLogs.Add(m);
				}
			}
			else
			{
				TaskManager.Write(m);
			}
		}

		private static void Write(TaskManager.TaskLog m)
		{
			switch (m.Type)
			{
			case 0:
			case 4:
				Debug.LogError(m.Message);
				break;
			case 1:
			case 3:
				Debug.Log(m.Message);
				break;
			case 2:
				Debug.LogWarning(m.Message);
				break;
			}
		}

		protected void Awake()
		{
			if (TaskManager._instance == null)
			{
				TaskManager._instance = this;
			}
		}

		protected void Update()
		{
			if (TaskManager.PendingAdd.get_Count() == 0 && TaskManager.PendingRemove.get_Count() == 0 && TaskManager.PendingActions.get_Count() == 0 && TaskManager.PendingLogs.get_Count() == 0 && TaskManager.PendingCoroutineInfo.get_Count() == 0)
			{
				return;
			}
			object obj = TaskManager.syncRoot;
			lock (obj)
			{
				for (int i = 0; i < TaskManager.PendingLogs.get_Count(); i++)
				{
					TaskManager.Write(TaskManager.PendingLogs.get_Item(i));
				}
				for (int j = 0; j < TaskManager.PendingAdd.get_Count(); j++)
				{
					base.StartCoroutine(TaskManager.PendingAdd.get_Item(j));
				}
				for (int k = 0; k < TaskManager.PendingRemove.get_Count(); k++)
				{
					base.StopCoroutine(TaskManager.PendingRemove.get_Item(k));
				}
				for (int l = 0; l < TaskManager.PendingCoroutineInfo.get_Count(); l++)
				{
					base.StartCoroutine(this.RunCoroutineInfo(TaskManager.PendingCoroutineInfo.get_Item(l)));
				}
				for (int m = 0; m < TaskManager.PendingActions.get_Count(); m++)
				{
					TaskManager.PendingActions.get_Item(m).Invoke();
				}
				TaskManager.PendingAdd.Clear();
				TaskManager.PendingRemove.Clear();
				TaskManager.PendingActions.Clear();
				TaskManager.PendingLogs.Clear();
				TaskManager.PendingCoroutineInfo.Clear();
			}
		}

		[DebuggerHidden]
		private IEnumerator RunCoroutineInfo(TaskManager.CoroutineInfo info)
		{
			TaskManager.<RunCoroutineInfo>c__IteratorD <RunCoroutineInfo>c__IteratorD = new TaskManager.<RunCoroutineInfo>c__IteratorD();
			<RunCoroutineInfo>c__IteratorD.info = info;
			<RunCoroutineInfo>c__IteratorD.<$>info = info;
			<RunCoroutineInfo>c__IteratorD.<>f__this = this;
			return <RunCoroutineInfo>c__IteratorD;
		}
	}
}
