using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Foundation.Core.Databinding
{
	[ExecuteInEditMode]
	public class BindingContext : MonoBehaviour, IBindingElement
	{
		public enum BindingContextMode
		{
			None,
			MonoBinding,
			MockBinding,
			PropBinding
		}

		private static Assembly[] _assemblies;

		private static Type[] _modelTypes;

		private static string[] _namespaces;

		[HideInInspector]
		public BindingContext.BindingContextMode ContextMode;

		[HideInInspector]
		public bool ModelIsMock;

		[HideInInspector]
		public string ModelAssembly;

		[HideInInspector]
		public string ModelNamespace;

		[HideInInspector]
		public string ModelFullName;

		[HideInInspector]
		public string ModelType;

		[HideInInspector]
		public MonoBehaviour ViewModel;

		private Type _dataType;

		private object _dataInstance;

		protected bool IsWrappedBinder;

		protected List<IBindingElement> Binders = new List<IBindingElement>();

		private BindingContext _parentContext;

		[SerializeField]
		private IObservableModel _model;

		[HideInInspector, SerializeField]
		private string _propertyName;

		public static Assembly[] Assemblies
		{
			get
			{
				BindingContext.RefreshAssembly();
				return BindingContext._assemblies;
			}
		}

		public static Type[] ModelTypes
		{
			get
			{
				BindingContext.RefreshAssembly();
				return BindingContext._modelTypes;
			}
		}

		public static string[] NameSpaces
		{
			get
			{
				BindingContext.RefreshAssembly();
				return BindingContext._namespaces;
			}
		}

		public Type DataType
		{
			get
			{
				return this._dataType;
			}
			set
			{
				if (this._dataType == value)
				{
					return;
				}
				this._dataType = value;
				if (value != null)
				{
					this.ModelType = value.get_Name();
					this.ModelFullName = value.get_FullName();
					this.ModelNamespace = value.get_Namespace();
					this.ModelAssembly = value.get_Assembly().get_FullName();
				}
			}
		}

		public object DataInstance
		{
			get
			{
				return this._dataInstance;
			}
			set
			{
				if (this._dataInstance == value)
				{
					return;
				}
				this.OnRemoveInstance();
				this._dataInstance = value;
				if (this.DataInstance != null)
				{
					this.OnAddInstance();
				}
			}
		}

		protected IObservableModel BindableContext
		{
			get;
			set;
		}

		public BindingContext Context
		{
			get
			{
				return this._parentContext;
			}
			set
			{
				if (this._parentContext == value)
				{
					return;
				}
				if (this._parentContext != null)
				{
					this._parentContext.UnsubscribeBinder(this);
				}
				this._parentContext = value;
				if (this._parentContext != null)
				{
					this._parentContext.SubscribeBinder(this);
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
				this._model = value;
				this.InitialValue();
			}
		}

		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
			set
			{
				if (this._propertyName == value)
				{
					return;
				}
				this._propertyName = value;
				this.SetPropertyTypeData();
				this.InitialValue();
			}
		}

		private static void RefreshAssembly()
		{
			if (BindingContext._assemblies == null)
			{
				BindingContext._assemblies = Enumerable.ToArray<Assembly>(Enumerable.OrderBy<Assembly, string>(Enumerable.Where<Assembly>(AppDomain.get_CurrentDomain().GetAssemblies(), (Assembly o) => !o.get_Location().Contains("Editor")), (Assembly o) => o.get_FullName()));
				BindingContext._modelTypes = Enumerable.ToArray<Type>(Enumerable.OrderBy<Type, string>(Enumerable.Where<Type>(Enumerable.SelectMany<Assembly, Type>(BindingContext._assemblies, (Assembly o) => o.GetTypes()), (Type o) => o.get_IsPublic()), (Type o) => o.get_Name()));
				BindingContext._namespaces = Enumerable.ToArray<string>(Enumerable.Distinct<string>(Enumerable.OrderBy<string, string>(Enumerable.Select<Type, string>(BindingContext._modelTypes, (Type o) => o.get_Namespace()), (string o) => o)));
			}
		}

		public Type GetDataType()
		{
			if (Application.get_isPlaying())
			{
				return null;
			}
			if (BindingContext.ModelTypes != null)
			{
				return Enumerable.FirstOrDefault<Type>(BindingContext.ModelTypes, (Type o) => o.get_FullName() == this.ModelFullName);
			}
			return null;
		}

		public bool HasDataType()
		{
			return !string.IsNullOrEmpty(this.ModelFullName) && !string.IsNullOrEmpty(this.ModelNamespace);
		}

		public void OnEnableSelf()
		{
			this.FindModel();
		}

		protected void OnEnable()
		{
			this.FindModel();
		}

		protected void OnDisable()
		{
			if (Application.get_isPlaying())
			{
				this.Context = null;
			}
		}

		[ContextMenu("Find Model")]
		public void FindModel()
		{
			switch (this.ContextMode)
			{
			case BindingContext.BindingContextMode.None:
				this.ClearTypeData();
				this.DataInstance = null;
				break;
			case BindingContext.BindingContextMode.MonoBinding:
				this.Context = null;
				this.DataInstance = this.ViewModel;
				break;
			case BindingContext.BindingContextMode.MockBinding:
				this.Context = null;
				if (this.DataType == null)
				{
					this.DataType = this.GetDataType();
				}
				break;
			case BindingContext.BindingContextMode.PropBinding:
				this.Context = this.GetComponenetInParent(true);
				if (!Application.get_isPlaying())
				{
					this.SetPropertyTypeData();
				}
				break;
			}
		}

		[ContextMenu("Clear TypeData")]
		public void ClearTypeData()
		{
			this.DataType = null;
			this.Context = null;
			this.Model = null;
			this.ModelType = null;
			this.ModelFullName = null;
			this.ModelNamespace = null;
			this.ModelAssembly = null;
		}

		private void OnRemoveInstance()
		{
			if (!Application.get_isPlaying())
			{
				return;
			}
			if (this.BindableContext != null)
			{
				this.BindableContext.OnBindingUpdate -= new Action<ObservableMessage>(this.RelayBindingUpdate);
				if (this.IsWrappedBinder)
				{
					((ModelBinder)this.BindableContext).Dispose();
				}
			}
			this.BindableContext = null;
			this.IsWrappedBinder = false;
			IBindingElement[] array = this.Binders.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				if (!(array[i].Context != this))
				{
					array[i].Model = null;
				}
			}
		}

		private void OnAddInstance()
		{
			this.DataType = this.DataInstance.GetType();
			if (!Application.get_isPlaying())
			{
				return;
			}
			this.BindableContext = (this.DataInstance as IObservableModel);
			if (this.BindableContext == null)
			{
				this.BindableContext = new ModelBinder(this.DataInstance);
				this.IsWrappedBinder = true;
			}
			this.BindableContext.OnBindingUpdate += new Action<ObservableMessage>(this.RelayBindingUpdate);
			IBindingElement[] array = this.Binders.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				if (!(array[i].Context != this))
				{
					array[i].Model = this.BindableContext;
				}
			}
		}

		public void SubscribeBinder(IBindingElement child)
		{
			if (!this.Binders.Contains(child))
			{
				this.Binders.Add(child);
			}
			child.Model = this.BindableContext;
		}

		public void UnsubscribeBinder(IBindingElement child)
		{
			child.Model = null;
			this.Binders.Remove(child);
		}

		public void ClearBinders()
		{
			this.Binders.Clear();
		}

		private void RelayBindingUpdate(ObservableMessage message)
		{
			IBindingElement[] array = this.Binders.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnBindingMessage(message);
			}
		}

		public void OnBindingMessage(ObservableMessage message)
		{
			if (message.Name == this.PropertyName)
			{
				this.DataInstance = message.Value;
			}
		}

		private void InitialValue()
		{
			if (!Application.get_isPlaying())
			{
				return;
			}
			if (string.IsNullOrEmpty(this.PropertyName))
			{
				return;
			}
			if (this.Model == null)
			{
				return;
			}
			this.DataInstance = this.Model.GetValue(this.PropertyName);
		}

		private void SetPropertyTypeData()
		{
			if (Application.get_isPlaying())
			{
				return;
			}
			if (string.IsNullOrEmpty(this.PropertyName))
			{
				return;
			}
			if (this.Context == null)
			{
				return;
			}
			if (this.Context.DataType == null)
			{
				return;
			}
			MemberInfo memberInfo = Enumerable.FirstOrDefault<MemberInfo>(this.Context.DataType.GetMember(this.PropertyName));
			if (memberInfo == null)
			{
				return;
			}
			if (memberInfo is FieldInfo)
			{
				this.DataType = ((FieldInfo)memberInfo).get_FieldType();
			}
			if (memberInfo is PropertyInfo)
			{
				this.DataType = ((PropertyInfo)memberInfo).get_PropertyType();
			}
		}
	}
}
