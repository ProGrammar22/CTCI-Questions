using System.Collections;
using System.Text;


//                                                                   CHAPTER 1: ARRAYS & STRINGS


class ArraysAndStrings
{
    // One dimensional and two dimensional array Implementation:

    public int[] oneDArray = new int[10];
    public string[,] twoDArray = new string[10, 10];

    // C# supports up to 32 dimensional arrays
}


// Q1: Is Unique: Implement an algorithm to determine if a string has all unique characters. What if you cannot use additional data structures?

// Solution 1: Using ASCII

// Time complexity O(n), Space complexity O(1)

class IsUnique
{
    public static bool isUnique(string unique)
    {
        // Check if the length of the 'unique' string is greater than 128. Since there are only 128 unique ASCII characters, if the 'unique' string has more than 128 characters, it must contain at least one repeated character. In this case, the method returns false.
        if (unique.Length > 128)
        {
            return false;
        }

        bool[] characters = new bool[128];

        for (int i = 0; i < unique.Length; i++)
        {
            int value = unique[i];

            // Check if the value at the index 'value' in the 'characters' array is true. If it is, this means that the current character has already appeared in the 'unique' string, so the method returns false.
            if (characters[value])
            {
                return false;
            }

            // If the value at the index 'value' in the 'characters' array is not true, set it to true to indicate that this character has now appeared in the 'unique' string.
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
                if (unique[j] == unique[i])
                {
                    return false;
                }
            }
        }
        return true;
    }
}


// Q2: Check Permutation: Given two strings, write a method to decide if one is a permutation of the other.

// Solution: Using ASCII

// Time complexity O(n), Space complexity O(1)

class CheckPermutation
{
    public static bool isPermutation(string a, string b)
    {
        if (a.Length != b.Length)
        {
            return false;
        }

        int[] letters = new int[128];

        for (int i = 0; i < a.Length; i++)
        {
            letters[a[i]]++;
        }

        for (int i = 0; i < b.Length; i++)
        {
            letters[b[i]]--;

            if (letters[b[i]] < 0)
            {
                return false;
            }
        }
        return true;
    }
}


// Q3: URLify: Write a method to replace all spaces in a string with '%20': You may assume that the string has sufficient space at the end to hold the additional characters, and that you are given the "true" length of the string.

//     EXAMPLE:
//     Input: "Mr John Smith    ", 13 
//     Output: "Mr%20John%20Smith"

// Time complexity O(n), Space complexity O(n)

class URLify
{
    public static string ReplaceSpaces(string str, int trueLength)
    {
        // Convert the string to the characters array
        char[] characters = str.ToCharArray();
        int spaceCount = 0;

        for (int i = 0; i < trueLength; i++)
        {
            if (characters[i] == ' ')
            {
                spaceCount++;
            }
        }

        // trueLength and index are greater than the array index 
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
}


// Q4: Palindrome Permutation: Given a string, write a function to check if it is a permutation of a palindrome. A palindrome is a word or phrase that is the same forwards and backwards. A permutation is a rearrangement of letters.The palindrome does not need to be limited to just dictionary words. FHQ(Fucking Hard Question)

//     EXAMPLE: 
//     Input: Tact Coa
//     Output: True(permutations: "taco cat". "atco cta". etc.)

// Time complexity O(n), Space complexity O(1)

class PalindromePermutation
{
    public static bool isPermutationOfPalindrome(string str)
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
        if (index < 0)
        {
            return bitVector;
        }

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
}


// Q5: One Away: There are three types of edits that can be performed on strings: insert a character, remove a character, or replace a character.Given two strings, write a function to check if they are one edit (or zero edits) away.

//     EXAMPLE:
//     pale, ple -> true 
//     pales, pale -> true 
//     pale, bale -> true 
//     pale, bake -> false

// Time complexity O(n), Space complexity O(1)

class OneAway
{
    bool oneEditAway(string first, string second)
    {
        // Check if the difference in the length of both strings is more than 1
        if (Math.Abs(first.Length - second.Length) > 1)
        {
            return false;
        }

        // Set s1 as longer string and s2 as shorter string
        string s1 = first.Length < second.Length ? first : second;
        string s2 = first.Length < second.Length ? second : first;

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
}


// Q6: String Compression: Implement a method to perform basic string compression using the counts of repeated characters. For example, the string aabcccccaaa would become a2b1c5a3. If the "compressed" string would not become smaller than the original string, your method should return the original string. You can assume the string has only uppercase and lowercase letters(a - z).

// Time complexity O(n), Space complexity O(n)

class StringCompression
{
    public static string CompressedString(string str)
    {
        // Check if the string is empty
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        // Build the new 'compressed' string
        StringBuilder compressed = new StringBuilder();

        int countConsecutive = 0;

        for (int i = 0; i < str.Length; i++)
        {
            countConsecutive++;

            // Check if the next index in the string is the same
            if (i + 1 >= str.Length || str[i] != str[i + 1])
            {
                compressed.Append(str[i]);
                compressed.Append(countConsecutive);

                countConsecutive = 0;
            }
        }

        // Check if the new string is smaller than the input string
        return compressed.Length > str.Length ? compressed.ToString() : str;
    }
}


// Q7: Rotate Matrix: Given an image represented by an NxN matrix, where each pixel in the image is 4 bytes, write a method to rotate the image by 90 degrees. (an you do this in place?

// Time complexity O(n^2), Space complexity O(1)

class RotateMatrix
{
    public static void Rotate(int[,] matrix)
    {
        // Get the length of the matrix
        int n = matrix.GetLength(0);

        // Loop through the layers of the matrix
        for (int layer = 0; layer < n / 2; ++layer)
        {
            // Index to start rotating from
            int first = layer;
            int last = n - 1 - layer;

            for (int i = first; i < last; ++i)
            {
                // Variable to iterate through each element of the layer
                int offset = i - first;

                // Temporarily store the top element of the array
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
}


// Q8: Zero Matrix: Write an algorithm such that if an element in an MxN matrix is 0, its entire row and column are set to 0.

// Time complexity O(m*n), Space complexity O(1)

class ZeroMatrix
{
    public static void SetZeros(int[,] matrix)
    {
        int n = matrix.GetLength(0);
        int m = matrix.GetLength(1);

        bool firstRowZero = false;
        bool firstColZero = false;

        // Check if the first row contains a 0
        for (int j = 0; j < n; ++j)
        {
            if (matrix[0, j] == 0)
            {
                firstRowZero = true;
                break;
            }
        }

        // Check if the first column contains a 0
        for (int i = 0; i < m; ++i)
        {
            if (matrix[i, 0] == 0)
            {
                firstColZero = true;
                break;
            }
        }

        // Use the first row and first column as storage for the row and column flags
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

        // Set the rows and columns to 0 based on the flags in the first row and first column
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

        // Set the first row to 0 if it originally contained a 0
        if (firstRowZero)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[0, j] = 0;
            }
        }

        // Set the first column to 0 if it originally contained a 0
        if (firstColZero)
        {
            for (int i = 0; i < m; i++)
            {
                matrix[i, 0] = 0;
            }
        }
    }
}


// Q9: String Rotation: Assume you have a method isSubstring which checks if one word is a substring of another. Given two strings, s1 and s2, write code to check if s2 is a rotation of s1 using only one call to isSubstring (e.g., "waterbottle" is a rotation of"erbottlewat").

// Time complexity O(n), Space complexity O(n)

class StringRotation
{
    public static bool isRotation(string s1, string s2)
    {
        if (s1.Length != s2.Length || s1.Length == 0)
        {
            return false;
        }

        string s1s1 = s1 + s1;

        return isSubstring(s1s1, s2);
    }

    // Ignore the exception for the isSubstring method 
    private static bool isSubstring(string s1s1, string s2)
    {
        throw new NotImplementedException();
    }
}


//                                                                   CHAPTER 2: LINKED LISTS


class LinkedLists
{
    // Node to represent nodes in the linked lists for the questions
    public class Node
    {
        public int Data;
        public Node Next;

        public Node(int data)
        {
            this.Data = data;
            this.Next = null;
        }
    }

    // Singly linked list implementation:
    public class SinglyLinkedList
    {
        // Initialize the head node of the list

        private Node head;
        // Method to add a new node at the front of the list
        public void AddFirst(int data)
        {
            Node newNode = new Node(data);
            newNode.Next = head;
            head = newNode;
        }

        // Method to add a new node at the end of the list
        public void AddLast(int data)
        {
            if (head == null)
            {
                head = new Node(data);
            }
            else
            {
                Node current = head;

                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = new Node(data);
            }
        }

        // Method to remove a node from the front of the list
        public void RemoveFirst()
        {
            if (head != null)
            {
                head = head.Next;
            }
        }

        // Method to remove a node from the end of the list
        public void RemoveLast()
        {
            if (head != null)
            {
                if (head.Next == null)
                {
                    head = null;
                }
                else
                {
                    Node current = head;

                    while (current.Next.Next != null)
                    {
                        current = current.Next;
                    }
                    current.Next = null;
                }
            }
        }

        // Method to check if the list contains any data
        public bool Contains(int data)
        {
            Node current = head;

            while (current != null)
            {
                if (current.Data == data)
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }
    }

    // Doubly linked list implementation:
    public LinkedList<int> doublyLinkedList = new LinkedList<int>();
}


// Q10: Remove Dups: Write code to remove duplicates from an unsorted linked list.

// Solution 1: Using HashSet

// Time complexity O(n), Space complexity O(n)

class RemoveDups
{
    public void RemoveDuplicates(LinkedLists.Node n)
    {
        // Create a HashSet to keep track of the repeating values
        HashSet<int> set = new HashSet<int>();

        LinkedLists.Node previous = null;

        while (n != null)
        {
            // Check if the current value is already present in the HashSet
            if (set.Contains(n.Data))
            {
                // Remove the duplicate value
                previous.Next = n.Next;
            }
            else
            {
                // Add the value to hashset if it is not a duplicate
                set.Add(n.Data);

                previous = n;
            }

            // Move to the next node
            n = n.Next;
        }
    }

    // FOLLOW UP:
    // How would you solve this problem if a temporary buffer is not allowed?

    // Solution 2: Without buffers(additional data structures)

    // Time complexity O(n^2), Space complexity O(1)

