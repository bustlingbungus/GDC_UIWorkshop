using UnityEngine;

public class Flag : MonoBehaviour
{
    GM_Platform gamemode = null;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ValidGMRef();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (ValidGMRef())
            {
                gamemode.CollectFlag();
                Destroy(gameObject);
            }
        }
    }


    bool ValidGMRef()
    {
        if (gamemode == null)
        {
            GameObject gmobj = GameObject.FindGameObjectWithTag("Gamemode");
            if (gmobj == null) return false;
            gamemode = gmobj.GetComponent<GM_Platform>();
        }
        return gamemode != null;
    }
}
