using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralBotMasterFramework.Interfaces;
using NeuralBotMasterFramework.Models;

namespace NeuralBotMasterFramework.Models
{
    public class Layer : ILayer
    {
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
    }
}
