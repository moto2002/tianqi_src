using System;
using System.Collections.Generic;
using System.Reflection;

public class IListSort<T>
{
	private IList<T> list;

	private List<string> propertyName;

	private bool[] sortBy;

	public IList<T> List
	{
		get
		{
			return this.list;
		}
		set
		{
			this.list = value;
		}
	}

	public List<string> PropertyName
	{
		get
		{
			return this.propertyName;
		}
		set
		{
			this.propertyName = value;
		}
	}

	public bool[] SortBy
	{
		get
		{
			return this.sortBy;
		}
		set
		{
			this.sortBy = value;
		}
	}

	public IListSort(IList<T> list, List<string> propertyName)
	{
		this.list = list;
		this.propertyName = propertyName;
		bool[] array = new bool[propertyName.get_Count()];
		for (int i = 0; i < propertyName.get_Count(); i++)
		{
			array[i] = false;
		}
		this.sortBy = array;
	}

	public IListSort(IList<T> list, List<string> propertyName, bool[] sortBy)
	{
		this.list = list;
		this.propertyName = propertyName;
		this.sortBy = sortBy;
	}

	public IList<T> Sort(Type type = null)
	{
		if (this.list.get_Count() == 0)
		{
			return this.list;
		}
		type = ((type != null) ? type : typeof(T));
		for (int i = 1; i < this.list.get_Count(); i++)
		{
			T t = this.list.get_Item(i);
			int num = i;
			while (num > 0 && this.Compare(this.list.get_Item(num - 1), t) < 0)
			{
				this.list.set_Item(num, this.list.get_Item(num - 1));
				num--;
			}
			this.list.set_Item(num, t);
		}
		return this.list;
	}

	private int Compare(T x, T y)
	{
		int num = 0;
		PropertyInfo[] array = new PropertyInfo[this.propertyName.get_Count()];
		for (int i = 0; i < this.propertyName.get_Count(); i++)
		{
			if (string.IsNullOrEmpty(this.propertyName.get_Item(i)))
			{
				throw new ArgumentNullException("没有指字对象的排序字段属性名");
			}
			array[i] = typeof(T).GetProperty(this.propertyName.get_Item(i));
			if (array[i] == null)
			{
				throw new ArgumentNullException("在对象中没有找到指定属性");
			}
			num = this.Comparer(x, y, array[i], this.sortBy[i]);
			if (num != 0)
			{
				return num;
			}
		}
		return num;
	}

	private int Comparer(T x, T y, PropertyInfo property, bool sortBy)
	{
		string text = property.get_PropertyType().ToString();
		if (text != null)
		{
			if (IListSort<T>.<>f__switch$map18 == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(2);
				dictionary.Add("System.Int32", 0);
				dictionary.Add("System.String", 1);
				IListSort<T>.<>f__switch$map18 = dictionary;
			}
			int num;
			if (IListSort<T>.<>f__switch$map18.TryGetValue(text, ref num))
			{
				if (num != 0)
				{
					if (num == 1)
					{
						string text2 = string.Empty;
						string text3 = string.Empty;
						if (property.GetValue(x, null) != null)
						{
							text2 = property.GetValue(x, null).ToString();
						}
						if (property.GetValue(y, null) != null)
						{
							text3 = property.GetValue(y, null).ToString();
						}
						if (sortBy)
						{
							return text3.CompareTo(text2);
						}
						return text2.CompareTo(text3);
					}
				}
				else
				{
					int num2 = 0;
					int num3 = 0;
					if (property.GetValue(x, null) != null)
					{
						num2 = Convert.ToInt32(property.GetValue(x, null));
					}
					if (property.GetValue(y, null) != null)
					{
						num3 = Convert.ToInt32(property.GetValue(y, null));
					}
					if (sortBy)
					{
						return num3.CompareTo(num2);
					}
					return num2.CompareTo(num3);
				}
			}
		}
		return 0;
	}
}
