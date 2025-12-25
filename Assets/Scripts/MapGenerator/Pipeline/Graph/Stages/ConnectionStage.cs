using System.Collections.Generic;
using UnityEngine;

public class ConnectionStage : IPipelineStage<GraphGenerationContext> {
    private readonly ConnectionStageConfig config;
    private readonly IRandomService randomService;
    public string StageName => "Connection Creation";

    public ConnectionStage(ConnectionStageConfig config, IRandomService randomService) {
        this.config = config;
        this.randomService = randomService;
    }

    public void Execute(GraphGenerationContext context) {
        Debug.Log("Connecting nodes...");
        Graph graph = context.Graph;
        MapGenerationData settings = context.Config;

        List<List<GraphNode>> levelNodes = graph.GetLevelNodes();

        // Основний прохід для створення зв'язків
        for (int level = 0; level < levelNodes.Count; level++) {
            List<GraphNode> currentLevel = levelNodes[level];
            List<GraphNode> nextLevel = level < levelNodes.Count - 1 ? levelNodes[level + 1] : new List<GraphNode>();

            foreach (var currentNode in currentLevel) {
                // Створення зв'язків на наступний рівень
                if (nextLevel.Count > 0) {
                    List<GraphNode> potentialNextConnections = GetNearbyNodes(currentNode, currentLevel, nextLevel);
                    EnsureConnections(currentNode, potentialNextConnections, settings);
                }

                // Перевірка зв'язків з попереднім рівнем (для рівнів > 0)
                if (level >= 1 && !currentNode.HasConnectionsToPrevLevel()) {
                    List<GraphNode> prevLevel = levelNodes[level - 1];
                    List<GraphNode> prevPotentialConnections = GetNearbyNodes(currentNode, currentLevel, prevLevel);
                    CreateMinimalConnection(currentNode, prevPotentialConnections);
                }
            }
        }


        context.SetData("ConnectionStageComplete", true);
    }


    private List<GraphNode> GetNearbyNodes(GraphNode currentNode, List<GraphNode> currentLevel, List<GraphNode> targetLevel) {
        List<GraphNode> nearbyNodes = new List<GraphNode>();

        if (targetLevel.Count == 0)
            return nearbyNodes;

        int currentIndex = currentLevel.IndexOf(currentNode);
        float relativePosition = currentLevel.Count > 1 ? (float)currentIndex / (currentLevel.Count - 1) : 0.5f;
        int targetIndex = Mathf.FloorToInt(relativePosition * (targetLevel.Count - 1));

        nearbyNodes.Add(targetLevel[targetIndex]);

        if (targetIndex > 0) {
            nearbyNodes.Add(targetLevel[targetIndex - 1]);
        }

        if (targetIndex < targetLevel.Count - 1) {
            nearbyNodes.Add(targetLevel[targetIndex + 1]);
        }

        return nearbyNodes;
    }

    private void EnsureConnections(GraphNode currentNode, List<GraphNode> potentialConnections, MapGenerationData settings) {
        bool hasConnected = false;

        foreach (var targetNode in potentialConnections) {
            bool shouldConnect = UnityEngine.Random.value <= config.connectionProbability;

            if (shouldConnect) {
                currentNode.ConnectTo(targetNode);
                hasConnected = true;
            }
        }

        if (!hasConnected && potentialConnections.Count > 0) {
            ConnectOneRandomNode(currentNode, potentialConnections);
        }
    }

    private GraphNode ConnectOneRandomNode(GraphNode currentNode, List<GraphNode> connections) {
        if (connections.Count == 0)
            return null;

        int randomIndex = randomService.Next(0, connections.Count);
        GraphNode targetNode = connections[randomIndex];

        if (targetNode != null && !currentNode.IsConnectedTo(targetNode)) {
            currentNode.ConnectTo(targetNode);
            return targetNode;
        }

        return null;
    }

    private void CreateMinimalConnection(GraphNode currentNode, List<GraphNode> potentialConnections) {
        if (potentialConnections.Count == 0)
            return;

        GraphNode connectedNode = ConnectOneRandomNode(currentNode, potentialConnections);

        if (connectedNode != null && connectedNode.nextLevelConnections.Count > 0) {
            foreach (var nextNode in connectedNode.nextLevelConnections) {
                if (nextNode.prevLevelConnections.Count > 1) {
                    GraphNode unnecessaryConnection = nextNode;
                    connectedNode.Disconnect(unnecessaryConnection);
                    break;
                }
            }
        }
    }
}
