using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(725), ForSend(725), ProtoContract(Name = "MonthCardInfoPush")]
	[Serializable]
	public class MonthCardInfoPush : IExtensible
	{
		public static readonly short OP = 725;

		private int _Times;

		private int _silver;

		private int _gold;

		private int _diamond;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "Times", DataFormat = DataFormat.TwosComplement)]
		public int Times
		{
			get
			{
				return this._Times;
			}
			set
			{
				this._Times = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "silver", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int silver
		{
			get
			{
				return this._silver;
			}
			set
			{
				this._silver = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "gold", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int gold
		{
			get
			{
				return this._gold;
			}
			set
			{
				this._gold = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "diamond", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int diamond
		{
			get
			{
				return this._diamond;
			}
			set
			{
				this._diamond = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
