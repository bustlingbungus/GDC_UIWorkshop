using System.Collections.Generic;
using UnityEngine;

public class GM_Platform : MonoBehaviour
{
    public UIScript uiCanvas;



    AudioSource confetti_sound = null;
    AudioSource fire_sound = null;


    [SerializeField]
    int FlagsToWin = 3;

    int flags_collected = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioSource[] audio = GetComponents<AudioSource>();
        if (audio.Length > 0) confetti_sound = audio[0];
        if (audio.Length > 1) fire_sound = audio[1];
        flags_collected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectFlag()
    {
        if (ValidAudioRefs()) confetti_sound.Play();
        
        ++flags_collected;
        if (flags_collected >= FlagsToWin)
        {
            EndGame(true);
        }


        uiCanvas.SetFlagCount(flags_collected);
    }


    void EndGame(bool win)
    {
        // diable player control
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController pc = player.GetComponent<PlayerController>();
            if (pc != null) pc.player_control = false;
        }


        if (win)
        {
            Debug.Log("You Win!");
        }
        else
        {
            Debug.Log("You Lose!");
        }


        uiCanvas.EndGame(win);
    }







    public void PlayerDeath()
    {
        if (ValidAudioRefs()) fire_sound.Play();
        EndGame(false);
    }



    bool ValidAudioRefs()
    {
        if (confetti_sound != null && fire_sound != null) return true;

        AudioSource[] audio = GetComponents<AudioSource>();
        if (audio.Length > 0) confetti_sound = audio[0];
        if (audio.Length > 1) fire_sound = audio[1];
        return confetti_sound != null && fire_sound != null;
    }
}
