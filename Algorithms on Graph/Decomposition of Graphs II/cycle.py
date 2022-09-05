#Uses python3

import sys

def explore(Original_Vertex , Current_vertex):
    visited[Current_vertex] = True
    for neighbor in adj[Current_vertex]:
        if neighbor == Original_Vertex:
            global Has_Cycle
            Has_Cycle = True
            break
        elif not visited[neighbor]:
            explore(Original_Vertex,neighbor)
def acyclic(adj):
    n = len(adj)
    global visited 
    visited = [0] * n
    for i in range(n):
        if not visited[i]:
            explore(i,i)
            if Has_Cycle:
                break
    if Has_Cycle:
        return 1
    else:
        return 0
        
if __name__ == '__main__':
    input = sys.stdin.read()
    data = list(map(int, input.split()))
    n, m = data[0:2]
    data = data[2:]
    edges = list(zip(data[0:(2 * m):2], data[1:(2 * m):2]))
    global adj 
    adj = [[] for _ in range(n)]
    for (a, b) in edges:
        adj[a - 1].append(b - 1)
    global Has_Cycle 
    Has_Cycle = False
    print(acyclic(adj))
