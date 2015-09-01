using UnityEngine;
using System.Collections;
using NeuralNetwork;

public class NeuralNetworkTest : MonoBehaviour
{

    NeuralNetwork.NeuralNetwork network;
    NeuralNetwork.LearningAlgorithm learning;

    public int[] layers;
    public float[] input;
    public float[] output;
    public bool genetic = true;
    public int testCases = 1000;

    [ContextMenu("Create")]
    void Create()
    {
        network = new NeuralNetwork.NeuralNetwork(input.Length, layers);
        if (genetic)
            learning = new GeneticLearningAlgorithm(network);
        else
            learning = new BackPropagationLearningAlgorithm(network);
        network.randomizeAll();
        network.LearningAlg = learning;

    }

    [ContextMenu("Output")]
    public void Output()
    {
        output = network.Output(input);
    }

    [ContextMenu("Learn")]
    public void Learn()
    {
        int testCount = testCases;
        float[][] inputs = new float[testCount][];
        float[][] outputs = new float[testCount][];
        for (int a = 0; a < testCount; ++a)
        {
            inputs[a] = new float[input.Length];
            for (int b = 0; b < inputs[a].Length; ++b)
            {
                inputs[a][b] = Random.value;
            }
            outputs[a] = new float[output.Length];
            for (int b = 0; b < outputs[a].Length; ++b)
            {
                outputs[a][b] = inputs[a][b];
                for (int c = 0; c < inputs[a].Length; ++c)
                {
                    if (c == b) continue;
                    outputs[a][b] -= inputs[a][c] / 3f;
                }
            }
        }

        float startTime = Time.realtimeSinceStartup;
        learning.Learn(inputs, outputs);
        Debug.Log("Learning complete: " + (Time.realtimeSinceStartup - startTime));
    }
}