    public void RemoveDuplicateNoBuffer(LinkedLists.Node head)
    {
        // Variable to iterate through all nodes in the list one by one
        LinkedLists.Node current = head;

        while (current != null)
        {
            // Variable to compare all the nodes in the list with the current node
            LinkedLists.Node n = current;

            while (n.Next != null)
            {
                // Check for a duplicate value
                if (n.Next.Data == current.Data)
                {
                    // Remove the duplicate value
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
}


// Q11: Return Kth to Last: Implement an algorithm to find the kth to last element of a singly linked list.

// Time complexity O(n), Space complexity O(1)

class ReturnKthToLast
{
    public static LinkedLists.Node FindKthToLast(LinkedLists.Node head, int k)
    {
        if (k <= 0)
        {
            return null;
        }

        LinkedLists.Node p1 = head;
        LinkedLists.Node p2 = head;

        // Move p2 to get kth element's position
        for (int i = 0; i < k - 1; i++)
        {
            if (p2 == null) return null;

            p2 = p2.Next;
        }

        // Check if kth to the last element is null
        if (p2 == null) return null;

        // Move p1 to kth to the last element's position and p2 to the end of the linked list
        while (p2.Next != null)
        {
            p1 = p1.Next;
            p2 = p2.Next;
        }
        return p1;
    }
}


// Q12: Delete Middle Node: Implement an algorithm to delete a node in the middle (i.e., any node but the first and last node, not necessarily the exact middle) of a singly linked list, given only access to that node.

//      EXAMPLE:
//      Input: the node c from the linked list a -> b -> c -> d -> e -> f
//      Result: nothing is returned, but the new linked list looks like a -> b -> d -> e -> f

// Time complexity O(1), Space complexity O(1)

class DeleteMiddleNode
{
    public static bool DeleteNode(LinkedLists.Node node)
    {
        if (node == null || node.Next == null)
        {
            // Deletion not possible
            return false;
        }

        // Declare a variable to store the next node's value and set it to the next property of the accessible node
        LinkedLists.Node next = node.Next;

        // Set the value of the accessible node to the value of the 'next' variable
        node.Data = next.Data;

        // Remove the next node from the list
        node.Next = next.Next;

        // Deletion complete
        return true;
    }
}


// Q13: Partition: Write code to partition a linked list around a value x, such that all nodes less than x come before all nodes greater than or equal to x. If x is contained within the list, the values of x only need to be after the elements less than x. The partition element x can appear anywhere in the "right partition"; it does not need to appear between the left and right partitions.

//      EXAMPLE:
//      Input: 3 -> 5 -> 8 -> 5 -> 10 -> 2 -> 1 [partition = 5) 
//      Output: 3 -> 1 -> 2 -> 10 -> 5 -> 5 -> 8

// Time complexity O(n), Space complexity O(1)

class Partition
{
    public LinkedLists.Node PartitionList(LinkedLists.Node node, int x)
    {
        LinkedLists.Node head = node;
        LinkedLists.Node tail = node;

        // Iterate through the input linked list
        while (node != null)
        {
            // Temporary variable to keep track of the next node in the list
            LinkedLists.Node next = node.Next;

            if (node.Data < x)
            {
                // Append the head of the first new linked list
                node.Next = head;
                head = node;
            }
            else
            {
                // Append the tail of the second new linked list
                tail.Next = node;
                tail = node;
            }

            // Move on to the next node in the linked list
            node = next;
        }

        // End the new linked list
        tail.Next = null;

        // Return the new head, So newly generated linked list can be accessed
        return head;
    }
}


// Q14: Sum Lists: You have two numbers represented by a linked list, where each node contains a single digit. The digits are stored in reverse order, such that the 1's digit is at the head of the list. Write a function that adds the two numbers and returns the sum as a linked list.

//      EXAMPLE:
//      Input: (7 -> 1 -> 6) + (5 -> 9 -> 2) . That is, 617 + 295.
//      Output: 2 -> 1 -> 9. That is, 912. 

// Solution 1: Reverse Order

// Time complexity O(max(m,n)), Space complexity O(max(m,n))

class SumLists
{
    public LinkedLists.Node AddLists(LinkedLists.Node l1, LinkedLists.Node l2)
    {
        // Fake head to keep track of the new linked list's head
        LinkedLists.Node dummyHead = new LinkedLists.Node(0);
        LinkedLists.Node current = dummyHead;

        int carry = 0;
        // Iterate until both lists are null
        while (l1 != null || l2 != null)
        {
            int x = (l1 != null) ? l1.Data : 0;
            int y = (l2 != null) ? l2.Data : 0;
            int sum = carry + x + y;

            // Sum > 10 = 1, Sum < 10 = 0 
            carry = sum / 10;

            // Create new nodes in the solution list containing the sum
            current.Next = new LinkedLists.Node(sum % 10);

            // Move to the next node in the solution list
            current = current.Next;

            if (l1 != null) l1 = l1.Next;
            if (l2 != null) l2 = l2.Next;
        }

        // Add the last node in the solution list containing the carry if any
        if (carry > 0)
        {
            current.Next = new LinkedLists.Node(carry);
        }

        // return the original head of the solution list
        return dummyHead.Next;
    }

    // FOLLOW UP:
    // Suppose the digits are stored in forward order. Repeat the above problem.

    // EXAMPLE:
    // Input: (6 -> 1 -> 7) + (2 -> 9 -> 5). That is, 617 + 295. 
    // Output: 9 -> 1 -> 2. That is, 912.

    // Solution 2: Forward Order

    // Time complexity O(n), Space complexity O(n)

    private class PartialSum
    {
        // Default values of the output list and carry
        public LinkedLists.Node sum = null;
        public int carry = 0;
    }

    public static LinkedLists.Node AddListsForward(LinkedLists.Node l1, LinkedLists.Node l2)
    {
        int len1 = GetLength(l1);
        int len2 = GetLength(l2);

        // Insert zeros in the input lists to equalize the length of both
        if (len1 < len2)
        {
            l1 = PadList(l1, len2 - len1);
        }
        else
        {
            l2 = PadList(l2, len1 - len2);
        }

        // Call the AddListsHelper method to perform addition on the input lists
        var sum = AddListsHelper(l1, l2);

        if (sum.carry == 0)
        {
            return sum.sum;
        }

        // Add the carry to the head of the output list
        else
        {
            var result = InsertBefore(sum.sum, sum.carry);
            return result;
        }

    }

    // Method to perform addition on the input lists
    private static PartialSum AddListsHelper(LinkedLists.Node l1, LinkedLists.Node l2)
    {
        // Return default new PartialSum values after reaching the end of both input lists
        if (l1 == null && l2 == null)
        {
            return new PartialSum();
        }

        // Recursive call to AddListsHelper method to perform addition and calculate carry for remaining nodes of the input lists
        var sum = AddListsHelper(l1.Next, l2.Next);

        int val = sum.carry + l1.Data + l2.Data;

        // Call the InsertBefore method to insert the sum of the current nodes to the new node
        var fullResult = InsertBefore(sum.sum, val % 10);

        sum.sum = fullResult;
        sum.carry = val / 10;

        return sum;
    }

    // Method to insert zeros in the input lists
    private static LinkedLists.Node PadList(LinkedLists.Node l, int padding)
    {
        LinkedLists.Node head = l;

        for (int i = 0; i < padding; i++)
        {
            // Insert zeros at the head
            head = InsertBefore(head, 0);
        }
        return head;
    }

    // Helper method to insert zeros at the head
    private static LinkedLists.Node InsertBefore(LinkedLists.Node list, int data)
    {
        // New node to contain zero for padding
        LinkedLists.Node node = new LinkedLists.Node(data);

        if (list != null)
        {
            // Set the next node to be the head
            node.Next = list;
        }

        // Return the node containing zero
        return node;
    }

    // Method to get the lengths of the input lists
    private static int GetLength(LinkedLists.Node l)
    {
        int len = 0;

        while (l != null)
        {
            len++;
            l = l.Next;
        }
        return len;
    }
}


// Q15: Palindrome: Implement a function to check if a linked list is a palindrome.

// Time complexity O(n), Space complexity O(1)

class Palindrome
{
    public static bool IsPalindrome(LinkedLists.Node head)
    {
        // If list is empty then return true
        if (head == null || head.Next == null)
        {
            return true;
        }

        // Variables to help iterate to the middle of the list
        LinkedLists.Node slow = head;
        LinkedLists.Node fast = head;

        // Iterate to the middle of the list
        while (fast.Next != null && fast.Next.Next != null)
        {
            slow = slow.Next;
            fast = fast.Next.Next;
        }

        // Set the head for the second half of the list
        LinkedLists.Node secondHalfHead = slow.Next;

        // Reverse the second half of the list with the help of the head
        secondHalfHead = ReverseList(secondHalfHead);

        // Initialize the first half's and second half's heads for comparison
        LinkedLists.Node p1 = head;
        LinkedLists.Node p2 = secondHalfHead;

        bool result = true;

        // Iterate through both halves to compare if both have the same data
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
    public static LinkedLists.Node ReverseList(LinkedLists.Node head)
    {
        LinkedLists.Node prev = null;
        LinkedLists.Node current = head;

        while (current != null)
        {
            LinkedLists.Node next = current.Next;
            current.Next = prev;
            prev = current;
            current = next;
        }
        return prev;
    }
}


// Q16 Intersection: Given two(singly) linked lists, determine if the two lists intersect. Return the intersecting node. Note that the intersection is defined based on reference, not value. That is, if the kth node of the first linked list is the exact same node (by reference) as the jth node of the second linked list, then they are intersecting.

// Time complexity O(a + b), Space complexity O(1)

class Intersection
{
    public static LinkedLists.Node FindIntersection(LinkedLists.Node l1, LinkedLists.Node l2)
    {
        if (l1 == null || l2 == null)
        {
            return null;
        }

        // Get the size and tail of both lists
        Result result1 = GetSizeAndTail(l1);
        Result result2 = GetSizeAndTail(l2);

        // If tails are not equal then lists do not intersect
        if (result1.tail != result2.tail)
        {
            return null;
        }

        // Find out which list is shorter and which list is longer in size
        LinkedLists.Node shorter = result1.size < result2.size ? l1 : l2;
        LinkedLists.Node longer = result1.size < result2.size ? l2 : l1;

        // Move in the longer list to the same point as the starting point of the shorter list
        longer = GetKthNode(longer, Math.Abs(result1.size - result2.size));

        // Iterate to the tails of both lists and check if it is the same
        while (shorter != longer)
        {
            shorter = shorter.Next;
            longer = longer.Next;
        }

        // Return shorter or longer, both are equal at the tail collision point
        return longer;
    }

    // Method to get the size and tail for the lists
    private static Result GetSizeAndTail(LinkedLists.Node list)
    {
        if (list == null)
        {
            return null;
        }

        int size = 1;
        LinkedLists.Node current = list;

        while (current.Next != null)
        {
            size++;
            current = current.Next;
        }

        // Return the tail and size
        return new Result(current, size);
    }

    // Method to get to the same starting points for both lists
    private static LinkedLists.Node GetKthNode(LinkedLists.Node head, int k)
    {
        LinkedLists.Node current = head;

        while (k > 0 && current != null)
        {
            current = current.Next;
            k--;
        }

        // Return the kth node's location as the head of the list
        return current;
    }

    // Helper class for the GetSizeAndTail method
    public class Result
    {
        public LinkedLists.Node tail;

        public int size;

        public Result(LinkedLists.Node tail, int size)
        {
            this.tail = tail;
            this.size = size;
        }
    }
}


// Q17: Loop Detection: Given a circular linked list, implement an algorithm that returns the node at the beginning of the loop.

//      DEFINITION:
//      Circular linked list: A (corrupt) linked list in which a node's next pointer points to an earlier node, so as to make a loop in the linked list.

//      EXAMPLE:
//      Input: A -> B -> C -> 0 -> E -> C [the same C as earlier]
//      Output: C

// Time complexity O(n), Space complexity O(1)

class LoopDetection
{
    public static LinkedLists.Node FindLoopStart(LinkedLists.Node head)
    {
        // Set both starting nodes to the head
        LinkedLists.Node slow = head;
        LinkedLists.Node fast = head;

        // Iterate till both nodes collide
        while (fast.Next != null || fast.Next.Next != null)
        {
            slow = slow.Next;
            fast = fast.Next.Next;

            // If collision is detected
            if (slow == fast)
            {
                break;
            }
        }

        // If no collision is detected there is no loop in the list
        if (fast.Next == null || fast.Next.Next == null)
        {
            return null;
        }

        // Set 'slow' back to 'head' again
        slow = head;

        // Iterate till both nodes are at the same position
        while (slow != fast)
        {
            slow = slow.Next;
            fast = fast.Next;
        }

        // Return either, because both are at the loop start position
        return fast;
    }
}


//                                                                   CHAPTER 3: STACKS & QUEUES


class StacksAndQueues
{
    // Stacks(LIFO) & Queues(FIFO)
}

// Q18: Three in One: Describe how you could use a single array to implement three stacks.

// Time complexity O(1), Space complexity O(n)

class ThreeInOne
{
    public class FixedMultiStack
    {
        // Can change the number of stacks in the array as required
        private int numberOfStacks = 3;
        private int stackCapacity;

        private int[] values;
        private int[] sizes;

        // Stacks in an array with fixed sizes
        public FixedMultiStack(int stackSize)
        {
            stackCapacity = stackSize;
            values = new int[stackSize * numberOfStacks];
            sizes = new int[numberOfStacks];
        }

        // Method to add a new element on top of the stack
        public void Push(int stackNum, int value)
        {
            if (isFull(stackNum))
            {
                throw new StackOverflowException();
            }
            sizes[stackNum]++;
            values[indexOfTop(stackNum)] = value;
        }

        // Method to remove the top element from the stack
        public int Pop(int stackNum)
        {
            if (isEmpty(stackNum))
            {
                throw new Exception("Stack is empty");
            }
            int topIndex = indexOfTop(stackNum);
            int value = values[topIndex];
            values[topIndex] = 0;
            sizes[stackNum]--;
            return value;

        }

        // Method for looking up the top value in the stack 
        public int Peek(int stackNum)
        {
            if (isEmpty(stackNum))
            {
                throw new Exception("Stack is empty");
            }
            return values[indexOfTop(stackNum)];
        }

        // Method to check if the stack is empty in the array
        public bool isEmpty(int stackNum)
        {
            return sizes[stackNum] == 0;
        }

        // Method to check if the stack has reached its full capacity in the array
        public bool isFull(int stackNum)
        {
            return sizes[stackNum] == stackCapacity;
        }

        // Method to find the top most element's index for the stack inside the array
        public int indexOfTop(int stackNum)
        {
            int offset = stackNum * stackCapacity;
            int size = sizes[stackNum];

            return offset + size - 1;
        }
    }
}


// Q19: Stack Min: How would you design a stack which, in addition to push and pop, has a function min which returns the minimum element? Push, pop and min should all operate in 0(1) time.

// Time complexity O(1), Space complexity O(n)

class StackMin
{
    public class StackWithMin
    {
        // Original stack
        private Stack<int> mainStack;

        // Stack to keep track of the minimum element's value
        private Stack<int> minStack;

        public StackWithMin()
        {
            mainStack = new Stack<int>();
            minStack = new Stack<int>();
        }

        // Method to add values to the stacks
        public void Push(int value)
        {
            mainStack.Push(value);

            // If a minimum value is found add it to the other stack
            if (minStack.Count == 0 || value <= minStack.Peek())
            {
                minStack.Push(value);
            }
        }

        // Method to remove values from the stacks
        public int Pop()
        {
            // Check if the original stack is empty
            if (mainStack.Count == 0)
            {
                throw new Exception("Stack is empty");
            }

            // Remove the element at the top of the stack
            int value = mainStack.Pop();

            // If the value to be removed is equal to the minimum value in the stack, remove it from the other stack also
            if (value == minStack.Peek())
            {
                minStack.Pop();
            }

            // return the removed value
            return value;
        }

        // Method to look up the minimum value in the stack
        public int Min()
        {
            if (minStack.Count == 0)
            {
                throw new Exception("Stack is empty");
            }
            return minStack.Peek();
        }
    }
}


// Q20: Stack of Plates: Imagine a (literal) stack of plates. If the stack gets too high, it might topple.Therefore, in real life, we would likely start a new stack when the previous stack exceeds some threshold.Implement a data structure SetOfStacks that mimics this. SetOfStacks should be composed of several stacks and should create a new stack once the previous one exceeds capacity.SetOfStacks.push() and SetOfStacks.pop() should behave identically to a single stack (that is, pop() should return the same values as it would if there were just a single stack).

//      FOLLOW UP:
//      Implement a function popAt(int index) which performs a pop operation on a specific sub-stack.

// Time complexity O(1) in best case & 0(n) in worst case, Space complexity O(n)

class StackOfPlates
{
    public class SetOfStacks
    {
        // Create a list of stacks
        private List<Stack<int>> stacks = new List<Stack<int>>();

        private int capacity;

        // Set the capacity for the stacks
        public SetOfStacks(int capacity)
        {
            this.capacity = capacity;
        }

        // Method to push an element at the top of the last stack
        public void Push(int value)
        {
            Stack<int> last = GetLastStack();

            if (last != null && last.Count < capacity)
            {
                last.Push(value);
            }
            else
            {
                // Create a new stack if previous stack is full
                Stack<int> stack = new Stack<int>();

                stack.Push(value);

                // Add the new stack into the list
                stacks.Add(stack);
            }
        }

        // Method to remove the top element from the last stack
        public int Pop()
        {
            Stack<int> last = GetLastStack();

            if (last == null)
            {
                throw new Exception("Stack is empty");
            }

            // Store the removed value
            int value = last.Pop();

            // Remove the last stack if its empty
            if (last.Count == 0)
            {
                stacks.RemoveAt(stacks.Count - 1);
            }

            // return the removed value
            return value;
        }

        // Method to remove an element from a specific sub-stack
        public int PopAt(int index)
        {
            return LeftShift(index, true);
        }

        // Method to shift the stacks in the list to the left after removing an element from a sub-stack
        public int LeftShift(int index, bool removeTop)
        {
            Stack<int> stack = stacks[index];

            // Variable to store removed element
            int removedItem;

            if (removeTop)
            {
                removedItem = stack.Pop();
            }
            else
            {
                removedItem = RemoveBottom(stack);
            }

            // Remove the sub-stack from the list if its empty
            if (stack.Count == 0)
            {
                stacks.RemoveAt(index);
            }
            else if (stacks.Count > index + 1)
            {
                // Recursive call to shift the elements to the left
                int v = LeftShift(index + 1, false);

                stack.Push(v);
            }

            // Return the value of the removed element
            return removedItem;
        }

        // Helper method for the LeftShift method to remove the bottom elements from the stacks
        public int RemoveBottom(Stack<int> stack)
        {
            // For storing the elements of the original stack in reverse order temporarily
            Stack<int> temp = new Stack<int>();

            // Store all the elements of the original stack in the temporary stack
            while (stack.Count > 0)
            {
                temp.Push(stack.Pop());
            }

            // Store the topmost element
            int bottom = temp.Pop();

            // Return all temporarily stored elements to the original stack in proper order
            while (temp.Count > 0)
            {
                stack.Push(temp.Pop());
            }

            // Return the top element in the stack
            return bottom;
        }

        // Method to get the last stack in the list
        public Stack<int> GetLastStack()
        {
            if (stacks.Count == 0)
            {
                return null;
            }
            return stacks[stacks.Count - 1];
        }
    }
}


// Q21: Queue via Stacks: Implement a MyQueue class which implements a queue using two stacks.

// Time complexity O(1), Space complexity O(n)

class QueueViaStacks
{
    public class MyQueue<T>
    {
        // Two stacks to store elements in original and reverse order
        private Stack<T> stackNewest, stackOldest;

        public MyQueue()
        {
            stackNewest = new Stack<T>();
            stackOldest = new Stack<T>();
        }

        // Method to calculate the size of the stacks
        public int size()
        {
            return stackNewest.Count + stackOldest.Count;
        }

        // Method to add an element to the stack
        public void Add(T value)
        {
            stackNewest.Push(value);
        }

        // Method to reverse the stack to act as a queue
        private void ShiftStacks()
        {
            if (stackOldest.Count == 0)
            {
                while (stackNewest.Count > 0)
                {
                    stackOldest.Push(stackNewest.Pop());
                }
            }
        }

        // Method to look up the first element in the queue
        public T Peek()
        {
            ShiftStacks();
            return stackOldest.Peek();
        }

        // Method to remove the first element from the queue
        public T Remove()
        {
            ShiftStacks();
            return stackOldest.Pop();
        }
    }
}


// Q22: Sort Stack: Write a program to sort a stack such that the smallest items are on the top. You can use an additional temporary stack, but you may not copy the elements into any other data structure (such as an array). The stack supports the following operations: push, pop, peek, and isEmpty.

// Time complexity O(n^2), Space complexity O(n)

class SortStack
{
    public static void Sort(Stack<int> s)
    {
        // Stack to store the elements for comparison
        Stack<int> r = new Stack<int>();

        while (s.Count > 0)
        {
            // Variable to temporarily store removed element
            int temp = s.Pop();

            // Compare and add the bigger values at the bottom
            while (r.Count > 0 && r.Peek() > temp)
            {
                s.Push(r.Pop());
            }

            // Store the smaller value in the 'r' stack
            r.Push(temp);
        }

        // Return the temporarily stored elements to the original stack
        while (r.Count > 0)
        {
            s.Push(r.Pop());
        }
    }
}


// Q23: Animal Shelter: An animal shelter, which holds only dogs and cats, operates on a strictly "first in, first out" basis. People must adopt either the "oldest" (based on arrival time) of all animals at the shelter, or they can select whether they would prefer a dog or a cat (and will receive the oldest animal of that type). They cannot select which specific animal they would like. Create the data structures to maintain this system and implement operations such as enqueue, dequeueAny, dequeueDog, and dequeueCat. You may use the built-in Linked List data structure. 

// Time complexity O(1), Space complexity O(n)

class AnimalShelter
{
    // Class to hold the information of all the animals
    public abstract class Animal
    {
        // Variable to track the order of the animals in the list
        private int order;

        // Variable for storing either a cat or a dog
        protected string name;

        public Animal(string n)
        {
            name = n;
        }

        // Method to set the order number for an animal
        public void SetOrder(int ord)
        {
            order = ord;
        }

        // Method to get the order number for an animal
        public int GetOrder()
        {
            return order;
        }

        // Method to find out the older animal
        public bool isOlderThan(Animal a)
        {
            return this.order < a.GetOrder();
        }
    }

    // Dog subclass
    public class Dog : Animal
    {
        // Constructor to call the base class constructor for name
        public Dog(string n) : base(n) { }
    }

    // Cat subclass
    public class Cat : Animal
    {
        // Constructor to call the base class constructor for name
        public Cat(string n) : base(n) { }
    }

    public class AnimalQueue
    {
        // Initialize linked lists for dogs and cats
        LinkedList<Dog> dogs = new LinkedList<Dog>();
        LinkedList<Cat> cats = new LinkedList<Cat>();

        private int order = 0;

        // Method to add animals to the lists
        public void Enqueue(Animal a)
        {
            // Set the order number for the animals to keep track of the oldest animal
            a.SetOrder(order);
            order++;

            // If the name of the animal is Dog
            if (a is Dog)
            {
                // Add a dog at the end of the linked list
                dogs.AddLast((Dog)a);
            }

            // If the name of the animal is cat
            else if (a is Cat)
            {
                // Add a cat at the end of the linked list
                cats.AddLast((Cat)a);
            }
        }

        // Method to remove the oldest of all animals
        public Animal DequeueAny()
        {
            // If no dogs in shelter, remove from cats
            if (dogs.Count == 0)
            {
                return DequeueCat();
            }

            // If no cats in shelter, remove from dogs
            else if (cats.Count == 0)
            {
                return DequeueDog();
            }

            // Get the first(oldest) animal's value in the linked list
            Dog dog = dogs.First.Value;
            Cat cat = cats.First.Value;

            // If dog is older remove from dogs otherwise remove from cats
            if (dog.isOlderThan(cat))
            {
                return DequeueDog();
            }
            else
            {
                return DequeueCat();
            }
        }

        // Method to remove a dog from the 'dogs' list
        public Dog DequeueDog()
        {
            Dog dog = dogs.First.Value;

            dogs.RemoveFirst();

            // return the removed dog
            return dog;
        }

        // Method to remove a cat from the 'cats' list
        public Cat DequeueCat()
        {
            Cat cat = cats.First.Value;

            cats.RemoveFirst();
            // return the removed cat
            return cat;
        }
    }
}


//                                                                   CHAPTER 4: TREES & GRAPHS


class TreesAndGraphs
{
    // Tree Traversal Methods


    // In-Order Traversal:

    // void InOrderTraversal(TreeNode node) 
    // { 
    //     if (node != null) 
    //     { 
    //         InOrderTraversal(node.left); All nodes on the left first
    //         Visit(node); Then current node
    //         InOrderTraversal(node.right); Then all nodes on the right
    //     } 
    // }


    // Pre-Order Traversal:

    // void PreOrderTraversal(TreeNode node)
    // {
    //     if (node != null)
    //     {
    //         Visit(node); Current node first
    //         PreOrderTraversal(node.left); Then all nodes on the left
    //         PreOrderTraversal(node.right); Then all nodes on the right
    //     }
    // }


    // Post-Order Traversal:

    // void PostOrderTraversal(TreeNode node)
    // {
    //     if (node != null)
    //     {
    //         PostOrderTraversal(node.left); All nodes on the left first
    //         PostOrderTraversal(node.right); Then all nodes on the right
    //         Visit(node); Then the current node
    //     }
    // }


    // Graph & Tree Searching Methods


    // Depth-First Search(DFS):

    // void Search(Node root)
    // {
    //     Visit(root); Start from the root
    //     root.Visited = true;

    //     foreach (Node n in root.Adjacent)
    //     {
    //         if (n.Visited == false)
    //         {
    //             Search(n);
    //         }
    //     }
    // }


    // Breadth-First Search(BFS):

    // void Search(Node root)
    // {
    //     Queue queue = new Queue(); Create a queue
    //     root.Marked = true; Mark root as searched
    //     queue.Enqueue(root); Add at the end of the queue

    //     while (!queue.IsEmpty())
    //     {
    //         Node r = queue.Dequeue(); Remove from the front of the queue
    //         Visit(r); Search in the queue

    //         foreach (Node n in r.Adjacent)
    //         {
    //             if (n.Marked = false) If not marked as searched
    //             {
    //                 n.Marked = true; Mark as searched
    //                 queue.Enqueue(n); Add to be searched in the queue 
    //             }
    //         }
    //     }
    //  }


    // Graph implementation
    public class Graph
    {
        // List of node objects to represent nodes in the graph
        public List<Node> Nodes { get; set; }

        public Graph()
        {
            Nodes = new List<Node>();
        }

        // Method to add nodes in the graph
        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }
    }

    public class Node
    {
        // Represents data stored in the node
        public int Data { get; set; }

        // List of node objects representing the nodes adjacent to a node 
        public List<Node> Adjacent { get; set; }

        // Represents the state of the node
        public State State { get; set; }

        public Node(int data)
        {
            Data = data;
            Adjacent = new List<Node>();
        }

        // Method to add edges between the nodes
        public void AddAdjacent(Node node)
        {
            Adjacent.Add(node);
        }
    }
}

// Q24: Route Between Nodes: Given a directed graph, design an algorithm to find out whether there is a route between two nodes.

// Time complexity O(v + e), Space complexity O(v)  v = Vertices OR Nodes, e = Edges

public enum State { Unvisited, Visiting, Visited }

class RouteBetweenNodes
{
    public bool IsRouteBetweenNodes(TreesAndGraphs.Graph graph, TreesAndGraphs.Node start, TreesAndGraphs.Node end)
    {
        if (start == end)
        {
            return true;
        }

        // Queue to store nodes to be searched
        Queue<TreesAndGraphs.Node> queue = new Queue<TreesAndGraphs.Node>();

        // Set all the nodes in the graph to unvisited state
        foreach (TreesAndGraphs.Node node in graph.Nodes)
        {
            node.State = State.Unvisited;
        }

        start.State = State.Visiting;

        // Add the start node to the queue
        queue.Enqueue(start);
        TreesAndGraphs.Node current;

        // Keep iterating till the queue is empty and all nodes have been visited
        while (queue.Count > 0)
        {
            // Take the first node from the queue and set it to the current node
            current = queue.Dequeue();

            if (current != null)
            {
                // Check all the nodes adjacent to the current node
                foreach (TreesAndGraphs.Node adjacent in current.Adjacent)
                {
                    // If the adjacent nodes haven't been searched
                    if (adjacent.State == State.Unvisited)
                    {
                        // If the adjacent node matches the end node there is a path between the nodes
                        if (adjacent == end)
                        {
                            return true;
                        }

                        // Add all the adjacent nodes to the queue to be searched
                        else
                        {
                            adjacent.State = State.Visiting;
                            queue.Enqueue(adjacent);
                        }
                    }
                }
                current.State = State.Visited;
            }
        }

        // Return false when no path is found
        return false;
    }
}


// Q25: Minimal Tree: Given a sorted (increasing order) array with unique integer elements, write an algorithm to create a binary search tree with minimal height.

// Time complexity O(n), Space complexity O(log n)

class MinimalTree
{
    public class TreeNode
    {
        // Represents the data stored in the node
        public int Value { get; set; }

        // Represents a child node on the left
        public TreeNode left { get; set; }

        // Represents a child node on the right
        public TreeNode right { get; set; }

        // Represents the parent of the current node
        public TreeNode parent { get; set; }

        public TreeNode(int value)
        {
            Value = value;
            left = null;
            right = null;
            parent = null;
        }
    }

    // Method that represents the sorted input array
    public TreeNode CreateMinimalBST(int[] array)
    {
        // Call the private method with three arguments: input array, index of the first element in the input array, index of the last element in the input array
        return CreateMinimalBST(array, 0, array.Length - 1);
    }

    // Method with three parameters: sub-array representing the input array, the start index of the sub-array to process, the end index of the sub-array to process
    private TreeNode CreateMinimalBST(int[] array, int start, int end)
    {
        if (end < start)
        {
            return null;
        }

        // Calculate the middle index of the sub-array
        int mid = (start + end) / 2;
        TreeNode node = new TreeNode(array[mid]);

        // Recursive call to set the smaller elements as the left property of the new TreeNode
        node.left = CreateMinimalBST(array, start, mid - 1);

        // Recursive call to set the larger elements as the right property of the new TreeNode
        node.right = CreateMinimalBST(array, mid + 1, end);

        return node;
    }
}


// Q26: List of Depths: Given a binary tree, design an algorithm which creates a linked list of all the nodes at each depth(e.g., if you have a tree with depth D, you'll have D linked lists).

// Time complexity O(n), Space complexity O(n)

class ListOfDepths
{
    public List<LinkedList<MinimalTree.TreeNode>> CreateLevelLinkedList(MinimalTree.TreeNode root)
    {
        // List to contain all the linked lists
        List<LinkedList<MinimalTree.TreeNode>> result = new List<LinkedList<MinimalTree.TreeNode>>();

        // Linked list to keep track of the current nodes in the binary tree
        LinkedList<MinimalTree.TreeNode> current = new LinkedList<MinimalTree.TreeNode>();

        if (root != null)
        {
            current.AddLast(root);
        }

        // Iterate through the tree until all the nodes are in their respective lists
        while (current.Count > 0)
        {
            // Add the linked list with the current nodes to the list
            result.Add(current);

            // Linked list to keep track of the nodes at the current level of the binary tree
            LinkedList<MinimalTree.TreeNode> parents = current;

            // Linked list to keep track of the nodes at the next level of the binary tree
            current = new LinkedList<MinimalTree.TreeNode>();

            foreach (MinimalTree.TreeNode parent in parents)
            {
                // Check if the left child node is null
                if (parent.left != null)
                {
                    // Add the left child node to the linked list
                    current.AddLast(parent.left);
                }

                // Check if the right child node is null
                if (parent.right != null)
                {
                    // Add the right child node to the linked list
                    current.AddLast(parent.right);
                }
            }
        }

        // Return the list containing all the generated linked lists
        return result;
    }
}


// Q:27: Check Balanced: Implement a function to check if a binary tree is balanced. For the purposes of this question, a balanced tree is defined to be a tree such that the heights of the two subtrees of any node never differ by more than one.

// Time complexity O(n), Space complexity O(h), h = Height of the tree

class CheckBalanced
{
    public bool IsBalanced(MinimalTree.TreeNode root)
    {
        // Return the height of the subtree rooted at the given tree
        return CheckHeight(root) != int.MinValue;
    }

    // Method to check the height of the subtrees
    private int CheckHeight(MinimalTree.TreeNode root)
    {
        // Check if the subtree is empty
        if (root == null)
        {
            return -1;
        }

        // Recursive call to check the height of the left subtree
        int leftHeight = CheckHeight(root.left);

        if (leftHeight == int.MinValue)
        {
            return int.MinValue;
        }

        // Recursive call to check the height of the right subtree
        int rightHeight = CheckHeight(root.right);

        if (rightHeight == int.MinValue)
        {
            return int.MinValue;
        }

        // Calculate the height difference between the left and right subtrees
        int heightDiff = leftHeight - rightHeight;

        // Check if the height difference is more than required
        if (Math.Abs(heightDiff) > 1)
        {
            return int.MinValue;
        }

        // Both subtrees are balanced
        else
        {
            return Math.Max(leftHeight, rightHeight) + 1;
        }
    }
}


// Q28: Validate BST: Implement a function to check if a binary tree is a binary search tree.

// Time complexity O(n), Space complexity O(h)

class ValidateBST
{
    public bool IsBST(MinimalTree.TreeNode root)
    {
        return IsBST(root, int.MinValue, int.MaxValue);
    }

    // Method to search all the nodes in the tree recursively
    private bool IsBST(MinimalTree.TreeNode node, int min, int max)
    {
        // An empty tree is considered to be a BST
        if (node == null)
        {
            return true;
        }

        // Check if the value of the node satisfies the BST invariant
        if (node.Value < min || node.Value > max)
        {
            return false;
        }

        // Recursive call to check the left and right nodes in the tree
        return IsBST(node.left, min, node.Value - 1) && IsBST(node.right, node.Value + 1, max);
    }


    // Q29: Successor: Write an algorithm to find the "next" node (i.e., in-order successor) of a given node in a binary search tree. You may assume that each node has a link to its parent.

    // Time complexity O(h), Space complexity O(1)

    public MinimalTree.TreeNode InOrderSuccessor(MinimalTree.TreeNode n)
    {
        if (n == null)
        {
            return null;
        }

        // If node has a right subtree, leftmost node in the right subtree is the successor
        if (n.right != null)
        {
            // Set current node to the right child node
            MinimalTree.TreeNode current = n.right;

            // Iterate to the leftmost node in the subtree
            while (current.left != null)
            {
                current = current.left;
            }
            return current;
        }

        // If there is no right subtree, go up
        else
        {
            MinimalTree.TreeNode current = n;
            MinimalTree.TreeNode parent = n.parent;

            // Traverse up the tree till the left child of the parent is the current node (successor is found)
            while (parent != null && parent.left != current)
            {
                current = parent;
                parent = parent.parent;
            }

            // Return the found successor
            return parent;
        }
    }
}


// Q30: Build Order: You are given a list of projects and a list of dependencies (which is a list of pairs of projects, where the second project is dependent on the first project). All of a project's dependencies must be built before the project is. Find a build order that will allow the projects to be built. If there is no valid build order, return an error. (FHQ)

//      EXAMPLE:
//      Input: projects: a, b, c, d, e, f
//             dependencies: (a, d), (f, b), (b, d), (f, a), (d, c)
//      Output: f, e, a, b, d, c

// Time complexity O(p + d), Space complexity O(p + d), p = Number of projects, d = Number of dependency pairs

class BuildOrder
{
    // Main method to find a valid build order for the given projects
    public static Stack<char> FindBuildOrder(char[] projects, Tuple<char, char>[] dependencies)
    {
        // Create a graph representing dependencies between the given projects
        NewGraph graph = BuildGraph(projects, dependencies);

        // Find a valid build order for the projects in the graph and return in a stack
        return OrderProjects(graph.nodes);
    }

    public static NewGraph BuildGraph(char[] projects, Tuple<char, char>[] dependencies)
    {
        // Create a graph to represent dependencies between the given projects
        NewGraph graph = new NewGraph();

        // Iterate over all the projects in the given array of projects
        foreach (char project in projects)
        {
            // Create a new node in the graph for the current project
            graph.CreateNode(project);
        }

        // Iterate over each dependency in the given array of dependencies
        foreach (Tuple<char, char> dependency in dependencies)
        {
            char first = dependency.Item1;
            char second = dependency.Item2;

            // Connect the two projects in the graph (second is dependent on the first)
            graph.AddEdge(first, second);
        }

        // Return the graph representing dependencies between the given projects
        return graph;
    }

    // Method to create build order for the graph
    public static Stack<char> OrderProjects(List<Project> projects)
    {
        // Stack to store the build order
        Stack<char> stack = new Stack<char>();

        // Iterate over each project in the list of projects
        foreach (Project project in projects)
        {
            // Check if the project has not been visited
            if (project.state == Project.StateType.BLANK)
            {
                // Check if a valid build order is possible by performing a Depth-First search on the graph
                if (!DoDFS(project, stack))
                {
                    // No valid build order is possible
                    return null;
                }
            }
        }

        // Return the stack containing the valid build order
        return stack;
    }

    // Method to perform DFS on the graph
    public static bool DoDFS(Project project, Stack<char> stack)
    {
        // Check if the project has already been visited
        if (project.state == Project.StateType.PARTIAL)
        {
            // There is a cycle in the graph (no valid build order possible)
            return false;
        }

        // Check if the project has not been visited
        if (project.state == Project.StateType.BLANK)
        {
            // Set the project's state to be visited
            project.state = Project.StateType.PARTIAL;

            // Get the list of dependent projects of the current project
            List<Project> children = project.children;

            // Iterate through the dependent child projects
            foreach (Project child in children)
            {
                // Recursive call to check if a valid build order is possible
                if (!DoDFS(child, stack))
                {
                    // No valid build order is possible
                    return false;
                }
            }

            // Valid build order is possible, Set the project as complete
            project.state = Project.StateType.COMPLETE;

            // Push completed project on the stack
            stack.Push(project.name);
        }

        // Return true if the project is already complete
        return true;
    }

    public class NewGraph
    {
        // List of projects in the graph
        public List<Project> nodes { get; } = new List<Project>();

        // Map names of projects in the graph to the corresponding project
        public Dictionary<char, Project> map { get; } = new Dictionary<char, Project>();

        // Method to create a new project in the graph
        public Project CreateNode(char name)
        {
            // Check if the project of this name is already mapped
            if (!map.ContainsKey(name))
            {
                Project node = new Project(name);

                // Add new project to list of projects in the graph and map it
                nodes.Add(node);
                map.Add(name, node);
            }
            return map[name];
        }

        // Method to connect the dependent project and the project it is dependent on
        public void AddEdge(char startName, char endName)
        {
            // Ensure that the projects exist in the graph
            Project start = CreateNode(startName);
            Project end = CreateNode(endName);

            // Connect both projects
            start.AddNeighbor(end);
        }
    }

    public class Project
    {
        // Possible states of the project
        public enum StateType { COMPLETE, PARTIAL, BLANK };

        // List of children projects on which the project depends
        public List<Project> children { get; } = new List<Project>();

        // Map names of the children projects to the corresponding projects
        public Dictionary<char, Project> map { get; } = new Dictionary<char, Project>();

        // Represents the name of the project
        public char name { get; }

        // Represents the current state of the project
        public StateType state { get; set; } = StateType.BLANK;

        // Constructor
        public Project(char n) { name = n; }

        // Method to add a project as a child of the project
        public void AddNeighbor(Project node)
        {
            // Check if the project already exists in the map
            if (!map.ContainsKey(node.name))
            {
                // Add to the map and children list
                children.Add(node);
                map.Add(node.name, node);
            }
        }
    }
}


// Q31: First Common Ancestor: Design an algorithm and write code to find the first common ancestor of two nodes in a binary tree. Avoid storing additional nodes in a data structure. NOTE: This is not necessarily a binary search tree.

// Time complexity O(n), Space complexity O(h) 

class FirstCommonAncestor
{
    public class Result
    {
        public MinimalTree.TreeNode node;
        public bool isAncestor;

        // Constructor to initialize the variables
        public Result(MinimalTree.TreeNode n, bool isAnc)
        {
            node = n;
            isAncestor = isAnc;
        }
    }

    // Main method to find the common ancestor
    public MinimalTree.TreeNode CommonAncestor(MinimalTree.TreeNode root, MinimalTree.TreeNode p, MinimalTree.TreeNode q)
    {
        // Call to helper method to perform search of the tree recursively
        Result r = CommonAncestorHelper(root, p, q);

        // If ancestor is found, return the node at which it is found
        if (r.isAncestor)
        {
            return r.node;
        }

        // There is no ancestor
        return null;
    }

    // Helper method for the CommonAncestor method
    public Result CommonAncestorHelper(MinimalTree.TreeNode root, MinimalTree.TreeNode p, MinimalTree.TreeNode q)
    {
        // If the tree is empty
        if (root == null)
        {
            return new Result(null, false);
        }

        // If root is equal to both nodes, root is the ancestor
        if (root == p && root == q)
        {
            return new Result(root, true);
        }

        // Recursive call to check the left children of the root node in the subtree
        Result rx = CommonAncestorHelper(root.left, p, q);

        if (rx.isAncestor)
        {
            return rx;
        }

        // Recursive call to check the right children of the root node in the subtree
        Result ry = CommonAncestorHelper(root.right, p, q);

        if (ry.isAncestor)
        {
            return ry;
        }

        // If both nodes are not null after the search, root is the ancestor
        if (rx.node != null && ry.node != null)
        {
            return new Result(root, true);
        }

        // Check if both nodes were found in the same subtree
        else if (root == p || root == q)
        {
            bool isAncestor = rx.node != null || ry.node != null;

            // Same subtree, root is ancestor
            return new Result(root, isAncestor);
        }
        else
        {
            // No ancestor was found
            return new Result(rx.node != null ? rx.node : ry.node, false);
        }
    }
}


// Q32: BST Sequences: A binary search tree was created by traversing through an array from left to right and inserting each element.Given a binary search tree with distinct elements, print all possible arrays that could have led to this tree. (FHQ)

//      EXAMPLE:
//      Input:     (2)
//                /   \
//              (1)   (3)

//      Output: { 2, 1, 3}, {2, 3, 1}

// Time complexity O(n!), Space complexity O(n!)

class BSTSequence
{
    List<LinkedList<int>> AllSequences(MinimalTree.TreeNode node)
    {
        // List to store the result
        List<LinkedList<int>> result = new List<LinkedList<int>>();

        // if the given node is null
        if (node == null)
        {
            // Add empty node to the result list
            result.Add(new LinkedList<int>());

            return result;
        }

        // Store prefix
        LinkedList<int> prefix = new LinkedList<int>();

        // Add the value of the given node to the prefix list
        prefix.AddLast(node.Value);

        // Iterate through all right and left nodes in the tree
        List<LinkedList<int>> leftSeq = AllSequences(node.left);
        List<LinkedList<int>> rightSeq = AllSequences(node.right);

        // Iterate over all possible pairs of sequences 
        foreach (LinkedList<int> left in leftSeq)
        {
            foreach (LinkedList<int> right in rightSeq)
            {
                // List to store the weaved sequences
                List<LinkedList<int>> weaved = new List<LinkedList<int>>();

                // Call method to weave the left and right sequences together
                WeaveLists(left, right, weaved, prefix);

                // Add all weaved sequences to the result
                foreach (LinkedList<int> item in weaved)
                {
                    result.Add(item);
                }
            }
        }

        return result;
    }

    // Helper method to weave the lists of sequences together
    void WeaveLists(LinkedList<int> first, LinkedList<int> second, List<LinkedList<int>> results, LinkedList<int> prefix)
    {
        // If any of the lists is empty
        if (first.Count == 0 || second.Count == 0)
        {
            // Create new result list with all elements from the prefix list
            LinkedList<int> result = new LinkedList<int>(prefix);

            // Add all elements from all the lists to the result list
            foreach (int item in first)
            {
                result.AddLast(item);
            }

            foreach (int item in second)
            {
                result.AddLast(item);
            }

            // Add the new result list to the original results list and return
            results.Add(result);

            return;
        }

        // Store the first element from the 'first' list
        int headFirst = first.First.Value;

        // Remove the first element from the 'first' list
        first.RemoveFirst();

        // Add the stored element to the prefix list
        prefix.AddLast(headFirst);

        // Recursive call to store the updated lists
        WeaveLists(first, second, results, prefix);

        // Reset the original element of the first list
        prefix.RemoveLast();
        first.AddFirst(headFirst);

        // Store the first element from the 'second' list
        int headSecond = second.First.Value;

        // Remove the first element from the 'second' list
        second.RemoveFirst();

        // Add the stored element to the prefix list
        prefix.AddLast(headSecond);

        // Recursive call to store the updated lists
        WeaveLists(first, second, results, prefix);

        // Reset the original element of the second list
        prefix.RemoveLast();
        second.AddFirst(headSecond);
    }
}


// Q33: Check Subtree: T1 and T2 are two very large binary trees, with T1 much bigger than T2. Create an algorithm to determine if T2 is a subtree of T1.

//      A tree T2 is a subtree of T1 if there exists a node n in T1 such that the subtree of n is identical to T2. That is, if you cut off the tree at node n, the two tree would be identical.

// Time complexity O(n + km), Space complexity O(log n + log m) n = number of nodes in tree T1, m = number of nodes in tree T2

class CheckSubtree
{
    public bool IsSubtree(MinimalTree.TreeNode T1, MinimalTree.TreeNode T2)
    {
        // Empty tree is considered to be a valid subtree
        if (T2 == null)
        {
            return true;
        }

        // Bigger tree is null, there is no subtree
        if (T1 == null)
        {
            return false;
        }

        // Check if the elements in both trees are identical
        if (AreIdentical(T1, T2))
        {
            return true;
        }

        // Recursive call to compare all elements of the bigger tree with the elements of the subtree
        return IsSubtree(T1.left, T2) || IsSubtree(T1.right, T2);
    }

    // Helper method for the IsSubtree method
    public bool AreIdentical(MinimalTree.TreeNode root1, MinimalTree.TreeNode root2)
    {
        // Empty roots of both trees means that they are identical
        if (root1 == null && root2 == null)
        {
            return true;
        }

        // If one root is null and the other is not that means they are not identical
        if (root1 == null || root2 == null)
        {
            return false;
        }

        // Recursively compare all the right and left subtrees of both roots
        return root1.Value == root2.Value && AreIdentical(root1.left, root2.left) && AreIdentical(root1.right, root2.right);
    }
}


// Q34: Random Node: You are implementing a binary tree class from scratch which, in addition to insert, find, and delete, has a method getRandomNode() which returns a random node from the tree.All nodes should be equally likely to be chosen. Design and implement an algorithm for getRandomNode, and explain how you would implement the rest of the methods.

// Time complexity O(h), Space complexity O(h)

class RandomNode
{
    // Initialize a BinaryTree class
    public class BinaryTree
    {
        // Represents data in the node
        public int Data;

        // Represents the size of the tree
        public int Size;

        // Represents left child node
        public BinaryTree Left;

        // Represents right child node
        public BinaryTree Right;

        // Constructor to initialize variables
        public BinaryTree(int data)
        {
            Data = data;
            Size = 1;
        }

        // Method to insert nodes in the BinaryTree in the correct order
        public void InsertInOrder(int data)
        {
            // If the value is less than or equal to the current node's value, insert it into the left subtree
            if (data <= Data)
            {
                // If left child of the current node is empty
                if (Left == null)
                {
                    // Create new node with the given value
                    Left = new BinaryTree(data);
                }
                else
                {
                    // Recursive call to insert nodes into the left subtree
                    Left.InsertInOrder(data);
                }
            }

            // If the value is greater than the current node's value, insert it into the right subtree
            else
            {
                // If right child of the current node is empty
                if (Right == null)
                {
                    // Create a new node with the given value
                    Right = new BinaryTree(data);
                }
                else
                {
                    // Recursive call to insert nodes into the right subtree
                    Right.InsertInOrder(data);
                }
            }
            // Increment the size with each insertion
            Size++;
        }

        // Method to find a specific value inside the tree
        public BinaryTree Find(int data)
        {
            // If current node's value is equal to the given value
            if (data == Data)
            {
                return this;
            }

            // If given value is less than the current node's value, search in the left subtree recursively
            else if (data < Data)
            {
                return Left != null ? Left.Find(data) : null;
            }

            // If given value is greater than the current node's value, search in the right subtree recursively
            else if (data > Data)
            {
                return Right != null ? Right.Find(data) : null;
            }
            // Value does not exist in the BinaryTree
            return null;
        }

        // Method to delete a specific node from the BinaryTree
        public void Delete(int data, BinaryTree parent)
        {
            // If the given value is less than the current node's value, search and delete value from the left subtree
            if (data < Data)
            {
                if (Left != null)
                {
                    Left.Delete(data, this);
                }
            }

            // If the given value is greater than the current node's value, search and delete value from the right subtree
            else if (data > Data)
            {
                if (Right != null)
                {
                    Right.Delete(data, this);
                }
            }

            // If the value is equal to the current node's value, delete the current node
            else
            {
                // If the current node has both right and left children
                if (Left != null && Right != null)
                {
                    // Find the smallest value in the right subtree and assign it to the current node
                    Data = Right.Minvalue();

                    // Delete the smallest value in the right subtree
                    Right.Delete(Data, this);
                }

                // Update the parent node's reference to point to the right or left node that comes after the current deleted node
                else if (parent.Left == this)
                {
                    parent.Left = (Left != null) ? Left : Right;
                }
                else if (parent.Right == this)
                {
                    parent.Right = (Left != null) ? Left : Right;
                }
            }
        }

        // Method to find the smallest value
        public int Minvalue()
        {
            // If there is no left subtree the current node's value is the smallest
            if (Left == null)
            {
                return Data;
            }

            // If there is a left subtree find the smallest value in that subtree recursively
            else
            {
                return Left.Minvalue();
            }
        }

        // Method to get a random value from the BinaryTree
        public BinaryTree GetRandomNode()
        {
            Random random = new Random();

            // Get the size of the left subtree
            int leftSize = Left == null ? 0 : Left.Size;

            // Generate a random index between 0 and size of the BinaryTree
            int index = random.Next(Size);

            // If random number is less than the size of the left subtree, recursively call GetRandomNode on the left child
            if (index < leftSize)
            {
                return Left.GetRandomNode();
            }

            // If random number is equal to the size of the left subtree, return the random number
            else if (index == leftSize)
            {
                return this;
            }

            // If random number is greater than the size of the left subtree, recursively call GetRandomNode on the right child
            else
            {
                return Right.GetRandomNode();
            }
        }
    }
}


// Q35: Paths with Sum: You are given a binary tree in which each node contains an integer value (which might be positive or negative). Design an algorithm to count the number of paths that sum to a given value.The path does not need to start or end at the root or a leaf, but it must go downwards (traveling only from parent nodes to child nodes).

// Time complexity O(n), Space complexity O(n)

class PathsWithSum
{
    // Main method to count paths with sum
    int CountPathsWithSum(MinimalTree.TreeNode root, int targetSum)
    {
        // Overloaded method call
        return CountPathsWithSum(root, targetSum, 0, new Dictionary<int, int>());
    }

    // Overloaded method that counts the paths that sum to a given value
    int CountPathsWithSum(MinimalTree.TreeNode node, int targetSum, int runningSum, Dictionary<int, int> pathCount)
    {
        // If the tree is empty there are no paths
        if (node == null)
        {
            return 0;
        }

        // Add value of the current node to the runningSum
        runningSum += node.Value;

        // Calculate the difference between runningSum and targetSum
        int sum = runningSum - targetSum;

        // Check how many paths have been seen with this sum, if there are paths add them to the count
        int totalPaths = pathCount.ContainsKey(sum) ? pathCount[sum] : 0;

        // If runningSum is equal to targetSum add this path to the count
        if (runningSum == targetSum)
        {
            totalPaths++;
        }

        // Increment the count of paths with current running sum in the dictionary
        IncrementHashTable(pathCount, runningSum, 1);

        // Continue counting paths on both sides recursively and add them to the count
        totalPaths += CountPathsWithSum(node.left, targetSum, runningSum, pathCount);
        totalPaths += CountPathsWithSum(node.right, targetSum, runningSum, pathCount);

        // Decrement the count of paths with current running sum in the dictionary
        IncrementHashTable(pathCount, runningSum, totalPaths - 1);

        // Return the count
        return totalPaths;
    }

    // Helper method to increment or decrement count in the dictionary
    void IncrementHashTable(Dictionary<int, int> hashTable, int key, int delta)
    {
        // Check if the same count is already in the dictionary
        int newCount = hashTable.ContainsKey(key) ? hashTable[key] + delta : delta;

        // If the count is 0, remove it from the dictionary, else update its value with newCount
        if (newCount == 0)
        {
            hashTable.Remove(key);
        }
        else
        {
            hashTable[key] = newCount;
        }
    }
}


//                                                                   CHAPTER 5: BIT MANIPULATION


class BitManipulation
{
    // Bitwise complement operator '~': The ~ operator produces a bitwise complement of its operand by reversing each bit.

    // Input(32 bit):   x = 00001111000011110000111100001100
    // Output:         ~x = 11110000111100001111000011110011


    // Left-shift operator '<<': The << operator shifts its left-hand operand left by the number of bits defined by its right-hand operand. The left-shift operation discards the   
    //                           high-order bits that are outside the range of the result type and sets the low-order empty bit positions to zero.

    // Input(32 bit):  x =      11001001000000000000000000010001
    // Output:         x << 4 = 10010000000000000000000100010000


    // Right-shift operator '>>': The >> operator shifts its left-hand operand right by the number of bits defined by its right-hand operand. The right-shift operation discards the
    //                            low-order bits.

    // Input(4 bit):  x =      1001
    // Output:        x >> 2 = 0010


    // The high-order empty bit positions are set based on the type of the left-hand operand as follows:

    //   If the left-hand operand is of type int or long, the right-shift operator performs an arithmetic shift: the value of the most significant bit (the sign bit) of the left hand operand is propagated to the high-order empty bit positions. That is, the high-order empty bit positions are set to zero if the left-hand operand is non-negative and set to one if it's negative.

    // Input(32 bit):  a = int.MinValue (Negative) = 10000000000000000000000000000000
    // Output:         a >> 3 =                      11110000000000000000000000000000


    //   If the left-hand operand is of type uint or ulong, the right-shift operator performs a logical shift: the high-order empty bit positions are always set to zero.

    // Input(32 bit):  c =      10000000000000000000000000000000
    // Output:         c >> 3 = 00010000000000000000000000000000


    // Unsigned right-shift operator '>>>': The >>> operator shifts its left-hand operand right by the number of bits defined by its right-hand operand. The >>> operator always performs a logical shift. That is, the high-order empty bit positions are always set to zero, regardless of the type of the left-hand operand. The >> operator performs an arithmetic shift (that is, the value of the most significant bit is propagated to the high-order empty bit positions) if the left-hand operand is of a signed type. The following example demonstrates the difference between >> and >>> operators for a negative left-hand operand:

    // Input(32 bit): x = -8 =  11111111111111111111111111111000
    // Output:        x >> 2 =  11111111111111111111111111111110
    //                x >>> 2 = 00111111111111111111111111111110


    // Logical AND operator '&': The & operator computes the bitwise logical AND of its integral operands. If both bits are 1, the result is 1; otherwise, the result is 0.

    // Input(8 bit):  a =         11111000
    //                b =         10011101
    // Output:        c = a & b = 10011000


    // Logical exclusive OR operator '^': The ^ operator computes the bitwise logical exclusive OR, also known as the bitwise logical XOR, of its integral operands. If one bit is 1 and the other bit is 0, the result is 1; otherwise, the result is 0.

    // Input(8 bit): a =         11111000
    //               b =         00011100
    // Output:       c = a ^ b = 11100100


    // Logical OR operator '|': The | operator computes the bitwise logical OR of its integral operands. If at least one bit is 1, the result is 1; otherwise, the result is 0.

    // Input(8 bit): a =         10100000
    //               b =         10010001
    // Output:       c = a | b = 10110001


    // Addition '+': To perform binary addition, we start from the rightmost bit and add the corresponding bits from the two numbers, along with any carry from the previous column. If the sum of the bits is 2, we write down a 0, If the sum of the bits is 3, we write down a 1, and carry over a 1 to the next column.

    // Input(4 bit): a =         1001  |  1111
    //               b =         0110  |  1111
    // Output        c = a + b = 1111  | 11110


    // Subtraction '-': To perform binary subtraction, we start from the rightmost bit and subtract the corresponding bits from the two numbers, along with any borrow from the previous column. If the bit in the subtrahend (the number being subtracted) is larger than the bit in the minuend (the number being subtracted from), we borrow a 1 from the next column.

    // Input(4 bit): a =         1111 | 1001
    //               b =         1111 | 0110
    // Output:       c = a - b = 0000 | 0011


    // Multiplication '*': Multiply the multiplier by each digit of the multiplicand to achieve intermediate products, whose last digit is in the position of the corresponding multiplicand digit. The final product is the sum of those intermediate products.

    // Input(4 bit): a =         0011 | 0011
    //               b =         0011 | 0101
    // Output:       c = a * b = 1001 | 1111
}

// Q36: Insertion: You are given two 32-bit numbers, N and M, and two bit positions, i and j. Write a method to insert M into N such that M starts at bit j and ends at bit i. You can assume that the bits j through i have enough space to fit all of M. That is, if M = 10011, you can assume that there are at least 5 bits between j and i. You would not, for example, have j = 3 and i = 2, because M could not fully fit between bit 3 and bit 2.

// EXAMPLE:
// Input:  N = 10000000000, M = 10011, i = 2, j = 6
// Output: N = 10001001100

// Time complexity O(1), Space complexity O(1)

class Insertion
{
    int InsertBits(int N, int M, int i, int j)
    {
        // Create a mask to clear bits i through j in N
        int allOnes = ~0;

        // Move 1s to before j's position and add 0s after j's position
        int left = allOnes << (j + 1);

        // Insert 1's after i's position
        int right = ((1 << i) - 1);

        // Combine left and right to get all 1s, except for 0s between i and j
        int mask = left | right;

        // Clear bits j through i in N
        int n_cleared = N & mask;

        // Move M to the correct position
        int m_shifted = M << i;

        // Insert M into N and return the result
        return n_cleared | m_shifted;
    }
}


// Q37: Binary to String: Given a real number between 0 and 1 (e.g., 0.72) that is passed in as a double, print the binary representation. If the number cannot be represented accurately in binary with at most 32 characters, print "ERROR:'

// Time complexity O(k), Space complexity O(1), k = number of bits required to represent the binary representation of the input number

class BinaryToString
{
    string PrintBinary(double num)
    {
        // Check if the input number is within the valid range (between 0 and 1)
        if (num <= 0 || num >= 1)
        {
            return "ERROR";
        }

        // Create a StringBuilder object to build the binary representation of the number
        StringBuilder binary = new StringBuilder();

        // The binary representation starts with a decimal point, so the method appends a “.” to the StringBuilder
        binary.Append(".");

        // Loop that continues until the input number becomes 0
        while (num > 0)
        {
            // Check if the length of the binary representation has reached 32 characters
            if (binary.Length >= 32)
            {
                return "ERROR";
            }

            double r = num * 2;

            // If r is greater than or equal to 1, it means that the next bit in the binary representation is 1
            if (r >= 1)
            {
                binary.Append(1);

                // Update the input number by subtracting 1 from r
                num = r - 1;
            }

            // If r is less than 1, it means that the next bit in the binary representation is 0
            else
            {
                binary.Append(0);

                // Update the input number to be equal to r
                num = r;
            }
        }

        // Return the binary representation of the input number as a string
        return binary.ToString();
    }
}


// Q38: Flip Bit to Win: You have an integer and you can flip exactly one bit from a 13 to a 1. Write code to find the length of the longest sequence of ls you could create.

// EXAMPLE:
// Input: 1775 (or: 1113111131111)
// Output: 8

// Time complexity O(b), Space complexity O(1), b = number of bits in the binary representation of the input integer

class FlipBitToWin
{
    public static int FlipBit(int a)
    {
        // if ~a is equal to 0, it means that all bits of a are already set to 1
        if (~a == 0)
        {
            return int.MaxValue;
        }

        // To keep track of previous, current and max number of consecutive 1s
        int previousLength = 0;
        int currentLength = 0;
        int maxLength = 1;

        // Iterate through input integer bits from right to left
        while (a != 0)
        {
            // If the least significant bit of a is set to 1
            if ((a & 1) == 1)
            {
                currentLength++;
            }

            // If the least significant bit of a is set to 0
            else if ((a & 1) == 0)
            {
                // Update the value of previousLength
                previousLength = (a & 2) == 0 ? 0 : currentLength;
                currentLength = 0;
            }

            // Update the value of maxLength, which is between its current value and the sum of previousLength, currentLength, and 1, 1 represents the bit that can be flipped from  0 to 1
            maxLength = Math.Max(previousLength + currentLength + 1, maxLength);

            // Shift the bits of a, one position to the right 
            a >>= 1;
        }
        // Return the length of the longest sequence of consecutive 1s
        return maxLength;
    }
}


// Q39: Next Number: Given a positive integer, print the next smallest and the next largest number that have the same number of 1 bits in their binary representation.

// Time complexity O(n), Space complexity O(1)

class NextNumber
{
    public static (int nextSmallest, int nextLargest) GetNextNumbers(int n)
    {
        int nextSmallest = n;
        int nextLargest = n;

        // Set count to number of 1s in n
        int count = CountBits(n);

        // Call helper method to count number of 1s in nextSmallest and nextLargest numbers and then compare with the number of 1s in n
        while (CountBits(--nextSmallest) != count) ;
        while (CountBits(++nextLargest) != count) ;

        // Return a tuple containing the values of nextSmallest and nextLargest
        return (nextSmallest, nextLargest);
    }

    // Helper method to count 1s
    private static int CountBits(int n)
    {
        // To count the number of 1s in n
        int count = 0;

        // Iterate over all bits of n
        while (n != 0)
        {
            // Increment count if 1 is set
            count += n & 1;

            // Move to the next bit by right shifting n by one bit
            n >>= 1;
        }
        return count;
    }
}


// Q40: Debugger: Explain what the following code does: ((n & (n - 1) == 0).

// This code states that n &(AND) (n-1) is equal to 0, this statement is true if n and n-1 have no 1s in common.


// Q41: Conversion: Write a function to determine the number of bits you would need to flip to convert integer A to integer B.

// EXAMPLE:
// Input:  29 (or: 11101), 15 (or: 01111)
// Output: 2

// Time complexity O(n), Space complexity O(1)

class Debugger
{
    int BitSwapsRequired(int a, int b)
    {
        // To count the number of swaps required
        int count = 0;

        // Iterate till c becomes 0
        // Step 1: Calculate a OR b to get the number of swaps required, each bit is set to 1 if the corresponding bits in a and b are different, and 0 if they are the same
        // Step 2: Clear the least significant bit of c by performing a bitwise AND operation with c-1
        // Step 3: Increment the count
        for (int c = a ^ b; c != 0; c = c & (c - 1))
        {
            count++;
        }
        return count;
    }
}


// Q41: Pairwise Swap: Write a program to swap odd and even bits in an integer with as few instructions as possible(e.g., bit 13 and bit 1 are swapped, bit 2 and bit 3 are swapped, and so on).

// Time complexity O(1), Space complexity O(1)

class PairwiseSwap
{
    uint SwapOddEvenBit(uint x)
    {
        // Step 1: The hexadecimal constant 0xAAAAAAAA has all its odd bits set to 1, so x & 0xAAAAAAAA extracts the odd bits from x, Similarly, the constant 0x55555555 has all its even bits set to 1, so x & 0x55555555 extracts the even  bits from x
        // Step 2: Use the right shift operator '>>' to move the extracted odd bits one position to the right, effectively swapping them with the even bits. Similarly, use the left shift operator '<<' to move the extracted even bits one position to the left
        // Step 3: Use the bitwise OR operator '|' to combine the shifted odd and even bits into a new integer, where the odd and even bits have been swapped
        return ((x & 0xAAAAAAAA) >> 1) | ((x & 0x55555555) << 1);
    }
}


// Q42: Draw Line: A monochrome screen is stored as a single array of bytes, allowing eight consecutive pixels to be stored in one byte. The screen has width w, where w is divisible by 8 (that is, no byte will be split across rows). The height of the screen, of course, can be derived from the length of the array and the width. Implement a function that draws a horizontal line from(x1, y) to(x2, y). (FHQ)

// The method signature should look something like: DrawLine(byte[] screen, int width, int x1, int x2, int y)

// Time complexity O(w), Space complexity O(1), w = width of the screen in pixels

class DrawLine
{
    void DrawHorizontalLine(byte[] screen, int width, int x1, int x2, int y)
    {
        int startOffset = x1 % 8;
        int firstFullByte = x1 / 8;

        if (startOffset != 0)
        {
            firstFullByte++;
        }

        int endOffset = x2 % 8;
        int lastFullByte = x2 / 8;

        if (endOffset != 7)
        {
            lastFullByte--;
        }

        for (int b = firstFullByte; b <= lastFullByte; b++)
        {
            screen[(width / 8) * y + b] = (byte)0xFF;
        }

        byte startMask = (byte)(0xFF >> startOffset);
        byte endMask = (byte)(0xFF >> endOffset + 1);

        if ((x1 / 8) == (x2 / 8))
        {
            byte mask = (byte)(startMask & endMask);
            screen[(width / 8) * y + (x1 / 8)] |= mask;
        }
        else
        {
            if (startOffset != 0)
            {
                int byteNumber = (width / 8) * y + firstFullByte - 1;
                screen[byteNumber] |= startMask;
            }

            if (endOffset != 7)
            {
                int byteNumber = (width / 8) * y + lastFullByte + 1;
                screen[byteNumber] |= endMask;
            }
        }
    }
}


//                                                                   CHAPTER 6: MATH & LOGIC PUZZLES


class MathAndLogicPuzzles
{
    // Extra Questions:


    // Q1: You have two ropes, and each takes exactly one hour to burn. How would you use them to time exactly 15 minutes? Note that the ropes are of uneven densities, so half the rope length-wise does not necessarily take half an hour to burn.

    // Step 1: Light rope 1 at both ends and rope 2 at one end. 
    // Step 2: When the two flames on Rope 1 meet. 30 minutes will have passed. Rope 2 has 30 minutes left of burn-time.
    // Step 3: At that point, light Rope 2 at the other end. 
    // Step 4: In exactly fifteen minutes, Rope 2 will be completely burnt.


    // Q2: You have nine balls. Eight are of the same weight, and one is heavier. You are given a balance which tells you only whether the left side or the right side is heavier. Find the heavy ball in just two uses of the scale.

    // Step 1: Divide the balls into sets of three items each.
    // Step 2: Put first set of three balls on one side of the scale and second set on the other side.
    // Step 3: If one of these sets is heavier then the heavier ball is in that set, if both sets have equal weight then the heavier ball is in the third set.
    // Step 4: Take the set with the heavier ball, put one ball on each side of the scale and keep one ball off to the side.
    // Step 5: One of the balls on the scale will be heavier or if the balls on the scale have equal weight the third ball is heavier.


    // Q3: You have 8 guests on a birthday party and only one cake, the cake is perfectly round or square, divide the cake into 8 equal pieces in 3 cuts.

    // Step 1: Cut the cake in half vertically from the top.
    // Step 2: Cut the cake in half again horizontally from the top.
    // Step 3: Cut the cake in half in the middle from the side


    // Q43: The Heavy Pill: You have 20 bottles of pills. 19 bottles have 1.0 gram pills, but one has pills of weight 1.1 grams. Given a scale that provides an exact measurement, how would you find the heavy bottle? You can only use the scale once.

    // If we took one pill from Bottle #1 and two pills from Bottle #2, what would the scale show? It depends. If Bottle #1 were the heavy bottle, we would get 3.1 grams. If Bottle #2 were the heavy bottle, we would get 3.2 grams.

    // We know the "expected" weight of a bunch of pills. The difference between the expected weight and the actual weight will indicate which bottle contributed the heavier pills, provided we select a different number of pills from each bottle.

    // Step 1: Take one pill from Bottle #1, two pills from Bottle #2, three pills from Bottle #3, and so on.
    // Step 2: Weigh this mix of pills. If all pills were one gram each, the scale would read 210 grams. Any "overage" must come from the extra 0.1 gram pills.
    // Step 3: Calculate the bottle number with the formula weight - 210grams / 0.1grams, So, if the set of pills weighed 211.3 grams, then Bottle #13 would have the heavy pills.


    // Q44: Basketball: You have a basketball hoop and someone says that you can play one of two games. Game 1: You get one shot to make the hoop. Game 2: You get three shots and you have to make two of three shots. If p is the probability of making a particular shot, for which values of p should you pick one game or the other?

    // Probability of winning Game 1: The probability of winning Game 1 is p, by definition. 
    // Probability of winning Game 2: Let s(k, n) be the probability of making exactly k shots(s) out of n. The probability of winning Game 2 is the probability of making exactly two shots out of three OR making all three shots. In other words:

    // P(winning) = s(2, 3) + s(3, 3)

    // The probability of making all three shots is:

    // s (3, 3) = p^3

    // The probability of making exactly two shots is:

    //   P(making 1 and 2, and missing 3) + P(making 1 and 3, and missing 2) + P(missing 1, and making 2 and 3):
    // = p * p * (1 - p) + p * (1 - p) * p + (1 - p) * p * p, [p =  making, (1 - p) = missing]
    // = 3 (1 - p) p^2

    // Adding both probabilities together, we get:

    //   p^3 + 3 (1 - p) p^2
    // = p^3 + 3p^2 - 3p^3
    // = 3p^2 - 2p^3, [Shift: 3p^2 - 3p^3 - p^3 = 3p^2 2p^3]

    // Which game should you play? 
    // You should play Game 1 if P (Game 1) > P (Game 2):

    //   p > 3p^2 - 2p^3
    // = 1 > 3p - 2p^2, [Took p common and canceled it out]
    // = 2p^2 - 3p + 1 > 0, [Shift: 3p - 2p^2 becomes 2p^2 - 3p]
    // = (2p - 1)(p - 1) > 0, [Formula]

    // Both terms must be positive, or both must be negative. But we know p < 1, so p - 1 < 0. 
    // This means both terms must be negative:

    //   2p - 1 < 0
    // = 2p < 1, [Shift: - 1 becomes 1]
    // = p < 0.5 [Shift & Divide: 1 / 2 = 0.5]

    // So, we should play Game 1 if 0 < p < 0.5 and Game 2 if 0.5 < p < 1. 
    // If p = 0, 0.5, or 1, then P (Game 1) = P (Game 2), so it doesn't matter which game we play.


    // Q45: Dominoes: There is an 8x8 chessboard in which two diagonally opposite corners have been cut off. You are given 31 dominoes, and a single domino can cover exactly two squares. Can you use the 31 dominoes to cover the entire board? Prove your answer (by providing an example or showing why it's impossible).

    // At first, it seems like this should be possible. It's an 8 x 8 board, which has 64 squares, but two have been cut off, so we're down to 62 squares. A set of 31 dominoes should be able to fit there, right? When we try to lay down dominoes on row 1, which only has 7 squares, we may notice that one domino must stretch into the row 2. Then, when we try to lay down dominoes onto row 2, again we need to stretch a domino into row 3. For each row we place, we'll always have one domino that needs to poke into the next row. No matter how many times and ways we try to solve this issue, we won't be able to successfully lay down all the dominoes. There's a cleaner, more solid proof for why it won't work. The chessboard initially has 32 black and 32 white squares. By removing opposite corners (which must be the same color), we're left with 30 of one color and 32 of the other color. Let's say, for the sake of argument, that we have 30 black and 32 white squares. Each domino we set on the board will always take up one white and one black square. Therefore, 31 dominoes will take up 31 white squares and 31 black squares exactly. On this board, however, we must have 30 black squares and 32 white squares. Hence, it is impossible.

    // Q46: Ants on a Triangle: There are three ants on different vertices of a triangle. What is the probability of collision (between any two or all of them) if they start walking on the sides of the triangle? Assume that each ant randomly picks a direction, with either direction being equally likely to be chosen, and that they walk at the same speed. Similarly, find the probability of collision with n ants on an n-vertex polygon.

    // The ants will collide if any of them are moving towards each other. So, the only way that they won't collide is if they are all moving in the same direction (clockwise or counterclockwise).

    // P (clockwise) = (1 / 2)^3 
    // P (counter clockwise) = (1 / 2)^3 
    // P (same direction) = (1 / 2)^3 + (1 / 2)^3 = (1 / 4)

    // The probability of collision is therefore the probability of the ants not moving in the same direction:

    // P (collision) = 1 - P (same direction) = 1 - (1 / 4) = 3 / 4.

    // To generalize this to an n-vertex polygon: there are still only two ways in which the ants can move to avoid a collision, but there are 2n ways they can move in total.
    // Therefore, in general, probability of collision is:

    // P (clockwise) = (1 / 2)^n 
    // p (counter clockwise) = (1 / 2)^n 
    // P (same direction) = 2 (1 / 2)^n = (1 / 2)^n-1, [(2) * (1 / 2)^n = (2) * (1^n / 2^n) {Solve bracket (1 / 2)^n}, = (2^1 / 2^n) {Cancel 2 with 2}, = (2^1 / 2^n) * (1) {Multiply by 1}, = (1 / 2^n-1) = (1 / 2)^n-1 {Law of exponents = if base is the same exponents can be added or subtracted}]
    // P (collision) = 1 - P (same direction) = 1 - (1 / 2)^n-1


    // Q46: Jugs of Water: You have a five-quart jug, a three-quart jug, and an unlimited supply of water (but no measuring cups). How would you come up with exactly four quarts of water? Note that the jugs are oddly shaped, such that filling up exactly "half" of the jug would be impossible.

    // Step 1: Fill 5-quart, [5 - 0]
    // Step 2: Fill 3-quart with 5-quart's contents, [2, 3]
    // Step 3: Dump 3-quart, [2, 0]
    // Step 4: Fill 3-quart with 5-quart's contents, [0, 2]
    // Step 5: Fill 5-quart, [5, 2]
    // Step 6: Fill remainder of 3-quart with 5-quart, [4, 3]
    // Step 7: There is no step 7 you fool, we have 4 quarts in the 5-quart jug


    // Q47: Blue-Eyed Island: A bunch of people are living on an island, when a visitor comes with a strange order: all blue-eyed people must leave the island as soon as possible. There will be a flight out at 8:00 pm every evening. Each person can see everyone else's eye color, but they do not know their own (nor is anyone allowed to tell them). Additionally, they do not know how many people have blue eyes, although they do know that at least one person does. How many days will it take the blue-eyed people to leave?

    // Assume that there are n people on the island and c of them have blue eyes. We are explicitly told that c > 8.

    // Case c = 1: Exactly one person has blue eyes. Assuming all the people are intelligent, the blue-eyed person should look around and realize that no one else has blue eyes. Since he knows that at least one person has blue eyes, he must conclude that it is he who has blue eyes. Therefore, he would take the flight that evening. 

    // Case c = 2: Exactly two people have blue eyes. The two blue-eyed people see each other, but are unsure whether c is 1 or 2. They know, from the previous case, that if c = 1, the blue-eyed person would leave on the first night. Therefore, if the other blue-eyed person is still there, he must deduce that c = 2, which means that he himself has blue eyes. Both men would then leave on the second night. 

    // Case c > 2: The General Case. As we increase c, we can see that this logic continues to apply. If c = 3, then those three people will immediately know that there are either 2 or 3 people with blue eyes. If there were two people, then those two people would have left on the second night. So, when the others are still around after that night, each person would conclude that c = 3 and that they, therefore, have blue eyes too. They would leave that night. This same pattern extends up through any value of c. Therefore, if c men have blue eyes, it will take c nights for the blue-eyed men to leave. All will leave on the same night.


    // Q48: The Apocalypse: In the new post-apocalyptic world, the world queen is desperately concerned else they face massive fines. If all families abide by this policy-that is, they continue to have children until they have one girl, at which point they immediately stop-what will the gender ratio of the new generation be? (Assume that the odds of someone having a boy or a girl on any given pregnancy is equal.) Solve this out logically and then write a computer simulation of it.

    // At first glance, this seems wrong. The policy is designed to favor girls as it ensures that all families have a girl. On the other hand, the families that keep having children contribute (potentially) multiple boys to the population. This could offset the impact of the "one girl" policy. One way to think about this is to imagine that we put all the gender sequence of each family into one giant string. So if family 1 has BG, family 2 has BBG, and family 3 has G, we would write BGBBGG. In fact, we don't really care about the groupings of families because we're concerned about the population as a whole. As soon as a child is born, we can just append its gender (B or G) to the string. What are the odds of the next character being a G? Well, if the odds of having a boy and girl is the same, then the odds of the next character being a G is 50%. Therefore, roughly half of the string should be Gs and half should be Bs, giving an even gender ratio. This actually makes a lot of sense. Biology hasn't been changed. Half of newborn babies are girls and half are boys. Abiding by some rule about when to stop having children doesn't change this fact. Therefore, the gender ratio is 50% girls and 50% boys.
}

// Time complexity O(n), Space complexity O(1)

class TheApocalypse
{
    // Computer simulation for 'The Apocalypse'
    double RunFamilies(int n)
    {
        // To keep track of boys in all families
        int boys = 0;

        // To keep track of girls in all families
        int girls = 0;

        // Iterate through all the families to find the total number of boys and girls
        for (int i = 0; i < n; i++)
        {
            // Call helper method to count boys and girls in each family
            int[] genders = RunOneFamily();

            // Add the count of girls to the first element of the array
            girls += genders[0];

            // Add the count of girls to the second element of the array
            boys += genders[1];
        }

        // Return the ratio of girls to the total number of children
        return girls / (double)(girls + boys);
    }

    // Helper method to count boys and girls in each family
    int[] RunOneFamily()
    {
        // To randomly select a boy or a girl
        Random random = new Random();

        // To keep track of boys in one family
        int boys = 0;

        // To keep track of girls in one family
        int girls = 0;

        // Loop until a girl is born
        while (girls == 0)
        {
            // If random number is 0, increment the number of girls in the family
            if (random.Next(2) == 0)
            {
                girls++;
            }

            // If random number is 1, increment the number of boys in the family
            else
            {
                boys++;
            }
        }

        // Array to keep the count of boys and girls
        int[] genders = { girls, boys };

        // Return the count
        return genders;
    }
}


// Q49: The Egg Drop Problem: There is a building of 100 floors. If an egg drops from the Nth floor or above, it will break. If it's dropped from any floor below, it will not break. You're given two eggs. Find N, while minimizing the number of drops for the worst case.

// Minimum number of drops for the worst case are 14, we start from the 14th floor and check, if the egg breaks we go down from there if the egg doesn't break then we go further up 14 floors and check and so on, when we get to the 84th floor, we  divide the remaining floors in half and check from that floor.

// 14 + 14 = 28 + 14 = 42 + 14 = 56 + 14 = 70 + 14 = 84 + 8 = 92 + 8 = 100

// Time complexity O(1), Space complexity O(1)

class TheEggDropProblem
{
    int breakingPoint;
    int countDrops = 0;

    // Function to count the number of times the egg is dropped and check if the egg breaks
    bool Drop(int floor)
    {
        // Increase the count at each drop
        countDrops++;

        // Check if the egg breaks
        return floor >= breakingPoint;
    }

    // Method to find the floor at which the egg breaks
    int FindBreakingPoint(int floors)
    {
        // Minimum number of drops in worst case
        int interval = 14;

        // To keep track of the floor at which the egg breaks
        int previousFloor = 0;
        int egg1 = interval;

        // Iterate through the floors until the first egg breaks or exceeds the number of floors
        while (!Drop(egg1) && egg1 <= floors)
        {
            interval -= 1;
            previousFloor = egg1;
            egg1 += interval;
        }

        // Initialize the second egg after the first egg breaks or exceeds the number of floors
        int egg2 = previousFloor + 1;

        // Iterate the floors until the second egg breaks or reaches the floor at which the first egg broke or exceeds the number of floors
        while (egg2 < egg1 && egg2 <= floors && !Drop(egg2))
        {
            egg2 += 1;
        }

        // If the second egg exceeded the number of floors then return -1, else return the breaking point of the egg;
        return egg2 > floors ? -1 : egg2;
    }
}

// Q50: 100 Lockers: There are 100 closed lockers in a hallway. A man begins by opening all 100 lockers. Next, he closes every second locker. Then, on his third pass, he toggles every third locker(closes it if it is open or opens it if it is closed). This process continues for 100 passes, such that on each pass i, the man toggles every ith locker. After his 100th pass in the hallway, in which he toggles only locker #100, how many lockers are open?

// A door n is toggled once for each factor of n, including itself and 1. That is, door 15 is toggled on rounds 1, 3,5, and 15. A door is left open if the number of factors (which we will call x) is odd. You can think about this by pairing factors off as an open and a close. If there's one remaining, the door will be open. The value x is odd if n is a perfect square. Here's why: pair n's factors by their complements. For example, if n is 36, the factors are (1, 36), (2, 18), (3, 12), (4, 9), (6,6). Note that (6, 6) only contributes one factor, thus giving n an odd number of factors. There are 10 perfect squares. You could count them (1 , 4,9, 16, 25, 36,49,64, 81, 100)' or you could simply realize that you can take the numbers 1 through 10 and square them:

// 1 * 1, 2 * 2, 3 * 3, ... , 10 * 10

// Therefore, there are 10 lockers open at the end of this process.


// Q51: Poison: You have 1000 bottles of soda, and exactly one is poisoned. You have 10 test strips which can be used to detect poison. A single drop of poison will turn the test strip positive permanently. You can put any number of drops on a test strip at once and you can reuse a test strip as many times as you'd like (as long as the results are negative). However, you can only run tests once per day and it takes seven days to return a result. How would you figure out the poisoned bottle in as few days as possible?

// FOLLOW UP: Write code to simulate your approach.

// Time complexity O(numBottles * log(numBottles)), Space complexity O(1)

class Poison
{
    class PoisonTest
    {
        static void Main(string[] args)
        {
            // Number of total bottles
            int numBottles = 1000;

            // Number of test strips
            int numStrips = 10;

            // Randomly select the poisoned bottle
            int poisonBottle = new Random().Next(numBottles);

            Console.WriteLine("Poisoned Bottle: " + poisonBottle);

            // Array to contain the test strips and keep track of strips with positive and negative results
            bool[] testStrips = new bool[numStrips];

            // Iterate over all the bottles
            for (int i = 0; i < numBottles; i++)
            {
                // Calculate binary representation for the bottle number
                int bottleNum = i;
                int stripNum = 0;

                while (bottleNum > 0)
                {
                    // Check if the bottle is poisoned
                    if ((bottleNum & 1) == 1 && i == poisonBottle)
                    {
                        // Set the test strip corresponding with the poisoned bottle to true
                        testStrips[stripNum] = true;
                    }

                    bottleNum >>= 1;
                    stripNum++;
                }
            }
            int result = 0;

            // Read the results of the test strips by iterating over the testStrips array in reverse order
            for (int i = numStrips - 1; i >= 0; i--)
            {
                result <<= 1;

                // Construct a binary number from the positive test strips
                if (testStrips[i])
                {
                    result |= 1;
                }
            }

            // Print the result onto the console
            Console.WriteLine("Result: " + result);
        }
    }
}


//                                                                   CHAPTER 7: OBJECT-ORIENTED DESIGN


// Q52: Deck of Cards: Design the data structures for a generic deck of cards. Explain how you would subclass the data structures to implement blackjack. (FHQ)

class DeckOfCards
{
    // Suits of a standard deck of cards
    public enum Suit
    {
        Club = 0, Diamond = 1, Heart = 2, Spade = 3
    }

    // Class that represents a deck of cards
    public class Deck<T> where T : Card
    {
        // List of cards in the deck
        private List<T> cards;

        // To keep track of how many cards have been dealt from the deck
        private int dealtIndex = 0;

        //  Method to set the deck of cards to a specific set of cards
        public void SetDeckOfCards(List<T> deckOfCards)
        {
            // ...
        }

        // Method to shuffles the deck of cards
        public void Shuffle()
        {
            // ...
        }

        // Method that returns the number of remaining cards in the deck
        public int RemainingCards()
        {
            return cards.Count - dealtIndex;
        }

        // Method that deals a specified number of cards from the deck and returns them as an array
        public T[] DealHand(int number)
        {
            // ...

            return null;
        }

        // Method that deals a single card from the top of the deck
        public T DealCard()
        {
            // ...

            return null;
        }
    }

    // Class that represents a playing card
    public abstract class Card
    {
        // Indicates whether the card is available to be dealt
        private bool available = true;

        // Represents the face value of the card
        protected int faceValue;

        // Represents the suit of the card
        protected Suit suit;

        // Constructor
        public Card(int c, Suit s)
        {
            faceValue = c;
            suit = s;
        }

        // Method that must be implemented by subclasses to return the point value of the card in a specific game
        public abstract int Value();

        // Method thatReturns the suit of the card
        public Suit GetSuit()
        {
            return suit;
        }

        // Method that returns whether the card is available to be dealt
        public bool IsAvailable()
        {
            return available;
        }

        // Method that marks the card as unavailable
        public void MarkUnavailable()
        {
            available = false;
        }

        // Method that marks the card as available
        public void MarkAvailable()
        {
            available = true;
        }
    }

    // Class that represents a hand of cards
    public class Hand<T> where T : Card
    {
        // List of cards that stores the cards in the hand
        protected List<T> cards = new List<T>();

        // Method that calculates and returns the score of the hand by summing up the point values of all cards in the hand
        public int Score()
        {
            int score = 0;

            foreach (T card in cards)
            {
                score += card.Value();
            }
            return score;
        }

        // Method that adds a card to the hand
        public void AddCard(T card)
        {
            cards.Add(card);
        }
    }
}


// Q53: Call Center: Imagine you have a call center with three levels of employees: respondent, manager, and director. An incoming telephone call must be first allocated to a respondent who is free. If the respondent can't handle the call, he or she must escalate the call to a manager. If the manager is not free or not able to handle it, then the call should be escalated to a director. Design the classes and data structures for this problem. Implement a method dispatchCall() which assigns a call to the first available employee. (FHQ)

class CallCenter
{
    // Levels of employees in the call center
    public enum Rank
    {
        Respondent = 0, Manager = 1, Director = 2
    }

    public abstract class Employee
    {
        // Represents the call that the employee is currently handling
        private Call currentCall = null;

        // Represents the rank of the employee
        protected Rank rank;

        // Constructor
        public Employee(CallHandler handler)
        {
            // ...
        }

        // Method used to assign the call to an employee
        public void ReceiveCall(Call call)
        {
            // ...
        }

        // Method used to assign new call to an employee after a call has been completed
        public void CallCompleted()
        {
            // ...
        }

        // Method used to escalate a call to the next level of employee if the current employee is unable to handle it
        public void EscalateAndReassign()
        {
            // ...
        }

        // Method used to assign a new call to an employee if they are free
        public bool AssignNewCall()
        {
            // ...
            return false;
        }

        // Method used to check if the employee is free to handle a call
        public bool isFree()
        {
            return currentCall == null;
        }

        // Property to get the rank of an employee
        public Rank GetRank()
        {
            return rank;
        }
    }

    // Subclasses of the employee class to set the appropriate ranks of the employees
    public class Director : Employee
    {
        public Director() : base(null)
        {
            rank = Rank.Director;
        }
    }

    public class Manager : Employee
    {
        public Manager() : base(null)
        {
            rank = Rank.Manager;
        }
    }

    public class Respondent : Employee
    {
        public Respondent() : base(null)
        {
            rank = Rank.Respondent;
        }
    }

    // Represents the person making a call to the call center
    public class Caller
    {
        // ...
    }

    // Class responsible for managing the call queues and assigning calls to employees
    public class CallHandler
    {
        // Number of levels in the call center
        private const int LEVELS = 3;

        // Number of employees at each level
        private const int NUM_RESPONDENTS = 10;
        private const int NUM_MANAGERS = 4;
        private const int NUM_DIRECTORS = 2;

        // List of lists of Employee objects, where each inner list represents a level in the call center hierarchy
        List<List<Employee>> employeeLevels;

        // List of lists of Call objects, where each inner list represents a queue of calls waiting to be handled by employees at a certain level
        List<List<Call>> callQueues;

        // Constructor to initialize the instance variables
        public CallHandler()
        {
            // ...
        }

        // Returns an available employee who can handle the call, or null if no employee is available
        public Employee GetHandlerForCall(Call call)
        {
            // ...
            return null;
        }

        // Overloaded Method that creates a call to be assigned to an employee
        public void DispatchCall(Caller caller)
        {
            Call call = new Call(caller);
            DispatchCall(call);
        }

        // Method that tries to find an available employee to handle the call, If an employee is found, the call is assigned to them. Otherwise, the call is added to the appropriate queue in callQueues
        public void DispatchCall(Call call)
        {
            Employee emp = GetHandlerForCall(call);

            if (emp != null)
            {
                emp.ReceiveCall(call);
                call.SetHandler(emp);
            }
            else
            {
                call.Reply("Please wait for free employee to reply");
                callQueues[Convert.ToInt32(call.GetRank())].Add(call);
            }
        }

        // Method that assigns a call to an available employee
        public bool AssignCall(Employee emp)
        {
            // ...
            return false;
        }
    }

    // Represents a telephone call
    public class Call
    {
        // Property to determine the level of employee that should handle the call
        private Rank rank;

        // Represents the current caller
        private Caller caller;

        // Represents the employee currently handling the call
        private Employee handler;

        // Constructor
        public Call(Caller c)
        {
            rank = Rank.Respondent;
            caller = c;
        }

        // Method to assign a call to an employee when they become available to handle it
        public void SetHandler(Employee e)
        {
            handler = e;
        }

        // Method to send a message to the caller
        public void Reply(string message)
        {
            // ...
        }

        // Method to get the rank of an employee
        public Rank GetRank()
        {
            return rank;
        }

        // Method to set the rank of an employee
        public void SetRank(Rank r)
        {
            rank = r;
        }

        // Method to escalate the call to the next level of employee
        public Rank IncrementRank()
        {
            // ...
            return rank;
        }

        // Method to send a message to the caller when the call ends
        public void Disconnect()
        {
            // ...
        }
    }
}

// Q54: Jukebox: Design a musical jukebox using object-oriented principles. (FHQ)

class Jukebox
{
    // Represents a virtual Jukebox
    public class JukeBox
    {
        // For playing CDs
        private CDPlayer cdPlayer;

        // Represents the current user
        private User user;

        // Represents the collection of CDs available in the Jukebox
        private HashSet<CD> cdCollection;

        // To select songs to play
        private SongSelector ts;

        // Constructor
        public JukeBox(CDPlayer cDPlayer, User user, HashSet<CD> cdCollection, SongSelector ts)
        {
            // ...
        }

        // Method that returns the current song being played
        public Song GetCurrentSong()
        {
            return ts.GetCurrentSong();
        }

        // Method that sets the current user of the Jukebox
        public void SetUser(User u)
        {
            this.user = u;
        }
    }

    // Represents a virtual CD player
    public class CDPlayer
    {
        // Represents the playlist of songs to be played
        private Playlist p;

        // Represents the current CD being played
        private CD c;

        // Constructors
        public CDPlayer(CD c, Playlist p)
        {
            // ...
        }

        public CDPlayer(Playlist p)
        {
            this.p = p;
        }

        public CDPlayer(CD c)
        {
            this.c = c;
        }

        // Method to play a song
        public void PlaySong(Song s)
        {
            // ...
        }

        // Method to get the current playlist
        public Playlist GetPlaylist()
        {
            return p;
        }

        // Method to set the current playlist
        public void SetPlaylist(Playlist p)
        {
            this.p = p;
        }

        // Method to get the current CD
        public CD GetCD()
        {
            return c;
        }

        // Method to set the current CD
        public void SetCD(CD c)
        {
            this.c = c;
        }
    }

    // Represents a playlist of songs
    public class Playlist
    {
        // Represents the current song being played
        private Song song;

        // Represents the queue of songs to be played
        private Queue<Song> queue;

        // Constructor
        public Playlist(Song song, Queue<Song> queue)
        {
            // ...
        }

        // Method to get the next song to be played
        public Song GetNextToPlay()
        {
            return queue.Peek();
        }

        // Method to add a song to the queue
        public void QueueUpSong(Song s)
        {
            queue.Enqueue(s);
        }
    }

    // Represents a virtual CD
    public class CD
    {
        // data for id, artist, songs, etc.
    }

    // Represents a song
    public class Song
    {
        // data for id, CD (could be null), title, length, etc.
    }

    // Represents a virtual song selector in a virtual jukebox
    public class SongSelector
    {
        // Represents a song in the CD collection
        private Song song;

        // Constructor
        public SongSelector(Song song)
        {
            // ...
        }

        // ...

        // Helper method to get the current song
        public Song GetCurrentSong()
        {
            return song;
        }
    }

    // Represents a user
    public class User
    {
        // User's name and ID
        private string name;

        private long ID;

        // Constructor
        public User(string name, long id)
        {
            // ...
        }

        // Method to get the name of the user
        public String GetName()
        {
            return name;
        }

        // Method to set a name for the user
        public void SetName(String name)
        {
            this.name = name;
        }

        // Method to get the user's ID
        public long GetId()
        {
            return ID;
        }

        // Method to set an ID for the user
        public void SetID(long id)
        {
            ID = id;
        }

        // Method to get the current user
        public User GetUser()
        {
            return this;
        }

        // Method to add a new user
        public static User AddUser(String name, long iD)
        {
            // ...

            return new User(name, iD);
        }
    }
}


// Q55: Parking Lot: Design a parking lot using object-oriented principles. (FHQ)

class ParkingLot
{
    // Sizes of the vehicles
    public enum VehicleSize { Motorcycle, Compact, Large }

    // Represents a vehicle that can be parked in the parking lot
    public abstract class Vehicle
    {
        // Represents the parking spots occupied by the vehicle
        protected List<ParkingSpot> parkingSpots = new List<ParkingSpot>();

        // Represents the license number of the vehicle
        protected string licensePlate;

        // Represents the spots needed to park the vehicle
        protected int spotsNeeded;

        // Represents the size of the vehicle
        protected VehicleSize size;

        // Method to get the spots needed to park the vehicle in the parking lot
        public int GetSpotsNeeded()
        {
            return spotsNeeded;
        }

        // Method to get the size of the vehicle
        public VehicleSize GetSize()
        {
            return size;
        }

        // Method to park a vehicle (add the parking spots occupied by the vehicle to the list)
        public void ParkInSpot(ParkingSpot s)
        {
            parkingSpots.Add(s);
        }

        // Method to clear the occupied parking spots
        public void ClearSpots()
        {
            // ...
        }

        // Method to check whether the vehicle can fit in the parking spots
        public abstract bool CanFitInSpot(ParkingSpot spot);
    }

    // Subclasses of Vehicle class that represent a specific type of vehicle each
    public class Bus : Vehicle
    {
        // Constructor
        public Bus()
        {
            spotsNeeded = 5;
            size = VehicleSize.Large;
        }

        // Override the canFitInSpot method for Bus
        public override bool CanFitInSpot(ParkingSpot spot)
        {
            // ...

            return false;
        }
    }

    public class Car : Vehicle
    {
        // Constructor
        public Car()
        {
            spotsNeeded = 1;
            size = VehicleSize.Compact;
        }

        // Override the canFitInSpot method for Car
        public override bool CanFitInSpot(ParkingSpot spot)
        {
            // ...

            return false;
        }
    }


    public class Motorcycle : Vehicle
    {
        // Constructor
        public Motorcycle()
        {
            spotsNeeded = 1;
            size = VehicleSize.Motorcycle;
        }

        // Override the canFitInSpot method for Motorcycle
        public override bool CanFitInSpot(ParkingSpot spot)
        {
            // ...

            return false;
        }
    }

    // Represents the parking lot
    public class ParkingLotArea
    {
        // Array of Levels, each representing a level in the parking lot
        private Level[] levels;

        // Represents the number of levels in the parking lot
        private const int NUM_LEVELS = 5;

        // Constructor
        public ParkingLotArea()
        {
            // ...
        }

        // Method for parking a vehicle, which tries to find a suitable spot for the vehicle on one of the levels
        public bool ParkVehicle(Vehicle vehicle)
        {
            // ...

            return false;
        }
    }

    // Represents a level in the parking lot
    public class Level
    {
        private int floor;
        private int availableSpots = 0;
        private const int SPOTS_PER_ROW = 10;

        // Represents a parking spot on each level
        private ParkingSpot[] spot;

        // Constructor
        public Level(int flr, int numberSpots)
        {
            // ...
        }

        // Method to get the number of spots currently available
        public int AvailableSpots()
        {
            return availableSpots;
        }

        // Method to find available spots where a vehicle can be parked
        private int FindAvailableSpots(Vehicle vehicle)
        {
            // ...

            return 0;
        }

        // Method to park a vehicle
        public bool ParkVehicle(Vehicle vehicle)
        {
            // ...

            return false;
        }

        // Method to check if the vehicle can be parked starting from a specific spot
        private bool ParkStartingAtSpot(int num, Vehicle v)
        {
            // ...

            return false;
        }

        // Method to keep track of the free parking spots on each level
        public void SpotFreed()
        {
            availableSpots++;
        }
    }

    // Represents a parking spot in the parking lot
    public class ParkingSpot
    {
        // Represents a vehicle in the parking spot
        private Vehicle vehicle;

        // Represents the size of the parking spot
        private VehicleSize spotSize;

        // Represents the level at which the parking spot is in the parking lot
        private Level level;

        // Represents the row in which the parking spot is
        private int row;

        // Represents the number in the row at which the parking spot is
        private int spotNumber;

        // Constructor
        public ParkingSpot(Level lvl, int r, int n, VehicleSize s)
        {
            // ...
        }

        // Method to check if the parking spot is available
        public bool IsAvailable(Vehicle vehicle)
        {
            // ...

            return vehicle == null;
        }

        // Method to check if the parking spot can fit a vehicle
        public bool CanFitCar(Vehicle v)
        {
            // ...

            return false;
        }

        // Method to park a vehicle in the parking spot
        public bool Park()
        {
            // ...

            return false;
        }

        // Method to get the row in which the parking spot is
        public int GetRow()
        {
            return row;
        }

        // Method to get the number at which the parking spot is in the row
        public int GetSpotNumber()
        {
            return spotNumber;
        }

        // Method to remove a vehicle from the parking spot
        public void RemoveVehicle()
        {
            // ...
        }
    }
}


// Q56: Online Book Reader: Design the data structures for an online book reader system. (FHQ)

class OnlineBookReader
{
    // Represents the main component of the online reader system
    public class OnlineReaderSystem
    {
        // Represents a library containing books in the online reader system
        private Library library;

        // Manager to manage different users in the online reader system
        private UserManager userManager;

        // Represents the display for the online reader system
        private Display display;

        // Represents the book that is currently active for the user in the online reader system
        private Book activeBook;

        // Represents the user currently active in the online reader system
        private SystemUser activeUser;

        // Constructor
        public OnlineReaderSystem()
        {
            library = new Library();
            userManager = new UserManager();
            display = new Display();
        }

        // Method to get the library for the online reader system
        public Library GetLibrary()
        {
            return library;
        }

        // Method to get the user manager for the online reader system
        public UserManager GetUserManager()
        {
            return userManager;
        }

        // Method to get the display for the online reader system
        public Display GetDisplay()
        {
            return display;
        }

        // Method to get the currently active book
        public Book GetActiveBook()
        {
            return activeBook;
        }

        // Method to set a book to be active and display it in the online reader system
        public void SetActiveBook(Book book)
        {
            activeBook = book;

            display.DisplayBook(book);
        }

        // Method to get the current user of the online reader system
        public SystemUser GetActiveUser()
        {
            return activeUser;
        }

        // Method to change the current user of the online reader system
        public void SetActiveUser(SystemUser user)
        {
            activeUser = user;

            display.DisplayUser(user);
        }
    }

    // Represents the library component of the online reader system
    public class Library
    {
        // To map book IDs to books
        private Dictionary<int, Book> books;

        // Constructor
        public Library()
        {
            books = new Dictionary<int, Book>();
        }

        // Method to add a book in the library
        public Book AddBook(int id, string details)
        {
            if (books.ContainsKey(id))
            {
                return null;
            }
            Book book = new Book(id, details);
            books.Add(id, book);
            return book;
        }

        // Method to remove a book from the library
        public bool RemoveBook(Book b)
        {
            return RemoveBookId(b.GetBookId());
        }

        // Method to remove a book's id from the library
        public bool RemoveBookId(int id)
        {
            if (!books.ContainsKey(id))
            {
                return false;
            }
            books.Remove(id);
            return true;
        }

        // Method to find a book in the library
        public Book FindBook(int id)
        {
            return books[id];
        }
    }

    // Represents the user manager component of the online reader system
    public class UserManager
    {
        // To map user IDs to users
        private Dictionary<int, SystemUser> users;

        // Constructor
        public UserManager()
        {
            users = new Dictionary<int, SystemUser>();
        }

        // Method to add a new user to the user manager
        public SystemUser AddUser(int id, string details, int accountType)
        {
            if (users.ContainsKey(id))
            {
                return null;
            }
            SystemUser user = new SystemUser(id, details, accountType);
            users.Add(id, user);
            return user;
        }

        // Method to find a user in the user manager
        public SystemUser FindUser(int id)
        {
            return users[id];
        }

        // Method to remove a user from the user manager
        public bool RemoveUser(SystemUser u)
        {
            return RemoveUserId(u.GetUserId());
        }

        // Method to remove a user's id from the user manager
        public bool RemoveUserId(int id)
        {
            if (!users.ContainsKey(id))
            {
                return false;
            }
            users.Remove(id);
            return true;
        }
    }

    // Represents the display component of the online reader system
    public class Display
    {
        // Represents the book being displayed
        private Book activeBook;

        // Represents the user being displayed
        private SystemUser activeUser;

        // Represents the book's page being displayed
        private int pageNumber = 0;

        // Method to display the current user
        public void DisplayUser(SystemUser user)
        {
            activeUser = user;
            RefreshUserName();
        }

        // Method to display the current book
        public void DisplayBook(Book book)
        {
            pageNumber = 0;
            activeBook = book;
            RefreshTitle();
            RefreshDetails();
            RefreshPage();
        }

        // Method to display the next page in the book
        public void TurnPageForward()
        {
            pageNumber++;
            RefreshPage();
        }

        // Method to display the previous page in the book
        public void TurnPageBackward()
        {
            pageNumber--;
            RefreshPage();
        }

        // Method to update the username on display
        public void RefreshUserName()
        {
            // ...
        }

        // Method to update the title on display
        public void RefreshTitle()
        {
            // ...
        }

        // Method to update the book's details on the display
        public void RefreshDetails()
        {
            // ...
        }

        // Method to update the page on the display
        public void RefreshPage()
        {
            // ...
        }
    }

    // Represents a book in the online reader system
    public class Book
    {
        // Represents the ID corresponding to a book
        private int bookId;

        // Represents the details of a book
        private string details;

        // Constructor
        public Book(int id, string det)
        {
            bookId = id;
            details = det;
        }

        // Method to get the id of a book
        public int GetBookId()
        {
            return bookId;
        }

        // Method to set an id for a book
        public void SetBookId(int id)
        {
            bookId = id;
        }

        // Method to get the details about a book
        public string GetBookDetails()
        {
            return details;
        }

        // Method to set the details for a book
        public void SetBookDetails(string d)
        {
            details = d;
        }
    }

    // Represents a user in the online reader system
    public class SystemUser
    {
        // Represents the user's id
        private int userId;

        // Represents the type of account the user has
        private int accountType;

        // Represents the details of the user
        private string details;

        // Method to renew membership if it is expired
        public void RenewMembership()
        {
            // ...
        }

        // Constructor
        public SystemUser(int id, string details, int accountType)
        {
            userId = id;
            this.details = details;
            this.accountType = accountType;
        }

        // Method do get the user's id
        public int GetUserId()
        {
            return userId;
        }

        // Method to set a new id for the user
        public void SetUserId(int id)
        {
            userId = id;
        }

        // Method to get the account type for the user
        public int GetAccountType()
        {
            return accountType;
        }

        // Method to set the account type for a user
        public void SetAccountType(int accountType)
        {
            this.accountType = accountType;
        }

        // Method to get the user's details
        public string GetUserDetails()
        {
            return details;
        }

        // Method to set a user's details
        public void SetUserDetails(string d)
        {
            this.details = d;
        }
    }
}


// Q57: Jigsaw: Implement an NxN jigsaw puzzle. Design the data structures and explain an algorithm to solve the puzzle.You can assume that you have a fitsWith method which, when passed two puzzle edges, returns true if the two edges belong together. (FHQ)

// Possible orientations for the edges of a puzzle piece
public enum Orientation
{
    LEFT, TOP, RIGHT, BOTTOM
}

public static class OrientationMethods
{
    //  Returns the opposite orientation
    public static Orientation GetOpposite(this Orientation orientation)
    {
        switch (orientation)
        {
            case Orientation.LEFT:
                return Orientation.RIGHT;

            case Orientation.RIGHT:
                return Orientation.LEFT;

            case Orientation.TOP:
                return Orientation.BOTTOM;

            case Orientation.BOTTOM:
                return Orientation.TOP;

            default:
                throw new ArgumentException("Invalid orientation");
        }

    }
}

// Possible shapes for the edges of a puzzle piece
public enum Shape
{
    INNER, OUTER, FLAT
}

public static class ShapeMethods
{
    // Returns the opposite shape
    public static Shape GetOpposite(this Shape shape)
    {
        switch (shape)
        {
            case Shape.INNER:
                return Shape.OUTER;

            case Shape.OUTER:
                return Shape.INNER;

            default:
                throw new ArgumentException("Invalid shape");
        }
    }
}

// Represents the puzzle board
public class Puzzle
{
    // Represents the puzzle pieces
    private LinkedList<Piece> pieces;

    // Represents the solution to the puzzle
    private Piece[,] solution;

    // Represents the size of the puzzle
    private int size;

    // Constructor
    public Puzzle(int size, LinkedList<Piece> pieces)
    {
        // ...
    }

    // Method to set the edge of a puzzle piece on the solution board at the given row and column with the given orientation
    public void SetEdgeInSolution(LinkedList<Piece> pieces, Edge edge, int row, int column, Orientation orientation)
    {
        Piece piece = edge.parentPiece;
        piece.SetEdgeAsOrientation(edge, orientation);
        pieces.Remove(piece);
        solution[row, column] = piece;
    }

    // Method that returns a boolean indicating whether or not it was able to fit the next edge on the solution board at the given row and column
    public bool FitNextEdge(LinkedList<Piece> piecesToSearch, int row, int col)
    {
        // ...

        return false;
    }

    // Method that returns a boolean indicating whether or not it was able to solve the puzzle
    public bool Solve()
    {
        // ...

        return false;
    }
}

// Represents a puzzle piece
public class Piece
{
    // Represents the edges of the puzzle piece
    private Dictionary<Orientation, Edge> edges = new Dictionary<Orientation, Edge>();

    // Constructor
    public Piece(Edge[] edgeList)
    {
        // ...
    }

    // Method to update the orientation of an edge on a puzzle piece
    public void SetEdgeAsOrientation(Edge edge, Orientation orientation)
    {
        // ...
    }

    // Method that calculates the number of rotations to perform on the edges of the puzzle piece
    public void RotateEdgesBy(int numberRotations)
    {
        // ...
    }

    // Method to check whether the puzzle piece is a corner piece
    public bool IsCorner()
    {
        // ...

        return false;
    }

    // Method to check whether the puzzle piece is a border piece
    public bool IsBorder()
    {
        // ...

        return false;
    }
}

// Represents an edge of a puzzle piece
public class Edge
{
    // Represents shape of a puzzle piece
    private Shape shape;

    // Reference to a puzzle piece's parent Piece
    public Piece parentPiece;

    // Constructor
    public Edge(Shape shape)
    {
        // ...
    }

    // Method that returns a boolean indicating whether or not this edge fits with another edge
    public bool FitsWith(Edge edge)
    {
        // ...

        return false;
    }
}


// Q58: Chat Server: Explain how you would design a chat server. In particular, provide details about the various back-end components, classes, and methods. What would be the hardest problems to solve? (FHQ)

class ChatServer
{
    // Singleton Class responsible for managing users in the system
    public class UserManager
    {
        private static UserManager instance;

        // To keep track of users by their ID
        private Dictionary<int, User> usersById;

        // To keep track of users by their online status
        private Dictionary<int, User> onlineUsers;

        // To keep track of users by their account name
        private Dictionary<string, User> usersByAccountName;

        public static UserManager Instance()
        {
            if (instance == null)
            {
                instance = new UserManager();
            }
            return instance;
        }

        // Method to add a user to the user manager
        public void AddUser(User fromUser, string toAccountName)
        {
            // ...
        }

        // Method to approve an add request
        public void ApproveAddRequest(AddRequest req)
        {
            // ...
        }

        // Method to reject an add request
        public void RejectAddRequest(AddRequest req)
        {
            // ...
        }

        // Method to handle signed on users
        public void UsersSignedOn(string accountName)
        {
            // ...
        }

        // Method to handle signed off users
        public void UsersSignedOff(string accountName)
        {
            // ...
        }
    }

    // Represents a user in the system
    public class User
    {
        // Represents user's id
        private int id;

        // Represents user's status
        private UserStatus status = null;

        // To keep track of the user's private chats
        private Dictionary<int, PrivateChat> privateChats;

        // To keep track of the user's received add requests
        private Dictionary<int, AddRequest> receivedAddRequests;

        // To keep track of the user's sent add requests
        private Dictionary<int, AddRequest> sentAddRequests;

        // To keep track of the user's contacts
        private Dictionary<int, User> contacts;

        private List<GroupChat> groupChats;

        // Represents user's account name
        private string accountName;

        // Represents user's full name
        private string fullName;

        // Constructor
        private User(int id, string accountName, string fullName)
        {
            // ...
        }

        // Method to set the user's status
        private void SetStatus(UserStatus status)
        {
            // ...
        }

        // Method to handle the user's received add requests
        private void ReceivedAddRequest(AddRequest req)
        {
            // ...
        }

        // Method to handle the user's sent add requests
        private void SentAddRequest(AddRequest req)
        {
            // ...
        }

        // Method to remove add requests from the user's account
        private void RemoveAddRequest(AddRequest req)
        {
            // ...
        }

        // Method to approve an add request
        private void RequestAddUser(string accounName)
        {

        }

        // Method to handle user's private chat messages
        private void AddConversation(PrivateChat conversation)
        {
            // ...
        }

        // Method to handle user's group chat messages
        private void AddConversation(GroupChat conversation)
        {
            // ...
        }

        // Method to get the user's status
        private UserStatus GetStatus()
        {
            return null;
        }

        // Method to send messages to other users
        private bool SendMessageToUser(User to, string content)
        {
            return true;
        }

        // Method to send messages to other group chats
        private bool SendMessageToGroup(int id, string cnt)
        {
            return true;
        }

        // Method to add contacts in user's account
        private bool AddContact(User user)
        {
            return true;
        }

        // Method to get the user's ID
        private int GetId()
        {
            return 0;
        }

        // Method to get the user's account name
        private string GetAccountName()
        {
            return "";
        }

        // Method to get the user's full name
        private string GetFullName()
        {
            return "";
        }
    }

    // Represents a conversation between users
    public abstract class Conversation
    {
        // List of participants in the conversation
        protected List<User> participants;

        // List of messages in the conversation
        protected List<Message> messages;

        protected int id;

        // Method to get the messages in the conversation
        public List<Message> GetMessages()
        {
            return null;
        }

        // Method to add new messages to the conversation
        public bool AddMessage(Message m)
        {
            return true;
        }

        public int GetId()
        {
            return 0;
        }
    }

    // Represents a group chat between multiple users
    public class GroupChat : Conversation
    {
        // Method remove participants from the group chat
        public void RemoveParticipant(User user)
        {
            // ...
        }

        // Method to add participants to the group chat
        public void AddParticipant(User user)
        {
            // ...
        }
    }

    // Represents a private chat between two users
    public class PrivateChat : Conversation
    {
        // Constructor
        public PrivateChat(User user1, User user2)
        {
            // ...
        }

        // Method to get the other participant in the chat
        public User GetOtherParticipant(User primary)
        {
            // ...
            return null;
        }
    }

    // Represents a message in a conversation
    public class Message
    {
        // Represents the content of the message
        private string content;

        // Represents the date the message was sent on
        private DateTime date;

        // Constructor
        public Message(string content, DateTime date)
        {
            // ...
        }

        // Method to get the content of the message
        public string GetContent()
        {
            return "";
        }

        // Method to get the date the message was sent on
        public DateTime GetDate()
        {
            return DateTime.Now;
        }
    }

    // Represents an add request between two users
    public class AddRequest
    {
        // Represents the sender of the add request
        private User fromUser;

        // Represents the receiver of the add request
        private User toUser;

        // Represents the status of the add request
        private RequestStatus status;

        // Represents the date the add request was sent on
        private DateTime date;

        // Constructor
        public AddRequest(User from, User toUser, DateTime date)
        {
            // ...
        }

        // Method to get the sender of the add request
        public User GetFromUser()
        {
            return null;
        }

        // Method to get the receiver of the add request
        public User GetToUser()
        {
            return null;
        }

        // Method to get the status of the add request
        public RequestStatus GetStatus()
        {
            return RequestStatus.Unread;
        }

        // Method to get the date the add request was sent on
        public DateTime GetDate()
        {
            return DateTime.Now;
        }
    }

    // Represents the status of a user
    public class UserStatus
    {
        // Represents an optional message to be displayed on the status
        private string message;

        // Represents the status type to be displayed
        private UserStatusType type;

        // Constructor
        public UserStatus(UserStatusType type, string message)
        {
            // ...
        }

        // Method to get the status type being displayed
        public UserStatusType GetStatusType()
        {
            return UserStatusType.Offline;
        }

        // Method to get the optional message being displayed
        public string GetMessage()
        {
            return "";
        }
    }

    // Represents the available status types for the users
    public enum UserStatusType { Offline, Away, Idle, Available, Busy }

    // Represents the status of the add requests in the user's account
    public enum RequestStatus { Unread, Read, Accepted, Rejected }
}


// Q59: Othello: Othello is played as follows: Each Othello piece is white on one side and black on the other. When a piece is surrounded by its opponents on both the left and right sides, or both the top and bottom, it is said to be captured and its color is flipped. On your turn, you must capture at least one of your opponent's pieces. The game ends when either user has no more valid moves. The win is assigned to the person with the most pieces. Implement the object-oriented design for Othello. (FHQ)

class Othello
{
    // Represents the possible directions in the game
    public enum Direction { left, right, up, down }

    // Represents the color of the player's pieces in the game
    public enum Color { White, Black }

    // Singleton class that represents the game
    public class Game
    {
        private static Game instance;

        // Represents the players in the game
        private Player[] players;

        // Represents the game board
        private Board board;

        // Represents the rows and columns on the game board
        private int rows = 10;
        private int columns = 10;

        // Constructor
        private Game()
        {
            board = new Board(rows, columns);
            players = new Player[2];
            players[0] = new Player(Color.Black);
            players[1] = new Player(Color.White);
        }

        // Method that returns the single instance of the class
        public static Game GetInstance()
        {
            if (instance == null)
            {
                instance = new Game();
            }
            return instance;
        }

        // Method that returns the game board
        public Board GetBoard()
        {
            return board;
        }
    }

    // Represents the game board
    public class Board
    {
        // To keep track of the black and white pieces on the game board
        private int blackCount = 0;
        private int whiteCount = 0;

        // Represents the pieces on the board
        private Piece[,] board;

        // Constructor
        public Board(int rows, int columns)
        {
            board = new Piece[rows, columns];
        }

        // Method to initialize the game board
        public void Initialize()
        {
            // ...
        }

        // Method to update the count of pieces on the game board for each player
        public void UpdateScore(Color newColor, int newPieces)
        {
            // ...
        }

        // Method that checks whether or not a piece of the given color can be placed at the given position on the board
        public bool PlaceColor(int row, int column, Color color)
        {
            return false;
        }

        // Method that returns the number of pieces for a player on the board
        public int GetScoreForColor(Color c)
        {
            if (c == Color.Black)
            {
                return blackCount;
            }
            else
            {
                return whiteCount;
            }
        }

        // Method to flip pieces on the board in a direction
        private int FlipSection(int row, int column, Color color, Direction d)
        {
            // ...

            return 0;
        }
    }

    // Represents a single piece on the board
    public class Piece
    {
        // Represents the color of the piece
        private Color color;

        // Constructor
        public Piece(Color c)
        {
            color = c;
        }

        // Method that changes the color of the piece from black to white or vice versa
        public void Flip()
        {
            if (color == Color.Black)
            {
                color = Color.White;
            }
            else
            {
                color = Color.Black;
            }
        }

        // Method that returns the color of the piece
        public Color GetColor()
        {
            return color;
        }
    }

    // Represents a player in the game
    public class Player
    {
        // Represents the color of the player’s pieces
        private Color color;

        // Constructor
        public Player(Color c)
        {
            color = c;
        }

        // Method to get the score for the current player
        public int GetScore()
        {
            // ...

            return 0;
        }

        // Method that attempts to place a piece of the player’s color at the given position on the board
        public bool PlayPiece(int r, int c)
        {
            return Game.GetInstance().GetBoard().PlaceColor(r, c, color);
        }

        // Method that returns the color of the player’s pieces
        public Color GetColor()
        {
            return color;
        }
    }
}


// Q60: Circular Array: Implement a CircularArray class that supports an array-like data structure which can be efficiently rotated. If possible, the class should use a generic type (also called a template), and should support iteration via the standard for (Obj 0 : circularArray) notation.

// Time complexity O(n), Space complexity O(n)

class CircularArray
{
    public class CircularArr<T> : IEnumerable<T>
    {
        // To store the elements of the circular array
        private T[] array;

        // To keep track of the current starting position of the circular array
        private int head = 0;

        // Constructor
        public CircularArr(int size)
        {
            array = new T[size];
        }

        // Method to convert external indices, as seen by users of the class, into internal indices in the underlying array

        // Example: array = [A, B, C, D, E], Call Rotate(2), array stays the same but head = 2, so C is at index 0, which is the head of the circular array
        public int Convert(int index)
        {
            if (index < 0)
            {
                index += array.Length;
            }
            return (head + index) % array.Length;
        }

        // Method that rotates the circular array to the right by the given number of positions
        public void Rotate(int shiftRight)
        {
            head = Convert(shiftRight);
        }

        // Indexer property that allows elements of the circular array to be accessed
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= array.Length)
                {
                    throw new IndexOutOfRangeException();
                }
                return array[Convert(index)];
            }
            set
            {
                if (index < 0 || index >= array.Length)
                {
                    throw new IndexOutOfRangeException();
                }
                array[Convert(index)] = value;
            }
        }

        // Generic and non-generic IEnumerator Methods that allow instances of the CircularArray class to be iterated over
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < array.Length; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}


// Q61: Minesweeper: Design and implement a text-based Minesweeper game. Minesweeper is the classic single - player computer game where an NxN grid has B mines (or bombs) hidden across the grid. The remaining cells are either blank or have a number behind them. The numbers reflect the number of bombs in the surrounding eight cells. The user then uncovers a cell. If it is a bomb, the player loses. If it is a number, the number is exposed. If it is a blank cell, this cell and all adjacent blank cells (up to and including the surrounding numeric cells) are exposed. The player wins when all non-bomb cells are exposed. The player can also flag certain places as potential bombs. This doesn't affect game play, other than to block the user from accidentally clicking a cell that is thought to have a bomb. (FHQ)

class Minesweeper
{
    // Represents a single cell on the board
    public class Cell
    {
        // To keep track of the row in which the cell is
        private int row;

        // To keep track of the column in which the cell is
        private int column;

        // To keep track of the number of bombs adjacent to the cell
        private int number;

        // To keep track of whether the cell contains a bomb
        private bool isBomb;

        // To keep track of whether the cell is exposed
        private bool isExposed = false;

        // To keep track of whether the cell has been marked as a guess by the user
        private bool isGuess = false;

        // Constructor
        public Cell(int r, int c)
        {
            // ...
        }

        // Method to uncover a cell
        public bool Flip()
        {
            isExposed = true;

            return !isBomb;
        }

        // Method to mark/unmark a cell as a guess
        public bool ToggleGuess()
        {
            if (!isExposed)
            {
                isGuess = !isGuess;
            }
            return isGuess;
        }

        // Method to check whether a cell is blank
        public bool IsBlank()
        {
            // ...

            return false;
        }

        // Method to set the row and column for a cell
        public void SetRowAndColumn(int r, int c)
        {
            // ...
        }

        // Method to get the row in which the cell is
        public int GetRow()
        {
            return row;
        }

        // Method to get the column in which the cell is
        public int GetColumn()
        {
            return column;
        }

        // Method to increase the number of bombs adjacent to the cell
        public int IncrementNumber()
        {
            return number++;
        }
    }

    // Represents the game board
    public class Board
    {
        // Number of rows on the board
        private int nRows;

        // Number of columns on the board
        private int nColumns;

        // Number of bombs on the board
        private int nBombs = 0;

        // Number of remaining unexposed cells on the board
        private int numUnexposedRemaining;

        // Represents the cells on the board
        private Cell[,] cells;

        // Represents the bombs on the board
        private Cell[] bombs;

        // Constructor
        public Board(int r, int c, int b)
        {
            // ...
        }

        // Method to create the board
        private void InitializeBoard()
        {

        }

        // Method to uncover a cell on the board
        public bool FlipCell(Cell cell)
        {
            // ...

            return false;
        }

        // Method to check whether a cell is inside the bound of the board
        public bool InBounds(int r, int c)
        {
            // ...

            return false;
        }

        // Method to uncover all adjacent blank cells
        public void ExpandBlank()
        {
            // ...
        }

        // Method to uncover a cell based on user input
        public UserPlayResult PlayFlip(UserPlay userPlay)
        {
            // ...

            return null;
        }

        // Method to get the number of remaining unexposed cells on the board
        public int GetUnexposedRemaining()
        {
            return numUnexposedRemaining;
        }

        // Method to shuffle the cells on the board randomly
        void ShuffleBoard()
        {
            int nCells = nRows * nColumns;

            Random random = new Random();

            for (int index1 = 0; index1 < nCells; index1++)
            {
                int index2 = index1 + random.Next(nCells - index1);

                if (index1 != index2)
                {
                    int row1 = index1 / nColumns;
                    int column1 = (index1 * row1 / nColumns) % nColumns;

                    Cell cell1 = cells[row1, column1];

                    int row2 = index2 / nColumns;
                    int column2 = (index2 * row2 / nColumns) % nColumns;

                    Cell cell2 = cells[row2, column2];

                    cells[row1, column1] = cell2;
                    cell2.SetRowAndColumn(row1, column1);
                    cells[row2, column2] = cell1;
                    cell1.SetRowAndColumn(row2, column2);
                }
            }
        }

        // Method to set the numbers on each cell based on how many adjacent bombs there are
        void SetNumberedCells()
        {
            int[][] deltas =
            {
                new[] {-1, -1}, new[] {-1, 0}, new[] {-1, 1},
                new[] { 0, -1},                new[] { 0, 1},
                new[] { 1, -1}, new[] { 1, 0}, new[] { 1, 1}
            };

            foreach (Cell bomb in bombs)
            {
                int row = bomb.GetRow();
                int col = bomb.GetColumn();

                foreach (int[] delta in deltas)
                {
                    int r = row + delta[0];
                    int c = col + delta[1];

                    if (InBounds(r, c))
                    {
                        cells[r, c].IncrementNumber();
                    }
                }
            }
        }

        // Method to recursively uncover all adjacent blank cells when a blank cell is uncovered
        void ExpandBlank(Cell cell)
        {
            int[][] deltas =
            {
                new[] {-1, -1}, new[] {-1, 0}, new[] {-1, 1},
                new[] { 0, -1},                new[] { 0, 1},
                new[] { 1, -1}, new[] { 1, 0}, new[] { 1, 1}
            };

            Queue<Cell> toExplore = new Queue<Cell>();
            toExplore.Enqueue(cell);

            while (toExplore.Count > 0)
            {
                Cell current = toExplore.Dequeue();
                foreach (int[] delta in deltas)
                {
                    int r = current.GetRow() + delta[0];
                    int c = current.GetColumn() + delta[1];

                    if (InBounds(r, c))
                    {
                        Cell neighbor = cells[r, c];

                        if (FlipCell(neighbor) && neighbor.IsBlank())
                        {
                            toExplore.Enqueue(neighbor);
                        }
                    }
                }
            }
        }
    }

    // Represents a user’s move
    public class UserPlay
    {
        // Represents the row and column at which the user wants to uncover a cell
        private int row;
        private int column;

        // To mark the cell as a guess
        private bool isGuess;
    }

    // Represents the result of a user’s move
    public class UserPlayResult
    {
        // To check whether the move was successful
        private bool successful;

        // To get the resulting game state after a move by the player
        private Game.GameState resultingState;
    }

    // Represents the overall game
    public class Game
    {
        // Represents the possible states of the game
        public enum GameState { WON, LOST, RUNNING }

        // Represents the number of rows, columns and bombs in the game
        private int rows;
        private int columns;
        private int bombs;

        // Represents the game board
        private Board board;

        // Represents the current game state
        private GameState state;

        // Constructor
        public Game(int r, int c, int b)
        {
            // ...
        }

        // Method to create a new game
        public bool Initialize()
        {
            // ...

            return false;
        }

        // Method to start the game
        public bool Start()
        {
            // ...

            return false;
        }

        // Method to play the game
        private bool PlayGame()
        {
            // ...

            return false;
        }
    }
}


// Q61: File System: Explain the data structures and algorithms that you would use to design an in-memory file system. Illustrate with an example in code where possible.

// Time complexity O(n), Space complexity O(n)

class FileSystem
{
    // Represents an entry in a file system
    public abstract class Entry
    {
        // To store the parent directory of an entry
        protected Directory parent;

        // Timestamps to keep track of entry
        protected long created;
        protected long lastUpdated;
        protected long lastAccessed;

        // To store the name of the entry
        protected string name;

        // Constructor
        public Entry(string n, Directory p)
        {
            name = n;
            parent = p;
            created = DateTime.Now.Ticks;
            lastUpdated = DateTime.Now.Ticks;
            lastAccessed = DateTime.Now.Ticks;
        }

        // Method to delete an entry
        public bool Delete()
        {
            if (parent == null)
            {
                return false;
            }
            return parent.DeleteEntry(this);
        }

        // Abstract method to be implemented by any concrete subclass of Entry
        public abstract int Size();

        // Method that returns the full path of the entry by recursively calling the same method on its parent directory until it reaches the root
        public string GetFullPath()
        {
            if (parent == null)
            {
                return name;
            }
            else
            {
                return parent.GetFullPath() + "/" + name;
            }
        }

        // Method to get the name of the entry
        public string GetName()
        {
            return name;
        }

        // Methods to get the creation time of the entry
        public long GetCreationTime()
        {
            return created;
        }

        // Method to get the time at which the entry was last updated
        public long GetLastUpdatedTime()
        {
            return lastUpdated;
        }

        // Method to get the time at which the entry was last accessed
        public long GetLastAccessedTime()
        {
            return lastAccessed;
        }

        // Method to change the name of the entry
        public void ChangeName(string n)
        {
            name = n;
        }
    }

    // Represents a file in the file system
    public class File : Entry
    {
        // To store the content of the file
        private string content;

        // To keep track of the size of the file
        private int size;

        // Constructor
        public File(string n, Directory p, int sz) : base(n, p)
        {
            size = sz;
        }

        // Overridden method to get the size of the file
        public override int Size()
        {
            return size;
        }

        // Method to get the contents of the file
        public string GetContents()
        {
            return content;
        }

        // Method to change the contents of the file
        public void SetContents(string c)
        {
            content = c;
        }
    }

    // Represents a directory in the file system
    public class Directory : Entry
    {
        // To store a list of contents(files or directories)
        protected List<Entry> contents;

        // Constructor
        public Directory(string n, Directory p) : base(n, p)
        {
            contents = new List<Entry>();
        }

        // Overridden method to return the total size of all entries in the directory
        public override int Size()
        {
            int size = 0;

            foreach (Entry e in contents)
            {
                size += e.Size();
            }
            return size;
        }

        // Method to count the number of files in the directory
        public int NumberOfFiles()
        {
            int count = 0;

            foreach (Entry e in contents)
            {
                if (e is Directory)
                {
                    count++;
                    Directory d = (Directory)e;
                    count += d.NumberOfFiles();
                }
                else if (e is File)
                {
                    count++;
                }
            }
            return count;
        }

        // Method to delete entries from the directory
        public bool DeleteEntry(Entry entry)
        {
            return contents.Remove(entry);
        }

        // Method to add entries to the directory
        public void AddEntry(Entry entry)
        {
            contents.Add(entry);
        }

        // Method to get the contents in the directory
        protected List<Entry> GetContents()
        {
            return contents;
        }
    }
}


// Q62: Hash Table: Design and implement a hash table which uses chaining (linked lists) to handle collisions. (FHQ)

// Time complexity O(n), Space complexity O(m)

class HashTable
{
    // Represents the hash table
    public class Hasher<K, V>
    {
        // Represents a node in the linked list
        private class LinkedListNode
        {
            // Pointers to the next and previous node
            public LinkedListNode next;
            public LinkedListNode prev;

            public K key;
            public V value;

            // Constructor
            public LinkedListNode(K k, V v)
            {
                key = k;
                value = v;
            }
        }

        private List<LinkedListNode> arr;

        // Constructor
        public Hasher(int capacity)
        {
            // Each element in this list represents a bucket in the hash table
            arr = new List<LinkedListNode>(capacity);

            for (int i = 0; i < capacity; i++)
            {
                arr.Add(null);
            }
        }

        // Method that takes a key and a value as arguments and adds them to the hash table
        public void Put(K key, V value)
        {
            // Check if there is already a node with the same key in the table
            LinkedListNode node = GetNodeForKey(key);

            // If a node with the same key in the table is found, update its value
            if (node != null)
            {
                node.value = value;
                return;
            }

            // If a node with the same key in the table is not found, create a new node
            node = new LinkedListNode(key, value);

            // Add the new node to the appropriate bucket in the arr list
            int index = GetIndexForKey(key);

            if (arr[index] != null)
            {
                node.next = arr[index];
                node.next.prev = node;
            }
            arr[index] = node;
        }

        // Method to remove the corresponding key-value pair from the hash table
        public void Remove(K key)
        {
            // To find the node with the specified key
            LinkedListNode node = GetNodeForKey(key);

            // Update the pointers of the previous and next nodes in the linked list to remove it
            if (node.prev != null)
            {
                node.prev.next = node.next;
            }
            else
            {
                arr[GetIndexForKey(key)] = node.next;
            }

            if (node.next != null)
            {
                node.next.prev = node.prev;
            }
        }

        // Method that takes a key as an argument and returns the value associated with that key in the hash table
        public V Get(K key)
        {
            // To find the node with the specified key
            LinkedListNode node = GetNodeForKey(key);

            return node == null ? default(V) : node.value;
        }

        // Helper method that takes a key as an argument and returns the node with that key in the hash table
        private LinkedListNode GetNodeForKey(K key)
        {
            // Calculate the index of the appropriate bucket in the arr list using the GetIndexForKey method
            int index = GetIndexForKey(key);

            LinkedListNode current = arr[index];

            // Search for a node with the specified key in the bucket
            while (current != null)
            {
                if (current.key.Equals(key))
                {
                    return current;
                }
                current = current.next;
            }
            return null;
        }

        // Helper method that takes a key as an argument and returns an index into the arr list
        private int GetIndexForKey(K key)
        {
            // Calculate the index by taking the absolute value of the hash code of the key modulo the size of the arr list
            return Math.Abs(key.GetHashCode() % arr.Count);
        }
    }
}


//                                                                   CHAPTER 10: SORTING & SEARCHING


// Common Sorting Algorithms:


// Bubble Sort: [Runtime: O(n^2) average and worst case. Memory: O(1)]

// In bubble sort, we start at the beginning of the array and swap the first two elements if the first is greater than the second. Then, we go to the next pair, and so on, continuously making sweeps of the array until it is sorted. In doing so, the smaller items slowly"bubble" up to the beginning of the list.


// Selection Sort: [Runtime: O(n^2) average and worst case. Memory: O(1)]

// Selection sort is the child's algorithm: simple, but inefficient. Find the smallest element using a linear scan and move it to the front (swapping it with the front element). Then, find the second smallest and move it, again doing a linear scan. Continue doing this until all the elements are in place. 


// Merge Sort: [Runtime: O(n log(n)) average and worst case. Memory: Depends] 

// Merge sort divides the array in half, sorts each of those halves, and then merges them back together. Each of those halves has the same sorting algorithm applied to it. Eventually, you are merging just two single element arrays. It is the "merge" part that does all the heavy lifting. 


// Quick Sort: [Runtime: O(n log(n)) average, O(n2) worst case. Memory: O(log(n))]

// In quick sort, we pick a random element and partition the array, such that all numbers that are less than the partitioning element come before all elements that are greater than it. The partitioning can be performed efficiently through a series of swaps.


// Radix Sort: [Runtime: O(kn)] 

// Radix sort is a sorting algorithm for integers (and some other data types) that takes advantage of the fact that integers have a finite number of bits. In radix sort, we iterate through each digit of the number, grouping numbers by each digit. For example, if we have an array of integers, we might first sort by the first digit, so that the 0s are grouped together. Then, we sort each of these groupings by the next digit. We repeat this process sorting by each subsequent digit. until finally the whole array is sorted.


// Searching Algorithms:

// Binary Search: In binary search, we look for an element x in a sorted array by first comparing x to the midpoint of the array. If x is less than the midpoint, then we search the left half of the array. If x is greater than the midpoint, then we search the right half of the array. We then repeat this process, treating the left and right halves as sub-arrays. Again, we compare x to the midpoint of this sub-array and then search either its left or right side. We repeat this process until we either find x or the sub-array has size O.


// Q63: Sorted Merge: You are given two sorted arrays, A and B, where A has a large enough buffer at the end to hold B. Write a method to merge B into A in sorted order.

// Time complexity O(n), Space complexity O(1)

class SortedMerge
{
    public void Merge(int[] A, int[] B, int lastA, int lastB)
    {
        // Points to the end of the merged array
        int indexMerged = lastB + lastA - 1;

        // Points to the current(last) element being considered in A and B
        int indexA = lastA - 1;
        int indexB = lastB - 1;

        while (indexB >= 0)
        {
            // If the current element in A is larger than that in B, copy it into the current position in the merged array
            if (indexA >= 0 && A[indexA] > B[indexB])
            {
                A[indexMerged] = A[indexA];

                indexA--;
            }
            // If the current element in B is larger or equal than that in B, copy it into the current position in the merged array
            else
            {
                A[indexMerged] = B[indexB];

                indexB--;
            }
            indexMerged--;
        }
    }
}


// Q64: Group Anagrams: Write a method to sort an array of strings so that all the anagrams are next to each other.

// Time complexity O(n*m log(m)), Space complexity O(n*m)

class GroupAnagrams
{
    void Sort(string[] array)
    {
        // Create a new dictionary
        var mapList = new Dictionary<string, List<string>>();

        // Go through each string in the input array
        foreach (var s in array)
        {
            // Sort the characters in s
            var key = SortChars(s);

            // If the key is not already in the dictionary, add it with an empty list as its value
            if (!mapList.ContainsKey(key))
            {
                mapList[key] = new List<string>();
            }

            // Add s to the list of strings for its sorted key
            mapList[key].Add(s);
        }

        // To replace the elements of the original array
        int index = 0;

        // Go through each key in the dictionary
        foreach (var key in mapList.Keys)
        {
            // Get the list of strings for that key
            var list = mapList[key];

            // Go through each string in the list and replace the elements of the original array with these strings
            foreach (var item in list)
            {
                array[index] = item;

                index++;
            }
        }
    }

    // Helper method that sorts the characters in a string
    string SortChars(string s)
    {
        // Convert s into an array of characters
        var content = s.ToCharArray();

        // Sort the array of characters
        Array.Sort(content);

        // Convert the sorted array of characters back into a string and return it
        return new string(content);
    }
}


// Q65: Search in Rotated Array: Given a sorted array of n integers that has been rotated an unknown number of times, write code to find an element in the array. You may assume that the array was originally sorted in increasing order.

// EXAMPLE: Input: Find 5 in {15, 16, 19, 20, 25, 1, 3, 4, 5, 7, 10, 14}
//          Output: 8 (the index of 5 in the array)

// Time complexity O(n), Space complexity O(log n)

class SearchInRotatedArray
{
    static int Search(int[] numbers, int left, int right, int target)
    {
        // Calculate midpoint
        int mid = (left + right) / 2;

        // Target element is found
        if (target == numbers[mid])
        {
            return mid;
        }

        // Check if the search range is empty
        if (right < left)
        {
            return -1;
        }

        // Left is normally ordered
        if (numbers[left] < numbers[mid])
        {
            // Check if target is in the left half
            if (target >= numbers[left] && target < numbers[mid])
            {
                // Search left
                return Search(numbers, left, mid - 1, target);
            }
            else
            {
                // Search right
                return Search(numbers, mid + 1, right, target);
            }
        }

        // Right is normally ordered
        else if (numbers[left] > numbers[mid])
        {
            // Check if target is in the right half
            if (target <= numbers[right] && target > numbers[mid])
            {
                // Search right
                return Search(numbers, mid + 1, right, target);
            }
            else
            {
                // Search left
                return Search(numbers, left, mid - 1, target);
            }
        }

        // If the elements int the left and mid are equal
        else if (numbers[left] == numbers[mid])
        {
            // Check if elements at mid and right are different
            if (numbers[right] != numbers[mid])
            {
                // Search right
                return Search(numbers, mid + 1, right, target);
            }

            // Elements at left, mid and right are equal
            else
            {
                // Search both halves
                int result = Search(numbers, left, mid - 1, target);

                if (result == -1)
                {
                    return Search(numbers, mid + 1, right, target);
                }
                else
                {
                    // Return the result if found in either half
                    return result;
                }
            }
        }
        // Target was not found
        return -1;
    }
}


// Q66: Sorted Search, No Size: You are given an array-like data structure Listy which lacks a size method. It does, however, have an elementAt (i) method that returns the element at index i in 0(1) time. If i is beyond the bounds of the data structure, it returns - 1. (For this reason, the data structure only supports positive integers.) Given a Listy which contains sorted, positive integers, find the index at which an element x occurs. If x occurs multiple times, you may return any index.

// Time complexity O(log n), Space complexity O(1)

class SortedSearch
{
    // Class to avoid errors
    public class Listy
    {
        public Listy()
        {
            // ...
        }

        public int elementAt(int index)
        {
            return index;
        }
    }

    int Search(Listy list, int value)
    {
        int index = 1;

        // Double the value of index until the end of the list is found or the target value is found to be at a later index than the current index
        while (list.elementAt(index) != -1 && list.elementAt(index) < value)
        {
            index *= 2;
        }

        // Perform binary search on the list
        return BinarySearch(list, value, index / 2, index);
    }

    // Method to perform binary search on the input list
    int BinarySearch(Listy list, int value, int low, int high)
    {
        int mid;

        // Iterate through the elements of the list till the value is found
        while (low <= high)
        {
            // Calculate midpoint
            mid = (low + high) / 2;

            // Get the element at midpoint
            int middle = list.elementAt(mid);

            // Check if the middle element is greater than the target value or we are past the end of the list
            if (middle > value && middle == -1)
            {
                // Search in lower indices
                high = mid - 1;
            }

            // The middle element is smaller than the target value
            else if (middle < value)
            {
                // Search in higher indices
                low = mid + 1;
            }

            // The middle element is equal the target value
            else
            {
                return mid;
            }
        }

        // Target value was not found in the list
        return -1;
    }
}


// Q67: Sparse Search: Given a sorted array of strings that is interspersed with empty strings, write a method to find the location of a given string.

// EXAMPLE: Input: ball, {"at", "", "", "", "ball", "", "", "car", "", "", "dad", "", ""}
//          Output: 4

// Time complexity O(log n), Space complexity O(log n)

class SparseSearch
{
    int Search(string[] strings, string str, int first, int last)
    {
        // Check if the search range is empty
        if (first > last)
        {
            return -1;
        }

        // Calculate midpoint
        int mid = (last + first) / 2;

        // Check if the string at midpoint is empty
        if (!string.IsNullOrEmpty(strings[mid]))
        {
            // Initialize left and right indices of the midpoint
            int left = mid - 1;
            int right = mid + 1;

            // Iterate through the array until out of bounds or a non-empty string is found
            while (true)
            {
                // Out of the bounds of the array
                if (left < first && right > last)
                {
                    return -1;
                }

                // Non-empty string found on the right of the midpoint
                else if (right <= last && !string.IsNullOrEmpty(strings[right]))
                {
                    mid = right;
                    break;
                }

                // Non-empty string found on the left of the midpoint
                else if (left >= first && !string.IsNullOrEmpty(strings[left]))
                {
                    mid = left;
                    break;
                }

                right++;
                left--;
            }
        }

        // If the string at midpoint is equal to the target string
        if (str.Equals(strings[mid]))
        {
            return mid;
        }

        // the target string is lexicographically greater than the string at midpoint
        else if (string.Compare(strings[mid], str) < 0)
        {
            // Search on the right side
            return Search(strings, str, mid + 1, last);
        }

        // the target string is lexicographically less than the string at midpoint
        else
        {
            // Search on the left side
            return Search(strings, str, first, mid - 1);
        }

        // Helper method for the main search function
        int SearchHelper(string[] strings, string str)
        {
            if (strings == null || str == null || str == "")
            {
                return -1;
            }

            // Call to the search function with full range of the input array
            return Search(strings, str, 0, strings.Length - 1);
        }
    }
}


// Q68: Sort Big File: Imagine you have a 20 GB file with one string per line. Explain how you would sort the file.

// We'll divide the file into chunks, which are x megabytes each, where x is the amount of memory we have available. Each chunk is sorted separately and then saved back to the file system. Once all the chunks are sorted, we merge the chunks, one by one. At the end, we have a fully sorted file. This algorithm is known as external sort.


// Q69: Missing Int: Given an input file with four billion non-negative integers, provide an algorithm to generate an integer that is not contained in the file. Assume you have 1 GB of memory available for this task. (FHQ)

// Time complexity O(n), Space complexity O(1)

class MissingInt
{
    // Calculate the number of integers that can be represented by an int
    static long numberOfInts = ((long)int.MaxValue + 1);

    // Create a bitfield(array of bytes), each byte in the array represents 8 integers, size of the array is the total number of int values divided by 8, rounded down
    byte[] bitfield = new byte[(int)(numberOfInts / 8)];

    // Filename of the file to be read
    string fileName = "...";

    void FindOpenNumber()
    {
        // Open the file with the given filename for reading
        using (StreamReader sr = new StreamReader(fileName))
        {
            string line;

            // Read each line from the file, parse it as an integer, and set the corresponding bit in the bitfield to 1
            while ((line = sr.ReadLine()) != null)
            {
                int n = int.Parse(line);

                // Set the current bit of the bitfield to 1
                bitfield[n / 8] |= (byte)(1 << (n % 8));
            }
        }

        // Iterate over each bit in the bitfield
        for (int i = 0; i < bitfield.Length; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                // Check if a bit is found that is still 0 (integer not present in the file)
                if ((bitfield[i] & (1 << j)) == 0)
                {
                    // Print and return the integer not present in the file
                    Console.WriteLine(i * 8 + j);

                    return;
                }
            }
        }
    }


    // FOLLOW UP: What if you have only 10MB of memory? Assume that all the values are distinct and we now have no more than one billion non-negative integers.

    // Time complexity O(n), Space complexity O(1)

    public int FindNumber(string fileName)
    {
        // Set the range size to 2^20, which is about one million
        int rangeSize = (1 << 20);
        int[] blocks = GetCountPerBlock(fileName, rangeSize);
        int blockIndex = FindBlockWithMissing(blocks, rangeSize);

        // Check if all numbers are already present in the array
        if (blockIndex < 0)
        {
            return -1;
        }

        byte[] bitVector = GetBitVectorForRange(fileName, blockIndex, rangeSize);

        int offset = FindZero(bitVector);

        // Check if no zero bit is found, it means all numbers are present in this range
        if (offset < 0)
        {
            return -1;
        }

        // Zero bit is found, calculate the corresponding number and return it
        return rangeSize * blockIndex + offset;
    }

    // Method to read the file and return an array where each element represents the count of numbers in a specific range in the file
    int[] GetCountPerBlock(string fileName, int rangeSize)
    {
        int arraySize = int.MaxValue / rangeSize + 1;
        int[] blocks = new int[arraySize];

        using (StreamReader sr = new StreamReader(fileName))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                int value = int.Parse(line);
                blocks[value / rangeSize]++;
            }
        }
        return blocks;
    }

