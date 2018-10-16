using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class FlexibleUIButton : FlexibleUI {
//     public enum ButtonTypes
//     {
//         Default,
//         Confirm,
//         Decline,
//         Warning
//     }

    Image _im;
    Image _Icon;
    Button _bu;
// 
//     public ButtonTypes buttonType;

    protected override void OnSkinUI()
    {
        base.OnSkinUI();

        _im = GetComponent<Image>();
        //_Icon = transform.Find("Icon").GetComponent<Image>();
        _bu = GetComponent<Button>();

        _bu.transition = Selectable.Transition.SpriteSwap;
        _bu.targetGraphic = _im;

        _im.sprite = skinData.ButtonSprite;
        _im.color = skinData.SpriteColorMutliply;
        _bu.spriteState = skinData.ButtonSpriteState;
        Debug.Log("update");

// 
//         switch (buttonType)
//         {
//             case ButtonTypes.Confirm:
//                 _im.color = skinData.confirmColor;
//                 _Icon.sprite = skinData.confirmIcon;
//                 break;
//             case ButtonTypes.Decline:
//                 _im.color = skinData.declineColor;
//                 _Icon.sprite = skinData.declineIcon;
//                 break;
//             case ButtonTypes.Default:
//                 _im.color = skinData.DefaultColor;
//                 _Icon.sprite = skinData.DefaultIcon;
//                 break;
//             case ButtonTypes.Warning:
//                 _im.color = skinData.warningColor;
//                 _Icon.sprite = skinData.warningIcon;
//                 break; 
//         }
    }

}
