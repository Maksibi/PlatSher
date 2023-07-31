using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private EntityFX fx;

    [Header("Major stats")]
    public Stat strength; // damage, crit damage
    public Stat agility; // crit chance, evasion
    public Stat intelligence; // magic damage, magic resist
    public Stat vitality; // health, armor, magic resist
    [Header("Defensive stats")]
    public Stat maxHealth;
    private int currentHealth;

    public int CurrentHealth => currentHealth;

    public Stat armor; // damage - armor
    public Stat evasion; // ignore damage chance
    public Stat magicResist; // magic dmg - mR
    [Header("Offensive stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower; // damage * critPower
    [Header("Magic stats")]
    public Stat fireDamage; // damage overtime
    public Stat iceDamage; // slow target
    public Stat lightingDamage; // reduce accuracy
    [SerializeField] private float ailmentsDuration = 3;

    public bool isIgnited, isChilled, isShocked;

    private float ignitedTimer, chilledTimer, shockedTimer;

    private float igniteDamageCooldown = 0.3f;
    private float igniteDamageTimer;
    private int igniteDamage;

    public Action OnHealthChanged;

    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        currentHealth = GetMaxHealthValue();

        fx = GetComponent<EntityFX>();
    }

    protected virtual void Update()
    {
        ignitedTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;
        shockedTimer -= Time.deltaTime;

        igniteDamageTimer -= Time.deltaTime;

        if (ignitedTimer < 0)
            isIgnited = false;
        if (chilledTimer < 0)
            isChilled = false;
        if (shockedTimer < 0)
            isShocked = false;

        if (igniteDamageTimer < 0 && isIgnited)
        {
            Debug.Log("burnDamage" + igniteDamage);

            DecreaseHealthBy(igniteDamage);

            if (currentHealth <= 0)
                Die();

            igniteDamageTimer = igniteDamageCooldown;
        }
    }

    #region Combat

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + vitality.GetValue();
    }

    private int CheckTargetArmor(CharacterStats _targetStats, int totalDamage)
    {
        totalDamage -= _targetStats.armor.GetValue() + _targetStats.vitality.GetValue();
        totalDamage = Mathf.Clamp(totalDamage, 1, int.MaxValue);
        return totalDamage;
    }

    private bool TargetCanAvoidAttack(CharacterStats _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        if (isShocked)
            totalEvasion += 20;

        if (UnityEngine.Random.Range(0, 100) < totalEvasion)
        {
            Debug.Log("MISS");
            return true;
        }
        return false;
    }

    private bool CanCrit()
    {
        int totalCriticalChance = critChance.GetValue() + agility.GetValue();

        if (UnityEngine.Random.Range(0, 100) <= totalCriticalChance)
            return true;

        return false;
    }

    private int CalculateCritDamage(int _damage)
    {
        float totalCritPower = (critPower.GetValue() + strength.GetValue()) * 0.1f;

        float critDamage = _damage * totalCritPower;

        return Mathf.RoundToInt(critDamage);
    }

    public virtual void TakeDamage(int _damage)
    {
        DecreaseHealthBy(_damage);

        if (currentHealth <= 0)
            Die();

        OnHealthChanged?.Invoke();
    }

    public virtual void DoMagicDamage(CharacterStats _targetStats)
    {
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightingDamage = lightingDamage.GetValue();

        if (_fireDamage + _iceDamage + _lightingDamage <= 0)
            return;

        int totalMagicDamage = _fireDamage + _iceDamage + _lightingDamage + intelligence.GetValue();

        totalMagicDamage = CheckTargetMagicResist(_targetStats, totalMagicDamage);
        _targetStats.TakeDamage(totalMagicDamage);

        if (Mathf.Max(_fireDamage, _iceDamage, _lightingDamage) <= 0)
            return;

        bool canApplyIgnite = _fireDamage > _iceDamage && _fireDamage > _lightingDamage;
        bool canApplyChill = _iceDamage > _fireDamage && _iceDamage > _lightingDamage;
        bool canApplyShock = _lightingDamage > _fireDamage && _lightingDamage > _iceDamage;

        while (!canApplyIgnite && !canApplyChill && !canApplyShock)
        {
            if (UnityEngine.Random.value < 0.5f && _fireDamage > 0)
            {
                canApplyIgnite = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
            if (UnityEngine.Random.value < 0.5f && _iceDamage > 0)
            {
                canApplyChill = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
            if (UnityEngine.Random.value < 0.5f && _lightingDamage > 0)
            {
                canApplyShock = true;
                _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
        }

        if (canApplyIgnite)
            _targetStats.SetupIgniteDamage(Mathf.RoundToInt(_fireDamage * 0.2f));

        _targetStats.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
    }

    public void SetupIgniteDamage(int _damage) => igniteDamage = _damage;

    private int CheckTargetMagicResist(CharacterStats _targetStats, int totalMagicDamage)
    {
        totalMagicDamage -= _targetStats.magicResist.GetValue() + intelligence.GetValue() + vitality.GetValue();
        totalMagicDamage = Mathf.Clamp(totalMagicDamage, 1, int.MaxValue);
        return totalMagicDamage;
    }

    public void ApplyAilments(bool _ignite, bool _chill, bool _shock)
    {
        if (isIgnited || isChilled || isShocked)
            return;

        if (_ignite)
        {
            isIgnited = _ignite;
            ignitedTimer = ailmentsDuration;

            fx.IgniteFXFor(ailmentsDuration);
        }
        if (_chill)
        {
            isChilled = _chill;
            chilledTimer = ailmentsDuration;

            fx.ChillFXFor(ailmentsDuration);
        }
        if (_shock)
        {
            isShocked = _shock;
            shockedTimer = ailmentsDuration;

            fx.ShockFXFor(ailmentsDuration);
        }

        isIgnited = _ignite;
        isChilled = _chill;
        isShocked = _shock;
    }

    public virtual void DoDamage(CharacterStats _targetStats)
    {
        if (TargetCanAvoidAttack(_targetStats))
            return;

        int totalDamage = damage.GetValue() + strength.GetValue();

        if (CanCrit())
        {
            Debug.Log("CRITICAL");
            totalDamage = CalculateCritDamage(totalDamage);
        }

        totalDamage = CheckTargetArmor(_targetStats, totalDamage);

        //if (totalDamage > 0)
           // _targetStats.TakeDamage(totalDamage);
        DoMagicDamage(_targetStats);
    }

    protected virtual void Die()
    {
        //Destroy(gameObject);
    }

    protected virtual void DecreaseHealthBy(int _damage)
    {
        currentHealth -= _damage;

        if (OnHealthChanged != null)
            OnHealthChanged();
    }

    #endregion
}
