using System;
using UnityEngine;

public class LiquidumDropOnScreenEmitter : MonoBehaviour
{
	public GameObject DropPrefab;

	private float OrDelay;

	private int ActualNumberOfDropOnScreen;

	private GameObject Drop;

	private AudioSource DropsSound;

	private float MyTimer;

	private void Awake()
	{
		this.OrDelay = Liquidum.Instance.DropCreationDelay;
		if (Liquidum.Instance.DropsSoundClip)
		{
			this.DropsSound = base.get_gameObject().AddComponent<AudioSource>();
			this.DropsSound.set_volume(Liquidum.Instance.DropsSoundVolume);
			this.DropsSound.set_clip(Liquidum.Instance.DropsSoundClip);
			this.DropsSound.set_loop(true);
		}
	}

	private void DropOnScreen()
	{
		if (Liquidum.Instance.Emit && this.MyTimer >= Liquidum.Instance.DropCreationDelay)
		{
			float maxDistanceFromCenter = Liquidum.Instance.MaxDistanceFromCenter;
			Vector3 vector = new Vector3(Random.Range(-maxDistanceFromCenter * 1.5f, maxDistanceFromCenter * 1.5f), Random.Range(-maxDistanceFromCenter, maxDistanceFromCenter), 1f + Random.Range(-maxDistanceFromCenter / 150f, maxDistanceFromCenter / 150f));
			if (vector.get_magnitude() > Liquidum.Instance.MinDistanceFromCenter * 2.5f)
			{
				this.Drop = (GameObject)Object.Instantiate(this.DropPrefab, vector, this.DropPrefab.get_transform().get_rotation());
				this.Drop.get_transform().set_parent(base.get_transform());
				this.Drop.get_transform().set_localPosition(vector);
				this.MyTimer = 0f;
				if (this.DropsSound != null && !this.DropsSound.get_isPlaying())
				{
					this.DropsSound.Play();
				}
			}
		}
		if (this.DropsSound != null && this.DropsSound.get_clip() && !Liquidum.Instance.Emit)
		{
			this.DropsSound.Stop();
		}
	}

	private void DropOnScreenNoDipendence()
	{
		if (Liquidum.Instance.Emit && this.MyTimer >= Liquidum.Instance.DropCreationDelay)
		{
			Vector3 vector = new Vector3(Random.Range(-Liquidum.Instance.MaxDistanceFromCenter * 1.5f, Liquidum.Instance.MaxDistanceFromCenter * 1.5f), Random.Range(-Liquidum.Instance.MaxDistanceFromCenter, Liquidum.Instance.MaxDistanceFromCenter), 1f + Random.Range(-Liquidum.Instance.MaxDistanceFromCenter, Liquidum.Instance.MaxDistanceFromCenter));
			if (vector.get_magnitude() > Liquidum.Instance.MinDistanceFromCenter * 2f)
			{
				this.Drop = (GameObject)Object.Instantiate(this.DropPrefab, vector, this.DropPrefab.get_transform().get_rotation());
				this.Drop.get_transform().set_parent(base.get_transform());
				this.Drop.get_transform().set_localPosition(vector);
				this.MyTimer = 0f;
				if (this.DropsSound != null && !this.DropsSound.get_isPlaying())
				{
					this.DropsSound.Play();
				}
			}
		}
		if ((!Liquidum.Instance.Emit || Liquidum.Instance.DropCreationDelay > 1f) && this.DropsSound != null)
		{
			this.DropsSound.Stop();
		}
	}

	private void Update()
	{
		this.MyTimer += Time.get_deltaTime();
		if (!Liquidum.Instance.TheCam)
		{
			Debuger.Error("WARNING: No main parent. Drag the Liquidum_Effect prefab under your Main Camera or your Character/Player Controller", new object[0]);
			return;
		}
		if (Liquidum.Instance.UseCameraAngleAdjustment)
		{
			if (Liquidum.Instance.TheCam.get_transform().get_forward().y < 0f)
			{
				Liquidum.Instance.DropCreationDelay = 100f;
			}
			else
			{
				Liquidum.Instance.DropCreationDelay = this.OrDelay;
			}
		}
		if (Liquidum.Instance.Drops_RainDependence)
		{
			this.DropOnScreen();
		}
		else
		{
			this.DropOnScreenNoDipendence();
		}
		if (Liquidum.Instance.ClearAllDropsImmediately)
		{
			for (int i = 0; i < base.GetComponentsInChildren<Liquidum_Drops>().Length; i++)
			{
				base.GetComponentsInChildren<Liquidum_Drops>()[i].ClearImmediate();
			}
			Liquidum.Instance.ClearAllDropsImmediately = false;
		}
	}
}
