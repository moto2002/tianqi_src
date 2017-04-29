using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(2002), ForSend(2002), ProtoContract(Name = "EndMemoryFlopReq")]
	[Serializable]
	public class EndMemoryFlopReq : IExtensible
	{
		public static readonly short OP = 2002;

		private readonly List<int> _cardArrange = new List<int>();

		private int _flopTimes;

		private IExtension extensionObject;

		[ProtoMember(1, Name = "cardArrange", DataFormat = DataFormat.TwosComplement)]
		public List<int> cardArrange
		{
			get
			{
				return this._cardArrange;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "flopTimes", DataFormat = DataFormat.TwosComplement)]
		public int flopTimes
		{
			get
			{
				return this._flopTimes;
			}
			set
			{
				this._flopTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
