using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "XiTongKaiQiYuGao")]
	[Serializable]
	public class XiTongKaiQiYuGao : IExtensible
	{
		private int _priority;

		private int _artifactId;

		private int _openType;

		private int _startLevel;

		private int _start;

		private int _ending;

		private int _icon;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "priority", DataFormat = DataFormat.TwosComplement)]
		public int priority
		{
			get
			{
				return this._priority;
			}
			set
			{
				this._priority = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "artifactId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int artifactId
		{
			get
			{
				return this._artifactId;
			}
			set
			{
				this._artifactId = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "openType", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int openType
		{
			get
			{
				return this._openType;
			}
			set
			{
				this._openType = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "startLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int startLevel
		{
			get
			{
				return this._startLevel;
			}
			set
			{
				this._startLevel = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "start", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int start
		{
			get
			{
				return this._start;
			}
			set
			{
				this._start = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "ending", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int ending
		{
			get
			{
				return this._ending;
			}
			set
			{
				this._ending = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "icon", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				this._icon = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
