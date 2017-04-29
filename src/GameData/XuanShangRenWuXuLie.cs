using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "XuanShangRenWuXuLie")]
	[Serializable]
	public class XuanShangRenWuXuLie : IExtensible
	{
		private int _winNum;

		private int _missionID;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "winNum", DataFormat = DataFormat.TwosComplement)]
		public int winNum
		{
			get
			{
				return this._winNum;
			}
			set
			{
				this._winNum = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "missionID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int missionID
		{
			get
			{
				return this._missionID;
			}
			set
			{
				this._missionID = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
