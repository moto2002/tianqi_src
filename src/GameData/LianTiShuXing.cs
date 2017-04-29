using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "LianTiShuXing")]
	[Serializable]
	public class LianTiShuXing : IExtensible
	{
		private int _id;

		private int _career;

		private int _stage;

		private int _points;

		private int _notActivateIcons;

		private int _activateIcons;

		private readonly List<int> _coordinate = new List<int>();

		private int _attributeTemplateID;

		private int _peopleAbility;

		private int _word;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(2, IsRequired = false, Name = "career", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int career
		{
			get
			{
				return this._career;
			}
			set
			{
				this._career = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "stage", DataFormat = DataFormat.TwosComplement)]
		public int stage
		{
			get
			{
				return this._stage;
			}
			set
			{
				this._stage = value;
			}
		}

		[ProtoMember(4, IsRequired = true, Name = "points", DataFormat = DataFormat.TwosComplement)]
		public int points
		{
			get
			{
				return this._points;
			}
			set
			{
				this._points = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "notActivateIcons", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int notActivateIcons
		{
			get
			{
				return this._notActivateIcons;
			}
			set
			{
				this._notActivateIcons = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "activateIcons", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int activateIcons
		{
			get
			{
				return this._activateIcons;
			}
			set
			{
				this._activateIcons = value;
			}
		}

		[ProtoMember(7, Name = "coordinate", DataFormat = DataFormat.TwosComplement)]
		public List<int> coordinate
		{
			get
			{
				return this._coordinate;
			}
		}

		[ProtoMember(8, IsRequired = true, Name = "attributeTemplateID", DataFormat = DataFormat.TwosComplement)]
		public int attributeTemplateID
		{
			get
			{
				return this._attributeTemplateID;
			}
			set
			{
				this._attributeTemplateID = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "peopleAbility", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int peopleAbility
		{
			get
			{
				return this._peopleAbility;
			}
			set
			{
				this._peopleAbility = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "word", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int word
		{
			get
			{
				return this._word;
			}
			set
			{
				this._word = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
