using GameData;
using System;
using System.Collections.Generic;
using System.Text;

public class ExteriorArithmeticUnit
{
	protected bool isAutoArithmetic = true;

	protected bool isAutoUpdateExterior;

	protected int finalModelID;

	protected int finalWeaponID;

	protected int finalWeaponGogok;

	protected int finalClothesID;

	protected int finalWingID;

	protected int oldFinalModelID;

	protected int oldFinalWeaponID;

	protected int oldFinalWeaponGogok;

	protected int oldFinalClothesID;

	protected int oldFinalWingID;

	protected Action<Action> UpdateModelAction;

	protected Action UpdateWeaponAction;

	protected Action UpdateClothesAction;

	protected Action UpdateWingAction;

	protected int standardModelID;

	protected List<int> equipIDs = new List<int>();

	protected int gogok;

	protected int wingID;

	protected bool isHideWing = true;

	protected int clientModelID;

	protected int serverModelID;

	protected List<string> fashionIDs = new List<string>();

	protected int fashionModelID;

	protected List<int> fashionEquipIDs = new List<int>();

	protected int fashionWingID;

	public bool IsAutoArithmetic
	{
		get
		{
			return this.isAutoArithmetic;
		}
		set
		{
			bool flag = this.isAutoArithmetic;
			this.isAutoArithmetic = value;
			if (!flag && value)
			{
				this.Arithmetic();
			}
		}
	}

	public bool IsAutoUpdateExterior
	{
		get
		{
			return this.isAutoUpdateExterior;
		}
		set
		{
			bool flag = this.isAutoUpdateExterior;
			this.isAutoUpdateExterior = value;
			if (!flag && value)
			{
				this.Arithmetic();
			}
		}
	}

	public int FinalModelID
	{
		get
		{
			return this.finalModelID;
		}
		set
		{
			this.finalModelID = value;
		}
	}

	public int FinalWeaponID
	{
		get
		{
			return this.finalWeaponID;
		}
		set
		{
			this.finalWeaponID = value;
		}
	}

	public int FinalWeaponGogok
	{
		get
		{
			return this.finalWeaponGogok;
		}
		set
		{
			this.finalWeaponGogok = value;
		}
	}

	public int FinalClothesID
	{
		get
		{
			return this.finalClothesID;
		}
		set
		{
			this.finalClothesID = value;
		}
	}

	public int FinalWingID
	{
		get
		{
			return this.finalWingID;
		}
		set
		{
			this.finalWingID = value;
		}
	}

	public int OldFinalModelID
	{
		get
		{
			return this.oldFinalModelID;
		}
		set
		{
			this.oldFinalModelID = value;
		}
	}

	public int OldFinalWeaponID
	{
		get
		{
			return this.oldFinalWeaponID;
		}
		set
		{
			this.oldFinalWeaponID = value;
		}
	}

	public int OldFinalWeaponGogok
	{
		get
		{
			return this.oldFinalWeaponGogok;
		}
		set
		{
			this.oldFinalWeaponGogok = value;
		}
	}

	public int OldFinalClothesID
	{
		get
		{
			return this.oldFinalClothesID;
		}
		set
		{
			this.oldFinalClothesID = value;
		}
	}

	public int OldFinalWingID
	{
		get
		{
			return this.oldFinalWingID;
		}
		set
		{
			this.oldFinalWingID = value;
		}
	}

	public int StandardModelID
	{
		get
		{
			return this.standardModelID;
		}
		set
		{
			bool flag = this.standardModelID == value;
			this.standardModelID = value;
			if (!flag)
			{
				this.AutoArithmetic();
			}
		}
	}

	public List<int> EquipIDs
	{
		get
		{
			return this.equipIDs;
		}
		set
		{
			bool flag = value != null && this.equipIDs.get_Count() == value.get_Count();
			if (flag)
			{
				for (int i = 0; i < this.equipIDs.get_Count(); i++)
				{
					if (this.equipIDs.get_Item(i) != value.get_Item(i))
					{
						flag = false;
						break;
					}
				}
			}
			this.equipIDs.Clear();
			if (value != null && value.get_Count() > 0)
			{
				this.equipIDs.AddRange(value);
			}
			if (!flag)
			{
				this.AutoArithmetic();
			}
		}
	}

	public int Gogok
	{
		get
		{
			return this.gogok;
		}
		set
		{
			bool flag = this.gogok == value;
			this.gogok = value;
			if (!flag)
			{
				this.AutoArithmetic();
			}
		}
	}

	public int WingID
	{
		get
		{
			return this.wingID;
		}
		set
		{
			bool flag = this.wingID == value;
			this.wingID = value;
			if (!flag)
			{
				this.AutoArithmetic();
			}
		}
	}

