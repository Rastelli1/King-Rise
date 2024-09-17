using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Listas : ScriptableObject
{
    public Ficha[] skins;
    
    public int contSkins
    {
        get
        {
            return skins.Length;
        }
    }

    public Ficha ObtenerSkins(int index)
    {
        return skins[index];
    }
}
