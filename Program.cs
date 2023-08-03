using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Xml.Linq;

public class ArraysAndStrings
{
    // Chapter 1: Arrays & Strings

    // Q1: Is Unique: Implement an algorithm to determine if a string has all unique characters. What if you cannot use additional data structures?

    // Solution 1: Using ASCII
    // Time complexity O(n), Space complexity O(1)

    public static bool isUnique(string unique)
    {
        // checks if the length of the 'unique' string is greater than 128. Since there are only 128 unique ASCII characters, if the 'unique' string has more than 128 characters, it must  contain at least one repeated character. In this case, the method returns false.
        if (unique.Length > 128) return false;

        bool[] characters = new bool[128];

        for (int i = 0; i < unique.Length; i++)
        {
            int value = unique[i];

            // checks if the value at index 'value' in the 'characters' array is true.If it is, this means that the current character has already appeared in the 'unique' string, so the method returns false.
            if (characters[value]) return false;

            // If the value at index 'value' in the 'characters' array is not true, we set it to true to indicate that this character has now appeared in the 'unique' string.
            characters[value] = true;
        }
        return true;
    }

    // Solution 2: No Additional Data Structures
    // Time complexity O(n^2), Space complexity O(1)
    public static bool isUniqueNoDs(string unique)
    {
        for (int i = 0; i < unique.Length; i++)
        {
            for (int j = i + 1; j < unique.Length; j++)
            {
                if (unique[j] == unique[i]) return true;
            }
        }
        return false;
    }


    // Q2: Check Permutation: Given two strings, write a method to decide if one is a permutation of the other.

    // Solution: Using ASCII
    // Time complexity O(n), Space complexity O(1)

    public static bool isPermutation(string a, string b)
    {
        if (a.Length != b.Length) return false;

        int[] letters = new int[128];

        for (int i = 0; i < a.Length; i++)
        {
            letters[a[i]]++;
        }

        for (int i = 0; i < b.Length; i++)
        {
            letters[b[i]]--;

            if (letters[b[i]] < 0) return false;
        }
        return true;
    }

    // Q3: URLify: Write a method to replace all spaces in a string with '%20': You may assume that the string has sufficient space at the end to hold the additional characters, and     that you are given the "true" length of the string.

    // Time complexity O(n), Space complexity O(n)

    public static string ReplaceSpaces(string str, int trueLength)
    {
        // Converts string to characters array
        char[] characters = str.ToCharArray();
        int spaceCount = 0;

        for (int i = 0; i < trueLength; i++)
        {
            if (characters[i] == ' ')
            {
                spaceCount++;
            }
        }
        // trueLength and index are greater than array index 
        int index = trueLength + spaceCount * 2;

        for (int i = trueLength - 1; i >= 0; i--)
        {
            if (characters[i] == ' ')
            {
                characters[index - 1] = '0';
                characters[index - 2] = '2';
                characters[index - 3] = '%';
                index -= 3;
            }
            else
            {
                characters[index - 1] = characters[i];
                index--;
            }
        }
        return new string(characters);
    }

    // Q4: Palindrome Permutation: Given a string, write a function to check if it is a permutation of a palindrome. A palindrome is a word or phrase that is the same forwards and      backwards. A permutation is a rearrangement of letters.The palindrome does not need to be limited to just dictionary words.

    // Time complexity O(n), Space complexity O(1)

    public static bool isPermutaionOfPalindrome(string str)
    {
        int bitVector = CreateBitVector(str);

        return bitVector == 0 || CheckExactlyOneBitSet(bitVector);
    }

    static int CreateBitVector(string str)
    {
        int bitVector = 0;

        foreach (char c in str)
        {
            int x = GetCharNumber(c);
            bitVector = Toggle(bitVector, x);
        }
        return bitVector;
    }

    static int Toggle(int bitVector, int index)
    {
        if (index < 0) return bitVector;

        int mask = 1 << index;

        if ((bitVector & mask) == 0)
        {
            bitVector |= mask;
        }
        else
        {
            bitVector &= ~mask;
        }
        return bitVector;
    }

