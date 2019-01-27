using UnityEngine;

public class ClickAudioController : MonoBehaviour
{
    public static AudioSource ClickAudio;

    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        ClickAudio = GetComponent<AudioSource>();
    }
}
