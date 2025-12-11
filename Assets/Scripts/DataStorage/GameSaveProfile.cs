using System;
using System.Collections.Generic;

[Serializable]
public class GameSaveProfile {
    public string Nickname = "Player";
    public string SavedDate;

    public string MessageSent = null;

    public List<ToyData> Toys = new List<ToyData>();
}