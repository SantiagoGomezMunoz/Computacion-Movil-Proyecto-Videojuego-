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

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (upgradePanel != null) upgradePanel.SetActive(false);

        // bind clicks
        assaultBuyButton.onClick.AddListener(() => { PurchaseAssault(); });
        axeBuyButton.onClick.AddListener(() => { PurchaseAxe(); });
        vitBuyButton.onClick.AddListener(() => { PurchaseVitality(); });

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

        pointsText.text = $"Puntos: {UpgradeManager.Instance.upgradePoints}";

        // Assault
        int aLevel = UpgradeManager.Instance.assaultRifleLevel;
        assaultLevelText.text = $"Nivel: {aLevel}/3";
        if (aLevel < 3) assaultCostText.text = $"Coste: {UpgradeManager.Instance.assaultRifleCosts[aLevel]}";
        else assaultCostText.text = "Max";
        assaultBuyButton.interactable = (aLevel < 3) && (UpgradeManager.Instance.upgradePoints >= UpgradeManager.Instance.assaultRifleCosts[aLevel]);

        // Axe
        int axL = UpgradeManager.Instance.axeLevel;
        axeLevelText.text = $"Nivel: {axL}/3";
        if (axL < 3) axeCostText.text = $"Coste: {UpgradeManager.Instance.axeCosts[axL]}";
        else axeCostText.text = "Max";
        axeBuyButton.interactable = (axL < 3) && (UpgradeManager.Instance.upgradePoints >= UpgradeManager.Instance.axeCosts[axL]);

        // Vitality
        int vL = UpgradeManager.Instance.vitalityLevel;
        vitLevelText.text = $"Nivel: {vL}/3";
        if (vL < 3) vitCostText.text = $"Coste: {UpgradeManager.Instance.vitalityCosts[vL]}";
        else vitCostText.text = "Max";
        vitBuyButton.interactable = (vL < 3) && (UpgradeManager.Instance.upgradePoints >= UpgradeManager.Instance.vitalityCosts[vL]);
    }

    public void PurchaseAssault()
    {
        if (UpgradeManager.Instance.UpgradeAssaultRifle())
        {
            // opcional: reproducir sonido/efecto
            RefreshUI();
        }
    }

    public void PurchaseAxe()
    {
        if (UpgradeManager.Instance.UpgradeAxe()) RefreshUI();
    }

    public void PurchaseVitality()
    {
        if (UpgradeManager.Instance.UpgradeVitality()) RefreshUI();
    }

    public void UpdatePointsUI() => RefreshUI();
}
