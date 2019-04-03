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
            InitializeHiddenNodes(hiddenLayers, hiddenNodesPerLayer);
            InitializeOutputNodes(outputNodes);
        }

        private void InitializeInputNodes(int inputNodes)
        {
            InputLayer = new Layer(inputNodes);
            throw new NotImplementedException();
        }

        private void InitializeHiddenNodes(int hiddenLayers, int hiddenNodesPerLayer)
        {
            throw new NotImplementedException();
        }

        private void InitializeOutputNodes(int outputNodes)
        {
            throw new NotImplementedException();
        }

        public void SetInput()
        {
            throw new NotImplementedException();
        }

        public double[] GetOutput()
        {
            throw new NotImplementedException();
        }

        public void Propagate()
        {
            throw new NotImplementedException();
        }

    }
}
