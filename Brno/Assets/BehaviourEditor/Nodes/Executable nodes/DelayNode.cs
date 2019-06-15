using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BehaviourTreeEditor
{
    [CreateAssetMenu(menuName ="BehaviourEditor/Nodes/Delay")]
    public class DelayNode : ExecutableNode
    {
        public override void DrawCurve(BaseNode b)
        {
        }

        public override void DrawWindow(BaseNode b)
        {


            EditorGUILayout.LabelField("delay:(float)/s-1");
            b.delay = EditorGUILayout.FloatField(b.delay);
        }

        public override void Execute(BaseNode b)
        {
            if(b.timer == null)
            {
                b.timer = new _Timer(1.0f,1.0f,b.Graph.character);

                b.timer.OnUpdate += () =>
                {
                    Debug.Log("Time: " + b.timer.ElapsedTimeF);

                    if (b.timer.ElapsedTimeF == b.delay)
                    {
                        b.nodeCompleted = true;
                        b.timer.Stop();
                        b.timer = null;


                    }

                };
                b.timer.Start();
            }
        }
    }
}