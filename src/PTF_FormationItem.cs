using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PTF_FormationItem : MonoBehaviour
{
	private void Awake()
	{
		base.get_transform().GetComponent<Button>().get_onClick().AddListener(new UnityAction(this.OnButtonClick));
	}

	private void OnButtonClick()
	{
		PetSelectUI petSelectUI = UIManagerControl.Instance.OpenUI("PetSelectUI", null, false, UIType.NonPush) as PetSelectUI;
		petSelectUI.RefreshUIIsPetTask(PetTaskFormationUIView.Instance.m_petIds, PetTaskFormationUIView.Instance.m_task_quality);
	}
}
