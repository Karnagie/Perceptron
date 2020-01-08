using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Network
{
    public Layer[] layers;
    public float streight = 0.1f;
    public float chance = 0.05f;
    public float position = 0;

    public Network(int[] neurons, int input){

        this.layers = new Layer[neurons.Length+1];
        InitLayers(neurons, input);

    }

    void InitLayers(int[] neurons, int input){
        layers[0] = new Layer(input);
        for (int i = 1; i < layers.Length; i++)
        {
            this.layers[i] = new Layer(neurons[i-1], layers[i-1].Length);
        }
    }

    public void Save(string path){
        File.Create(path).Close();
        StreamWriter writer = new StreamWriter(path, true);

        for (int i = 1; i < layers.Length; i++)
        {
            for (int j = 0; j < layers[i].Length; j++)
            {
                writer.WriteLine(layers[i].neurons[j].weight);
                for (int k = 0; k < layers[i].neurons[j].biases.Length; k++)
                {
                    writer.WriteLine(layers[i].neurons[j].biases[k]);
                }
            }
        }
        writer.Close();
    }

    public void Load(string path){
        TextReader tr = new StreamReader(path);
        int NumberOfLines = (int)new FileInfo(path).Length;
        string[] listLines = new string[NumberOfLines];
        int index = 1;

        for (int i = 1; i < NumberOfLines; i++)
        {
            listLines[i] = tr.ReadLine();
        }
        tr.Close();

        if(new FileInfo(path).Length > 0){
            for (int i = 1; i < layers.Length; i++)
            {
                for (int j = 0; j < layers[i].Length; j++)
                {
                    layers[i].neurons[j].weight = float.Parse(listLines[index]);
                    index++;
                    for (int k = 0; k < layers[i].neurons[j].biases.Length; k++)
                    {
                        layers[i].neurons[j].biases[k] = float.Parse(listLines[index]);
                        index++;
                    }
                }
            }
        }
    }

    public void Learn(float streight, int chance){
        for (int i = 1; i < layers.Length; i++)
        {
            for (int j = 0; j < layers[i].Length; j++)
            {
                layers[i].neurons[j].Learn(streight, chance, layers[i-1].neurons);
            }
        }
    }
    
    public float[] GetOutputs(float[] input){

        for (int i = 0; i < input.Length; i++)
        {
            layers[0].neurons[i].value = input[i];
        }

        for (int i = 1; i < layers.Length; i++)
        {
            layers[i].FeedNeurons(layers[i-1]);
        }

        float[] outputs = new float[layers[layers.Length-1].neurons.Length];
        for (int i = 0; i < layers[layers.Length-1].neurons.Length; i++)
        {
            outputs[i] = layers[layers.Length-1].neurons[i].value;
        }

        return outputs;
    }

    public Network Copy(int[] neurons, int input){
        Network CopyNetwork = new Network(neurons, input);

        for (int i = 1; i < layers.Length; i++)
        {
            for (int j = 0; j < layers[i].Length; j++)
            {
                CopyNetwork.layers[i].neurons[j].weight = layers[i].neurons[j].weight;
                for (int k = 0; k < layers[i].neurons[j].biases.Length; k++)
                {
                    CopyNetwork.layers[i].neurons[j].biases[k] = layers[i].neurons[j].biases[k];
                }
            }
        }
        
        return CopyNetwork;
    }

}