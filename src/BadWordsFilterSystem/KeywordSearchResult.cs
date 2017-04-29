using System;

namespace BadWordsFilterSystem
{
	public struct KeywordSearchResult
	{
		private int index;

		private string keyword;

		public static readonly KeywordSearchResult Empty = new KeywordSearchResult(-1, string.Empty);

		public int Index
		{
			get
			{
				return this.index;
			}
		}

		public string Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		public KeywordSearchResult(int index, string keyword)
		{
			this.index = index;
			this.keyword = keyword;
		}
	}
}
