using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(610), ForSend(610), ProtoContract(Name = "GemSlotOpenNty")]
	[Serializable]
	public class GemSlotOpenNty : IExtensible
	{
		public static readonly short OP = 610;

		private readonly List<GemSlotOpen> _slotOpen = new List<GemSlotOpen>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "slotOpen", DataFormat = DataFormat.Default)]
		public List<GemSlotOpen> slotOpen
		{
			get
			{
				return this._slotOpen;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
