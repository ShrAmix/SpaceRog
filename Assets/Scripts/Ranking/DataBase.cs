using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Collections;
using UnityEngine.XR;
using System.Linq;

public class DataBase : MonoBehaviour
{
    DatabaseReference dbRef;
    void Start()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveData(string name,string password, string email)
    {
        User user= new User(name,password,email, PlayerPrefs.GetInt("BestScore0"));
        string json=JsonUtility.ToJson(user);
        dbRef.Child("users").Child(name).SetRawJsonValueAsync(json);
    }
    public IEnumerator LoadData(string str)
    {
        var user = dbRef.Child("users").OrderByChild("score").GetValueAsync();

        yield return new WaitUntil(predicate: () => user.IsCompleted);
        if (user.Exception != null)
        {
            Debug.LogError(user.Exception);
        }
        else if (user.Result == null)
        {
            Debug.Log("NULL");
        }
        else
        {
            DataSnapshot snapshot = user.Result;
            foreach(DataSnapshot chilldSnaphop in snapshot.Children.Reverse())
            {
                Debug.Log(chilldSnaphop.Child("name").Value.ToString()+ chilldSnaphop.Child("score").Value.ToString());
            }
            
        }
    }
    public void RemoveData()
    {
        
    }
}
public class User
{
    public string name;
    public string password;
    public string email;
    public int score;

    public User(string name, string password, string email, int score)
    {
        this.name = name;
        this.password = password;
        this.email = email;
        this.score = score;
        this.score = score;
    }
}