using ProtoBuf;
using System;
using System.Collections.Generic;

namespace Package
{
	[ProtoContract(Name = "GuildStorageInfo")]
	[Serializable]
	public class GuildStorageInfo : IExtensible
	{
		private GuildStorageBaseInfo _baseInfo;

		private readonly List<GuildLogTrace> _logTraces = new List<GuildLogTrace>();

		private readonly List<EquipSimpleInfo> _equipsInfo = new List<EquipSimpleInfo>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "baseInfo", DataFormat = DataFormat.Default)]
		public GuildStorageBaseInfo baseInfo
		{
			get
			{
				return this._baseInfo;
			}
			set
			{
				this._baseInfo = value;
			}
		}

		[ProtoMember(2, Name = "logTraces", DataFormat = DataFormat.Default)]
		public List<GuildLogTrace> logTraces
		{
			get
			{
				return this._logTraces;
			}
		}

		[ProtoMember(3, Name = "equipsInfo", DataFormat = DataFormat.Default)]
		public List<EquipSimpleInfo> equipsInfo
		{
			get
			{
				return this._equipsInfo;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
