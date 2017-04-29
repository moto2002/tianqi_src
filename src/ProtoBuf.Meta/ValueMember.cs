using ProtoBuf.Serializers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace ProtoBuf.Meta
{
	public class ValueMember
	{
		internal sealed class Comparer : IComparer, IComparer<ValueMember>
		{
			public static readonly ValueMember.Comparer Default = new ValueMember.Comparer();

			public int Compare(object x, object y)
			{
				return this.Compare(x as ValueMember, y as ValueMember);
			}

			public int Compare(ValueMember x, ValueMember y)
			{
				if (object.ReferenceEquals(x, y))
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				return x.FieldNumber.CompareTo(y.FieldNumber);
			}
		}

		private const byte OPTIONS_IsStrict = 1;

		private const byte OPTIONS_IsPacked = 2;

		private const byte OPTIONS_IsRequired = 4;

		private const byte OPTIONS_OverwriteList = 8;

		private const byte OPTIONS_SupportNull = 16;

		private readonly int fieldNumber;

		private readonly MemberInfo member;

		private readonly Type parentType;

		private readonly Type itemType;

		private readonly Type defaultType;

		private readonly Type memberType;

		private object defaultValue;

		private readonly RuntimeTypeModel model;

		private IProtoSerializer serializer;

		private DataFormat dataFormat;

		private bool asReference;

		private bool dynamicType;

		private MethodInfo getSpecified;

		private MethodInfo setSpecified;

		private string name;

		private byte flags;

		public int FieldNumber
		{
			get
			{
				return this.fieldNumber;
			}
		}

		public MemberInfo Member
		{
			get
			{
				return this.member;
			}
		}

		public Type ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		public Type MemberType
		{
			get
			{
				return this.memberType;
			}
		}

		public Type DefaultType
		{
			get
			{
				return this.defaultType;
			}
		}

		public Type ParentType
		{
			get
			{
				return this.parentType;
			}
		}

		public object DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.ThrowIfFrozen();
				this.defaultValue = value;
			}
		}

		internal IProtoSerializer Serializer
		{
			get
			{
				if (this.serializer == null)
				{
					this.serializer = this.BuildSerializer();
				}
				return this.serializer;
			}
		}

		public DataFormat DataFormat
		{
			get
			{
				return this.dataFormat;
			}
			set
			{
				this.ThrowIfFrozen();
				this.dataFormat = value;
			}
		}

		public bool IsStrict
		{
			get
			{
				return this.HasFlag(1);
			}
			set
			{
				this.SetFlag(1, value, true);
			}
		}

		public bool IsPacked
		{
			get
			{
				return this.HasFlag(2);
			}
			set
			{
				this.SetFlag(2, value, true);
			}
		}

		public bool OverwriteList
		{
			get
			{
				return this.HasFlag(8);
			}
			set
			{
				this.SetFlag(8, value, true);
			}
		}

		public bool IsRequired
		{
			get
			{
				return this.HasFlag(4);
			}
			set
			{
				this.SetFlag(4, value, true);
			}
		}

		public bool AsReference
		{
			get
			{
				return this.asReference;
			}
			set
			{
				this.ThrowIfFrozen();
				this.asReference = value;
			}
		}

		public bool DynamicType
		{
			get
			{
				return this.dynamicType;
			}
			set
			{
				this.ThrowIfFrozen();
				this.dynamicType = value;
			}
		}

		public string Name
		{
			get
			{
				return (!Helpers.IsNullOrEmpty(this.name)) ? this.name : this.member.get_Name();
			}
		}

		public bool SupportNull
		{
			get
			{
				return this.HasFlag(16);
			}
			set
			{
				this.SetFlag(16, value, true);
			}
		}

		public ValueMember(RuntimeTypeModel model, Type parentType, int fieldNumber, MemberInfo member, Type memberType, Type itemType, Type defaultType, DataFormat dataFormat, object defaultValue) : this(model, fieldNumber, memberType, itemType, defaultType, dataFormat)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			if (parentType == null)
			{
				throw new ArgumentNullException("parentType");
			}
			if (fieldNumber < 1 && !Helpers.IsEnum(parentType))
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			this.member = member;
			this.parentType = parentType;
			if (fieldNumber < 1 && !Helpers.IsEnum(parentType))
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if (defaultValue != null && model.MapType(defaultValue.GetType()) != memberType)
			{
				defaultValue = ValueMember.ParseDefaultValue(memberType, defaultValue);
			}
			this.defaultValue = defaultValue;
			MetaType metaType = model.FindWithoutAdd(memberType);
			if (metaType != null)
			{
				this.asReference = metaType.AsReferenceDefault;
			}
			else
			{
				this.asReference = MetaType.GetAsReferenceDefault(model, memberType);
			}
		}

		internal ValueMember(RuntimeTypeModel model, int fieldNumber, Type memberType, Type itemType, Type defaultType, DataFormat dataFormat)
		{
			if (memberType == null)
			{
				throw new ArgumentNullException("memberType");
			}
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			this.fieldNumber = fieldNumber;
			this.memberType = memberType;
			this.itemType = itemType;
			this.defaultType = defaultType;
			this.model = model;
			this.dataFormat = dataFormat;
		}

		internal object GetRawEnumValue()
		{
			return ((FieldInfo)this.member).GetRawConstantValue();
		}

		private static object ParseDefaultValue(Type type, object value)
		{
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			if (value is string)
			{
				string text = (string)value;
				if (Helpers.IsEnum(type))
				{
					return Helpers.ParseEnum(type, text);
				}
				ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
				switch (typeCode)
				{
				case ProtoTypeCode.Boolean:
					return bool.Parse(text);
				case ProtoTypeCode.Char:
					if (text.get_Length() == 1)
					{
						return text.get_Chars(0);
					}
					throw new FormatException("Single character expected: \"" + text + "\"");
				case ProtoTypeCode.SByte:
					return sbyte.Parse(text, 7, CultureInfo.get_InvariantCulture());
				case ProtoTypeCode.Byte:
					return byte.Parse(text, 7, CultureInfo.get_InvariantCulture());
				case ProtoTypeCode.Int16:
					return short.Parse(text, 511, CultureInfo.get_InvariantCulture());
				case ProtoTypeCode.UInt16:
					return ushort.Parse(text, 511, CultureInfo.get_InvariantCulture());
				case ProtoTypeCode.Int32:
					return int.Parse(text, 511, CultureInfo.get_InvariantCulture());
				case ProtoTypeCode.UInt32:
					return uint.Parse(text, 511, CultureInfo.get_InvariantCulture());
				case ProtoTypeCode.Int64:
					return long.Parse(text, 511, CultureInfo.get_InvariantCulture());
				case ProtoTypeCode.UInt64:
					return ulong.Parse(text, 511, CultureInfo.get_InvariantCulture());
				case ProtoTypeCode.Single:
					return float.Parse(text, 511, CultureInfo.get_InvariantCulture());
				case ProtoTypeCode.Double:
					return double.Parse(text, 511, CultureInfo.get_InvariantCulture());
				case ProtoTypeCode.Decimal:
					return decimal.Parse(text, 511, CultureInfo.get_InvariantCulture());
				case ProtoTypeCode.DateTime:
					return DateTime.Parse(text, CultureInfo.get_InvariantCulture());
				case (ProtoTypeCode)17:
					IL_84:
					switch (typeCode)
					{
					case ProtoTypeCode.TimeSpan:
						return TimeSpan.Parse(text);
					case ProtoTypeCode.ByteArray:
						goto IL_1F4;
					case ProtoTypeCode.Guid:
						return new Guid(text);
					case ProtoTypeCode.Uri:
						return text;
					default:
						goto IL_1F4;
					}
					break;
				case ProtoTypeCode.String:
					return text;
				}
				goto IL_84;
			}
			IL_1F4:
			if (Helpers.IsEnum(type))
			{
				return Enum.ToObject(type, value);
			}
			return Convert.ChangeType(value, type, CultureInfo.get_InvariantCulture());
		}

		public void SetSpecified(MethodInfo getSpecified, MethodInfo setSpecified)
		{
			if (getSpecified != null && (getSpecified.get_ReturnType() != this.model.MapType(typeof(bool)) || getSpecified.get_IsStatic() || getSpecified.GetParameters().Length != 0))
			{
				throw new ArgumentException("Invalid pattern for checking member-specified", "getSpecified");
			}
			ParameterInfo[] parameters;
			if (setSpecified != null && (setSpecified.get_ReturnType() != this.model.MapType(typeof(void)) || setSpecified.get_IsStatic() || (parameters = setSpecified.GetParameters()).Length != 1 || parameters[0].get_ParameterType() != this.model.MapType(typeof(bool))))
			{
				throw new ArgumentException("Invalid pattern for setting member-specified", "setSpecified");
			}
			this.ThrowIfFrozen();
			this.getSpecified = getSpecified;
			this.setSpecified = setSpecified;
		}

		private void ThrowIfFrozen()
		{
			if (this.serializer != null)
			{
				throw new InvalidOperationException("The type cannot be changed once a serializer has been generated");
			}
		}

		private IProtoSerializer BuildSerializer()
		{
			int opaqueToken = 0;
			IProtoSerializer result;
			try
			{
				this.model.TakeLock(ref opaqueToken);
				Type type = (this.itemType != null) ? this.itemType : this.memberType;
				WireType wireType;
				IProtoSerializer protoSerializer = ValueMember.TryGetCoreSerializer(this.model, this.dataFormat, type, out wireType, this.asReference, this.dynamicType, this.OverwriteList, true);
				if (protoSerializer == null)
				{
					throw new InvalidOperationException("No serializer defined for type: " + type.get_FullName());
				}
				if (this.itemType != null && this.SupportNull)
				{
					if (this.IsPacked)
					{
						throw new NotSupportedException("Packed encodings cannot support null values");
					}
					protoSerializer = new TagDecorator(1, wireType, this.IsStrict, protoSerializer);
					protoSerializer = new NullDecorator(this.model, protoSerializer);
					protoSerializer = new TagDecorator(this.fieldNumber, WireType.StartGroup, false, protoSerializer);
				}
				else
				{
					protoSerializer = new TagDecorator(this.fieldNumber, wireType, this.IsStrict, protoSerializer);
				}
				if (this.itemType != null)
				{
					Type arg_119_0 = (!this.SupportNull) ? (Helpers.GetUnderlyingType(this.itemType) ?? this.itemType) : this.itemType;
					if (this.memberType.get_IsArray())
					{
						protoSerializer = new ArrayDecorator(this.model, protoSerializer, this.fieldNumber, this.IsPacked, wireType, this.memberType, this.OverwriteList, this.SupportNull);
					}
					else
					{
						protoSerializer = ListDecorator.Create(this.model, this.memberType, this.defaultType, protoSerializer, this.fieldNumber, this.IsPacked, wireType, this.member != null && PropertyDecorator.CanWrite(this.model, this.member), this.OverwriteList, this.SupportNull);
					}
				}
				else if (this.defaultValue != null && !this.IsRequired && this.getSpecified == null)
				{
					protoSerializer = new DefaultValueDecorator(this.model, this.defaultValue, protoSerializer);
				}
				if (this.memberType == this.model.MapType(typeof(Uri)))
				{
					protoSerializer = new UriDecorator(this.model, protoSerializer);
				}
				if (this.member != null)
				{
					PropertyInfo propertyInfo = this.member as PropertyInfo;
					if (propertyInfo != null)
					{
						protoSerializer = new PropertyDecorator(this.model, this.parentType, (PropertyInfo)this.member, protoSerializer);
					}
					else
					{
						FieldInfo fieldInfo = this.member as FieldInfo;
						if (fieldInfo == null)
						{
							throw new InvalidOperationException();
						}
						protoSerializer = new FieldDecorator(this.parentType, (FieldInfo)this.member, protoSerializer);
					}
					if (this.getSpecified != null || this.setSpecified != null)
					{
						protoSerializer = new MemberSpecifiedDecorator(this.getSpecified, this.setSpecified, protoSerializer);
					}
				}
				result = protoSerializer;
			}
			finally
			{
				this.model.ReleaseLock(opaqueToken);
			}
			return result;
		}

		private static WireType GetIntWireType(DataFormat format, int width)
		{
			switch (format)
			{
			case DataFormat.Default:
			case DataFormat.TwosComplement:
				return WireType.Variant;
			case DataFormat.ZigZag:
				return WireType.SignedVariant;
			case DataFormat.FixedSize:
				return (width != 32) ? WireType.Fixed64 : WireType.Fixed32;
			default:
				throw new InvalidOperationException();
			}
		}

		private static WireType GetDateTimeWireType(DataFormat format)
		{
			switch (format)
			{
			case DataFormat.Default:
				return WireType.String;
			case DataFormat.FixedSize:
				return WireType.Fixed64;
			case DataFormat.Group:
				return WireType.StartGroup;
			}
			throw new InvalidOperationException();
		}

		internal static IProtoSerializer TryGetCoreSerializer(RuntimeTypeModel model, DataFormat dataFormat, Type type, out WireType defaultWireType, bool asReference, bool dynamicType, bool overwriteList, bool allowComplexTypes)
		{
			Type underlyingType = Helpers.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			if (!Helpers.IsEnum(type))
			{
				ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
				ProtoTypeCode protoTypeCode = typeCode;
				switch (protoTypeCode)
				{
				case ProtoTypeCode.Boolean:
					defaultWireType = WireType.Variant;
					return new BooleanSerializer(model);
				case ProtoTypeCode.Char:
					defaultWireType = WireType.Variant;
					return new CharSerializer(model);
				case ProtoTypeCode.SByte:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new SByteSerializer(model);
				case ProtoTypeCode.Byte:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new ByteSerializer(model);
				case ProtoTypeCode.Int16:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new Int16Serializer(model);
				case ProtoTypeCode.UInt16:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new UInt16Serializer(model);
				case ProtoTypeCode.Int32:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new Int32Serializer(model);
				case ProtoTypeCode.UInt32:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
					return new UInt32Serializer(model);
				case ProtoTypeCode.Int64:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 64);
					return new Int64Serializer(model);
				case ProtoTypeCode.UInt64:
					defaultWireType = ValueMember.GetIntWireType(dataFormat, 64);
					return new UInt64Serializer(model);
				case ProtoTypeCode.Single:
					defaultWireType = WireType.Fixed32;
					return new SingleSerializer(model);
				case ProtoTypeCode.Double:
					defaultWireType = WireType.Fixed64;
					return new DoubleSerializer(model);
				case ProtoTypeCode.Decimal:
					defaultWireType = WireType.String;
					return new DecimalSerializer(model);
				case ProtoTypeCode.DateTime:
					defaultWireType = ValueMember.GetDateTimeWireType(dataFormat);
					return new DateTimeSerializer(model);
				case (ProtoTypeCode)17:
					IL_91:
					switch (protoTypeCode)
					{
					case ProtoTypeCode.TimeSpan:
						defaultWireType = ValueMember.GetDateTimeWireType(dataFormat);
						return new TimeSpanSerializer(model);
					case ProtoTypeCode.ByteArray:
						defaultWireType = WireType.String;
						return new BlobSerializer(model, overwriteList);
					case ProtoTypeCode.Guid:
						defaultWireType = WireType.String;
						return new GuidSerializer(model);
					case ProtoTypeCode.Uri:
						defaultWireType = WireType.String;
						return new StringSerializer(model);
					case ProtoTypeCode.Type:
						defaultWireType = WireType.String;
						return new SystemTypeSerializer(model);
					default:
					{
						IProtoSerializer protoSerializer = (!model.AllowParseableTypes) ? null : ParseableSerializer.TryCreate(type, model);
						if (protoSerializer != null)
						{
							defaultWireType = WireType.String;
							return protoSerializer;
						}
						if (allowComplexTypes && model != null)
						{
							int key = model.GetKey(type, false, true);
							if (asReference || dynamicType)
							{
								defaultWireType = ((dataFormat != DataFormat.Group) ? WireType.String : WireType.StartGroup);
								BclHelpers.NetObjectOptions netObjectOptions = BclHelpers.NetObjectOptions.None;
								if (asReference)
								{
									netObjectOptions |= BclHelpers.NetObjectOptions.AsReference;
								}
								if (dynamicType)
								{
									netObjectOptions |= BclHelpers.NetObjectOptions.DynamicType;
								}
								if (key >= 0)
								{
									if (asReference && Helpers.IsValueType(type))
									{
										string text = "AsReference cannot be used with value-types";
										if (type.get_Name() == "KeyValuePair`2")
										{
											text += "; please see http://stackoverflow.com/q/14436606/";
										}
										else
										{
											text = text + ": " + type.get_FullName();
										}
										throw new InvalidOperationException(text);
									}
									MetaType metaType = model[type];
									if (asReference && metaType.IsAutoTuple)
									{
										netObjectOptions |= BclHelpers.NetObjectOptions.LateSet;
									}
									if (metaType.UseConstructor)
									{
										netObjectOptions |= BclHelpers.NetObjectOptions.UseConstructor;
									}
								}
								return new NetObjectSerializer(model, type, key, netObjectOptions);
							}
							if (key >= 0)
							{
								defaultWireType = ((dataFormat != DataFormat.Group) ? WireType.String : WireType.StartGroup);
								return new SubItemSerializer(type, key, model[type], true);
							}
						}
						defaultWireType = WireType.None;
						return null;
					}
					}
					break;
				case ProtoTypeCode.String:
					defaultWireType = WireType.String;
					if (asReference)
					{
						return new NetObjectSerializer(model, model.MapType(typeof(string)), 0, BclHelpers.NetObjectOptions.AsReference);
					}
					return new StringSerializer(model);
				}
				goto IL_91;
			}
			if (allowComplexTypes && model != null)
			{
				defaultWireType = WireType.Variant;
				return new EnumSerializer(type, model.GetEnumMap(type));
			}
			defaultWireType = WireType.None;
			return null;
		}

		internal void SetName(string name)
		{
			this.ThrowIfFrozen();
			this.name = name;
		}

		private bool HasFlag(byte flag)
		{
			return (this.flags & flag) == flag;
		}

		private void SetFlag(byte flag, bool value, bool throwIfFrozen)
		{
			if (throwIfFrozen && this.HasFlag(flag) != value)
			{
				this.ThrowIfFrozen();
			}
			if (value)
			{
				this.flags |= flag;
			}
			else
			{
				this.flags &= ~flag;
			}
		}

		internal string GetSchemaTypeName(bool applyNetObjectProxy, ref bool requiresBclImport)
		{
			Type type = this.ItemType;
			if (type == null)
			{
				type = this.MemberType;
			}
			return this.model.GetSchemaTypeName(type, this.DataFormat, applyNetObjectProxy && this.asReference, applyNetObjectProxy && this.dynamicType, ref requiresBclImport);
		}
	}
}
