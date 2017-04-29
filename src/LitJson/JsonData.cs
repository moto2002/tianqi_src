using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace LitJson
{
	public class JsonData : IList, ICollection, IDictionary, IEnumerable, IOrderedDictionary, IJsonWrapper, IEquatable<JsonData>
	{
		private IList<JsonData> inst_array;

		private bool inst_boolean;

		private double inst_double;

		private int inst_int;

		private long inst_long;

		private IDictionary<string, JsonData> inst_object;

		private string inst_string;

		private string json;

		private JsonType type;

		private IList<KeyValuePair<string, JsonData>> object_list;

		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		bool ICollection.IsSynchronized
		{
			get
			{
				return this.EnsureCollection().get_IsSynchronized();
			}
		}

		object ICollection.SyncRoot
		{
			get
			{
				return this.EnsureCollection().get_SyncRoot();
			}
		}

		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.EnsureDictionary().get_IsFixedSize();
			}
		}

		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.EnsureDictionary().get_IsReadOnly();
			}
		}

		ICollection IDictionary.Keys
		{
			get
			{
				this.EnsureDictionary();
				IList<string> list = new List<string>();
				using (IEnumerator<KeyValuePair<string, JsonData>> enumerator = this.object_list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, JsonData> current = enumerator.get_Current();
						list.Add(current.get_Key());
					}
				}
				return (ICollection)list;
			}
		}

		ICollection IDictionary.Values
		{
			get
			{
				this.EnsureDictionary();
				IList<JsonData> list = new List<JsonData>();
				using (IEnumerator<KeyValuePair<string, JsonData>> enumerator = this.object_list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, JsonData> current = enumerator.get_Current();
						list.Add(current.get_Value());
					}
				}
				return (ICollection)list;
			}
		}

		bool IJsonWrapper.IsArray
		{
			get
			{
				return this.IsArray;
			}
		}

		bool IJsonWrapper.IsBoolean
		{
			get
			{
				return this.IsBoolean;
			}
		}

		bool IJsonWrapper.IsDouble
		{
			get
			{
				return this.IsDouble;
			}
		}

		bool IJsonWrapper.IsInt
		{
			get
			{
				return this.IsInt;
			}
		}

		bool IJsonWrapper.IsLong
		{
			get
			{
				return this.IsLong;
			}
		}

		bool IJsonWrapper.IsObject
		{
			get
			{
				return this.IsObject;
			}
		}

		bool IJsonWrapper.IsString
		{
			get
			{
				return this.IsString;
			}
		}

		bool IList.IsFixedSize
		{
			get
			{
				return this.EnsureList().get_IsFixedSize();
			}
		}

		bool IList.IsReadOnly
		{
			get
			{
				return this.EnsureList().get_IsReadOnly();
			}
		}

		object IDictionary.Item
		{
			get
			{
				return this.EnsureDictionary().get_Item(key);
			}
			set
			{
				if (!(key is string))
				{
					throw new ArgumentException("The key has to be a string");
				}
				JsonData value2 = this.ToJsonData(value);
				this[(string)key] = value2;
			}
		}

		object IOrderedDictionary.Item
		{
			get
			{
				this.EnsureDictionary();
				return this.object_list.get_Item(idx).get_Value();
			}
			set
			{
				this.EnsureDictionary();
				JsonData jsonData = this.ToJsonData(value);
				KeyValuePair<string, JsonData> keyValuePair = this.object_list.get_Item(idx);
				this.inst_object.set_Item(keyValuePair.get_Key(), jsonData);
				KeyValuePair<string, JsonData> keyValuePair2 = new KeyValuePair<string, JsonData>(keyValuePair.get_Key(), jsonData);
				this.object_list.set_Item(idx, keyValuePair2);
			}
		}

		object IList.Item
		{
			get
			{
				return this.EnsureList().get_Item(index);
			}
			set
			{
				this.EnsureList();
				JsonData value2 = this.ToJsonData(value);
				this[index] = value2;
			}
		}

		public int Count
		{
			get
			{
				return this.EnsureCollection().get_Count();
			}
		}

		public bool IsArray
		{
			get
			{
				return this.type == JsonType.Array;
			}
		}

		public bool IsBoolean
		{
			get
			{
				return this.type == JsonType.Boolean;
			}
		}

		public bool IsDouble
		{
			get
			{
				return this.type == JsonType.Double;
			}
		}

		public bool IsInt
		{
			get
			{
				return this.type == JsonType.Int;
			}
		}

		public bool IsLong
		{
			get
			{
				return this.type == JsonType.Long;
			}
		}

		public bool IsObject
		{
			get
			{
				return this.type == JsonType.Object;
			}
		}

		public bool IsString
		{
			get
			{
				return this.type == JsonType.String;
			}
		}

		public IDictionary<string, JsonData> Inst_Object
		{
			get
			{
				if (this.type == JsonType.Object)
				{
					return this.inst_object;
				}
				return null;
			}
		}

		public JsonData this[string prop_name]
		{
			get
			{
				this.EnsureDictionary();
				return this.inst_object.get_Item(prop_name);
			}
			set
			{
				this.EnsureDictionary();
				KeyValuePair<string, JsonData> keyValuePair = new KeyValuePair<string, JsonData>(prop_name, value);
				if (this.inst_object.ContainsKey(prop_name))
				{
					for (int i = 0; i < this.object_list.get_Count(); i++)
					{
						if (this.object_list.get_Item(i).get_Key() == prop_name)
						{
							this.object_list.set_Item(i, keyValuePair);
							break;
						}
					}
				}
				else
				{
					this.object_list.Add(keyValuePair);
				}
				this.inst_object.set_Item(prop_name, value);
				this.json = null;
			}
		}

		public JsonData this[int index]
		{
			get
			{
				this.EnsureCollection();
				if (this.type == JsonType.Array)
				{
					return this.inst_array.get_Item(index);
				}
				return this.object_list.get_Item(index).get_Value();
			}
			set
			{
				this.EnsureCollection();
				if (this.type == JsonType.Array)
				{
					this.inst_array.set_Item(index, value);
				}
				else
				{
					KeyValuePair<string, JsonData> keyValuePair = this.object_list.get_Item(index);
					KeyValuePair<string, JsonData> keyValuePair2 = new KeyValuePair<string, JsonData>(keyValuePair.get_Key(), value);
					this.object_list.set_Item(index, keyValuePair2);
					this.inst_object.set_Item(keyValuePair.get_Key(), value);
				}
				this.json = null;
			}
		}

		public JsonData()
		{
		}

		public JsonData(bool boolean)
		{
			this.type = JsonType.Boolean;
			this.inst_boolean = boolean;
		}

		public JsonData(double number)
		{
			this.type = JsonType.Double;
			this.inst_double = number;
		}

		public JsonData(int number)
		{
			this.type = JsonType.Int;
			this.inst_int = number;
		}

		public JsonData(long number)
		{
			this.type = JsonType.Long;
			this.inst_long = number;
		}

		public JsonData(object obj)
		{
			if (obj is bool)
			{
				this.type = JsonType.Boolean;
				this.inst_boolean = (bool)obj;
				return;
			}
			if (obj is double)
			{
				this.type = JsonType.Double;
				this.inst_double = (double)obj;
				return;
			}
			if (obj is int)
			{
				this.type = JsonType.Int;
				this.inst_int = (int)obj;
				return;
			}
			if (obj is long)
			{
				this.type = JsonType.Long;
				this.inst_long = (long)obj;
				return;
			}
			if (obj is string)
			{
				this.type = JsonType.String;
				this.inst_string = (string)obj;
				return;
			}
			throw new ArgumentException("Unable to wrap the given object with JsonData");
		}

		public JsonData(string str)
		{
			this.type = JsonType.String;
			this.inst_string = str;
		}

		void ICollection.CopyTo(Array array, int index)
		{
			this.EnsureCollection().CopyTo(array, index);
		}

		void IDictionary.Add(object key, object value)
		{
			JsonData jsonData = this.ToJsonData(value);
			this.EnsureDictionary().Add(key, jsonData);
			KeyValuePair<string, JsonData> keyValuePair = new KeyValuePair<string, JsonData>((string)key, jsonData);
			this.object_list.Add(keyValuePair);
			this.json = null;
		}

		void IDictionary.Clear()
		{
			this.EnsureDictionary().Clear();
			this.object_list.Clear();
			this.json = null;
		}

		bool IDictionary.Contains(object key)
		{
			return this.EnsureDictionary().Contains(key);
		}

		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		void IDictionary.Remove(object key)
		{
			this.EnsureDictionary().Remove(key);
			for (int i = 0; i < this.object_list.get_Count(); i++)
			{
				if (this.object_list.get_Item(i).get_Key() == (string)key)
				{
					this.object_list.RemoveAt(i);
					break;
				}
			}
			this.json = null;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.EnsureCollection().GetEnumerator();
		}

		bool IJsonWrapper.GetBoolean()
		{
			if (this.type != JsonType.Boolean)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a boolean");
			}
			return this.inst_boolean;
		}

		double IJsonWrapper.GetDouble()
		{
			if (this.type != JsonType.Double)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a double");
			}
			return this.inst_double;
		}

		int IJsonWrapper.GetInt()
		{
			if (this.type != JsonType.Int)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold an int");
			}
			return this.inst_int;
		}

		long IJsonWrapper.GetLong()
		{
			if (this.type != JsonType.Long)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a long");
			}
			return this.inst_long;
		}

		string IJsonWrapper.GetString()
		{
			if (this.type != JsonType.String)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a string");
			}
			return this.inst_string;
		}

		void IJsonWrapper.SetBoolean(bool val)
		{
			this.type = JsonType.Boolean;
			this.inst_boolean = val;
			this.json = null;
		}

		void IJsonWrapper.SetDouble(double val)
		{
			this.type = JsonType.Double;
			this.inst_double = val;
			this.json = null;
		}

		void IJsonWrapper.SetInt(int val)
		{
			this.type = JsonType.Int;
			this.inst_int = val;
			this.inst_double = (double)(this.inst_long = (long)val);
			this.json = null;
		}

		void IJsonWrapper.SetLong(long val)
		{
			this.type = JsonType.Long;
			this.inst_long = val;
			this.inst_double = (double)val;
			this.json = null;
		}

		void IJsonWrapper.SetString(string val)
		{
			this.type = JsonType.String;
			this.inst_string = val;
			this.json = null;
		}

		string IJsonWrapper.ToJson()
		{
			return this.ToJson();
		}

		void IJsonWrapper.ToJson(JsonWriter writer)
		{
			this.ToJson(writer);
		}

		int IList.Add(object value)
		{
			return this.Add(value);
		}

		void IList.Clear()
		{
			this.EnsureList().Clear();
			this.json = null;
		}

		bool IList.Contains(object value)
		{
			return this.EnsureList().Contains(value);
		}

		int IList.IndexOf(object value)
		{
			return this.EnsureList().IndexOf(value);
		}

		void IList.Insert(int index, object value)
		{
			this.EnsureList().Insert(index, value);
			this.json = null;
		}

		void IList.Remove(object value)
		{
			this.EnsureList().Remove(value);
			this.json = null;
		}

		void IList.RemoveAt(int index)
		{
			this.EnsureList().RemoveAt(index);
			this.json = null;
		}

		IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
		{
			this.EnsureDictionary();
			return new OrderedDictionaryEnumerator(this.object_list.GetEnumerator());
		}

		void IOrderedDictionary.Insert(int idx, object key, object value)
		{
			string text = (string)key;
			JsonData jsonData = this.ToJsonData(value);
			this[text] = jsonData;
			KeyValuePair<string, JsonData> keyValuePair = new KeyValuePair<string, JsonData>(text, jsonData);
			this.object_list.Insert(idx, keyValuePair);
		}

		void IOrderedDictionary.RemoveAt(int idx)
		{
			this.EnsureDictionary();
			this.inst_object.Remove(this.object_list.get_Item(idx).get_Key());
			this.object_list.RemoveAt(idx);
		}

		private ICollection EnsureCollection()
		{
			if (this.type == JsonType.Array)
			{
				return (ICollection)this.inst_array;
			}
			if (this.type == JsonType.Object)
			{
				return (ICollection)this.inst_object;
			}
			throw new InvalidOperationException("The JsonData instance has to be initialized first");
		}

		private IDictionary EnsureDictionary()
		{
			if (this.type == JsonType.Object)
			{
				return (IDictionary)this.inst_object;
			}
			if (this.type != JsonType.None)
			{
				throw new InvalidOperationException("Instance of JsonData is not a dictionary");
			}
			this.type = JsonType.Object;
			this.inst_object = new Dictionary<string, JsonData>();
			this.object_list = new List<KeyValuePair<string, JsonData>>();
			return (IDictionary)this.inst_object;
		}

		private IList EnsureList()
		{
			if (this.type == JsonType.Array)
			{
				return (IList)this.inst_array;
			}
			if (this.type != JsonType.None)
			{
				throw new InvalidOperationException("Instance of JsonData is not a list");
			}
			this.type = JsonType.Array;
			this.inst_array = new List<JsonData>();
			return (IList)this.inst_array;
		}

		private JsonData ToJsonData(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is JsonData)
			{
				return (JsonData)obj;
			}
			return new JsonData(obj);
		}

		private static void WriteJson(IJsonWrapper obj, JsonWriter writer)
		{
			if (obj.IsString)
			{
				writer.Write(obj.GetString());
				return;
			}
			if (obj.IsBoolean)
			{
				writer.Write(obj.GetBoolean());
				return;
			}
			if (obj.IsDouble)
			{
				writer.Write(obj.GetDouble());
				return;
			}
			if (obj.IsInt)
			{
				writer.Write(obj.GetInt());
				return;
			}
			if (obj.IsLong)
			{
				writer.Write(obj.GetLong());
				return;
			}
			if (obj.IsArray)
			{
				writer.WriteArrayStart();
				IEnumerator enumerator = obj.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object current = enumerator.get_Current();
						JsonData.WriteJson((JsonData)current, writer);
					}
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				writer.WriteArrayEnd();
				return;
			}
			if (obj.IsObject)
			{
				writer.WriteObjectStart();
				IDictionaryEnumerator enumerator2 = obj.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator2.get_Current();
						writer.WritePropertyName((string)dictionaryEntry.get_Key());
						JsonData.WriteJson((JsonData)dictionaryEntry.get_Value(), writer);
					}
				}
				finally
				{
					IDisposable disposable2 = enumerator2 as IDisposable;
					if (disposable2 != null)
					{
						disposable2.Dispose();
					}
				}
				writer.WriteObjectEnd();
				return;
			}
		}

		public int Add(object value)
		{
			JsonData jsonData = this.ToJsonData(value);
			this.json = null;
			return this.EnsureList().Add(jsonData);
		}

		public void Clear()
		{
			if (this.IsObject)
			{
				this.Clear();
				return;
			}
			if (this.IsArray)
			{
				this.Clear();
				return;
			}
		}

		public bool Equals(JsonData x)
		{
			if (x == null)
			{
				return false;
			}
			if (x.type != this.type)
			{
				return false;
			}
			switch (this.type)
			{
			case JsonType.None:
				return true;
			case JsonType.Object:
				return this.inst_object.Equals(x.inst_object);
			case JsonType.Array:
				return this.inst_array.Equals(x.inst_array);
			case JsonType.String:
				return this.inst_string.Equals(x.inst_string);
			case JsonType.Int:
				return this.inst_int.Equals(x.inst_int);
			case JsonType.Long:
				return this.inst_long.Equals(x.inst_long);
			case JsonType.Double:
				return this.inst_double.Equals(x.inst_double);
			case JsonType.Boolean:
				return this.inst_boolean.Equals(x.inst_boolean);
			default:
				return false;
			}
		}

		public JsonType GetJsonType()
		{
			return this.type;
		}

		public void SetJsonType(JsonType type)
		{
			if (this.type == type)
			{
				return;
			}
			switch (type)
			{
			case JsonType.Object:
				this.inst_object = new Dictionary<string, JsonData>();
				this.object_list = new List<KeyValuePair<string, JsonData>>();
				break;
			case JsonType.Array:
				this.inst_array = new List<JsonData>();
				break;
			case JsonType.String:
				this.inst_string = null;
				break;
			case JsonType.Int:
				this.inst_int = 0;
				break;
			case JsonType.Long:
				this.inst_long = 0L;
				break;
			case JsonType.Double:
				this.inst_double = 0.0;
				break;
			case JsonType.Boolean:
				this.inst_boolean = false;
				break;
			}
			this.type = type;
		}

		public string ToJson()
		{
			if (this.json != null)
			{
				return this.json;
			}
			StringWriter stringWriter = new StringWriter();
			JsonData.WriteJson(this, new JsonWriter(stringWriter)
			{
				Validate = false
			});
			this.json = stringWriter.ToString();
			return this.json;
		}

		public void ToJson(JsonWriter writer)
		{
			bool validate = writer.Validate;
			writer.Validate = false;
			JsonData.WriteJson(this, writer);
			writer.Validate = validate;
		}

		public override string ToString()
		{
			switch (this.type)
			{
			case JsonType.Object:
				return "JsonData object";
			case JsonType.Array:
				return "JsonData array";
			case JsonType.String:
				return this.inst_string;
			case JsonType.Int:
				return this.inst_int.ToString();
			case JsonType.Long:
				return this.inst_long.ToString();
			case JsonType.Double:
				return this.inst_double.ToString();
			case JsonType.Boolean:
				return this.inst_boolean.ToString();
			default:
				return "Uninitialized JsonData";
			}
		}

		public static implicit operator JsonData(bool data)
		{
			return new JsonData(data);
		}

		public static implicit operator JsonData(double data)
		{
			return new JsonData(data);
		}

		public static implicit operator JsonData(int data)
		{
			return new JsonData(data);
		}

		public static implicit operator JsonData(long data)
		{
			return new JsonData(data);
		}

		public static implicit operator JsonData(string data)
		{
			return new JsonData(data);
		}

		public static explicit operator bool(JsonData data)
		{
			if (data.type != JsonType.Boolean)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a double");
			}
			return data.inst_boolean;
		}

		public static explicit operator double(JsonData data)
		{
			if (data.type != JsonType.Double && data.type != JsonType.Int && data.type != JsonType.Long)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a double");
			}
			return data.inst_double;
		}

		public static explicit operator int(JsonData data)
		{
			if (data.type != JsonType.Int)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold an int");
			}
			return data.inst_int;
		}

		public static explicit operator long(JsonData data)
		{
			if (data.type != JsonType.Long && data.type != JsonType.Int)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold an int");
			}
			return data.inst_long;
		}

		public static explicit operator string(JsonData data)
		{
			if (data.type != JsonType.String)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a string");
			}
			return data.inst_string;
		}
	}
}
