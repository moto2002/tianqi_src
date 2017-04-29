using System;
using System.Collections;
using System.Collections.Generic;

namespace LitJson
{
	internal class OrderedDictionaryEnumerator : IEnumerator, IDictionaryEnumerator
	{
		private IEnumerator<KeyValuePair<string, JsonData>> list_enumerator;

		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		public DictionaryEntry Entry
		{
			get
			{
				KeyValuePair<string, JsonData> current = this.list_enumerator.get_Current();
				return new DictionaryEntry(current.get_Key(), current.get_Value());
			}
		}

		public object Key
		{
			get
			{
				KeyValuePair<string, JsonData> current = this.list_enumerator.get_Current();
				return current.get_Key();
			}
		}

		public object Value
		{
			get
			{
				KeyValuePair<string, JsonData> current = this.list_enumerator.get_Current();
				return current.get_Value();
			}
		}

		public OrderedDictionaryEnumerator(IEnumerator<KeyValuePair<string, JsonData>> enumerator)
		{
			this.list_enumerator = enumerator;
		}

		public bool MoveNext()
		{
			return this.list_enumerator.MoveNext();
		}

		public void Reset()
		{
			this.list_enumerator.Reset();
		}
	}
}
