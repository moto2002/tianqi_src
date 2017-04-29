using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[CfgIndices(new string[]
	{
		"ChapterId"
	}), ProtoContract(Name = "DarkChapter")]
	[Serializable]
	public class DarkChapter : IExtensible
	{
		private int _ChapterId;

		private readonly List<int> _MissionGroup = new List<int>();

		private int _Name;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "ChapterId", DataFormat = DataFormat.TwosComplement)]
		public int ChapterId
		{
			get
			{
				return this._ChapterId;
			}
			set
			{
				this._ChapterId = value;
			}
		}

		[ProtoMember(3, Name = "MissionGroup", DataFormat = DataFormat.TwosComplement)]
		public List<int> MissionGroup
		{
			get
			{
				return this._MissionGroup;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "Name", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
