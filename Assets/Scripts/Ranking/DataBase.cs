using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Collections;
using UnityEngine.XR;
using System.Linq;
using UnityEngine.UI;
using Firebase.Auth;
using TMPro;
using System;

public class DataBase : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI email;
    public TextMeshProUGUI password;
    public TextMeshProUGUI textInfo;
    DatabaseReference dbRef;
    FirebaseAuth auth;
    public GameObject canvasLogin;
    public GameObject canvasBoard;
    public GameObject LeaderBoard;
    private HintSystem hintSystem;
    void Start()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        auth = FirebaseAuth.DefaultInstance;
        //auth.StateChanged += Auth_StateChanged;
        hintSystem=GetComponent<HintSystem>();
    }

    /*private void Auth_StateChanged(object sender, System.EventArgs e)
    {
        if(auth.CurrentUser != null)
        {
            //textInfo.text = "Login Is "+auth.CurrentUser.Email;
            canvasLogin.SetActive(false);
            canvasBoard.SetActive(true);
        }
        else
        {
            canvasLogin.SetActive(true);
            canvasBoard.SetActive(false);
        }
    }*/

    public void SaveData(string name, string password, string email)
    {
        User user = new User(name, password, email, PlayerPrefs.GetInt("BestScore0"), PlayerPrefs.GetInt("BestScore1"), PlayerPrefs.GetInt("BestScore2"), PlayerPrefs.GetInt("Money"));
        string json = JsonUtility.ToJson(user);
        dbRef.Child("users").Child(name).SetRawJsonValueAsync(json);
        /*DatabaseReference userRef = dbRef.Child("users").Child(name);

        userRef.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Обробка помилки
                Debug.LogError("Error checking account existence: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (snapshot.Exists)
                {
                    // Акаунт із вказаним іменем вже існує
                    Debug.LogWarning("Account with name '" + name + "' already exists.");
                }
                else
                {
                    // Акаунт з іменем не існує, створюємо новий
                    User user = new User(name, password, email, PlayerPrefs.GetInt("BestScore0"), PlayerPrefs.GetInt("BestScore1"), PlayerPrefs.GetInt("BestScore2"), PlayerPrefs.GetInt("Money"));
                    string json = JsonUtility.ToJson(user);
                    dbRef.Child("users").Child(name).SetRawJsonValueAsync(json);
                }
            }
        });*/
    }

    public IEnumerator LoadData()
    {
        var user = dbRef.Child("users").Child(name.text).Child("score").OrderByChild("1").GetValueAsync();

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
                Debug.Log(chilldSnaphop.Child("name").Value.ToString()+" "+ chilldSnaphop.Child("1").Value.ToString());
            }
            
        }
    }
    public void ButtonLogin()
    {
        auth.SignInWithEmailAndPasswordAsync(email.text, password.text);
    }
    public void ButtonRegister()
    {
        auth.CreateUserWithEmailAndPasswordAsync(email.text, password.text);
    }
}
public class User
{
    public string name;
    public string password;
    public string email;
    public int score0;
    public int score1;
    public int score2;
    public int money;

    public User(string name, string password, string email, int score1, int score2, int score3, int money)
    {
        this.name = name;
        this.password = password;
        this.email = email;
        this.score0 = score0;
        this.score1 = score1;
        this.score2 = score2;
        this.money = money;
    }
}