using ProtoBuf.Serializers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ProtoBuf.Meta
{
	public class MetaType : ISerializerProxy
	{
		internal sealed class Comparer : IComparer, IComparer<MetaType>
		{
			public static readonly MetaType.Comparer Default = new MetaType.Comparer();

			public int Compare(object x, object y)
			{
				return this.Compare(x as MetaType, y as MetaType);
			}

			public int Compare(MetaType x, MetaType y)
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
				return string.Compare(x.GetSchemaTypeName(), y.GetSchemaTypeName(), 4);
			}
		}

		[Flags]
		internal enum AttributeFamily
		{
			None = 0,
			ProtoBuf = 1,
			DataContractSerialier = 2,
			XmlSerializer = 4,
			AutoTuple = 8
		}

		private const byte OPTIONS_Pending = 1;

		private const byte OPTIONS_EnumPassThru = 2;

		private const byte OPTIONS_Frozen = 4;

		private const byte OPTIONS_PrivateOnApi = 8;

		private const byte OPTIONS_SkipConstructor = 16;

		private const byte OPTIONS_AsReferenceDefault = 32;

		private const byte OPTIONS_AutoTuple = 64;

		private const byte OPTIONS_IgnoreListHandling = 128;

		private MetaType baseType;

		private BasicList subTypes;

		internal static readonly Type ienumerable = typeof(IEnumerable);

		private CallbackSet callbacks;

		private string name;

		private MethodInfo factory;

		private readonly RuntimeTypeModel model;

		private readonly Type type;

		private IProtoTypeSerializer serializer;

		private Type constructType;

		private Type surrogate;

		private readonly BasicList fields = new BasicList();

		private volatile byte flags;

		IProtoSerializer ISerializerProxy.Serializer
		{
			get
			{
				return this.Serializer;
			}
		}

		public MetaType BaseType
		{
			get
			{
				return this.baseType;
			}
		}

		internal TypeModel Model
		{
			get
			{
				return this.model;
			}
		}

		public bool IncludeSerializerMethod
		{
			get
			{
				return !this.HasFlag(8);
			}
			set
			{
				this.SetFlag(8, !value, true);
			}
		}

		public bool AsReferenceDefault
		{
			get
			{
				return this.HasFlag(32);
			}
			set
			{
				this.SetFlag(32, value, true);
			}
		}

		public bool HasCallbacks
		{
			get
			{
				return this.callbacks != null && this.callbacks.NonTrivial;
			}
		}

		public bool HasSubtypes
		{
			get
			{
				return this.subTypes != null && this.subTypes.Count != 0;
			}
		}

		public CallbackSet Callbacks
		{
			get
			{
				if (this.callbacks == null)
				{
					this.callbacks = new CallbackSet(this);
				}
				return this.callbacks;
			}
		}

		private bool IsValueType
		{
			get
			{
				return this.type.get_IsValueType();
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.ThrowIfFrozen();
				this.name = value;
			}
		}

		public Type Type
		{
			get
			{
				return this.type;
			}
		}

		internal IProtoTypeSerializer Serializer
		{
			get
			{
				if (this.serializer == null)
				{
					int opaqueToken = 0;
					try
					{
						this.model.TakeLock(ref opaqueToken);
						if (this.serializer == null)
						{
							this.SetFlag(4, true, false);
							this.serializer = this.BuildSerializer();
						}
					}
					finally
					{
						this.model.ReleaseLock(opaqueToken);
					}
				}
				return this.serializer;
			}
		}

		internal bool IsList
		{
			get
			{
				Type type = (!this.IgnoreListHandling) ? TypeModel.GetListItemType(this.model, this.type) : null;
				return type != null;
			}
		}

		public bool UseConstructor
		{
			get
			{
				return !this.HasFlag(16);
			}
			set
			{
				this.SetFlag(16, !value, true);
			}
		}

		public Type ConstructType
		{
			get
			{
				return this.constructType;
			}
			set
			{
				this.ThrowIfFrozen();
				this.constructType = value;
			}
		}

		public ValueMember this[int fieldNumber]
		{
			get
			{
				BasicList.NodeEnumerator enumerator = this.fields.GetEnumerator();
				while (enumerator.MoveNext())
				{
					ValueMember valueMember = (ValueMember)enumerator.Current;
					if (valueMember.FieldNumber == fieldNumber)
					{
						return valueMember;
					}
				}
				return null;
			}
		}

		public ValueMember this[MemberInfo member]
		{
			get
			{
				if (member == null)
				{
					return null;
				}
				BasicList.NodeEnumerator enumerator = this.fields.GetEnumerator();
				while (enumerator.MoveNext())
				{
					ValueMember valueMember = (ValueMember)enumerator.Current;
					if (valueMember.Member == member)
					{
						return valueMember;
					}
				}
				return null;
			}
		}

		public bool EnumPassthru
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

		public bool IgnoreListHandling
		{
			get
			{
				return this.HasFlag(128);
			}
			set
			{
				this.SetFlag(128, value, true);
			}
		}

		internal bool Pending
		{
			get
			{
				return this.HasFlag(1);
			}
			set
			{
				this.SetFlag(1, value, false);
			}
		}

		internal IEnumerable Fields
		{
			get
			{
				return this.fields;
			}
		}

		internal bool IsAutoTuple
		{
			get
			{
				return this.HasFlag(64);
			}
		}

		internal MetaType(RuntimeTypeModel model, Type type, MethodInfo factory)
		{
			this.factory = factory;
			if (model == null)
			{
				throw new ArgumentNullException("model");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			IProtoSerializer protoSerializer = model.TryGetBasicTypeSerializer(type);
			if (protoSerializer != null)
			{
				throw MetaType.InbuiltType(type);
			}
			this.type = type;
			this.model = model;
			if (Helpers.IsEnum(type))
			{
				this.EnumPassthru = type.IsDefined(model.MapType(typeof(FlagsAttribute)), false);
			}
		}

		public override string ToString()
		{
			return this.type.ToString();
		}

		private bool IsValidSubType(Type subType)
		{
			return this.type.IsAssignableFrom(subType);
		}

		public MetaType AddSubType(int fieldNumber, Type derivedType)
		{
			return this.AddSubType(fieldNumber, derivedType, DataFormat.Default);
		}

		public MetaType AddSubType(int fieldNumber, Type derivedType, DataFormat dataFormat)
		{
			if (derivedType == null)
			{
				throw new ArgumentNullException("derivedType");
			}
			if (fieldNumber < 1)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if ((!this.type.get_IsClass() && !this.type.get_IsInterface()) || this.type.get_IsSealed())
			{
				throw new InvalidOperationException("Sub-types can only be added to non-sealed classes");
			}
			if (!this.IsValidSubType(derivedType))
			{
				throw new ArgumentException(derivedType.get_Name() + " is not a valid sub-type of " + this.type.get_Name(), "derivedType");
			}
			MetaType metaType = this.model[derivedType];
			this.ThrowIfFrozen();
			metaType.ThrowIfFrozen();
			SubType value = new SubType(fieldNumber, metaType, dataFormat);
			this.ThrowIfFrozen();
			metaType.SetBaseType(this);
			if (this.subTypes == null)
			{
				this.subTypes = new BasicList();
			}
			this.subTypes.Add(value);
			return this;
		}

		private void SetBaseType(MetaType baseType)
		{
			if (baseType == null)
			{
				throw new ArgumentNullException("baseType");
			}
			if (this.baseType == baseType)
			{
				return;
			}
			if (this.baseType != null)
			{
				throw new InvalidOperationException("A type can only participate in one inheritance hierarchy");
			}
			for (MetaType metaType = baseType; metaType != null; metaType = metaType.baseType)
			{
				if (object.ReferenceEquals(metaType, this))
				{
					throw new InvalidOperationException("Cyclic inheritance is not allowed");
				}
			}
			this.baseType = baseType;
		}

		public MetaType SetCallbacks(MethodInfo beforeSerialize, MethodInfo afterSerialize, MethodInfo beforeDeserialize, MethodInfo afterDeserialize)
		{
			CallbackSet callbackSet = this.Callbacks;
			callbackSet.BeforeSerialize = beforeSerialize;
			callbackSet.AfterSerialize = afterSerialize;
			callbackSet.BeforeDeserialize = beforeDeserialize;
			callbackSet.AfterDeserialize = afterDeserialize;
			return this;
		}

		public MetaType SetCallbacks(string beforeSerialize, string afterSerialize, string beforeDeserialize, string afterDeserialize)
		{
			if (this.IsValueType)
			{
				throw new InvalidOperationException();
			}
			CallbackSet callbackSet = this.Callbacks;
			callbackSet.BeforeSerialize = this.ResolveMethod(beforeSerialize, true);
			callbackSet.AfterSerialize = this.ResolveMethod(afterSerialize, true);
			callbackSet.BeforeDeserialize = this.ResolveMethod(beforeDeserialize, true);
			callbackSet.AfterDeserialize = this.ResolveMethod(afterDeserialize, true);
			return this;
		}

		internal string GetSchemaTypeName()
		{
			if (this.surrogate != null)
			{
				return this.model[this.surrogate].GetSchemaTypeName();
			}
			if (!Helpers.IsNullOrEmpty(this.name))
			{
				return this.name;
			}
			string text = this.type.get_Name();
			if (this.type.get_IsGenericType())
			{
				StringBuilder stringBuilder = new StringBuilder(text);
				int num = text.IndexOf('`');
				if (num >= 0)
				{
					stringBuilder.set_Length(num);
				}
				Type[] genericArguments = this.type.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					Type type = genericArguments[i];
					stringBuilder.Append('_');
					Type type2 = type;
					int key = this.model.GetKey(ref type2);
					MetaType metaType;
					if (key >= 0 && (metaType = this.model[type2]) != null && metaType.surrogate == null)
					{
						stringBuilder.Append(metaType.GetSchemaTypeName());
					}
					else
					{
						stringBuilder.Append(type2.get_Name());
					}
				}
				return stringBuilder.ToString();
			}
			return text;
		}

		public MetaType SetFactory(MethodInfo factory)
		{
			this.model.VerifyFactory(factory, this.type);
			this.ThrowIfFrozen();
			this.factory = factory;
			return this;
		}

		public MetaType SetFactory(string factory)
		{
			return this.SetFactory(this.ResolveMethod(factory, false));
		}

		private MethodInfo ResolveMethod(string name, bool instance)
		{
			if (Helpers.IsNullOrEmpty(name))
			{
				return null;
			}
			return (!instance) ? Helpers.GetStaticMethod(this.type, name) : Helpers.GetInstanceMethod(this.type, name);
		}

		internal static Exception InbuiltType(Type type)
		{
			return new ArgumentException("Data of this type has inbuilt behaviour, and cannot be added to a model in this way: " + type.get_FullName());
		}

		protected internal void ThrowIfFrozen()
		{
			if ((this.flags & 4) != 0)
			{
				throw new InvalidOperationException("The type cannot be changed once a serializer has been generated for " + this.type.get_FullName());
			}
		}

		private IProtoTypeSerializer BuildSerializer()
		{
			if (Helpers.IsEnum(this.type))
			{
				return new TagDecorator(1, WireType.Variant, false, new EnumSerializer(this.type, this.GetEnumMap()));
			}
			Type type = (!this.IgnoreListHandling) ? TypeModel.GetListItemType(this.model, this.type) : null;
			if (type != null)
			{
				if (this.surrogate != null)
				{
					throw new ArgumentException("Repeated data (a list, collection, etc) has inbuilt behaviour and cannot use a surrogate");
				}
				if (this.subTypes != null && this.subTypes.Count != 0)
				{
					throw new ArgumentException("Repeated data (a list, collection, etc) has inbuilt behaviour and cannot be subclassed");
				}
				Type defaultType = null;
				MetaType.ResolveListTypes(this.model, this.type, ref type, ref defaultType);
				ValueMember valueMember = new ValueMember(this.model, 1, this.type, type, defaultType, DataFormat.Default);
				return new TypeSerializer(this.model, this.type, new int[]
				{
					1
				}, new IProtoSerializer[]
				{
					valueMember.Serializer
				}, null, true, true, null, this.constructType, this.factory);
			}
			else
			{
				if (this.surrogate != null)
				{
					MetaType metaType = this.model[this.surrogate];
					MetaType metaType2;
					while ((metaType2 = metaType.baseType) != null)
					{
						metaType = metaType2;
					}
					return new SurrogateSerializer(this.model, this.type, this.surrogate, metaType.Serializer);
				}
				if (!this.IsAutoTuple)
				{
					this.fields.Trim();
					int count = this.fields.Count;
					int num = (this.subTypes != null) ? this.subTypes.Count : 0;
					int[] array = new int[count + num];
					IProtoSerializer[] array2 = new IProtoSerializer[count + num];
					int num2 = 0;
					if (num != 0)
					{
						BasicList.NodeEnumerator enumerator = this.subTypes.GetEnumerator();
						while (enumerator.MoveNext())
						{
							SubType subType = (SubType)enumerator.Current;
							if (!subType.DerivedType.IgnoreListHandling && this.model.MapType(MetaType.ienumerable).IsAssignableFrom(subType.DerivedType.Type))
							{
								throw new ArgumentException("Repeated data (a list, collection, etc) has inbuilt behaviour and cannot be used as a subclass");
							}
							array[num2] = subType.FieldNumber;
							array2[num2++] = subType.Serializer;
						}
					}
					if (count != 0)
					{
						BasicList.NodeEnumerator enumerator2 = this.fields.GetEnumerator();
						while (enumerator2.MoveNext())
						{
							ValueMember valueMember2 = (ValueMember)enumerator2.Current;
							array[num2] = valueMember2.FieldNumber;
							array2[num2++] = valueMember2.Serializer;
						}
					}
					BasicList basicList = null;
					for (MetaType metaType3 = this.BaseType; metaType3 != null; metaType3 = metaType3.BaseType)
					{
						MethodInfo methodInfo = (!metaType3.HasCallbacks) ? null : metaType3.Callbacks.BeforeDeserialize;
						if (methodInfo != null)
						{
							if (basicList == null)
							{
								basicList = new BasicList();
							}
							basicList.Add(methodInfo);
						}
					}
					MethodInfo[] array3 = null;
					if (basicList != null)
					{
						array3 = new MethodInfo[basicList.Count];
						basicList.CopyTo(array3, 0);
						Array.Reverse(array3);
					}
					return new TypeSerializer(this.model, this.type, array, array2, array3, this.baseType == null, this.UseConstructor, this.callbacks, this.constructType, this.factory);
				}
				MemberInfo[] members;
				ConstructorInfo constructorInfo = MetaType.ResolveTupleConstructor(this.type, out members);
				if (constructorInfo == null)
				{
					throw new InvalidOperationException();
				}
				return new TupleSerializer(this.model, constructorInfo, members);
			}
		}

		private static Type GetBaseType(MetaType type)
		{
			return type.type.get_BaseType();
		}

		internal static bool GetAsReferenceDefault(RuntimeTypeModel model, Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (Helpers.IsEnum(type))
			{
				return false;
			}
			AttributeMap[] array = AttributeMap.Create(model, type, false);
			for (int i = 0; i < array.Length; i++)
			{
				object obj;
				if (array[i].AttributeType.get_FullName() == "ProtoBuf.ProtoContractAttribute" && array[i].TryGet("AsReferenceDefault", out obj))
				{
					return (bool)obj;
				}
			}
			return false;
		}

		internal void ApplyDefaultBehaviour()
		{
			Type type = MetaType.GetBaseType(this);
			if (type != null && this.model.FindWithoutAdd(type) == null && MetaType.GetContractFamily(this.model, type, null) != MetaType.AttributeFamily.None)
			{
				this.model.FindOrAddAuto(type, true, false, false);
			}
			AttributeMap[] array = AttributeMap.Create(this.model, this.type, false);
			MetaType.AttributeFamily attributeFamily = MetaType.GetContractFamily(this.model, this.type, array);
			if (attributeFamily == MetaType.AttributeFamily.AutoTuple)
			{
				this.SetFlag(64, true, true);
			}
			bool flag = !this.EnumPassthru && Helpers.IsEnum(this.type);
			if (attributeFamily == MetaType.AttributeFamily.None && !flag)
			{
				return;
			}
			BasicList basicList = null;
			BasicList basicList2 = null;
			int dataMemberOffset = 0;
			int num = 1;
			bool flag2 = this.model.InferTagFromNameDefault;
			ImplicitFields implicitFields = ImplicitFields.None;
			string text = null;
			for (int i = 0; i < array.Length; i++)
			{
				AttributeMap attributeMap = array[i];
				string fullName = attributeMap.AttributeType.get_FullName();
				object obj;
				if (!flag && fullName == "ProtoBuf.ProtoIncludeAttribute")
				{
					int fieldNumber = 0;
					if (attributeMap.TryGet("tag", out obj))
					{
						fieldNumber = (int)obj;
					}
					DataFormat dataFormat = DataFormat.Default;
					if (attributeMap.TryGet("DataFormat", out obj))
					{
						dataFormat = (DataFormat)((int)obj);
					}
					Type type2 = null;
					try
					{
						if (attributeMap.TryGet("knownTypeName", out obj))
						{
							type2 = this.model.GetType((string)obj, this.type.get_Assembly());
						}
						else if (attributeMap.TryGet("knownType", out obj))
						{
							type2 = (Type)obj;
						}
					}
					catch (Exception ex)
					{
						throw new InvalidOperationException("Unable to resolve sub-type of: " + this.type.get_FullName(), ex);
					}
					if (type2 == null)
					{
						throw new InvalidOperationException("Unable to resolve sub-type of: " + this.type.get_FullName());
					}
					if (this.IsValidSubType(type2))
					{
						this.AddSubType(fieldNumber, type2, dataFormat);
					}
				}
				if (fullName == "ProtoBuf.ProtoPartialIgnoreAttribute" && attributeMap.TryGet("MemberName", out obj) && obj != null)
				{
					if (basicList == null)
					{
						basicList = new BasicList();
					}
					basicList.Add((string)obj);
				}
				if (!flag && fullName == "ProtoBuf.ProtoPartialMemberAttribute")
				{
					if (basicList2 == null)
					{
						basicList2 = new BasicList();
					}
					basicList2.Add(attributeMap);
				}
				if (fullName == "ProtoBuf.ProtoContractAttribute")
				{
					if (attributeMap.TryGet("Name", out obj))
					{
						text = (string)obj;
					}
					if (Helpers.IsEnum(this.type))
					{
						if (attributeMap.TryGet("EnumPassthruHasValue", false, out obj) && (bool)obj && attributeMap.TryGet("EnumPassthru", out obj))
						{
							this.EnumPassthru = (bool)obj;
							if (this.EnumPassthru)
							{
								flag = false;
							}
						}
					}
					else
					{
						if (attributeMap.TryGet("DataMemberOffset", out obj))
						{
							dataMemberOffset = (int)obj;
						}
						if (attributeMap.TryGet("InferTagFromNameHasValue", false, out obj) && (bool)obj && attributeMap.TryGet("InferTagFromName", out obj))
						{
							flag2 = (bool)obj;
						}
						if (attributeMap.TryGet("ImplicitFields", out obj) && obj != null)
						{
							implicitFields = (ImplicitFields)((int)obj);
						}
						if (attributeMap.TryGet("SkipConstructor", out obj))
						{
							this.UseConstructor = !(bool)obj;
						}
						if (attributeMap.TryGet("IgnoreListHandling", out obj))
						{
							this.IgnoreListHandling = (bool)obj;
						}
						if (attributeMap.TryGet("AsReferenceDefault", out obj))
						{
							this.AsReferenceDefault = (bool)obj;
						}
						if (attributeMap.TryGet("ImplicitFirstTag", out obj) && (int)obj > 0)
						{
							num = (int)obj;
						}
					}
				}
				if (fullName == "System.Runtime.Serialization.DataContractAttribute" && text == null && attributeMap.TryGet("Name", out obj))
				{
					text = (string)obj;
				}
				if (fullName == "System.Xml.Serialization.XmlTypeAttribute" && text == null && attributeMap.TryGet("TypeName", out obj))
				{
					text = (string)obj;
				}
			}
			if (!Helpers.IsNullOrEmpty(text))
			{
				this.Name = text;
			}
			if (implicitFields != ImplicitFields.None)
			{
				attributeFamily &= MetaType.AttributeFamily.ProtoBuf;
			}
			MethodInfo[] array2 = null;
			BasicList basicList3 = new BasicList();
			MemberInfo[] members = this.type.GetMembers((!flag) ? 52 : 24);
			MemberInfo[] array3 = members;
			for (int j = 0; j < array3.Length; j++)
			{
				MemberInfo memberInfo = array3[j];
				if (memberInfo.get_DeclaringType() == this.type)
				{
					if (!memberInfo.IsDefined(this.model.MapType(typeof(ProtoIgnoreAttribute)), true))
					{
						if (basicList == null || !basicList.Contains(memberInfo.get_Name()))
						{
							bool flag3 = false;
							PropertyInfo propertyInfo;
							FieldInfo fieldInfo;
							MethodInfo methodInfo;
							if ((propertyInfo = (memberInfo as PropertyInfo)) != null)
							{
								if (!flag)
								{
									Type type3 = propertyInfo.get_PropertyType();
									bool isPublic = Helpers.GetGetMethod(propertyInfo, false, false) != null;
									bool isField = false;
									MetaType.ApplyDefaultBehaviour_AddMembers(this.model, attributeFamily, flag, basicList2, dataMemberOffset, flag2, implicitFields, basicList3, memberInfo, ref flag3, isPublic, isField, ref type3);
								}
							}
							else if ((fieldInfo = (memberInfo as FieldInfo)) != null)
							{
								Type type3 = fieldInfo.get_FieldType();
								bool isPublic = fieldInfo.get_IsPublic();
								bool isField = true;
								if (!flag || fieldInfo.get_IsStatic())
								{
									MetaType.ApplyDefaultBehaviour_AddMembers(this.model, attributeFamily, flag, basicList2, dataMemberOffset, flag2, implicitFields, basicList3, memberInfo, ref flag3, isPublic, isField, ref type3);
								}
							}
							else if ((methodInfo = (memberInfo as MethodInfo)) != null)
							{
								if (!flag)
								{
									AttributeMap[] array4 = AttributeMap.Create(this.model, methodInfo, false);
									if (array4 != null && array4.Length > 0)
									{
										MetaType.CheckForCallback(methodInfo, array4, "ProtoBuf.ProtoBeforeSerializationAttribute", ref array2, 0);
										MetaType.CheckForCallback(methodInfo, array4, "ProtoBuf.ProtoAfterSerializationAttribute", ref array2, 1);
										MetaType.CheckForCallback(methodInfo, array4, "ProtoBuf.ProtoBeforeDeserializationAttribute", ref array2, 2);
										MetaType.CheckForCallback(methodInfo, array4, "ProtoBuf.ProtoAfterDeserializationAttribute", ref array2, 3);
										MetaType.CheckForCallback(methodInfo, array4, "System.Runtime.Serialization.OnSerializingAttribute", ref array2, 4);
										MetaType.CheckForCallback(methodInfo, array4, "System.Runtime.Serialization.OnSerializedAttribute", ref array2, 5);
										MetaType.CheckForCallback(methodInfo, array4, "System.Runtime.Serialization.OnDeserializingAttribute", ref array2, 6);
										MetaType.CheckForCallback(methodInfo, array4, "System.Runtime.Serialization.OnDeserializedAttribute", ref array2, 7);
									}
								}
							}
						}
					}
				}
			}
			ProtoMemberAttribute[] array5 = new ProtoMemberAttribute[basicList3.Count];
			basicList3.CopyTo(array5, 0);
			if (flag2 || implicitFields != ImplicitFields.None)
			{
				Array.Sort<ProtoMemberAttribute>(array5);
				int num2 = num;
				ProtoMemberAttribute[] array6 = array5;
				for (int k = 0; k < array6.Length; k++)
				{
					ProtoMemberAttribute protoMemberAttribute = array6[k];
					if (!protoMemberAttribute.TagIsPinned)
					{
						protoMemberAttribute.Rebase(num2++);
					}
				}
			}
			ProtoMemberAttribute[] array7 = array5;
			for (int l = 0; l < array7.Length; l++)
			{
				ProtoMemberAttribute normalizedAttribute = array7[l];
				ValueMember valueMember = this.ApplyDefaultBehaviour(flag, normalizedAttribute);
				if (valueMember != null)
				{
					this.Add(valueMember);
				}
			}
			if (array2 != null)
			{
				this.SetCallbacks(MetaType.Coalesce(array2, 0, 4), MetaType.Coalesce(array2, 1, 5), MetaType.Coalesce(array2, 2, 6), MetaType.Coalesce(array2, 3, 7));
			}
		}

		private static void ApplyDefaultBehaviour_AddMembers(TypeModel model, MetaType.AttributeFamily family, bool isEnum, BasicList partialMembers, int dataMemberOffset, bool inferTagByName, ImplicitFields implicitMode, BasicList members, MemberInfo member, ref bool forced, bool isPublic, bool isField, ref Type effectiveType)
		{
			if (implicitMode != ImplicitFields.AllPublic)
			{
				if (implicitMode == ImplicitFields.AllFields)
				{
					if (isField)
					{
						forced = true;
					}
				}
			}
			else if (isPublic)
			{
				forced = true;
			}
			if (effectiveType.IsSubclassOf(model.MapType(typeof(Delegate))))
			{
				effectiveType = null;
			}
			if (effectiveType != null)
			{
				ProtoMemberAttribute protoMemberAttribute = MetaType.NormalizeProtoMember(model, member, family, forced, isEnum, partialMembers, dataMemberOffset, inferTagByName);
				if (protoMemberAttribute != null)
				{
					members.Add(protoMemberAttribute);
				}
			}
		}

		private static MethodInfo Coalesce(MethodInfo[] arr, int x, int y)
		{
			MethodInfo methodInfo = arr[x];
			if (methodInfo == null)
			{
				methodInfo = arr[y];
			}
			return methodInfo;
		}

		internal static MetaType.AttributeFamily GetContractFamily(RuntimeTypeModel model, Type type, AttributeMap[] attributes)
		{
			MetaType.AttributeFamily attributeFamily = MetaType.AttributeFamily.None;
			if (attributes == null)
			{
				attributes = AttributeMap.Create(model, type, false);
			}
			for (int i = 0; i < attributes.Length; i++)
			{
				string fullName = attributes[i].AttributeType.get_FullName();
				if (fullName != null)
				{
					if (MetaType.<>f__switch$mapE == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
						dictionary.Add("ProtoBuf.ProtoContractAttribute", 0);
						dictionary.Add("System.Xml.Serialization.XmlTypeAttribute", 1);
						dictionary.Add("System.Runtime.Serialization.DataContractAttribute", 2);
						MetaType.<>f__switch$mapE = dictionary;
					}
					int num;
					if (MetaType.<>f__switch$mapE.TryGetValue(fullName, ref num))
					{
						switch (num)
						{
						case 0:
						{
							bool flag = false;
							MetaType.GetFieldBoolean(ref flag, attributes[i], "UseProtoMembersOnly");
							if (flag)
							{
								return MetaType.AttributeFamily.ProtoBuf;
							}
							attributeFamily |= MetaType.AttributeFamily.ProtoBuf;
							break;
						}
						case 1:
							if (!model.AutoAddProtoContractTypesOnly)
							{
								attributeFamily |= MetaType.AttributeFamily.XmlSerializer;
							}
							break;
						case 2:
							if (!model.AutoAddProtoContractTypesOnly)
							{
								attributeFamily |= MetaType.AttributeFamily.DataContractSerialier;
							}
							break;
						}
					}
				}
			}
			MemberInfo[] array;
			if (attributeFamily == MetaType.AttributeFamily.None && MetaType.ResolveTupleConstructor(type, out array) != null)
			{
				attributeFamily |= MetaType.AttributeFamily.AutoTuple;
			}
			return attributeFamily;
		}

		internal static ConstructorInfo ResolveTupleConstructor(Type type, out MemberInfo[] mappedMembers)
		{
			mappedMembers = null;
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.get_IsAbstract())
			{
				return null;
			}
			ConstructorInfo[] constructors = Helpers.GetConstructors(type, false);
			if (constructors.Length == 0 || (constructors.Length == 1 && constructors[0].GetParameters().Length == 0))
			{
				return null;
			}
			MemberInfo[] instanceFieldsAndProperties = Helpers.GetInstanceFieldsAndProperties(type, true);
			BasicList basicList = new BasicList();
			for (int i = 0; i < instanceFieldsAndProperties.Length; i++)
			{
				PropertyInfo propertyInfo = instanceFieldsAndProperties[i] as PropertyInfo;
				if (propertyInfo != null)
				{
					if (!propertyInfo.get_CanRead())
					{
						return null;
					}
					if (propertyInfo.get_CanWrite() && Helpers.GetSetMethod(propertyInfo, false, false) != null)
					{
						return null;
					}
					basicList.Add(propertyInfo);
				}
				else
				{
					FieldInfo fieldInfo = instanceFieldsAndProperties[i] as FieldInfo;
					if (fieldInfo != null)
					{
						if (!fieldInfo.get_IsInitOnly())
						{
							return null;
						}
						basicList.Add(fieldInfo);
					}
				}
			}
			if (basicList.Count == 0)
			{
				return null;
			}
			MemberInfo[] array = new MemberInfo[basicList.Count];
			basicList.CopyTo(array, 0);
			int[] array2 = new int[array.Length];
			int num = 0;
			ConstructorInfo constructorInfo = null;
			mappedMembers = new MemberInfo[array2.Length];
			for (int j = 0; j < constructors.Length; j++)
			{
				ParameterInfo[] parameters = constructors[j].GetParameters();
				if (parameters.Length == array.Length)
				{
					for (int k = 0; k < array2.Length; k++)
					{
						array2[k] = -1;
					}
					for (int l = 0; l < parameters.Length; l++)
					{
						string text = parameters[l].get_Name().ToLower();
						for (int m = 0; m < array.Length; m++)
						{
							if (!(array[m].get_Name().ToLower() != text))
							{
								Type memberType = Helpers.GetMemberType(array[m]);
								if (memberType == parameters[l].get_ParameterType())
								{
									array2[l] = m;
								}
							}
						}
					}
					bool flag = false;
					for (int n = 0; n < array2.Length; n++)
					{
						if (array2[n] < 0)
						{
							flag = true;
							break;
						}
						mappedMembers[n] = array[array2[n]];
					}
					if (!flag)
					{
						num++;
						constructorInfo = constructors[j];
					}
				}
			}
			return (num != 1) ? null : constructorInfo;
		}

		private static void CheckForCallback(MethodInfo method, AttributeMap[] attributes, string callbackTypeName, ref MethodInfo[] callbacks, int index)
		{
			for (int i = 0; i < attributes.Length; i++)
			{
				if (attributes[i].AttributeType.get_FullName() == callbackTypeName)
				{
					if (callbacks == null)
					{
						callbacks = new MethodInfo[8];
					}
					else if (callbacks[index] != null)
					{
						Type reflectedType = method.get_ReflectedType();
						throw new ProtoException("Duplicate " + callbackTypeName + " callbacks on " + reflectedType.get_FullName());
					}
					callbacks[index] = method;
				}
			}
		}

		private static bool HasFamily(MetaType.AttributeFamily value, MetaType.AttributeFamily required)
		{
			return (value & required) == required;
		}

		private static ProtoMemberAttribute NormalizeProtoMember(TypeModel model, MemberInfo member, MetaType.AttributeFamily family, bool forced, bool isEnum, BasicList partialMembers, int dataMemberOffset, bool inferByTagName)
		{
			if (member == null || (family == MetaType.AttributeFamily.None && !isEnum))
			{
				return null;
			}
			int num = -2147483648;
			int num2 = (!inferByTagName) ? 1 : -1;
			string text = null;
			bool isPacked = false;
			bool flag = false;
			bool flag2 = false;
			bool isRequired = false;
			bool asReference = false;
			bool flag3 = false;
			bool dynamicType = false;
			bool tagIsPinned = false;
			bool overwriteList = false;
			DataFormat dataFormat = DataFormat.Default;
			if (isEnum)
			{
				forced = true;
			}
			AttributeMap[] attribs = AttributeMap.Create(model, member, true);
			if (isEnum)
			{
				AttributeMap attribute = MetaType.GetAttribute(attribs, "ProtoBuf.ProtoIgnoreAttribute");
				if (attribute != null)
				{
					flag = true;
				}
				else
				{
					attribute = MetaType.GetAttribute(attribs, "ProtoBuf.ProtoEnumAttribute");
					num = Convert.ToInt32(((FieldInfo)member).GetRawConstantValue());
					if (attribute != null)
					{
						MetaType.GetFieldName(ref text, attribute, "Name");
						object obj;
						if ((bool)Helpers.GetInstanceMethod(attribute.AttributeType, "HasValue").Invoke(attribute.Target, null) && attribute.TryGet("Value", out obj))
						{
							num = (int)obj;
						}
					}
				}
				flag2 = true;
			}
			if (!flag && !flag2)
			{
				AttributeMap attribute = MetaType.GetAttribute(attribs, "ProtoBuf.ProtoMemberAttribute");
				MetaType.GetIgnore(ref flag, attribute, attribs, "ProtoBuf.ProtoIgnoreAttribute");
				if (!flag && attribute != null)
				{
					MetaType.GetFieldNumber(ref num, attribute, "Tag");
					MetaType.GetFieldName(ref text, attribute, "Name");
					MetaType.GetFieldBoolean(ref isRequired, attribute, "IsRequired");
					MetaType.GetFieldBoolean(ref isPacked, attribute, "IsPacked");
					MetaType.GetFieldBoolean(ref overwriteList, attribute, "OverwriteList");
					MetaType.GetDataFormat(ref dataFormat, attribute, "DataFormat");
					MetaType.GetFieldBoolean(ref flag3, attribute, "AsReferenceHasValue", false);
					if (flag3)
					{
						flag3 = MetaType.GetFieldBoolean(ref asReference, attribute, "AsReference", true);
					}
					MetaType.GetFieldBoolean(ref dynamicType, attribute, "DynamicType");
					tagIsPinned = (flag2 = (num > 0));
				}
				if (!flag2 && partialMembers != null)
				{
					BasicList.NodeEnumerator enumerator = partialMembers.GetEnumerator();
					while (enumerator.MoveNext())
					{
						AttributeMap attributeMap = (AttributeMap)enumerator.Current;
						object obj2;
						if (attributeMap.TryGet("MemberName", out obj2) && (string)obj2 == member.get_Name())
						{
							MetaType.GetFieldNumber(ref num, attributeMap, "Tag");
							MetaType.GetFieldName(ref text, attributeMap, "Name");
							MetaType.GetFieldBoolean(ref isRequired, attributeMap, "IsRequired");
							MetaType.GetFieldBoolean(ref isPacked, attributeMap, "IsPacked");
							MetaType.GetFieldBoolean(ref overwriteList, attribute, "OverwriteList");
							MetaType.GetDataFormat(ref dataFormat, attributeMap, "DataFormat");
							MetaType.GetFieldBoolean(ref flag3, attribute, "AsReferenceHasValue", false);
							if (flag3)
							{
								flag3 = MetaType.GetFieldBoolean(ref asReference, attributeMap, "AsReference", true);
							}
							MetaType.GetFieldBoolean(ref dynamicType, attributeMap, "DynamicType");
							if (flag2 = (tagIsPinned = (num > 0)))
							{
								break;
							}
						}
					}
				}
			}
			if (!flag && !flag2 && MetaType.HasFamily(family, MetaType.AttributeFamily.DataContractSerialier))
			{
				AttributeMap attribute = MetaType.GetAttribute(attribs, "System.Runtime.Serialization.DataMemberAttribute");
				if (attribute != null)
				{
					MetaType.GetFieldNumber(ref num, attribute, "Order");
					MetaType.GetFieldName(ref text, attribute, "Name");
					MetaType.GetFieldBoolean(ref isRequired, attribute, "IsRequired");
					flag2 = (num >= num2);
					if (flag2)
					{
						num += dataMemberOffset;
					}
				}
			}
			if (!flag && !flag2 && MetaType.HasFamily(family, MetaType.AttributeFamily.XmlSerializer))
			{
				AttributeMap attribute = MetaType.GetAttribute(attribs, "System.Xml.Serialization.XmlElementAttribute");
				if (attribute == null)
				{
					attribute = MetaType.GetAttribute(attribs, "System.Xml.Serialization.XmlArrayAttribute");
				}
				MetaType.GetIgnore(ref flag, attribute, attribs, "System.Xml.Serialization.XmlIgnoreAttribute");
				if (attribute != null && !flag)
				{
					MetaType.GetFieldNumber(ref num, attribute, "Order");
					MetaType.GetFieldName(ref text, attribute, "ElementName");
					flag2 = (num >= num2);
				}
			}
			if (!flag && !flag2 && MetaType.GetAttribute(attribs, "System.NonSerializedAttribute") != null)
			{
				flag = true;
			}
			if (flag || (num < num2 && !forced))
			{
				return null;
			}
			return new ProtoMemberAttribute(num, forced || inferByTagName)
			{
				AsReference = asReference,
				AsReferenceHasValue = flag3,
				DataFormat = dataFormat,
				DynamicType = dynamicType,
				IsPacked = isPacked,
				OverwriteList = overwriteList,
				IsRequired = isRequired,
				Name = (!Helpers.IsNullOrEmpty(text)) ? text : member.get_Name(),
				Member = member,
				TagIsPinned = tagIsPinned
			};
		}

		private ValueMember ApplyDefaultBehaviour(bool isEnum, ProtoMemberAttribute normalizedAttribute)
		{
			MemberInfo member;
			if (normalizedAttribute == null || (member = normalizedAttribute.Member) == null)
			{
				return null;
			}
			Type memberType = Helpers.GetMemberType(member);
			Type type = null;
			Type defaultType = null;
			MetaType.ResolveListTypes(this.model, memberType, ref type, ref defaultType);
			if (type != null)
			{
				int num = this.model.FindOrAddAuto(memberType, false, true, false);
				if (num >= 0 && this.model[memberType].IgnoreListHandling)
				{
					type = null;
					defaultType = null;
				}
			}
			AttributeMap[] attribs = AttributeMap.Create(this.model, member, true);
			object defaultValue = null;
			if (this.model.UseImplicitZeroDefaults)
			{
				ProtoTypeCode typeCode = Helpers.GetTypeCode(memberType);
				switch (typeCode)
				{
				case ProtoTypeCode.Boolean:
					defaultValue = false;
					break;
				case ProtoTypeCode.Char:
					defaultValue = '\0';
					break;
				case ProtoTypeCode.SByte:
					defaultValue = 0;
					break;
				case ProtoTypeCode.Byte:
					defaultValue = 0;
					break;
				case ProtoTypeCode.Int16:
					defaultValue = 0;
					break;
				case ProtoTypeCode.UInt16:
					defaultValue = 0;
					break;
				case ProtoTypeCode.Int32:
					defaultValue = 0;
					break;
				case ProtoTypeCode.UInt32:
					defaultValue = 0u;
					break;
				case ProtoTypeCode.Int64:
					defaultValue = 0L;
					break;
				case ProtoTypeCode.UInt64:
					defaultValue = 0uL;
					break;
				case ProtoTypeCode.Single:
					defaultValue = 0f;
					break;
				case ProtoTypeCode.Double:
					defaultValue = 0.0;
					break;
				case ProtoTypeCode.Decimal:
					defaultValue = 0m;
					break;
				default:
					switch (typeCode)
					{
					case ProtoTypeCode.TimeSpan:
						defaultValue = TimeSpan.Zero;
						break;
					case ProtoTypeCode.Guid:
						defaultValue = Guid.Empty;
						break;
					}
					break;
				}
			}
			AttributeMap attribute;
			object obj;
			if ((attribute = MetaType.GetAttribute(attribs, "System.ComponentModel.DefaultValueAttribute")) != null && attribute.TryGet("Value", out obj))
			{
				defaultValue = obj;
			}
			ValueMember valueMember = (!isEnum && normalizedAttribute.Tag <= 0) ? null : new ValueMember(this.model, this.type, normalizedAttribute.Tag, member, memberType, type, defaultType, normalizedAttribute.DataFormat, defaultValue);
			if (valueMember != null)
			{
				Type declaringType = this.type;
				PropertyInfo propertyInfo = Helpers.GetProperty(declaringType, member.get_Name() + "Specified", true);
				MethodInfo getMethod = Helpers.GetGetMethod(propertyInfo, true, true);
				if (getMethod == null || getMethod.get_IsStatic())
				{
					propertyInfo = null;
				}
				if (propertyInfo != null)
				{
					valueMember.SetSpecified(getMethod, Helpers.GetSetMethod(propertyInfo, true, true));
				}
				else
				{
					MethodInfo instanceMethod = Helpers.GetInstanceMethod(declaringType, "ShouldSerialize" + member.get_Name(), Helpers.EmptyTypes);
					if (instanceMethod != null && instanceMethod.get_ReturnType() == this.model.MapType(typeof(bool)))
					{
						valueMember.SetSpecified(instanceMethod, null);
					}
				}
				if (!Helpers.IsNullOrEmpty(normalizedAttribute.Name))
				{
					valueMember.SetName(normalizedAttribute.Name);
				}
				valueMember.IsPacked = normalizedAttribute.IsPacked;
				valueMember.IsRequired = normalizedAttribute.IsRequired;
				valueMember.OverwriteList = normalizedAttribute.OverwriteList;
				if (normalizedAttribute.AsReferenceHasValue)
				{
					valueMember.AsReference = normalizedAttribute.AsReference;
				}
				valueMember.DynamicType = normalizedAttribute.DynamicType;
			}
			return valueMember;
		}

		private static void GetDataFormat(ref DataFormat value, AttributeMap attrib, string memberName)
		{
			if (attrib == null || value != DataFormat.Default)
			{
				return;
			}
			object obj;
			if (attrib.TryGet(memberName, out obj) && obj != null)
			{
				value = (DataFormat)((int)obj);
			}
		}

		private static void GetIgnore(ref bool ignore, AttributeMap attrib, AttributeMap[] attribs, string fullName)
		{
			if (ignore || attrib == null)
			{
				return;
			}
			ignore = (MetaType.GetAttribute(attribs, fullName) != null);
		}

		private static void GetFieldBoolean(ref bool value, AttributeMap attrib, string memberName)
		{
			MetaType.GetFieldBoolean(ref value, attrib, memberName, true);
		}

		private static bool GetFieldBoolean(ref bool value, AttributeMap attrib, string memberName, bool publicOnly)
		{
			if (attrib == null)
			{
				return false;
			}
			if (value)
			{
				return true;
			}
			object obj;
			if (attrib.TryGet(memberName, publicOnly, out obj) && obj != null)
			{
				value = (bool)obj;
				return true;
			}
			return false;
		}

		private static void GetFieldNumber(ref int value, AttributeMap attrib, string memberName)
		{
			if (attrib == null || value > 0)
			{
				return;
			}
			object obj;
			if (attrib.TryGet(memberName, out obj) && obj != null)
			{
				value = (int)obj;
			}
		}

		private static void GetFieldName(ref string name, AttributeMap attrib, string memberName)
		{
			if (attrib == null || !Helpers.IsNullOrEmpty(name))
			{
				return;
			}
			object obj;
			if (attrib.TryGet(memberName, out obj) && obj != null)
			{
				name = (string)obj;
			}
		}

		private static AttributeMap GetAttribute(AttributeMap[] attribs, string fullName)
		{
			for (int i = 0; i < attribs.Length; i++)
			{
				AttributeMap attributeMap = attribs[i];
				if (attributeMap != null && attributeMap.AttributeType.get_FullName() == fullName)
				{
					return attributeMap;
				}
			}
			return null;
		}

		public MetaType Add(int fieldNumber, string memberName)
		{
			this.AddField(fieldNumber, memberName, null, null, null);
			return this;
		}

		public ValueMember AddField(int fieldNumber, string memberName)
		{
			return this.AddField(fieldNumber, memberName, null, null, null);
		}

		public MetaType Add(string memberName)
		{
			this.Add(this.GetNextFieldNumber(), memberName);
			return this;
		}

		public void SetSurrogate(Type surrogateType)
		{
			if (surrogateType == this.type)
			{
				surrogateType = null;
			}
			if (surrogateType != null && surrogateType != null && Helpers.IsAssignableFrom(this.model.MapType(typeof(IEnumerable)), surrogateType))
			{
				throw new ArgumentException("Repeated data (a list, collection, etc) has inbuilt behaviour and cannot be used as a surrogate");
			}
			this.ThrowIfFrozen();
			this.surrogate = surrogateType;
		}

		internal MetaType GetSurrogateOrSelf()
		{
			if (this.surrogate != null)
			{
				return this.model[this.surrogate];
			}
			return this;
		}

		internal MetaType GetSurrogateOrBaseOrSelf(bool deep)
		{
			if (this.surrogate != null)
			{
				return this.model[this.surrogate];
			}
			MetaType metaType = this.baseType;
			if (metaType == null)
			{
				return this;
			}
			if (deep)
			{
				MetaType result;
				do
				{
					result = metaType;
					metaType = metaType.baseType;
				}
				while (metaType != null);
				return result;
			}
			return metaType;
		}

		private int GetNextFieldNumber()
		{
			int num = 0;
			BasicList.NodeEnumerator enumerator = this.fields.GetEnumerator();
			while (enumerator.MoveNext())
			{
				ValueMember valueMember = (ValueMember)enumerator.Current;
				if (valueMember.FieldNumber > num)
				{
					num = valueMember.FieldNumber;
				}
			}
			if (this.subTypes != null)
			{
				BasicList.NodeEnumerator enumerator2 = this.subTypes.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					SubType subType = (SubType)enumerator2.Current;
					if (subType.FieldNumber > num)
					{
						num = subType.FieldNumber;
					}
				}
			}
			return num + 1;
		}

		public MetaType Add(params string[] memberNames)
		{
			if (memberNames == null)
			{
				throw new ArgumentNullException("memberNames");
			}
			int nextFieldNumber = this.GetNextFieldNumber();
			for (int i = 0; i < memberNames.Length; i++)
			{
				this.Add(nextFieldNumber++, memberNames[i]);
			}
			return this;
		}

		public MetaType Add(int fieldNumber, string memberName, object defaultValue)
		{
			this.AddField(fieldNumber, memberName, null, null, defaultValue);
			return this;
		}

		public MetaType Add(int fieldNumber, string memberName, Type itemType, Type defaultType)
		{
			this.AddField(fieldNumber, memberName, itemType, defaultType, null);
			return this;
		}

		public ValueMember AddField(int fieldNumber, string memberName, Type itemType, Type defaultType)
		{
			return this.AddField(fieldNumber, memberName, itemType, defaultType, null);
		}

		private ValueMember AddField(int fieldNumber, string memberName, Type itemType, Type defaultType, object defaultValue)
		{
			MemberInfo memberInfo = null;
			MemberInfo[] member = this.type.GetMember(memberName, (!Helpers.IsEnum(this.type)) ? 52 : 24);
			if (member != null && member.Length == 1)
			{
				memberInfo = member[0];
			}
			if (memberInfo == null)
			{
				throw new ArgumentException("Unable to determine member: " + memberName, "memberName");
			}
			MemberTypes memberType = memberInfo.get_MemberType();
			Type memberType2;
			if (memberType != 4)
			{
				if (memberType != 16)
				{
					throw new NotSupportedException(memberInfo.get_MemberType().ToString());
				}
				memberType2 = ((PropertyInfo)memberInfo).get_PropertyType();
			}
			else
			{
				memberType2 = ((FieldInfo)memberInfo).get_FieldType();
			}
			MetaType.ResolveListTypes(this.model, memberType2, ref itemType, ref defaultType);
			ValueMember valueMember = new ValueMember(this.model, this.type, fieldNumber, memberInfo, memberType2, itemType, defaultType, DataFormat.Default, defaultValue);
			this.Add(valueMember);
			return valueMember;
		}

		internal static void ResolveListTypes(TypeModel model, Type type, ref Type itemType, ref Type defaultType)
		{
			if (type == null)
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
				if (itemType == model.MapType(typeof(byte)))
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
				itemType = TypeModel.GetListItemType(model, type);
			}
			if (itemType != null)
			{
				Type type3 = null;
				Type type4 = null;
				MetaType.ResolveListTypes(model, itemType, ref type3, ref type4);
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
					if (type.get_IsGenericType() && type.GetGenericTypeDefinition() == model.MapType(typeof(IDictionary)) && itemType == model.MapType(typeof(KeyValuePair)).MakeGenericType(genericArguments = type.GetGenericArguments()))
					{
						defaultType = model.MapType(typeof(Dictionary)).MakeGenericType(genericArguments);
					}
					else
					{
						defaultType = model.MapType(typeof(List)).MakeGenericType(new Type[]
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

		private void Add(ValueMember member)
		{
			int opaqueToken = 0;
			try
			{
				this.model.TakeLock(ref opaqueToken);
				this.ThrowIfFrozen();
				this.fields.Add(member);
			}
			finally
			{
				this.model.ReleaseLock(opaqueToken);
			}
		}

		public ValueMember[] GetFields()
		{
			ValueMember[] array = new ValueMember[this.fields.Count];
			this.fields.CopyTo(array, 0);
			Array.Sort<ValueMember>(array, ValueMember.Comparer.Default);
			return array;
		}

		public SubType[] GetSubtypes()
		{
			if (this.subTypes == null || this.subTypes.Count == 0)
			{
				return new SubType[0];
			}
			SubType[] array = new SubType[this.subTypes.Count];
			this.subTypes.CopyTo(array, 0);
			Array.Sort<SubType>(array, SubType.Comparer.Default);
			return array;
		}

		internal bool IsDefined(int fieldNumber)
		{
			BasicList.NodeEnumerator enumerator = this.fields.GetEnumerator();
			while (enumerator.MoveNext())
			{
				ValueMember valueMember = (ValueMember)enumerator.Current;
				if (valueMember.FieldNumber == fieldNumber)
				{
					return true;
				}
			}
			return false;
		}

		internal int GetKey(bool demand, bool getBaseKey)
		{
			return this.model.GetKey(this.type, demand, getBaseKey);
		}

		internal EnumSerializer.EnumPair[] GetEnumMap()
		{
			if (this.HasFlag(2))
			{
				return null;
			}
			EnumSerializer.EnumPair[] array = new EnumSerializer.EnumPair[this.fields.Count];
			for (int i = 0; i < array.Length; i++)
			{
				ValueMember valueMember = (ValueMember)this.fields[i];
				int fieldNumber = valueMember.FieldNumber;
				object rawEnumValue = valueMember.GetRawEnumValue();
				array[i] = new EnumSerializer.EnumPair(fieldNumber, rawEnumValue, valueMember.MemberType);
			}
			return array;
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

		internal static MetaType GetRootType(MetaType source)
		{
			while (source.serializer != null)
			{
				MetaType metaType = source.baseType;
				if (metaType == null)
				{
					return source;
				}
				source = metaType;
			}
			RuntimeTypeModel runtimeTypeModel = source.model;
			int opaqueToken = 0;
			MetaType result;
			try
			{
				runtimeTypeModel.TakeLock(ref opaqueToken);
				MetaType metaType2;
				while ((metaType2 = source.baseType) != null)
				{
					source = metaType2;
				}
				result = source;
			}
			finally
			{
				runtimeTypeModel.ReleaseLock(opaqueToken);
			}
			return result;
		}

		internal bool IsPrepared()
		{
			return false;
		}

		internal static StringBuilder NewLine(StringBuilder builder, int indent)
		{
			return Helpers.AppendLine(builder).Append(' ', indent * 3);
		}

		internal void WriteSchema(StringBuilder builder, int indent, ref bool requiresBclImport)
		{
			if (this.surrogate != null)
			{
				return;
			}
			ValueMember[] array = new ValueMember[this.fields.Count];
			this.fields.CopyTo(array, 0);
			Array.Sort<ValueMember>(array, ValueMember.Comparer.Default);
			if (this.IsList)
			{
				string schemaTypeName = this.model.GetSchemaTypeName(TypeModel.GetListItemType(this.model, this.type), DataFormat.Default, false, false, ref requiresBclImport);
				MetaType.NewLine(builder, indent).Append("message ").Append(this.GetSchemaTypeName()).Append(" {");
				MetaType.NewLine(builder, indent + 1).Append("repeated ").Append(schemaTypeName).Append(" items = 1;");
				MetaType.NewLine(builder, indent).Append('}');
			}
			else if (this.IsAutoTuple)
			{
				MemberInfo[] array2;
				if (MetaType.ResolveTupleConstructor(this.type, out array2) != null)
				{
					MetaType.NewLine(builder, indent).Append("message ").Append(this.GetSchemaTypeName()).Append(" {");
					for (int i = 0; i < array2.Length; i++)
					{
						Type effectiveType;
						if (array2[i] is PropertyInfo)
						{
							effectiveType = ((PropertyInfo)array2[i]).get_PropertyType();
						}
						else
						{
							if (!(array2[i] is FieldInfo))
							{
								throw new NotSupportedException("Unknown member type: " + array2[i].GetType().get_Name());
							}
							effectiveType = ((FieldInfo)array2[i]).get_FieldType();
						}
						MetaType.NewLine(builder, indent + 1).Append("optional ").Append(this.model.GetSchemaTypeName(effectiveType, DataFormat.Default, false, false, ref requiresBclImport).Replace('.', '_')).Append(' ').Append(array2[i].get_Name()).Append(" = ").Append(i + 1).Append(';');
					}
					MetaType.NewLine(builder, indent).Append('}');
				}
			}
			else if (Helpers.IsEnum(this.type))
			{
				MetaType.NewLine(builder, indent).Append("enum ").Append(this.GetSchemaTypeName()).Append(" {");
				if (array.Length == 0 && this.EnumPassthru)
				{
					if (this.type.IsDefined(this.model.MapType(typeof(FlagsAttribute)), false))
					{
						MetaType.NewLine(builder, indent + 1).Append("// this is a composite/flags enumeration");
					}
					else
					{
						MetaType.NewLine(builder, indent + 1).Append("// this enumeration will be passed as a raw value");
					}
					FieldInfo[] array3 = this.type.GetFields();
					for (int j = 0; j < array3.Length; j++)
					{
						FieldInfo fieldInfo = array3[j];
						if (fieldInfo.get_IsStatic() && fieldInfo.get_IsLiteral())
						{
							object rawConstantValue = fieldInfo.GetRawConstantValue();
							MetaType.NewLine(builder, indent + 1).Append(fieldInfo.get_Name()).Append(" = ").Append(rawConstantValue).Append(";");
						}
					}
				}
				else
				{
					ValueMember[] array4 = array;
					for (int k = 0; k < array4.Length; k++)
					{
						ValueMember valueMember = array4[k];
						MetaType.NewLine(builder, indent + 1).Append(valueMember.Name).Append(" = ").Append(valueMember.FieldNumber).Append(';');
					}
				}
				MetaType.NewLine(builder, indent).Append('}');
			}
			else
			{
				MetaType.NewLine(builder, indent).Append("message ").Append(this.GetSchemaTypeName()).Append(" {");
				ValueMember[] array5 = array;
				for (int l = 0; l < array5.Length; l++)
				{
					ValueMember valueMember2 = array5[l];
					string text = (valueMember2.ItemType == null) ? ((!valueMember2.IsRequired) ? "optional" : "required") : "repeated";
					MetaType.NewLine(builder, indent + 1).Append(text).Append(' ');
					if (valueMember2.DataFormat == DataFormat.Group)
					{
						builder.Append("group ");
					}
					string schemaTypeName2 = valueMember2.GetSchemaTypeName(true, ref requiresBclImport);
					builder.Append(schemaTypeName2).Append(" ").Append(valueMember2.Name).Append(" = ").Append(valueMember2.FieldNumber);
					if (valueMember2.DefaultValue != null)
					{
						if (valueMember2.DefaultValue is string)
						{
							builder.Append(" [default = \"").Append(valueMember2.DefaultValue).Append("\"]");
						}
						else if (valueMember2.DefaultValue is bool)
						{
							builder.Append((!(bool)valueMember2.DefaultValue) ? " [default = false]" : " [default = true]");
						}
						else
						{
							builder.Append(" [default = ").Append(valueMember2.DefaultValue).Append(']');
						}
					}
					if (valueMember2.ItemType != null && valueMember2.IsPacked)
					{
						builder.Append(" [packed=true]");
					}
					builder.Append(';');
					if (schemaTypeName2 == "bcl.NetObjectProxy" && valueMember2.AsReference && !valueMember2.DynamicType)
					{
						builder.Append(" // reference-tracked ").Append(valueMember2.GetSchemaTypeName(false, ref requiresBclImport));
					}
				}
				if (this.subTypes != null && this.subTypes.Count != 0)
				{
					MetaType.NewLine(builder, indent + 1).Append("// the following represent sub-types; at most 1 should have a value");
					SubType[] array6 = new SubType[this.subTypes.Count];
					this.subTypes.CopyTo(array6, 0);
					Array.Sort<SubType>(array6, SubType.Comparer.Default);
					SubType[] array7 = array6;
					for (int m = 0; m < array7.Length; m++)
					{
						SubType subType = array7[m];
						string schemaTypeName3 = subType.DerivedType.GetSchemaTypeName();
						MetaType.NewLine(builder, indent + 1).Append("optional ").Append(schemaTypeName3).Append(" ").Append(schemaTypeName3).Append(" = ").Append(subType.FieldNumber).Append(';');
					}
				}
				MetaType.NewLine(builder, indent).Append('}');
			}
		}
	}
}
