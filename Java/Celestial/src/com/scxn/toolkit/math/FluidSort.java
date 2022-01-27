package com.scxn.toolkit.math;

public class FluidSort
{
	private Fluid array[];
    private int length;
 
    public void sortX(Fluid[] inputArr) {
         
        if (inputArr == null || inputArr.length == 0) {
            return;
        }
        this.array = inputArr;
        length = inputArr.length;
        quickSortX(0, length - 1);
    }
 
    private void quickSortX(int lowerIndex, int higherIndex) {
         
        int i = lowerIndex;
        int j = higherIndex;
        // calculate pivot number, I am taking pivot as middle index number
        int pivot = array[lowerIndex+(higherIndex-lowerIndex)/2].gridX;
        // Divide into two arrays
        while (i <= j) {
            /**
             * In each iteration, we will identify a number from left side which 
             * is greater then the pivot value, and also we will identify a number 
             * from right side which is less then the pivot value. Once the search 
             * is done, then we exchange both numbers.
             */
            while (array[i].gridX < pivot) {
                i++;
            }
            while (array[j].gridX > pivot) {
                j--;
            }
            if (i <= j) {
                exchangeNumbers(i, j);
                //move index to next position on both sides
                i++;
                j--;
            }
        }
        // call quickSort() method recursively
        if (lowerIndex < j)
            quickSortX(lowerIndex, j);
        if (i < higherIndex)
            quickSortX(i, higherIndex);
    }
    
    public void sortY(Fluid[] inputArr) {
        
        if (inputArr == null || inputArr.length == 0) {
            return;
        }
        this.array = inputArr;
        length = inputArr.length;
        quickSortY(0, length - 1);
    }
 
    private void quickSortY(int lowerIndex, int higherIndex) {
         
        int i = lowerIndex;
        int j = higherIndex;
        // calculate pivot number, I am taking pivot as middle index number
        int pivot = array[lowerIndex+(higherIndex-lowerIndex)/2].gridY;
        // Divide into two arrays
        while (i <= j) {
            /**
             * In each iteration, we will identify a number from left side which 
             * is greater then the pivot value, and also we will identify a number 
             * from right side which is less then the pivot value. Once the search 
             * is done, then we exchange both numbers.
             */
            while (array[i].gridY < pivot) {
                i++;
            }
            while (array[j].gridY > pivot) {
                j--;
            }
            if (i <= j) {
                exchangeNumbers(i, j);
                //move index to next position on both sides
                i++;
                j--;
            }
        }
        // call quickSort() method recursively
        if (lowerIndex < j)
            quickSortY(lowerIndex, j);
        if (i < higherIndex)
            quickSortY(i, higherIndex);
    }
 
    private void exchangeNumbers(int i, int j) {
        Fluid temp = array[i];
        //temp.ID(array[i].ID());
        
        array[i] = array[j];
        //array[i].ID(array[j].ID());
        
        array[j] = temp;
        //array[j].ID(temp.ID());
    }
}
