import sys

def distance(adj, s, t):
    n = len(adj)
    dist = [-1]*n
    dist[s] = 0
    q =[]
    q.append(s)
    while len(q)!=0:
        vertex = q.pop(0)
        for neighbor in adj[vertex]:
            if dist[neighbor]==-1:
                q.append(neighbor)
                dist[neighbor] = dist[vertex]+1
    return dist[t]

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
    s, t = data[2 * m] - 1, data[2 * m + 1] - 1
    print(distance(adj, s, t))