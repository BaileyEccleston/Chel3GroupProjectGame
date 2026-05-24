using UnityEngine;

public class CharacterCustomisation : MonoBehaviour
{
    public enum BlueOutfits
    {
        Default,
        Fish,
        Sad,
    }

    public enum RedOutfits
    {
        Default,
        Fish,
        Happy,
    }

    private BlueOutfits blueOutfit = BlueOutfits.Default;
    private RedOutfits redOutfit = RedOutfits.Default;

    void Start()
    {
        SaveOutfits();
    }

    void SaveOutfits()
    {
        PlayerPrefs.SetInt("BlueOutfit", (int)blueOutfit);
        PlayerPrefs.SetInt("RedOutfit", (int)redOutfit);
        PlayerPrefs.Save();
    }

    void SwitchBlue(int direction)
    {
        int outfitCount = System.Enum.GetValues(typeof(BlueOutfits)).Length;

        int newIndex = ((int)blueOutfit + direction + outfitCount) % outfitCount;

        blueOutfit = (BlueOutfits)newIndex;

        SaveOutfits();

    }

    void SwitchRed(int direction)
    {
        int outfitCount = System.Enum.GetValues(typeof(RedOutfits)).Length;

        int newIndex = ((int)redOutfit + direction + outfitCount) % outfitCount;

        redOutfit = (RedOutfits)newIndex;

        SaveOutfits();

    }

    public void BlueSwitchRight()
    {
        SwitchBlue(1);
    }

    public void BlueSwitchLeft()
    {
        SwitchBlue(-1);
    }

    public void RedSwitchRight()
    {
        SwitchRed(1);
    }

    public void RedSwitchLeft()
    {
        SwitchRed(-1);
    }
}