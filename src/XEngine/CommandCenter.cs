using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using XEngineActor;

namespace XEngine
{
	public static class CommandCenter
	{
		public static EventSystem current = XEngineCore.AddPermanentGameObject<EventSystem>("CommandCenter");

		public static XDict<Type, MethodInfo[]> map_type_methodinfos = new XDict<Type, MethodInfo[]>();

		public static XDict<Type, MethodInfo> map_class_methodinfo = new XDict<Type, MethodInfo>();

		public static void ExecuteCommand(Transform t, BaseEventData data)
		{
			ExecuteEvents.Execute<ICommandReceiver>(t.get_gameObject(), data, new ExecuteEvents.EventFunction<ICommandReceiver>(CommandCenter.EventFunction));
		}

		public static void EventFunction(ICommandReceiver handler, BaseEventData eventData)
		{
			Type type = eventData.GetType();
			if (!CommandCenter.map_class_methodinfo.ContainsKey(type))
			{
				CommandCenter.map_class_methodinfo[type] = typeof(Actor).GetFirstMethodsBySig(type);
			}
			CommandCenter.map_class_methodinfo[type].Invoke(handler, new object[]
			{
				eventData
			});
		}

		public static MethodInfo GetFirstMethodsBySig(this Type type, Type parameterType)
		{
			if (!CommandCenter.map_type_methodinfos.ContainsKey(type))
			{
				CommandCenter.map_type_methodinfos[type] = type.GetMethods();
			}
			MethodInfo[] array = CommandCenter.map_type_methodinfos[type];
			for (int i = 0; i < array.Length; i++)
			{
				ParameterInfo[] parameters = array[i].GetParameters();
				for (int j = 0; j < parameters.Length; j++)
				{
					if (parameters[j].get_ParameterType() == parameterType)
					{
						return array[i];
					}
				}
			}
			return null;
		}
	}
}
