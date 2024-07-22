using R3;
using VContainer.Unity;

namespace Dungeon.UserInterface
{
    public class CharacterSelectionPresenter : IPostInitializable
    {
        private readonly CharacterSelectionModel _model;
        private readonly CharacterSelectionView _view;

        public CharacterSelectionPresenter(CharacterSelectionModel model, CharacterSelectionView view)
        {
            _model = model;
            _view = view;
        }

        // 初期化処理
        public void PostInitialize()
        {
            BindModelAndView();
        }

        /// <summary>  Model と View を接続します </summary>
        private void BindModelAndView()
        {
            // ------ [ Model -> View ]

            // Model のキャラクター選択情報が更新された場合、View のキャラクター選択UIを再構成します
            _model.SelectionEvent
                .Subscribe(datas =>
                {
                    _view.UpdateCharacterSelect(datas);
                });

            // Model が有効化 Event を発火した場合、View を表示します
            _model.EnableEvent
                .Subscribe(_ => _view.Show()).AddTo(_view);

            // Model が無効化 Event を発火した場合、View を非表示にします
            _model.DisableEvent
                .Subscribe(_ => _view.Hide()).AddTo(_view);

            // ------ [ View -> Model ]

            // View が決定 Event を発火した場合、Model のキャラクター選択を決定するメソッドを呼び出します
            _view.OnConfirmButton
                .Subscribe(data => _model.OnClickConfirm(data)).AddTo(_view);
        }
    }
}


