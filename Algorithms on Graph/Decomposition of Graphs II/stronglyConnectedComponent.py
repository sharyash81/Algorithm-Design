#Uses python3
import sys


sys.setrecursionlimit(200000)

def normalExplore(visited,adj,vertex):
    visited[vertex] = True
    for neighbor in adj[vertex]:
        if not visited[neighbor]:
            normalExplore(visited,adj,neighbor)


def explore(visited,adj,vertex):
    visited[vertex] = True
    for neighbor in adj[vertex]:
        if not visited[neighbor]:
            explore(visited,adj,neighbor)
    global ReversePostOrder
    ReversePostOrder.insert(0,vertex)

def dfs(adj):
    n = len(adj)
    visited = [False]*n
    for i in range(n):
        if not visited[i]:
            explore(visited,adj,i)



def number_of_strongly_connected_components(adj):
    result = 0
    Recursive_adj = reverseGraph(adj)
    dfs(Recursive_adj)    
    visited = [False]*n
    global ReversePostOrder
    for vertex in ReversePostOrder:
        if not visited[vertex]:
            result+=1
            normalExplore(visited,adj,vertex)
    return result
def reverseGraph(adj):
    n = len(adj)
    Reverse_Graph = [[] for i in range(n)]
    for i in range(n):
        for vertex in adj[i]:
            if not Reverse_Graph[vertex].__contains__(i):
                Reverse_Graph[vertex].append(i)
    return Reverse_Graph
if __name__ == '__main__':
    input = sys.stdin.read()
    data = list(map(int, input.split()))
    n, m = data[0:2]
    data = data[2:]
    edges = list(zip(data[0:(2 * m):2], data[1:(2 * m):2]))
    adj = [[] for _ in range(n)]
    for (a, b) in edges:
        adj[a - 1].append(b - 1)
    global ReversePostOrder
    ReversePostOrder = []
    print(number_of_strongly_connected_components(adj))