using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(453), ForSend(453), ProtoContract(Name = "MineInfoRes")]
	[Serializable]
	public class MineInfoRes : IExtensible
	{
		public static readonly short OP = 453;

		private readonly List<MineInfo> _mineInfos = new List<MineInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "mineInfos", DataFormat = DataFormat.Default)]
		public List<MineInfo> mineInfos
		{
			get
			{
				return this._mineInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
