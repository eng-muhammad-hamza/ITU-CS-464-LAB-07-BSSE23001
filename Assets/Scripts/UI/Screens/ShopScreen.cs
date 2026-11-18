using System.Collections;
using UnityEngine;

public class ShopScreen : UIScreen
{
    public RectTransform topBar;
    public GemChip gemChip;
    public RectTransform tabIndicator;
    public RectTransform[] tabButtons;
    public RectTransform orbsPanel;
    public RectTransform boostsPanel;
    public ShopRow[] orbRows;
    public ShopRow[] boostRows;

    int tab;
    int requestedTab = -1;
    Coroutine tabRoutine;

    public void RequestTab(int index) => requestedTab = index;

    protected override void OnShowStart()
    {
        int want = requestedTab >= 0 ? requestedTab : 0;
        requestedTab = -1;
        tab = want;
        PlaceTab(want);
        Refresh();
    }

    protected override void OnShown()
    {
        RectTransform[] rows = new RectTransform[orbRows.Length];
        for (int i = 0; i < rows.Length; i++) rows[i] = (RectTransform)orbRows[i].transform;
        if (tab == 0) Stagger(0f, rows);
    }

    float PanelWidth => ((RectTransform)orbsPanel.parent).rect.width > 0f ? ((RectTransform)orbsPanel.parent).rect.width : 960f;

    void PlaceTab(int index)
    {
        float w = PanelWidth;
        orbsPanel.anchoredPosition = new Vector2(index == 0 ? 0f : -w, 0f);
        boostsPanel.anchoredPosition = new Vector2(index == 0 ? w : 0f, 0f);
        tabIndicator.anchoredPosition = new Vector2(TabX(index), 0f);
    }

    float TabX(int index) => tabButtons[index].anchoredPosition.x;

    public void SetTab(int index, bool animate)
    {
        if (index == tab) return;
        tab = index;
        if (tabRoutine != null) StopCoroutine(tabRoutine);
        if (animate && gameObject.activeInHierarchy) tabRoutine = StartCoroutine(SlideTab(index));
        else PlaceTab(index);
    }

    IEnumerator SlideTab(int index)
    {
        float w = PanelWidth;
        yield return UiAnim.Parallel(this,
            UiAnim.Move(orbsPanel, orbsPanel.anchoredPosition, new Vector2(index == 0 ? 0f : -w, 0f), 0.22f, EaseType.OutQuad),
            UiAnim.Move(boostsPanel, boostsPanel.anchoredPosition, new Vector2(index == 0 ? w : 0f, 0f), 0.22f, EaseType.OutQuad),
            UiAnim.Move(tabIndicator, tabIndicator.anchoredPosition, new Vector2(TabX(index), 0f), 0.22f, EaseType.OutQuad));
    }

    void Refresh()
    {
        for (int i = 0; i < orbRows.Length; i++)
        {
            OrbDef orb = Catalog.Orbs[i];
            ShopRow row = orbRows[i];
            row.icon.color = orb.color;
            row.title.text = orb.name.ToUpper();
            row.detail.text = orb.perk;
            if (!GameData.OwnsOrb(i)) row.SetState("BUY  " + orb.price, UiTheme.Reward, UiTheme.TextOnBright, true);
            else if (GameData.SelectedOrb == i) row.SetState("EQUIPPED", UiTheme.Success, UiTheme.TextOnBright, false);
            else row.SetState("EQUIP", UiTheme.Action, UiTheme.TextOnBright, true);
        }
        for (int i = 0; i < boostRows.Length; i++)
        {
            BoostDef boost = Catalog.Boosts[i];
            ShopRow row = boostRows[i];
            row.title.text = boost.name.ToUpper();
            row.detail.text = boost.description;
            row.extra.text = "Owned: " + GameData.BoostCount(i);
            row.SetState("BUY  " + boost.price, UiTheme.Reward, UiTheme.TextOnBright, true);
        }
    }

    public void BuyOrb(int index)
    {
        OrbDef orb = Catalog.Orbs[index];
        ShopRow row = orbRows[index];
        if (GameData.OwnsOrb(index))
        {
            GameData.SelectedOrb = index;
            PlayerPrefs.Save();
            AudioHub.Play(SfxId.Confirm);
            UIManager.Instance.Toast(orb.name + " equipped");
        }
        else if (GameData.TrySpend(orb.price))
        {
            GameData.GiveOrb(index);
            GameData.SelectedOrb = index;
            PlayerPrefs.Save();
            AudioHub.Play(SfxId.Buy);
            GameData.Vibrate();
            StartCoroutine(UiAnim.Punch(row.transform, 0.05f, 0.3f));
            UIManager.Instance.Toast(orb.name + " unlocked!");
        }
        else Denied(row);
        Refresh();
    }

    public void BuyBoost(int index)
    {
        BoostDef boost = Catalog.Boosts[index];
        ShopRow row = boostRows[index];
        if (GameData.TrySpend(boost.price))
        {
            GameData.AddBoost(index, 1);
            AudioHub.Play(SfxId.Buy);
            StartCoroutine(UiAnim.Punch(row.extra.transform, 0.2f, 0.3f));
            UIManager.Instance.Toast(boost.name + " added");
        }
        else Denied(row);
        Refresh();
    }

    void Denied(ShopRow row)
    {
        AudioHub.Play(SfxId.Deny);
        StartCoroutine(UiAnim.Shake((RectTransform)row.transform, 12f, 0.25f));
        gemChip.FlashDenied();
        UIManager.Instance.Toast("Not enough gems - complete levels to earn more");
    }
}