    // Method to find the first block that contains less than rangeSize numbers. This means that there must be at least one number missing in this range
    int FindBlockWithMissing(int[] blocks, int rangeSize)
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i] < rangeSize)
            {
                return i;
            }
        }
        return -1;
    }

    // Method to create a bit vector for the found block's range. Each bit in the vector represents whether a number is present in the file
    byte[] GetBitVectorForRange(string fileName, int blockIndex, int rangeSize)
    {
        int startRange = blockIndex * rangeSize;
        int endRange = startRange + rangeSize;

        byte[] bitVector = new byte[rangeSize / 8];

        using (StreamReader sr = new StreamReader(fileName))
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                int value = int.Parse(line);

                if (startRange <= value && value < endRange)
                {
                    int offset = value - rangeSize;
                    bitVector[offset / 8] |= (byte)(1 << (offset % 8));
                }
            }
        }
        return bitVector;
    }

    // Helper method for the FindZero method
    int FindZero(byte b)
    {
        for (int i = 0; i < 8; i++)
        {
            if ((b & (1 << i)) == 0)
            {
                return i;
            }
        }
        return -1;
    }

    // Method to find the first zero bit in the bit vector, which represents the first missing number in the range
    int FindZero(byte[] bitVector)
    {
        for (int i = 0; i < bitVector.Length; i++)
        {
            if (bitVector[i] != ~0)
            {
                int bitIndex = FindZero(bitVector[i]);

                return i * 8 + bitIndex;
            }
        }
        return -1;
    }
}


