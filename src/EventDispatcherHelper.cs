using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class EventDispatcherHelper : MonoBehaviour
{
	public Queue<Action> todo_queue = new Queue<Action>();

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.get_gameObject());
	}

	private void Update()
	{
		if (this.todo_queue.get_Count() > 0)
		{
			this.todo_queue.Dequeue().Invoke();
		}
	}

	public void AddTask(Action ac)
	{
		this.todo_queue.Enqueue(ac);
	}
}