	public bool IsHideWing
	{
		get
		{
			return this.isHideWing;
		}
		set
		{
			bool flag = this.isHideWing == value;
			this.isHideWing = value;
			if (!flag)
			{
				this.AutoArithmetic();
			}
		}
	}

	public int ClientModelID
	{
		get
		{
			return this.clientModelID;
		}
		set
		{
			bool flag = this.clientModelID == value;
			this.clientModelID = value;
			if (!flag)
			{
				this.AutoArithmetic();
			}
		}
	}

	public int ServerModelID
	{
		get
		{
			return this.serverModelID;
		}
		set
		{
			bool flag = this.serverModelID == value;
			this.serverModelID = value;
			if (!flag)
			{
				this.AutoArithmetic();
			}
		}
	}

	public List<string> FashionIDs
	{
		get
		{
			return this.fashionIDs;
		}
		set
		{
			bool flag = value != null && this.fashionIDs.get_Count() == value.get_Count();
			if (flag)
			{
				for (int i = 0; i < this.fashionIDs.get_Count(); i++)
				{
					if (this.fashionIDs.get_Item(i) != value.get_Item(i))
					{
						flag = false;
						break;
					}
				}
			}
			this.fashionIDs.Clear();
			if (value != null && value.get_Count() > 0)
			{
				this.fashionIDs.AddRange(value);
			}
			if (!flag)
			{
				this.DecomposeFashion();
				this.AutoArithmetic();
			}
		}
	}

	public int FashionModelID
	{
		get
		{
			return this.fashionModelID;
		}
		set
		{
			this.fashionModelID = value;
		}
	}

	public List<int> FashionEquipIDs
	{
		get
		{
			return this.fashionEquipIDs;
		}
		set
		{
			this.fashionEquipIDs.Clear();
			if (value != null && value.get_Count() > 0)
			{
				this.fashionEquipIDs.AddRange(value);
			}
		}
	}

	public int FashionWingID
	{
		get
		{
			return this.fashionWingID;
		}
		set
		{
			this.fashionWingID = value;
		}
	}

	protected ExteriorArithmeticUnit()
	{
	}

	public ExteriorArithmeticUnit(Action<Action> theUpdateModelAction = null, Action theUpdateWeaponAction = null, Action theUpdateClothesAction = null, Action theUpdateWingAction = null)
	{
		this.UpdateModelAction = theUpdateModelAction;
		this.UpdateWeaponAction = theUpdateWeaponAction;
		this.UpdateClothesAction = theUpdateClothesAction;
		this.UpdateWingAction = theUpdateWingAction;
	}

	public void Clone(ExteriorArithmeticUnit origin, bool isArithmetic = true)
	{
		this.Reset();
		this.finalModelID = origin.FinalModelID;
		this.finalWeaponID = origin.FinalWeaponID;
		this.finalWeaponGogok = origin.FinalWeaponGogok;
		this.finalClothesID = origin.FinalClothesID;
		this.finalWingID = origin.FinalWingID;
		this.standardModelID = origin.StandardModelID;
		this.equipIDs.Clear();
		this.equipIDs.AddRange(origin.EquipIDs);
		this.gogok = origin.Gogok;
		this.wingID = origin.WingID;
		this.isHideWing = origin.IsHideWing;
		this.clientModelID = origin.ClientModelID;
		this.serverModelID = origin.ServerModelID;
		this.fashionIDs.Clear();
		this.fashionIDs.AddRange(origin.FashionIDs);
		this.fashionModelID = origin.FashionModelID;
		this.fashionEquipIDs.Clear();
		this.fashionEquipIDs.AddRange(origin.FashionEquipIDs);
		this.fashionWingID = origin.FashionWingID;
		if (isArithmetic)
		{
			this.Arithmetic();
		}
	}

	public void Reset()
	{
		this.OldFinalModelID = 0;
		this.OldFinalWeaponID = 0;
		this.OldFinalWeaponGogok = 0;
		this.OldFinalClothesID = 0;
		this.OldFinalWingID = 0;
	}

	protected void AutoArithmetic()
	{
		if (!this.IsAutoArithmetic)
		{
			return;
		}
		this.Arithmetic();
	}

