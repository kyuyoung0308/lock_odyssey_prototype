using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake(){
        if (GameManager.instance != null){
            Destroy(gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        //DontDestroyOnLoad(gameObject);
    }

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> exp;

    public Player player;
    //public Weapon weapon;
    public TextManager floatingTextManager;
    // Start is called before the first frame update
    public int experience;
    public int gold;

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration){
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public void SaveState(){
        string save = "";
        save += "0" +"|";
        save += gold.ToString() + "|";
        save += exp.ToString()+"|";
        save += "0";

        PlayerPrefs.SetString("SaveState", save);

    }
    public void LoadState(Scene s, LoadSceneMode mode){
        if(PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        gold = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        Debug.Log("LoadState");
    }
}
