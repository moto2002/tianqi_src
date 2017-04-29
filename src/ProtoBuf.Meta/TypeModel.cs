using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ProtoBuf.Meta
{
	public abstract class TypeModel
	{
		private sealed class DeserializeItemsIterator<T> : TypeModel.DeserializeItemsIterator, IEnumerator, IDisposable, IEnumerable, IEnumerator<T>, IEnumerable<T>
		{
			public new T Current
			{
				get
				{
					return (T)((object)base.Current);
				}
			}

			public DeserializeItemsIterator(TypeModel model, Stream source, PrefixStyle style, int expectedField, SerializationContext context) : base(model, source, model.MapType(typeof(T)), style, expectedField, null, context)
			{
			}

			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return this;
			}

			void IDisposable.Dispose()
			{
			}
		}

		private class DeserializeItemsIterator : IEnumerator, IEnumerable
		{
			private bool haveObject;

			private object current;

			private readonly Stream source;

			private readonly Type type;

			private readonly PrefixStyle style;

			private readonly int expectedField;

			private readonly Serializer.TypeResolver resolver;

			private readonly TypeModel model;

			private readonly SerializationContext context;

			public object Current
			{
				get
				{
					return this.current;
				}
			}

			public DeserializeItemsIterator(TypeModel model, Stream source, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver, SerializationContext context)
			{
				this.haveObject = true;
				this.source = source;
				this.type = type;
				this.style = style;
				this.expectedField = expectedField;
				this.resolver = resolver;
				this.model = model;
				this.context = context;
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return this;
			}

			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			public bool MoveNext()
			{
				if (this.haveObject)
				{
					int num;
					this.current = this.model.DeserializeWithLengthPrefix(this.source, null, this.type, this.style, this.expectedField, this.resolver, out num, out this.haveObject, this.context);
				}
				return this.haveObject;
			}
		}

		protected internal enum CallbackType
		{
			BeforeSerialize,
			AfterSerialize,
			BeforeDeserialize,
			AfterDeserialize
		}

		private static readonly Type ilist = typeof(IList);

		public event TypeFormatEventHandler DynamicTypeFormatting
		{
			[MethodImpl(32)]
			add
			{
				this.DynamicTypeFormatting = (TypeFormatEventHandler)Delegate.Combine(this.DynamicTypeFormatting, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.DynamicTypeFormatting = (TypeFormatEventHandler)Delegate.Remove(this.DynamicTypeFormatting, value);
			}
		}

		protected internal Type MapType(Type type)
		{
			return this.MapType(type, true);
		}

		protected internal virtual Type MapType(Type type, bool demand)
		{
			return type;
		}

		private WireType GetWireType(ProtoTypeCode code, DataFormat format, ref Type type, out int modelKey)
		{
			modelKey = -1;
			if (Helpers.IsEnum(type))
			{
				modelKey = this.GetKey(ref type);
				return WireType.Variant;
			}
			switch (code)
			{
			case ProtoTypeCode.Boolean:
			case ProtoTypeCode.Char:
			case ProtoTypeCode.SByte:
			case ProtoTypeCode.Byte:
			case ProtoTypeCode.Int16:
			case ProtoTypeCode.UInt16:
			case ProtoTypeCode.Int32:
			case ProtoTypeCode.UInt32:
				return (format != DataFormat.FixedSize) ? WireType.Variant : WireType.Fixed32;
			case ProtoTypeCode.Int64:
			case ProtoTypeCode.UInt64:
				return (format != DataFormat.FixedSize) ? WireType.Variant : WireType.Fixed64;
			case ProtoTypeCode.Single:
				return WireType.Fixed32;
			case ProtoTypeCode.Double:
				return WireType.Fixed64;
			case ProtoTypeCode.Decimal:
			case ProtoTypeCode.DateTime:
			case ProtoTypeCode.String:
				return WireType.String;
			case (ProtoTypeCode)17:
				IL_66:
				switch (code)
				{
				case ProtoTypeCode.TimeSpan:
				case ProtoTypeCode.ByteArray:
				case ProtoTypeCode.Guid:
				case ProtoTypeCode.Uri:
					return WireType.String;
				default:
					if ((modelKey = this.GetKey(ref type)) >= 0)
					{
						return WireType.String;
					}
					return WireType.None;
				}
				break;
			}
			goto IL_66;
		}

		internal bool TrySerializeAuxiliaryType(ProtoWriter writer, Type type, DataFormat format, int tag, object value, bool isInsideList)
		{
			if (type == null)
			{
				type = value.GetType();
			}
			ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
			int num;
			WireType wireType = this.GetWireType(typeCode, format, ref type, out num);
			if (num < 0)
			{
				if (wireType != WireType.None)
				{
					ProtoWriter.WriteFieldHeader(tag, wireType, writer);
				}
				ProtoTypeCode protoTypeCode = typeCode;
				switch (protoTypeCode)
				{
				case ProtoTypeCode.Boolean:
					ProtoWriter.WriteBoolean((bool)value, writer);
					return true;
				case ProtoTypeCode.Char:
					ProtoWriter.WriteUInt16((ushort)((char)value), writer);
					return true;
				case ProtoTypeCode.SByte:
					ProtoWriter.WriteSByte((sbyte)value, writer);
					return true;
				case ProtoTypeCode.Byte:
					ProtoWriter.WriteByte((byte)value, writer);
					return true;
				case ProtoTypeCode.Int16:
					ProtoWriter.WriteInt16((short)value, writer);
					return true;
				case ProtoTypeCode.UInt16:
					ProtoWriter.WriteUInt16((ushort)value, writer);
					return true;
				case ProtoTypeCode.Int32:
					ProtoWriter.WriteInt32((int)value, writer);
					return true;
				case ProtoTypeCode.UInt32:
					ProtoWriter.WriteUInt32((uint)value, writer);
					return true;
				case ProtoTypeCode.Int64:
					ProtoWriter.WriteInt64((long)value, writer);
					return true;
				case ProtoTypeCode.UInt64:
					ProtoWriter.WriteUInt64((ulong)value, writer);
					return true;
				case ProtoTypeCode.Single:
					ProtoWriter.WriteSingle((float)value, writer);
					return true;
				case ProtoTypeCode.Double:
					ProtoWriter.WriteDouble((double)value, writer);
					return true;
				case ProtoTypeCode.Decimal:
					BclHelpers.WriteDecimal((decimal)value, writer);
					return true;
				case ProtoTypeCode.DateTime:
					BclHelpers.WriteDateTime((DateTime)value, writer);
					return true;
				case (ProtoTypeCode)17:
					IL_FA:
					switch (protoTypeCode)
					{
					case ProtoTypeCode.TimeSpan:
						BclHelpers.WriteTimeSpan((TimeSpan)value, writer);
						return true;
					case ProtoTypeCode.ByteArray:
						ProtoWriter.WriteBytes((byte[])value, writer);
						return true;
					case ProtoTypeCode.Guid:
						BclHelpers.WriteGuid((Guid)value, writer);
						return true;
					case ProtoTypeCode.Uri:
						ProtoWriter.WriteString(((Uri)value).get_AbsoluteUri(), writer);
						return true;
					default:
					{
						IEnumerable enumerable = value as IEnumerable;
						if (enumerable == null)
						{
							return false;
						}
						if (isInsideList)
						{
							throw TypeModel.CreateNestedListsNotSupported();
						}
						IEnumerator enumerator = enumerable.GetEnumerator();
						try
						{
							while (enumerator.MoveNext())
							{
								object current = enumerator.get_Current();
								if (current == null)
								{
									throw new NullReferenceException();
								}
								if (!this.TrySerializeAuxiliaryType(writer, null, format, tag, current, true))
								{
									TypeModel.ThrowUnexpectedType(current.GetType());
								}
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
						return true;
					}
					}
					break;
				case ProtoTypeCode.String:
					ProtoWriter.WriteString((string)value, writer);
					return true;
				}
				goto IL_FA;
			}
			if (Helpers.IsEnum(type))
			{
				this.Serialize(num, value, writer);
				return true;
			}
			ProtoWriter.WriteFieldHeader(tag, wireType, writer);
			WireType wireType2 = wireType;
			switch (wireType2 + 1)
			{
			case WireType.Variant:
				throw ProtoWriter.CreateException(writer);
			case WireType.StartGroup:
			case WireType.EndGroup:
			{
				SubItemToken token = ProtoWriter.StartSubItem(value, writer);
				this.Serialize(num, value, writer);
				ProtoWriter.EndSubItem(token, writer);
				return true;
			}
			}
			this.Serialize(num, value, writer);
			return true;
		}

		private void SerializeCore(ProtoWriter writer, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			Type type = value.GetType();
			int key = this.GetKey(ref type);
			if (key >= 0)
			{
				this.Serialize(key, value, writer);
			}
			else if (!this.TrySerializeAuxiliaryType(writer, type, DataFormat.Default, 1, value, false))
			{
				TypeModel.ThrowUnexpectedType(type);
			}
		}

		public void Serialize(Stream dest, object value)
		{
			this.Serialize(dest, value, null);
		}

		public void Serialize(Stream dest, object value, SerializationContext context)
		{
			using (ProtoWriter protoWriter = new ProtoWriter(dest, this, context))
			{
				protoWriter.SetRootObject(value);
				this.SerializeCore(protoWriter, value);
				protoWriter.Close();
			}
		}

		public void Serialize(ProtoWriter dest, object value)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			dest.CheckDepthFlushlock();
			dest.SetRootObject(value);
			this.SerializeCore(dest, value);
			dest.CheckDepthFlushlock();
			ProtoWriter.Flush(dest);
		}

		public object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style, int fieldNumber)
		{
			int num;
			return this.DeserializeWithLengthPrefix(source, value, type, style, fieldNumber, null, out num);
		}

		public object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver)
		{
			int num;
			return this.DeserializeWithLengthPrefix(source, value, type, style, expectedField, resolver, out num);
		}

		public object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver, out int bytesRead)
		{
			bool flag;
			return this.DeserializeWithLengthPrefix(source, value, type, style, expectedField, resolver, out bytesRead, out flag, null);
		}

		private object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver, out int bytesRead, out bool haveObject, SerializationContext context)
		{
			haveObject = false;
			bytesRead = 0;
			if (type == null && (style != PrefixStyle.Base128 || resolver == null))
			{
				throw new InvalidOperationException("A type must be provided unless base-128 prefixing is being used in combination with a resolver");
			}
			while (true)
			{
				bool flag = expectedField > 0 || resolver != null;
				int num2;
				int num3;
				int num = ProtoReader.ReadLengthPrefix(source, flag, style, out num2, out num3);
				if (num3 == 0)
				{
					break;
				}
				bytesRead += num3;
				if (num < 0)
				{
					return value;
				}
				bool flag2;
				if (style != PrefixStyle.Base128)
				{
					flag2 = false;
				}
				else if (flag && expectedField == 0 && type == null && resolver != null)
				{
					type = resolver(num2);
					flag2 = (type == null);
				}
				else
				{
					flag2 = (expectedField != num2);
				}
				if (flag2)
				{
					if (num == 2147483647)
					{
						goto Block_12;
					}
					ProtoReader.Seek(source, num, null);
					bytesRead += num;
				}
				if (!flag2)
				{
					goto Block_13;
				}
			}
			return value;
			Block_12:
			throw new InvalidOperationException();
			Block_13:
			ProtoReader protoReader = null;
			object result;
			try
			{
				int num;
				protoReader = ProtoReader.Create(source, this, context, num);
				int key = this.GetKey(ref type);
				if (key >= 0 && !Helpers.IsEnum(type))
				{
					value = this.Deserialize(key, value, protoReader);
				}
				else if (!this.TryDeserializeAuxiliaryType(protoReader, DataFormat.Default, 1, type, ref value, true, false, true, false) && num != 0)
				{
					TypeModel.ThrowUnexpectedType(type);
				}
				bytesRead += protoReader.Position;
				haveObject = true;
				result = value;
			}
			finally
			{
				ProtoReader.Recycle(protoReader);
			}
			return result;
		}

		public IEnumerable DeserializeItems(Stream source, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver)
		{
			return this.DeserializeItems(source, type, style, expectedField, resolver, null);
		}

		public IEnumerable DeserializeItems(Stream source, Type type, PrefixStyle style, int expectedField, Serializer.TypeResolver resolver, SerializationContext context)
		{
			return new TypeModel.DeserializeItemsIterator(this, source, type, style, expectedField, resolver, context);
		}

		public IEnumerable<T> DeserializeItems<T>(Stream source, PrefixStyle style, int expectedField)
		{
			return this.DeserializeItems<T>(source, style, expectedField, null);
		}

		public IEnumerable<T> DeserializeItems<T>(Stream source, PrefixStyle style, int expectedField, SerializationContext context)
		{
			return new TypeModel.DeserializeItemsIterator<T>(this, source, style, expectedField, context);
		}

		public void SerializeWithLengthPrefix(Stream dest, object value, Type type, PrefixStyle style, int fieldNumber)
		{
			this.SerializeWithLengthPrefix(dest, value, type, style, fieldNumber, null);
		}

		public void SerializeWithLengthPrefix(Stream dest, object value, Type type, PrefixStyle style, int fieldNumber, SerializationContext context)
		{
			if (type == null)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				type = this.MapType(value.GetType());
			}
			int key = this.GetKey(ref type);
			using (ProtoWriter protoWriter = new ProtoWriter(dest, this, context))
			{
				switch (style)
				{
				case PrefixStyle.None:
					this.Serialize(key, value, protoWriter);
					break;
				case PrefixStyle.Base128:
				case PrefixStyle.Fixed32:
				case PrefixStyle.Fixed32BigEndian:
					ProtoWriter.WriteObject(value, key, protoWriter, style, fieldNumber);
					break;
				default:
					throw new ArgumentOutOfRangeException("style");
				}
				protoWriter.Close();
			}
		}

		public object Deserialize(Stream source, object value, Type type)
		{
			return this.Deserialize(source, value, type, null);
		}

		public object Deserialize(Stream source, object value, Type type, SerializationContext context)
		{
			bool noAutoCreate = this.PrepareDeserialize(value, ref type);
			ProtoReader protoReader = null;
			object result;
			try
			{
				protoReader = ProtoReader.Create(source, this, context, -1);
				if (value != null)
				{
					protoReader.SetRootObject(value);
				}
				object obj = this.DeserializeCore(protoReader, type, value, noAutoCreate);
				protoReader.CheckFullyConsumed();
				result = obj;
			}
			finally
			{
				ProtoReader.Recycle(protoReader);
			}
			return result;
		}

		private bool PrepareDeserialize(object value, ref Type type)
		{
			if (type == null)
			{
				if (value == null)
				{
					throw new ArgumentNullException("type");
				}
				type = this.MapType(value.GetType());
			}
			bool result = true;
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
				result = false;
			}
			return result;
		}

		public object Deserialize(Stream source, object value, Type type, int length)
		{
			return this.Deserialize(source, value, type, length, null);
		}

		public object Deserialize(Stream source, object value, Type type, int length, SerializationContext context)
		{
			bool noAutoCreate = this.PrepareDeserialize(value, ref type);
			ProtoReader protoReader = null;
			object result;
			try
			{
				protoReader = ProtoReader.Create(source, this, context, length);
				if (value != null)
				{
					protoReader.SetRootObject(value);
				}
				object obj = this.DeserializeCore(protoReader, type, value, noAutoCreate);
				protoReader.CheckFullyConsumed();
				result = obj;
			}
			finally
			{
				ProtoReader.Recycle(protoReader);
			}
			return result;
		}

		public object Deserialize(ProtoReader source, object value, Type type)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			bool noAutoCreate = this.PrepareDeserialize(value, ref type);
			if (value != null)
			{
				source.SetRootObject(value);
			}
			object result = this.DeserializeCore(source, type, value, noAutoCreate);
			source.CheckFullyConsumed();
			return result;
		}

		private object DeserializeCore(ProtoReader reader, Type type, object value, bool noAutoCreate)
		{
			int key = this.GetKey(ref type);
			if (key >= 0 && !Helpers.IsEnum(type))
			{
				return this.Deserialize(key, value, reader);
			}
			this.TryDeserializeAuxiliaryType(reader, DataFormat.Default, 1, type, ref value, true, false, noAutoCreate, false);
			return value;
		}

		internal static MethodInfo ResolveListAdd(TypeModel model, Type listType, Type itemType, out bool isList)
		{
			isList = model.MapType(TypeModel.ilist).IsAssignableFrom(listType);
			Type[] array = new Type[]
			{
				itemType
			};
			MethodInfo instanceMethod = Helpers.GetInstanceMethod(listType, "Add", array);
			if (instanceMethod == null)
			{
				bool flag = listType.get_IsInterface() && listType == model.MapType(typeof(IEnumerable)).MakeGenericType(array);
				Type type = model.MapType(typeof(ICollection)).MakeGenericType(array);
				if (flag || type.IsAssignableFrom(listType))
				{
					instanceMethod = Helpers.GetInstanceMethod(type, "Add", array);
				}
			}
			if (instanceMethod == null)
			{
				Type[] interfaces = listType.GetInterfaces();
				for (int i = 0; i < interfaces.Length; i++)
				{
					Type type2 = interfaces[i];
					if (type2.get_Name() == "IProducerConsumerCollection`1" && type2.get_IsGenericType() && type2.GetGenericTypeDefinition().get_FullName() == "System.Collections.Concurrent.IProducerConsumerCollection`1")
					{
						instanceMethod = Helpers.GetInstanceMethod(type2, "TryAdd", array);
						if (instanceMethod != null)
						{
							break;
						}
					}
				}
			}
			if (instanceMethod == null)
			{
				array[0] = model.MapType(typeof(object));
				instanceMethod = Helpers.GetInstanceMethod(listType, "Add", array);
			}
			if (instanceMethod == null && isList)
			{
				instanceMethod = Helpers.GetInstanceMethod(model.MapType(TypeModel.ilist), "Add", array);
			}
			return instanceMethod;
		}

		internal static Type GetListItemType(TypeModel model, Type listType)
		{
			if (listType == model.MapType(typeof(string)) || listType.get_IsArray() || !model.MapType(typeof(IEnumerable)).IsAssignableFrom(listType))
			{
				return null;
			}
			BasicList basicList = new BasicList();
			MethodInfo[] methods = listType.GetMethods();
			for (int i = 0; i < methods.Length; i++)
			{
				MethodInfo methodInfo = methods[i];
				if (!methodInfo.get_IsStatic() && !(methodInfo.get_Name() != "Add"))
				{
					ParameterInfo[] parameters = methodInfo.GetParameters();
					Type parameterType;
					if (parameters.Length == 1 && !basicList.Contains(parameterType = parameters[0].get_ParameterType()))
					{
						basicList.Add(parameterType);
					}
				}
			}
			string name = listType.get_Name();
			if (name == null || (name.IndexOf("Queue") < 0 && name.IndexOf("Stack") < 0))
			{
				TypeModel.TestEnumerableListPatterns(model, basicList, listType);
				Type[] interfaces = listType.GetInterfaces();
				for (int j = 0; j < interfaces.Length; j++)
				{
					Type iType = interfaces[j];
					TypeModel.TestEnumerableListPatterns(model, basicList, iType);
				}
			}
			PropertyInfo[] properties = listType.GetProperties(52);
			for (int k = 0; k < properties.Length; k++)
			{
				PropertyInfo propertyInfo = properties[k];
				if (!(propertyInfo.get_Name() != "Item") && !basicList.Contains(propertyInfo.get_PropertyType()))
				{
					ParameterInfo[] indexParameters = propertyInfo.GetIndexParameters();
					if (indexParameters.Length == 1 && indexParameters[0].get_ParameterType() == model.MapType(typeof(int)))
					{
						basicList.Add(propertyInfo.get_PropertyType());
					}
				}
			}
			switch (basicList.Count)
			{
			case 0:
				return null;
			case 1:
				return (Type)basicList[0];
			case 2:
				if (TypeModel.CheckDictionaryAccessors(model, (Type)basicList[0], (Type)basicList[1]))
				{
					return (Type)basicList[0];
				}
				if (TypeModel.CheckDictionaryAccessors(model, (Type)basicList[1], (Type)basicList[0]))
				{
					return (Type)basicList[1];
				}
				break;
			}
			return null;
		}

		private static void TestEnumerableListPatterns(TypeModel model, BasicList candidates, Type iType)
		{
			if (iType.get_IsGenericType())
			{
				Type genericTypeDefinition = iType.GetGenericTypeDefinition();
				if (genericTypeDefinition == model.MapType(typeof(IEnumerable)) || genericTypeDefinition == model.MapType(typeof(ICollection)) || genericTypeDefinition.get_FullName() == "System.Collections.Concurrent.IProducerConsumerCollection`1")
				{
					Type[] genericArguments = iType.GetGenericArguments();
					if (!candidates.Contains(genericArguments[0]))
					{
						candidates.Add(genericArguments[0]);
					}
				}
			}
		}

		private static bool CheckDictionaryAccessors(TypeModel model, Type pair, Type value)
		{
			return pair.get_IsGenericType() && pair.GetGenericTypeDefinition() == model.MapType(typeof(KeyValuePair)) && pair.GetGenericArguments()[1] == value;
		}

		private bool TryDeserializeList(TypeModel model, ProtoReader reader, DataFormat format, int tag, Type listType, Type itemType, ref object value)
		{
			bool flag;
			MethodInfo methodInfo = TypeModel.ResolveListAdd(model, listType, itemType, out flag);
			if (methodInfo == null)
			{
				throw new NotSupportedException("Unknown list variant: " + listType.get_FullName());
			}
			bool result = false;
			object obj = null;
			IList list = value as IList;
			object[] array = (!flag) ? new object[1] : null;
			BasicList basicList = (!listType.get_IsArray()) ? null : new BasicList();
			while (this.TryDeserializeAuxiliaryType(reader, format, tag, itemType, ref obj, true, true, true, true))
			{
				result = true;
				if (value == null && basicList == null)
				{
					value = TypeModel.CreateListInstance(listType, itemType);
					list = (value as IList);
				}
				if (list != null)
				{
					list.Add(obj);
				}
				else if (basicList != null)
				{
					basicList.Add(obj);
				}
				else
				{
					array[0] = obj;
					methodInfo.Invoke(value, array);
				}
				obj = null;
			}
			if (basicList != null)
			{
				if (value != null)
				{
					if (basicList.Count != 0)
					{
						Array array2 = (Array)value;
						Array array3 = Array.CreateInstance(itemType, array2.get_Length() + basicList.Count);
						Array.Copy(array2, array3, array2.get_Length());
						basicList.CopyTo(array3, array2.get_Length());
						value = array3;
					}
				}
				else
				{
					Array array3 = Array.CreateInstance(itemType, basicList.Count);
					basicList.CopyTo(array3, 0);
					value = array3;
				}
			}
			return result;
		}

		private static object CreateListInstance(Type listType, Type itemType)
		{
			Type type = listType;
			if (listType.get_IsArray())
			{
				return Array.CreateInstance(itemType, 0);
			}
			if (!listType.get_IsClass() || listType.get_IsAbstract() || Helpers.GetConstructor(listType, Helpers.EmptyTypes, true) == null)
			{
				bool flag = false;
				string fullName;
				if (listType.get_IsInterface() && (fullName = listType.get_FullName()) != null && fullName.IndexOf("Dictionary") >= 0)
				{
					if (listType.get_IsGenericType() && listType.GetGenericTypeDefinition() == typeof(IDictionary))
					{
						Type[] genericArguments = listType.GetGenericArguments();
						type = typeof(Dictionary).MakeGenericType(genericArguments);
						flag = true;
					}
					if (!flag && listType == typeof(IDictionary))
					{
						type = typeof(Hashtable);
						flag = true;
					}
				}
				if (!flag)
				{
					type = typeof(List).MakeGenericType(new Type[]
					{
						itemType
					});
					flag = true;
				}
				if (!flag)
				{
					type = typeof(ArrayList);
				}
			}
			return Activator.CreateInstance(type);
		}

		internal bool TryDeserializeAuxiliaryType(ProtoReader reader, DataFormat format, int tag, Type type, ref object value, bool skipOtherFields, bool asListItem, bool autoCreate, bool insideList)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
			int num;
			WireType wireType = this.GetWireType(typeCode, format, ref type, out num);
			bool flag = false;
			if (wireType == WireType.None)
			{
				Type type2 = TypeModel.GetListItemType(this, type);
				if (type2 == null && type.get_IsArray() && type.GetArrayRank() == 1 && type != typeof(byte[]))
				{
					type2 = type.GetElementType();
				}
				if (type2 != null)
				{
					if (insideList)
					{
						throw TypeModel.CreateNestedListsNotSupported();
					}
					flag = this.TryDeserializeList(this, reader, format, tag, type, type2, ref value);
					if (!flag && autoCreate)
					{
						value = TypeModel.CreateListInstance(type, type2);
					}
					return flag;
				}
				else
				{
					TypeModel.ThrowUnexpectedType(type);
				}
			}
			while (!flag || !asListItem)
			{
				int num2 = reader.ReadFieldHeader();
				if (num2 <= 0)
				{
					IL_35C:
					if (!flag && !asListItem && autoCreate && type != typeof(string))
					{
						value = Activator.CreateInstance(type);
					}
					return flag;
				}
				if (num2 != tag)
				{
					if (!skipOtherFields)
					{
						throw ProtoReader.AddErrorData(new InvalidOperationException("Expected field " + tag.ToString() + ", but found " + num2.ToString()), reader);
					}
					reader.SkipField();
				}
				else
				{
					flag = true;
					reader.Hint(wireType);
					if (num < 0)
					{
						ProtoTypeCode protoTypeCode = typeCode;
						switch (protoTypeCode)
						{
						case ProtoTypeCode.Boolean:
							value = reader.ReadBoolean();
							continue;
						case ProtoTypeCode.Char:
							value = (char)reader.ReadUInt16();
							continue;
						case ProtoTypeCode.SByte:
							value = reader.ReadSByte();
							continue;
						case ProtoTypeCode.Byte:
							value = reader.ReadByte();
							continue;
						case ProtoTypeCode.Int16:
							value = reader.ReadInt16();
							continue;
						case ProtoTypeCode.UInt16:
							value = reader.ReadUInt16();
							continue;
						case ProtoTypeCode.Int32:
							value = reader.ReadInt32();
							continue;
						case ProtoTypeCode.UInt32:
							value = reader.ReadUInt32();
							continue;
						case ProtoTypeCode.Int64:
							value = reader.ReadInt64();
							continue;
						case ProtoTypeCode.UInt64:
							value = reader.ReadUInt64();
							continue;
						case ProtoTypeCode.Single:
							value = reader.ReadSingle();
							continue;
						case ProtoTypeCode.Double:
							value = reader.ReadDouble();
							continue;
						case ProtoTypeCode.Decimal:
							value = BclHelpers.ReadDecimal(reader);
							continue;
						case ProtoTypeCode.DateTime:
							value = BclHelpers.ReadDateTime(reader);
							continue;
						case (ProtoTypeCode)17:
							IL_1D1:
							switch (protoTypeCode)
							{
							case ProtoTypeCode.TimeSpan:
								value = BclHelpers.ReadTimeSpan(reader);
								continue;
							case ProtoTypeCode.ByteArray:
								value = ProtoReader.AppendBytes((byte[])value, reader);
								continue;
							case ProtoTypeCode.Guid:
								value = BclHelpers.ReadGuid(reader);
								continue;
							case ProtoTypeCode.Uri:
								value = new Uri(reader.ReadString());
								continue;
							default:
								continue;
							}
							break;
						case ProtoTypeCode.String:
							value = reader.ReadString();
							continue;
						}
						goto IL_1D1;
					}
					WireType wireType2 = wireType;
					if (wireType2 != WireType.String && wireType2 != WireType.StartGroup)
					{
						value = this.Deserialize(num, value, reader);
					}
					else
					{
						SubItemToken token = ProtoReader.StartSubItem(reader);
						value = this.Deserialize(num, value, reader);
						ProtoReader.EndSubItem(token, reader);
					}
				}
			}
			goto IL_35C;
		}

		public static RuntimeTypeModel Create()
		{
			return new RuntimeTypeModel(false);
		}

		protected internal static Type ResolveProxies(Type type)
		{
			if (type == null)
			{
				return null;
			}
			if (type.get_IsGenericParameter())
			{
				return null;
			}
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				return underlyingType;
			}
			string fullName = type.get_FullName();
			if (fullName != null && fullName.StartsWith("System.Data.Entity.DynamicProxies."))
			{
				return type.get_BaseType();
			}
			Type[] interfaces = type.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				string fullName2 = interfaces[i].get_FullName();
				if (fullName2 != null)
				{
					if (TypeModel.<>f__switch$mapF == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
						dictionary.Add("NHibernate.Proxy.INHibernateProxy", 0);
						dictionary.Add("NHibernate.Proxy.DynamicProxy.IProxy", 0);
						dictionary.Add("NHibernate.Intercept.IFieldInterceptorAccessor", 0);
						TypeModel.<>f__switch$mapF = dictionary;
					}
					int num;
					if (TypeModel.<>f__switch$mapF.TryGetValue(fullName2, ref num))
					{
						if (num == 0)
						{
							return type.get_BaseType();
						}
					}
				}
			}
			return null;
		}

		public bool IsDefined(Type type)
		{
			return this.GetKey(ref type) >= 0;
		}

		protected internal int GetKey(ref Type type)
		{
			if (type == null)
			{
				return -1;
			}
			int keyImpl = this.GetKeyImpl(type);
			if (keyImpl < 0)
			{
				Type type2 = TypeModel.ResolveProxies(type);
				if (type2 != null)
				{
					type = type2;
					keyImpl = this.GetKeyImpl(type);
				}
			}
			return keyImpl;
		}

		protected abstract int GetKeyImpl(Type type);

		protected internal abstract void Serialize(int key, object value, ProtoWriter dest);

		protected internal abstract object Deserialize(int key, object value, ProtoReader source);

		public object DeepClone(object value)
		{
			if (value == null)
			{
				return null;
			}
			Type type = value.GetType();
			int key = this.GetKey(ref type);
			object result;
			if (key >= 0 && !Helpers.IsEnum(type))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (ProtoWriter protoWriter = new ProtoWriter(memoryStream, this, null))
					{
						protoWriter.SetRootObject(value);
						this.Serialize(key, value, protoWriter);
						protoWriter.Close();
					}
					memoryStream.set_Position(0L);
					ProtoReader protoReader = null;
					try
					{
						protoReader = ProtoReader.Create(memoryStream, this, null, -1);
						result = this.Deserialize(key, null, protoReader);
						return result;
					}
					finally
					{
						ProtoReader.Recycle(protoReader);
					}
				}
			}
			if (type == typeof(byte[]))
			{
				byte[] array = (byte[])value;
				byte[] array2 = new byte[array.Length];
				Helpers.BlockCopy(array, 0, array2, 0, array.Length);
				return array2;
			}
			int num;
			if (this.GetWireType(Helpers.GetTypeCode(type), DataFormat.Default, ref type, out num) != WireType.None && num < 0)
			{
				return value;
			}
			using (MemoryStream memoryStream2 = new MemoryStream())
			{
				using (ProtoWriter protoWriter2 = new ProtoWriter(memoryStream2, this, null))
				{
					if (!this.TrySerializeAuxiliaryType(protoWriter2, type, DataFormat.Default, 1, value, false))
					{
						TypeModel.ThrowUnexpectedType(type);
					}
					protoWriter2.Close();
				}
				memoryStream2.set_Position(0L);
				ProtoReader reader = null;
				try
				{
					reader = ProtoReader.Create(memoryStream2, this, null, -1);
					value = null;
					this.TryDeserializeAuxiliaryType(reader, DataFormat.Default, 1, type, ref value, true, false, true, false);
					result = value;
				}
				finally
				{
					ProtoReader.Recycle(reader);
				}
			}
			return result;
		}

		protected internal static void ThrowUnexpectedSubtype(Type expected, Type actual)
		{
			if (expected != TypeModel.ResolveProxies(actual))
			{
				throw new InvalidOperationException("Unexpected sub-type: " + actual.get_FullName());
			}
		}

		protected internal static void ThrowUnexpectedType(Type type)
		{
			string text = (type != null) ? type.get_FullName() : "(unknown)";
			if (type != null)
			{
				Type baseType = type.get_BaseType();
				if (baseType != null && baseType.get_IsGenericType() && baseType.GetGenericTypeDefinition().get_Name() == "GeneratedMessage`2")
				{
					throw new InvalidOperationException("Are you mixing protobuf-net and protobuf-csharp-port? See http://stackoverflow.com/q/11564914; type: " + text);
				}
			}
			throw new InvalidOperationException("Type is not expected, and no contract can be inferred: " + text);
		}

		internal static Exception CreateNestedListsNotSupported()
		{
			return new NotSupportedException("Nested or jagged lists and arrays are not supported");
		}

		public static void ThrowCannotCreateInstance(Type type)
		{
			throw new ProtoException("No parameterless constructor found for " + ((type != null) ? type.get_Name() : "(null)"));
		}

		internal static string SerializeType(TypeModel model, Type type)
		{
			if (model != null)
			{
				TypeFormatEventHandler dynamicTypeFormatting = model.DynamicTypeFormatting;
				if (dynamicTypeFormatting != null)
				{
					TypeFormatEventArgs typeFormatEventArgs = new TypeFormatEventArgs(type);
					dynamicTypeFormatting(model, typeFormatEventArgs);
					if (!Helpers.IsNullOrEmpty(typeFormatEventArgs.FormattedName))
					{
						return typeFormatEventArgs.FormattedName;
					}
				}
			}
			return type.get_AssemblyQualifiedName();
		}

		internal static Type DeserializeType(TypeModel model, string value)
		{
			if (model != null)
			{
				TypeFormatEventHandler dynamicTypeFormatting = model.DynamicTypeFormatting;
				if (dynamicTypeFormatting != null)
				{
					TypeFormatEventArgs typeFormatEventArgs = new TypeFormatEventArgs(value);
					dynamicTypeFormatting(model, typeFormatEventArgs);
					if (typeFormatEventArgs.Type != null)
					{
						return typeFormatEventArgs.Type;
					}
				}
			}
			return Type.GetType(value);
		}

		public bool CanSerializeContractType(Type type)
		{
			return this.CanSerialize(type, false, true, true);
		}

		public bool CanSerialize(Type type)
		{
			return this.CanSerialize(type, true, true, true);
		}

		public bool CanSerializeBasicType(Type type)
		{
			return this.CanSerialize(type, true, false, true);
		}

		private bool CanSerialize(Type type, bool allowBasic, bool allowContract, bool allowLists)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
			ProtoTypeCode protoTypeCode = typeCode;
			if (protoTypeCode != ProtoTypeCode.Empty && protoTypeCode != ProtoTypeCode.Unknown)
			{
				return allowBasic;
			}
			int key = this.GetKey(ref type);
			if (key >= 0)
			{
				return allowContract;
			}
			if (allowLists)
			{
				Type type2 = null;
				if (type.get_IsArray())
				{
					if (type.GetArrayRank() == 1)
					{
						type2 = type.GetElementType();
					}
				}
				else
				{
					type2 = TypeModel.GetListItemType(this, type);
				}
				if (type2 != null)
				{
					return this.CanSerialize(type2, allowBasic, allowContract, false);
				}
			}
			return false;
		}

		public virtual string GetSchema(Type type)
		{
			throw new NotSupportedException();
		}

		internal virtual Type GetType(string fullName, Assembly context)
		{
			return TypeModel.ResolveKnownType(fullName, this, context);
		}

		[MethodImpl(8)]
		internal static Type ResolveKnownType(string name, TypeModel model, Assembly assembly)
		{
			if (Helpers.IsNullOrEmpty(name))
			{
				return null;
			}
			try
			{
				Type type = Type.GetType(name);
				if (type != null)
				{
					Type result = type;
					return result;
				}
			}
			catch
			{
			}
			try
			{
				int num = name.IndexOf(',');
				string text = ((num <= 0) ? name : name.Substring(0, num)).Trim();
				if (assembly == null)
				{
					assembly = Assembly.GetCallingAssembly();
				}
				Type type2 = (assembly != null) ? assembly.GetType(text) : null;
				if (type2 != null)
				{
					Type result = type2;
					return result;
				}
			}
			catch
			{
			}
			return null;
		}
	}
}
