using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "XBuWeiShuXingCanShu")]
	[Serializable]
	public class XBuWeiShuXingCanShu : IExtensible
	{
		private int _job;

		private int _part;

		private readonly List<int> _attrid = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(3, IsRequired = false, Name = "job", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int job
		{
			get
			{
				return this._job;
			}
			set
			{
				this._job = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "part", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int part
		{
			get
			{
				return this._part;
			}
			set
			{
				this._part = value;
			}
		}

		[ProtoMember(5, Name = "attrid", DataFormat = DataFormat.TwosComplement)]
		public List<int> attrid
		{
			get
			{
				return this._attrid;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
