using System;

public abstract class BaseApi {
    //protected static readonly string BaseUrl = "http://localhost:3001/miniapp";
    protected static readonly string BaseUrl = "https://stoic-bot-grafenters.amvera.io/miniapp";
}


[Serializable]
public class AbstractMongoEntity {
    public string _id;
    public string createdAt;
    public string updatedAt;
    public string __v;

    public DateTime CreatedAt => DateTime.Parse(createdAt);
    public DateTime UpdatedAt => DateTime.Parse(updatedAt);
}