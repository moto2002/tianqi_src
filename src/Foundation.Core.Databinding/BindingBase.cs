using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Foundation.Core.Databinding
{
	[ExecuteInEditMode]
	[Serializable]
	public abstract class BindingBase : MonoBehaviour, IBindingElement
	{
		public enum BindingFilter
		{
			Commands,
			Properties
		}

		[Serializable]
		public class BindingInfo
		{
			public string BindingName;

			public string MemberName;

			public BindingBase.BindingFilter Filters;

			public Type[] FilterTypes;

			public Action<object> Action
			{
				get;
				set;
			}

			public Func<bool> ShouldShow
			{
				get;
				set;
			}
		}

		public GameObject BindingProxy;

		protected BindingBase.BindingInfo[] _infoCache;

		[HideInInspector]
		private BindingContext _context;

		[HideInInspector]
		private IObservableModel _model;

		public virtual bool IsApplicationQuit
		{
			get;
			protected set;
		}

		public BindingContext Context
		{
			get
			{
				return this._context;
			}
			set
			{
				if (this._context == value)
				{
					return;
				}
				if (this._context != null)
				{
					this._context.UnsubscribeBinder(this);
				}
				this._context = value;
				if (this._context != null)
				{
					this._context.SubscribeBinder(this);
				}
			}
		}

		public IObservableModel Model
		{
			get
			{
				return this._model;
			}
			set
			{
				if (this._model == value)
				{
					return;
				}
				this.BeforeModelChanged();
				this._model = value;
				this.OnModelChanged();
			}
		}

		private void Awake()
		{
			this.InitBinding();
		}

		protected abstract void InitBinding();

		protected virtual void OnEnable()
		{
			this.FindContext();
		}

		protected virtual void OnDisable()
		{
			if (!Application.get_isPlaying())
			{
				return;
			}
			if (this.IsApplicationQuit)
			{
				return;
			}
			this.Context = null;
			this.Model = null;
		}

		protected virtual void OnApplicationQuit()
		{
			this.IsApplicationQuit = true;
		}

		protected object GetValue(string memberName)
		{
			if (this.Model == null)
			{
				Debuger.Warning(string.Concat(new object[]
				{
					"Model is null ! ",
					base.get_gameObject().get_name(),
					" ",
					base.GetType()
				}), new object[0]);
				return null;
			}
			return this.Model.GetValue(memberName);
		}

		protected object GetValue(string memberName, object argument)
		{
			if (this.Model == null)
			{
				Debuger.Warning(string.Concat(new object[]
				{
					"Model is null ! ",
					base.get_gameObject().get_name(),
					" ",
					base.GetType()
				}), new object[0]);
				return null;
			}
			return this.Model.GetValue(memberName);
		}

		protected void SetValue(string memberName, object argument)
		{
			if (!base.get_enabled())
			{
				return;
			}
			if (this.IsApplicationQuit)
			{
				return;
			}
			if (string.IsNullOrEmpty(memberName))
			{
				return;
			}
			if (this.Model == null)
			{
				return;
			}
			if (argument == null)
			{
				this.Model.Command(memberName);
			}
			else
			{
				this.Model.Command(memberName, argument);
			}
		}

		public virtual void OnBindingMessage(ObservableMessage m)
		{
			if (!Application.get_isPlaying())
			{
				return;
			}
			if (this.Model == null)
			{
				return;
			}
			if (this == null)
			{
				return;
			}
			if (!base.get_enabled())
			{
				return;
			}
			BindingBase.BindingInfo[] bindingInfos = this.GetBindingInfos();
			BindingBase.BindingInfo[] array = Enumerable.ToArray<BindingBase.BindingInfo>(Enumerable.Where<BindingBase.BindingInfo>(bindingInfos, (BindingBase.BindingInfo o) => o.MemberName == m.Name && o.Action != null));
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Action != null)
				{
					array[i].Action.Invoke(m.Value);
				}
			}
		}

		protected virtual void BeforeModelChanged()
		{
		}

		protected virtual void OnModelChanged()
		{
			if (!Application.get_isPlaying())
			{
				return;
			}
			if (this.Model == null)
			{
				return;
			}
			if (!base.get_enabled())
			{
				return;
			}
			BindingBase.BindingInfo[] bindingInfos = this.GetBindingInfos();
			for (int i = 0; i < bindingInfos.Length; i++)
			{
				BindingBase.BindingInfo bindingInfo = bindingInfos[i];
				if (!string.IsNullOrEmpty(bindingInfo.MemberName))
				{
					if (bindingInfo.Action != null)
					{
						bindingInfo.Action.Invoke(this.GetValue(bindingInfo.MemberName));
					}
				}
			}
		}

		public BindingBase.BindingInfo[] GetBindingInfos()
		{
			if (this._infoCache == null)
			{
				this._infoCache = Enumerable.ToArray<BindingBase.BindingInfo>(Enumerable.Cast<BindingBase.BindingInfo>(Enumerable.Select<FieldInfo, object>(Enumerable.Where<FieldInfo>(base.GetType().GetFields(), (FieldInfo o) => o.get_FieldType() == typeof(BindingBase.BindingInfo)), (FieldInfo o) => o.GetValue(this))));
			}
			return this._infoCache;
		}

		[ContextMenu("Find BindingContext")]
		public void FindContext()
		{
			if (this.BindingProxy != null)
			{
				this.Context = this.BindingProxy.GetComponent<BindingContext>();
			}
			else
			{
				this.Context = this.GetComponenetInParent<BindingContext>();
			}
		}
	}
}
