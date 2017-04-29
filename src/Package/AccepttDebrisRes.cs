using NetWork.Utilities;
using ProtoBuf;
using System;

namespace Package
{
	[ForRecv(1650), ForSend(1650), ProtoContract(Name = "AccepttDebrisRes")]
	[Serializable]
	public class AccepttDebrisRes : IExtensible
	{
		public static readonly short OP = 1650;

		private MineInfo _mineInfo;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "mineInfo", DataFormat = DataFormat.Default)]
		public MineInfo mineInfo
		{
			get
			{
				return this._mineInfo;
			}
			set
			{
				this._mineInfo = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
