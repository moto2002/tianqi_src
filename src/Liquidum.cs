using System;
using UnityEngine;

public class Liquidum : MonoBehaviour
{
	private const string EmittersPath = "ShaderEffect/ScreenRainDrop/ScreenRainDrop_Emitters";

	private const string DropPrefabPath = "ShaderEffect/ScreenRainDrop/DropPrefab01";

	public static Liquidum Instance;

	public bool RainEmit = true;

	public GameObject LIQUIDUMEmitter;

	private GameObject m_theCam;

	[W_Header("Drops On Screen Configuration", "", 16)]
	public string space;

	[W_ToolTip("Start/Stop this effect.\n*N.B. Drops_RainDependence override this bool.")]
	public bool Emit = true;

	[W_ToolTip("The Drops On Screen Effect is affected by the camera angle\nIt stops when the camera looks down.")]
	public bool UseCameraAngleAdjustment = true;

	[W_ToolTip("This effect depends on the amount of rain?\nFor example, in case of lack of rain this effect fade out.\nUseful if you want to use this effect with not LIQUIDUM rain effect")]
	public bool Drops_RainDependence = true;

	[W_ToolTip("The sound for this effect (when it is active).")]
	public AudioClip DropsSoundClip;

	[Range(0f, 1f)]
	public float DropsSoundVolume = 1f;

	[W_ToolTip("Color of drops.\nAlpha channel also manages the overall transparency of the drops")]
	public Color DropsColor = new Color(0.6f, 0.6f, 0.7f, 0.5f);

	[Range(0f, 50f)]
	public float Distortion = 30f;

	[Range(0.1f, 1f)]
	public float DropCreationDelay = 0.1f;

	[Range(0f, 1f)]
	public float MaxDistanceFromCenter = 0.6f;

	[Range(0f, 1f)]
	public float MinDistanceFromCenter;

	[Range(0f, 20f)]
	public float DropFadeSpeed = 4f;

	[Range(0f, 1f)]
	public float DropSlipSpeed = 0.5f;

	public Vector2 DropsScale = new Vector2(0.6f, 0.7f);

	public bool RandomScale = true;

	public bool RandomSpeed;

	[W_ToolTip("When true clear immediately all the drops on the screen")]
	public bool ClearAllDropsImmediately;

	[W_ToolTip("The number drop frames.\nNB: If you use multiple dorps prefabs, all of thems must have the same number of frames")]
	public int NumDropFrames = 10;

	[W_Header("Trail On Screen Configuration", "", 16)]
	public string space12;

	[W_ToolTip("Start/Stop this effect.\n*N.B. TrialRain_Dependence override this bool.")]
	public bool TrailEmit = true;

	[W_ToolTip("This effect depends on the amount of rain?\nFor example, in case of lack of rain this effect fade out.\nUseful if you want to use this effect without LIQUIDUM rain effect")]
	public bool TrialRain_Dependence = true;

	public Color TrailsColor = new Color(0.6f, 0.6f, 0.7f, 0.5f);

	[Range(0f, 50f)]
	public float TrailDistortion = 30f;

	[Range(0.5f, 10f)]
	public float TrailCreationDelay = 1f;

	[Range(0f, 1f)]
	public float TrailMaxDistanceFromCenter = 0.6f;

	[Range(0f, 1f)]
	public float TrailMinDistanceFromCenter;

	[Range(1f, 200f)]
	public float TrailSlipSpeed = 50f;

	[Range(0.1f, 1f)]
	public float TrailDropsFriction = 0.2f;

	[Range(0.1f, 2f)]
	public float TrialTail = 0.4f;

	[Range(-1f, 1f)]
	public float TrailConstantAngle = 0.1f;

	[Range(0.5f, 10f)]
	public float TrailScale = 1f;

	[W_Header("Trigger Distortion Global Setting", "", 16)]
	public string space65;

	[W_ToolTip("Start/Stop (no fade) this effect.")]
	public bool TriggerDistortionActive = true;

	public Color TriggerDistortionColor = new Color(0.6f, 0.6f, 0.7f, 0.5f);

	[Range(0f, 500f)]
	public float TriggerDistortionPower = 100f;

	[W_ToolTip("The X scroll speed for All TriggerDistortion Effect in the scene")]
	public float TriggerDistortionXscrollSpeed;

	[W_ToolTip("The Y scroll speed for All TriggerDistortion Effect in the scene")]
	public float TriggerDistortionYscrollSpeed = 0.4f;

	[Range(1f, 5f)]
	public float TriggerDistortionFadeOutSpeed = 3f;

	[Range(1f, 5f)]
	public float TriggerDistortionFadeInSpeed = 3f;

	public bool FadeInTriggerDistortionNow;

	public bool FadeOutTriggerDistortionNow;

	public GameObject TheCam
	{
		get
		{
			if (this.m_theCam == null)
			{
				this.m_theCam = GameObject.Find("MainCamera");
			}
			return this.m_theCam;
		}
	}

	public void EnableScreenRainDrop(bool bEnable)
	{
		Liquidum.Instance = this;
		if (this.LIQUIDUMEmitter != null)
		{
			this.LIQUIDUMEmitter.SetActive(bEnable);
		}
		if (bEnable)
		{
			this.InitVariables();
			if (this.LIQUIDUMEmitter == null)
			{
				Object @object = AssetManager.AssetOfNoPool.LoadAssetNowNoAB("ShaderEffect/ScreenRainDrop/ScreenRainDrop_Emitters", typeof(Object));
				this.LIQUIDUMEmitter = (Object.Instantiate(@object, new Vector3(1000f, 1000f, 1000f), (@object as GameObject).get_transform().get_rotation()) as GameObject);
				this.LIQUIDUMEmitter.set_name("Liquidum (On-Screen Main Emitters)");
			}
		}
	}

	private void InitVariables()
	{
		this.UseCameraAngleAdjustment = false;
		this.Distortion = 35f;
		this.MaxDistanceFromCenter = 1f;
		this.MinDistanceFromCenter = 0.1f;
		this.DropFadeSpeed = 3f;
		this.DropSlipSpeed = 0.3f;
		this.DropsScale = new Vector2(0.3f, 0.3f);
		this.NumDropFrames = 10;
		this.RandomScale = false;
	}
}
