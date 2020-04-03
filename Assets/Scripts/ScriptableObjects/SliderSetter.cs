using UnityEngine.UI;
using UnityEngine;

[ExecuteInEditMode]
public class SliderSetter : MonoBehaviour
{
    public Slider Slider;
    public FloatVariable Variable;

    void Start()
    {
        Slider = GetComponent<Slider>();
    }
    private void Update()
    {
        if (Slider != null && Variable != null)
            Slider.value = Variable.Value;
    }
}