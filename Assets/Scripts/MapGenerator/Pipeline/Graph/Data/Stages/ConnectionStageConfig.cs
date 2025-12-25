using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ConnectionStageConfig", menuName = "Graph Generation/Stages/Connection Config")]
public class ConnectionStageConfig : GraphStageConfig {
    public float connectionProbability = 0.3f;
    public int maxConnectionsPerNode = 4;

    public override Type RuntimeType => typeof(ConnectionStage);
}
