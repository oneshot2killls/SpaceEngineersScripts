using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {
        #region Custom Conition Action     
        public class CustomConditionAction<ConditionParameterType, CallbackParameterType>
        {
            public Action<CallbackParameterType> Callback;
            public Func<ConditionParameterType, bool> Condition;            
            public bool ShouldRepeat;
            public bool HasActivated
            { get; private set; }

            public CustomConditionAction(Func<ConditionParameterType, bool> condition, bool shouldRepeat, Action<CallbackParameterType> callback)
            {
                this.Condition = condition;
                this.Callback = callback;
                this.ShouldRepeat = shouldRepeat;
            }

            public void TriggerWithParameter(ConditionParameterType conditionParameter, CallbackParameterType callbackParameter)
            {
                if (!HasActivated)
                {
                    if (Condition.Invoke(conditionParameter))
                    {
                        HasActivated = true;
                        Callback.Invoke(callbackParameter);
                        return;
                    }                  
                }              
                if (ShouldRepeat)
                {
                    if (Condition.Invoke(conditionParameter))
                    {
                        Callback.Invoke(callbackParameter);
                        return;
                    }
                }               
            }   
            
            public void Reset()
            {
                HasActivated = false;
            }
        }
        #endregion Custom Condition Action
    }
}
