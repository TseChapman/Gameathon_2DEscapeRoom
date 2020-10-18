using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] m_sounds;

    public void Play(string name)
    {
        Sound s = Array.Find(m_sounds, sound => sound.m_name == name);
        if (s == null)
            return;
        s.m_source.Play();
    }

    private void Awake()
    {
        foreach (Sound s in m_sounds)
        {
            s.m_source = gameObject.AddComponent<AudioSource>();
            s.m_source.clip = s.m_clip;

            s.m_source.volume = s.m_volume;
            s.m_source.pitch = s.m_pitch;
            s.m_source.loop = s.m_loop;
        }
    }

    private void Start()
    {
        Play("Theme");
    }
}
