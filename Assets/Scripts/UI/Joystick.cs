using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Survivors.Input
{
	public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		[SerializeField]
		private Image m_Stick;
		[SerializeField]
		private RectTransform m_StickParent;

		private readonly Subject<Vector2> _onInput = new();
		private bool _isDragging;

		public IObservable<Vector2> OnInput => _onInput;

		private void Update()
		{
			if (_isDragging)
			{
				var stickPosition = m_StickParent.InverseTransformPoint(UnityEngine.Input.mousePosition);
				var stickParentRect = m_StickParent.rect;

				var radius = stickParentRect.width / 2;
				var distance = stickPosition.magnitude;
				if (distance > radius)
				{
					stickPosition = stickPosition.normalized * radius;
				}

				m_Stick.rectTransform.localPosition = stickPosition;
				_onInput.OnNext(m_Stick.rectTransform.localPosition / radius);
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			_isDragging = true;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			ResetStick();
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			ResetStick();
		}

		private void ResetStick()
		{
			_isDragging = false;
			m_Stick.rectTransform.localPosition = Vector2.zero;
		}
	}
}