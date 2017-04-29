using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "MineInfoResidueTime")]
	[Serializable]
	public class MineInfoResidueTime : IExtensible
	{
		private string _blockId;

		private int _residueTime;

		private readonly List<DebrisInfo> _debrisInfos = new List<DebrisInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "blockId", DataFormat = DataFormat.Default)]
		public string blockId
		{
			get
			{
				return this._blockId;
			}
			set
			{
				this._blockId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "residueTime", DataFormat = DataFormat.TwosComplement)]
		public int residueTime
		{
			get
			{
				return this._residueTime;
			}
			set
			{
				this._residueTime = value;
			}
		}

		[ProtoMember(6, Name = "debrisInfos", DataFormat = DataFormat.Default)]
		public List<DebrisInfo> debrisInfos
		{
			get
			{
				return this._debrisInfos;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
