using System;
using UnityEngine;

public class ShieldCollisionBehaviour : MonoBehaviour
{
	public GameObject EffectOnHit;

	public GameObject ExplosionOnHit;

	public bool IsWaterInstance;

	public float ScaleWave = 0.89f;

	public bool CreateMechInstanceOnHit;

	public Vector3 AngleFix;

	public int currentQueue = 3001;

	public void ShieldCollisionEnter(CollisionInfo e)
	{
		if (e.Hit.get_transform() != null)
		{
			if (this.IsWaterInstance)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.ExplosionOnHit);
				Transform transform = gameObject.get_transform();
				transform.set_parent(base.get_transform());
				float num = base.get_transform().get_localScale().x * this.ScaleWave;
				transform.set_localScale(new Vector3(num, num, num));
				transform.set_localPosition(new Vector3(0f, 0.001f, 0f));
				transform.LookAt(e.Hit.get_point());
			}
			else
			{
				if (this.EffectOnHit != null)
				{
					if (!this.CreateMechInstanceOnHit)
					{
						Transform transform2 = e.Hit.get_transform();
						Renderer componentInChildren = transform2.GetComponentInChildren<Renderer>();
						GameObject gameObject2 = Object.Instantiate<GameObject>(this.EffectOnHit);
						gameObject2.get_transform().set_parent(componentInChildren.get_transform());
						gameObject2.get_transform().set_localPosition(Vector3.get_zero());
						AddMaterialOnHit component = gameObject2.GetComponent<AddMaterialOnHit>();
						component.SetMaterialQueue(this.currentQueue);
						component.UpdateMaterial(e.Hit);
					}
					else
					{
						GameObject gameObject3 = Object.Instantiate<GameObject>(this.EffectOnHit);
						Transform transform3 = gameObject3.get_transform();
						transform3.set_parent(base.GetComponent<Renderer>().get_transform());
						transform3.set_localPosition(Vector3.get_zero());
						transform3.set_localScale(base.get_transform().get_localScale() * this.ScaleWave);
						transform3.LookAt(e.Hit.get_point());
						transform3.Rotate(this.AngleFix);
						gameObject3.GetComponent<Renderer>().get_material().set_renderQueue(this.currentQueue - 1000);
					}
				}
				if (this.currentQueue > 4000)
				{
					this.currentQueue = 3001;
				}
				else
				{
					this.currentQueue++;
				}
				if (this.ExplosionOnHit != null)
				{
					GameObject gameObject4 = Object.Instantiate(this.ExplosionOnHit, e.Hit.get_point(), default(Quaternion)) as GameObject;
					gameObject4.get_transform().set_parent(base.get_transform());
				}
			}
		}
	}

	private void Update()
	{
	}
}
