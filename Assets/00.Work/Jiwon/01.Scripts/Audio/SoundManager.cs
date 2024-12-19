using System;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerGroup masterGroup;
    [SerializeField] private AudioMixerGroup _bgmGroup;
    [SerializeField] private AudioMixerGroup _sfxGroup;
    [SerializeField] private SoundDataSO bgmData;
    
    private float _masterVolume;
    private float _bgmVolume;
    private float _sfxVolume;

    private void Awake()
    {
        _masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        _bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        _sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        
        SetMasterVolume(_masterVolume);
        SetBGMVolume(_bgmVolume);
        SetSFXVolume(_sfxVolume);
        
    }

    private void Start()
    {
        if (bgmData != null)
        {
            var player = PoolManager.Instance.Pop("SoundPlayer") as SoundPlayer;
           player.PlaySound(bgmData);
        }
    }

    private void OnEnable()
    {
        PlayerPrefs.SetFloat("Mater", _masterVolume);
        PlayerPrefs.SetFloat("Bgm", _bgmVolume);
        PlayerPrefs.SetFloat("Sfx", _sfxVolume);
        
    }

    public void SetMasterVolume(float volume)
    {
        float value = Mathf.Log10(volume) * 20;
        _masterVolume = value;
        audioMixer.SetFloat(masterGroup.name, value );
    }
    
    public void SetSFXVolume(float volume)
    {
        float value = Mathf.Log10(volume) * 20;
        _sfxVolume = value;
        audioMixer.SetFloat(_sfxGroup.name, value);
    }

    public void SetBGMVolume(float volume)
    {
        float value = Mathf.Log10(volume) * 20;
        _bgmVolume = value;
        audioMixer.SetFloat(_bgmGroup.name, value);
    }
}