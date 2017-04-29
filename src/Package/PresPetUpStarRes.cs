using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(3698), ForSend(3698), ProtoContract(Name = "PresPetUpStarRes")]
	[Serializable]
	public class PresPetUpStarRes : IExtensible
	{
		public static readonly short OP = 3698;

		private long _fighting;

		private long _afterFighting;

		private int _afterAtk;

		private int _afterDefence;

		private long _afterHpLmt;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = false, Name = "fighting", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long fighting
		{
			get
			{
				return this._fighting;
			}
			set
			{
				this._fighting = value;
			}
		}

		[ProtoMember(2, IsRequired = false, Name = "afterFighting", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long afterFighting
		{
			get
			{
				return this._afterFighting;
			}
			set
			{
				this._afterFighting = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "afterAtk", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int afterAtk
		{
			get
			{
				return this._afterAtk;
			}
			set
			{
				this._afterAtk = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "afterDefence", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int afterDefence
		{
			get
			{
				return this._afterDefence;
			}
			set
			{
				this._afterDefence = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "afterHpLmt", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long afterHpLmt
		{
			get
			{
				return this._afterHpLmt;
			}
			set
			{
				this._afterHpLmt = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
