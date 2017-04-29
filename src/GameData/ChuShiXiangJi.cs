using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameData
{
	[ProtoContract(Name = "ChuShiXiangJi")]
	[Serializable]
	public class ChuShiXiangJi : IExtensible
	{
		private int _instanceId;

		private readonly List<float> _posA = new List<float>();

		private readonly List<float> _cameraA = new List<float>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "instanceId", DataFormat = DataFormat.TwosComplement)]
		public int instanceId
		{
			get
			{
				return this._instanceId;
			}
			set
			{
				this._instanceId = value;
			}
		}

		[ProtoMember(3, Name = "posA", DataFormat = DataFormat.FixedSize)]
		public List<float> posA
		{
			get
			{
				return this._posA;
			}
		}

		[ProtoMember(4, Name = "cameraA", DataFormat = DataFormat.FixedSize)]
		public List<float> cameraA
		{
			get
			{
				return this._cameraA;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
