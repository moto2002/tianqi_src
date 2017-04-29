using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProtoBuf
{
	public static class Serializer
	{
		public static class NonGeneric
		{
			public static object DeepClone(object instance)
			{
				return (instance != null) ? RuntimeTypeModel.Default.DeepClone(instance) : null;
			}

			public static void Serialize(Stream dest, object instance)
			{
				if (instance != null)
				{
					RuntimeTypeModel.Default.Serialize(dest, instance);
				}
			}

			public static object Deserialize(Type type, Stream source)
			{
				return RuntimeTypeModel.Default.Deserialize(source, null, type);
			}

			public static object Merge(Stream source, object instance)
			{
				if (instance == null)
				{
					throw new ArgumentNullException("instance");
				}
				return RuntimeTypeModel.Default.Deserialize(source, instance, instance.GetType(), null);
			}

			public static void SerializeWithLengthPrefix(Stream destination, object instance, PrefixStyle style, int fieldNumber)
			{
				if (instance == null)
				{
					throw new ArgumentNullException("instance");
				}
				RuntimeTypeModel @default = RuntimeTypeModel.Default;
				@default.SerializeWithLengthPrefix(destination, instance, @default.MapType(instance.GetType()), style, fieldNumber);
			}

			public static bool TryDeserializeWithLengthPrefix(Stream source, PrefixStyle style, Serializer.TypeResolver resolver, out object value)
			{
				value = RuntimeTypeModel.Default.DeserializeWithLengthPrefix(source, null, null, style, 0, resolver);
				return value != null;
			}

			public static bool CanSerialize(Type type)
			{
				return RuntimeTypeModel.Default.IsDefined(type);
			}
		}

		public static class GlobalOptions
		{
			[Obsolete("Please use RuntimeTypeModel.Default.InferTagFromNameDefault instead (or on a per-model basis)", false)]
			public static bool InferTagFromName
			{
				get
				{
					return RuntimeTypeModel.Default.InferTagFromNameDefault;
				}
				set
				{
					RuntimeTypeModel.Default.InferTagFromNameDefault = value;
				}
			}
		}

		public delegate Type TypeResolver(int fieldNumber);

		private const string ProtoBinaryField = "proto";

		public const int ListItemTag = 1;

		public static string GetProto<T>()
		{
			return RuntimeTypeModel.Default.GetSchema(RuntimeTypeModel.Default.MapType(typeof(T)));
		}

		public static T DeepClone<T>(T instance)
		{
			return (instance != null) ? ((T)((object)RuntimeTypeModel.Default.DeepClone(instance))) : instance;
		}

		public static T Merge<T>(Stream source, T instance)
		{
			return (T)((object)RuntimeTypeModel.Default.Deserialize(source, instance, typeof(T)));
		}

		public static T Deserialize<T>(Stream source)
		{
			return (T)((object)RuntimeTypeModel.Default.Deserialize(source, null, typeof(T)));
		}

		public static void Serialize<T>(Stream destination, T instance)
		{
			if (instance != null)
			{
				RuntimeTypeModel.Default.Serialize(destination, instance);
			}
		}

		public static TTo ChangeType<TFrom, TTo>(TFrom instance)
		{
			TTo result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Serializer.Serialize<TFrom>(memoryStream, instance);
				memoryStream.set_Position(0L);
				result = Serializer.Deserialize<TTo>(memoryStream);
			}
			return result;
		}

		public static void PrepareSerializer<T>()
		{
		}

		public static IEnumerable<T> DeserializeItems<T>(Stream source, PrefixStyle style, int fieldNumber)
		{
			return RuntimeTypeModel.Default.DeserializeItems<T>(source, style, fieldNumber);
		}

		public static T DeserializeWithLengthPrefix<T>(Stream source, PrefixStyle style)
		{
			return Serializer.DeserializeWithLengthPrefix<T>(source, style, 0);
		}

		public static T DeserializeWithLengthPrefix<T>(Stream source, PrefixStyle style, int fieldNumber)
		{
			RuntimeTypeModel @default = RuntimeTypeModel.Default;
			return (T)((object)@default.DeserializeWithLengthPrefix(source, null, @default.MapType(typeof(T)), style, fieldNumber));
		}

		public static T MergeWithLengthPrefix<T>(Stream source, T instance, PrefixStyle style)
		{
			RuntimeTypeModel @default = RuntimeTypeModel.Default;
			return (T)((object)@default.DeserializeWithLengthPrefix(source, instance, @default.MapType(typeof(T)), style, 0));
		}

		public static void SerializeWithLengthPrefix<T>(Stream destination, T instance, PrefixStyle style)
		{
			Serializer.SerializeWithLengthPrefix<T>(destination, instance, style, 0);
		}

		public static void SerializeWithLengthPrefix<T>(Stream destination, T instance, PrefixStyle style, int fieldNumber)
		{
			RuntimeTypeModel @default = RuntimeTypeModel.Default;
			@default.SerializeWithLengthPrefix(destination, instance, @default.MapType(typeof(T)), style, fieldNumber);
		}

		public static bool TryReadLengthPrefix(Stream source, PrefixStyle style, out int length)
		{
			int num;
			int num2;
			length = ProtoReader.ReadLengthPrefix(source, false, style, out num, out num2);
			return num2 > 0;
		}

		public static bool TryReadLengthPrefix(byte[] buffer, int index, int count, PrefixStyle style, out int length)
		{
			bool result;
			using (Stream stream = new MemoryStream(buffer, index, count))
			{
				result = Serializer.TryReadLengthPrefix(stream, style, out length);
			}
			return result;
		}

		public static void FlushPool()
		{
			BufferPool.Flush();
		}
	}
}
