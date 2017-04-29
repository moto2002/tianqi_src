using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameData
{
	[ProtoContract(Name = "ChuanSongMenNPC")]
	[Serializable]
	public class ChuanSongMenNPC : IExtensible
	{
		private int _id;

		private int _model;

		private int _scene;

		private readonly List<int> _position = new List<int>();

		private readonly List<int> _face = new List<int>();

		private readonly List<int> _triggeredRange = new List<int>();

		private int _targetScene;

		private readonly List<int> _transferBornPoint = new List<int>();

		private int _mainSceneBornArea;

		private int _frontMainTask;

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

		[ProtoMember(3, IsRequired = false, Name = "model", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int model
		{
			get
			{
				return this._model;
			}
			set
			{
				this._model = value;
			}
		}

		[ProtoMember(4, IsRequired = false, Name = "scene", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int scene
		{
			get
			{
				return this._scene;
			}
			set
			{
				this._scene = value;
			}
		}

		[ProtoMember(5, Name = "position", DataFormat = DataFormat.TwosComplement)]
		public List<int> position
		{
			get
			{
				return this._position;
			}
		}

		[ProtoMember(6, Name = "face", DataFormat = DataFormat.TwosComplement)]
		public List<int> face
		{
			get
			{
				return this._face;
			}
		}

		[ProtoMember(7, Name = "triggeredRange", DataFormat = DataFormat.TwosComplement)]
		public List<int> triggeredRange
		{
			get
			{
				return this._triggeredRange;
			}
		}

		[ProtoMember(8, IsRequired = false, Name = "targetScene", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int targetScene
		{
			get
			{
				return this._targetScene;
			}
			set
			{
				this._targetScene = value;
			}
		}

		[ProtoMember(9, Name = "transferBornPoint", DataFormat = DataFormat.TwosComplement)]
		public List<int> transferBornPoint
		{
			get
			{
				return this._transferBornPoint;
			}
		}

		[ProtoMember(10, IsRequired = false, Name = "mainSceneBornArea", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int mainSceneBornArea
		{
			get
			{
				return this._mainSceneBornArea;
			}
			set
			{
				this._mainSceneBornArea = value;
			}
		}

		[ProtoMember(11, IsRequired = false, Name = "frontMainTask", DataFormat = DataFormat.TwosComplement), DefaultValue(0)]
		public int frontMainTask
		{
			get
			{
				return this._frontMainTask;
			}
			set
			{
				this._frontMainTask = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
		}
	}
}
