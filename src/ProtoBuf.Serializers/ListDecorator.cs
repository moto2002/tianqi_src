using ProtoBuf.Meta;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace ProtoBuf.Serializers
{
	internal class ListDecorator : ProtoDecoratorBase
	{
		private const byte OPTIONS_IsList = 1;

		private const byte OPTIONS_SuppressIList = 2;

		private const byte OPTIONS_WritePacked = 4;

		private const byte OPTIONS_ReturnList = 8;

		private const byte OPTIONS_OverwriteList = 16;

		private const byte OPTIONS_SupportNull = 32;

		private readonly byte options;

		private readonly Type declaredType;

		private readonly Type concreteType;

		private readonly MethodInfo add;

		private readonly int fieldNumber;

		protected readonly WireType packedWireType;

		private static readonly Type ienumeratorType = typeof(IEnumerator);

		private static readonly Type ienumerableType = typeof(IEnumerable);

		private bool IsList
		{
			get
			{
				return (this.options & 1) != 0;
			}
		}

		private bool SuppressIList
		{
			get
			{
				return (this.options & 2) != 0;
			}
		}

		private bool WritePacked
		{
			get
			{
				return (this.options & 4) != 0;
			}
		}

		private bool SupportNull
		{
			get
			{
				return (this.options & 32) != 0;
			}
		}

		private bool ReturnList
		{
			get
			{
				return (this.options & 8) != 0;
			}
		}

		protected virtual bool RequireAdd
		{
			get
			{
				return true;
			}
		}

		public override Type ExpectedType
		{
			get
			{
				return this.declaredType;
			}
		}

		public override bool RequiresOldValue
		{
			get
			{
				return this.AppendToCollection;
			}
		}

		public override bool ReturnsValue
		{
			get
			{
				return this.ReturnList;
			}
		}

		protected bool AppendToCollection
		{
			get
			{
				return (this.options & 16) == 0;
			}
		}

		protected ListDecorator(TypeModel model, Type declaredType, Type concreteType, IProtoSerializer tail, int fieldNumber, bool writePacked, WireType packedWireType, bool returnList, bool overwriteList, bool supportNull) : base(tail)
		{
			if (returnList)
			{
				this.options |= 8;
			}
			if (overwriteList)
			{
				this.options |= 16;
			}
			if (supportNull)
			{
				this.options |= 32;
			}
			if ((writePacked || packedWireType != WireType.None) && fieldNumber <= 0)
			{
				throw new ArgumentOutOfRangeException("fieldNumber");
			}
			if (!ListDecorator.CanPack(packedWireType))
			{
				if (writePacked)
				{
					throw new InvalidOperationException("Only simple data-types can use packed encoding");
				}
				packedWireType = WireType.None;
			}
			this.fieldNumber = fieldNumber;
			if (writePacked)
			{
				this.options |= 4;
			}
			this.packedWireType = packedWireType;
			if (declaredType == null)
			{
				throw new ArgumentNullException("declaredType");
			}
			if (declaredType.get_IsArray())
			{
				throw new ArgumentException("Cannot treat arrays as lists", "declaredType");
			}
			this.declaredType = declaredType;
			this.concreteType = concreteType;
			if (this.RequireAdd)
			{
				bool flag;
				this.add = TypeModel.ResolveListAdd(model, declaredType, tail.ExpectedType, out flag);
				if (flag)
				{
					this.options |= 1;
					string fullName = declaredType.get_FullName();
					if (fullName != null && fullName.StartsWith("System.Data.Linq.EntitySet`1[["))
					{
						this.options |= 2;
					}
				}
				if (this.add == null)
				{
					throw new InvalidOperationException("Unable to resolve a suitable Add method for " + declaredType.get_FullName());
				}
			}
		}

		internal static bool CanPack(WireType wireType)
		{
			switch (wireType)
			{
			case WireType.Variant:
			case WireType.Fixed64:
			case WireType.Fixed32:
				return true;
			case WireType.String:
			case WireType.StartGroup:
			case WireType.EndGroup:
				IL_20:
				if (wireType != WireType.SignedVariant)
				{
					return false;
				}
				return true;
			}
			goto IL_20;
		}

		internal static ListDecorator Create(TypeModel model, Type declaredType, Type concreteType, IProtoSerializer tail, int fieldNumber, bool writePacked, WireType packedWireType, bool returnList, bool overwriteList, bool supportNull)
		{
			MethodInfo builderFactory;
			MethodInfo methodInfo;
			MethodInfo addRange;
			MethodInfo finish;
			if (returnList && ImmutableCollectionDecorator.IdentifyImmutable(model, declaredType, out builderFactory, out methodInfo, out addRange, out finish))
			{
				return new ImmutableCollectionDecorator(model, declaredType, concreteType, tail, fieldNumber, writePacked, packedWireType, returnList, overwriteList, supportNull, builderFactory, methodInfo, addRange, finish);
			}
			return new ListDecorator(model, declaredType, concreteType, tail, fieldNumber, writePacked, packedWireType, returnList, overwriteList, supportNull);
		}

		protected MethodInfo GetEnumeratorInfo(TypeModel model, out MethodInfo moveNext, out MethodInfo current)
		{
			Type type = null;
			Type expectedType = this.ExpectedType;
			MethodInfo instanceMethod = Helpers.GetInstanceMethod(expectedType, "GetEnumerator", null);
			Type expectedType2 = this.Tail.ExpectedType;
			Type returnType;
			Type type2;
			if (instanceMethod != null)
			{
				returnType = instanceMethod.get_ReturnType();
				type2 = returnType;
				moveNext = Helpers.GetInstanceMethod(type2, "MoveNext", null);
				PropertyInfo property = Helpers.GetProperty(type2, "Current", false);
				current = ((property != null) ? Helpers.GetGetMethod(property, false, false) : null);
				if (moveNext == null && model.MapType(ListDecorator.ienumeratorType).IsAssignableFrom(type2))
				{
					moveNext = Helpers.GetInstanceMethod(model.MapType(ListDecorator.ienumeratorType), "MoveNext", null);
				}
				if (moveNext != null && moveNext.get_ReturnType() == model.MapType(typeof(bool)) && current != null && current.get_ReturnType() == expectedType2)
				{
					return instanceMethod;
				}
				MethodInfo methodInfo;
				current = (methodInfo = null);
				moveNext = methodInfo;
			}
			Type type3 = model.MapType(typeof(IEnumerable), false);
			if (type3 != null)
			{
				type3 = type3.MakeGenericType(new Type[]
				{
					expectedType2
				});
				type = type3;
			}
			if (type != null && type.IsAssignableFrom(expectedType))
			{
				instanceMethod = Helpers.GetInstanceMethod(type, "GetEnumerator");
				returnType = instanceMethod.get_ReturnType();
				type2 = returnType;
				moveNext = Helpers.GetInstanceMethod(model.MapType(ListDecorator.ienumeratorType), "MoveNext");
				current = Helpers.GetGetMethod(Helpers.GetProperty(type2, "Current", false), false, false);
				return instanceMethod;
			}
			type = model.MapType(ListDecorator.ienumerableType);
			instanceMethod = Helpers.GetInstanceMethod(type, "GetEnumerator");
			returnType = instanceMethod.get_ReturnType();
			type2 = returnType;
			moveNext = Helpers.GetInstanceMethod(type2, "MoveNext");
			current = Helpers.GetGetMethod(Helpers.GetProperty(type2, "Current", false), false, false);
			return instanceMethod;
		}

		public override void Write(object value, ProtoWriter dest)
		{
			bool writePacked = this.WritePacked;
			SubItemToken token;
			if (writePacked)
			{
				ProtoWriter.WriteFieldHeader(this.fieldNumber, WireType.String, dest);
				token = ProtoWriter.StartSubItem(value, dest);
				ProtoWriter.SetPackedField(this.fieldNumber, dest);
			}
			else
			{
				token = default(SubItemToken);
			}
			bool flag = !this.SupportNull;
			IEnumerator enumerator = ((IEnumerable)value).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object current = enumerator.get_Current();
					if (flag && current == null)
					{
						throw new NullReferenceException();
					}
					this.Tail.Write(current, dest);
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
			if (writePacked)
			{
				ProtoWriter.EndSubItem(token, dest);
			}
		}

		public override object Read(object value, ProtoReader source)
		{
			int field = source.FieldNumber;
			object obj = value;
			if (value == null)
			{
				value = Activator.CreateInstance(this.concreteType);
			}
			bool flag = this.IsList && !this.SuppressIList;
			if (this.packedWireType != WireType.None && source.WireType == WireType.String)
			{
				SubItemToken token = ProtoReader.StartSubItem(source);
				if (flag)
				{
					IList list = (IList)value;
					while (ProtoReader.HasSubValue(this.packedWireType, source))
					{
						list.Add(this.Tail.Read(null, source));
					}
				}
				else
				{
					object[] array = new object[1];
					while (ProtoReader.HasSubValue(this.packedWireType, source))
					{
						array[0] = this.Tail.Read(null, source);
						this.add.Invoke(value, array);
					}
				}
				ProtoReader.EndSubItem(token, source);
			}
			else if (flag)
			{
				IList list2 = (IList)value;
				do
				{
					list2.Add(this.Tail.Read(null, source));
				}
				while (source.TryReadFieldHeader(field));
			}
			else
			{
				object[] array2 = new object[1];
				do
				{
					array2[0] = this.Tail.Read(null, source);
					this.add.Invoke(value, array2);
				}
				while (source.TryReadFieldHeader(field));
			}
			return (obj != value) ? value : null;
		}
	}
}
