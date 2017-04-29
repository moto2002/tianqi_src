using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadWordsFilterSystem
{
	public class KeywordSearch
	{
		private class Node
		{
			private Dictionary<char, KeywordSearch.Node> transDict;

			public char Char
			{
				get;
				private set;
			}

			public KeywordSearch.Node Parent
			{
				get;
				private set;
			}

			public KeywordSearch.Node Failure
			{
				get;
				set;
			}

			public List<KeywordSearch.Node> Transitions
			{
				get;
				private set;
			}

			public List<string> Results
			{
				get;
				private set;
			}

			public Node(char c, KeywordSearch.Node parent)
			{
				this.Char = c;
				this.Parent = parent;
				this.Transitions = new List<KeywordSearch.Node>();
				this.Results = new List<string>();
				this.transDict = new Dictionary<char, KeywordSearch.Node>();
			}

			public void AddResult(string result)
			{
				if (!this.Results.Contains(result))
				{
					this.Results.Add(result);
				}
			}

			public void AddTransition(KeywordSearch.Node node)
			{
				this.transDict.Add(node.Char, node);
				this.Transitions = Enumerable.ToList<KeywordSearch.Node>(this.transDict.get_Values());
			}

			public KeywordSearch.Node GetTransition(char c)
			{
				KeywordSearch.Node result;
				if (this.transDict.TryGetValue(c, ref result))
				{
					return result;
				}
				return null;
			}

			public bool ContainsTransition(char c)
			{
				return this.GetTransition(c) != null;
			}
		}

		private KeywordSearch.Node root;

		private string[] keywords;

		public KeywordSearch(IEnumerable<string> keywords)
		{
			this.keywords = Enumerable.ToArray<string>(keywords);
			this.Initialize();
		}

		private void Initialize()
		{
			this.root = new KeywordSearch.Node(' ', null);
			for (int i = 0; i < this.keywords.Length; i++)
			{
				string text = this.keywords[i];
				KeywordSearch.Node node = this.root;
				string text2 = text;
				for (int j = 0; j < text2.get_Length(); j++)
				{
					char c = text2.get_Chars(j);
					KeywordSearch.Node node2 = null;
					using (List<KeywordSearch.Node>.Enumerator enumerator = node.Transitions.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeywordSearch.Node current = enumerator.get_Current();
							if (current.Char == c)
							{
								node2 = current;
								break;
							}
						}
					}
					if (node2 == null)
					{
						node2 = new KeywordSearch.Node(c, node);
						node.AddTransition(node2);
					}
					node = node2;
				}
				node.AddResult(text);
			}
			List<KeywordSearch.Node> list = new List<KeywordSearch.Node>();
			using (List<KeywordSearch.Node>.Enumerator enumerator2 = this.root.Transitions.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					KeywordSearch.Node current2 = enumerator2.get_Current();
					current2.Failure = this.root;
					using (List<KeywordSearch.Node>.Enumerator enumerator3 = current2.Transitions.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							KeywordSearch.Node current3 = enumerator3.get_Current();
							list.Add(current3);
						}
					}
				}
			}
			while (list.get_Count() != 0)
			{
				List<KeywordSearch.Node> list2 = new List<KeywordSearch.Node>();
				using (List<KeywordSearch.Node>.Enumerator enumerator4 = list.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						KeywordSearch.Node current4 = enumerator4.get_Current();
						KeywordSearch.Node failure = current4.Parent.Failure;
						char @char = current4.Char;
						while (failure != null && !failure.ContainsTransition(@char))
						{
							failure = failure.Failure;
						}
						if (failure == null)
						{
							current4.Failure = this.root;
						}
						else
						{
							current4.Failure = failure.GetTransition(@char);
							using (List<string>.Enumerator enumerator5 = current4.Failure.Results.GetEnumerator())
							{
								while (enumerator5.MoveNext())
								{
									string current5 = enumerator5.get_Current();
									current4.AddResult(current5);
								}
							}
						}
						for (int k = 0; k < current4.Transitions.get_Count(); k++)
						{
							list2.Add(current4.Transitions.get_Item(k));
						}
					}
				}
				list = list2;
			}
			this.root.Failure = this.root;
		}

		public List<KeywordSearchResult> FindAllKeywords(string text)
		{
			List<KeywordSearchResult> list = new List<KeywordSearchResult>();
			KeywordSearch.Node node = this.root;
			int i = 0;
			while (i < text.get_Length())
			{
				KeywordSearch.Node transition;
				do
				{
					transition = node.GetTransition(text.get_Chars(i));
					if (node == this.root)
					{
						break;
					}
					if (transition == null)
					{
						node = node.Failure;
					}
				}
				while (transition == null);
				IL_46:
				if (transition != null)
				{
					node = transition;
				}
				using (List<string>.Enumerator enumerator = node.Results.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string current = enumerator.get_Current();
						list.Add(new KeywordSearchResult(i - current.get_Length() + 1, current));
					}
				}
				i++;
				continue;
				goto IL_46;
			}
			return list;
		}

		public string FilterKeywords(string text)
		{
			StringBuilder stringBuilder = new StringBuilder();
			KeywordSearch.Node node = this.root;
			int i = 0;
			while (i < text.get_Length())
			{
				KeywordSearch.Node transition;
				do
				{
					transition = node.GetTransition(text.get_Chars(i));
					if (node == this.root)
					{
						break;
					}
					if (transition == null)
					{
						node = node.Failure;
					}
				}
				while (transition == null);
				IL_46:
				if (transition != null)
				{
					node = transition;
				}
				if (node.Results.get_Count() > 0)
				{
					string text2 = node.Results.get_Item(0);
					stringBuilder.Remove(stringBuilder.get_Length() - text2.get_Length() + 1, text2.get_Length() - 1);
					stringBuilder.Append(new string('*', node.Results.get_Item(0).get_Length()));
				}
				else
				{
					stringBuilder.Append(text.get_Chars(i));
				}
				i++;
				continue;
				goto IL_46;
			}
			return stringBuilder.ToString();
		}
	}
}
