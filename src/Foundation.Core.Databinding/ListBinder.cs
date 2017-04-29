using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Foundation.Core.Databinding
{
	public class ListBinder : BindingBase
	{
		[HideInInspector]
		private const int MAX = 100;

		private int mLoadNumberFrame = 100;

		public string ITEM_NAME = string.Empty;

		public GameObject Prefab;

		public string PrefabName;

		public GameObject LoadingMask;

		[HideInInspector]
		public BindingBase.BindingInfo SourceBinding = new BindingBase.BindingInfo
		{
			BindingName = "DataSource"
		};

		[HideInInspector]
		public BindingBase.BindingInfo ShiftBinding = new BindingBase.BindingInfo
		{
			BindingName = "Shift"
		};

		private Transform ParentNode;

		public List<Transform> ParentNodes = new List<Transform>();

		private IObservableCollection DataList;

		public List<BindingContext> m_listUse = new List<BindingContext>();

		private List<BindingContext> m_listAll = new List<BindingContext>();

		private List<object> m_listData = new List<object>();

		public Transform Target2ScrollRect;

		private ScrollRectCustom m_thisScrollRectCustom;

		private object m_arg;

		public int LoadNumberFrame
		{
			get
			{
				return this.mLoadNumberFrame;
			}
			set
			{
				this.mLoadNumberFrame = Mathf.Clamp(value, 1, 100);
			}
		}

		private ScrollRectCustom thisScrollRectCustom
		{
			get
			{
				if (this.m_thisScrollRectCustom == null)
				{
					if (this.Target2ScrollRect == null)
					{
						this.m_thisScrollRectCustom = base.get_transform().get_parent().GetComponent<ScrollRectCustom>();
					}
					else
					{
						this.m_thisScrollRectCustom = this.Target2ScrollRect.GetComponent<ScrollRectCustom>();
					}
				}
				return this.m_thisScrollRectCustom;
			}
		}

		protected override void InitBinding()
		{
			this.SetPrefab();
			if (this.LoadingMask)
			{
				this.LoadingMask.SetActive(false);
			}
			this.SourceBinding.Action = new Action<object>(this.UpdateSource);
			this.SourceBinding.Filters = BindingBase.BindingFilter.Properties;
			this.SourceBinding.FilterTypes = new Type[]
			{
				typeof(IEnumerable)
			};
			this.ShiftBinding.Action = new Action<object>(this.UpdateShift);
			this.ShiftBinding.Filters = BindingBase.BindingFilter.Properties;
			this.ShiftBinding.FilterTypes = new Type[]
			{
				typeof(int)
			};
		}

		private void SetPrefab()
		{
			this.ParentNode = base.GetComponent<RectTransform>();
			if (this.Prefab != null)
			{
				this.Prefab.SetActive(false);
				if (this.Prefab.GetComponent<BindingContext>() == null)
				{
					base.set_enabled(false);
				}
			}
		}

		private void OnDestroy()
		{
			this.Prefab = null;
			if (this.m_listUse != null)
			{
				for (int i = 0; i < this.m_listUse.get_Count(); i++)
				{
					Object.Destroy(this.m_listUse.get_Item(i));
				}
				this.m_listUse.Clear();
			}
			if (this.m_listAll != null)
			{
				for (int j = 0; j < this.m_listAll.get_Count(); j++)
				{
					Object.Destroy(this.m_listAll.get_Item(j));
				}
				this.m_listAll.Clear();
			}
		}

		private void UpdateShift(object arg)
		{
			this.m_arg = arg;
			ListShifts.SetShift(this.thisScrollRectCustom, arg);
		}

		protected void UpdateSource(object value)
		{
			this.Bind(value);
		}

		public void Bind(object data)
		{
			this.ResetListBinder();
			if (data is IObservableCollection)
			{
				this.DataList = (data as IObservableCollection);
				base.StartCoroutine(this.AddAsync(this.DataList.GetObjects()));
				this.DataList.OnObjectAdd += new Action<object>(this.OnAdd);
				this.DataList.OnObjectOpen += new Action<object>(this.OnOpen);
				this.DataList.OnObjectHide += new Action<object>(this.OnHide);
				this.DataList.OnObjectOpenAll += new Action(this.OnOpenAll);
				this.DataList.OnObjectHideAll += new Action(this.OnHideAll);
				this.DataList.OnObjectGet += new Func<object, object>(this.OnGet);
				this.DataList.OnClear += new Action(this.OnClear);
				this.DataList.OnObjectRemove += new Action<object>(this.OnRemove);
			}
			else if (data is IEnumerable)
			{
				IEnumerable enumerable = data as IEnumerable;
				base.StartCoroutine(this.AddAsync(Enumerable.Cast<object>(enumerable)));
			}
		}

		private void ResetListBinder()
		{
			if (this.DataList != null)
			{
				this.DataList.OnObjectAdd -= new Action<object>(this.OnAdd);
				this.DataList.OnObjectOpen -= new Action<object>(this.OnOpen);
				this.DataList.OnObjectHide -= new Action<object>(this.OnHide);
				this.DataList.OnObjectOpenAll -= new Action(this.OnOpenAll);
				this.DataList.OnObjectHideAll -= new Action(this.OnHideAll);
				this.DataList.OnObjectGet -= new Func<object, object>(this.OnGet);
				this.DataList.OnClear -= new Action(this.OnClear);
				this.DataList.OnObjectRemove -= new Action<object>(this.OnRemove);
			}
			for (int i = 0; i < this.m_listUse.get_Count(); i++)
			{
				BindingContext bindingContext = this.m_listUse.get_Item(i);
				if (bindingContext != null)
				{
					bindingContext.DataInstance = null;
					this.Recycle(bindingContext.get_gameObject());
				}
			}
			this.DataList = null;
			this.OnClear();
			base.StopAllCoroutines();
		}

		[DebuggerHidden]
		private IEnumerator AddAsync(IEnumerable<object> objects)
		{
			ListBinder.<AddAsync>c__Iterator2F <AddAsync>c__Iterator2F = new ListBinder.<AddAsync>c__Iterator2F();
			<AddAsync>c__Iterator2F.objects = objects;
			<AddAsync>c__Iterator2F.<$>objects = objects;
			<AddAsync>c__Iterator2F.<>f__this = this;
			return <AddAsync>c__Iterator2F;
		}

		private void OnAdd(object obj)
		{
			this.AddData(obj);
		}

		private void OnRemove(object obj)
		{
			this.RemoveData(obj);
			this.RemoveInstantiate(obj);
		}

		private void OnClear()
		{
			this.ClearData();
			this.ClearInstantiate();
		}

		private object OnGet(object obj)
		{
			return Enumerable.FirstOrDefault<BindingContext>(this.m_listUse, (BindingContext o) => o.DataInstance == obj).DataInstance;
		}

		private void OnOpen(object obj)
		{
			BindingContext bindingContext = Enumerable.FirstOrDefault<BindingContext>(this.m_listUse, (BindingContext o) => o.DataInstance == obj);
			if (bindingContext != null)
			{
				bindingContext.get_gameObject().SetActive(true);
			}
			else
			{
				Debuger.Error("not found obj", new object[0]);
			}
		}

		private void OnOpenAll()
		{
			for (int i = 0; i < this.m_listUse.get_Count(); i++)
			{
				BindingContext bindingContext = this.m_listUse.get_Item(i);
				if (bindingContext != null)
				{
					bindingContext.get_gameObject().SetActive(true);
				}
			}
		}

		private void OnHide(object obj)
		{
			BindingContext bindingContext = Enumerable.FirstOrDefault<BindingContext>(this.m_listUse, (BindingContext o) => o.DataInstance == obj);
			if (bindingContext != null)
			{
				bindingContext.get_gameObject().SetActive(false);
			}
		}

		private void OnHideAll()
		{
			this.HideAll();
		}

		public void HideAll()
		{
			for (int i = 0; i < this.m_listUse.get_Count(); i++)
			{
				BindingContext bindingContext = this.m_listUse.get_Item(i);
				if (bindingContext != null)
				{
					bindingContext.get_gameObject().SetActive(false);
				}
			}
		}

		private void RemoveInstantiate(object obj)
		{
			BindingContext bindingContext = Enumerable.FirstOrDefault<BindingContext>(this.m_listUse, (BindingContext o) => o.DataInstance == obj);
			if (bindingContext != null)
			{
				bindingContext.DataInstance = null;
				this.m_listUse.Remove(bindingContext);
				this.Recycle(bindingContext.get_gameObject());
			}
		}

		private void ClearInstantiate()
		{
			for (int i = 0; i < this.m_listUse.get_Count(); i++)
			{
				BindingContext bindingContext = this.m_listUse.get_Item(i);
				bindingContext.DataInstance = null;
				this.Recycle(bindingContext.get_gameObject());
			}
			this.m_listUse.Clear();
		}

		private void AddData(object data)
		{
			this.m_listData.Add(data);
		}

		private void RemoveData(object data)
		{
			this.m_listData.Remove(data);
		}

		private void ClearData()
		{
			this.m_listData.Clear();
		}

		private void Update()
		{
			int count = this.m_listData.get_Count();
			int num = Mathf.Min(this.LoadNumberFrame, this.m_listData.get_Count());
			for (int i = 0; i < count; i++)
			{
				if (this.IsPoolExist())
				{
					this.DoInstantiateJustOne();
				}
				else
				{
					if (i >= num)
					{
						break;
					}
					this.DoInstantiateJustOne();
				}
			}
		}

		private void DoInstantiateJustOne()
		{
			if (this.m_listData.get_Count() > 0)
			{
				BindingContext next = this.GetNext();
				next.DataInstance = this.m_listData.get_Item(0);
				this.m_listData.RemoveAt(0);
				this.m_listUse.Add(next);
				if (this.m_listData.get_Count() == 0)
				{
					this.UpdateShift(this.m_arg);
				}
			}
		}

		private bool IsPoolExist()
		{
			return this.m_listUse.get_Count() < this.m_listAll.get_Count();
		}

		private BindingContext GetNext()
		{
			BindingContext bindingContext;
			if (this.IsPoolExist())
			{
				bindingContext = this.m_listAll.get_Item(this.m_listUse.get_Count());
			}
			else
			{
				bindingContext = this.DoInstantiate().GetComponent<BindingContext>();
				this.m_listAll.Add(bindingContext);
			}
			bindingContext.get_transform().set_localScale(Vector3.get_one());
			bindingContext.get_gameObject().SetActive(true);
			return bindingContext;
		}

		private void Recycle(GameObject instance)
		{
			instance.SetActive(false);
		}

		private GameObject DoInstantiate()
		{
			Transform transform = this.ParentNode;
			if (this.m_listUse.get_Count() < this.ParentNodes.get_Count())
			{
				transform = this.ParentNodes.get_Item(this.m_listUse.get_Count());
			}
			GameObject gameObject;
			if (this.Prefab != null)
			{
				gameObject = UGUITools.AddChild(transform.get_gameObject(), this.Prefab, false);
			}
			else
			{
				gameObject = ResourceManager.GetInstantiate2Prefab(this.PrefabName);
				UGUITools.SetParent(transform.get_gameObject(), gameObject, false);
			}
			if (string.IsNullOrEmpty(this.ITEM_NAME))
			{
				gameObject.set_name("_Item " + this.m_listUse.get_Count());
			}
			else
			{
				gameObject.set_name(this.ITEM_NAME + this.m_listUse.get_Count());
			}
			return gameObject;
		}
	}
}
