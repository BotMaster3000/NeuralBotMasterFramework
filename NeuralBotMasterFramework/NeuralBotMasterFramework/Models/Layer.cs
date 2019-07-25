using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Interfaces;
using NeuralBotMasterFramework.Models;
using Newtonsoft.Json;
using NeuralBotMasterFramework.Helper;

namespace NeuralBotMasterFramework.Models
{
    public class Layer : ILayer
    {
        [JsonConverter(typeof(ConcreteTypeConverter<Node[]>))]
        public INode[] Nodes { get; set; }

        public Layer() { }

        public Layer(int totalNodes)
        {
            InitializeNodes(totalNodes);
        }

        private void InitializeNodes(int totalNodes)
        {
            Nodes = new INode[totalNodes];
            for (int i = 0; i < Nodes.Length; i++)
            {
                Nodes[i] = new Node();
            }
        }

        public void SetValues(double[] input)
        {
            if (ArraysAreOfSameLength(input.Length))
            {
                SetInput(input);
            }
        }

        private bool ArraysAreOfSameLength(int length)
        {
            return Nodes.Length == length;
        }

        private void SetInput(double[] input)
        {
            for(int i = 0; i < input.Length; ++i)
            {
                Nodes[i].Value = input[i];
            }
        }

        public double[] GetValues()
        {
            double[] results = new double[Nodes.Length];
            for(int i = 0; i < results.Length; ++i)
            {
                results[i] = Nodes[i].Value;
            }
            return results;
        }
    }
}
