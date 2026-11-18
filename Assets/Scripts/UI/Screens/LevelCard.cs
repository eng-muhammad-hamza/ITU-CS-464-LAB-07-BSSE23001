using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCard : MonoBehaviour
{
    public UiButton button;
    public Image background;
    public TMP_Text numberText;
    public TMP_Text nameText;
    public TMP_Text goalText;
    public Image[] stars;
    public GameObject lockOverlay;
    public GameObject newBadge;

    public void Refresh(int index)
    {
        bool unlocked = GameData.IsLevelUnlocked(index);
        int earned = GameData.GetStars(index);
        LevelDef def = Catalog.Levels[index];

        numberText.text = (index + 1).ToString();
        nameText.text = def.name.ToUpper();
        goalText.text = "Collect " + def.shardTarget;
        lockOverlay.SetActive(!unlocked);
        newBadge.SetActive(unlocked && earned == 0);
        background.color = unlocked ? UiTheme.Surface : UiTheme.WithAlpha(UiTheme.Surface, 0.6f);
        button.SetRestColor(background.color);
        numberText.color = unlocked ? UiTheme.Action : UiTheme.Locked;
        nameText.color = unlocked ? UiTheme.Text : UiTheme.Locked;
        goalText.color = unlocked ? UiTheme.TextMuted : UiTheme.Locked;
        for (int i = 0; i < stars.Length; i++)
            stars[i].color = i < earned ? UiTheme.Reward : UiTheme.WithAlpha(UiTheme.Outline, 0.8f);
    }
}
