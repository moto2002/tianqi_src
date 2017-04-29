using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(745), ForSend(745), ProtoContract(Name = "PveEnterBattleNty")]
	[Serializable]
	public class PveEnterBattleNty : IExtensible
	{
		public static readonly short OP = 745;

		private int _dungeonId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "dungeonId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
