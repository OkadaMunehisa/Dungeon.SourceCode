using R3;
using VContainer.Unity;

namespace Dungeon.UserInterface
{
    public class SessionCreatePresenter : IPostInitializable
    {
        private readonly SessionCreateModel _model;
        private readonly SessionCreateView _view;

        public SessionCreatePresenter(SessionCreateModel model, SessionCreateView view)
        {
            _model = model;
            _view = view;
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

            _view.OnConfirmButton
                .Subscribe(_ => _model.OnClickConfirm()).AddTo(_view);

            _view.OnCloseButton
                .Subscribe(_ => _model.OnClickClose()).AddTo(_view);

            _view.NameEntryInputField
                .Subscribe(_ => _model.SetSessionName(_)).AddTo(_view);
        }
    }
}
