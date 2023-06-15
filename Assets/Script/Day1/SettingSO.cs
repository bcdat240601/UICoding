using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingSO : ScriptableObject
{
    public float Sound = 0f;
    public float Music = 0f;
    public int Graphic = 0;
    public int Language = 0;

    public virtual void LoadData(SettingSO settingSO)
    {
        Sound = settingSO.Sound;
        Music = settingSO.Music;
        Graphic = settingSO.Graphic;
        Language = settingSO.Language;
    }
}