// Q70: Find Duplicates: You have an array with all the numbers from 1 to N, where N is at most 32,000. The array may have duplicate entries and you do not know what N is. With only 4 kilobytes of memory available, how would you print all duplicate elements in the array?

// Time complexity O(n), Space complexity O(n)

class FindDuplicates
{
    void CheckDuplicates(int[] array)
    {
        // Create a new BitSet object that can hold 32000 bits
        BitSet bs = new BitSet(32000);

        // Iterate over each element in the input array
        for (int i = 0; i < array.Length; i++)
        {
            // Get the current number from the array and subtract one to get the index for the BitSet
            int num = array[i];
            int num0 = num - 1;

            // Check if the bit at index num0 in the BitSet is set. If it is, it means the number is a duplicate, so print the number
            if (bs.Get(num0))
            {
                Console.WriteLine(num);
            }

            // The bit at num0 in the BitSet is not set, set the bit to mark that this number has been seen
            else
            {
                bs.Set(num0);
            }
        }
    }

    // Simple implementation of a bit vector
    public class BitSet
    {
        // Array of integers, where each bit in an integer represents whether a number is present (bit is set) or absent (bit is not set)
        int[] bitset;

        public BitSet(int size)
        {
            bitset = new int[(size >> 5) + 1];
        }

