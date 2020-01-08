using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron
{
    public float value = 0;
    public float weight;
    public float[] biases;

    public Neuron(float weight, float[] biases){
        this.weight  = weight;
        this.biases  = biases;
    }

    public Neuron(int length){
        this.weight  = Random.Range(-1,1);
        biases = new float[length];
        for (int i = 0; i < length; i++)
        {
            this.biases[i] = Random.Range(-1,1);
        }
    }

    public Neuron(){
        this.weight  = Random.Range(-1,1);
    }

    public void Learn(float val, int chance, Neuron[] previousNeurons){
        for (int i = 0; i < biases.Length; i++)
        {
            if((UnityEngine.Random.Range(0f, chance) <= 5)) biases[i] += Random.Range(-val, val);
        }

        if((UnityEngine.Random.Range(0f, chance) <= 5)) weight += Random.Range(-val, val);
        

        SetValue(previousNeurons);
    }

    public void SetValue(Neuron[] previousWeights){
        value = 0;
        for (int i = 0; i < biases.Length; i++)
        {
            value += previousWeights[i].value * biases[i];
        }
        value *= weight;
        value = (float)System.Math.Tanh(value);
    }
}