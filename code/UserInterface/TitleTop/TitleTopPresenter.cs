using R3;
using VContainer.Unity;

namespace Dungeon.UserInterface
{
    public class TitleTopPresenter : IPostInitializable
    {
        private readonly ITitleTopModel _model;
        private readonly TitleTopView _view;

        public TitleTopPresenter(ITitleTopModel titleTopModel, TitleTopView titleTopView)
        {
            _model = titleTopModel;
            _view = titleTopView;
        }

        public void PostInitialize()
        {
            Bind();
        }

        private void Bind()
        {
            // ------ [ Model -> View ]

            _model.EnableEvent
                .Subscribe(_ => _view.Show()).AddTo(_view);

            _model.DisableEvent
                .Subscribe(_ => _view.Hide()).AddTo(_view);

            // ------ [ View -> Model ]

            _view.OnStartButton
                .Subscribe(button =>
                {
                    _model.OnClickStart();
                }).AddTo(_view);

            _view.OnSettingButton
                .Subscribe(button =>
                {
                    _model.OnClickSetting();
                }).AddTo(_view);

            _view.OnCloseButton
                .Subscribe(_ => _model.OnClickClose()).AddTo(_view);

        }
    }
}
