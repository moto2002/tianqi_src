using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(346), ForSend(346), ProtoContract(Name = "PetInfos")]
	[Serializable]
	public class PetInfos : IExtensible
	{
		public static readonly short OP = 346;

		private readonly List<PetInfo> _info = new List<PetInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "info", DataFormat = DataFormat.Default)]
		public List<PetInfo> info
		{
			get
			{
				return this._info;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
