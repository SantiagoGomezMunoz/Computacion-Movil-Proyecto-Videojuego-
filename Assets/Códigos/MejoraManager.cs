using System; 
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [Header("Puntos")]
    public int upgradePoints = 0;

    [Header("Niveles (0..3)")]
    [Range(0, 3)] public int assaultRifleLevel = 0;
    [Range(0, 3)] public int axeLevel = 0;
    [Range(0, 3)] public int vitalityLevel = 0;
    [Range(0, 3)] public int shotgunLevel = 0;

    [Header("Costes por mejora")]
    public int[] assaultRifleCosts = new int[] { 4, 6, 8 };
    public int[] axeCosts = new int[] { 3, 5, 7 };
    public int[] vitalityCosts = new int[] { 7, 10, 13 };
    public int[] shotgunCosts = new int[] { 5, 7, 9 }; 

    [Header("Valores de mejora")]
    public int[] assaultRifleAmmoByLevel = new int[] { 45, 60, 75 }; 
    public int axeDamagePerLevel = 1;
    public int[] shotgunPelletsByLevel = new int[] { 5, 7, 9 }; 
    public event Action OnUpgradesChanged;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        ResetUpgrades();
    }

        void ResetUpgrades()
    {
        upgradePoints = 0;
        assaultRifleLevel = 0;
        axeLevel = 0;
        vitalityLevel = 0;
        shotgunLevel = 0;
    }

    // ---- Puntos ----
    public void AddUpgradePoint(int amount = 1)
    {
        upgradePoints += amount;
        OnUpgradesChanged?.Invoke();
    }

    // ---- Getters ----
    public int GetAssaultRifleMaxAmmo()
    {
        if (assaultRifleLevel <= 0) return -1;
        return assaultRifleAmmoByLevel[assaultRifleLevel - 1];
    }

    public int GetAxeExtraDamage() => axeLevel * axeDamagePerLevel;

    public int GetVitalityExtraHearts() => vitalityLevel;

    public int GetShotgunPellets()
    {
        if (shotgunLevel <= 0) return 3; // valor base
        return shotgunPelletsByLevel[shotgunLevel - 1];
    }

    // ---- Compras ----
    public bool UpgradeAssaultRifle()
    {
        return TryUpgrade(ref assaultRifleLevel, assaultRifleCosts);
    }

    public bool UpgradeAxe()
    {
        return TryUpgrade(ref axeLevel, axeCosts);
    }

    public bool UpgradeVitality()
    {
        if (TryUpgrade(ref vitalityLevel, vitalityCosts))
        {
            var vida = FindFirstObjectByType<VidaJugador>();
            if (vida != null)
            {
                vida.ApplyVitalityUpgrades(1);
                vida.ActualizarCorazones();
            }
            return true;
        }
        return false;
    }

    public bool UpgradeShotgun()
    {
        return TryUpgrade(ref shotgunLevel, shotgunCosts); 
    }

    // ---- Lógica común ----
    bool TryUpgrade(ref int level, int[] costs)
    {
        if (level >= 3) return false;
        int cost = costs[level];
        if (upgradePoints >= cost)
        {
            upgradePoints -= cost;
            level++;
            OnUpgradesChanged?.Invoke();
            ApplyUpgradesToExistingItems();
            return true;
        }
        NotifyUpgrades();
        return false;
    }

    // ---- Aplicar mejoras ----
    void ApplyUpgradesToExistingItems()
    {
        var armas = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        foreach (var m in armas)
        {
            if (m is IApplyUpgrades aplicable)
                aplicable.ApplyUpgrades();
        }
    }

    public void NotifyUpgrades()
    {
        var upgradables = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var u in upgradables)
        {
            if (u is IApplyUpgrades target)
                target.ApplyUpgrades();
        }
    }
}

public interface IApplyUpgrades
{
    void ApplyUpgrades();
}