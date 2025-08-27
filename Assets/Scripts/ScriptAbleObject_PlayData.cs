using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[System.Serializable] public class Items
{
    public string name;
    public string code;
    public int codeNumber;
    public string rarity;
    public int rarityNumber;
    public Sprite sprite;
    public Image image;
    public ulong amount = 1;
    
}

[System.Serializable] public class CreateArea
{
    public int AreaWidth;
    public int AreaHeight;
    public float space;
    public GameObject playArea;
    public GameObject prefabObj;
    public float scalePrefabObj;
    
}

[System.Serializable] public class Items_C
{
    public string name;
    public string code;
    public int codeNumber;
    public Sprite sprite;
    public Image image;
    public ulong amount;
    public AudioClip audio;
}

[System.Serializable] public class Items_U
{
    public string name;
    public string code;
    public int codeNumber;
    public Sprite sprite;
    public Image image;
    public ulong amount;
    public AudioClip audio;
}

[System.Serializable] public class Items_R
{
    public string name;
    public string code;
    public int codeNumber;
    public Sprite sprite;
    public Image image;
    public ulong amount;
    public AudioClip audio;
}

[System.Serializable]public class Items_L
{
    public string name;
    public string code;
    public int codeNumber;
    public Sprite sprite;
    public Image image;
    public ulong amount;
    public AudioClip audio;
}

[System.Serializable] public class Items_M
{
    public string name;
    public string code;
    public int codeNumber;
    public Sprite sprite;
    public Image image;
    public ulong amount;
    public AudioClip audio;
}
[System.Serializable] public class Others  // used by others game
{
    public float barFill_Min = 0.0f;
    public float barFill_Max;
    public float barFill_inc;

    //public int match_target;

    //public string Item_Common;
    //public string Item_Uncommon;
    //public string Item_Rare;
    //public string Item_Legendary;
    //public string Item_Mythic;

    //public int amount_B_item;
    //public int amount_A_item;
    //public int amount_S_item;
}

[System.Serializable] public class SFX 
{
    public string name;
    public string code;
    public int codeNumber;
    public AudioClip sound;
}

[System.Serializable] public class Prefab
{
    public string name;
    public string code;
    public int codeNumber;
    public AudioClip sound;
    public GameObject gameObject;
}

[System.Serializable] public class PrefabObst
{
    public string name;
    public string code;
    public int codeNumber;
    public AudioClip sound;
    public GameObject gameObject;
}

[System.Serializable]
public class ballLv
{
    public string name;
    public string code;
    public int codeNumber;
    public float scale;
    public GameObject gameObject;
    public Sprite sprite;
    public ulong amount;
}

[System.Serializable]
public class ballImg
{
    public string code;
    public int codeNumber;
    public GameObject gameObject;
    public Sprite sprite;
    
}

[CreateAssetMenu(fileName = "PlayData", menuName = "ScriptAbleObject/PlayData", order = 1)]

public class ScriptAbleObject_PlayData : ScriptableObject
{
    public List<CreateArea> CreateArea;
    public List<Items> Items;
    public List<Items_C> Items_C;
    public List<Items_U> Items_U;
    public List<Items_R> ItemsR;
    public List<Items_L> Items_L;
    public List<Items_M> Items_M;
    public List<SFX> SFX;
    public List<Others> Others;
    //public List<Prefab> Prefab;
    //public List<PrefabObst> PrefabObst;
    public List<ballLv> ballLv;
    public List<ballImg> ballImg;
    public ulong overLvBonus;
    public float timerSoftWarning;
    public float BetweenTouchDelay;
    public float JumpDelay;
    public float jumpPower;
    public float collisionDelay;
    public ulong HighScore;

    [Range(0f, 1f)] public float audioVolume = 1f;
    [Range(0f, 1f)] public float FxVolume = 1f;
}
