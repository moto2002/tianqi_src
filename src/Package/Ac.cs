using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Package
{
	[ProtoContract(Name = "Ac")]
	[Serializable]
	public class Ac : IExtensible
	{
		[ProtoContract(Name = "AcType")]
		[Serializable]
		public class AcType : IExtensible
		{
			[ProtoContract(Name = "Type")]
			public enum Type
			{
				[ProtoEnum(Name = "Recharge", Value = 1)]
				Recharge = 1,
				[ProtoEnum(Name = "Cost", Value = 6)]
				Cost = 6
			}

			private IExtension extensionObject;

			IExtension IExtensible.GetExtensionObject(bool createIfMissing)
			{
				return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
			}
		}

		private Ac.AcType.Type _typeId;

		private readonly List<AcItemInfo> _acItemInfo = new List<AcItemInfo>();

		private int _beginTime;

		private int _endTime;

		private int _curData;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "typeId", DataFormat = DataFormat.TwosComplement)]
		public Ac.AcType.Type typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				this._typeId = value;
			}
		}

		[ProtoMember(2, Name = "acItemInfo", DataFormat = DataFormat.Default)]
		public List<AcItemInfo> acItemInfo
		{
			get
			{
				return this._acItemInfo;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "beginTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int beginTime
		{
			get
			{
				return this._beginTime;
			}
			set
			{
				this._beginTime = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "endTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int endTime
		{
			get
			{
				return this._endTime;
			}
			set
			{
				this._endTime = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "curData", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int curData
		{
			get
			{
				return this._curData;
			}
			set
			{
				this._curData = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
