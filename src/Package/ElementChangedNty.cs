using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(723), ForSend(723), ProtoContract(Name = "ElementChangedNty")]
	[Serializable]
	public class ElementChangedNty : IExtensible
	{
		public static readonly short OP = 723;

		private readonly List<ElementInfo> _elems = new List<ElementInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "elems", DataFormat = DataFormat.Default)]
		public List<ElementInfo> elems
		{
			get
			{
				return this._elems;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
