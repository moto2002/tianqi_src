using NetWork.Utilities;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ForRecv(934), ForSend(934), ProtoContract(Name = "MultiPvpBattleInfoNty")]
	[Serializable]
	public class MultiPvpBattleInfoNty : IExtensible
	{
		[ProtoContract(Name = "MultiPvpBattleInfo")]
		[Serializable]
		public class MultiPvpBattleInfo : IExtensible
		{
			private int _camp;

			private int _killCount;

			private IExtension extensionObject;

			[ProtoMember(1, IsRequired = false, Name = "camp", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int camp
			{
				get
				{
					return this._camp;
				}
				set
				{
					this._camp = value;
				}
			}

			[ProtoMember(2, IsRequired = false, Name = "killCount", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
			public int killCount
			{
				get
				{
					return this._killCount;
				}
				set
				{
					this._killCount = value;
				}
			}

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		public static readonly short OP = 934;

		private readonly List<MultiPvpBattleInfoNty.MultiPvpBattleInfo> _infoList = new List<MultiPvpBattleInfoNty.MultiPvpBattleInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, Name = "infoList", DataFormat = DataFormat.Default)]
		public List<MultiPvpBattleInfoNty.MultiPvpBattleInfo> infoList
		{
			get
			{
				return this._infoList;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
