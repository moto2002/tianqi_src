using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "FHuSongKuangCheJiangLi")]
	[Serializable]
	public class FHuSongKuangCheJiangLi : IExtensible
	{
		private int _id;

		private int _rewardId;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "rewardId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int rewardId
		{
			get
			{
				return this._rewardId;
			}
			set
			{
				this._rewardId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
