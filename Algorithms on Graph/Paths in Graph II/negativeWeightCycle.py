#Uses python3
import sys


def negative_cycle(edges,n):
    dist = [float("Inf")]*n
    dist[0] = 0 
    for i in range(n-1):
        for edge in edges:
            # print(edge)
            if dist[edge[0]] != float("Inf") and dist[edge[1]] > dist[edge[0]] + edge[2]:
                dist[edge[1]] = dist[edge[0]] + edge[2]

    nc_detec = False
    for edge in edges:
        if dist[edge[0]] != float("Inf") and dist[edge[1]] > dist[edge[0]] + edge[2]:
            dist[edge[1]] = dist[edge[0]] + edge[2]
            nc_detec = True
    if  nc_detec:
        return 1 
    return 0

if __name__ == '__main__':
    n , m = map(int,input().split())
    adj = []
    for i in range(m):
        start,end , weight = map(int,input().split())
        adj.append([start-1,end-1,weight])
    print(negative_cycle(adj,n))
