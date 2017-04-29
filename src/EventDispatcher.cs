using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventDispatcher
{
	public class BroadcastException : Exception
	{
		public BroadcastException(string msg) : base(msg)
		{
		}
	}

	public class ListenerException : Exception
	{
		public ListenerException(string msg) : base(msg)
		{
		}
	}

	private static EventDispatcherHelper messengerHelper = new GameObject("MessengerHelper").AddComponent<EventDispatcherHelper>();

	public static Dictionary<string, Delegate> eventTable = new Dictionary<string, Delegate>();

	public static List<string> permanentMessages = new List<string>();

	public static void MarkAsPermanent(string eventType)
	{
		EventDispatcher.permanentMessages.Add(eventType);
	}

	public static void Cleanup()
	{
		List<string> list = new List<string>();
		using (Dictionary<string, Delegate>.Enumerator enumerator = EventDispatcher.eventTable.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, Delegate> current = enumerator.get_Current();
				bool flag = false;
				for (int i = 0; i < EventDispatcher.permanentMessages.get_Count(); i++)
				{
					if (current.get_Key() == EventDispatcher.permanentMessages.get_Item(i))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					list.Add(current.get_Key());
				}
			}
		}
		for (int j = 0; j < list.get_Count(); j++)
		{
			EventDispatcher.eventTable.Remove(list.get_Item(j));
		}
	}

	public static void PrintEventTable()
	{
		Debug.Log("\t\t\t=== MESSENGER PrintEventTable ===");
		using (Dictionary<string, Delegate>.Enumerator enumerator = EventDispatcher.eventTable.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, Delegate> current = enumerator.get_Current();
				Debug.Log("\t\t\t" + current.get_Key() + ("\t\t" + current.get_Value()));
			}
		}
		Debug.Log("\n");
	}

	public static void OnListenerAdding(string eventType, Delegate listenerBeingAdded)
	{
		if (!EventDispatcher.eventTable.ContainsKey(eventType))
		{
			return;
		}
		Delegate @delegate = EventDispatcher.eventTable.get_Item(eventType);
		if (@delegate != null && @delegate.GetType() != listenerBeingAdded.GetType())
		{
			throw new EventDispatcher.ListenerException(string.Format("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", eventType, @delegate.GetType().get_Name(), listenerBeingAdded.GetType().get_Name()));
		}
	}

	public static void OnListenerRemoving(string eventType, Delegate listenerBeingRemoved)
	{
		if (!EventDispatcher.eventTable.ContainsKey(eventType))
		{
			throw new EventDispatcher.ListenerException(string.Format("Attempting to remove listener for type \"{0}\" but Messenger doesn't know about this event type.", eventType));
		}
		Delegate @delegate = EventDispatcher.eventTable.get_Item(eventType);
		if (@delegate == null)
		{
			throw new EventDispatcher.ListenerException(string.Format("Attempting to remove listener with for event type \"{0}\" but current listener is null.", eventType));
		}
		if (@delegate.GetType() != listenerBeingRemoved.GetType())
		{
			throw new EventDispatcher.ListenerException(string.Format("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", eventType, @delegate.GetType().get_Name(), listenerBeingRemoved.GetType().get_Name()));
		}
	}

	public static void OnListenerRemoved(string eventType)
	{
		if (EventDispatcher.eventTable.get_Item(eventType) == null)
		{
			EventDispatcher.eventTable.Remove(eventType);
		}
	}

	public static void OnBroadcasting(string eventType)
	{
	}

	public static EventDispatcher.BroadcastException CreateBroadcastSignatureException(string eventType)
	{
		return new EventDispatcher.BroadcastException(string.Format("Broadcasting message \"{0}\" but listeners have a different signature than the broadcaster.", eventType));
	}

	public static void AddListener(string eventType, Callback handler)
	{
		EventDispatcher.OnListenerAdding(eventType, handler);
		if (!EventDispatcher.eventTable.ContainsKey(eventType))
		{
			EventDispatcher.eventTable.Add(eventType, handler);
		}
		else
		{
			EventDispatcher.eventTable.set_Item(eventType, (Callback)Delegate.Combine((Callback)EventDispatcher.eventTable.get_Item(eventType), handler));
		}
	}

	public static void AddListener<T>(string eventType, Callback<T> handler)
	{
		EventDispatcher.OnListenerAdding(eventType, handler);
		if (!EventDispatcher.eventTable.ContainsKey(eventType))
		{
			EventDispatcher.eventTable.Add(eventType, handler);
		}
		else
		{
			EventDispatcher.eventTable.set_Item(eventType, (Callback<T>)Delegate.Combine((Callback<T>)EventDispatcher.eventTable.get_Item(eventType), handler));
		}
	}

	public static void AddListener<T, U>(string eventType, Callback<T, U> handler)
	{
		EventDispatcher.OnListenerAdding(eventType, handler);
		if (!EventDispatcher.eventTable.ContainsKey(eventType))
		{
			EventDispatcher.eventTable.Add(eventType, handler);
		}
		else
		{
			EventDispatcher.eventTable.set_Item(eventType, (Callback<T, U>)Delegate.Combine((Callback<T, U>)EventDispatcher.eventTable.get_Item(eventType), handler));
		}
	}

	public static void AddListener<T, U, V>(string eventType, Callback<T, U, V> handler)
	{
		EventDispatcher.OnListenerAdding(eventType, handler);
		if (!EventDispatcher.eventTable.ContainsKey(eventType))
		{
			EventDispatcher.eventTable.Add(eventType, handler);
		}
		else
		{
			EventDispatcher.eventTable.set_Item(eventType, (Callback<T, U, V>)Delegate.Combine((Callback<T, U, V>)EventDispatcher.eventTable.get_Item(eventType), handler));
		}
	}

	public static void AddListener<T, U, V, W>(string eventType, Callback<T, U, V, W> handler)
	{
		EventDispatcher.OnListenerAdding(eventType, handler);
		if (!EventDispatcher.eventTable.ContainsKey(eventType))
		{
			EventDispatcher.eventTable.Add(eventType, handler);
		}
		else
		{
			EventDispatcher.eventTable.set_Item(eventType, (Callback<T, U, V, W>)Delegate.Combine((Callback<T, U, V, W>)EventDispatcher.eventTable.get_Item(eventType), handler));
		}
	}

	public static void AddListener<T, U, V, W, X>(string eventType, Callback<T, U, V, W, X> handler)
	{
		EventDispatcher.OnListenerAdding(eventType, handler);
		if (!EventDispatcher.eventTable.ContainsKey(eventType))
		{
			EventDispatcher.eventTable.Add(eventType, handler);
		}
		else
		{
			EventDispatcher.eventTable.set_Item(eventType, (Callback<T, U, V, W, X>)Delegate.Combine((Callback<T, U, V, W, X>)EventDispatcher.eventTable.get_Item(eventType), handler));
		}
	}

	public static void RemoveListener(string eventType, Callback handler)
	{
		EventDispatcher.OnListenerRemoving(eventType, handler);
		EventDispatcher.eventTable.set_Item(eventType, (Callback)Delegate.Remove((Callback)EventDispatcher.eventTable.get_Item(eventType), handler));
		EventDispatcher.OnListenerRemoved(eventType);
	}

	public static void RemoveListener<T>(string eventType, Callback<T> handler)
	{
		EventDispatcher.OnListenerRemoving(eventType, handler);
		EventDispatcher.eventTable.set_Item(eventType, (Callback<T>)Delegate.Remove((Callback<T>)EventDispatcher.eventTable.get_Item(eventType), handler));
		EventDispatcher.OnListenerRemoved(eventType);
	}

	public static void RemoveListener<T, U>(string eventType, Callback<T, U> handler)
	{
		EventDispatcher.OnListenerRemoving(eventType, handler);
		EventDispatcher.eventTable.set_Item(eventType, (Callback<T, U>)Delegate.Remove((Callback<T, U>)EventDispatcher.eventTable.get_Item(eventType), handler));
		EventDispatcher.OnListenerRemoved(eventType);
	}

	public static void RemoveListener<T, U, V>(string eventType, Callback<T, U, V> handler)
	{
		EventDispatcher.OnListenerRemoving(eventType, handler);
		EventDispatcher.eventTable.set_Item(eventType, (Callback<T, U, V>)Delegate.Remove((Callback<T, U, V>)EventDispatcher.eventTable.get_Item(eventType), handler));
		EventDispatcher.OnListenerRemoved(eventType);
	}

	public static void RemoveListener<T, U, V, W>(string eventType, Callback<T, U, V, W> handler)
	{
		EventDispatcher.OnListenerRemoving(eventType, handler);
		EventDispatcher.eventTable.set_Item(eventType, (Callback<T, U, V, W>)Delegate.Remove((Callback<T, U, V, W>)EventDispatcher.eventTable.get_Item(eventType), handler));
		EventDispatcher.OnListenerRemoved(eventType);
	}

	public static void Broadcast(string eventType)
	{
		EventDispatcher.OnBroadcasting(eventType);
		Action action = delegate
		{
			Delegate @delegate;
			if (EventDispatcher.eventTable.TryGetValue(eventType, ref @delegate))
			{
				Callback callback = @delegate as Callback;
				if (callback == null)
				{
					throw EventDispatcher.CreateBroadcastSignatureException(eventType);
				}
				callback();
			}
		};
		action.Invoke();
	}

	public static void Broadcast<T>(string eventType, T arg1)
	{
		EventDispatcher.OnBroadcasting(eventType);
		Action action = delegate
		{
			Delegate @delegate;
			if (EventDispatcher.eventTable.TryGetValue(eventType, ref @delegate))
			{
				Callback<T> callback = @delegate as Callback<T>;
				if (callback == null)
				{
					throw EventDispatcher.CreateBroadcastSignatureException(eventType);
				}
				callback(arg1);
			}
		};
		action.Invoke();
	}

	public static void Broadcast<T, U>(string eventType, T arg1, U arg2)
	{
		EventDispatcher.OnBroadcasting(eventType);
		Action action = delegate
		{
			Delegate @delegate;
			if (EventDispatcher.eventTable.TryGetValue(eventType, ref @delegate))
			{
				Callback<T, U> callback = @delegate as Callback<T, U>;
				if (callback == null)
				{
					throw EventDispatcher.CreateBroadcastSignatureException(eventType);
				}
				callback(arg1, arg2);
			}
		};
		action.Invoke();
	}

	public static void Broadcast<T, U, V>(string eventType, T arg1, U arg2, V arg3)
	{
		EventDispatcher.OnBroadcasting(eventType);
		Action action = delegate
		{
			Delegate @delegate;
			if (EventDispatcher.eventTable.TryGetValue(eventType, ref @delegate))
			{
				Callback<T, U, V> callback = @delegate as Callback<T, U, V>;
				if (callback == null)
				{
					throw EventDispatcher.CreateBroadcastSignatureException(eventType);
				}
				callback(arg1, arg2, arg3);
			}
		};
		action.Invoke();
	}

	public static void Broadcast<T, U, V, W>(string eventType, T arg1, U arg2, V arg3, W arg4)
	{
		EventDispatcher.OnBroadcasting(eventType);
		Action action = delegate
		{
			Delegate @delegate;
			if (EventDispatcher.eventTable.TryGetValue(eventType, ref @delegate))
			{
				Callback<T, U, V, W> callback = @delegate as Callback<T, U, V, W>;
				if (callback == null)
				{
					throw EventDispatcher.CreateBroadcastSignatureException(eventType);
				}
				callback(arg1, arg2, arg3, arg4);
			}
		};
		action.Invoke();
	}

	public static void BroadcastAsync(string eventType)
	{
		EventDispatcher.OnBroadcasting(eventType);
		Action ac = delegate
		{
			Delegate @delegate;
			if (EventDispatcher.eventTable.TryGetValue(eventType, ref @delegate))
			{
				Callback callback = @delegate as Callback;
				if (callback == null)
				{
					throw EventDispatcher.CreateBroadcastSignatureException(eventType);
				}
				callback();
			}
		};
		EventDispatcher.messengerHelper.GetComponent<EventDispatcherHelper>().AddTask(ac);
	}

	public static void BroadcastAsync<T>(string eventType, T arg1)
	{
		EventDispatcher.OnBroadcasting(eventType);
		Action ac = delegate
		{
			Delegate @delegate;
			if (EventDispatcher.eventTable.TryGetValue(eventType, ref @delegate))
			{
				Callback<T> callback = @delegate as Callback<T>;
				if (callback == null)
				{
					throw EventDispatcher.CreateBroadcastSignatureException(eventType);
				}
				callback(arg1);
			}
		};
		EventDispatcher.messengerHelper.GetComponent<EventDispatcherHelper>().AddTask(ac);
	}

	public static void BroadcastAsync<T, U>(string eventType, T arg1, U arg2)
	{
		EventDispatcher.OnBroadcasting(eventType);
		Action ac = delegate
		{
			Delegate @delegate;
			if (EventDispatcher.eventTable.TryGetValue(eventType, ref @delegate))
			{
				Callback<T, U> callback = @delegate as Callback<T, U>;
				if (callback == null)
				{
					throw EventDispatcher.CreateBroadcastSignatureException(eventType);
				}
				callback(arg1, arg2);
			}
		};
		EventDispatcher.messengerHelper.GetComponent<EventDispatcherHelper>().AddTask(ac);
	}

	public static void BroadcastAsync<T, U, V>(string eventType, T arg1, U arg2, V arg3)
	{
		EventDispatcher.OnBroadcasting(eventType);
		Action ac = delegate
		{
			Delegate @delegate;
			if (EventDispatcher.eventTable.TryGetValue(eventType, ref @delegate))
			{
				Callback<T, U, V> callback = @delegate as Callback<T, U, V>;
				if (callback == null)
				{
					throw EventDispatcher.CreateBroadcastSignatureException(eventType);
				}
				callback(arg1, arg2, arg3);
			}
		};
		EventDispatcher.messengerHelper.GetComponent<EventDispatcherHelper>().AddTask(ac);
	}
}
