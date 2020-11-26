using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public List<CharacterSlot> characters = new List<CharacterSlot>();
    public GameObject charCellPrefab;
    public List<GameObject> charCells;
    public CSSCursor cursor;
    CSSCursor p1Cursor, p2Cursor;
    public GameController gc;

    private void Awake()
    {
        gc = FindObjectOfType<GameController>();
        foreach(CharacterSlot c in characters)
        {
            SpawnCharacterSlot(c);

        }
        if (!gc.isNetworked)
        {
            p1Cursor = Instantiate<CSSCursor>(cursor, transform);
            p1Cursor.playerID = 1;
            p2Cursor = Instantiate<CSSCursor>(cursor, transform);
            p2Cursor.playerID = 2;
        }

        else
        {
            p1Cursor = Instantiate<CSSCursor>(cursor, transform);
            p1Cursor.playerID = 1;
        }
    }

    void SpawnCharacterSlot(CharacterSlot character)
    {
        GameObject charCell = Instantiate(charCellPrefab, transform);

        charCell.name = character.characterName;

        Image artwork = charCell.transform.Find("artwork").GetComponent<Image>();

        artwork.sprite = character.characterSprite;
        charCells.Add(charCell);
       

    }


}
