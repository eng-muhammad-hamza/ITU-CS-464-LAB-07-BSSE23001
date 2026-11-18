using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrbTile : MonoBehaviour
{
    public UiButton button;
    public Image background;
    public Image orb;
    public Image selectRing;
    public GameObject lockIcon;

    public void Refresh(int index, bool selected)
    {
        bool owned = GameData.OwnsOrb(index);
        orb.color = owned ? Catalog.Orbs[index].color : UiTheme.WithAlpha(Catalog.Orbs[index].color, 0.25f);
        lockIcon.SetActive(!owned);
        selectRing.gameObject.SetActive(selected);
        background.color = selected ? UiTheme.SurfaceHi : UiTheme.Surface;
        button.SetRestColor(background.color);
    }
}
