using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioControls : MonoBehaviour {

	[System.Serializable]
	private class AudioChannel {
		[SerializeField] public AudioMixerGroup channel;
		[SerializeField] public Slider slider;
	}

	[Header("Audio References")]
	[SerializeField] private AudioMixer audioMixer;
	[SerializeField] private AudioChannel[] audioChannels;

	private void Start() {
		foreach (AudioChannel channel in audioChannels) {
			audioMixer.GetFloat($"{channel.channel.name}Volume", out float volume);
			channel.slider.value = volume;
		}
	}

	public void OnValueUpdate() {
		foreach (AudioChannel channel in audioChannels) {
			
			audioMixer.SetFloat($"{channel.channel.name}Volume", channel.slider.value);
		}
	}

}
