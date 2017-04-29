using System;
using UnityEngine;

public class GameLevelManager
{
	public class LODLevel
	{
		public const int Normal = 200;

		public const int Better = 250;

		public const int Excellent = 300;

		public const int TopEffectHidden = 1000;

		public static int GetDefault()
		{
			int @int = SysConfigID.GetInt(16);
			if (@int == 1)
			{
				return 200;
			}
			if (@int == 2)
			{
				return 250;
			}
			if (@int == 3)
			{
				return 300;
			}
			return 200;
		}
	}

	public class ResolutionNum
	{
		public const int High = 1920;

		public const int Middle = 1280;

		public const int Low = 960;

		public const string HighName = "1920";

		public const string MiddleName = "1280";

		public const string LowName = "960";
	}

	public class GameLevelVariable
	{
		public const int RealTimeShadow = 1000;

		public const int LuminousOutline = 1000;

		public const int HitFX = 200;

		public const int REALLY_MAX_PEOPLE = 100;

		public const int MIN_PEOPLE_NUM = 0;

		public const int SHOW_PEOPLE_NUM = 50;

		public const int MAX_PEOPLE_NUM = 100;

		private static int _LODLEVEL = -1;

		public static int FXMaxParticles = 1000;

		private static int _PeopleNum = 45;

		public static int LODLEVEL
		{
			get
			{
				if (GameLevelManager.GameLevelVariable._LODLEVEL == -1)
				{
					GameLevelManager.GameLevelVariable._LODLEVEL = PlayerPrefs.GetInt("QualityOfLODName", GameLevelManager.LODLevel.GetDefault());
				}
				return GameLevelManager.GameLevelVariable._LODLEVEL;
			}
			set
			{
				GameLevelManager.GameLevelVariable._LODLEVEL = value;
				PlayerPrefs.SetInt("QualityOfLODName", GameLevelManager.GameLevelVariable._LODLEVEL);
			}
		}

		public static int PeopleNum
		{
			get
			{
				return GameLevelManager.GameLevelVariable._PeopleNum;
			}
			set
			{
				GameLevelManager.GameLevelVariable._PeopleNum = Mathf.Max(value, 0);
			}
		}

		public static int GetRealLODLEVEL()
		{
			if (!SystemConfig.IsPostProcessOn)
			{
				return GameLevelManager.LODLevel.GetDefault();
			}
			return GameLevelManager.GameLevelVariable.LODLEVEL;
		}
	}

	private static GameObject go_scene_fx;

	public static void SetGameQuality(int lod, bool init = false)
	{
		GameLevelManager.GameLevelVariable.LODLEVEL = lod;
		if (!SystemConfig.IsPostProcessOn)
		{
			lod = GameLevelManager.LODLevel.GetDefault();
		}
		GameLevelManager.SetGlobalMaximumLOD(lod);
		GameLevelManager.SetFXMaxParticles(lod);
		GameLevelManager.SetShadow(lod, init);
		GameLevelManager.SetFog(lod);
		GameLevelManager.SetSceneFX(lod, false);
		EventDispatcher.Broadcast(ShaderEffectEvent.LODChanged);
	}

	public static bool IsPostProcessReachQuality(int lod)
	{
		return SystemConfig.IsPostProcessOn && Shader.get_globalMaximumLOD() >= lod;
	}

	public static bool IsHitFXOn()
	{
		return Shader.get_globalMaximumLOD() >= 200;
	}

	public static bool IsRealTimeShadowOn()
	{
		return Shader.get_globalMaximumLOD() >= 1000;
	}

	public static bool IsLuminousOutlineOn()
	{
		return Shader.get_globalMaximumLOD() >= 1000;
	}

	private static void SetGlobalMaximumLOD(int lod)
	{
		GameLevelManager.GameLevelVariable.LODLEVEL = lod;
		Shader.set_globalMaximumLOD(Mathf.Min(lod, 300));
	}

	private static void SetFXMaxParticles(int lod)
	{
		if (lod != 200)
		{
			if (lod != 250)
			{
				if (lod != 300)
				{
					if (lod == 1000)
					{
						GameLevelManager.GameLevelVariable.FXMaxParticles = 1000;
					}
				}
				else
				{
					GameLevelManager.GameLevelVariable.FXMaxParticles = 1000;
				}
			}
			else
			{
				GameLevelManager.GameLevelVariable.FXMaxParticles = 50;
			}
		}
		else
		{
			GameLevelManager.GameLevelVariable.FXMaxParticles = 10;
		}
	}

	private static void SetShadow(int lod, bool init = false)
	{
		if (!init)
		{
			ShadowController.RefreshShadows();
		}
		else
		{
			ShadowController.IsPreviousRealTimeShadowEnable = GameLevelManager.IsRealTimeShadowOn();
		}
	}

	private static void SetFog(int lod)
	{
		if (lod != 200)
		{
			if (lod == 250 || lod == 300 || lod == 1000)
			{
				RenderSettings.set_fog(true);
			}
		}
		else
		{
			RenderSettings.set_fog(true);
		}
	}

	public static void SetSceneFX(int lod, bool isLoadScene)
	{
		if (isLoadScene)
		{
			GameLevelManager.go_scene_fx = GameObject.Find("SceneFX");
		}
		else if (GameLevelManager.go_scene_fx == null)
		{
			GameLevelManager.go_scene_fx = GameObject.Find("SceneFX");
		}
		if (GameLevelManager.go_scene_fx == null)
		{
			return;
		}
		if (lod != 200 && lod != 250)
		{
			if (lod == 300 || lod == 1000)
			{
				GameLevelManager.go_scene_fx.SetActive(true);
			}
		}
		else
		{
			GameLevelManager.go_scene_fx.SetActive(false);
		}
	}
}
