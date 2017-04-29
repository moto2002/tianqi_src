using Package;
using System;

public class SceneLoadedUIManager
{
	public bool IsFromBattleClick;

	private static SceneLoadedUIManager instance;

	public static SceneLoadedUIManager Instance
	{
		get
		{
			if (SceneLoadedUIManager.instance == null)
			{
				SceneLoadedUIManager.instance = new SceneLoadedUIManager();
			}
			return SceneLoadedUIManager.instance;
		}
	}

	public void Init()
	{
		EventDispatcher.AddListener("SHOW_TOWN_UI", new Callback(this.ShowTownUI));
		EventDispatcher.AddListener("SHOW_PREVIOUS_UI", new Callback(this.ShowPreUI));
		EventDispatcher.AddListener("SHOW_EQUIPQUALITY", new Callback(this.ShowEquipQua));
		EventDispatcher.AddListener("SHOW_EQUIPLEVEL", new Callback(this.ShowEquipLv));
		EventDispatcher.AddListener("SHOW_GEMLEVEL", new Callback(this.ShowEquipGem));
		EventDispatcher.AddListener("SHOW_SKILL", new Callback(this.ShowSkill));
		EventDispatcher.AddListener("SHOW_PETLEVEL", new Callback(this.ShowPetLv));
		EventDispatcher.AddListener("SHOW_PETSTRA", new Callback(this.ShowPetStar));
		EventDispatcher.AddListener("SHOW_PETSKILL", new Callback(this.ShowPetSkill));
		EventDispatcher.AddListener("SHOW_CHANGE_CAREER_UI", new Callback(this.ShowChangeCareerUI));
		EventDispatcher.AddListener("SHOW_GODSOLDIER", new Callback(this.ShowGodSoldier));
		EventDispatcher.AddListener("SHOW_WING", new Callback(this.ShowWing));
	}

	public void Release()
	{
		EventDispatcher.RemoveListener("SHOW_TOWN_UI", new Callback(this.ShowTownUI));
		EventDispatcher.RemoveListener("SHOW_PREVIOUS_UI", new Callback(this.ShowPreUI));
		EventDispatcher.RemoveListener("SHOW_EQUIPQUALITY", new Callback(this.ShowEquipQua));
		EventDispatcher.RemoveListener("SHOW_EQUIPLEVEL", new Callback(this.ShowEquipLv));
		EventDispatcher.RemoveListener("SHOW_GEMLEVEL", new Callback(this.ShowEquipGem));
		EventDispatcher.RemoveListener("SHOW_SKILL", new Callback(this.ShowSkill));
		EventDispatcher.RemoveListener("SHOW_PETLEVEL", new Callback(this.ShowPetLv));
		EventDispatcher.RemoveListener("SHOW_PETSTRA", new Callback(this.ShowPetStar));
		EventDispatcher.RemoveListener("SHOW_PETSKILL", new Callback(this.ShowPetSkill));
		EventDispatcher.RemoveListener("SHOW_CHANGE_CAREER_UI", new Callback(this.ShowChangeCareerUI));
		EventDispatcher.RemoveListener("SHOW_GODSOLDIER", new Callback(this.ShowGodSoldier));
		EventDispatcher.RemoveListener("SHOW_WING", new Callback(this.ShowWing));
	}

	private void ShowTownUI()
	{
		UIStackManager.Instance.RemoveStack("BattleUI");
		UIStackManager.Instance.PopTownUI();
	}

	private void ShowPreUI()
	{
		UIStackManager.Instance.RemoveStack("BattleUI");
		UIStackManager.Instance.PopUILast_FullScreen();
	}

	private void ShowEquipQua()
	{
		UIStackManager.Instance.ClearPush();
		UIStackManager.Instance.PushUI("TownUI", UIType.FullScreen, UINodesManager.NormalUIRoot);
		LinkNavigationManager.OpenEquipStarUpUI(EquipLibType.ELT.Weapon, null);
	}

	private void ShowEquipLv()
	{
		UIStackManager.Instance.ClearPush();
		UIStackManager.Instance.PushUI("TownUI", UIType.FullScreen, UINodesManager.NormalUIRoot);
		LinkNavigationManager.OpenEquipStrengthenUI(EquipLibType.ELT.Weapon, null);
	}

	private void ShowEquipGem()
	{
		UIStackManager.Instance.ClearPush();
		UIStackManager.Instance.PushUI("TownUI", UIType.FullScreen, UINodesManager.NormalUIRoot);
		LinkNavigationManager.OpenEquipGemUI(EquipLibType.ELT.Weapon, null);
	}

	private void ShowSkill()
	{
		UIStackManager.Instance.ClearPush();
		UIStackManager.Instance.PushUI("TownUI", UIType.FullScreen, UINodesManager.NormalUIRoot);
		LinkNavigationManager.OpenSkillUI(null);
	}

	private void ShowPetLv()
	{
		UIStackManager.Instance.ClearPush();
		UIStackManager.Instance.PushUI("TownUI", UIType.FullScreen, UINodesManager.NormalUIRoot);
		LinkNavigationManager.OpenPetLevelUI();
	}

	private void ShowPetStar()
	{
		UIStackManager.Instance.ClearPush();
		LinkNavigationManager.OpenTownUI();
		LinkNavigationManager.OpenPetStarUI();
	}

	private void ShowPetSkill()
	{
		UIStackManager.Instance.ClearPush();
		UIStackManager.Instance.PushUI("TownUI", UIType.FullScreen, UINodesManager.NormalUIRoot);
		LinkNavigationManager.OpenPetSkillUI();
	}

	private void ShowGodSoldier()
	{
		UIStackManager.Instance.ClearPush();
		UIStackManager.Instance.PushUI("TownUI", UIType.FullScreen, UINodesManager.NormalUIRoot);
		LinkNavigationManager.OpenGodSoldierUI();
	}

	private void ShowWing()
	{
		UIStackManager.Instance.ClearPush();
		UIStackManager.Instance.PushUI("TownUI", UIType.FullScreen, UINodesManager.NormalUIRoot);
		LinkNavigationManager.OpenActorUI(null);
	}

	public void ShowChangeCareerUI()
	{
		this.ShowChangeCareerUI(ChangeCareerInstanceManager.Instance.dst_profession);
		if (ChangeCareerInstanceManager.Instance.IsWinWithChange)
		{
			ChangeCareerManager.Instance.SendChangeCareer(ChangeCareerInstanceManager.Instance.dst_profession);
		}
	}

	public void ShowChangeCareerUI(int profession)
	{
		UIStackManager.Instance.ClearPush();
		UIStackManager.Instance.PushUI("TownUI", UIType.FullScreen, UINodesManager.NormalUIRoot);
		LinkNavigationManager.OpenChangeCareerUI();
		if (ChangeCareerUIView.Instance != null)
		{
			ChangeCareerUIView.Instance.SetCurrentInfo(profession);
		}
	}
}
