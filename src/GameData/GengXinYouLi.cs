using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "GengXinYouLi")]
	[Serializable]
	public class GengXinYouLi : IExtensible
	{
		private int _Id;

		private int _OpenLevel;

		private int _HintLevel;

		private int _FinishPar;

		private int _DropId;

		private readonly List<int> _ItemId = new List<int>();

		private readonly List<int> _ItemNum = new List<int>();

		private IExtension extensionObject;

		[ProtoMember(2, IsRequired = true, Name = "Id", DataFormat = DataFormat.TwosComplement)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				this._Id = value;
			}
		}

		[ProtoMember(3, IsRequired = false, Name = "OpenLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int OpenLevel
		{
			get
			{
				return this._OpenLevel;
			}
			set
			{
				this._OpenLevel = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "HintLevel", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int HintLevel
		{
			get
			{
				return this._HintLevel;
			}
			set
			{
				this._HintLevel = value;
			}
		}

		[ProtoMember(5, IsRequired = false, Name = "FinishPar", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int FinishPar
		{
			get
			{
				return this._FinishPar;
			}
			set
			{
				this._FinishPar = value;
			}
		}

		[ProtoMember(6, IsRequired = false, Name = "DropId", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int DropId
		{
			get
			{
				return this._DropId;
			}
			set
			{
				this._DropId = value;
			}
		}

		[ProtoMember(7, Name = "ItemId", DataFormat = DataFormat.TwosComplement)]
		public List<int> ItemId
		{
			get
			{
				return this._ItemId;
			}
		}

		[ProtoMember(8, Name = "ItemNum", DataFormat = DataFormat.TwosComplement)]
		public List<int> ItemNum
		{
			get
			{
				return this._ItemNum;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
