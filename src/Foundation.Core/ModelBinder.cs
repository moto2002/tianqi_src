using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Foundation.Core
{
	public class ModelBinder : IDisposable, IObservableModel
	{
		private Type _myType;

		private object _instance;

		private MonoBehaviour _insanceBehaviour;

		private IObservableModel _bindableInstance;

		private ObservableMessage _bindingMessage = new ObservableMessage();

		public event Action<ObservableMessage> OnBindingUpdate
		{
			[MethodImpl(32)]
			add
			{
				this.OnBindingUpdate = (Action<ObservableMessage>)Delegate.Combine(this.OnBindingUpdate, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.OnBindingUpdate = (Action<ObservableMessage>)Delegate.Remove(this.OnBindingUpdate, value);
			}
		}

		public ModelBinder(object instance)
		{
			this._instance = instance;
			this._myType = this._instance.GetType();
			this._insanceBehaviour = (instance as MonoBehaviour);
			this._bindableInstance = (instance as IObservableModel);
			if (this._bindableInstance != null)
			{
				this._bindableInstance.OnBindingUpdate += new Action<ObservableMessage>(this._bindableInstance_OnBindingUpdate);
			}
		}

		[HideInInspector]
		public void Dispose()
		{
			this._bindingMessage.Dispose();
			if (this._bindableInstance != null)
			{
				this._bindableInstance.OnBindingUpdate -= new Action<ObservableMessage>(this._bindableInstance_OnBindingUpdate);
			}
			this._myType = null;
			this._instance = null;
			this._insanceBehaviour = null;
			this._bindableInstance = null;
			this._bindingMessage = null;
		}

		public void RaiseBindingUpdate(string memberName, object paramater)
		{
			if (this.OnBindingUpdate != null)
			{
				this._bindingMessage.Name = memberName;
				this._bindingMessage.Sender = this;
				this._bindingMessage.Value = paramater;
				this.OnBindingUpdate.Invoke(this._bindingMessage);
			}
		}

		[HideInInspector]
		public void Command(string memberName)
		{
			this.Command(memberName, null);
		}

		public void Command(string memberName, object paramater)
		{
			MemberInfo memberInfo = Enumerable.FirstOrDefault<MemberInfo>(this._myType.GetMember(memberName));
			if (memberInfo == null)
			{
				Debuger.Error(string.Concat(new object[]
				{
					"Member not found ! ",
					memberName,
					" ",
					this._myType
				}), new object[0]);
				return;
			}
			object obj = ConverterHelper.ConvertTo(memberInfo.GetParamaterType(), paramater);
			if (memberInfo is MethodInfo)
			{
				MethodInfo methodInfo = memberInfo as MethodInfo;
				if (methodInfo.get_ReturnType() == typeof(IEnumerator))
				{
					if (this._insanceBehaviour)
					{
						if (!this._insanceBehaviour.get_gameObject().get_activeSelf())
						{
							Debuger.Error("Behaviour is inactive !", new object[0]);
						}
						if (obj == null)
						{
							this._insanceBehaviour.StartCoroutine(methodInfo.get_Name());
						}
						else
						{
							this._insanceBehaviour.StartCoroutine(methodInfo.get_Name(), obj);
						}
					}
					else
					{
						MethodBase arg_107_0 = methodInfo;
						object arg_107_1 = this._instance;
						object arg_107_2;
						if (obj == null)
						{
							arg_107_2 = null;
						}
						else
						{
							(arg_107_2 = new object[1])[0] = obj;
						}
						object obj2 = arg_107_0.Invoke(arg_107_1, arg_107_2);
						TaskManager.StartRoutine((IEnumerator)obj2);
					}
					return;
				}
			}
			memberInfo.SetMemberValue(this._instance, obj);
		}

		[HideInInspector]
		public object GetValue(string memberName)
		{
			MemberInfo memberInfo = Enumerable.FirstOrDefault<MemberInfo>(this._myType.GetMember(memberName));
			if (memberInfo == null)
			{
				return null;
			}
			return memberInfo.GetMemberValue(this._instance);
		}

		public object GetValue(string memberName, object paramater)
		{
			MethodInfo method = this._myType.GetMethod(memberName);
			if (method == null)
			{
				return null;
			}
			if (paramater == null)
			{
				return method.Invoke(this._instance, null);
			}
			ParameterInfo parameterInfo = Enumerable.FirstOrDefault<ParameterInfo>(method.GetParameters());
			if (parameterInfo == null)
			{
				return this.GetValue(memberName);
			}
			object obj = ConverterHelper.ConvertTo(parameterInfo.GetType(), paramater);
			return method.Invoke(this._instance, new object[]
			{
				obj
			});
		}

		private void _bindableInstance_OnBindingUpdate(ObservableMessage obj)
		{
			if (this.OnBindingUpdate != null)
			{
				this.OnBindingUpdate.Invoke(obj);
			}
		}

		public virtual void NotifyProperty(string propertyName, object propValue)
		{
			this.RaiseBindingUpdate(propertyName, propValue);
		}
	}
}
