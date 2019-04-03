using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Interfaces;
using NeuralBotMasterFramework.Models;

namespace NeuralBotMasterFramework.Logic
{
    public class WeightedNetwork : IWeightedNetwork
    {
        public ILayer InputLayer { get; set; }
        public IWeightedLayer[] HiddenLayers { get; set; }
        public IWeightedLayer OutputLayer { get; set; }

        public WeightedNetwork(int inputNodes, int hiddenLayers, int hiddenNodesPerLayer, int outputNodes)
        {
            InitializeInputNodes(inputNodes);
            InitializeHiddenNodes(hiddenLayers, hiddenNodesPerLayer, inputNodes);
            InitializeOutputNodes(outputNodes, hiddenNodesPerLayer);
        }

        private void InitializeInputNodes(int inputNodes)
        {
            InputLayer = new Layer(inputNodes);
        }

        private void InitializeHiddenNodes(int totalHiddenLayers, int totalHiddenNodesPerLayer, int previousNodes)
        {
            HiddenLayers = new IWeightedLayer[totalHiddenLayers];
            for(int i = 0; i < totalHiddenLayers; ++i)
            {
                HiddenLayers[i] = new WeightedLayer(totalHiddenNodesPerLayer, previousNodes);
            }
        }

        private void InitializeOutputNodes(int outputNodes, int previousNodes)
        {
            OutputLayer = new WeightedLayer(outputNodes, previousNodes);
        }

        public void SetInput(double[] input)
        {
            InputLayer.SetValues(input);
        }

        public double[] GetOutput()
        {
            return OutputLayer.GetValues();
        }

        public void Propagate()
        {
            throw new NotImplementedException();
        }

    }
}
