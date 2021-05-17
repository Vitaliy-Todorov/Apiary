using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProgressBar
{
    void MaxValue(float maxValue);

    void SetProgress(float health);
}
