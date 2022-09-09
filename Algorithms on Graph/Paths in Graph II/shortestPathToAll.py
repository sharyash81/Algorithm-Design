#Uses python3
import sys
def shortest_paths(number_of_vertices , adj , starting_vertex):
    dist = [float('inf')]*number_of_vertices
    # in_negative_cycle = [False]*number_of_vertices
    prev = [None]*number_of_vertices
    dist[starting_vertex] = 0
    for i in range(number_of_vertices-1):
        for vertex in range(number_of_vertices):
            for edge in adj[vertex]:
                if dist[edge[0]] > dist[vertex] + edge[1]:
                    dist[edge[0]] = dist[vertex] + edge[1]
                    prev[edge[0]] = vertex
    nc_detect = False
    last_change = 0
    for vertex in range(number_of_vertices):
        for edge in adj[vertex]:
            if dist[edge[0]] > dist[vertex] + edge[1]:
                dist[edge[0]] = dist[vertex] + edge[1]
                prev[edge[0]] = vertex
                nc_detect = True
                last_change = edge[0]
    if (nc_detect):
        for i in range(number_of_vertices):
            dist[last_change] = '-'
            last_change = prev[last_change]
    return dist

if __name__ == '__main__':
    number_of_vertices  ,number_of_edges = map(int,input().split())
    adj = [[] for _ in range(number_of_vertices)]
    for i in range(number_of_edges):
        start , end , weight = map(int , input().split())
        adj[start-1].append((end - 1,weight))
    vertex = int(input()) - 1
    shortest = shortest_paths(number_of_vertices , adj , vertex)
    for i in range(len(shortest)):
        if shortest[i] == '-':
            print('-')
        elif shortest[i] == float('inf'):
            print('*')
        else:
            print(shortest[i])
        