    static bool CheckExactlyOneBitSet(int bitVector)
    {
        return (bitVector & (bitVector - 1)) == 0;
    }

    static int GetCharNumber(char c)
    {
        if ('a' <= c && c <= 'z')
        {
            return c - 'a';
        }
        else if ('A' <= c && c <= 'Z')
        {
            return c - 'A';
        }
        return -1;
    }

    // Q5: One Away: There are three types of edits that can be performed on strings: insert a character, remove a character, or replace a character.Given two strings, write a function to check if they are one edit (or zero edits) away.

    // Time complexity O(n), Space complexity O(1)

    bool oneEditAway(string first, string second)
    {
        // Check if the difference in length of both strings is more than 1
        if (Math.Abs(first.Length - second.Length) > 1)
        {
            return false;
        }

        // Set s1 as longer string and s2 as shorter string
        string s1 = first.Length < second.Length ? first : second;
        string s2 = first.Length > second.Length ? second : first;

        int index1 = 0;
        int index2 = 0;

        bool foundDifference = false;

        while (index1 < s1.Length && index2 < s2.Length)
        {
            if (s1[index1] != s2[index2])
            {
                if (foundDifference) return false;

                foundDifference = true;

                if (s1.Length == s2.Length)
                {
                    index1++;
                }
            }
            else
            {
                index1++;
            }
            index2++;
        }
        return true;
    }

    // Q6: String Compression: Implement a method to perform basic string compression using the counts of repeated characters.For example, the string aabcccccaaa would become a2b1c5a3.If the "compressed" string would not become smaller than the original string, your method should return the original string. You can assume the string has only uppercase and lowercase letters(a - z).

    // Time complexity O(n), Space complexity O(n)

    public static string CompressedString(string str)
    {
        // Check if string is empty
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        // To Build new 'compressed' string
        StringBuilder compressed = new StringBuilder();

        int countConsecutive = 0;

        for (int i = 0; i < str.Length; i++)
        {
            countConsecutive++;

            // Check if the next index in string is same
            if (i + 1 >= str.Length || str[i] != str[i + 1])
            {
                compressed.Append(str[i]);
                compressed.Append(countConsecutive);

                countConsecutive = 0;
            }
        }
        // Check if new string is smaller than input string
        return compressed.Length > str.Length ? compressed.ToString() : str;
    }

    // Q7: Rotate Matrix: Given an image represented by an NxN matrix, where each pixel in the image is 4 bytes, write a method to rotate the image by 90 degrees. (an you do this in place?

    // Time complexity O(n^2), Space complexity O(1)

    public static void Rotate(int[,] matrix)
    {
        // Get length of matrix
        int n = matrix.GetLength(0);

        // Loop through layers of matrix
        for (int layer = 0; layer < n / 2; ++layer)
        {
            // Index to start rotating from
            int first = layer;
            int last = n - 1 - layer;

            for (int i = first; i < last; ++i)
            {
                // Variable to iterate through each element of the layer
                int offset = i - first;
                // To Temporarily store the top element of the array
                int top = matrix[first, i];

                // Shift left bottom to left top
                matrix[first, i] = matrix[last - offset, first];
                // Shift right bottom to left bottom
                matrix[last - offset, first] = matrix[last, last - offset];
                // Shift top right to right bottom
                matrix[last, last - offset] = matrix[i, last];
                // Shift saved left top to right top
                matrix[i, last] = top;
            }
        }
    }

    // Q8: Zero Matrix: Write an algorithm such that if an element in an MxN matrix is 0, its entire row and column are set to 0.

    // Time complexity O(M*N), Space complexity O(1)

