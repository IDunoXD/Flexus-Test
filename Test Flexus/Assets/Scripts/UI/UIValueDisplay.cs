using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIValueDisplay : MonoBehaviour
{
	[SerializeField] private Slider slider;
	[SerializeField] private TMP_Text text;
	[SerializeField] private float valueMultiplier = 1;

	private void OnEnable()
	{
		slider.onValueChanged.AddListener(UpdateValue);
	}

	private void OnDisable()
	{
		slider.onValueChanged.RemoveListener(UpdateValue);
	}
	
	private void UpdateValue(float value)
	{
		text.text = (value * valueMultiplier).ToString("0");
	}
}

