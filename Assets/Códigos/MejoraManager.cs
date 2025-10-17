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

    public int[] assaultRifleCosts = new int[] { 4, 6, 8 };
    public int[] axeCosts = new int[] { 3, 5, 7 };
    public int[] vitalityCosts = new int[] { 7, 10, 13 };

    public int[] assaultRifleAmmoByLevel = new int[] { 45, 60, 75 }; // level 1..3
    public int axeDamagePerLevel = 1; // cada nivel suma +1 daño

    public event Action OnUpgradesChanged;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        upgradePoints = 0;
        assaultRifleLevel = 0;
        axeLevel = 0;
        vitalityLevel = 0;
    }

    // ---- Puntos ----
    public void AddUpgradePoint(int amount = 1)
    {
        upgradePoints += amount;
        OnUpgradesChanged?.Invoke();
    }

    // ---- Getters / helpers ----
    public int GetAssaultRifleMaxAmmo()
    {
        if (assaultRifleLevel <= 0) return -1; // indica "usar valor base del arma"
        return assaultRifleAmmoByLevel[assaultRifleLevel - 1];
    }

    public int GetAxeExtraDamage()
    {
        return axeLevel * axeDamagePerLevel;
    }

    public int GetVitalityExtraHearts()
    {
        return vitalityLevel; // cada nivel suma 1 corazón
    }

    // ---- Compras ----
    public bool UpgradeAssaultRifle()
    {
        if (assaultRifleLevel >= 3) return false;
        int cost = assaultRifleCosts[assaultRifleLevel];
        if (upgradePoints >= cost)
        {
            upgradePoints -= cost;
            assaultRifleLevel++;
            OnUpgradesChanged?.Invoke();
            ApplyUpgradesToExistingItems();
            SaveState();
            return true;
        }
        NotifyUpgrades();
        return false;
    }

    public bool UpgradeAxe()
    {
        if (axeLevel >= 3) return false;
        int cost = axeCosts[axeLevel];
        if (upgradePoints >= cost)
        {
            upgradePoints -= cost;
            axeLevel++;
            OnUpgradesChanged?.Invoke();
            ApplyUpgradesToExistingItems();
            SaveState();
            return true;
        }
        NotifyUpgrades();
        return false;
    }

    public bool UpgradeVitality()
    {
        if (vitalityLevel >= 3) return false;
        int cost = vitalityCosts[vitalityLevel];
        if (upgradePoints >= cost)
        {
            upgradePoints -= cost;
            vitalityLevel++;
            // Aquí sí curamos al máximo porque es mejora de vitalidad
            var vida = FindFirstObjectByType<VidaJugador>();
            if (vida != null)
            {
                vida.ApplyVitalityUpgrades(1);
                vida.ActualizarCorazones(); // 🔹 fuerza refresco del HUD
            }
            OnUpgradesChanged?.Invoke();
            ApplyUpgradesToExistingItems();
            SaveState();
            return true;
        }
        NotifyUpgrades();
        return false;
    }

    // Aplica los efectos a armas/vida que ya existan en escena o en el inventario
    void ApplyUpgradesToExistingItems()
    {
        // Buscar todos los MonoBehaviours en escena
        var armas = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        foreach (var m in armas)
        {
            if (m is IApplyUpgrades aplicable)
                aplicable.ApplyUpgrades();
        }

        // Vida del jugador -> SOLO si es Vitality
        //if (vitalityLevel > 0)
        //{
            //var vida = FindFirstObjectByType<VidaJugador>();
            //if (vida != null) vida.ApplyVitalityUpgrades(1);
        //}

        //var inventario = FindFirstObjectByType<InventarioJugador>();
        //if (inventario != null && inventario.armaEquipada is IApplyUpgrades armaEquipada)
        //{
            //armaEquipada.ApplyUpgrades();
        //}
    }

    // ---- Guardado simple (PlayerPrefs) ----
    void SaveState()
    {
        PlayerPrefs.SetInt("UP_points", upgradePoints);
        PlayerPrefs.SetInt("UP_assault", assaultRifleLevel);
        PlayerPrefs.SetInt("UP_axe", axeLevel);
        PlayerPrefs.SetInt("UP_vit", vitalityLevel);
        PlayerPrefs.Save();
    }

    void LoadState()
    {
        if (PlayerPrefs.HasKey("UP_points"))
        {
            upgradePoints = PlayerPrefs.GetInt("UP_points", 0);
            assaultRifleLevel = PlayerPrefs.GetInt("UP_assault", 0);
            axeLevel = PlayerPrefs.GetInt("UP_axe", 0);
            vitalityLevel = PlayerPrefs.GetInt("UP_vit", 0);
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

// Interfaz ligera para que armas y otros implementen ApplyUpgrades
public interface IApplyUpgrades
{
    void ApplyUpgrades();
}