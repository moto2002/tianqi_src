using GameData;
using System;
using System.Collections.Generic;
using System.Reflection;

public class GoodsSort
{
	private List<Goods> list;

	private List<string> propertyName;

	private GoodsTab goodtp;

	public List<Goods> List
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

	public GoodsTab GoodTP
	{
		get
		{
			return this.goodtp;
		}
		set
		{
			this.goodtp = value;
		}
	}

	public GoodsSort(List<Goods> list, GoodsTab goodtp)
	{
		this.list = list;
		this.propertyName = BackpackManager.Instance.OnGetSort(goodtp);
		this.goodtp = goodtp;
	}

	public List<Goods> Sort()
	{
		if (this.list.get_Count() == 0)
		{
			return this.list;
		}
		for (int i = 1; i < this.list.get_Count(); i++)
		{
			Goods goods = this.list.get_Item(i);
			int num = i;
			while (num > 0 && this.Compare(this.list.get_Item(num - 1), goods) < 0)
			{
				this.list.set_Item(num, this.list.get_Item(num - 1));
				num--;
			}
			this.list.set_Item(num, goods);
		}
		return this.list;
	}

	private int Compare(Goods x, Goods y)
	{
		int num = 0;
		PropertyInfo[] array = new PropertyInfo[this.propertyName.get_Count()];
		for (int i = 0; i < this.propertyName.get_Count(); i++)
		{
			if (string.IsNullOrEmpty(this.propertyName.get_Item(i)))
			{
				throw new ArgumentNullException("没有指字对象的排序字段属性名");
			}
			array[i] = typeof(Items).GetProperty(this.propertyName.get_Item(i));
			if (array[i] == null)
			{
				throw new ArgumentNullException("在对象中没有找到指定属性");
			}
			num = this.Comparer(x, y, array[i]);
			if (num != 1000)
			{
				if (num != 0)
				{
					return num;
				}
			}
		}
		return num;
	}

	private int Comparer(Goods x, Goods y, PropertyInfo property)
	{
		string text = property.get_PropertyType().ToString();
		if (text != null)
		{
			if (GoodsSort.<>f__switch$map19 == null)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(2);
				dictionary.Add("System.Int32", 0);
				dictionary.Add("System.String", 1);
				GoodsSort.<>f__switch$map19 = dictionary;
			}
			int num;
			if (GoodsSort.<>f__switch$map19.TryGetValue(text, ref num))
			{
				if (num != 0)
				{
					if (num == 1)
					{
						string text2 = string.Empty;
						string text3 = string.Empty;
						if (property.GetValue(x.LocalItem, null) != null)
						{
							text2 = property.GetValue(x.LocalItem, null).ToString();
						}
						if (property.GetValue(y.LocalItem, null) != null)
						{
							text3 = property.GetValue(y.LocalItem, null).ToString();
						}
						return text2.CompareTo(text3);
					}
				}
				else
				{
					int num2 = 0;
					int num3 = 0;
					if (property.GetValue(x.LocalItem, null) != null)
					{
						num2 = Convert.ToInt32(property.GetValue(x.LocalItem, null));
					}
					if (property.GetValue(y.LocalItem, null) != null)
					{
						num3 = Convert.ToInt32(property.GetValue(y.LocalItem, null));
					}
					if (!(property.get_Name() == "secondType"))
					{
						return num2.CompareTo(num3);
					}
					num2 = BackpackManager.Instance.OnGetTypeToSort(this.goodtp, num2);
					num3 = BackpackManager.Instance.OnGetTypeToSort(this.goodtp, num3);
					if (num2 < 0 && num3 < 0)
					{
						return 1000;
					}
					return num3.CompareTo(num2);
				}
			}
		}
		return 0;
	}
}
