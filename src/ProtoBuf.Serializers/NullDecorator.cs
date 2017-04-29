using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
	internal sealed class NullDecorator : ProtoDecoratorBase
	{
		public const int Tag = 1;

		private readonly Type expectedType;

		public override Type ExpectedType
		{
			get
			{
				return this.expectedType;
			}
		}

		public override bool ReturnsValue
		{
			get
			{
				return true;
			}
		}

		public override bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		public NullDecorator(TypeModel model, IProtoSerializer tail) : base(tail)
		{
			if (!tail.ReturnsValue)
			{
				throw new NotSupportedException("NullDecorator only supports implementations that return values");
			}
			Type type = tail.ExpectedType;
			if (Helpers.IsValueType(type))
			{
				this.expectedType = model.MapType(typeof(Nullable)).MakeGenericType(new Type[]
				{
					type
				});
			}
			else
			{
				this.expectedType = type;
			}
		}

		public override object Read(object value, ProtoReader source)
		{
			SubItemToken token = ProtoReader.StartSubItem(source);
			int num;
			while ((num = source.ReadFieldHeader()) > 0)
			{
				if (num == 1)
				{
					value = this.Tail.Read(value, source);
				}
				else
				{
					source.SkipField();
				}
			}
			ProtoReader.EndSubItem(token, source);
			return value;
		}

		public override void Write(object value, ProtoWriter dest)
		{
			SubItemToken token = ProtoWriter.StartSubItem(null, dest);
			if (value != null)
			{
				this.Tail.Write(value, dest);
			}
			ProtoWriter.EndSubItem(token, dest);
		}
	}
}
