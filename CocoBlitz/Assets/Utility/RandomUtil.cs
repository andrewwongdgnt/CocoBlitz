using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUtil : MonoBehaviour {

	public static float nonUniformDistributedRandom(float min, float max, Func<float,float> distributionModel)
    {
        float x = UnityEngine.Random.Range(0.0f, 1.0f);

        float y = distributionModel(x);

        // With 1-(x-1)^4, it has a value of 1 when x = 1, a value of 0 when x = 0, but is on average more likely to be high than low.
        return min + y * (max - min);
    }
}
