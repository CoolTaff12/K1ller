using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapSelection : MonoBehaviour
{
    public UnityStandardAssets.Network.LobbyManager LobbyScenes;
    public Text OptionSelected;
    public Image MapFolder;
    public Sprite[] MapImages;

    // Use this for initialization
    void Start ()
    {
        LobbyScenes = GameObject.Find("LobbyManager").GetComponent< UnityStandardAssets.Network.LobbyManager>();
        OptionSelected = GameObject.Find("OptionSelection").GetComponent<Text>();
        MapFolder = GameObject.Find("MapImage").GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(OptionSelected.text == "GYM_1")
        {
            GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().playScene = "HermanGympasal";
            GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().matchMap = "GYM_1";
            MapFolder.sprite = MapImages[0];
        }
        if (OptionSelected.text == "GYM_2")
        {
            GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().playScene = "SergejTestGym";
            GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().matchMap = "GYM_2";
            MapFolder.sprite = MapImages[1];
        }
        if (OptionSelected.text == "Chin_1")
        {
            GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().playScene = "Chinese";
            GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().matchMap = "Chin_1";
            MapFolder.sprite = MapImages[2];
        }

        if (OptionSelected.text == "Char_1")
        {
            GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().playScene = "Character";
            GameObject.Find("LobbyManager").GetComponent<UnityStandardAssets.Network.LobbyManager>().matchMap = "Char_1";
            MapFolder.sprite = MapImages[3];
        }
    }
}
