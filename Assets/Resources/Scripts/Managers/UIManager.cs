using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI 바")]
    public Slider hpBar;
    public Slider staminaBar;

    [Header("크로스헤어")]
    public GameObject crosshair;

    [Header("보스 UI")]
    public Slider bossHPBar;
    public Text bossNameText;

    [Header("아이템 UI")]
    public Image itemIcon;
    public Text itemCountText;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void UpdateHP(float current, float max)
    {
        if (hpBar != null)
            hpBar.value = current / max;
    }

    public void UpdateStamina(float current, float max)
    {
        if (staminaBar != null)
            staminaBar.value = current / max;
    }

    public void SetCrosshairVisible(bool visible)
    {
        if (crosshair != null)
            crosshair.SetActive(visible);
    }

    public void UpdateBossHP(float current, float max)
    {
        if (bossHPBar != null)
            bossHPBar.value = current / max;
    }

    public void SetBossName(string name)
    {
        if (bossNameText != null)
            bossNameText.text = name;
    }

    public void UpdateItem(Sprite icon, int count)
    {
        if (itemIcon != null)
            itemIcon.sprite = icon;
        if (itemCountText != null)
            itemCountText.text = count.ToString();
    }
}
