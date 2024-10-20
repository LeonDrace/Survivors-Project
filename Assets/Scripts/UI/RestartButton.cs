
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
	[SerializeField]
	private Button _button;
	[SerializeField]
	private string _sceneName = "SampleScene";

	private void Awake()
	{
		_button
			.OnClickAsObservable()
			.Subscribe(_ =>
			{
				SceneManager.LoadScene(_sceneName);
				_button.interactable = false;
			});
	}
}
