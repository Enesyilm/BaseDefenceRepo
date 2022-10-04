using System;
using System.Collections.Generic;
using Data.ValueObjects.GridData;
using Enum;
using Sirenix.OdinInspector;

namespace Data
{
    [Serializable]
            public class StackData
            {
                public StackingSystem StackingSystem;
        
                [ShowIf("StackingSystem",StackingSystem.Static)]
                public List<GridData> StaticGridDatas;
        
                [ShowIf("StackingSystem",StackingSystem.Dynamic)]
                public List<GridData> DynamicGridDatas;
            }
}