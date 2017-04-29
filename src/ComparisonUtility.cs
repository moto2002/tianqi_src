using GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ComparisonUtility
{
	public class ItemComparer : IComparer<Items>
	{
		public int Compare(Items x, Items y)
		{
			if (x == null || y == null)
			{
				Debug.LogError("ItemComparer比较对象为空");
				return 0;
			}
			if (x.color > y.color)
			{
				return -1;
			}
			if (x.color < y.color)
			{
				return 1;
			}
			if (x.step > y.step)
			{
				return -1;
			}
			if (x.step < y.step)
			{
				return 1;
			}
			return 0;
		}
	}

	public class CityDataComparer : IComparer<ZhuChengPeiZhi>
	{
		public int Compare(ZhuChengPeiZhi x, ZhuChengPeiZhi y)
		{
			if (x == null || y == null)
			{
				Debug.LogError("ItemComparer比较对象为空");
				return 0;
			}
			if (x.sort > y.sort)
			{
				return 1;
			}
			if (x.sort < y.sort)
			{
				return -1;
			}
			return 0;
		}
	}

	public class RadarItemComparer : IComparer<RadarItemMessage>
	{
		public int Compare(RadarItemMessage x, RadarItemMessage y)
		{
			if (x.sortWeight > y.sortWeight)
			{
				return 1;
			}
			if (x.sortWeight < y.sortWeight)
			{
				return -1;
			}
			return 0;
		}
	}

	public class DarkLevelComparer : IComparer<DarkLevel>
	{
		public int Compare(DarkLevel x, DarkLevel y)
		{
			if (x.Lv > y.Lv)
			{
				return 1;
			}
			if (x.Lv < y.Lv)
			{
				return -1;
			}
			return 0;
		}
	}

	private static ComparisonUtility.ItemComparer itemComparison;

	private static ComparisonUtility.CityDataComparer cityDataComparison;

	private static ComparisonUtility.RadarItemComparer radarItemComparison;

	private static ComparisonUtility.DarkLevelComparer darkLevelComparison;

	public static ComparisonUtility.ItemComparer ItemComparison
	{
		get
		{
			if (ComparisonUtility.itemComparison == null)
			{
				ComparisonUtility.itemComparison = new ComparisonUtility.ItemComparer();
			}
			return ComparisonUtility.itemComparison;
		}
	}

	public static ComparisonUtility.CityDataComparer CityDataComparison
	{
		get
		{
			if (ComparisonUtility.cityDataComparison == null)
			{
				ComparisonUtility.cityDataComparison = new ComparisonUtility.CityDataComparer();
			}
			return ComparisonUtility.cityDataComparison;
		}
	}

	public static ComparisonUtility.RadarItemComparer RadarItemComparison
	{
		get
		{
			if (ComparisonUtility.radarItemComparison == null)
			{
				ComparisonUtility.radarItemComparison = new ComparisonUtility.RadarItemComparer();
			}
			return ComparisonUtility.radarItemComparison;
		}
	}

	public static ComparisonUtility.DarkLevelComparer DarkLevelComparison
	{
		get
		{
			if (ComparisonUtility.darkLevelComparison == null)
			{
				ComparisonUtility.darkLevelComparison = new ComparisonUtility.DarkLevelComparer();
			}
			return ComparisonUtility.darkLevelComparison;
		}
	}
}
