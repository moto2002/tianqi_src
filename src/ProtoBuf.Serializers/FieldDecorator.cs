using System;
using System.Reflection;

namespace ProtoBuf.Serializers
{
	internal sealed class FieldDecorator : ProtoDecoratorBase
	{
		private readonly FieldInfo field;

		private readonly Type forType;

		public override Type ExpectedType
		{
			get
			{
				return this.forType;
			}
		}

		public override bool RequiresOldValue
		{
			get
			{
				return true;
			}
		}

		public override bool ReturnsValue
		{
			get
			{
				return false;
			}
		}

		public FieldDecorator(Type forType, FieldInfo field, IProtoSerializer tail) : base(tail)
		{
			this.forType = forType;
			this.field = field;
		}

		public override void Write(object value, ProtoWriter dest)
		{
			value = this.field.GetValue(value);
			if (value != null)
			{
				this.Tail.Write(value, dest);
			}
		}

		public override object Read(object value, ProtoReader source)
		{
			object obj = this.Tail.Read((!this.Tail.RequiresOldValue) ? null : this.field.GetValue(value), source);
			if (obj != null)
			{
				this.field.SetValue(value, obj);
			}
			return null;
		}
	}
}
