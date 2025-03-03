using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    [SerializeField]SO_Playlist playlist;

    void OnEnable()
    {
        if(MUSIC.PLAYER) MUSIC.PLAYER.SetPlaylist(playlist);
        Debug.Log(gameObject.name + " lol");
    }


} // SCRIPT END