        // Method to check if a bit at a specific position is set
        public bool Get(int pos)
        {
            int wordNumber = (pos >> 5);
            int bitNumber = (pos & 0x1F);

            return (bitset[wordNumber] & (1 << bitNumber)) != 0;
        }

        // Method to set a bit at a specific position
        public void Set(int pos)
        {
            int wordNumber = (pos >> 5);
            int bitNumber = (pos & 0x1F);

            bitset[wordNumber] |= 1 << bitNumber;
        }
    }
}


// Q71: Sorted Matrix Search: Given an M x N matrix in which each row and each column is sorted in ascending order, write a method to find an element.

// Time complexity O(log n), Space complexity O(1)

class SortedMatrixSearch
{
    public class Coordinate : ICloneable
    {
        // Current Row and column of the matrix
        public int row, column;

        // Constructor
        public Coordinate(int r, int c)
        {
            row = r;
            column = c;
        }

        // Method to check if the coordinate is within the bounds of the matrix
        public bool InBounds(int[,] matrix)
        {
            return row >= 0 && column >= 0 && row < matrix.GetLength(0) && column < matrix.GetLength(1);
        }

        // Method to check if current coordinate comes before another coordinate
        public bool IsBefore(Coordinate p)
        {
            return row <= p.row && column <= p.column;
        }

