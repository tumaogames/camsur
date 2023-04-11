using UnityEngine;

[System.Serializable]
public class GoogleUser
{
    public string sub;
    public string name;
    public string given_name;
    public string family_name;
    public string picture;
    public string email;
    public string email_verified;
    public string locale;


    public static GoogleUser CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<GoogleUser>(jsonString);
    }

    // Given JSON input:
    // {"name":"Dr Charles","lives":3,"health":0.8}
    // this example will return a PlayerInfo object with
    // name == "Dr Charles", lives == 3, and health == 0.8f.
}
