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

        public WeightedLayer(int totalNodes)
        {
            InitializeNodes(totalNodes);
        }

        private void InitializeNodes(int totalNodes)
        {
            Nodes = new IWeightedNode[totalNodes];
            for (int i = 0; i < Nodes.Length; ++i)
            {
                Nodes[i] = new WeightedNode();
            }
        }
    }
}
