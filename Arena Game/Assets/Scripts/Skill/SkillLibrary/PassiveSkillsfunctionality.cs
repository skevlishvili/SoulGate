using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillsfunctionality : MonoBehaviour
{
    public static UnitStruct UnitStatsPassiveSum;

    float[] SumValuesArray;


    private void Start()
    {
        UnitStatsPassiveSum = new UnitStruct();

        SkillLibrary.Skills[20].Passive += (Skill Skill, GameObject Player) => {
            var Unitstat = Player.GetComponent<Unit>();
            SumValuesArray = new float[8]{ Skill.HealthBuff, Skill.HealthRegenBuff, Skill.PhysicalDamage, Skill.MagicDefenceBuff, Skill.DamageBuff, Skill.AgilityBuff, Skill.MoneyRegenBuff, Skill.CooldownBuff };
            Unitstat.ChangeUnitStats(SumUnitStatsChange(SumValuesArray));
        };

        SkillLibrary.Skills[21].Passive += (Skill Skill, GameObject Player) => {
            var Unitstat = Player.GetComponent<Unit>();
            SumValuesArray = new float[8] { Skill.HealthBuff, Skill.HealthRegenBuff, Skill.PhysicalDamage, Skill.MagicDefenceBuff, Skill.DamageBuff, Skill.AgilityBuff, Skill.MoneyRegenBuff, Skill.CooldownBuff };
            Unitstat.ChangeUnitStats(SumUnitStatsChange(SumValuesArray));
        };

        SkillLibrary.Skills[22].Passive += (Skill Skill, GameObject Player) => {
            var Unitstat = Player.GetComponent<Unit>();
            SumValuesArray = new float[8] { Skill.HealthBuff, Skill.HealthRegenBuff, Skill.PhysicalDamage, Skill.MagicDefenceBuff, Skill.DamageBuff, Skill.AgilityBuff, Skill.MoneyRegenBuff, Skill.CooldownBuff };
            Unitstat.ChangeUnitStats(SumUnitStatsChange(SumValuesArray));
        };

        SkillLibrary.Skills[23].Passive += (Skill Skill, GameObject Player) => {
            var Unitstat = Player.GetComponent<Unit>();
            SumValuesArray = new float[8] { Skill.HealthBuff, Skill.HealthRegenBuff, Skill.PhysicalDamage, Skill.MagicDefenceBuff, Skill.DamageBuff, Skill.AgilityBuff, Skill.MoneyRegenBuff, Skill.CooldownBuff };
            Unitstat.ChangeUnitStats(SumUnitStatsChange(SumValuesArray));
        };

        SkillLibrary.Skills[24].Passive += (Skill Skill, GameObject Player) => {
            var Unitstat = Player.GetComponent<Unit>();
            SumValuesArray = new float[8] { Skill.HealthBuff, Skill.HealthRegenBuff, Skill.PhysicalDamage, Skill.MagicDefenceBuff, Skill.DamageBuff, Skill.AgilityBuff, Skill.MoneyRegenBuff, Skill.CooldownBuff };
            Unitstat.ChangeUnitStats(SumUnitStatsChange(SumValuesArray));
        };

        SkillLibrary.Skills[25].Passive += (Skill Skill, GameObject Player) => {
            var Unitstat = Player.GetComponent<Unit>();
            SumValuesArray = new float[8] { Skill.HealthBuff, Skill.HealthRegenBuff, Skill.PhysicalDamage, Skill.MagicDefenceBuff, Skill.DamageBuff, Skill.AgilityBuff, Skill.MoneyRegenBuff, Skill.CooldownBuff };
            Unitstat.ChangeUnitStats(SumUnitStatsChange(SumValuesArray));
        };

        SkillLibrary.Skills[26].Passive += (Skill Skill, GameObject Player) => {
            var Unitstat = Player.GetComponent<Unit>();
            SumValuesArray = new float[8] { Skill.HealthBuff, Skill.HealthRegenBuff, Skill.PhysicalDamage, Skill.MagicDefenceBuff, Skill.DamageBuff, Skill.AgilityBuff, Skill.MoneyRegenBuff, Skill.CooldownBuff };
            Unitstat.ChangeUnitStats(SumUnitStatsChange(SumValuesArray));
        };

        SkillLibrary.Skills[27].Passive += (Skill Skill, GameObject Player) => {
            var Unitstat = Player.GetComponent<Unit>();
            SumValuesArray = new float[8] { Skill.HealthBuff, Skill.HealthRegenBuff, Skill.PhysicalDamage, Skill.MagicDefenceBuff, Skill.DamageBuff, Skill.AgilityBuff, Skill.MoneyRegenBuff, Skill.CooldownBuff };
            Unitstat.ChangeUnitStats(SumUnitStatsChange(SumValuesArray));
        };

        SkillLibrary.Skills[28].Passive += (Skill Skill, GameObject Player) =>
        {
            var Unitstat = Player.GetComponent<Unit>();
            if (Unitstat.Health <= Unitstat.MaxHealth/2)
            {
                SumValuesArray = new float[8] { Skill.HealthBuff, Skill.HealthRegenBuff, Skill.PhysicalDamage, Skill.MagicDefenceBuff, Skill.DamageBuff, Skill.AgilityBuff, Skill.MoneyRegenBuff, Skill.CooldownBuff };
                Unitstat.ChangeUnitStats(SumUnitStatsChange(SumValuesArray));
            }
        };

        SkillLibrary.Skills[29].Passive += (Skill Skill, GameObject Player) => {
            var Unitstat = Player.GetComponent<Unit>();
            if (Unitstat.Health <= Unitstat.MaxHealth / 10)
            {
                SumValuesArray = new float[8] { Skill.HealthBuff, Skill.HealthRegenBuff, Skill.PhysicalDamage, Skill.MagicDefenceBuff, Skill.DamageBuff, Skill.AgilityBuff, Skill.MoneyRegenBuff, Skill.CooldownBuff };
                Unitstat.ChangeUnitStats(SumUnitStatsChange(SumValuesArray));
            }
        };

    }

    float[] SumUnitStatsChange(float[] Values)
    {
        UnitStatsPassiveSum.Health += Values[0];
        UnitStatsPassiveSum.HealthRegen += Values[1];
        UnitStatsPassiveSum.PhysicalDefence += Values[2];
        UnitStatsPassiveSum.MagicDefence += Values[3];
        UnitStatsPassiveSum.Damage += Values[4];
        UnitStatsPassiveSum.Agility += Values[5];
        UnitStatsPassiveSum.MoneyRegen += Values[6];
        UnitStatsPassiveSum.AbilityCooldown += Values[7];

        return new float[8] { UnitStatsPassiveSum.Health, UnitStatsPassiveSum.HealthRegen, UnitStatsPassiveSum.PhysicalDefence, UnitStatsPassiveSum.MagicDefence, UnitStatsPassiveSum.Agility, UnitStatsPassiveSum.Agility, UnitStatsPassiveSum.MoneyRegen, UnitStatsPassiveSum.AbilityCooldown };
    }
}
