using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1799), ForSend(1799), ProtoContract(Name = "MineInfoChangedNty")]
	[Serializable]
	public class MineInfoChangedNty : IExtensible
	{
		public static readonly short OP = 1799;

		private readonly List<MinePetInfo> _minePetinfos = new List<MinePetInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "minePetinfos", DataFormat = DataFormat.Default)]
		public List<MinePetInfo> minePetinfos
		{
			get
			{
				return this._minePetinfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
