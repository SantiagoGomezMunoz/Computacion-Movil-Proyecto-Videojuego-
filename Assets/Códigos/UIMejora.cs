using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIUpgrade : MonoBehaviour
{
    public static UIUpgrade Instance;
    [Header("Refs")]
    public GameObject upgradePanel;
    public TMP_Text pointsText;

    [Header("Assault (fusil)")]
    public TMP_Text assaultLevelText;
    public TMP_Text assaultCostText;
    public Button assaultBuyButton;

    [Header("Axe (hacha)")]
    public TMP_Text axeLevelText;
    public TMP_Text axeCostText;
    public Button axeBuyButton;

    [Header("Vitality")]
    public TMP_Text vitLevelText;
    public TMP_Text vitCostText;
    public Button vitBuyButton;

    [Header("Shotgun (escopeta)")]
    public TMP_Text shotgunLevelText;
    public TMP_Text shotgunCostText;
    public Button shotgunBuyButton;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (upgradePanel != null) upgradePanel.SetActive(false);

        assaultBuyButton.onClick.AddListener(() => { PurchaseAssault(); });
        axeBuyButton.onClick.AddListener(() => { PurchaseAxe(); });
        vitBuyButton.onClick.AddListener(() => { PurchaseVitality(); });
        shotgunBuyButton.onClick.AddListener(() => { PurchaseShotgun(); }); // 🔹 NUEVO

        if (UpgradeManager.Instance != null)
            UpgradeManager.Instance.OnUpgradesChanged += RefreshUI;

        RefreshUI();
    }

    public void TogglePanel()
    {
        upgradePanel.SetActive(!upgradePanel.activeSelf);
        RefreshUI();
    }

    void RefreshUI()
    {
        if (UpgradeManager.Instance == null) return;

        var up = UpgradeManager.Instance;
        pointsText.text = $"Puntos: {up.upgradePoints}";

        // Assault
        SetUpgradeUI(up.assaultRifleLevel, up.assaultRifleCosts, assaultLevelText, assaultCostText, assaultBuyButton);

        // Axe
        SetUpgradeUI(up.axeLevel, up.axeCosts, axeLevelText, axeCostText, axeBuyButton);

        // Vitality
        SetUpgradeUI(up.vitalityLevel, up.vitalityCosts, vitLevelText, vitCostText, vitBuyButton);

        // Shotgun 
        SetUpgradeUI(up.shotgunLevel, up.shotgunCosts, shotgunLevelText, shotgunCostText, shotgunBuyButton);
    }

    void SetUpgradeUI(int level, int[] costs, TMP_Text levelText, TMP_Text costText, Button button)
    {
        levelText.text = $"Nivel: {level}/3";
        if (level < 3)
        {
            costText.text = $"Coste: {costs[level]}";
            button.interactable = UpgradeManager.Instance.upgradePoints >= costs[level];
        }
        else
        {
            costText.text = "Max";
            button.interactable = false;
        }
    }

    public void PurchaseAssault() { if (UpgradeManager.Instance.UpgradeAssaultRifle()) RefreshUI(); }
    public void PurchaseAxe() { if (UpgradeManager.Instance.UpgradeAxe()) RefreshUI(); }
    public void PurchaseVitality() { if (UpgradeManager.Instance.UpgradeVitality()) RefreshUI(); }
    public void PurchaseShotgun() { if (UpgradeManager.Instance.UpgradeShotgun()) RefreshUI(); } 

    public void UpdatePointsUI() => RefreshUI();
}