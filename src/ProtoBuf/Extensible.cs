using ProtoBuf.Meta;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ProtoBuf
{
	public abstract class Extensible : IExtensible
	{
		private IExtension extensionObject;

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return this.GetExtensionObject(createIfMissing);
		}

		protected virtual IExtension GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}

		public static IExtension GetExtensionObject(ref IExtension extensionObject, bool createIfMissing)
		{
			if (createIfMissing && extensionObject == null)
			{
				extensionObject = new BufferExtension();
			}
			return extensionObject;
		}

		public static void AppendValue<TValue>(IExtensible instance, int tag, TValue value)
		{
			Extensible.AppendValue<TValue>(instance, tag, DataFormat.Default, value);
		}

		public static void AppendValue<TValue>(IExtensible instance, int tag, DataFormat format, TValue value)
		{
			ExtensibleUtil.AppendExtendValue(RuntimeTypeModel.Default, instance, tag, format, value);
		}

		public static TValue GetValue<TValue>(IExtensible instance, int tag)
		{
			return Extensible.GetValue<TValue>(instance, tag, DataFormat.Default);
		}

		public static TValue GetValue<TValue>(IExtensible instance, int tag, DataFormat format)
		{
			TValue result;
			Extensible.TryGetValue<TValue>(instance, tag, format, out result);
			return result;
		}

		public static bool TryGetValue<TValue>(IExtensible instance, int tag, out TValue value)
		{
			return Extensible.TryGetValue<TValue>(instance, tag, DataFormat.Default, out value);
		}

		public static bool TryGetValue<TValue>(IExtensible instance, int tag, DataFormat format, out TValue value)
		{
			return Extensible.TryGetValue<TValue>(instance, tag, format, false, out value);
		}

		public static bool TryGetValue<TValue>(IExtensible instance, int tag, DataFormat format, bool allowDefinedTag, out TValue value)
		{
			value = default(TValue);
			bool result = false;
			using (IEnumerator<TValue> enumerator = ExtensibleUtil.GetExtendedValues<TValue>(instance, tag, format, true, allowDefinedTag).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TValue current = enumerator.get_Current();
					value = current;
					result = true;
				}
			}
			return result;
		}

		public static IEnumerable<TValue> GetValues<TValue>(IExtensible instance, int tag)
		{
			return ExtensibleUtil.GetExtendedValues<TValue>(instance, tag, DataFormat.Default, false, false);
		}

		public static IEnumerable<TValue> GetValues<TValue>(IExtensible instance, int tag, DataFormat format)
		{
			return ExtensibleUtil.GetExtendedValues<TValue>(instance, tag, format, false, false);
		}

		public static bool TryGetValue(TypeModel model, Type type, IExtensible instance, int tag, DataFormat format, bool allowDefinedTag, out object value)
		{
			value = null;
			bool result = false;
			IEnumerator enumerator = ExtensibleUtil.GetExtendedValues(model, type, instance, tag, format, true, allowDefinedTag).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object current = enumerator.get_Current();
					value = current;
					result = true;
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
			return result;
		}

		public static IEnumerable GetValues(TypeModel model, Type type, IExtensible instance, int tag, DataFormat format)
		{
			return ExtensibleUtil.GetExtendedValues(model, type, instance, tag, format, false, false);
		}

		public static void AppendValue(TypeModel model, IExtensible instance, int tag, DataFormat format, object value)
		{
			ExtensibleUtil.AppendExtendValue(model, instance, tag, format, value);
		}
	}
}
