using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralBotMasterFramework.Interfaces
{
    public interface IWeightedNode : INode
    {
        decimal Weight { get; set; }
        void ApplyWeight();
    }
}