    public static void SetZeroes(int[,] matrix)
    {
        int n = matrix.GetLength(0);
        int m = matrix.GetLength(1);

        bool firstRowZero = false;
        bool firstColZero = false;

        // Check if first row contains a 0
        for (int j = 0; j < n; ++j)
        {
            if (matrix[0, j] == 0)
            {
                firstRowZero = true;
                break;
            }
        }

        // Check if first column contains a 0
        for (int i = 0; i < m; ++i)
        {
            if (matrix[i, 0] == 0)
            {
                firstColZero = true;
                break;
            }
        }

        // Use first row and first column as storage for row and column flags
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (matrix[i, j] == 0)
                {
                    matrix[i, 0] = 0;
                    matrix[0, j] = 0;
                }
            }
        }

        // Set rows and columns to 0 based on flags in first row and first column
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (matrix[i, 0] == 0 || matrix[0, j] == 0)
                {
                    matrix[i, j] = 0;
                }
            }
        }

        // Set first row to 0 if it originally contained a 0
        if (firstRowZero)
        {
            for (int j = 0; j <= n; j++)
            {
                matrix[0, j] = 0;
            }
        }

        // Set first column to 0 if it originally contained a 0
        if (firstColZero)
        {
            for (int i = 0; i < m; i++)
            {
                matrix[i, 0] = 0;
            }
        }
    }

    // Q9: String Rotation: Assume you have a method isSubst ring which checks if one word is a substring of another.Given two strings, s1 and s2, write code to check if s2 is a rotation of s1 using only one call to isSubstring (e.g., "waterbottle" is a rotation of"erbottlewat").

    // Time complexity O(n), Space complexity O(n)

    public static bool isRotation(string s1, string s2)
    {
        if (s1.Length != s2.Length || s1.Length == 0)
        {
            return false;
        }

        string s1s1 = s1 + s1;

        return isSubstring(s1s1, s2);
    }

    // To ignore the exception for isSubstring method 
    private static bool isSubstring(string s1s1, string s2)
    {
        throw new NotImplementedException();
    }
}

public class LinkedLists
{
    // Chapter 2: Linked Lists

    // Node to represent nodes in linked lists for the questions
    public class LinkedListNode
    {
        public int Data;
        public LinkedListNode Next;

        public LinkedListNode(int data)
        {
            Data = data;
            Next = null;
        }
    }

    // Q10: Remove Dups: Write code to remove duplicates from an unsorted linked list.

    // Solution 1: Using HashSet
    // Time complexity O(n), Space complexity O(n)

    // Node is an assumed singly linked list for each question
    public void RemoveDuplicates(LinkedListNode n)
    {
        // Create HashSet to keep track of repeating values
        HashSet<int> set = new HashSet<int>();

        LinkedListNode previous = null;

        while (n != null)
        {
            // Check if the current value is already present in the HashSet
            if (set.Contains(n.Data))
            {
                // Remove duplicate value
                previous.Next = n.Next;
            }
            else
            {
                // Add value to the hashset if it is not a duplicate
                set.Add(n.Data);

                previous = n;
            }
            // Move to the next node
            n = n.Next;
        }
    }

    // Solution 2: Without buffers(additional data structires)
    // Time complexity O(n^2), Space complexity O(1)

    public void RemoveDuplicateNoBuffer(LinkedListNode head)
    {
        // Variable to iterate through all nodes in the list one by one
        LinkedListNode current = head;

        while (current != null)
        {
            // Variable to compare all nodes in the list with the current node
            LinkedListNode n = current.Next;

            while (n.Next != null)
            {
                // Check for duplicate value
                if (n.Next.Data == current.Data)
                {
                    // Remove duplicate value
                    n.Next = n.Next.Next;
                }
                else
                {
                    n = n.Next;
                }
            }
            current = current.Next;
        }
    }

    // Q11: Return Kth to Last: Implement an algorithm to find the kth to last element of a singly linked list.

    // Time complexity O(n), Space complexity O(1)

    public static LinkedListNode FindKthToLast(LinkedListNode head, int k)
    {
        if (k <= 0) return null;

        LinkedListNode p1 = head;
        LinkedListNode p2 = head;

        // Move p2 to get kth element's position
        for (int i = 0; i < k - 1; i++)
        {
            if (p2 == null) return null;

            p2 = p2.Next;
        }

        // Check if kth to the last element is null
        if (p2 == null) return null;

        // Move p1 to kth to last element's position and p2 to the end of the linked list
        while (p2.Next != null)
        {
            p1 = p1.Next;
            p2 = p2.Next;
        }
        return p1;
    }

