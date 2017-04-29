using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "EquipLib")]
	[Serializable]
	public class EquipLib : IExtensible
	{
		private EquipLibType.ELT _type;

		private readonly List<EquipSimpleInfo> _equips = new List<EquipSimpleInfo>();

		private long _wearingId;

		private int _lv;

		private int _blessValue;

		private int _blessRatio;

		private readonly List<JobToEquip> _jobToEquips = new List<JobToEquip>();

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "type", DataFormat = DataFormat.TwosComplement)]
		public EquipLibType.ELT type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		[ProtoMember(2, Name = "equips", DataFormat = DataFormat.Default)]
		public List<EquipSimpleInfo> equips
		{
			get
			{
				return this._equips;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "wearingId", DataFormat = DataFormat.TwosComplement), DefaultValue(0L)]
		public long wearingId
		{
			get
			{
				return this._wearingId;
			}
			set
			{
				this._wearingId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "lv", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lv
		{
			get
			{
				return this._lv;
			}
			set
			{
				this._lv = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "blessValue", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int blessValue
		{
			get
			{
				return this._blessValue;
			}
			set
			{
				this._blessValue = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "blessRatio", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int blessRatio
		{
			get
			{
				return this._blessRatio;
			}
			set
			{
				this._blessRatio = value;
			}
		}

		[ProtoMember(7, Name = "jobToEquips", DataFormat = DataFormat.Default)]
		public List<JobToEquip> jobToEquips
		{
			get
			{
				return this._jobToEquips;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