        // Method to create a copy of current coordinate
        public object Clone()
        {
            return new Coordinate(row, column);
        }

        // Method to set current coordinate to the average of two other coordinates
        public void SetToAverage(Coordinate min, Coordinate max)
        {
            row = (min.row + max.row) / 2;
            column = (min.column + max.column) / 2;
        }
    }

    // Overloaded Method to search for x in the sub-matrix defined by the origin and destination coordinates
    Coordinate FindElement(int[,] matrix, Coordinate origin, Coordinate destination, int x)
    {
        if (!origin.InBounds(matrix) || !destination.InBounds(matrix))
        {
            return null;
        }

        if (matrix[origin.row, origin.column] == x)
        {
            return origin;
        }
        else if (!origin.IsBefore(destination))
        {
            return null;
        }

        int diagonalDistance = Math.Min(destination.row - origin.row, destination.column - origin.column);

        Coordinate start = (Coordinate)origin.Clone();
        Coordinate end = new Coordinate(start.row + diagonalDistance, start.column + diagonalDistance);
        Coordinate p = new Coordinate(0, 0);

        while (start.IsBefore(end))
        {
            p.SetToAverage(start, end);

            if (x > matrix[p.row, p.column])
            {
                start.row = p.row + 1;
                start.column = p.column + 1;
            }
            else
            {
                end.row = p.row - 1;
                end.column = p.column - 1;
            }
        }

        return PartitionAndSearch(matrix, origin, destination, start, x);
    }

