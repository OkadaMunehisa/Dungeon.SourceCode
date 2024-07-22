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

        // ����������
        public void PostInitialize()
        {
            BindModelAndView();
        }

        /// <summary>  Model �� View ��ڑ����܂� </summary>
        private void BindModelAndView()
        {
            // ------ [ Model -> View ]

            // Model �̃L�����N�^�[�I����񂪍X�V���ꂽ�ꍇ�AView �̃L�����N�^�[�I��UI���č\�����܂�
            _model.SelectionEvent
                .Subscribe(datas =>
                {
                    _view.UpdateCharacterSelect(datas);
                });

            // Model ���L���� Event �𔭉΂����ꍇ�AView ��\�����܂�
            _model.EnableEvent
                .Subscribe(_ => _view.Show()).AddTo(_view);

            // Model �������� Event �𔭉΂����ꍇ�AView ���\���ɂ��܂�
            _model.DisableEvent
                .Subscribe(_ => _view.Hide()).AddTo(_view);

            // ------ [ View -> Model ]

            // View ������ Event �𔭉΂����ꍇ�AModel �̃L�����N�^�[�I�������肷�郁�\�b�h���Ăяo���܂�
            _view.OnConfirmButton
                .Subscribe(data => _model.OnClickConfirm(data)).AddTo(_view);
        }
    }
}


