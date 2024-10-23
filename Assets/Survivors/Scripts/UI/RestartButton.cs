
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
	[SerializeField]
	private Button m_Button;
	[SerializeField]
	private string m_SceneName = "SampleScene";

	private void Awake()
	{
		m_Button
			.OnClickAsObservable()
			.Subscribe(_ =>
			{
				SceneManager.LoadScene(m_SceneName);
				m_Button.interactable = false;
			});
	}
}
