using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(150), ForSend(150), ProtoContract(Name = "Bags")]
	[Serializable]
	public class Bags : IExtensible
	{
		public static readonly short OP = 150;

		private readonly List<Bag> _bags = new List<Bag>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "bags", DataFormat = DataFormat.Default)]
		public List<Bag> bags
		{
			get
			{
				return this._bags;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
