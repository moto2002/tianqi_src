using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(3805), ForSend(3805), ProtoContract(Name = "OpenMainCityNty")]
	[Serializable]
	public class OpenMainCityNty : IExtensible
	{
		public static readonly short OP = 3805;

		private readonly List<int> _ids = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "ids", DataFormat = DataFormat.TwosComplement)]
		public List<int> ids
		{
			get
			{
				return this._ids;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