    // Q12: Delete Middle Node: Implement an algorithm to delete a node in the middle (i.e., any node but the first and last node, not necessarily the exact middle) of a singly linked list, given only access to that node.

    // Time complexity O(1), Space complexity O(1)

    public static bool DeleteNode(LinkedListNode node)
    {
        if (node == null || node.Next == null)
        {
            // Deletion not possible
            return false;
        }

        // Declare variable to store next node's value and set it to the next property of accessible node
        LinkedListNode next = node.Next;
        // Set value of the accessible node to the value of 'next' variable
        node.Data = next.Data;
        // Remove the next node from the list
        node.Next = next.Next;

        // Deletion complete
        return true;
    }

    // Q13: Partition: Write code to partition a linked list around a value x, such that all nodes less than x come before all nodes greater than or equal to x.lf x is contained within the list, the values of x only need to be after the elements less than x. The partition element x can appear anywhere in the "right partition"; it does not need to appear between the left and right partitions.

    // Time complexity O(n), Space complexity O(1)

    public LinkedListNode Partition(LinkedListNode node, int x)
    {
        LinkedListNode head = node;
        LinkedListNode tail = node;

        // Iterate through the input linked list
        while (node != null)
        {
            // Temporary variable to keep track of the next node in the list
            LinkedListNode next = node.Next;

            if (node.Data < x)
            {
                // Append head of the first new linked list
                node.Next = head;
                head = node;
            }
            else
            {
                // Append tail of the second new linked list
                tail.Next = node;
                tail = node;
            }
            // Move on to the next node in the linked list
            node = next;
        }
        // To end the new linked list
        tail.Next = null;

        // Return the new head so that the newly generated linked list can be accessed
        return head;
    }

    // Q14: Sum Lists: You have two numbers represented by a linked list, where each node contains a single digit.The digits are stored in reverse order, such that the 1 's digit is at the head of the list. Write a function that adds the two numbers and returns the sum as a linked list.

    // EXAMPLE:
    // Input: (7-> 1 -> 6) + (5 -> 9 -> 2) .Thatis,617 + 295.
    // Output: 2 - > 1 - > 9. That is, 912. 

    // Solution 1: Reverse Order
    // Time complexity O(max(m,n)), Space complexity O(max(m,n))

    public LinkedListNode AddLists(LinkedListNode l1, LinkedListNode l2)
    {
        // Fake head to keep track of the new linked list's head
        LinkedListNode dummyHead = new LinkedListNode(0);
        LinkedListNode current = dummyHead;

        int carry = 0;
        // Iterate until both lists are null
        while (l1 != null || l2 != null)
        {
            int x = (l1 == null) ? l1.Data : 0;
            int y = (l2 == null) ? l2.Data : 0;
            int sum = carry+ x + y;
            // Sum > 10 = 1, Sum < 10 = 0 
            carry = sum / 10;
            // Create new nodes in the solution list containing the sum
            current.Next = new LinkedListNode(sum % 10);
            // Move to the next node in the solution list
            current = current.Next;

            if (l1 != null) l1 = l1.Next;
            if (l2 != null) l2 = l2.Next;
        }
        // Add last node in the solution list containing the carry if any
        if (carry > 0)
        {
            current.Next = new LinkedListNode(carry);
        }
        // return the original head of the solution list
        return dummyHead.Next;
    }

    // FOLLOW UP:
    // Suppose the digits are stored in forward order.Repeat the above problem.

    // EXAMPLE:
    // Input: (6 -) 1 -) 7) + (2 -) 9 -) 5).Thatis,617 + 295. 
    // Output: 9 -) 1 -) 2. That is, 912.

    // Solution 2: Forward Order
    // Time complexity O(n), Space complexity O(n)

