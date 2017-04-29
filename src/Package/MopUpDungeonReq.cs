using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(648), ForSend(648), ProtoContract(Name = "MopUpDungeonReq")]
	[Serializable]
	public class MopUpDungeonReq : IExtensible
	{
		public static readonly short OP = 648;

		private int _dungeonId;

		private int _times = 1;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "dungeonId", DataFormat = DataFormat.TwosComplement)]
		public int dungeonId
		{
			get
			{
				return this._dungeonId;
			}
			set
			{
				this._dungeonId = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "times", DataFormat = DataFormat.TwosComplement), DefaultValue(1)]
		public int times
		{
			get
			{
				return this._times;
			}
			set
			{
				this._times = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
