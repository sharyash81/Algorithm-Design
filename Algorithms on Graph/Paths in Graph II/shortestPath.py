#Uses python3
import sys
from heapq import heapify,heappush,heappop
import queue
# class Minheap:

#     def __init__(self,max):
#         self.size = 0
#         self.MaxSize = max
#         self.heap = [(_,float('inf')) for _ in range(self.MaxSize)]

#     def __Parent( self , index ):
#         return ((index - 1 )//2 )


#     def __LeftChild(self , index ):
#         return (2 * index + 1 )


#     def __RightChild(self , index ) : 
#         return (2 * (index + 1 ))


#     def GetMin(self):
#         if self.size > 0:
#             return self.heap[0]
#         else:
#             pass


#     def __SiftUp(self,index):
#         while index > 0 and self.heap[self.__Parent(index)][1] > self.heap[index][1]:
#             self.heap[self.__Parent(index)],self.heap[index] = self.heap[index],self.heap[self.__Parent(index)]
#             index = self.__Parent(index)


#     def __SiftDown(self,index):
#         maxindex = index 
#         l = self.__LeftChild(index)
#         if l <= self.size and self.heap[l][1] < self.heap[maxindex][1] :
#             maxindex = l
#         r = self.__RightChild(index)
#         if r <= self.size and self.heap[r][1] < self.heap[maxindex][1] :
#             maxindex = r 
#         if index != maxindex:
#             self.heap[index] , self.heap[maxindex] = self.heap[maxindex] , self.heap[index]
#             self.__SiftDown(maxindex)


#     def ExtractMin(self):
#         if self.size > 0 :
#             min = self.heap[0]
#             self.heap[0] = self.heap[self.size - 1 ]
#             self.size -= 1 
#             self.__SiftDown(0)
#             return min
#         else :
#             pass


#     def ChangePriority(self , index , value) : 
#         oldvalue = self.heap[index][1]
#         self.heap[index] = list(self.heap[index])
#         self.heap[index][1] = value
#         self.heap[index] = tuple(self.heap[index])
#         if value > oldvalue:
#             self.__SiftDown(index)
#         else :
#             self.__SiftUp(index)


#     def Remove(self , index ) :
#         self.heap[index][1] = float('inf')
#         self.__SiftUp(index)
#         self.ExtractMin()


#     def Insert(self,key):
#         if self.size > self.MaxSize - 1 :
#             pass
#         else :
#             self.heap[self.size] = list(self.heap[self.size])
#             self.heap[self.size][1] = key
#             self.heap[self.size] = tuple(self.heap[self.size])
#             self.__SiftUp(self.size)
#             self.size+=1 

    

# def relax(u,v):
#     global dist
#     if dist[v] > dist[u] + weight[(u,v)]:
#         dist[v] = dist[u] + weight[(u,v)]
# #        min_heap.put((v,dist[v]))


# def distance(adj,s,t):
#     n = len(adj)
#     global dist
#     dist[s] = 0
#     H = Minheap(n)
#     [H.Insert(dist[i]) for i in range(n)]
#     while H.size > 0 :
#         u = H.ExtractMin()
#         for vertex in adj[u[0]]:
#             relax(u[0],vertex)
#             H.ChangePriority(vertex,dist[vertex])
#     if dist[t]!=float('inf'):
#         return dist[t]
#     else:
#         return -1

# def distance1(adj, s, t):
#     global dist 
#     Minheap = [(_,float('inf')) for _ in range(len(adj))]
#     dist[s] = 0
#     Minheap[s] = (0,0)
#     heapify(Minheap)
#     while len(Minheap) > 0:
#         u = heappop(Minheap)
#         for v in adj[u[0]]:
#             relax(u[0],v)
#             list = [y[0] for y in Minheap]
#             for i in range(len(list)):
#                 if list[i] == v:
#                     Minheap[i] = (v,dist[v])
#     if dist[t]!=float('inf'):
#         return dist[t]
#     else:
#         return -1

def distance2(adj,s,t):
    #global dist
    n =  len(adj)
    dist = [float('inf')]*n
    dist[s] = 0
    min_heap = queue.PriorityQueue()
    min_heap.put((s,dist[s]))
    while not min_heap.empty():
        u = min_heap.get()
        index = u[0]
        for v in adj[index]:
            if dist[v] > dist[index] + weight[(index,v)]:
                dist[v] = dist[index] + weight[(index,v)]
                min_heap.put((v,dist[v]))
    if (dist[t] == float('inf')):
        return -1 
    return dist[t]


if __name__ == '__main__':

    input = sys.stdin.read()
    data = list(map(int, input.split()))
    n, m = data[0:2]
    data = data[2:]
    edges_format = list(zip(zip(data[0:(3 * m):3], data[1:(3 * m):3]), data[2:(3 * m):3]))
    data = data[3 * m:]
    adj = [[] for _ in range(n)]
    global weight 
    weight = dict()
    for ((a,b),w) in edges_format:
        adj[a-1].append(b-1)
        weight[(a-1,b-1)] = w 
    s, t = data[0] - 1, data[1] - 1
    #global dist
    #dist = [float('inf') for _ in range(n)]
    print(distance2(adj, s, t))