    private class PartialSum
    {
        // Default values of output list and carry
        public LinkedListNode sum = null;
        public int carry = 0;
    }

    public static LinkedListNode AddListsForward(LinkedListNode l1, LinkedListNode l2) 
    {
        int len1 = GetLength(l1);
        int len2 = GetLength(l2);

        // Insert zeros in input lists to equalize length of both
        if (len1 < len2)
        {
            l1 = Padlist(l1, len2 - len1); 
        }
        else
        {
            l2 = Padlist(l2, len1 - len2);
        }
        // Call AddListsHelper method to perform addition on the input lists
        var sum = AddListsHelper(l1 , l2);

        if (sum.carry == 0)
        {
            return sum.sum;
        }
        // Add carry to the head of the output list
        else
        {
            var result = InsertBefore(sum.sum, sum.carry);
            return result;
        }

    }
    // Method to perform addition on the input lists
    private static PartialSum AddListsHelper(LinkedListNode l1, LinkedListNode l2)
    {
        // Return default new PartailSum values after reaching the end of both input lists
        if (l1 == null && l2 == null)
        {
            return new PartialSum();
        }
        // Recursive call to the AddListsHelper method to perform addition and calculate carry for remaining nodes of the input lists
        var sum = AddListsHelper(l1.Next, l2.Next);

        int val = sum.carry + l1.Data + l2.Data;
        // Call InsertBefore method to insert sum of current nodes to new node
        var fullResult = InsertBefore(sum.sum, val % 10);

        sum.sum = fullResult;
        sum.carry = val / 10;

        return sum;
    }
    // Method to insert zeros in the input lists
    private static LinkedListNode Padlist(LinkedListNode l, int padding)
    {
        LinkedListNode head = l;

        for (int i = 0; i < padding; i++)
        {
            // Insert zeros at the head
            head = InsertBefore(head, 0); 
        }
        return head;
    }
    // Helper method to insert zeros at the head
    private static LinkedListNode InsertBefore(LinkedListNode list, int data)
    {
        // New node to contain the zero for padding
        LinkedListNode node = new LinkedListNode(data);

        if (list != null) 
        {
            // Set next node to be the head
            node.Next = list;
        }
        // Return the node containing zero
        return node;
    }
    // Method to get lengths of the input lists
    private static int GetLength(LinkedListNode l) 
    {
        int len = 0;

        while (l != null)
        {
            len++;
            l = l.Next;
        }
        return len;
    }

    // Q15: Palindrome: Implement a function to check if a linked list is a palindrome.

    // Time complexity O(n), Space complexity O(1)

    public static bool IsPalindrome(LinkedListNode head)
    {
        // If the list is empty return true
        if (head == null || head.Next == null)
        {
            return true;
        }
        // Variables to help iterate to the middle of the list
        LinkedListNode slow = head;
        LinkedListNode fast = head;
        // Iterate to the middle of the list
        while (fast.Next != null && fast.Next.Next != null)
        {
            slow = slow.Next;
            fast = fast.Next.Next;
        }
        // Set head for the second half of the list
        LinkedListNode secondHalfHead = slow.Next;
        // Reverse second half of the list with the help of the head
        secondHalfHead = ReverseList(secondHalfHead);
        // Initialize first half's and second half's heads for comparison
        LinkedListNode p1 = head;
        LinkedListNode p2 = secondHalfHead;

        bool result = true;
        // Iterate through both halfs to compare if both have the same data
        while (p2 != null)
        {
            if (p1.Data != p2.Data)
            {
                result = false;

                break;
            }
            p1 = p1.Next;
            p2 = p2.Next;
        }
        // Reverse the second half of the list again to return it to its original state
        ReverseList(secondHalfHead);

        return result;
    }
    // Method for reversing a list from the given head
    public static LinkedListNode ReverseList(LinkedListNode head)
    {
        LinkedListNode prev = null;
        LinkedListNode current = head;

        while (current != null)
        {
            LinkedListNode next = current.Next;
            current.Next = prev;
            prev = current;
            current = next;
        }
        return prev;
    }
}