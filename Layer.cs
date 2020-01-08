using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer 
{
    public Neuron[] neurons;
    private int length = 0;
    public int Length { get => length;}

    public Layer(int neurons){
        this.neurons = new Neuron[neurons];
        length = neurons;
        SetNeurons(neurons);
    }   

    public Layer(int neurons, int biases){
        this.neurons = new Neuron[neurons];
        length = neurons;
        SetNeurons(neurons, biases);
    }

    public void SetNeurons(int neurons){
        for (int i = 0; i < neurons; i++)
        {
            this.neurons[i] = new Neuron();
        } 
    }

    public void SetNeurons(int neurons, int biases){
        for (int i = 0; i < neurons; i++)
        {
            this.neurons[i] = new Neuron(biases);
        } 
    }

    public void FeedNeurons(Layer previousLayer){
        for (int i = 0; i < neurons.Length; i++)
        {
            neurons[i].SetValue(previousLayer.neurons);
        } 
    }
}
