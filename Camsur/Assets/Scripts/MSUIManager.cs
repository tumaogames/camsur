using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MSUIManager : MonoBehaviour
{
    public TMP_Text register;
    public GameObject loginMenu;
    public GameObject registerMenu;
    public TMP_Text LoginErrorMessage;
    public TMP_Text RegisterErrorMessage;
    public Image LoginLoadingImage;
    public Image RegisterLoadingImage;

    public void ShowRegisterMenu()
    {
        registerMenu.SetActive(true);
        loginMenu.SetActive(false);
    }

    public void ShowLoginMenu()
    {
        registerMenu.SetActive(false);
        loginMenu.SetActive(true);
    }

    public void ShowLoginLoadingImage(bool data = true)
    {
        LoginLoadingImage.gameObject.SetActive(data);
    }

    public void ShowRegisterLoadingImage(bool data = true)
    {
        RegisterLoadingImage.gameObject.SetActive(data);
    }
}
