using System;
using System.Collections.Generic;

[Serializable]
public class GameSaveProfile {
    public string Nickname = "Player";
    public string SavedDate;

    public string MessageSent = null;

    public TreePartData TreePartData =  new TreePartData();
}