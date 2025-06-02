using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public ObservableProperty<bool> IsAiming = new ObservableProperty<bool>(false);
}
