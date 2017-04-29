using ProtoBuf.Meta;
using System;
using System.Reflection;

namespace ProtoBuf.Serializers
{
	internal sealed class ParseableSerializer : IProtoSerializer
	{
		private readonly MethodInfo parse;

		bool IProtoSerializer.RequiresOldValue
		{
			get
			{
				return false;
			}
		}

		bool IProtoSerializer.ReturnsValue
		{
			get
			{
				return true;
			}
		}

		public Type ExpectedType
		{
			get
			{
				return this.parse.get_DeclaringType();
			}
		}

		private ParseableSerializer(MethodInfo parse)
		{
			this.parse = parse;
		}

		public static ParseableSerializer TryCreate(Type type, TypeModel model)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			MethodInfo method = type.GetMethod("Parse", 26, null, new Type[]
			{
				model.MapType(typeof(string))
			}, null);
			if (method != null && method.get_ReturnType() == type)
			{
				if (Helpers.IsValueType(type))
				{
					MethodInfo customToString = ParseableSerializer.GetCustomToString(type);
					if (customToString == null || customToString.get_ReturnType() != model.MapType(typeof(string)))
					{
						return null;
					}
				}
				return new ParseableSerializer(method);
			}
			return null;
		}

		private static MethodInfo GetCustomToString(Type type)
		{
			return type.GetMethod("ToString", 22, null, Helpers.EmptyTypes, null);
		}

		public object Read(object value, ProtoReader source)
		{
			return this.parse.Invoke(null, new object[]
			{
				source.ReadString()
			});
		}

		public void Write(object value, ProtoWriter dest)
		{
			ProtoWriter.WriteString(value.ToString(), dest);
		}
	}
}
