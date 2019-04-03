using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Interfaces;
using NeuralBotMasterFramework.Models;

namespace NeuralBotMasterFramework.Models
{
    public class WeightedLayer : IWeightedLayer
    {
        public IWeightedNode[] Nodes { get; set; }

        public WeightedLayer() { }

        public WeightedLayer(int totalNodes, int previousNodes)
        {
            InitializeNodes(totalNodes, previousNodes);
        }

        private void InitializeNodes(int totalNodes, int previousNodes)
        {
            Nodes = new IWeightedNode[totalNodes];
            for (int i = 0; i < Nodes.Length; ++i)
            {
                Nodes[i] = new WeightedNode(previousNodes);
            }
        }

        public void SetValues(double[] input)
        {
            if (ArraysAreSameLength(input.Length))
            {
                SetInput(input);
            }
        }

        private bool ArraysAreSameLength(int length)
        {
            return Nodes.Length == length;
        }

        private void SetInput(double[] input)
        {
            for (int i = 0; i < input.Length; ++i)
            {
                Nodes[i].SetValue(input);
            }
        }

        public double[] GetValues()
        {
            double[] returnValues = new double[Nodes.Length];
            for(int i = 0; i < returnValues.Length; ++i)
            {
                returnValues[i] = Nodes[i].Value;
            }
            return returnValues;
        }
    }
}
