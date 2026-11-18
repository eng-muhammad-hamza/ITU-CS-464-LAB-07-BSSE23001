using System.Collections.Generic;
using UnityEngine;

public enum SfxId { Click, Back, Confirm, Deny, Pickup, Heart, Shield, Hurt, Dash, Fire, Win, Lose, Tick, Go, Buy, Whoosh }

public class AudioHub : MonoBehaviour
{
    const int SampleRate = 44100;
    static AudioHub instance;

    AudioSource sfxSource;
    AudioSource musicSource;
    readonly Dictionary<SfxId, AudioClip> clips = new Dictionary<SfxId, AudioClip>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Boot()
    {
        if (instance != null) return;
        GameObject go = new GameObject("AudioHub");
        DontDestroyOnLoad(go);
        instance = go.AddComponent<AudioHub>();
    }

    void Awake()
    {
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.ignoreListenerPause = true;

        clips[SfxId.Click] = Tone(0.06f, 900f, 700f, 0.5f, false, 30f);
        clips[SfxId.Back] = Tone(0.07f, 600f, 420f, 0.5f, false, 30f);
        clips[SfxId.Confirm] = Sequence(new[] { 660f, 880f, 1320f }, 0.07f, 0.5f);
        clips[SfxId.Deny] = Tone(0.18f, 220f, 150f, 0.5f, true, 12f);
        clips[SfxId.Pickup] = Tone(0.12f, 880f, 1500f, 0.45f, false, 14f);
        clips[SfxId.Heart] = Sequence(new[] { 523f, 659f, 784f }, 0.06f, 0.5f);
        clips[SfxId.Shield] = Tone(0.22f, 400f, 1200f, 0.4f, false, 7f);
        clips[SfxId.Hurt] = Tone(0.25f, 260f, 80f, 0.6f, true, 9f);
        clips[SfxId.Dash] = Noise(0.18f, 0.35f);
        clips[SfxId.Fire] = Tone(0.1f, 500f, 200f, 0.35f, true, 20f);
        clips[SfxId.Win] = Sequence(new[] { 523f, 659f, 784f, 1047f, 1319f }, 0.09f, 0.5f);
        clips[SfxId.Lose] = Sequence(new[] { 392f, 330f, 262f, 196f }, 0.14f, 0.5f);
        clips[SfxId.Tick] = Tone(0.08f, 700f, 700f, 0.45f, false, 25f);
        clips[SfxId.Go] = Tone(0.25f, 1000f, 1000f, 0.5f, false, 8f);
        clips[SfxId.Buy] = Sequence(new[] { 988f, 1319f }, 0.08f, 0.5f);
        clips[SfxId.Whoosh] = Noise(0.28f, 0.22f);

        musicSource.clip = MakeMusic();
        RefreshVolumes();
        musicSource.Play();
    }

    public static void Play(SfxId id, float pitch = 1f)
    {
        if (instance == null || !instance.clips.TryGetValue(id, out AudioClip clip)) return;
        instance.sfxSource.pitch = pitch;
        instance.sfxSource.PlayOneShot(clip, GameData.SfxVolume);
    }

    public static void RefreshVolumes()
    {
        if (instance != null) instance.musicSource.volume = GameData.MusicVolume * 0.45f;
    }

    static AudioClip Tone(float seconds, float freqStart, float freqEnd, float volume, bool square, float decay)
    {
        int count = Mathf.CeilToInt(seconds * SampleRate);
        float[] data = new float[count];
        float phase = 0f;
        for (int i = 0; i < count; i++)
        {
            float t = (float)i / count;
            float freq = Mathf.Lerp(freqStart, freqEnd, t);
            phase += 2f * Mathf.PI * freq / SampleRate;
            float wave = square ? Mathf.Sign(Mathf.Sin(phase)) * 0.6f : Mathf.Sin(phase);
            float attack = Mathf.Clamp01(i / (0.004f * SampleRate));
            data[i] = wave * volume * attack * Mathf.Exp(-decay * t);
        }
        return MakeClip("tone", data);
    }

    static AudioClip Sequence(float[] notes, float noteSeconds, float volume)
    {
        int per = Mathf.CeilToInt(noteSeconds * SampleRate);
        float[] data = new float[per * notes.Length + per * 2];
        for (int n = 0; n < notes.Length; n++)
        {
            float phase = 0f;
            for (int i = 0; i < per * 3 && n * per + i < data.Length; i++)
            {
                phase += 2f * Mathf.PI * notes[n] / SampleRate;
                float t = (float)i / (per * 3);
                float env = Mathf.Clamp01(i / (0.004f * SampleRate)) * Mathf.Exp(-6f * t);
                data[n * per + i] += Mathf.Sin(phase) * volume * env;
            }
        }
        return MakeClip("seq", data);
    }

    static AudioClip Noise(float seconds, float volume)
    {
        int count = Mathf.CeilToInt(seconds * SampleRate);
        float[] data = new float[count];
        float low = 0f;
        System.Random rng = new System.Random(7);
        for (int i = 0; i < count; i++)
        {
            float t = (float)i / count;
            float white = (float)(rng.NextDouble() * 2.0 - 1.0);
            float k = Mathf.Lerp(0.05f, 0.5f, t);
            low += (white - low) * k;
            data[i] = low * volume * Mathf.Sin(Mathf.PI * t);
        }
        return MakeClip("noise", data);
    }

    static AudioClip MakeMusic()
    {
        float[] scale = { 261.6f, 293.7f, 329.6f, 392f, 440f, 523.3f, 587.3f, 659.3f };
        int[] pattern = { 0, 2, 4, 3, 5, 4, 2, 3, 0, 2, 4, 6, 7, 6, 4, 2 };
        float noteSeconds = 0.3f;
        int per = Mathf.CeilToInt(noteSeconds * SampleRate);
        float[] data = new float[per * pattern.Length];
        for (int n = 0; n < pattern.Length; n++)
        {
            float freq = scale[pattern[n]];
            float phase = 0f;
            for (int i = 0; i < per * 2 && n * per + i < data.Length; i++)
            {
                phase += 2f * Mathf.PI * freq / SampleRate;
                float t = (float)i / (per * 2);
                float env = Mathf.Clamp01(i / (0.01f * SampleRate)) * Mathf.Exp(-3.5f * t);
                data[n * per + i] += (Mathf.Sin(phase) + 0.25f * Mathf.Sin(phase * 2f)) * 0.22f * env;
            }
            if (n % 4 == 0)
            {
                float bass = scale[0] * 0.5f;
                float bp = 0f;
                for (int i = 0; i < per * 4 && n * per + i < data.Length; i++)
                {
                    bp += 2f * Mathf.PI * bass / SampleRate;
                    float t = (float)i / (per * 4);
                    data[n * per + i] += Mathf.Sin(bp) * 0.18f * Mathf.Exp(-2f * t);
                }
            }
        }
        return MakeClip("music", data);
    }

    static AudioClip MakeClip(string name, float[] data)
    {
        AudioClip clip = AudioClip.Create(name, data.Length, 1, SampleRate, false);
        clip.SetData(data, 0);
        return clip;
    }
}
