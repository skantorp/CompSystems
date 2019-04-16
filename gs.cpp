#include <iostream> 
using namespace std; 
  
// A function to sort the algorithm using gnome sort 
void gnomeSort(int arr[], int n) 
{ 
    int index = 0; 
  
    while (index < n) { 
        if (index == 0) 
            index++; 
        if (arr[index] >= arr[index - 1]) 
            index++; 
        else { 
            swap(arr[index], arr[index - 1]); 
            index--; 
        } 
    } 
    return; 
} 
  
// A utility function ot print an array of size n 
void printArray(int arr[], int n) 
{ 
    cout << "Sorted sequence after Gnome sort: "; 
    for (int i = 0; i < n; i++) 
        cout << arr[i] << " "; 
    cout << "\n"; 
} 
  
// Driver program to test above functions. 
int main() 
{ 
    srand(time(0));
    int arr [20000]={};
    
    
  for(int i=0;i<20000;i++){
      arr[i]=(rand() % 20000) + 1;
  }
  int n = sizeof(arr) / sizeof(arr[0]); 
    gnomeSort(arr, n); 
    printArray(arr, n); 
  
    return (0); 
} 
