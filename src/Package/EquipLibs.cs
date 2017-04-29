using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(389), ForSend(389), ProtoContract(Name = "EquipLibs")]
	[Serializable]
	public class EquipLibs : IExtensible
	{
		public static readonly short OP = 389;

		private readonly List<EquipLib> _libs = new List<EquipLib>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "libs", DataFormat = DataFormat.Default)]
		public List<EquipLib> libs
		{
			get
			{
				return this._libs;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
