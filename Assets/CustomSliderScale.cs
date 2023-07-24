using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class CustomSliderScale{
    public static float ScaleSliderValue(float sliderVal){

        float exponentialValue;
        if (sliderVal <= 0.5f)
        {
            // Slider value is in the first half of the range, so lerp between min and 1
            exponentialValue = Mathf.Lerp(0.5f, 1f, sliderVal * 2); // Multiply by 2 to map 0-0.5 range to 0-1
        }
        else
        {
            // Slider value is in the second half of the range, so lerp between 1 and max
            exponentialValue = Mathf.Lerp(1f, 2f, (sliderVal - 0.5f) * 2); // Subtract 0.5 and multiply by 2 to map 0.5-1 range to 0-1
        }
        return exponentialValue;
    }
}