using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkllGroupItem : MonoBehaviour
{
	private int useSkillGroup;

	private Text skillDesc;

	private void Awake()
	{
		this.skillDesc = base.get_transform().FindChild("TextSkill").GetComponent<Text>();
	}

	private void Start()
	{
		base.GetComponent<ButtonCustom>().onClickCustom = new ButtonCustom.VoidDelegateObj(this.OnClickSkillGroup);
	}

	private void OnClickSkillGroup(GameObject go)
	{
		EventDispatcher.Broadcast<int>(EventNames.OnGetSelectSkillUseGroup, this.useSkillGroup);
	}

	public void SetItem(int useGroup, int index)
	{
		this.useSkillGroup = useGroup;
		this.skillDesc.set_text(string.Format("技能配置{0}", this.useSkillGroup));
		base.GetComponent<RectTransform>().set_anchoredPosition(new Vector3(0f, (float)(index * 60), 0f));
	}
}
