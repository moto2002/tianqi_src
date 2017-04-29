using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Foundation.Core
{
	public class ObservableCollection<T> : IEnumerable, IEnumerable<T>, IObservableCollection
	{
		private readonly List<T> _OClist = new List<T>();

		public event Action<object> OnObjectAdd
		{
			[MethodImpl(32)]
			add
			{
				this.OnObjectAdd = (Action<object>)Delegate.Combine(this.OnObjectAdd, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.OnObjectAdd = (Action<object>)Delegate.Remove(this.OnObjectAdd, value);
			}
		}

		public event Action<object> OnObjectRemove
		{
			[MethodImpl(32)]
			add
			{
				this.OnObjectRemove = (Action<object>)Delegate.Combine(this.OnObjectRemove, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.OnObjectRemove = (Action<object>)Delegate.Remove(this.OnObjectRemove, value);
			}
		}

		public event Action<object> OnObjectHide
		{
			[MethodImpl(32)]
			add
			{
				this.OnObjectHide = (Action<object>)Delegate.Combine(this.OnObjectHide, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.OnObjectHide = (Action<object>)Delegate.Remove(this.OnObjectHide, value);
			}
		}

		public event Action<object> OnObjectOpen
		{
			[MethodImpl(32)]
			add
			{
				this.OnObjectOpen = (Action<object>)Delegate.Combine(this.OnObjectOpen, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.OnObjectOpen = (Action<object>)Delegate.Remove(this.OnObjectOpen, value);
			}
		}

		public event Action OnObjectHideAll
		{
			[MethodImpl(32)]
			add
			{
				this.OnObjectHideAll = (Action)Delegate.Combine(this.OnObjectHideAll, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.OnObjectHideAll = (Action)Delegate.Remove(this.OnObjectHideAll, value);
			}
		}

		public event Action OnObjectOpenAll
		{
			[MethodImpl(32)]
			add
			{
				this.OnObjectOpenAll = (Action)Delegate.Combine(this.OnObjectOpenAll, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.OnObjectOpenAll = (Action)Delegate.Remove(this.OnObjectOpenAll, value);
			}
		}

		public event Func<object, object> OnObjectGet
		{
			[MethodImpl(32)]
			add
			{
				this.OnObjectGet = (Func<object, object>)Delegate.Combine(this.OnObjectGet, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.OnObjectGet = (Func<object, object>)Delegate.Remove(this.OnObjectGet, value);
			}
		}

		public event Action<T> OnOpen
		{
			[MethodImpl(32)]
			add
			{
				this.OnOpen = (Action<T>)Delegate.Combine(this.OnOpen, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.OnOpen = (Action<T>)Delegate.Remove(this.OnOpen, value);
			}
		}

		public event Action<T> OnHide
		{
			[MethodImpl(32)]
			add
			{
				this.OnHide = (Action<T>)Delegate.Combine(this.OnHide, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.OnHide = (Action<T>)Delegate.Remove(this.OnHide, value);
			}
		}

		public event Action<T> OnAdd
		{
			[MethodImpl(32)]
			add
			{
				this.OnAdd = (Action<T>)Delegate.Combine(this.OnAdd, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.OnAdd = (Action<T>)Delegate.Remove(this.OnAdd, value);
			}
		}

		public event Action<T> OnRemove
		{
			[MethodImpl(32)]
			add
			{
				this.OnRemove = (Action<T>)Delegate.Combine(this.OnRemove, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.OnRemove = (Action<T>)Delegate.Remove(this.OnRemove, value);
			}
		}

		public event Action OnClear
		{
			[MethodImpl(32)]
			add
			{
				this.OnClear = (Action)Delegate.Combine(this.OnClear, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.OnClear = (Action)Delegate.Remove(this.OnClear, value);
			}
		}

		public List<T> OClist
		{
			get
			{
				return this._OClist;
			}
		}

		public T this[int i]
		{
			get
			{
				return this._OClist.get_Item(i);
			}
			set
			{
				this._OClist.set_Item(i, value);
			}
		}

		public int Count
		{
			get
			{
				return this._OClist.get_Count();
			}
		}

		public ObservableCollection()
		{
		}

		public ObservableCollection(IEnumerable<T> set)
		{
			this.Add(set);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._OClist.GetEnumerator();
		}

		public bool Contains(T item)
		{
			return this._OClist.Contains(item);
		}

		public TT Get<TT>(TT o) where TT : class
		{
			if (this.OnObjectGet != null)
			{
				return this.OnObjectGet.Invoke(o) as TT;
			}
			return (TT)((object)null);
		}

		public T GetItem(int index)
		{
			if (index < this._OClist.get_Count())
			{
				return this._OClist.get_Item(index);
			}
			return default(T);
		}

		public void Add(T o)
		{
			this._OClist.Add(o);
			if (this.OnAdd != null)
			{
				this.OnAdd.Invoke(o);
			}
			if (this.OnObjectAdd != null)
			{
				this.OnObjectAdd.Invoke(o);
			}
		}

		public void Add(IEnumerable<T> o)
		{
			T[] array = Enumerable.ToArray<T>(o);
			for (int i = 0; i < array.Length; i++)
			{
				this.Add(array[i]);
			}
		}

		public void Remove(T o)
		{
			if (this._OClist.Remove(o))
			{
				if (this.OnRemove != null)
				{
					this.OnRemove.Invoke(o);
				}
				if (this.OnObjectRemove != null)
				{
					this.OnObjectRemove.Invoke(o);
				}
			}
		}

		public void Remove(IEnumerable<T> o)
		{
			T[] array = Enumerable.ToArray<T>(o);
			for (int i = 0; i < array.Length; i++)
			{
				this.Remove(array[i]);
			}
		}

		public void Release()
		{
			if (this.OnClear != null)
			{
				this.Clear();
			}
		}

		public void Clear()
		{
			this._OClist.Clear();
			if (this.OnClear != null)
			{
				this.OnClear.Invoke();
			}
		}

		public IEnumerable<object> GetObjects()
		{
			if (this._OClist.get_Count() == 0)
			{
				return new object[0];
			}
			return Enumerable.Cast<object>(this._OClist);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return this._OClist.GetEnumerator();
		}

		public T[] ToArray()
		{
			return this._OClist.ToArray();
		}
	}
}
