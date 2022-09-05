import sys

def dfs(adj,used, order:list,vertex):
    used[vertex] = True
    for neighbor in adj[vertex]:
        if not used[neighbor]:
            dfs(adj,used,order,neighbor)
    order.insert(0,vertex)
    pass


def toposort(adj):
    used = [0] * len(adj)
    order = []
    n = len(adj)
    for vertex in range(n):
        if not used[vertex] :
            dfs(adj,used,order,vertex)

    return order

if __name__ == '__main__':
    input = sys.stdin.read()
    data = list(map(int, input.split()))
    n, m = data[0:2]
    data = data[2:]
    edges = list(zip(data[0:(2 * m):2], data[1:(2 * m):2]))
    adj = [[] for _ in range(n)]
    for (a, b) in edges:
        adj[a - 1].append(b - 1)
    order = toposort(adj)
    for x in order:
        print(x + 1, end=' ')