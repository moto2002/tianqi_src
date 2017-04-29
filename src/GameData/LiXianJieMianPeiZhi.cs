using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "LiXianJieMianPeiZhi")]
	[Serializable]
	public class LiXianJieMianPeiZhi : IExtensible
	{
		private int _panel;

		private int _panelname;

		private int _describeId;

		private int _describeId2;

		private int _describeId3;

		private int _describeId4;

		private int _describeId5;

		private int _describeId6;

		private int _timeLimit;

		private int _addLimit;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = false, Name = "panel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int panel
		{
			get
			{
				return this._panel;
			}
			set
			{
				this._panel = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "panelname", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int panelname
		{
			get
			{
				return this._panelname;
			}
			set
			{
				this._panelname = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "describeId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int describeId
		{
			get
			{
				return this._describeId;
			}
			set
			{
				this._describeId = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "describeId2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int describeId2
		{
			get
			{
				return this._describeId2;
			}
			set
			{
				this._describeId2 = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "describeId3", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int describeId3
		{
			get
			{
				return this._describeId3;
			}
			set
			{
				this._describeId3 = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "describeId4", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int describeId4
		{
			get
			{
				return this._describeId4;
			}
			set
			{
				this._describeId4 = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "describeId5", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int describeId5
		{
			get
			{
				return this._describeId5;
			}
			set
			{
				this._describeId5 = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "describeId6", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int describeId6
		{
			get
			{
				return this._describeId6;
			}
			set
			{
				this._describeId6 = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "timeLimit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int timeLimit
		{
			get
			{
				return this._timeLimit;
			}
			set
			{
				this._timeLimit = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "addLimit", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int addLimit
		{
			get
			{
				return this._addLimit;
			}
			set
			{
				this._addLimit = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
