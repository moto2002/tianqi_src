using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(2001), ForSend(2001), ProtoContract(Name = "BeginMemoryFlopRes")]
	[Serializable]
	public class BeginMemoryFlopRes : IExtensible
	{
		public static readonly short OP = 2001;

		private readonly List<int> _cardIndex = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "cardIndex", DataFormat = DataFormat.TwosComplement)]
		public List<int> cardIndex
		{
			get
			{
				return this._cardIndex;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
