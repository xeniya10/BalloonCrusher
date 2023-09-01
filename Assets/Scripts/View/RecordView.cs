namespace BalloonCrusher.View
{
using UnityEngine;
using UnityEngine.UI;
using System;
using Model;

public class RecordView : AbstractButtonView
{
    [SerializeField] private Text _scoreText = default;
    [SerializeField] private Text _nameText = default;
    [SerializeField] private InputField _renamingInputField = default;
    [SerializeField] private int _inputCharacterLimit = 3;
    
    private Record _data = default;

    public event Action<string, string> onSubmitRenaming = delegate {};

    public void Init(Record data)
    {
        _data = data;
        _nameText.text = data.Name.ToUpper();
        _scoreText.text = data.Score.ToString();
    }

    protected override void Awake()
    {
        base.Awake();

        _renamingInputField.characterLimit = _inputCharacterLimit;
        _renamingInputField.onSubmit.AddListener(OnSubmitRename);
    }

    private void OnSubmitRename(string newName)
    {
        _nameText.gameObject.SetActive(true);
        _renamingInputField.gameObject.SetActive(false);
        _nameText.text = newName.ToUpper();
        onSubmitRenaming(_data.ID, newName.ToUpper());
    }

    protected override void OnButtonClick()
    {
        _nameText.gameObject.SetActive(false);
        _renamingInputField.gameObject.SetActive(true);
        _renamingInputField.text = _nameText.text;
        _renamingInputField.Select();
    }
}
}