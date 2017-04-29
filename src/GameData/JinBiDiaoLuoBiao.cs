using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "JinBiDiaoLuoBiao")]
	[Serializable]
	public class JinBiDiaoLuoBiao : IExtensible
	{
		private int _flopId;

		private int _modelId1;

		private readonly List<int> _num1 = new List<int>();

		private int _modelId2;

		private readonly List<int> _num2 = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "flopId", DataFormat = DataFormat.TwosComplement)]
		public int flopId
		{
			get
			{
				return this._flopId;
			}
			set
			{
				this._flopId = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "modelId1", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int modelId1
		{
			get
			{
				return this._modelId1;
			}
			set
			{
				this._modelId1 = value;
			}
		}

		[ProtoMember(4, Name = "num1", DataFormat = DataFormat.TwosComplement)]
		public List<int> num1
		{
			get
			{
				return this._num1;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "modelId2", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int modelId2
		{
			get
			{
				return this._modelId2;
			}
			set
			{
				this._modelId2 = value;
			}
		}

		[ProtoMember(6, Name = "num2", DataFormat = DataFormat.TwosComplement)]
		public List<int> num2
		{
			get
			{
				return this._num2;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