	protected void Arithmetic()
	{
		bool flag = false;
		int num = 0;
		bool flag2 = false;
		int num2 = 0;
		this.FinalModelID = this.StandardModelID;
		this.FinalWeaponID = 0;
		this.FinalWeaponGogok = 0;
		this.FinalClothesID = 0;
		this.FinalWingID = this.WingID;
		ExteriorArithmeticUnit.AnalysisEquipList(this.EquipIDs, out flag, out num, out flag2, out num2);
		if (flag)
		{
			this.FinalWeaponID = num;
			this.FinalWeaponGogok = this.Gogok;
		}
		if (flag2)
		{
			this.FinalModelID = this.StandardModelID;
			this.FinalClothesID = num2;
		}
		if (this.ServerModelID != 0)
		{
			this.FinalModelID = this.ServerModelID;
			this.FinalClothesID = 0;
		}
		if (this.FashionWingID != 0)
		{
			this.FinalWingID = this.FashionWingID;
		}
		ExteriorArithmeticUnit.AnalysisEquipList(this.FashionEquipIDs, out flag, out num, out flag2, out num2);
		if (flag)
		{
			this.FinalWeaponID = num;
			this.FinalWeaponGogok = 0;
		}
		if (flag2)
		{
			this.FinalModelID = this.StandardModelID;
			this.FinalClothesID = num2;
		}
		if (this.FashionModelID != 0)
		{
			this.FinalModelID = this.FashionModelID;
			this.FinalClothesID = 0;
		}
		if (this.ClientModelID != 0)
		{
			this.FinalModelID = this.ClientModelID;
			this.FinalWeaponID = 0;
			this.FinalWeaponGogok = 0;
			this.FinalClothesID = 0;
			this.FinalWingID = 0;
		}
		if (this.IsHideWing)
		{
			this.FinalWingID = 0;
		}
		this.AutoUpdateExterior();
	}

	protected void AutoUpdateExterior()
	{
		if (!this.IsAutoUpdateExterior)
		{
			return;
		}
		this.UpdateExterior();
	}

	protected void UpdateExterior()
	{
		if (this.OldFinalModelID == this.FinalModelID)
		{
			if ((this.OldFinalWeaponID != this.FinalWeaponID || this.OldFinalWeaponGogok != this.FinalWeaponGogok) && this.UpdateWeaponAction != null)
			{
				this.OldFinalWeaponID = this.FinalWeaponID;
				this.OldFinalWeaponGogok = this.FinalWeaponGogok;
				this.UpdateWeaponAction.Invoke();
			}
			if (this.OldFinalClothesID != this.FinalClothesID && this.UpdateClothesAction != null)
			{
				this.OldFinalClothesID = this.FinalClothesID;
				this.UpdateClothesAction.Invoke();
			}
			if (this.OldFinalWingID != this.FinalWingID && this.UpdateWingAction != null)
			{
				this.OldFinalWingID = this.FinalWingID;
				this.UpdateWingAction.Invoke();
			}
		}
		else
		{
			this.OldFinalModelID = this.FinalModelID;
			this.OldFinalWeaponID = this.FinalWeaponID;
			this.OldFinalWeaponGogok = this.FinalWeaponGogok;
			this.OldFinalClothesID = this.FinalClothesID;
			this.OldFinalWingID = this.FinalWingID;
			if (this.UpdateModelAction != null)
			{
				this.UpdateModelAction.Invoke(delegate
				{
					if (this.UpdateWeaponAction != null)
					{
						this.UpdateWeaponAction.Invoke();
					}
					if (this.UpdateClothesAction != null)
					{
						this.UpdateClothesAction.Invoke();
					}
					if (this.UpdateWingAction != null)
					{
						this.UpdateWingAction.Invoke();
					}
				});
			}
		}
	}

	public void WrapSetData(Action action)
	{
		this.IsAutoArithmetic = false;
		if (action != null)
		{
			action.Invoke();
		}
		this.IsAutoArithmetic = true;
	}

	public void WrapSetDataAndForceUpdate(Action action)
	{
		this.IsAutoArithmetic = false;
		this.IsAutoUpdateExterior = false;
		if (action != null)
		{
			action.Invoke();
		}
		this.IsAutoArithmetic = true;
		this.IsAutoUpdateExterior = true;
	}

	public void SetType(int typeID)
	{
		if (DataReader<RoleCreate>.Contains(typeID))
		{
			this.StandardModelID = DataReader<RoleCreate>.Get(typeID).modle;
		}
	}

	protected void DecomposeFashion()
	{
		this.FashionModelID = 0;
		this.FashionEquipIDs = null;
		this.FashionWingID = 0;
		for (int i = 0; i < this.fashionIDs.get_Count(); i++)
		{
			FashionModelType fashionModelType = FashionModelType.None;
			int fashionModelTypeID = ExteriorArithmeticUnit.GetFashionModelTypeID(this.fashionIDs.get_Item(i), out fashionModelType);
			if (fashionModelTypeID != 0)
			{
				switch (fashionModelType)
				{
				case FashionModelType.Equip:
					this.FashionEquipIDs.Add(fashionModelTypeID);
					break;
				case FashionModelType.Wing:
					this.FashionWingID = fashionModelTypeID;
					break;
				case FashionModelType.Model:
					this.FashionModelID = fashionModelTypeID;
					break;
				}
			}
		}
	}

