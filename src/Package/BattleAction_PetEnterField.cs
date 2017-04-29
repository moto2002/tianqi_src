using ProtoBuf;
using System;

namespace Package
{
	[ProtoContract(Name = "BattleAction_PetEnterField")]
	[Serializable]
	public class BattleAction_PetEnterField : IExtensible
	{
		private long _petId;

		private Pos _pos;

		private Vector2 _vector;

		private int _exitTime;

		private IExtension extensionObject;

		[ProtoMember(1, IsRequired = true, Name = "petId", DataFormat = DataFormat.TwosComplement)]
		public long petId
		{
			get
			{
				return this._petId;
			}
			set
			{
				this._petId = value;
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

		[ProtoMember(4, IsRequired = true, Name = "exitTime", DataFormat = DataFormat.TwosComplement)]
		public int exitTime
		{
			get
			{
				return this._exitTime;
			}
			set
			{
				this._exitTime = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
