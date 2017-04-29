using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Foundation.Core
{
	public abstract class ObservableObject : IObservableModel
	{
		private ModelBinder _binder;

		private ObservableMessage _bindingMessage;

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

		protected ObservableObject()
		{
			this._bindingMessage = new ObservableMessage
			{
				Sender = this
			};
			this._binder = new ModelBinder(this);
		}

		[HideInInspector]
		public virtual void Dispose()
		{
			if (this._binder != null)
			{
				this._binder.Dispose();
			}
			if (this._bindingMessage != null)
			{
				this._bindingMessage.Dispose();
			}
			this._bindingMessage = null;
			this._binder = null;
		}

		public void RaiseBindingUpdate(string memberName, object paramater)
		{
			if (this.OnBindingUpdate != null)
			{
				this._bindingMessage.Name = memberName;
				this._bindingMessage.Value = paramater;
				this.OnBindingUpdate.Invoke(this._bindingMessage);
			}
			this._binder.RaiseBindingUpdate(memberName, paramater);
		}

		[HideInInspector]
		public object GetValue(string memberName)
		{
			return this._binder.GetValue(memberName);
		}

		public object GetValue(string memberName, object paramater)
		{
			return this._binder.GetValue(memberName, paramater);
		}

		public void Command(string memberName)
		{
			this._binder.Command(memberName);
		}

		public void Command(string memberName, object paramater)
		{
			this._binder.Command(memberName, paramater);
		}

		public void NotifyProperty(string memberName, object paramater)
		{
			this.RaiseBindingUpdate(memberName, paramater);
		}

		public void SetValue(string memberName, object paramater)
		{
			this._binder.RaiseBindingUpdate(memberName, paramater);
		}

		public void StartCoroutine(IEnumerator routine)
		{
			TaskManager.StartRoutine(routine);
		}

		public void StopCoroutine(IEnumerator routine)
		{
			TaskManager.StopRoutine(routine);
		}
	}
}
