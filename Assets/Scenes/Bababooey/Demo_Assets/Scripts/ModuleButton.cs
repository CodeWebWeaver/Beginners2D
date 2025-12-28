using UnityEngine;
using UnityEngine.UI;

public class ModuleButton : MonoBehaviour
{
    public Image icon;
    public Text nameText;
    public Text descriptionText;

    ModuleData module;

    public void Setup(ModuleData data)
    {
        module = data;
        icon.sprite = data.icon;
        nameText.text = data.moduleName;
        descriptionText.text = data.description;
    }

    public void OnClick()
    {
        ModuleRewardManager.Instance.OnModuleChosen(module);
    }
}
