using System;
using UnityEngine;

public class WaveBloodAnimCurve : MonoBehaviour
{
	public AnimationCurve offsetCurve1 = new AnimationCurve();

	public AnimationCurve offsetXCurve1 = new AnimationCurve();

	public AnimationCurve alphaCurve1 = new AnimationCurve();

	public AnimationCurve scaleCurve1 = new AnimationCurve();

	public AnimationCurve offsetCurve2 = new AnimationCurve();

	public AnimationCurve offsetXCurve2 = new AnimationCurve();

	public AnimationCurve alphaCurve2 = new AnimationCurve();

	public AnimationCurve scaleCurve2 = new AnimationCurve();

	public AnimationCurve offsetCurve3 = new AnimationCurve();

	public AnimationCurve offsetXCurve3 = new AnimationCurve();

	public AnimationCurve alphaCurve3 = new AnimationCurve();

	public AnimationCurve scaleCurve3 = new AnimationCurve();

	public AnimationCurve offsetCurve4 = new AnimationCurve();

	public AnimationCurve offsetXCurve4 = new AnimationCurve();

	public AnimationCurve alphaCurve4 = new AnimationCurve();

	public AnimationCurve scaleCurve4 = new AnimationCurve();

	public AnimationCurve offsetCurve5 = new AnimationCurve();

	public AnimationCurve offsetXCurve5 = new AnimationCurve();

	public AnimationCurve alphaCurve5 = new AnimationCurve();

	public AnimationCurve scaleCurve5 = new AnimationCurve();

	public AnimationCurve offsetCurve6 = new AnimationCurve();

	public AnimationCurve offsetXCurve6 = new AnimationCurve();

	public AnimationCurve alphaCurve6 = new AnimationCurve();

	public AnimationCurve scaleCurve6 = new AnimationCurve();

	public AnimationCurve offsetCurve7 = new AnimationCurve();

	public AnimationCurve offsetXCurve7 = new AnimationCurve();

	public AnimationCurve alphaCurve7 = new AnimationCurve();

	public AnimationCurve scaleCurve7 = new AnimationCurve();

	public AnimationCurve offsetCurve8 = new AnimationCurve();

	public AnimationCurve offsetXCurve8 = new AnimationCurve();

	public AnimationCurve alphaCurve8 = new AnimationCurve();

	public AnimationCurve scaleCurve8 = new AnimationCurve();

	private static WaveBloodAnimCurve _Instance;

	public static WaveBloodAnimCurve Instance
	{
		get
		{
			if (WaveBloodAnimCurve._Instance == null)
			{
				GameObject instantiate2Prefab = ResourceManager.GetInstantiate2Prefab("WaveBloodAnimCurve");
				instantiate2Prefab.get_transform().SetParent(UINodesManager.UIRoot);
				WaveBloodAnimCurve._Instance = instantiate2Prefab.GetComponent<WaveBloodAnimCurve>();
			}
			return WaveBloodAnimCurve._Instance;
		}
	}

	public static void GetTrackList(int trackID, ref AnimationCurve offsetXCurve, ref AnimationCurve offsetYCurve, ref AnimationCurve alphaCurve, ref AnimationCurve scaleCurve)
	{
		switch (trackID)
		{
		case 1:
			offsetXCurve = WaveBloodAnimCurve.Instance.offsetXCurve1;
			offsetYCurve = WaveBloodAnimCurve.Instance.offsetCurve1;
			alphaCurve = WaveBloodAnimCurve.Instance.alphaCurve1;
			scaleCurve = WaveBloodAnimCurve.Instance.scaleCurve1;
			break;
		case 2:
			offsetXCurve = WaveBloodAnimCurve.Instance.offsetXCurve2;
			offsetYCurve = WaveBloodAnimCurve.Instance.offsetCurve2;
			alphaCurve = WaveBloodAnimCurve.Instance.alphaCurve2;
			scaleCurve = WaveBloodAnimCurve.Instance.scaleCurve2;
			break;
		case 3:
			offsetXCurve = WaveBloodAnimCurve.Instance.offsetXCurve3;
			offsetYCurve = WaveBloodAnimCurve.Instance.offsetCurve3;
			alphaCurve = WaveBloodAnimCurve.Instance.alphaCurve3;
			scaleCurve = WaveBloodAnimCurve.Instance.scaleCurve3;
			break;
		case 4:
			offsetXCurve = WaveBloodAnimCurve.Instance.offsetXCurve4;
			offsetYCurve = WaveBloodAnimCurve.Instance.offsetCurve4;
			alphaCurve = WaveBloodAnimCurve.Instance.alphaCurve4;
			scaleCurve = WaveBloodAnimCurve.Instance.scaleCurve4;
			break;
		case 5:
			offsetXCurve = WaveBloodAnimCurve.Instance.offsetXCurve5;
			offsetYCurve = WaveBloodAnimCurve.Instance.offsetCurve5;
			alphaCurve = WaveBloodAnimCurve.Instance.alphaCurve5;
			scaleCurve = WaveBloodAnimCurve.Instance.scaleCurve5;
			break;
		case 6:
			offsetXCurve = WaveBloodAnimCurve.Instance.offsetXCurve6;
			offsetYCurve = WaveBloodAnimCurve.Instance.offsetCurve6;
			alphaCurve = WaveBloodAnimCurve.Instance.alphaCurve6;
			scaleCurve = WaveBloodAnimCurve.Instance.scaleCurve6;
			break;
		case 7:
			offsetXCurve = WaveBloodAnimCurve.Instance.offsetXCurve7;
			offsetYCurve = WaveBloodAnimCurve.Instance.offsetCurve7;
			alphaCurve = WaveBloodAnimCurve.Instance.alphaCurve7;
			scaleCurve = WaveBloodAnimCurve.Instance.scaleCurve7;
			break;
		case 8:
			offsetXCurve = WaveBloodAnimCurve.Instance.offsetXCurve8;
			offsetYCurve = WaveBloodAnimCurve.Instance.offsetCurve8;
			alphaCurve = WaveBloodAnimCurve.Instance.alphaCurve8;
			scaleCurve = WaveBloodAnimCurve.Instance.scaleCurve8;
			break;
		default:
			offsetXCurve = WaveBloodAnimCurve.Instance.offsetXCurve1;
			offsetYCurve = WaveBloodAnimCurve.Instance.offsetCurve1;
			alphaCurve = WaveBloodAnimCurve.Instance.alphaCurve1;
			scaleCurve = WaveBloodAnimCurve.Instance.scaleCurve1;
			break;
		}
	}
}
