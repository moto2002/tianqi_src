using NetWork.Utilities;
using ProtoBuf;
using System;
using System.ComponentModel;

namespace Package
{
	[ForRecv(688), ForSend(688), ProtoContract(Name = "EndSkillManageReq")]
	[Serializable]
	public class EndSkillManageReq : IExtensible
	{
		public static readonly short OP = 688;

		private long _managedId;

		private Pos _pos;

		private Vector2 _vector;

		private int _mgrSn;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "managedId", DataFormat = DataFormat.TwosComplement)]
		public long managedId
		{
			get
			{
				return this._managedId;
			}
			set
			{
				this._managedId = value;
			}
		}

		[ProtoMember(2, IsRequired = true, Name = "pos", DataFormat = DataFormat.Default)]
		public Pos pos
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
			}
		}

		[ProtoMember(3, IsRequired = true, Name = "vector", DataFormat = DataFormat.Default)]
		public Vector2 vector
		{
			get
			{
				return this._vector;
			}
			set
			{
				this._vector = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "mgrSn", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mgrSn
		{
			get
			{
				return this._mgrSn;
			}
			set
			{
				this._mgrSn = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
