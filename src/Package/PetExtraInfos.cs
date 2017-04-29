using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(572), ForSend(572), ProtoContract(Name = "PetExtraInfos")]
	[Serializable]
	public class PetExtraInfos : IExtensible
	{
		public static readonly short OP = 572;

		private int _skillPoint;

		private int _purchaseNum;

		private int _residueRecoverTime;

		private long _petUUId;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "skillPoint", DataFormat = DataFormat.TwosComplement)]
		public int skillPoint
		{
			get
			{
				return this._skillPoint;
			}
			set
			{
				this._skillPoint = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "purchaseNum", DataFormat = DataFormat.TwosComplement)]
		public int purchaseNum
		{
			get
			{
				return this._purchaseNum;
			}
			set
			{
				this._purchaseNum = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "residueRecoverTime", DataFormat = DataFormat.TwosComplement)]
		public int residueRecoverTime
		{
			get
			{
				return this._residueRecoverTime;
			}
			set
			{
				this._residueRecoverTime = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "petUUId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long petUUId
		{
			get
			{
				return this._petUUId;
			}
			set
			{
				this._petUUId = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
