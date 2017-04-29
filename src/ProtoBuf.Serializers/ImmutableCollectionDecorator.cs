using ProtoBuf.Meta;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace ProtoBuf.Serializers
{
	internal sealed class ImmutableCollectionDecorator : ListDecorator
	{
		private readonly MethodInfo builderFactory;

		private readonly MethodInfo add;

		private readonly MethodInfo addRange;

		private readonly MethodInfo finish;

		protected override bool RequireAdd
		{
			get
			{
				return false;
			}
		}

		internal ImmutableCollectionDecorator(TypeModel model, Type declaredType, Type concreteType, IProtoSerializer tail, int fieldNumber, bool writePacked, WireType packedWireType, bool returnList, bool overwriteList, bool supportNull, MethodInfo builderFactory, MethodInfo add, MethodInfo addRange, MethodInfo finish) : base(model, declaredType, concreteType, tail, fieldNumber, writePacked, packedWireType, returnList, overwriteList, supportNull)
		{
			this.builderFactory = builderFactory;
			this.add = add;
			this.addRange = addRange;
			this.finish = finish;
		}

		private static Type ResolveIReadOnlyCollection(Type declaredType, Type t)
		{
			Type[] interfaces = declaredType.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				Type type = interfaces[i];
				if (type.get_IsGenericType() && type.get_Name().StartsWith("IReadOnlyCollection`"))
				{
					if (t != null)
					{
						Type[] genericArguments = type.GetGenericArguments();
						if (genericArguments.Length != 1 && genericArguments[0] != t)
						{
							goto IL_58;
						}
					}
					return type;
				}
				IL_58:;
			}
			return null;
		}

		internal static bool IdentifyImmutable(TypeModel model, Type declaredType, out MethodInfo builderFactory, out MethodInfo add, out MethodInfo addRange, out MethodInfo finish)
		{
			MethodInfo methodInfo;
			finish = (methodInfo = null);
			addRange = (methodInfo = methodInfo);
			add = (methodInfo = methodInfo);
			builderFactory = methodInfo;
			if (model == null || declaredType == null)
			{
				return false;
			}
			if (!declaredType.get_IsGenericType())
			{
				return false;
			}
			Type[] genericArguments = declaredType.GetGenericArguments();
			int num = genericArguments.Length;
			Type[] array;
			if (num != 1)
			{
				if (num != 2)
				{
					return false;
				}
				Type type = model.MapType(typeof(KeyValuePair));
				if (type == null)
				{
					return false;
				}
				type = type.MakeGenericType(genericArguments);
				array = new Type[]
				{
					type
				};
			}
			else
			{
				array = genericArguments;
			}
			if (ImmutableCollectionDecorator.ResolveIReadOnlyCollection(declaredType, null) == null)
			{
				return false;
			}
			string text = declaredType.get_Name();
			int num2 = text.IndexOf('`');
			if (num2 <= 0)
			{
				return false;
			}
			text = ((!declaredType.get_IsInterface()) ? text.Substring(0, num2) : text.Substring(1, num2 - 1));
			Type type2 = model.GetType(declaredType.get_Namespace() + "." + text, declaredType.get_Assembly());
			if (type2 == null && text == "ImmutableSet")
			{
				type2 = model.GetType(declaredType.get_Namespace() + ".ImmutableHashSet", declaredType.get_Assembly());
			}
			if (type2 == null)
			{
				return false;
			}
			MethodInfo[] methods = type2.GetMethods();
			for (int i = 0; i < methods.Length; i++)
			{
				MethodInfo methodInfo2 = methods[i];
				if (methodInfo2.get_IsStatic() && !(methodInfo2.get_Name() != "CreateBuilder") && methodInfo2.get_IsGenericMethodDefinition() && methodInfo2.GetParameters().Length == 0 && methodInfo2.GetGenericArguments().Length == genericArguments.Length)
				{
					builderFactory = methodInfo2.MakeGenericMethod(genericArguments);
					break;
				}
			}
			Type type3 = model.MapType(typeof(void));
			if (builderFactory == null || builderFactory.get_ReturnType() == null || builderFactory.get_ReturnType() == type3)
			{
				return false;
			}
			add = Helpers.GetInstanceMethod(builderFactory.get_ReturnType(), "Add", array);
			if (add == null)
			{
				return false;
			}
			finish = Helpers.GetInstanceMethod(builderFactory.get_ReturnType(), "ToImmutable", Helpers.EmptyTypes);
			if (finish == null || finish.get_ReturnType() == null || finish.get_ReturnType() == type3)
			{
				return false;
			}
			if (finish.get_ReturnType() != declaredType && !Helpers.IsAssignableFrom(declaredType, finish.get_ReturnType()))
			{
				return false;
			}
			addRange = Helpers.GetInstanceMethod(builderFactory.get_ReturnType(), "AddRange", new Type[]
			{
				declaredType
			});
			if (addRange == null)
			{
				Type type4 = model.MapType(typeof(IEnumerable), false);
				if (type4 != null)
				{
					addRange = Helpers.GetInstanceMethod(builderFactory.get_ReturnType(), "AddRange", new Type[]
					{
						type4.MakeGenericType(array)
					});
				}
			}
			return true;
		}

		public override object Read(object value, ProtoReader source)
		{
			object obj = this.builderFactory.Invoke(null, null);
			int fieldNumber = source.FieldNumber;
			object[] array = new object[1];
			if (base.AppendToCollection && value != null && ((IList)value).get_Count() != 0)
			{
				if (this.addRange != null)
				{
					array[0] = value;
					this.addRange.Invoke(obj, array);
				}
				else
				{
					IEnumerator enumerator = ((IList)value).GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							object current = enumerator.get_Current();
							array[0] = current;
							this.add.Invoke(obj, array);
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
			}
			if (this.packedWireType != WireType.None && source.WireType == WireType.String)
			{
				SubItemToken token = ProtoReader.StartSubItem(source);
				while (ProtoReader.HasSubValue(this.packedWireType, source))
				{
					array[0] = this.Tail.Read(null, source);
					this.add.Invoke(obj, array);
				}
				ProtoReader.EndSubItem(token, source);
			}
			else
			{
				do
				{
					array[0] = this.Tail.Read(null, source);
					this.add.Invoke(obj, array);
				}
				while (source.TryReadFieldHeader(fieldNumber));
			}
			return this.finish.Invoke(obj, null);
		}
	}
}
