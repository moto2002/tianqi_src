using ProtoBuf;
using System;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "MoGuBiao")]
	[Serializable]
	public class MoGuBiao : IExtensible
	{
		private int _id;

		private int _nameID;

		private int _animation1;

		private int _animation2;

		private int _animation3;

		private int _animation4;

		private int _animation5;

		private int _num;

		private int _result;

		private int _lifeTime;

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "id", DataFormat = DataFormat.TwosComplement)]
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

		[ProtoMember(3, IsRequired = false, Name = "nameID", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int nameID
		{
			get
			{
				return this._nameID;
			}
			set
			{
				this._nameID = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "animation1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int animation1
		{
			get
			{
				return this._animation1;
			}
			set
			{
				this._animation1 = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "animation2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int animation2
		{
			get
			{
				return this._animation2;
			}
			set
			{
				this._animation2 = value;
			}
		}

		[ProtoMember(7, IsRequired = false, Name = "animation3", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int animation3
		{
			get
			{
				return this._animation3;
			}
			set
			{
				this._animation3 = value;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "animation4", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int animation4
		{
			get
			{
				return this._animation4;
			}
			set
			{
				this._animation4 = value;
			}
		}

		[ProtoMember(9, IsRequired = false, Name = "animation5", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int animation5
		{
			get
			{
				return this._animation5;
			}
			set
			{
				this._animation5 = value;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "num", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int num
		{
			get
			{
				return this._num;
			}
			set
			{
				this._num = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "result", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int result
		{
			get
			{
				return this._result;
			}
			set
			{
				this._result = value;
			}
		}

		[ProtoMember(12, IsRequired = false, Name = "lifeTime", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int lifeTime
		{
			get
			{
				return this._lifeTime;
			}
			set
			{
				this._lifeTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
