using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Interfaces;
using NeuralBotMasterFramework.Helper;

namespace NeuralBotMasterFramework.Models
{
    public class WeightedNode : IWeightedNode
    {
        public double[] Weights { get; set; }
        public double Value { get; set; }

        public WeightedNode() { }

        public WeightedNode(int previousLayerNodes)
        {
            InitializeWeights(previousLayerNodes);
        }

        private void InitializeWeights(int totalWeights)
        {
            Weights = new double[totalWeights];
            for(int i = 0; i < Weights.Length; ++i)
            {
                Weights[i] = RandomNumberGenerator.GetNextDouble();
            }
        }

        public void SetValue(double[] unweightedValues)
        {
            if (ArraysAreEqualLengths(unweightedValues.Length))
            {
                ResetValue();
                WeightValues(unweightedValues);
            }
        }

        private bool ArraysAreEqualLengths(int length)
        {
            return length == Weights.Length;
        }

        private void ResetValue()
        {
            Value = 0.0;
        }

        private void WeightValues(double[] unweightedValues)
        {
            double[] weights = Weights;
            for (int i = 0; i < unweightedValues.Length; ++i)
            {
                Value += unweightedValues[i] * weights[i];
            }
        }
    }
}
