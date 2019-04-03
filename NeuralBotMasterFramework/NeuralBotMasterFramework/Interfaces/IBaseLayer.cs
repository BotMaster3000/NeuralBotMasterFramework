using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralBotMasterFramework.Interfaces
{
    public interface IBaseLayer
    {
        void SetValues(double[] input);
        double[] GetValues();
    }
}
