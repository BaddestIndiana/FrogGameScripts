using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveAndLoad : MonoBehaviour
{
    public playerData userInfo;
    public Data user;

    void Start()
    {
        user = userInfo.user;
        user.playerTag = Load().playerTag;
        user.kills = Load().kills;
        user.skin = Load().skin;
        user.character = Load().character;
        user.caps = Load().caps;
        user.feet = Load().feet;
        user.torso = Load().torso;
    }

    Data Load()
    {
        string loadInfo = File.ReadAllText(Application.persistentDataPath + "/userData/User.json");
        return JsonUtility.FromJson<Data>(loadInfo);
    }

    public void save()
    {
        userInfo.RefreshAtros();
        string thisSave = JsonUtility.ToJson(user);
        File.WriteAllText(Application.persistentDataPath + "/userData/User.json", thisSave);
    }
}
