using UnityEngine;

public class AudioSystem : MonoBehaviour
{

    public Sound[] soundArray;

    void Awake()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("AudioSystem");
        if(objects.Length > 1) Destroy(gameObject);
        DontDestroyOnLoad(this);
    }

    public void PlaySound(AudioSource audioSource, string clipName)
    {
        AudioClip clip = FindSound(clipName);
        if(clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private AudioClip FindSound(string clipName)
    {
        for(int i = 0; i < soundArray.Length; i++)
        {
            if(soundArray[i].name == clipName)
            {
                return soundArray[i].clip;
            }
        }
        return null;
    }

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

}
