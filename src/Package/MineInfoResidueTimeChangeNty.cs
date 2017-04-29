using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ForRecv(1811), ForSend(1811), ProtoContract(Name = "MineInfoResidueTimeChangeNty")]
	[Serializable]
	public class MineInfoResidueTimeChangeNty : IExtensible
	{
		public static readonly short OP = 1811;

		private readonly List<MineInfoResidueTime> _mineInfoResidueTimes = new List<MineInfoResidueTime>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "mineInfoResidueTimes", DataFormat = DataFormat.Default)]
		public List<MineInfoResidueTime> mineInfoResidueTimes
		{
			get
			{
				return this._mineInfoResidueTimes;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
