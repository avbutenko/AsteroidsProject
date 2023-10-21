using Newtonsoft.Json;
using System.Collections.Generic;

namespace AsteroidsProject.GameLogic.Core
{
    public struct COnOutOfLevel
    {
        //[JsonConverter(typeof(ComponentListJsonConverter))]
        public List<object> Components;
    }
}