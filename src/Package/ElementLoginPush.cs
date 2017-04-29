using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(706), ForSend(706), ProtoContract(Name = "ElementLoginPush")]
	[Serializable]
	public class ElementLoginPush : IExtensible
	{
		public static readonly short OP = 706;

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
