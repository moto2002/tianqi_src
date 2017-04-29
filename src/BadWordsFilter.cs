using BadWordsFilterSystem;
using GameData;
using System;
using System.Collections.Generic;

public class BadWordsFilter
{
	private static BadWordsFilter instance;

	private KeywordSearch KeywordSearch2Filter;

	public static BadWordsFilter Instance
	{
		get
		{
			if (BadWordsFilter.instance == null)
			{
				BadWordsFilter.instance = new BadWordsFilter();
			}
			return BadWordsFilter.instance;
		}
	}

	private BadWordsFilter()
	{
	}

	public void Init()
	{
		List<MinGanCiKu> dataList = DataReader<MinGanCiKu>.DataList;
		List<string> list = new List<string>();
		for (int i = 0; i < dataList.get_Count(); i++)
		{
			list.Add(dataList.get_Item(i).content);
		}
		this.KeywordSearch2Filter = new KeywordSearch(list);
	}

	public string Filter(string text)
	{
		return this.KeywordSearch2Filter.FilterKeywords(text);
	}

	public bool ExistBadWords(string text)
	{
		List<KeywordSearchResult> list = this.KeywordSearch2Filter.FindAllKeywords(text);
		return list.get_Count() > 0;
	}
}
