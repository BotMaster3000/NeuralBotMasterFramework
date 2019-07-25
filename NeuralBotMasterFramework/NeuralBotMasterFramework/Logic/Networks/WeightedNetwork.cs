using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Interfaces;
using NeuralBotMasterFramework.Models;
using Newtonsoft.Json;
using NeuralBotMasterFramework.Helper;

namespace NeuralBotMasterFramework.Logic.Networks
{
    public class WeightedNetwork : IWeightedNetwork, ISaveableNetwork
    {
        [JsonConverter(typeof(ConcreteTypeConverter<Layer>))]
        public ILayer InputLayer { get; set; }

        [JsonConverter(typeof(ConcreteTypeConverter<WeightedLayer[]>))]
        public IWeightedLayer[] HiddenLayers { get; set; }

        [JsonConverter(typeof(ConcreteTypeConverter<WeightedLayer>))]
        public IWeightedLayer OutputLayer { get; set; }

        public int ID { get; set; }

        // Required for JSON-Deserialization
        public WeightedNetwork() { }

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
            for (int i = 0; i < totalHiddenLayers; ++i)
            {
                HiddenLayers[i] = new WeightedLayer(totalHiddenNodesPerLayer, previousNodes);
                previousNodes = HiddenLayers[i].Nodes.Length;
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
            double[] values = InputLayer.GetValues();
            for (int i = 0; i < HiddenLayers.Length; ++i)
            {
                HiddenLayers[i].SetValues(values);
                values = HiddenLayers[i].GetValues();
            }
            OutputLayer.SetValues(values);
        }

        public string SaveNetwork()
        {
            return JsonConvert.SerializeObject(this);
        }

        public ISaveableNetwork LoadNetwork(string networkString)
        {
            return (ISaveableNetwork)JsonConvert.DeserializeObject(networkString, typeof(WeightedNetwork));
        }
    }
}
