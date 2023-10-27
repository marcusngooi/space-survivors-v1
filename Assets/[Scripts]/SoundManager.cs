/*  Author's Name:          Marcus Ngooi
 *  Last Modified By:       Marcus Ngooi
 *  Date Last Modified:     October 26, 2023
 *  Program Description:    Manages sound --> Plays sounds and changes volume.
 *  Revision History:       October 26, 2023: Initial SoundManager script.
 */

using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource musicAudioSource;
    [SerializeField] AudioSource sfxAudioSource;

    [Header("SFX Audio Clips")]
    [SerializeField] AudioClip skillToLevelUpSelect;

    [Header("Debug")]
    [SerializeField] private float musicVolume = 1f;
    [SerializeField] private float sfxVolume = 1f;
    [SerializeField] private string musicParameter = "MusicVol";
    [SerializeField] private string sfxParameter = "SfxVol";

    private const float setVolumeMultiplier = 20f;

    public float MusicVolume { get { return musicVolume; } }
    public float SfxVolume { get { return sfxVolume; } }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(musicParameter))
        {
            SetVolume(PlayerPrefs.GetFloat(musicParameter), SoundType.MUSIC);
        }
        else
        {
            PlayerPrefs.SetFloat(musicParameter, 0.5f);
            SetVolume(PlayerPrefs.GetFloat(musicParameter), SoundType.MUSIC);
        }
        if (PlayerPrefs.HasKey(sfxParameter))
        {
            SetVolume(PlayerPrefs.GetFloat(sfxParameter), SoundType.SFX);
        }
        else
        {
            PlayerPrefs.SetFloat(sfxParameter, 0.5f);
            SetVolume(PlayerPrefs.GetFloat(sfxParameter), SoundType.SFX);
        }
    }
    public void ChangeMusic(AudioClip clip)
    {
        if (!musicAudioSource.clip.name.Equals(clip.name)) musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }
    public void PlaySfx(SfxEvent sfxEvent)
    {
        switch (sfxEvent)
        {
            case SfxEvent.SkillToLevelUpSelect:
                sfxAudioSource.PlayOneShot(skillToLevelUpSelect);
                break;
        }

    }
    public void SetVolume(float value, SoundType type)
    {
        float newValue = Mathf.Log10(value) * setVolumeMultiplier;

        if (value == 0)
        {
            newValue = -100;
        }

        switch (type)
        {
            case SoundType.MUSIC:
                musicVolume = value;
                audioMixer.SetFloat(musicParameter, newValue);
                PlayerPrefs.SetFloat(musicParameter, value);
                break;
            case SoundType.SFX:
                sfxVolume = value;
                audioMixer.SetFloat(sfxParameter, newValue);
                PlayerPrefs.SetFloat(sfxParameter, value);
                break;
            default:
                Debug.LogError("Please assign the sound type before setting volume");
                break;
        }
    }
}