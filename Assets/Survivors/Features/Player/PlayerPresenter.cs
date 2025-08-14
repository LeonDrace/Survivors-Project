using System;
using Survivors.Features.Contracts;
using Survivors.Features.UI.Controls;
using UniRx;
using UnityEngine;

namespace Survivors.Features.Player
{
    public class PlayerPresenter : IPlayerTransformData
    {
        private readonly PlayerView _view;
        private readonly PlayerModel _model;

        public Transform Transform => _view.transform;

        public PlayerPresenter(
            Joystick joystick,
            PlayerView playerView,
            PlayerModel playerModel,
            CompositeDisposable disposables)
        {
            _model = playerModel;
            _view = playerView;

            _model.CurrentHealth
                .Where(x => x <= 0)
                .Subscribe(_ => _model.IsDead.Value = true)
                .AddTo(disposables);

            _model.CurrentHealth
                .Subscribe(x => _model.CurrentHealthPercentage.Value = x / _model.BaseHealth)
                .AddTo(disposables);

            //Move
            joystick.OnInput
                .TakeWhile(_ => !_model.IsDead.Value)
                .Subscribe(playerView.Move)
                .AddTo(disposables);

            //Damage
            _model.CurrentHealthPercentage
                .Subscribe(x =>
                {
                    if (x < 1)
                    {
                        _view.DamageRenderer.enabled = true;

                        Observable
                            .Timer(TimeSpan.FromSeconds(_model.DamageFlickerDuration))
                            .Subscribe(x => { _view.DamageRenderer.enabled = false; })
                            .AddTo(disposables);
                    }
                })
                .AddTo(disposables);

            //Current health
            _model.CurrentHealthPercentage
                .Subscribe(x => _view.HealthSlider.value = x);
        }
    }
}