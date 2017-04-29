using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Foundation.Core.Databinding
{
	public class ListViewBinder : BindingBase, ListViewInterface
	{
		[HideInInspector]
		public BindingBase.BindingInfo SourceBinding = new BindingBase.BindingInfo
		{
			BindingName = "DataSource"
		};

		public string ITEM_NAME = "_item";

		public string PrefabName;

		public ListView.ListViewScrollStyle m_scrollStype;

		public float m_spacing;

		public List<float> m_listSpacing;

		private IObservableCollection dataList;

		private List<object> alldataList = new List<object>();

		private ListView lv;

		private uint timerId;

		protected override void InitBinding()
		{
			this.SourceBinding.Action = new Action<object>(this.UpdateSource);
			this.SourceBinding.Filters = BindingBase.BindingFilter.Properties;
			this.SourceBinding.FilterTypes = new Type[]
			{
				typeof(IEnumerable)
			};
		}

		protected void UpdateSource(object value)
		{
			this.Bind(value);
		}

		public void Bind(object data)
		{
			this.ResetListBinder();
			this.lv = base.GetComponent<ListView>();
			this.lv.manager = this;
			this.lv.Init(this.m_scrollStype);
			if (data is IObservableCollection)
			{
				this.dataList = (data as IObservableCollection);
				this.AddDataList(this.dataList.GetObjects());
				this.dataList.OnObjectAdd += new Action<object>(this.OnAdd);
				this.dataList.OnObjectRemove += new Action<object>(this.OnRemove);
				this.dataList.OnClear += new Action(this.OnClear);
			}
		}

		private void ResetListBinder()
		{
			if (this.dataList != null)
			{
				this.dataList.OnObjectAdd -= new Action<object>(this.OnAdd);
				this.dataList.OnObjectRemove -= new Action<object>(this.OnRemove);
				this.dataList.OnClear -= new Action(this.OnClear);
			}
			this.dataList = null;
			this.alldataList.Clear();
			this.OnClear();
		}

		private void AddDataList(IEnumerable<object> objects)
		{
			using (IEnumerator<object> enumerator = objects.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object current = enumerator.get_Current();
					this.OnAdd(current);
				}
			}
		}

		private void OnAdd(object obj)
		{
			this.alldataList.Add(obj);
			this.RefreshView();
		}

		private void OnRemove(object obj)
		{
			object obj2 = Enumerable.FirstOrDefault<object>(this.alldataList, (object o) => o == obj);
			if (obj2 != null)
			{
				this.alldataList.Remove(obj2);
				this.RefreshView();
			}
		}

		private void OnClear()
		{
			this.alldataList.Clear();
			this.RefreshView();
		}

		public Cell CellForRow(ListView listView, int row)
		{
			string text = "cell";
			Cell cell = listView.CellForReuseIndentify(text);
			if (cell == null)
			{
				cell = new Cell(listView);
				cell.identify = text;
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab(this.PrefabName);
				cell.content = instantiate2Prefab;
				instantiate2Prefab.GetComponent<RectTransform>().set_localScale(new Vector3(1f, 1f, 1f));
				instantiate2Prefab.SetActive(true);
			}
			BindingContext component = cell.content.GetComponent<BindingContext>();
			if (row < this.alldataList.get_Count())
			{
				component.DataInstance = this.alldataList.get_Item(row);
			}
			return cell;
		}

		public float SpacingForRow(ListView listView, int row)
		{
			if (this.m_listSpacing != null && row < this.m_listSpacing.get_Count())
			{
				return this.m_listSpacing.get_Item(row);
			}
			return this.m_spacing;
		}

		public uint CountOfRows(ListView listView)
		{
			return (uint)this.alldataList.get_Count();
		}

		private void RefreshView()
		{
			TimerHeap.DelTimer(this.timerId);
			this.timerId = TimerHeap.AddTimer(100u, 0, delegate
			{
				if (this.lv != null)
				{
					this.lv.Refresh();
				}
			});
		}
	}
}