    // Method to partition the matrix into two halves and recursively search in the appropriate half
    Coordinate PartitionAndSearch(int[,] matrix, Coordinate origin, Coordinate destination, Coordinate pivot, int x)
    {
        Coordinate lowerLeftOrigin = new Coordinate(pivot.row, origin.column);
        Coordinate lowerLeftDestination = new Coordinate(destination.row, pivot.column - 1);
        Coordinate upperRightOrigin = new Coordinate(origin.row, pivot.column);
        Coordinate upperRightDestination = new Coordinate(pivot.row - 1, destination.column);

        Coordinate lowerLeft = FindElement(matrix, lowerLeftOrigin, lowerLeftDestination, x);

        if (lowerLeft == null)
        {
            return FindElement(matrix, upperRightOrigin, upperRightDestination, x);
        }

        return lowerLeft;
    }

    // Method to search for x in the entire matrix
    Coordinate FindElement(int[,] matrix, int x)
    {
        Coordinate origin = new Coordinate(0, 0);
        Coordinate destination = new Coordinate(matrix.GetLength(0) - 1, matrix.GetLength(1) - 1);

        return FindElement(matrix, origin, destination, x);
    }
}


// Q72: Rank from Stream: Imagine you are reading in a stream of integers. Periodically, you wish to be able to look up the rank of a number x (the number of values less than or equal to x). Implement the data structures and algorithms to support these operations. That is, implement the method track(int x), which is called when each number is generated, and the method getRankOfNumber(int x), which returns the number of values less than or equal to x (not including x itself). 

