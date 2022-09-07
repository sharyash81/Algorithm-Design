#Uses python3

import sys

def bipartite(adj):
    n = len(adj)
    colors = [-1]*n
    for vertex in range(n):
        if (colors[vertex]==-1):
            colors[vertex] = 1
            q = []
            q.append(vertex)
            while len(q)!=0:
                v = q.pop(0)
                for neighbor in adj[v]:
                    if (neighbor == v):
                        return 0
                    elif (colors[neighbor] == -1):
                        colors[neighbor] = 1 - colors[v]
                        q.append(neighbor)
                    elif (colors[neighbor] == colors[v]):
                        return 0
    return 1

if __name__ == '__main__':
    input = sys.stdin.read()
    data = list(map(int, input.split()))
    n, m = data[0:2]
    data = data[2:]
    edges = list(zip(data[0:(2 * m):2], data[1:(2 * m):2]))
    adj = [[] for _ in range(n)]
    for (a, b) in edges:
        adj[a - 1].append(b - 1)
        adj[b - 1].append(a - 1)
    print(bipartite(adj))