	public void GetInfoString(StringBuilder result)
	{
		result.Append("ExteriorUnit:{");
		result.Append("FinalModelID:");
		result.Append(this.FinalModelID);
		result.Append("  ");
		result.Append("FinalWeaponID:");
		result.Append(this.FinalWeaponID);
		result.Append("  ");
		result.Append("FinalClothesID:");
		result.Append(this.FinalClothesID);
		result.Append("  ");
		result.Append("FinalWingID:");
		result.Append(this.FinalWingID);
		result.Append("  ");
		result.Append("OldFinalModelID:");
		result.Append(this.OldFinalModelID);
		result.Append("  ");
		result.Append("OldFinalWeaponID:");
		result.Append(this.OldFinalWeaponID);
		result.Append("  ");
		result.Append("OldFinalClothesID:");
		result.Append(this.OldFinalClothesID);
		result.Append("  ");
		result.Append("OldFinalWingID:");
		result.Append(this.OldFinalWingID);
		result.Append("  ");
		result.Append("StandardModelID:");
		result.Append(this.StandardModelID);
		result.Append("  ");
		result.Append("EquipIDs:{");
		for (int i = 0; i < this.equipIDs.get_Count(); i++)
		{
			result.Append(this.equipIDs.get_Item(i));
			result.Append(",");
		}
		result.Append("}  ");
		result.Append("WingID:");
		result.Append(this.WingID);
		result.Append("  ");
		result.Append("IsHideWing:");
		result.Append(this.IsHideWing);
		result.Append("  ");
		result.Append("ClientModelID:");
		result.Append(this.ClientModelID);
		result.Append("  ");
		result.Append("ServerModelID:");
		result.Append(this.ServerModelID);
		result.Append("  ");
		result.Append("FashionIDs:{");
		for (int j = 0; j < this.fashionIDs.get_Count(); j++)
		{
			result.Append(this.fashionIDs.get_Item(j));
			result.Append(",");
		}
		result.Append("}  ");
		result.Append("FashionModelID:");
		result.Append(this.FashionModelID);
		result.Append("  ");
		result.Append("FashionEquipIDs:{");
		for (int k = 0; k < this.fashionEquipIDs.get_Count(); k++)
		{
			result.Append(this.fashionEquipIDs.get_Item(k));
			result.Append(",");
		}
		result.Append("}  ");
		result.Append("FashionWingID:");
		result.Append(this.FashionWingID);
		result.Append("  ");
		result.Append("}  ");
	}

	public static int GetFashionModelTypeID(string id, out FashionModelType type)
	{
		if (!DataReader<ShiZhuangXiTong>.Contains(id))
		{
			type = FashionModelType.None;
			return 0;
		}
		ShiZhuangXiTong shiZhuangXiTong = DataReader<ShiZhuangXiTong>.Get(id);
		switch (shiZhuangXiTong.kind)
		{
		case 1:
		case 2:
			if (shiZhuangXiTong.IsModelChange == 0)
			{
				type = FashionModelType.Equip;
				return shiZhuangXiTong.itemsID;
			}
			type = FashionModelType.Model;
			return shiZhuangXiTong.model;
		case 3:
			type = FashionModelType.Wing;
			return shiZhuangXiTong.model;
		default:
			type = FashionModelType.None;
			return 0;
		}
	}

	public static void AnalysisEquipList(List<int> equipList, out bool isContainWeapon, out int weaponID, out bool isContainClothes, out int clothesID)
	{
		isContainWeapon = false;
		isContainClothes = false;
		weaponID = 0;
		clothesID = 0;
		if (equipList == null)
		{
			return;
		}
		if (equipList.get_Count() == 0)
		{
			return;
		}
		for (int i = 0; i < equipList.get_Count(); i++)
		{
			if (DataReader<zZhuangBeiPeiZhiBiao>.Contains(equipList.get_Item(i)))
			{
				zZhuangBeiPeiZhiBiao zZhuangBeiPeiZhiBiao = DataReader<zZhuangBeiPeiZhiBiao>.Get(equipList.get_Item(i));
				if (DataReader<EquipBody>.Contains(zZhuangBeiPeiZhiBiao.model))
				{
					EquipBody equipBody = DataReader<EquipBody>.Get(zZhuangBeiPeiZhiBiao.model);
					int putOnMethod = equipBody.putOnMethod;
					if (putOnMethod != 1)
					{
						if (putOnMethod == 2)
						{
							isContainClothes = true;
							clothesID = equipList.get_Item(i);
						}
					}
					else
					{
						isContainWeapon = true;
						weaponID = equipList.get_Item(i);
					}
				}
			}
		}
	}
}
