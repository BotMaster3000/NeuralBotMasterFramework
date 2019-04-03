using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Interfaces;

namespace NeuralBotMasterFramework.Models
{
    public class WeightedNode : IWeightedNode
    {
        public double[] Weights { get; set; }
        public double Value { get; set; }

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
            for (int i = 0; i < unweightedValues.Length; ++i)
            {
                Value += unweightedValues[i] * Weights[i];
            }
        }
    }
}
