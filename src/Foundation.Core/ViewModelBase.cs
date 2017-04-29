using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Foundation.Core
{
	public abstract class ViewModelBase : BaseEventListener, IObservableModel
	{
		private ObservableMessage _bindingMessage;

		private bool _isDisposed;

		private ModelBinder _binder;

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

		private ModelBinder Binder
		{
			get
			{
				if (this._binder == null && !this._isDisposed)
				{
					this._binder = new ModelBinder(this);
				}
				return this._binder;
			}
		}

		protected bool IsApplicationQuit
		{
			get;
			private set;
		}

		protected virtual void Awake()
		{
			this._bindingMessage = new ObservableMessage
			{
				Sender = this
			};
			if (this._binder == null)
			{
				this._binder = new ModelBinder(this);
			}
			base.AddListenersWhenAwake();
		}

		protected virtual void OnApplicationQuit()
		{
			this.IsApplicationQuit = true;
		}

		protected override void OnDestroy()
		{
			if (this.IsApplicationQuit)
			{
				return;
			}
			base.OnDestroy();
			this.Dispose();
		}

		[HideInInspector]
		public virtual void Dispose()
		{
			this._isDisposed = true;
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
			if (this.Binder != null)
			{
				this.Binder.RaiseBindingUpdate(memberName, paramater);
			}
			if (this.OnBindingUpdate != null && this._bindingMessage != null)
			{
				this._bindingMessage.Name = memberName;
				this._bindingMessage.Value = paramater;
				this.OnBindingUpdate.Invoke(this._bindingMessage);
			}
		}

		[HideInInspector]
		public void Command(string memberName)
		{
			this._binder.Command(memberName);
		}

		public void Command(string memberName, object paramater)
		{
			this._binder.Command(memberName, paramater);
		}

		[HideInInspector]
		public object GetValue(string memberName)
		{
			return this.Binder.GetValue(memberName);
		}

		public object GetValue(string memberName, object paramater)
		{
			return this.Binder.GetValue(memberName, paramater);
		}

		public void NotifyProperty(string memberName, object paramater)
		{
			this.RaiseBindingUpdate(memberName, paramater);
		}
	}
}
