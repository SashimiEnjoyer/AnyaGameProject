using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillManager : MonoBehaviour
{
    public SkillUIManager[] skillUIs;
    [Space]
    public SkillStats dashSkillStats;
    public DashSkill dashSkill;
    [Space]
    public SkillStats radialSwingSkillStats;
    public RadialSwingSkill radialSwingSkill;
    [Space]
    public SkillStats kerisShootSkillStats;
    public KerisShootSkill kerisShootSkill;

    public List<Skill> SkillCatalogue = new List<Skill>();
    public Skill[] CurrentSkillSlot = new Skill[3];
    private float[] skillCooldown = new float[3];

    private void OnDisable()
    {
        dashSkillStats.onSkillEnded -= SkillEnded;
        radialSwingSkillStats.onSkillEnded -= SkillEnded;
    }

    public void InstantiateSkills(PlayerController player)
    {
        SkillCatalogue.Clear();

        dashSkill = new (player);
        dashSkillStats.onSkillEnded += SkillEnded;
        dashSkill.AssignStats(dashSkillStats);
        RegisterSkills(dashSkill, dashSkillStats);

/*
        radialSwingSkill = new (player);
        radialSwingSkillStats.onSkillEnded += SkillEnded;
        radialSwingSkill.AssignStats(radialSwingSkillStats);
        RegisterSkills(radialSwingSkill, radialSwingSkillStats);

        kerisShootSkill = new(player);
        kerisShootSkillStats.onSkillEnded += SkillEnded;
        kerisShootSkill.AssignStats(kerisShootSkillStats);
        RegisterSkills(kerisShootSkill, kerisShootSkillStats);
*/
        for (int i = 0; i < CurrentSkillSlot.Length; i++)
        {
            int temp = i;
            SetSkill(temp, SkillCatalogue[temp]);
        }
    }

    public CharacterState GetSkill(int skillIndex, CharacterState defaultState)
    {
        if (Time.time < skillCooldown[skillIndex])
        {
            Debug.LogWarning($"Skill {skillIndex + 1} is still cooldown!");
            return defaultState;
        }

        skillCooldown[skillIndex] = Time.time + CurrentSkillSlot[skillIndex].skillStats.cooldown + CurrentSkillSlot[skillIndex].skillStats.duration;
        skillUIs[skillIndex].SetValueToZero();
        return CurrentSkillSlot[skillIndex].SkillState;
    }

    public void SetSkill(int skillIndex, Skill skillToSet)
    {
        CurrentSkillSlot[skillIndex] = skillToSet;
        skillUIs[skillIndex].SetMaxValue(skillToSet.skillStats.cooldown + skillToSet.skillStats.duration);
        skillUIs[skillIndex].SetSkillImageSprite(skillToSet.skillStats.skillSprite);
    }

    private void SkillEnded()
    {
        Debug.Log("Skill 1 Ended!");
    }

    private void RegisterSkills(CharacterState skillsToAdd, SkillStats stats)
    {
        Skill s;
        s.SkillState = skillsToAdd;
        s.skillStats = stats;

        SkillCatalogue.Add(s);
    }
}

[System.Serializable]
public struct SkillStats
{
    public string name;
    public GameObject hitbox;
    public Sprite skillSprite;
    public float duration;
    public float cooldown;
    public float horizontalSpeed;
    public UnityAction onSkillEnded;
}

[System.Serializable]
public struct Skill
{
    public CharacterState SkillState;
    public SkillStats skillStats;
}

