using ProtoBuf.Serializers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace ProtoBuf.Meta
{
	public sealed class RuntimeTypeModel : TypeModel
	{
		private sealed class Singleton
		{
			internal static readonly RuntimeTypeModel Value = new RuntimeTypeModel(true);

			private Singleton()
			{
			}
		}

		private sealed class BasicType
		{
			private readonly Type type;

			private readonly IProtoSerializer serializer;

			public Type Type
			{
				get
				{
					return this.type;
				}
			}

			public IProtoSerializer Serializer
			{
				get
				{
					return this.serializer;
				}
			}

			public BasicType(Type type, IProtoSerializer serializer)
			{
				this.type = type;
				this.serializer = serializer;
			}
		}

		private const byte OPTIONS_InferTagFromNameDefault = 1;

		private const byte OPTIONS_IsDefaultModel = 2;

		private const byte OPTIONS_Frozen = 4;

		private const byte OPTIONS_AutoAddMissingTypes = 8;

		private const byte OPTIONS_UseImplicitZeroDefaults = 32;

		private const byte OPTIONS_AllowParseableTypes = 64;

		private const byte OPTIONS_AutoAddProtoContractTypesOnly = 128;

		private byte options;

		private static readonly BasicList.MatchPredicate MetaTypeFinder = new BasicList.MatchPredicate(RuntimeTypeModel.MetaTypeFinderImpl);

		private static readonly BasicList.MatchPredicate BasicTypeFinder = new BasicList.MatchPredicate(RuntimeTypeModel.BasicTypeFinderImpl);

		private BasicList basicTypes = new BasicList();

		private readonly BasicList types = new BasicList();

		private int metadataTimeoutMilliseconds = 5000;

		private int contentionCounter = 1;

		private MethodInfo defaultFactory;

		public event LockContentedEventHandler LockContended
		{
			[MethodImpl(32)]
			add
			{
				this.LockContended = (LockContentedEventHandler)Delegate.Combine(this.LockContended, value);
			}
			[MethodImpl(32)]
			remove
			{
				this.LockContended = (LockContentedEventHandler)Delegate.Remove(this.LockContended, value);
			}
		}

		public bool InferTagFromNameDefault
		{
			get
			{
				return this.GetOption(1);
			}
			set
			{
				this.SetOption(1, value);
			}
		}

		public bool AutoAddProtoContractTypesOnly
		{
			get
			{
				return this.GetOption(128);
			}
			set
			{
				this.SetOption(128, value);
			}
		}

		public bool UseImplicitZeroDefaults
		{
			get
			{
				return this.GetOption(32);
			}
			set
			{
				if (!value && this.GetOption(2))
				{
					throw new InvalidOperationException("UseImplicitZeroDefaults cannot be disabled on the default model");
				}
				this.SetOption(32, value);
			}
		}

		public bool AllowParseableTypes
		{
			get
			{
				return this.GetOption(64);
			}
			set
			{
				this.SetOption(64, value);
			}
		}

		public static RuntimeTypeModel Default
		{
			get
			{
				return RuntimeTypeModel.Singleton.Value;
			}
		}

		public MetaType this[Type type]
		{
			get
			{
				return (MetaType)this.types[this.FindOrAddAuto(type, true, false, false)];
			}
		}

		public bool AutoAddMissingTypes
		{
			get
			{
				return this.GetOption(8);
			}
			set
			{
				if (!value && this.GetOption(2))
				{
					throw new InvalidOperationException("The default model must allow missing types");
				}
				this.ThrowIfFrozen();
				this.SetOption(8, value);
			}
		}

		public int MetadataTimeoutMilliseconds
		{
			get
			{
				return this.metadataTimeoutMilliseconds;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("MetadataTimeoutMilliseconds");
				}
				this.metadataTimeoutMilliseconds = value;
			}
		}

		internal RuntimeTypeModel(bool isDefault)
		{
			this.AutoAddMissingTypes = true;
			this.UseImplicitZeroDefaults = true;
			this.SetOption(2, isDefault);
		}

		private bool GetOption(byte option)
		{
			return (this.options & option) == option;
		}

		private void SetOption(byte option, bool value)
		{
			if (value)
			{
				this.options |= option;
			}
			else
			{
				this.options &= ~option;
			}
		}

		public IEnumerable GetTypes()
		{
			return this.types;
		}

		public override string GetSchema(Type type)
		{
			BasicList basicList = new BasicList();
			MetaType metaType = null;
			bool flag = false;
			if (type == null)
			{
				BasicList.NodeEnumerator enumerator = this.types.GetEnumerator();
				while (enumerator.MoveNext())
				{
					MetaType metaType2 = (MetaType)enumerator.Current;
					MetaType surrogateOrBaseOrSelf = metaType2.GetSurrogateOrBaseOrSelf(false);
					if (!basicList.Contains(surrogateOrBaseOrSelf))
					{
						basicList.Add(surrogateOrBaseOrSelf);
						this.CascadeDependents(basicList, surrogateOrBaseOrSelf);
					}
				}
			}
			else
			{
				Type underlyingType = Helpers.GetUnderlyingType(type);
				if (underlyingType != null)
				{
					type = underlyingType;
				}
				WireType wireType;
				flag = (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out wireType, false, false, false, false) != null);
				if (!flag)
				{
					int num = this.FindOrAddAuto(type, false, false, false);
					if (num < 0)
					{
						throw new ArgumentException("The type specified is not a contract-type", "type");
					}
					metaType = ((MetaType)this.types[num]).GetSurrogateOrBaseOrSelf(false);
					basicList.Add(metaType);
					this.CascadeDependents(basicList, metaType);
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = null;
			if (!flag)
			{
				IEnumerable enumerable = (metaType != null) ? basicList : this.types;
				IEnumerator enumerator2 = enumerable.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						MetaType metaType3 = (MetaType)enumerator2.get_Current();
						if (!metaType3.IsList)
						{
							string @namespace = metaType3.Type.get_Namespace();
							if (!Helpers.IsNullOrEmpty(@namespace))
							{
								if (!@namespace.StartsWith("System."))
								{
									if (text == null)
									{
										text = @namespace;
									}
									else if (!(text == @namespace))
									{
										text = null;
										break;
									}
								}
							}
						}
					}
				}
				finally
				{
					IDisposable disposable = enumerator2 as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			if (!Helpers.IsNullOrEmpty(text))
			{
				stringBuilder.Append("package ").Append(text).Append(';');
				Helpers.AppendLine(stringBuilder);
			}
			bool flag2 = false;
			StringBuilder stringBuilder2 = new StringBuilder();
			MetaType[] array = new MetaType[basicList.Count];
			basicList.CopyTo(array, 0);
			Array.Sort<MetaType>(array, MetaType.Comparer.Default);
			if (flag)
			{
				Helpers.AppendLine(stringBuilder2).Append("message ").Append(type.get_Name()).Append(" {");
				MetaType.NewLine(stringBuilder2, 1).Append("optional ").Append(this.GetSchemaTypeName(type, DataFormat.Default, false, false, ref flag2)).Append(" value = 1;");
				Helpers.AppendLine(stringBuilder2).Append('}');
			}
			else
			{
				for (int i = 0; i < array.Length; i++)
				{
					MetaType metaType4 = array[i];
					if (!metaType4.IsList || metaType4 == metaType)
					{
						metaType4.WriteSchema(stringBuilder2, 0, ref flag2);
					}
				}
			}
			if (flag2)
			{
				stringBuilder.Append("import \"bcl.proto\"; // schema for protobuf-net's handling of core .NET types");
				Helpers.AppendLine(stringBuilder);
			}
			return Helpers.AppendLine(stringBuilder.Append(stringBuilder2)).ToString();
		}

		private void CascadeDependents(BasicList list, MetaType metaType)
		{
			if (metaType.IsList)
			{
				Type listItemType = TypeModel.GetListItemType(this, metaType.Type);
				WireType wireType;
				if (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, listItemType, out wireType, false, false, false, false) == null)
				{
					int num = this.FindOrAddAuto(listItemType, false, false, false);
					if (num >= 0)
					{
						MetaType metaType2 = ((MetaType)this.types[num]).GetSurrogateOrBaseOrSelf(false);
						if (!list.Contains(metaType2))
						{
							list.Add(metaType2);
							this.CascadeDependents(list, metaType2);
						}
					}
				}
			}
			else
			{
				MetaType metaType2;
				if (metaType.IsAutoTuple)
				{
					MemberInfo[] array;
					if (MetaType.ResolveTupleConstructor(metaType.Type, out array) != null)
					{
						for (int i = 0; i < array.Length; i++)
						{
							Type type = null;
							if (array[i] is PropertyInfo)
							{
								type = ((PropertyInfo)array[i]).get_PropertyType();
							}
							else if (array[i] is FieldInfo)
							{
								type = ((FieldInfo)array[i]).get_FieldType();
							}
							WireType wireType2;
							if (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out wireType2, false, false, false, false) == null)
							{
								int num2 = this.FindOrAddAuto(type, false, false, false);
								if (num2 >= 0)
								{
									metaType2 = ((MetaType)this.types[num2]).GetSurrogateOrBaseOrSelf(false);
									if (!list.Contains(metaType2))
									{
										list.Add(metaType2);
										this.CascadeDependents(list, metaType2);
									}
								}
							}
						}
					}
				}
				else
				{
					IEnumerator enumerator = metaType.Fields.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							ValueMember valueMember = (ValueMember)enumerator.get_Current();
							Type type2 = valueMember.ItemType;
							if (type2 == null)
							{
								type2 = valueMember.MemberType;
							}
							WireType wireType3;
							if (ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type2, out wireType3, false, false, false, false) == null)
							{
								int num3 = this.FindOrAddAuto(type2, false, false, false);
								if (num3 >= 0)
								{
									metaType2 = ((MetaType)this.types[num3]).GetSurrogateOrBaseOrSelf(false);
									if (!list.Contains(metaType2))
									{
										list.Add(metaType2);
										this.CascadeDependents(list, metaType2);
									}
								}
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
				}
				if (metaType.HasSubtypes)
				{
					SubType[] subtypes = metaType.GetSubtypes();
					for (int j = 0; j < subtypes.Length; j++)
					{
						SubType subType = subtypes[j];
						metaType2 = subType.DerivedType.GetSurrogateOrSelf();
						if (!list.Contains(metaType2))
						{
							list.Add(metaType2);
							this.CascadeDependents(list, metaType2);
						}
					}
				}
				metaType2 = metaType.BaseType;
				if (metaType2 != null)
				{
					metaType2 = metaType2.GetSurrogateOrSelf();
				}
				if (metaType2 != null && !list.Contains(metaType2))
				{
					list.Add(metaType2);
					this.CascadeDependents(list, metaType2);
				}
			}
		}

		internal MetaType FindWithoutAdd(Type type)
		{
			BasicList.NodeEnumerator enumerator = this.types.GetEnumerator();
			while (enumerator.MoveNext())
			{
				MetaType metaType = (MetaType)enumerator.Current;
				if (metaType.Type == type)
				{
					if (metaType.Pending)
					{
						this.WaitOnLock(metaType);
					}
					return metaType;
				}
			}
			Type type2 = TypeModel.ResolveProxies(type);
			return (type2 != null) ? this.FindWithoutAdd(type2) : null;
		}

		private static bool MetaTypeFinderImpl(object value, object ctx)
		{
			return ((MetaType)value).Type == (Type)ctx;
		}

		private static bool BasicTypeFinderImpl(object value, object ctx)
		{
			return ((RuntimeTypeModel.BasicType)value).Type == (Type)ctx;
		}

		private void WaitOnLock(MetaType type)
		{
			int opaqueToken = 0;
			try
			{
				this.TakeLock(ref opaqueToken);
			}
			finally
			{
				this.ReleaseLock(opaqueToken);
			}
		}

		internal IProtoSerializer TryGetBasicTypeSerializer(Type type)
		{
			int num = this.basicTypes.IndexOf(RuntimeTypeModel.BasicTypeFinder, type);
			if (num >= 0)
			{
				return ((RuntimeTypeModel.BasicType)this.basicTypes[num]).Serializer;
			}
			BasicList basicList = this.basicTypes;
			IProtoSerializer result;
			lock (basicList)
			{
				num = this.basicTypes.IndexOf(RuntimeTypeModel.BasicTypeFinder, type);
				if (num >= 0)
				{
					result = ((RuntimeTypeModel.BasicType)this.basicTypes[num]).Serializer;
				}
				else
				{
					IProtoSerializer arg_9A_0;
					if (MetaType.GetContractFamily(this, type, null) == MetaType.AttributeFamily.None)
					{
						WireType wireType;
						IProtoSerializer protoSerializer = ValueMember.TryGetCoreSerializer(this, DataFormat.Default, type, out wireType, false, false, false, false);
						arg_9A_0 = protoSerializer;
					}
					else
					{
						arg_9A_0 = null;
					}
					IProtoSerializer protoSerializer2 = arg_9A_0;
					if (protoSerializer2 != null)
					{
						this.basicTypes.Add(new RuntimeTypeModel.BasicType(type, protoSerializer2));
					}
					result = protoSerializer2;
				}
			}
			return result;
		}

		internal int FindOrAddAuto(Type type, bool demand, bool addWithContractOnly, bool addEvenIfAutoDisabled)
		{
			int num = this.types.IndexOf(RuntimeTypeModel.MetaTypeFinder, type);
			if (num >= 0)
			{
				MetaType metaType = (MetaType)this.types[num];
				if (metaType.Pending)
				{
					this.WaitOnLock(metaType);
				}
				return num;
			}
			bool flag = this.AutoAddMissingTypes || addEvenIfAutoDisabled;
			if (Helpers.IsEnum(type) || this.TryGetBasicTypeSerializer(type) == null)
			{
				Type type2 = TypeModel.ResolveProxies(type);
				if (type2 != null)
				{
					num = this.types.IndexOf(RuntimeTypeModel.MetaTypeFinder, type2);
					type = type2;
				}
				if (num < 0)
				{
					int opaqueToken = 0;
					try
					{
						this.TakeLock(ref opaqueToken);
						MetaType metaType;
						if ((metaType = this.RecogniseCommonTypes(type)) == null)
						{
							MetaType.AttributeFamily contractFamily = MetaType.GetContractFamily(this, type, null);
							if (contractFamily == MetaType.AttributeFamily.AutoTuple)
							{
								addEvenIfAutoDisabled = (flag = true);
							}
							if (!flag || (!Helpers.IsEnum(type) && addWithContractOnly && contractFamily == MetaType.AttributeFamily.None))
							{
								if (demand)
								{
									TypeModel.ThrowUnexpectedType(type);
								}
								return num;
							}
							metaType = this.Create(type);
						}
						metaType.Pending = true;
						bool flag2 = false;
						int num2 = this.types.IndexOf(RuntimeTypeModel.MetaTypeFinder, type);
						if (num2 < 0)
						{
							this.ThrowIfFrozen();
							num = this.types.Add(metaType);
							flag2 = true;
						}
						else
						{
							num = num2;
						}
						if (flag2)
						{
							metaType.ApplyDefaultBehaviour();
							metaType.Pending = false;
						}
					}
					finally
					{
						this.ReleaseLock(opaqueToken);
					}
					return num;
				}
				return num;
			}
			if (flag && !addWithContractOnly)
			{
				throw MetaType.InbuiltType(type);
			}
			return -1;
		}

		private MetaType RecogniseCommonTypes(Type type)
		{
			return null;
		}

		private MetaType Create(Type type)
		{
			this.ThrowIfFrozen();
			return new MetaType(this, type, this.defaultFactory);
		}

		public MetaType Add(Type type, bool applyDefaultBehaviour)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			MetaType metaType = this.FindWithoutAdd(type);
			if (metaType != null)
			{
				return metaType;
			}
			int opaqueToken = 0;
			if (type.get_IsInterface() && base.MapType(MetaType.ienumerable).IsAssignableFrom(type) && TypeModel.GetListItemType(this, type) == null)
			{
				throw new ArgumentException("IEnumerable[<T>] data cannot be used as a meta-type unless an Add method can be resolved");
			}
			try
			{
				metaType = this.RecogniseCommonTypes(type);
				if (metaType != null)
				{
					if (!applyDefaultBehaviour)
					{
						throw new ArgumentException("Default behaviour must be observed for certain types with special handling; " + type.get_FullName(), "applyDefaultBehaviour");
					}
					applyDefaultBehaviour = false;
				}
				if (metaType == null)
				{
					metaType = this.Create(type);
				}
				metaType.Pending = true;
				this.TakeLock(ref opaqueToken);
				if (this.FindWithoutAdd(type) != null)
				{
					throw new ArgumentException("Duplicate type", "type");
				}
				this.ThrowIfFrozen();
				this.types.Add(metaType);
				if (applyDefaultBehaviour)
				{
					metaType.ApplyDefaultBehaviour();
				}
				metaType.Pending = false;
			}
			finally
			{
				this.ReleaseLock(opaqueToken);
			}
			return metaType;
		}

		private void ThrowIfFrozen()
		{
			if (this.GetOption(4))
			{
				throw new InvalidOperationException("The model cannot be changed once frozen");
			}
		}

		public void Freeze()
		{
			if (this.GetOption(2))
			{
				throw new InvalidOperationException("The default model cannot be frozen");
			}
			this.SetOption(4, true);
		}

		protected override int GetKeyImpl(Type type)
		{
			return this.GetKey(type, false, true);
		}

		internal int GetKey(Type type, bool demand, bool getBaseKey)
		{
			int result;
			try
			{
				int num = this.FindOrAddAuto(type, demand, true, false);
				if (num >= 0)
				{
					MetaType metaType = (MetaType)this.types[num];
					if (getBaseKey)
					{
						metaType = MetaType.GetRootType(metaType);
						num = this.FindOrAddAuto(metaType.Type, true, true, false);
					}
				}
				result = num;
			}
			catch (NotSupportedException)
			{
				throw;
			}
			catch (Exception ex)
			{
				if (ex.get_Message().IndexOf(type.get_FullName()) >= 0)
				{
					throw;
				}
				throw new ProtoException(ex.get_Message() + " (" + type.get_FullName() + ")", ex);
			}
			return result;
		}

		protected internal override void Serialize(int key, object value, ProtoWriter dest)
		{
			((MetaType)this.types[key]).Serializer.Write(value, dest);
		}

		protected internal override object Deserialize(int key, object value, ProtoReader source)
		{
			IProtoSerializer serializer = ((MetaType)this.types[key]).Serializer;
			if (value == null && Helpers.IsValueType(serializer.ExpectedType))
			{
				if (serializer.RequiresOldValue)
				{
					value = Activator.CreateInstance(serializer.ExpectedType);
				}
				return serializer.Read(value, source);
			}
			return serializer.Read(value, source);
		}

		internal bool IsPrepared(Type type)
		{
			MetaType metaType = this.FindWithoutAdd(type);
			return metaType != null && metaType.IsPrepared();
		}

		internal EnumSerializer.EnumPair[] GetEnumMap(Type type)
		{
			int num = this.FindOrAddAuto(type, false, false, false);
			return (num >= 0) ? ((MetaType)this.types[num]).GetEnumMap() : null;
		}

		internal void TakeLock(ref int opaqueToken)
		{
			opaqueToken = 0;
			if (Monitor.TryEnter(this.types, this.metadataTimeoutMilliseconds))
			{
				opaqueToken = this.GetContention();
				return;
			}
			this.AddContention();
			throw new TimeoutException("Timeout while inspecting metadata; this may indicate a deadlock. This can often be avoided by preparing necessary serializers during application initialization, rather than allowing multiple threads to perform the initial metadata inspection; please also see the LockContended event");
		}

		private int GetContention()
		{
			return Interlocked.CompareExchange(ref this.contentionCounter, 0, 0);
		}

		private void AddContention()
		{
			Interlocked.Increment(ref this.contentionCounter);
		}

		internal void ReleaseLock(int opaqueToken)
		{
			if (opaqueToken != 0)
			{
				Monitor.Exit(this.types);
				if (opaqueToken != this.GetContention())
				{
					LockContentedEventHandler lockContended = this.LockContended;
					if (lockContended != null)
					{
						string stackTrace;
						try
						{
							throw new ProtoException();
						}
						catch (Exception ex)
						{
							stackTrace = ex.get_StackTrace();
						}
						lockContended(this, new LockContentedEventArgs(stackTrace));
					}
				}
			}
		}

		internal void ResolveListTypes(Type type, ref Type itemType, ref Type defaultType)
		{
			if (type == null)
			{
				return;
			}
			if (Helpers.GetTypeCode(type) != ProtoTypeCode.Unknown)
			{
				return;
			}
			if (this[type].IgnoreListHandling)
			{
				return;
			}
			if (type.get_IsArray())
			{
				if (type.GetArrayRank() != 1)
				{
					throw new NotSupportedException("Multi-dimension arrays are supported");
				}
				itemType = type.GetElementType();
				if (itemType == base.MapType(typeof(byte)))
				{
					Type type2;
					itemType = (type2 = null);
					defaultType = type2;
				}
				else
				{
					defaultType = type;
				}
			}
			if (itemType == null)
			{
				itemType = TypeModel.GetListItemType(this, type);
			}
			if (itemType != null)
			{
				Type type3 = null;
				Type type4 = null;
				this.ResolveListTypes(itemType, ref type3, ref type4);
				if (type3 != null)
				{
					throw TypeModel.CreateNestedListsNotSupported();
				}
			}
			if (itemType != null && defaultType == null)
			{
				if (type.get_IsClass() && !type.get_IsAbstract() && Helpers.GetConstructor(type, Helpers.EmptyTypes, true) != null)
				{
					defaultType = type;
				}
				if (defaultType == null && type.get_IsInterface())
				{
					Type[] genericArguments;
					if (type.get_IsGenericType() && type.GetGenericTypeDefinition() == base.MapType(typeof(IDictionary)) && itemType == base.MapType(typeof(KeyValuePair)).MakeGenericType(genericArguments = type.GetGenericArguments()))
					{
						defaultType = base.MapType(typeof(Dictionary)).MakeGenericType(genericArguments);
					}
					else
					{
						defaultType = base.MapType(typeof(List)).MakeGenericType(new Type[]
						{
							itemType
						});
					}
				}
				if (defaultType != null && !Helpers.IsAssignableFrom(type, defaultType))
				{
					defaultType = null;
				}
			}
		}

		internal string GetSchemaTypeName(Type effectiveType, DataFormat dataFormat, bool asReference, bool dynamicType, ref bool requiresBclImport)
		{
			Type underlyingType = Helpers.GetUnderlyingType(effectiveType);
			if (underlyingType != null)
			{
				effectiveType = underlyingType;
			}
			if (effectiveType == base.MapType(typeof(byte[])))
			{
				return "bytes";
			}
			WireType wireType;
			IProtoSerializer protoSerializer = ValueMember.TryGetCoreSerializer(this, dataFormat, effectiveType, out wireType, false, false, false, false);
			if (protoSerializer == null)
			{
				if (asReference || dynamicType)
				{
					requiresBclImport = true;
					return "bcl.NetObjectProxy";
				}
				return this[effectiveType].GetSurrogateOrBaseOrSelf(true).GetSchemaTypeName();
			}
			else
			{
				if (protoSerializer is ParseableSerializer)
				{
					if (asReference)
					{
						requiresBclImport = true;
					}
					return (!asReference) ? "string" : "bcl.NetObjectProxy";
				}
				ProtoTypeCode typeCode = Helpers.GetTypeCode(effectiveType);
				switch (typeCode)
				{
				case ProtoTypeCode.Boolean:
					return "bool";
				case ProtoTypeCode.Char:
				case ProtoTypeCode.Byte:
				case ProtoTypeCode.UInt16:
				case ProtoTypeCode.UInt32:
					if (dataFormat != DataFormat.FixedSize)
					{
						return "uint32";
					}
					return "fixed32";
				case ProtoTypeCode.SByte:
				case ProtoTypeCode.Int16:
				case ProtoTypeCode.Int32:
					switch (dataFormat)
					{
					case DataFormat.ZigZag:
						return "sint32";
					case DataFormat.FixedSize:
						return "sfixed32";
					}
					return "int32";
				case ProtoTypeCode.Int64:
					switch (dataFormat)
					{
					case DataFormat.ZigZag:
						return "sint64";
					case DataFormat.FixedSize:
						return "sfixed64";
					}
					return "int64";
				case ProtoTypeCode.UInt64:
					if (dataFormat != DataFormat.FixedSize)
					{
						return "uint64";
					}
					return "fixed64";
				case ProtoTypeCode.Single:
					return "float";
				case ProtoTypeCode.Double:
					return "double";
				case ProtoTypeCode.Decimal:
					requiresBclImport = true;
					return "bcl.Decimal";
				case ProtoTypeCode.DateTime:
					requiresBclImport = true;
					return "bcl.DateTime";
				case (ProtoTypeCode)17:
					IL_E5:
					switch (typeCode)
					{
					case ProtoTypeCode.TimeSpan:
						requiresBclImport = true;
						return "bcl.TimeSpan";
					case ProtoTypeCode.Guid:
						requiresBclImport = true;
						return "bcl.Guid";
					}
					throw new NotSupportedException("No .proto map found for: " + effectiveType.get_FullName());
				case ProtoTypeCode.String:
					if (asReference)
					{
						requiresBclImport = true;
					}
					return (!asReference) ? "string" : "bcl.NetObjectProxy";
				}
				goto IL_E5;
			}
		}

		public void SetDefaultFactory(MethodInfo methodInfo)
		{
			this.VerifyFactory(methodInfo, null);
			this.defaultFactory = methodInfo;
		}

		internal void VerifyFactory(MethodInfo factory, Type type)
		{
			if (factory != null)
			{
				if (type != null && Helpers.IsValueType(type))
				{
					throw new InvalidOperationException();
				}
				if (!factory.get_IsStatic())
				{
					throw new ArgumentException("A factory-method must be static", "factory");
				}
				if (type != null && factory.get_ReturnType() != type && factory.get_ReturnType() != base.MapType(typeof(object)))
				{
					throw new ArgumentException("The factory-method must return object" + ((type != null) ? (" or " + type.get_FullName()) : string.Empty), "factory");
				}
				if (!CallbackSet.CheckCallbackParameters(this, factory))
				{
					throw new ArgumentException("Invalid factory signature in " + factory.get_DeclaringType().get_FullName() + "." + factory.get_Name(), "factory");
				}
			}
		}
	}
}