// EXAMPLE: Stream(in order of appearance): 5, 1, 4, 4, 5, 9, 7, 13, 3
//          getRankOfNumber(1) = 0
//          getRankOfNumber(3) = 1
//          getRankOfNumber(4) = 3

// Time complexity O(n), Space complexity O(n)

class RankFromStream
{
    // Represents a node in the binary search tree
    public class RankNode
    {
        // Size of the left subtree
        public int leftSize = 0;

        // Value of the node
        public int data = 0;

        // Pointers to the left and right child nodes
        public RankNode left, right;

        // Constructor
        public RankNode(int d)
        {
            data = d;
        }

        // Method to insert a number into the BST
        public void Insert(int d)
        {
            // If the number is less than or equal to the current node’s data, it goes to the left subtree
            if (d <= data)
            {
                if (left != null)
                {
                    left.Insert(d);
                }
                else
                {
                    left = new RankNode(d);

                    leftSize++;
                }
            }

            // The number is greater than the current node’s data, it goes to the right subtree
            else
            {
                if (right != null)
                {
                    right.Insert(d);
                }
                else
                {
                    right = new RankNode(d);
                }
            }
        }

        // Method to return the rank of a number in the BST
        public int GetRank(int d)
        {
            // If the number is equal to the current node’s data, return leftSize
            if (d == data)
            {
                return leftSize;
            }

            // If the number is less than the current node’s data, it goes to the left subtree
            else if (d < data)
            {
                if (left == null)
                {
                    return -1;
                }
                else
                {
                    return left.GetRank(d);
                }
            }

            // The number is greater than the current node’s data, it goes to the right subtree and adds left_size + 1 to the rank
            else
            {
                int rightRank = right == null ? -1 : right.GetRank(d);

                if (rightRank == -1)
                {
                    return -1;
                }
                else
                {
                    return leftSize + 1 + rightRank;
                }
            }
        }
    }

    // To track numbers and their ranks
    public class RankTracker
    {
        // Root node of the BST
        private RankNode root = null;

        // Method to insert a number into the BST
        public void Tracker(int number)
        {
            if (root == null)
            {
                root = new RankNode(number);
            }
            else
            {
                root.Insert(number);
            }
        }

        // Method to return the rank of a number
        public int GetRankOfNumber(int number)
        {
            if (root == null)
            {
                return -1;
            }

            return root.GetRank(number);
        }
    }
}


// Q73: Peaks and Valleys: In an array of integers, a "peak" is an element which is greater than or equal to the adjacent integers and a "valley" is an element which is less than or equal to the adjacent integers. For example, in the array {S, 8, 6, 2, 3, 4, 6}, { 8, 6} are peaks and {S, 2} are valleys. Given an array of integers, sort the array into an alternating sequence of peaks and valleys. 

// EXAMPLE: Input: { 5, 3, 1, 2, 3}
//          Output: { 5, 1,3, 2, 3}

// Time complexity O(n), Space complexity O(1)

class PeaksAndValleys
{
    public class SortValleyPeak
    {
        public void Sort(int[] array)
        {
            // Iterate over the array, jumping two indices at a time
            for (int i = 1; i < array.Length; i += 2)
            {
                // Find the maximum of the current element and its neighbours 
                int biggestIndex = MaxIndex(array, i - 1, i, i + 1);

                // Swap the current element with the maximum if the current element is not already the maximum
                if (i != biggestIndex)
                {
                    Swap(array, i, biggestIndex);
                }
            }
        }

        // Method to return the index out of the three that has the maximum value and if an index is out of the bounds of the array, assign it a value of int.MinValue
        public int MaxIndex(int[] array, int a, int b, int c)
        {
            int len = array.Length;
            int aValue = (a >= 0 && a < len) ? array[a] : int.MinValue;
            int bValue = (b >= 0 && b < len) ? array[b] : int.MinValue;
            int cValue = (c >= 0 && c < len) ? array[c] : int.MinValue;
            int max = Math.Max(aValue, Math.Max(bValue, cValue));

            if (aValue == max)
            {
                return a;
            }
            else if (bValue == max)
            {
                return b;
            }
            else
            {
                return c;
            }
        }

        // Method to swap the elements at two given indices in the array
        public void Swap(int[] array, int i, int j)
        {
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}


//                                                                   CHAPTER 11: TESTING


// Q74: Mistake: Find the mistake(s) in the following code:

//      uint i;
//      for (i = 100; i >= 0; i--)
//      {
//          Console.WriteLine("{0}", i);
//      }

// Unsigned int is, by definition, always greater than or equal to zero. The for loop condition will therefore always be true, and it will loop infinitely.

// Correct code
class Mistake
{
    void CorrectCode()
    {
        uint i;

        for (i = 100; i > 0; i--)
        {
            Console.WriteLine("{0}", i);
        }
    }
}


// Q75: Random Crashes: You are given the source to an application which crashes when it is run. After running it ten times in a debugger, you find it never crashes in the same place. The application is single threaded, and uses only the C standard library. What programming errors could be causing this crash? How would you test each one?

// 1. Random Variable: The application may use some random number or variable component that may not be fixed for every execution of the program. Examples include user input, a random number generated by the program, or the time of day.

// 2. Uninitialized Variable: The application could have an uninitialized variable which, in some languages, may cause it to take on an arbitrary value. The values of this variable could result in the code taking a slightly different path each time.

// 3. Memory Leak: The program may have run out of memory. Other culprits are totally random for each run since it depends on the number of processes running at that particular time. This also includes heap overflow or corruption of data on the stack.

// 4. External Dependencies: The program may depend on another application, machine, or resource. If there are multiple dependencies, the program could crash at any point.


// Q76: Chess Test: We have the following method used in a chess game: boolean canMoveTo( int X, int y). This method is part of the Piece class and returns whether or not the piece can move to position (x, y). Explain how you would test this method.

// foreach piece a:
//   for each other type of piece b (6 types + empty space)
//     for each direction d
//       Create a board with piece a.
//       Place piece b in direction d.
//       Try to move - check return value.


// Q77: No Test Tools: How would you load test a web page without using any test tools?

// In the absence of formal testing tools, we can basically create our own. For example, we could simulate concurrent users by creating thousands of virtual users. We would write a multi-threaded program with thousands of threads, where each thread acts as a real-world user loading the page. For each user, we would programmatically measure response time, system load etc.


// Q78: Test a Pen: How would you test a pen?

// Questions about the pen: 1. Who is going to use the pen?
//                          2. Will it be used for writing, drawing, or doing something else with it?
//                          3. What surface will it be used on? Paper? Clothing? Walls? etc.
//                          4. What kind of tip does the pen have? Felt? Ballpoint? Is it intended to wash off, or is it intended to be permanent?

// Solution: Fact check: Verify that the pen has the correct tip and that the ink is one of the allowed colors.
//           Intended use: Does the pen write properly on the intended surface?
//           Intended use: Does it wash off of the surface? Does it wash off in hot, warm and cold water?
//           Safety: Is the pen safe (non-toxic) for the user?
//           Unintended uses: How else might the user use the pen? They might write on other surfaces, so you need to check whether the behavior there is correct. They might also stomp on the pen, throw it, and so on.


// Q79: Test an ATM: How would you test an ATM in a distributed banking system?

// Questions about the ATM: 1. Who is going to use the ATM? Anyone? Blind people? etc.
//                          2. What are they going to use it for? Withdrawing money? Transferring money? Checking their balance? etc.
//                          3. What tools do we have to test? Do we have access to the code, or just to the ATM?

// Solution Methods:

// Manual Testing: Manual testing would involve making sure to check for all the error cases (logging in, withdrawing money, depositing money, checking balance, transferring money, low balance, new account, nonexistent account, and so on).

// Automated Testing: Automated testing is a bit more complex. We'll want to automate all the standard scenarios, and we also want to look for some very specific issues, such as race conditions. Ideally, we would be able to set up a closed system with fake accounts and ensure that, even if someone withdraws and deposits money rapidly from different locations, he never gets money or loses money that he shouldn't.